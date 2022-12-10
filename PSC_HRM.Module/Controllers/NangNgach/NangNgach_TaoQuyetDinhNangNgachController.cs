using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.NangNgach;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Controllers
{
    public partial class NangNgach_TaoQuyetDinhNangNgachController : ViewController
    {
        public NangNgach_TaoQuyetDinhNangNgachController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NangNgach_TaoQuyetDinhNangNgachController");
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNangNgach>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            {//Lưu dữ liệu hiện tại lại
                XafApplicationHelper.CascadeCommit(View.ObjectSpace, false);
                View.ObjectSpace.Refresh();
            }
            //
            DeNghiNangNgach deNghi = View.CurrentObject as DeNghiNangNgach;
            if (deNghi != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();

                if (deNghi.ListChiTietDeNghiNangNgach.Count > 1)
                {
                    if (DialogUtil.ShowYesNo("Bạn có muốn lập mỗi quyết định cho 1 người không?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Tạo quyết định một người
                        CreateQuyetDinhMotNguoi(deNghi, obs);
                    }
                    else
                    {
                        //Tạo quyết định nhiều người
                        CreateQuyetDinhNhieuNguoi(deNghi, obs);
                    }
                }
                else
                {
                    //Tạo quyết định nhiều người
                    CreateQuyetDinhNhieuNguoi(deNghi, obs);
                }
            }
        }

        private void CreateQuyetDinhMotNguoi(DeNghiNangNgach deNghi, IObjectSpace obs)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                uow.BeginTransaction();

                try
                {
                    int count = 0;
                    foreach (ChiTietDeNghiNangNgach item in deNghi.ListChiTietDeNghiNangNgach)
                    {
                        QuyetDinhNangNgach quyetDinh = uow.FindObject<QuyetDinhNangNgach>(CriteriaOperator.Parse("DeNghiNangNgach=?", deNghi.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = new QuyetDinhNangNgach(uow);
                            quyetDinh.SoQuyetDinh = item.SoQuyetDinh;
                            quyetDinh.QuyetDinhMoi = true;
                            quyetDinh.DeNghiNangNgach = uow.GetObjectByKey<DeNghiNangNgach>(deNghi.Oid);

                            ChiTietQuyetDinhNangNgach chiTiet = uow.FindObject<ChiTietQuyetDinhNangNgach>(CriteriaOperator.Parse("QuyetDinhNangNgach=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                            if (chiTiet == null)
                            {
                                chiTiet = new ChiTietQuyetDinhNangNgach(uow);
                                chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                chiTiet.NgachLuongCu = uow.GetObjectByKey<NgachLuong>(item.NgachLuongCu.Oid);
                                chiTiet.BacLuongCu = uow.GetObjectByKey<BacLuong>(item.BacLuongCu.Oid);
                                chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                                chiTiet.NgayBoNhiemNgachCu = item.NgayBoNhiemNgachCu;
                                chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                                chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                                chiTiet.NgachLuongMoi = uow.GetObjectByKey<NgachLuong>(item.NgachLuongMoi.Oid);
                                chiTiet.BacLuongMoi = uow.GetObjectByKey<BacLuong>(item.BacLuongMoi.Oid);
                                chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                                chiTiet.NgayBoNhiemNgachMoi = item.NgayBoNhiemNgachMoi;
                                chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                                chiTiet.NgayHuongLuongMoi = item.NgayHuongLuongMoi;

                                quyetDinh.NgayPhatSinhBienDong = item.NgayHuongLuongMoi;
                                //
                                quyetDinh.ListChiTietQuyetDinhNangNgach.Add(chiTiet);
                            }

                            {//Trường hợp nếu trường không có hiệu trưởng như đại học công nghiệp thì cấu hình người ký tên của quyết định trong bảng người sử dụng

                                if (quyetDinh.NguoiKy == null && HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen != null)
                                {
                                    quyetDinh.ChucVuNguoiKy = uow.GetObjectByKey<ChucVu>(HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen.ChucVu.Oid);
                                    quyetDinh.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen.Oid);

                                }
                            }

                            count += 1;
                        }
                    }

                    //Tiến hành lưu dữ liệu
                    uow.CommitChanges();

                    DialogUtil.ShowInfo(String.Format("Thành công! Đã tạo được {0} quyết định nâng ngạch.", count));
                }
                catch (Exception ex)
                {
                    uow.RollbackTransaction();
                    //
                    DialogUtil.ShowError("Đã xảy ra lỗi trong quá trình tạo quyết định." + ex.Message);
                }
            }
        }
        private void CreateQuyetDinhNhieuNguoi(DeNghiNangNgach deNghi, IObjectSpace obs)
        {
            QuyetDinhNangNgach quyetDinh = obs.FindObject<QuyetDinhNangNgach>(CriteriaOperator.Parse("DeNghiNangNgach=?", deNghi.Oid));
            if (quyetDinh == null)
            {
                quyetDinh = obs.CreateObject<QuyetDinhNangNgach>();
                quyetDinh.SoQuyetDinh = "   /QĐNN  ";
                quyetDinh.QuyetDinhMoi = true;
                quyetDinh.DeNghiNangNgach = obs.GetObjectByKey<DeNghiNangNgach>(deNghi.Oid);

                ChiTietQuyetDinhNangNgach chiTiet;
                foreach (ChiTietDeNghiNangNgach item in deNghi.ListChiTietDeNghiNangNgach)
                {
                    chiTiet = obs.FindObject<ChiTietQuyetDinhNangNgach>(CriteriaOperator.Parse("QuyetDinhNangNgach=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                    if (chiTiet == null)
                    {
                        chiTiet = obs.CreateObject<ChiTietQuyetDinhNangNgach>();
                        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        chiTiet.NgachLuongCu = obs.GetObjectByKey<NgachLuong>(item.NgachLuongCu.Oid);
                        chiTiet.BacLuongCu = obs.GetObjectByKey<BacLuong>(item.BacLuongCu.Oid);
                        chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                        chiTiet.NgayBoNhiemNgachCu = item.NgayBoNhiemNgachCu;
                        chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                        chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                        chiTiet.NgachLuongMoi = obs.GetObjectByKey<NgachLuong>(item.NgachLuongMoi.Oid);
                        chiTiet.BacLuongMoi = obs.GetObjectByKey<BacLuong>(item.BacLuongMoi.Oid);
                        chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                        chiTiet.NgayBoNhiemNgachMoi = item.NgayBoNhiemNgachMoi;
                        chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                        chiTiet.NgayHuongLuongMoi = item.NgayHuongLuongMoi;
                        quyetDinh.ListChiTietQuyetDinhNangNgach.Add(chiTiet);
                    }
                }
            }
            

            Application.ShowView<QuyetDinhNangNgach>(obs, quyetDinh);
        }
    }
}
