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
using PSC_HRM.Module.TapSu;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Controllers
{
    public partial class TapSu_TaoQuyetDinhBoNhiemNgachController : ViewController
    {
        public TapSu_TaoQuyetDinhBoNhiemNgachController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TapSu_TaoQuyetDinhBoNhiemNgachController");
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhBoNhiemNgach>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            {//Tiến hành lưu dữ liệu hiện tại
                XafApplicationHelper.CascadeCommit(View.ObjectSpace, false);
                View.ObjectSpace.Refresh();
            }
            //
            DeNghiBoNhiemNgach deNghi = View.CurrentObject as DeNghiBoNhiemNgach;
            if (deNghi != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();

                if (deNghi.ListChiTietDeNghiBoNhiemNgach.Count > 1)
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

        private void CreateQuyetDinhMotNguoi(DeNghiBoNhiemNgach deNghi, IObjectSpace obs)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                uow.BeginTransaction();
                //
                try
                {
                    int count = 0;
                    foreach (ChiTietDeNghiBoNhiemNgach item in deNghi.ListChiTietDeNghiBoNhiemNgach)
                    {
                        QuyetDinhBoNhiemNgach quyetDinh = uow.FindObject<QuyetDinhBoNhiemNgach>(CriteriaOperator.Parse("DeNghiBoNhiemNgach=?", deNghi.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = new QuyetDinhBoNhiemNgach(uow);
                            quyetDinh.SoQuyetDinh = item.SoQuyetDinh;
                            quyetDinh.QuyetDinhMoi = true;
                            quyetDinh.DeNghiBoNhiemNgach = uow.GetObjectByKey<DeNghiBoNhiemNgach>(deNghi.Oid);
                            //
                            ChiTietQuyetDinhBoNhiemNgach chiTiet = uow.FindObject<ChiTietQuyetDinhBoNhiemNgach>(CriteriaOperator.Parse("QuyetDinhBoNhiemNgach=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                            if (chiTiet == null)
                            {
                                chiTiet = new ChiTietQuyetDinhBoNhiemNgach(uow);
                                chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                chiTiet.NgachLuong = uow.GetObjectByKey<NgachLuong>(item.NgachLuong.Oid);
                                chiTiet.BacLuong = uow.GetObjectByKey<BacLuong>(item.BacLuong.Oid);
                                chiTiet.HeSoLuong = item.HeSoLuong;
                                chiTiet.NgayBoNhiemNgach = item.NgayBoNhiemNgach;
                                chiTiet.NgayHuongLuong = item.NgayHuongLuong;
                                chiTiet.MocNangLuong = item.MocNangLuong;
                                //
                                quyetDinh.NgayPhatSinhBienDong = item.NgayHuongLuong;
                                //
                                quyetDinh.ListChiTietQuyetDinhBoNhiemNgach.Add(chiTiet);
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

                    DialogUtil.ShowInfo(String.Format("Thành công! Đã tạo được {0} quyết định bổ nhiệm ngạch.", count));

                }
                catch (Exception ex)
                {
                    uow.RollbackTransaction();
                    //
                    DialogUtil.ShowError("Đã xảy ra lỗi trong quá trình tạo quyết định." + ex.Message);
                }
            }
        }
        private void CreateQuyetDinhNhieuNguoi(DeNghiBoNhiemNgach deNghi, IObjectSpace obs)
        {
            QuyetDinhBoNhiemNgach quyetDinh = obs.FindObject<QuyetDinhBoNhiemNgach>(CriteriaOperator.Parse("DeNghiBoNhiemNgach=?", deNghi.Oid));
            if (quyetDinh == null)
            {
                quyetDinh = obs.CreateObject<QuyetDinhBoNhiemNgach>();
                quyetDinh.SoQuyetDinh = "   /QĐBNN  ";
                quyetDinh.QuyetDinhMoi = true;
                quyetDinh.DeNghiBoNhiemNgach = obs.GetObjectByKey<DeNghiBoNhiemNgach>(deNghi.Oid);
                //
                ChiTietQuyetDinhBoNhiemNgach chiTiet;
                foreach (ChiTietDeNghiBoNhiemNgach item in deNghi.ListChiTietDeNghiBoNhiemNgach)
                {
                    chiTiet = obs.FindObject<ChiTietQuyetDinhBoNhiemNgach>(CriteriaOperator.Parse("QuyetDinhBoNhiemNgach=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                    if (chiTiet == null)
                    {
                        chiTiet = obs.CreateObject<ChiTietQuyetDinhBoNhiemNgach>();
                        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        chiTiet.NgachLuong = obs.GetObjectByKey<NgachLuong>(item.NgachLuong.Oid);
                        chiTiet.BacLuong = obs.GetObjectByKey<BacLuong>(item.BacLuong.Oid);
                        chiTiet.HeSoLuong = item.HeSoLuong;
                        chiTiet.NgayBoNhiemNgach = item.NgayBoNhiemNgach;
                        chiTiet.NgayHuongLuong = item.NgayHuongLuong;
                        chiTiet.MocNangLuong = item.MocNangLuong;

                        quyetDinh.ListChiTietQuyetDinhBoNhiemNgach.Add(chiTiet);
                    }
                }
            }
           
            Application.ShowView<QuyetDinhBoNhiemNgach>(obs, quyetDinh);
        }
    }
}
