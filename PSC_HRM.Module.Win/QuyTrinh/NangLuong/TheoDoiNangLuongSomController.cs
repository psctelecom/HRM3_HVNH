using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DevExpress.ExpressApp;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.XuLyQuyTrinh.NangLuong;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using System.Data;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.NangLuong
{
    public partial class TheoDoiNangLuongSomController : TheoDoiBaseController
    {
        private QuanLyNangLuong _QuanLyNangLuong;

        public TheoDoiNangLuongSomController(XafApplication app, IObjectSpace obs, QuanLyNangLuong quanLyNangLuong)
            : base(app, obs)
        {
            InitializeComponent();

            _QuanLyNangLuong = quanLyNangLuong;
        }

        private void TheoDoiNangLuongSomController_Load(object sender, EventArgs e)
        {
            gridViewNangLuong.InitGridView(true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewNangLuong.ReadOnlyGridView();
            gridViewNangLuong.ShowField(
                new string[] { "BoPhan.TenBoPhan", "ThongTinNhanVien.HoTen", "NgachLuong.TenNgachLuong", "BacLuongMoi.TenBacLuong", "HeSoLuongMoi", "VuotKhungMoi", "MocNangLuongMoi", "GhiChu" },
                new string[] { "Đơn vị", "Cán bộ", "Ngạch lương", "Bậc lương mới", "Hệ số lương mới", "Vượt khung mới", "Mốc nâng lương mới", "Ghi chú" },
                new int[] { 200, 150, 120, 80, 80, 80, 80, 200 });
            gridViewNangLuong.GroupField("BoPhan.TenBoPhan");
            gridViewNangLuong.DisplayFormat("HeSoLuongMoi", DevExpress.Utils.FormatType.Numeric, "N2");
            
            DateTime tam  = HamDungChung.GetServerTime();
            deTuNgay.DateTime = tam.SetTime(SetTimeEnum.StartMonth);
            deDenNgay.DateTime = tam.SetTime(SetTimeEnum.EndMonth);

            GetDuLieu();            
        }

        private void btnLapDeNghiNangLuong_Click(object sender, EventArgs e)
        {
            List<ThongTinNangLuongSom> obj = GetListThongTinNangLuong();
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
                chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                chiTiet.PhanLoai = NangLuongEnum.CoThanhTichXuatSac;
                deNghi.ListChiTietDeNghiNangLuong.Add(chiTiet);

                app.ShowView<DeNghiNangLuong>(obs, deNghi);
                GetDuLieu();
            }
        }

        private void btnQDNangLuong_Click(object sender, EventArgs e)
        {
            List<ThongTinNangLuongSom> obj = GetListThongTinNangLuong();
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
                chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                quyetDinh.ListChiTietQuyetDinhNangLuong.Add(chiTiet);

                app.ShowView<QuyetDinhNangLuong>(obs, quyetDinh);
                GetDuLieu();
            }
        }

        private List<ThongTinNangLuongSom> GetListThongTinNangLuong()
        {
            var result = new List<ThongTinNangLuongSom>();
            var selects = gridViewNangLuong.GetSelectedRows();
            foreach (var item in selects)
            {
                result.Add(gridViewNangLuong.GetRow(item) as ThongTinNangLuongSom);
            }
            
            return result;
        }

        private ThongTinNangLuongSom GetThongTinNangLuong()
        {
            return gridViewNangLuong.GetFocusedRow() as ThongTinNangLuongSom;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            GetDuLieu();            
        }

        protected override void GetDuLieu()
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@TuNgay", deTuNgay.DateTime);
            para[1] = new SqlParameter("@DenNgay", deDenNgay.DateTime);

            List<ThongTinNangLuongSom> data = new List<ThongTinNangLuongSom>();
            using (DataTable dt = DataProvider.GetDataTable("spd_System_NangLuongSom", CommandType.StoredProcedure, para))
            {
                ThongTinNangLuongSom item;
                int soThang;
                foreach (DataRow dr in dt.Rows)
                { 
                    item = obs.CreateObject<ThongTinNangLuongSom>();
                    item.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(Guid.Parse(dr[0].ToString()));                    
                    //soThang = item.MocNangLuongCu.TinhSoThang(deDenNgay.DateTime);
                    if (item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.ThoiGianNangBac == 36)
                    {
                        soThang = 24;
                        item.MocNangLuongMoi = item.MocNangLuongCu.AddMonths(soThang);
                    }
                    if (item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.ThoiGianNangBac == 24)
                    {
                        soThang = 18;
                        item.MocNangLuongMoi = item.MocNangLuongCu.AddMonths(soThang);
                    }
                    item.GhiChu = dr[1].ToString();
                    data.Add(item);
                }
            }
            gridNangLuong.DataSource = data; 
            layoutControl1.Invalidate();
        }

        private void gridViewNangLuong_DoubleClick(object sender, EventArgs e)
        {
            ThongTinNangLuongSom obj = GetThongTinNangLuong();
            if (obj != null)
            {
                obs = app.CreateObjectSpace();
                obj = HamDungChung.Copy<ThongTinNangLuongSom>(((XPObjectSpace)obs).Session, obj);
                app.ShowView<ThongTinNangLuongSom>(obs, obj);
            }
        }
    }
}
