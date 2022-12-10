using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.NangLuong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Controllers
{
    public partial class NangLuong_TaoQuyetDinhNangLuongController : ViewController
    {
        public NangLuong_TaoQuyetDinhNangLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NangLuong_TaoQuyetDinhNangLuongController");
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNangLuong>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            {//Tiến hành lưu dữ liệu hiện tại
                XafApplicationHelper.CascadeCommit(View.ObjectSpace, false);
                View.ObjectSpace.Refresh();
            }
            //
            DeNghiNangLuong deNghi = View.CurrentObject as DeNghiNangLuong;
            //
            if (deNghi != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                if (deNghi.ListChiTietDeNghiNangLuong.Count > 1)
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

        private void CreateQuyetDinhNhieuNguoi(DeNghiNangLuong deNghi, IObjectSpace obs)
        {
            QuyetDinhNangLuong quyetDinh = obs.FindObject<QuyetDinhNangLuong>(CriteriaOperator.Parse("DeNghiNangLuong=?", deNghi.Oid));
            if (quyetDinh == null)
            {
                quyetDinh = obs.CreateObject<QuyetDinhNangLuong>();
                quyetDinh.QuyetDinhMoi = true;
                quyetDinh.DeNghiNangLuong = obs.GetObjectByKey<DeNghiNangLuong>(deNghi.Oid);
                quyetDinh.GhiChu = String.Format("Nâng lương cho {0} cán bộ", deNghi.ListChiTietDeNghiNangLuong.Count);
                //quyetDinh.SoQuyetDinh = "  /QĐNL";

                //
                ChiTietQuyetDinhNangLuong chiTiet;
                foreach (ChiTietDeNghiNangLuong item in deNghi.ListChiTietDeNghiNangLuong)
                {
                    chiTiet = obs.FindObject<ChiTietQuyetDinhNangLuong>(CriteriaOperator.Parse("QuyetDinhNangLuong=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                    if (chiTiet == null)
                    {
                        chiTiet = obs.CreateObject<ChiTietQuyetDinhNangLuong>();
                        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        if (TruongConfig.MaTruong != "HBU")
                        {
                            chiTiet.NgachLuong = obs.GetObjectByKey<NgachLuong>(item.NgachLuong.Oid);
                            chiTiet.BacLuongCu = obs.GetObjectByKey<BacLuong>(item.BacLuongCu.Oid);
                            chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                            chiTiet.VuotKhungCu = item.VuotKhungCu;
                            chiTiet.MocNangLuongCu = item.MocNangLuongDieuChinh == DateTime.MinValue ? item.MocNangLuongCu : item.MocNangLuongDieuChinh;
                            chiTiet.BacLuongMoi = obs.GetObjectByKey<BacLuong>(item.BacLuongMoi.Oid);
                            chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                            chiTiet.VuotKhungMoi = item.VuotKhungMoi;
                            chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                        }
                        chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                        chiTiet.NgayHuongLuongMoi = item.NgayHuongLuongMoi;
                        if (item.PhanLoai == NangLuongEnum.CoThanhTichXuatSac)
                        { chiTiet.NangLuongTruocHan = true; }
                        else if (item.PhanLoai == NangLuongEnum.TruocKhiNghiHuu)
                        { chiTiet.NangLuongTruocKhiNghiHuu = true; }
                        //
                        quyetDinh.NgayPhatSinhBienDong = item.NgayHuongLuongMoi;
                        quyetDinh.ListChiTietQuyetDinhNangLuong.Add(chiTiet);
                    }
                }
            }
            
            Application.ShowView<QuyetDinhNangLuong>(obs, quyetDinh);
        }

        private static void CreateQuyetDinhMotNguoi(DeNghiNangLuong deNghi, IObjectSpace obs)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                uow.BeginTransaction();

                try
                {
                    int count = 0;
                    foreach (ChiTietDeNghiNangLuong item in deNghi.ListChiTietDeNghiNangLuong)
                    {
                        QuyetDinhNangLuong quyetDinh = uow.FindObject<QuyetDinhNangLuong>(CriteriaOperator.Parse("DeNghiNangLuong=? and ListChiTietQuyetDinhNangLuong[ThongTinNhanVien=?]", deNghi.Oid, item.ThongTinNhanVien.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = new QuyetDinhNangLuong(uow);
                            quyetDinh.SoQuyetDinh = item.SoQuyetDinh;
                            if (item.NgayQuyetDinh != DateTime.MinValue)
                            {
                                quyetDinh.NgayQuyetDinh = item.NgayQuyetDinh;
                            }
                            
                            quyetDinh.QuyetDinhMoi = true;
                            quyetDinh.DeNghiNangLuong = uow.GetObjectByKey<DeNghiNangLuong>(deNghi.Oid);

                            if (TruongConfig.MaTruong.Equals("BUH") || TruongConfig.MaTruong.Equals("DLU")
                                || TruongConfig.MaTruong.Equals("GTVT") || TruongConfig.MaTruong.Equals("LUH"))
                            {
                                quyetDinh.GhiChu = String.Format("{0}-{1}",
                                    HamDungChung.NangLuong_GhiChu(item.PhanLoai, item.VuotKhungMoi), item.NgachLuong.MaQuanLy);
                            }
                            else
                            {
                                quyetDinh.GhiChu = String.Format("{0} cho cán bộ {1}", HamDungChung.NangLuong(item.PhanLoai, item.VuotKhungMoi), item.ThongTinNhanVien.HoTen);
                            }
                            
                            ChiTietQuyetDinhNangLuong chiTiet = uow.FindObject<ChiTietQuyetDinhNangLuong>(CriteriaOperator.Parse("QuyetDinhNangLuong=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                            if (chiTiet == null)
                            {
                                chiTiet = new ChiTietQuyetDinhNangLuong(uow);
                                chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                if (!TruongConfig.MaTruong.Equals("HBU"))
                                {
                                    chiTiet.NgachLuong = uow.GetObjectByKey<NgachLuong>(item.NgachLuong.Oid);
                                    chiTiet.BacLuongCu = uow.GetObjectByKey<BacLuong>(item.BacLuongCu.Oid);
                                    chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                                    chiTiet.VuotKhungCu = item.VuotKhungCu;
                                    chiTiet.MocNangLuongCu = item.MocNangLuongDieuChinh == DateTime.MinValue ? item.MocNangLuongCu : item.MocNangLuongDieuChinh;
                                    chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                                    chiTiet.BacLuongMoi = uow.GetObjectByKey<BacLuong>(item.BacLuongMoi.Oid);
                                    chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                                    chiTiet.VuotKhungMoi = item.VuotKhungMoi;
                                    chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                                }
                                chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                                chiTiet.NgayHuongLuongMoi = item.NgayHuongLuongMoi;
                                if (item.PhanLoai == NangLuongEnum.CoThanhTichXuatSac)
                                { 
                                    chiTiet.NangLuongTruocHan = true;
                                    chiTiet.LyDo = item.LyDo;
                                    chiTiet.SoThang = item.SoThang;
                                }
                                else if (item.PhanLoai == NangLuongEnum.TruocKhiNghiHuu)
                                { 
                                    chiTiet.NangLuongTruocKhiNghiHuu = true;
                                    chiTiet.NgayNghiHuu = item.NgayNghiHuu;
                                }

                                //
                                quyetDinh.NgayPhatSinhBienDong = item.NgayHuongLuongMoi;
                                quyetDinh.NgayHieuLuc = item.NgayHuongLuongMoi;
                                if (item.PhanLoai == NangLuongEnum.CoThanhTichXuatSac)
                                { quyetDinh.NoiDung = "nâng lương trước hạn"; }
                                else if (item.PhanLoai == NangLuongEnum.TruocKhiNghiHuu)
                                { quyetDinh.NoiDung = "nâng lương trước nghỉ hưu"; }
                                else
                                { quyetDinh.NoiDung = "nâng lương thường xuyên"; }
                                //
                                quyetDinh.ListChiTietQuyetDinhNangLuong.Add(chiTiet); 
                            }
                            {//Trường hợp nếu trường không có hiệu trưởng như đại học công nghiệp thì cấu hình người ký của quyết định trong cấu hình chung

                                if (quyetDinh.NguoiKy == null && HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen != null)
                                {
                                    quyetDinh.ChucVuNguoiKy = uow.GetObjectByKey<ChucVu>(HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen.ChucVu.Oid);
                                    quyetDinh.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen.Oid);

                                }
                            }
                            count += 1;
                        }  
                     }

                    uow.CommitChanges();
                    //
                    DialogUtil.ShowInfo(string.Format("Thành công! Đã tạo {0} quyết định nâng lương.",count));

                }
                catch (Exception ex)
                {
                    uow.RollbackTransaction();
                    //
                    DialogUtil.ShowError("Đã xảy ra lỗi trong quá trình tạo quyết định." + ex.Message);
                }
            }
        }
    }
}
