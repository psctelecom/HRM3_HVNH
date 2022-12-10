using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.ChuyenNgach;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.NonPersistentObjects;

namespace PSC_HRM.Module.Controllers
{
    public partial class ChuyenNgach_TaoQuyetDinhChuyenNgachController : ViewController
    {
        public ChuyenNgach_TaoQuyetDinhChuyenNgachController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ChuyenNgach_TaoQuyetDinhChuyenNgachController");
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhChuyenNgach>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            {//Lưu dữ liệu hiện tại
                //
                XafApplicationHelper.CascadeCommit(View.ObjectSpace, false);
                //
                View.ObjectSpace.Refresh();
            }
            //
            DeNghiChuyenNgach deNghi = View.CurrentObject as DeNghiChuyenNgach;
            if (deNghi != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();

                if (deNghi.ListChiTietDeNghiChuyenNgach.Count > 1)
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

        private static void CreateQuyetDinhMotNguoi(DeNghiChuyenNgach deNghi, IObjectSpace obs)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                uow.BeginTransaction();

                try
                {
                    int count = 0;
                    foreach (ChiTietDeNghiChuyenNgach item in deNghi.ListChiTietDeNghiChuyenNgach)
                    {
                        QuyetDinhChuyenNgach quyetDinh = obs.FindObject<QuyetDinhChuyenNgach>(CriteriaOperator.Parse("DeNghiChuyenNgach=?", deNghi.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = new QuyetDinhChuyenNgach(uow);
                            quyetDinh.DeNghiChuyenNgach = uow.GetObjectByKey<DeNghiChuyenNgach>(deNghi.Oid);
                            quyetDinh.QuyetDinhMoi = true;
                            quyetDinh.SoQuyetDinh = item.SoQuyetDinh;

                            ChiTietQuyetDinhChuyenNgach chiTiet = uow.FindObject<ChiTietQuyetDinhChuyenNgach>(CriteriaOperator.Parse("QuyetDinhChuyenNgach=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                            if (chiTiet == null)
                            {
                                chiTiet = new ChiTietQuyetDinhChuyenNgach(uow);
                                chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                chiTiet.NgachLuongCu = uow.GetObjectByKey<NgachLuong>(item.NgachLuongCu.Oid);
                                chiTiet.BacLuongCu = uow.GetObjectByKey<BacLuong>(item.BacLuongCu.Oid);
                                chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                                chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                                chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                                chiTiet.NgachLuongMoi = uow.GetObjectByKey<NgachLuong>(item.NgachLuongMoi.Oid);
                                chiTiet.BacLuongMoi = uow.GetObjectByKey<BacLuong>(item.BacLuongMoi.Oid);
                                chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                                chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                                chiTiet.NgayHuongLuongMoi = item.NgayHuongLuongMoi;
                                //
                                quyetDinh.ListChiTietQuyetDinhChuyenNgach.Add(chiTiet);
                            }

                            {//Trường hợp nếu trường không có hiệu trưởng như đại học công nghiệp thì cấu hình người ký tên của quyết định trong bảng người sử dụng

                                if (quyetDinh.NguoiKy == null && HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen!=null )
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

                    DialogUtil.ShowInfo(String.Format("Thành công! Đã tạo được {0} quyết định.", count));
                }
                catch (Exception ex)
                {
                    uow.RollbackTransaction();
                    //
                    DialogUtil.ShowError("Đã xảy ra lỗi trong quá trình tạo quyết định chuyển ngạch." + ex.Message);
                }
            }
        }

        private void CreateQuyetDinhNhieuNguoi(DeNghiChuyenNgach deNghi, IObjectSpace obs)
        {
            QuyetDinhChuyenNgach quyetDinh = obs.FindObject<QuyetDinhChuyenNgach>(CriteriaOperator.Parse("DeNghiChuyenNgach=?", deNghi.Oid));
            if (quyetDinh == null)
            {
                quyetDinh = obs.CreateObject<QuyetDinhChuyenNgach>();
                quyetDinh.DeNghiChuyenNgach = obs.GetObjectByKey<DeNghiChuyenNgach>(deNghi.Oid);
                quyetDinh.QuyetDinhMoi = true;
                quyetDinh.SoQuyetDinh = "   /QĐCN  ";

                ChiTietQuyetDinhChuyenNgach chiTiet;
                foreach (ChiTietDeNghiChuyenNgach item in deNghi.ListChiTietDeNghiChuyenNgach)
                {
                    chiTiet = obs.FindObject<ChiTietQuyetDinhChuyenNgach>(CriteriaOperator.Parse("QuyetDinhChuyenNgach=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                    if (chiTiet == null)
                    {
                        chiTiet = obs.CreateObject<ChiTietQuyetDinhChuyenNgach>();
                        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        chiTiet.NgachLuongCu = obs.GetObjectByKey<NgachLuong>(item.NgachLuongCu.Oid);
                        chiTiet.BacLuongCu = obs.GetObjectByKey<BacLuong>(item.BacLuongCu.Oid);
                        chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                        chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                        chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                        chiTiet.NgachLuongMoi = obs.GetObjectByKey<NgachLuong>(item.NgachLuongMoi.Oid);
                        chiTiet.BacLuongMoi = obs.GetObjectByKey<BacLuong>(item.BacLuongMoi.Oid);
                        chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                        chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                        chiTiet.NgayHuongLuongMoi = item.NgayHuongLuongMoi;
                        //
                        quyetDinh.ListChiTietQuyetDinhChuyenNgach.Add(chiTiet);
                    }
                }
            }
           //
            Application.ShowView<QuyetDinhChuyenNgach>(obs, quyetDinh);
        }
    }
}
