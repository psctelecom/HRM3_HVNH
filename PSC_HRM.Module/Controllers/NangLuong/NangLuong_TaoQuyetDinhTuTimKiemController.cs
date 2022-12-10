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
using PSC_HRM.Module.NangLuong;
using System.Windows.Forms;

namespace PSC_HRM.Module.Controllers
{
    public partial class NangLuong_TaoQuyetDinhTuTimKiemController : ViewController
    {
        private DanhSachDenHanNangLuong dsDenHanNangLuong;
        //
        public NangLuong_TaoQuyetDinhTuTimKiemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NangLuong_TaoQuyetDinhTuTimKiemController");
        }

        private void NangLuong_TaoQuyetDinhTuTimKiemController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<DanhSachDenHanNangLuong>()
                && HamDungChung.IsWriteGranted<QuyetDinh.QuyetDinhNangLuong>()
                && HamDungChung.IsWriteGranted<QuyetDinh.ChiTietQuyetDinhNangLuong>()
                && HamDungChung.IsWriteGranted<NhanVienThongTinLuong>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            dsDenHanNangLuong = View.CurrentObject as DanhSachDenHanNangLuong;
            //
            if (dsDenHanNangLuong != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();

                if (dsDenHanNangLuong != null)
                {
                    if (DialogUtil.ShowYesNo("Bạn có muốn lập mỗi quyết định cho 1 người không?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Tạo quyết định một người
                        CreateQuyetDinhMotNguoi(dsDenHanNangLuong, obs);
                    }
                    else
                    {
                        //Tạo quyết định nhiều người
                        CreateQuyetDinhNhieuNguoi(dsDenHanNangLuong, obs);
                    }
                }
                else
                {
                    //Tạo quyết định nhiều người
                    CreateQuyetDinhNhieuNguoi(dsDenHanNangLuong, obs);
                }
            }
        }

        private static void CreateQuyetDinhMotNguoi(DanhSachDenHanNangLuong dsDenHanNangLuong, IObjectSpace obs)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                uow.BeginTransaction();
                //
                try
                {
                    int count = 0;
                    foreach (DenHanNangLuong item in dsDenHanNangLuong.DanhSachNhanVien)
                    {
                        if (item.Chon)
                        {
                            QuyetDinhNangLuong quyetDinh = uow.FindObject<QuyetDinhNangLuong>
                                (CriteriaOperator.Parse("ListChiTietQuyetDinhNangLuong[ThongTinNhanVien=?] and ListChiTietQuyetDinhNangLuong[VuotKhungMoi=?] and ListChiTietQuyetDinhNangLuong[HeSoLuongMoi=?]"
                                , item.ThongTinNhanVien.Oid, item.PhanTramVuotKhungMoi, item.HeSoLuongMoi));
                            if (quyetDinh == null)
                            {
                                quyetDinh = new QuyetDinhNangLuong(uow);
                                quyetDinh.SoQuyetDinh = "";
                                if (TruongConfig.MaTruong == "BUH")
                                    quyetDinh.SoQuyetDinh = "/QĐ-ĐHNH";
                                quyetDinh.Imporrt = true;
                                quyetDinh.QuyetDinhMoi = true;
                                quyetDinh.GhiChu = String.Format("{0} cho cán bộ {1}", HamDungChung.NangLuong(item.PhanLoai, item.PhanTramVuotKhungMoi), item.ThongTinNhanVien.HoTen);
                                //
                                ChiTietQuyetDinhNangLuong chiTiet = uow.FindObject<ChiTietQuyetDinhNangLuong>(CriteriaOperator.Parse("QuyetDinhNangLuong=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                                if (chiTiet == null)
                                {
                                    chiTiet = new ChiTietQuyetDinhNangLuong(uow);
                                    chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                    chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                    chiTiet.NgachLuong = uow.GetObjectByKey<NgachLuong>(item.NgachLuong.Oid);
                                    chiTiet.BacLuongCu = uow.GetObjectByKey<BacLuong>(item.BacLuongCu.Oid);
                                    chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                                    chiTiet.VuotKhungCu = item.PhanTramVuotKhungCu;
                                    chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                                    chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                                    chiTiet.BacLuongMoi = uow.GetObjectByKey<BacLuong>(item.BacLuongMoi.Oid);
                                    chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                                    chiTiet.VuotKhungMoi = item.PhanTramVuotKhungMoi;
                                    chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                                    chiTiet.NangLuongTruocHan = item.PhanLoai != NangLuongEnum.ThuongXuyen;
                                    quyetDinh.NgayPhatSinhBienDong = item.NgayHuongLuongMoi;
                                    //
                                    quyetDinh.ListChiTietQuyetDinhNangLuong.Add(chiTiet);
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
                    }

                    //Tiến hành lưu dữ liệu
                    uow.CommitChanges();

                    DialogUtil.ShowInfo(String.Format("Thành công! Đã tạo được {0} quyết định.", count));
                    
                }
                catch (Exception ex)
                {
                    uow.RollbackTransaction();
                    //
                    DialogUtil.ShowError("Đã xảy ra lỗi trong quá trình tạo quyết định nâng lương." + ex.Message);
                }
            }
        }

        private void CreateQuyetDinhNhieuNguoi(DanhSachDenHanNangLuong dsDenHanNangLuong, IObjectSpace obs)
        {
            QuyetDinhNangLuong quyetDinh = obs.CreateObject<QuyetDinhNangLuong>();
            quyetDinh.SoQuyetDinh = "      /QĐ-";
            quyetDinh.QuyetDinhMoi = false;
            quyetDinh.Imporrt = true;
            quyetDinh.GhiChu = String.Format("Nâng lương cho {0} cán bộ", dsDenHanNangLuong.DanhSachNhanVien.Count);
            //
            foreach (DenHanNangLuong item in dsDenHanNangLuong.DanhSachNhanVien)
            {
                if (item.Chon)
                {
                    ChiTietQuyetDinhNangLuong chiTiet = obs.FindObject<ChiTietQuyetDinhNangLuong>(CriteriaOperator.Parse("QuyetDinhNangLuong=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                    if (chiTiet == null)
                    {
                        chiTiet = obs.CreateObject<ChiTietQuyetDinhNangLuong>();
                        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        chiTiet.NgachLuong = obs.GetObjectByKey<NgachLuong>(item.NgachLuong.Oid);
                        chiTiet.BacLuongCu = obs.GetObjectByKey<BacLuong>(item.BacLuongCu.Oid);
                        chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                        chiTiet.VuotKhungCu = item.PhanTramVuotKhungCu;
                        chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                        chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                        chiTiet.BacLuongMoi = obs.GetObjectByKey<BacLuong>(item.BacLuongMoi.Oid);
                        chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                        chiTiet.VuotKhungMoi = item.PhanTramVuotKhungMoi;
                        chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                        chiTiet.NangLuongTruocHan = item.PhanLoai != NangLuongEnum.ThuongXuyen;
                        quyetDinh.ListChiTietQuyetDinhNangLuong.Add(chiTiet);
                    }
                }
            }
            //
            Application.ShowView<QuyetDinhNangLuong>(obs, quyetDinh);
        }
    }
}
