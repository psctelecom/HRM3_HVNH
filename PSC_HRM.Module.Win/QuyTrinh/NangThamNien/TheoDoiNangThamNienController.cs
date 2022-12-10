using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DevExpress.ExpressApp;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.BanLamViec;
using PSC_HRM.Module.XuLyQuyTrinh.NangThamNien;
using PSC_HRM.Module.NangThamNien;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.NangThamNien
{
    public partial class TheoDoiNangThamNienController : TheoDoiBaseController
    {
        private QuanLyNangPhuCapThamNien _QuanLyNangThamNien;

        public TheoDoiNangThamNienController(XafApplication app, IObjectSpace obs, QuanLyNangPhuCapThamNien quanLy)
            : base(app, obs)
        {
            InitializeComponent();

            _QuanLyNangThamNien = quanLy;
        }

        private void TheoDoiNghiHuuController_Load(object sender, EventArgs e)
        {
            gridViewDanhSachNghiHuu.InitGridView(true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewDanhSachNghiHuu.ReadOnlyGridView();
            gridViewDanhSachNghiHuu.ShowField(
                new string[] { "BoPhan.TenBoPhan", "ThongTinNhanVien.HoTen", "ThamNienMoi", "Ngay" },
                new string[] { "Đơn vị", "Cán bộ", "Thâm niên mới", "Ngày nâng thâm niên" },
                new int[] { 150, 150, 80, 80 });

            deDenNgay.DateTime = HamDungChung.GetServerTime();
            GetDuLieu();
        }

        private void miLapDeNghiNangThamNien_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyNangPhuCapThamNien>())
            {
                obs = app.CreateObjectSpace();
                List<ThongTinNangThamNien> obj = GetThongTinNangThamNien();
                DeNghiNangPhuCapThamNien deNghi = obs.CreateObject<DeNghiNangPhuCapThamNien>();
                deNghi.QuanLyNangPhuCapThamNien = obs.GetObjectByKey<QuanLyNangPhuCapThamNien>(_QuanLyNangThamNien.Oid);
                ChiTietDeNghiNangPhuCapThamNien chiTiet;
                foreach (var item in obj)
                {
                    chiTiet = obs.CreateObject<ChiTietDeNghiNangPhuCapThamNien>();
                    chiTiet.DeNghiNangPhuCapThamNien = deNghi;
                    chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    chiTiet.ThamNienCu = item.ThamNienCu;
                    chiTiet.NgayHuongThamNienCu = item.NgayHuongThamNienCu;
                    chiTiet.ThamNienMoi = item.ThamNienMoi;
                    chiTiet.NgayHuongThamNienMoi = item.Ngay;
                    deNghi.ListChiTietDeNghiNangPhuCapThamNien.Add(chiTiet);
                }

                app.ShowView<DeNghiNangPhuCapThamNien>(obs, deNghi);
            }
            else
                HamDungChung.ShowWarningMessage("Bạn không được cấp quyền truy cập");
        }

        private void miLapQDNangThamNien_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyNangPhuCapThamNien>())
            {
                List<ThongTinNangThamNien> obj = GetThongTinNangThamNien();
                obs = app.CreateObjectSpace();
                QuyetDinhNangPhuCapThamNienNhaGiao quyetDinh = obs.CreateObject<QuyetDinhNangPhuCapThamNienNhaGiao>();
                quyetDinh.QuyetDinhMoi = true;
                ChiTietQuyetDinhNangPhuCapThamNienNhaGiao chiTiet;
                foreach (var item in obj)
                {
                    chiTiet = obs.CreateObject<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>();
                    chiTiet.QuyetDinhNangPhuCapThamNienNhaGiao = quyetDinh;
                    chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    chiTiet.ThamNienCu = item.ThamNienCu;
                    chiTiet.NgayHuongThamNienCu = item.NgayHuongThamNienCu;
                    chiTiet.ThamNienMoi = item.ThamNienMoi;
                    chiTiet.NgayHuongThamNienMoi = item.Ngay;
                    quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Add(chiTiet);
                }

                app.ShowView<QuyetDinhNangPhuCapThamNienNhaGiao>(obs, quyetDinh);
            }
            else
                HamDungChung.ShowWarningMessage("Bạn không được cấp quyền truy cập");
        }

        private List<ThongTinNangThamNien> GetThongTinNangThamNien()
        {
            List<ThongTinNangThamNien> result = new List<ThongTinNangThamNien>();
            int[] selects = gridViewDanhSachNghiHuu.GetSelectedRows();
            foreach (int item in selects)
            {
                result.Add((ThongTinNangThamNien)gridViewDanhSachNghiHuu.GetRow(item));
            }
            return result;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            GetDuLieu();
        }

        protected override void GetDuLieu()
        {
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@TuNgay", new DateTime(2008, 1, 1).SetTime(SetTimeEnum.StartMonth)),
                new SqlParameter("@DenNgay", deDenNgay.DateTime.SetTime(SetTimeEnum.EndMonth)),
                new SqlParameter("@ThongTinTruong", HamDungChung.ThongTinTruong(((XPObjectSpace)obs).Session).Oid)
            };
            gridDanhSachNghiHuu.DataSource = SystemContainer.Resolver<INhacViec<ThongTinNangThamNien>>().GetData(obs, param);
            layoutControl1.Invalidate();
        }

        private void gridViewDanhSachNghiHuu_DoubleClick(object sender, EventArgs e)
        {
            ThongTinNangThamNien obj = gridViewDanhSachNghiHuu.GetFocusedRow() as ThongTinNangThamNien;
            if (obj != null)
            {
                obs = app.CreateObjectSpace();
                obj = HamDungChung.Copy<ThongTinNangThamNien>(((XPObjectSpace)obs).Session, obj);
                app.ShowView<ThongTinNangThamNien>(obs, obj);
            }
        }
    }
}
