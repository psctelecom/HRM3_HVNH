using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.Xpo;
using PSC_HRM.Module.QuaTrinh;
using System.Windows.Forms;

namespace PSC_HRM.Module.Controllers
{
    public partial class HopDong_TaoHopDongTuTimKiemController : ViewController
    {
        private IObjectSpace obs;
        private HopDong_TaoHopDong chonHopDong;
        private DanhSachHetHanHopDong dsHetHanHopDong;
        private NhanVien nhanVien;
        private HopDong_NhanVien hopDong_NhanVien;
        private HopDong_LamViec hopDongLamViec = null;
        private HopDong_LaoDong hopDongLaoDong = null;
        private HopDong_Khoan hopDongKhoan = null;

        public HopDong_TaoHopDongTuTimKiemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_TaoHopDongTuTimKiemController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();
            //
            dsHetHanHopDong = View.CurrentObject as DanhSachHetHanHopDong;
            if (dsHetHanHopDong != null)
            {
                if (DialogUtil.ShowYesNo("Bạn thật sự muốn tạo hợp đồng mới cho cán bộ đã chọn?") == DialogResult.Yes)
                {
                    foreach (ChiTietHetHanHopDong item in dsHetHanHopDong.ChiTietHetHanHopDongList)
                    {
                        if (item.Chon)
                        {
                            nhanVien = obs.GetObjectByKey<NhanVien>(item.HopDongLaoDong.NhanVien.Oid);
                            hopDong_NhanVien = item.HopDongLaoDong as HopDong_NhanVien;
                            chonHopDong = obs.CreateObject<HopDong_TaoHopDong>();
                            if (item.HopDongLaoDong is HopDong_LamViec)
                            {
                                hopDongLamViec = obs.GetObjectByKey<HopDong_LamViec>(item.HopDongLaoDong.Oid);
                                chonHopDong.LoaiHopDong = TaoHopDongEnum.HopDongLamViec;
                            }
                            else if (item.HopDongLaoDong is HopDong_LaoDong)
                            {
                                hopDongLaoDong = obs.GetObjectByKey<HopDong_LaoDong>(item.HopDongLaoDong.Oid);
                                chonHopDong.LoaiHopDong = TaoHopDongEnum.HopDongHeSo;
                            }
                            else if (item.HopDongLaoDong is HopDong_Khoan)
                            {
                                hopDongKhoan = item.HopDongLaoDong as HopDong_Khoan;
                                chonHopDong.LoaiHopDong = TaoHopDongEnum.HopDongKhoan;
                            }
                            e.View = Application.CreateDetailView(obs, chonHopDong);
                            break;
                        }
                    }
                }
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            e.PopupWindow.View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            QuanLyHopDong qlHopDong = obs.FindObject<QuanLyHopDong>(CriteriaOperator.Parse("NamHoc=?",
                HamDungChung.GetCurrentNamHoc(((XPObjectSpace)obs).Session)));
            switch (chonHopDong.LoaiHopDong)
            {
                case TaoHopDongEnum.HopDongHeSo:
                    HopDong_LaoDong hdLaoDong = obs.CreateObject<HopDong_LaoDong>();
                    hdLaoDong.QuanLyHopDong = qlHopDong;

                    //kiểm tra hình thức hợp đồng
                    //nếu là hđ lần đầu thì 2 hợp đồng tiếp theo là 1 năm
                    //nếu là hđ 1 năm ký lần thứ 2 thì hợp đồng tiếp theo là không thời hạn
                    if (hopDongLaoDong != null && hopDongLaoDong.HinhThucHopDong != null)
                    {
                        if (hopDongLaoDong.PhanLoai == HopDongLaoDongEnum.TapSuThuViec)
                        {
                            hdLaoDong.PhanLoai = HopDongLaoDongEnum.CoThoiHan;
                            hdLaoDong.HinhThucHopDong = obs.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("TenHinhThucHopDong like ?", "%1 năm%"));
                        }
                        else
                        {
                            object obj = ((XPObjectSpace)obs).Session.Evaluate<HopDong_LamViec>(CriteriaOperator.Parse("COUNT()"),
                                CriteriaOperator.Parse("NhanVien=? and PhanLoai=1",
                                    hopDongLaoDong.NhanVien.Oid));
                            if (obj != null)
                            {
                                if ((int)obj == 1)
                                {
                                    hdLaoDong.HinhThucHopDong = obs.GetObjectByKey<HinhThucHopDong>(hopDongLaoDong.HinhThucHopDong.Oid);
                                    hdLaoDong.PhanLoai = HopDongLaoDongEnum.CoThoiHan;
                                }
                                else
                                    hdLaoDong.PhanLoai = HopDongLaoDongEnum.KhongThoiHan;
                            }
                        }
                    }
                    hdLaoDong.TuNgay = hopDong_NhanVien.DenNgay.AddDays(1);
                    hdLaoDong.NhanVien = obs.GetObjectByKey<NhanVien>(nhanVien.Oid);
                    hdLaoDong.ChucDanhChuyenMon = hdLaoDong.NhanVien.NhanVienThongTinLuong.NgachLuong != null ? hdLaoDong.NhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                    hdLaoDong.DieuKhoanHopDong.NgachLuong = hdLaoDong.NhanVien.NhanVienThongTinLuong.NgachLuong;
                    hdLaoDong.DieuKhoanHopDong.BacLuong = hdLaoDong.NhanVien.NhanVienThongTinLuong.BacLuong;
                    hdLaoDong.DieuKhoanHopDong.HeSoLuong = hdLaoDong.NhanVien.NhanVienThongTinLuong.HeSoLuong;

                    e.ShowViewParameters.Context = TemplateContext.View;
                    e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                    e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, hdLaoDong);
                    e.ShowViewParameters.CreatedView.ObjectSpace.Committed += obs_Committed;

                    break;
                case TaoHopDongEnum.HopDongKhoan:
                    HopDong_Khoan hdKhoan = obs.CreateObject<HopDong_Khoan>();
                    hdKhoan.QuanLyHopDong = qlHopDong;
                    hdKhoan.TuNgay = hopDong_NhanVien.DenNgay.AddDays(1);
                    hdKhoan.NhanVien = obs.GetObjectByKey<NhanVien>(nhanVien.Oid); ;
                    
                    if (hopDongKhoan != null)
                    {
                        if (hopDongKhoan.HinhThucHopDong != null)
                        { hdKhoan.HinhThucHopDong = obs.GetObjectByKey<HinhThucHopDong>(hopDongKhoan.HinhThucHopDong.Oid); }
                        hdKhoan.ChucDanhChuyenMon = hopDongKhoan.ChucDanhChuyenMon;
                        hdKhoan.HinhThucThanhToan = hopDongKhoan.HinhThucThanhToan;
                        hdKhoan.TienLuong = hopDongKhoan.TienLuong;
                    }

                    e.ShowViewParameters.Context = TemplateContext.View;
                    e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                    e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, hdKhoan);
                    e.ShowViewParameters.CreatedView.ObjectSpace.Committed += obs_Committed;

                    break;
                default://TaoHopDongEnum.HopDongLamViec:
                    HopDong_LamViec hdLamViec = obs.CreateObject<HopDong_LamViec>();
                    hdLamViec.QuanLyHopDong = qlHopDong;

                    if (hopDongLamViec != null && hopDongLamViec.HinhThucHopDong != null)
                    {
                        if (hopDongLamViec.PhanLoai == HopDongLamViecEnum.HopDongLanDau)
                        {
                            hdLamViec.PhanLoai = HopDongLamViecEnum.CoThoiHan;
                            hdLamViec.HinhThucHopDong = obs.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("TenHinhThucHopDong like ?", "%1 năm%"));
                        }
                        else
                        {
                            object obj = ((XPObjectSpace)obs).Session.Evaluate<HopDong_LamViec>(CriteriaOperator.Parse("COUNT()"),
                                CriteriaOperator.Parse("NhanVien=? and PhanLoai=1",
                                    hopDongLamViec.NhanVien.Oid));
                            if (obj != null)
                            {
                                if ((int)obj == 1)
                                {
                                    hdLamViec.HinhThucHopDong = obs.GetObjectByKey<HinhThucHopDong>(hopDongLamViec.HinhThucHopDong.Oid);
                                    hdLamViec.PhanLoai = HopDongLamViecEnum.CoThoiHan;
                                }
                                else
                                    hdLamViec.PhanLoai = HopDongLamViecEnum.KhongThoiHan;
                            }
                        }
                    }

                    hdLamViec.NhanVien = obs.GetObjectByKey<NhanVien>(nhanVien.Oid); ;
                    hdLamViec.TuNgay = hopDong_NhanVien.DenNgay.AddDays(1);
                    hdLamViec.ChucDanhChuyenMon = hdLamViec.NhanVien.NhanVienThongTinLuong.NgachLuong != null ? hdLamViec.NhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                    hdLamViec.DieuKhoanHopDong.NgachLuong = hdLamViec.NhanVien.NhanVienThongTinLuong.NgachLuong;
                    hdLamViec.DieuKhoanHopDong.BacLuong = hdLamViec.NhanVien.NhanVienThongTinLuong.BacLuong;
                    hdLamViec.DieuKhoanHopDong.HeSoLuong = hdLamViec.NhanVien.NhanVienThongTinLuong.HeSoLuong;
                    hdLamViec.DieuKhoanHopDong.VuotKhung = hdLamViec.NhanVien.NhanVienThongTinLuong.VuotKhung;

                    e.ShowViewParameters.Context = TemplateContext.View;
                    e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                    e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, hdLamViec);
                    e.ShowViewParameters.CreatedView.ObjectSpace.Committed += obs_Committed;
                    break;
            }
        }

        //reload listview after save object in detailvew
        void obs_Committed(object sender, EventArgs e)
        {
            //obs.Refresh();
            ////
            //if (dsHetHanHopDong != null)
            //    dsHetHanHopDong.LoadData();
        }

        private void HopDong_TaoHopDongTuTimKiemController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active.Clear();
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<DanhSachHetHanHopDong>()
                && HamDungChung.IsWriteGranted<QuanLyHopDong>()
                && HamDungChung.IsWriteGranted<HopDong.HopDong>()
                && HamDungChung.IsWriteGranted<HopDong_NhanVien>()
                && HamDungChung.IsWriteGranted<HopDong_LamViec>()
                && HamDungChung.IsWriteGranted<HopDong_Khoan>()
                && HamDungChung.IsWriteGranted<HopDong_LaoDong>();
        }
    }
}
