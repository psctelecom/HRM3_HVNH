using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;
using DevExpress.ExpressApp;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.XuLyQuyTrinh.NghiHuu;
using PSC_HRM.Module.BanLamViec;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.Win.Forms;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.NghiHuu
{
    public partial class TheoDoiNghiHuuController : TheoDoiBaseController
    {
        public TheoDoiNghiHuuController()
        {
            InitializeComponent();
        }

        public TheoDoiNghiHuuController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
        }

        private void TheoDoiNghiHuuController_Load(object sender, EventArgs e)
        {
            gridViewDanhSachNghiHuu.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewDanhSachNghiHuu.ReadOnlyGridView();
            gridViewDanhSachNghiHuu.ShowField(
                new string[] { "BoPhan.TenBoPhan", "ThongTinNhanVien.HoTen", "Ngay" },
                new string[] { "Đơn vị", "Cán bộ", "Ngày nghỉ hưu" },
                new int[] { 150, 150, 80, 80 });

            deDenNgay.DateTime = HamDungChung.GetServerTime();
            GetDuLieu();
        }

        private void btnInThongBaoNghiHuu_Click(object sender, EventArgs e)
        {
            ThongTinNghiHuu obj = GetThongTinNghiHuu();
            if (obj != null)
            {
                var list = new List<ThongTinNghiHuu>() { obj };
                MailMerge_ThongBaoNghiHuu mailMerge = new MailMerge_ThongBaoNghiHuu();
                mailMerge.Merge(obs, list);
            }
        }

        private void btnQDNghiHuu_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<BienDong_GiamLaoDong>())
            {
                ThongTinNghiHuu obj = GetThongTinNghiHuu();
                if (obj != null)
                {
                    IObjectSpace obs = app.CreateObjectSpace();
                    QuyetDinhNghiHuu quyetDinh = obs.CreateObject<QuyetDinhNghiHuu>();
                    quyetDinh.BoPhan = obs.GetObjectByKey<BaoMat.BoPhan>(obj.BoPhan.Oid);
                    quyetDinh.ThongTinNhanVien = obs.GetObjectByKey<HoSo.ThongTinNhanVien>(obj.ThongTinNhanVien.Oid);
                    quyetDinh.NghiViecTuNgay = obj.Ngay;

                    if (obj.ThongTinNhanVien.NoiOHienNay != null)
                        quyetDinh.NoiCuTruSauKhiThoiViec = HamDungChung.Copy<DiaChi>(((XPObjectSpace)obs).Session, obj.ThongTinNhanVien.NoiOHienNay);
                    app.ShowView<QuyetDinhNghiHuu>(obs, quyetDinh);
                }
            }
            else
                HamDungChung.ShowWarningMessage("Bạn không được cấp quyền");
        }

        private void btnQDKeoDaiThoiGianCongTac_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<BienDong_GiamLaoDong>())
            {
                ThongTinNghiHuu obj = GetThongTinNghiHuu();
                if (obj != null)
                {
                    IObjectSpace obs = app.CreateObjectSpace();
                    QuyetDinhKeoDaiThoiGianCongTac quyetDinh = obs.CreateObject<QuyetDinhKeoDaiThoiGianCongTac>();
                    quyetDinh.BoPhan = obs.GetObjectByKey<BaoMat.BoPhan>(obj.BoPhan.Oid);
                    quyetDinh.ThongTinNhanVien = obs.GetObjectByKey<HoSo.ThongTinNhanVien>(obj.ThongTinNhanVien.Oid);
                    quyetDinh.TuNgay = obj.Ngay;
                    quyetDinh.DenNgay = obj.Ngay.AddYears(1);
                    app.ShowView<QuyetDinhKeoDaiThoiGianCongTac>(obs, quyetDinh);
                }
            }
            else
                HamDungChung.ShowWarningMessage("Bạn không được cấp quyền");
        }

        private ThongTinNghiHuu GetThongTinNghiHuu()
        {
            return gridViewDanhSachNghiHuu.GetFocusedRow() as ThongTinNghiHuu;
        }

        private void miGuiHoSoLenCoQuanBaoHiem_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<BienDong_GiamLaoDong>())
            {
                frmPopUp<ChonDuLieuController<QuanLyBienDong>> popup = new frmPopUp<ChonDuLieuController<QuanLyBienDong>>(app, obs, new ChonDuLieuController<QuanLyBienDong>(app, obs, "Quản lý biến động", CriteriaOperator.Parse(""), new string[] { "Caption" }, new string[] { "Quản lý biến động" }, new int[] { 150 }), "Chọn quản lý biến động", true);
                if (popup.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    ThongTinNghiHuu nhanVien = GetThongTinNghiHuu();
                    QuanLyBienDong quanLy = popup.CurrentControl.GetData();
                    if (nhanVien != null && quanLy != null)
                    {
                        obs = app.CreateObjectSpace();
                        BienDong_GiamLaoDong bienDong = obs.CreateObject<BienDong_GiamLaoDong>();
                        bienDong.QuanLyBienDong = obs.GetObjectByKey<QuanLyBienDong>(quanLy.Oid);
                        bienDong.BoPhan = obs.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                        bienDong.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(nhanVien.ThongTinNhanVien.Oid);
                        bienDong.LyDo = LyDoNghiEnum.ThoiViec;
                        if (nhanVien.ThongTinNhanVien.NgayNghiHuu != DateTime.MinValue)
                            bienDong.TuNgay = nhanVien.ThongTinNhanVien.NgayNghiHuu;
                        else
                        {
                            TuoiNghiHuu tuoiNghiHuu = obs.FindObject<TuoiNghiHuu>(CriteriaOperator.Parse("GioiTinh=?", (byte)nhanVien.GioiTinh));
                            if (tuoiNghiHuu != null)
                            {
                                bienDong.TuNgay = nhanVien.NgaySinh.AddYears(tuoiNghiHuu.Tuoi);
                            }
                        }

                        app.ShowView<BienDong_GiamLaoDong>(obs, bienDong);
                    }
                }
            }
            else
                HamDungChung.ShowWarningMessage("Bạn không được cấp quyền");
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            GetDuLieu();
        }

        protected override void GetDuLieu()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DenNgay", deDenNgay.DateTime.SetTime(SetTimeEnum.EndMonth).AddMonths(1));
            param[1] = new SqlParameter("@ThongTinTruong", HamDungChung.ThongTinTruong(((XPObjectSpace)obs).Session).Oid);
            gridDanhSachNghiHuu.DataSource = SystemContainer.Resolver<INhacViec<ThongTinNghiHuu>>().GetData(obs, param);
            layoutControl1.Invalidate();
        }

        private void gridViewDanhSachNghiHuu_DoubleClick(object sender, EventArgs e)
        {
            ThongTinNghiHuu obj = GetThongTinNghiHuu();
            if (obj != null)
            {
                obs = app.CreateObjectSpace();
                obj = HamDungChung.Copy<ThongTinNghiHuu>(((XPObjectSpace)obs).Session, obj);
                app.ShowView<ThongTinNghiHuu>(obs, obj);
            }
        }
    }
}
