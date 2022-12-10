using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DevExpress.ExpressApp;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.BanLamViec;
using PSC_HRM.Module.XuLyQuyTrinh.NangLuong;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.NangLuong
{
    public partial class TheoDoiNangLuongController : TheoDoiBaseController
    {
        private QuanLyNangLuong _QuanLyNangLuong;

        public TheoDoiNangLuongController(XafApplication app, IObjectSpace obs, QuanLyNangLuong quanLyNangLuong)
            : base(app, obs)
        {
            InitializeComponent();

            _QuanLyNangLuong = quanLyNangLuong;
        }

        private void TheoDoiNghiHuuController_Load(object sender, EventArgs e)
        {
            gridViewNangLuong.InitGridView(true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewNangLuong.ReadOnlyGridView();
            gridViewNangLuong.ShowField(
                new string[] { "BoPhan.TenBoPhan", "ThongTinNhanVien.HoTen", "NgachLuong.TenNgachLuong", "BacLuongMoi.TenBacLuong", "HeSoLuongMoi", "VuotKhungMoi", "Ngay" },
                new string[] { "Đơn vị", "Cán bộ", "Ngạch lương", "Bậc lương mới", "Hệ số lương mới", "Vượt khung mới", "Mốc nâng lương mới" },
                new int[] { 200, 150, 120, 80, 80, 80, 80 });
            gridViewNangLuong.GroupField("BoPhan.TenBoPhan");
            gridViewNangLuong.DisplayFormat("HeSoLuongMoi", DevExpress.Utils.FormatType.Numeric, "N2");

            //deDenNgay.DateTime = HamDungChung.GetServerTime();
            DateTime tam = HamDungChung.GetServerTime();
            deTuNgay.DateTime = tam.SetTime(SetTimeEnum.StartMonth);
            deDenNgay.DateTime = tam.SetTime(SetTimeEnum.EndMonth);

            GetDuLieu();
        }

        private void btnLapDeNghiNangLuong_Click(object sender, EventArgs e)
        {
            List<ThongTinNangLuong> obj = GetListThongTinNangLuong();
            obs = app.CreateObjectSpace();
            DeNghiNangLuong deNghi = obs.CreateObject<DeNghiNangLuong>();
            deNghi.QuanLyNangLuong = obs.GetObjectByKey<QuanLyNangLuong>(_QuanLyNangLuong.Oid);
            ChiTietDeNghiNangLuong chiTiet;
            foreach (var item in obj)
            {
                chiTiet = obs.CreateObject<ChiTietDeNghiNangLuong>();
                chiTiet.DeNghiNangLuong = deNghi;
                chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                chiTiet.NgachLuong = obs.GetObjectByKey<NgachLuong>(item.NgachLuong.Oid);
                chiTiet.BacLuongCu = obs.GetObjectByKey<BacLuong>(item.BacLuongCu.Oid);
                chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                chiTiet.VuotKhungCu = item.VuotKhungCu;
                chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                chiTiet.BacLuongMoi = obs.GetObjectByKey<BacLuong>(item.BacLuongMoi.Oid);
                chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                chiTiet.VuotKhungMoi = item.VuotKhungMoi;
                chiTiet.MocNangLuongMoi = item.Ngay;
                deNghi.ListChiTietDeNghiNangLuong.Add(chiTiet);
            }
            app.ShowView<DeNghiNangLuong>(obs, deNghi);
            GetDuLieu();
        }

        private void btnQDNangLuong_Click(object sender, EventArgs e)
        {
            List<ThongTinNangLuong> obj = GetListThongTinNangLuong();
            obs = app.CreateObjectSpace();
            QuyetDinhNangLuong quyetDinh = obs.CreateObject<QuyetDinhNangLuong>();
            quyetDinh.QuyetDinhMoi = true;
            ChiTietQuyetDinhNangLuong chiTiet;
            foreach (var item in obj)
            {
                chiTiet = obs.CreateObject<ChiTietQuyetDinhNangLuong>();
                chiTiet.QuyetDinhNangLuong = quyetDinh;
                chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                chiTiet.NgachLuong = obs.GetObjectByKey<NgachLuong>(item.NgachLuong.Oid);
                chiTiet.BacLuongCu = obs.GetObjectByKey<BacLuong>(item.BacLuongCu.Oid);
                chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                chiTiet.VuotKhungCu = item.VuotKhungCu;
                chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                chiTiet.BacLuongMoi = obs.GetObjectByKey<BacLuong>(item.BacLuongMoi.Oid);
                chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                chiTiet.VuotKhungMoi = item.VuotKhungMoi;
                chiTiet.MocNangLuongMoi = item.Ngay;
                quyetDinh.ListChiTietQuyetDinhNangLuong.Add(chiTiet);
            }

            app.ShowView<QuyetDinhNangLuong>(obs, quyetDinh);
            GetDuLieu();
        }

        private List<ThongTinNangLuong> GetListThongTinNangLuong()
        {
            List<ThongTinNangLuong> result = new List<ThongTinNangLuong>();
            int[] selects = gridViewNangLuong.GetSelectedRows();
            foreach (int item in selects)
            {
                result.Add(gridViewNangLuong.GetRow(item) as ThongTinNangLuong);
            }
            return result;
        }

        private ThongTinNangLuong GetThongTinNangLuong()
        {
            return gridViewNangLuong.GetFocusedRow() as ThongTinNangLuong;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            GetDuLieu();
        }

        protected override void GetDuLieu()
        {
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@TuNgay", deTuNgay.DateTime),
                new SqlParameter("@DenNgay", deDenNgay.DateTime),
                new SqlParameter("@ThongTinTruong", HamDungChung.ThongTinTruong(((XPObjectSpace)obs).Session).Oid) 
            };
            gridNangLuong.DataSource = SystemContainer.Resolver<INhacViec<ThongTinNangLuong>>().GetData(obs, param);
            layoutControl1.Invalidate();
        }

        private void gridViewNangLuong_DoubleClick(object sender, EventArgs e)
        {
            ThongTinNangLuong obj = GetThongTinNangLuong();
            if (obj != null)
            {
                obs = app.CreateObjectSpace();
                obj = HamDungChung.Copy<ThongTinNangLuong>(((XPObjectSpace)obs).Session, obj);
                app.ShowView<ThongTinNangLuong>(obs, obj);
            }
        }
    }
}
