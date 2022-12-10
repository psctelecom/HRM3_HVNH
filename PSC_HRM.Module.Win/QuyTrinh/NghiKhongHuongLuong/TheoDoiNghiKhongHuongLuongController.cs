using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DevExpress.ExpressApp;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.BanLamViec;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.XuLyQuyTrinh.NghiKhongHuongLuong;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.NghiKhongHuongLuong
{
    public partial class TheoDoiNghiKhongHuongLuongController : TheoDoiBaseController
    {
        public TheoDoiNghiKhongHuongLuongController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
        }

        private void TheoDoiNghiKhongHuongLuongController_Load(object sender, EventArgs e)
        {
            gridViewDanhSachNghiHuu.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewDanhSachNghiHuu.ReadOnlyGridView();
            gridViewDanhSachNghiHuu.ShowField(
                new string[] { "BoPhan.TenBoPhan", "ThongTinNhanVien.HoTen", "QuyetDinhNghiKhongHuongLuong.SoQuyetDinh", "TuNgay", "Ngay" },
                new string[] { "Đơn vị", "Cán bộ", "Số quyết định", "Từ ngày", "Đến ngày" },
                new int[] { 150, 150, 100, 80, 80 });

            deDenNgay.DateTime = HamDungChung.GetServerTime();
            GetDuLieu();
        }

        private void btnQDKeoDaiThoiGianCongTac_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<BienDong_GiamLaoDong>())
            {
                ThongTinNghiKhongHuongLuong obj = GetThongTinNghiKhongHuongLuong();
                if (obj != null)
                {
                    IObjectSpace obs = app.CreateObjectSpace();
                    QuyetDinhTiepNhan quyetDinh = obs.CreateObject<QuyetDinhTiepNhan>();
                    quyetDinh.BoPhan = obs.GetObjectByKey<BaoMat.BoPhan>(obj.BoPhan.Oid);
                    quyetDinh.ThongTinNhanVien = obs.GetObjectByKey<HoSo.ThongTinNhanVien>(obj.ThongTinNhanVien.Oid);
                    quyetDinh.TuNgay = obj.Ngay.AddDays(1);
                    app.ShowView<QuyetDinhTiepNhan>(obs, quyetDinh);
                }
            }
            else
                HamDungChung.ShowWarningMessage("Bạn không được cấp quyền");
        }

        private ThongTinNghiKhongHuongLuong GetThongTinNghiKhongHuongLuong()
        {
            return gridViewDanhSachNghiHuu.GetFocusedRow() as ThongTinNghiKhongHuongLuong;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            GetDuLieu();
        }

        protected override void GetDuLieu()
        {
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@TuNgay", deDenNgay.DateTime.SetTime(SetTimeEnum.StartYear)),
                new SqlParameter("@DenNgay", deDenNgay.DateTime.SetTime(SetTimeEnum.EndMonth)) ,
                new SqlParameter("@ThongTinTruong", HamDungChung.ThongTinTruong(((XPObjectSpace)obs).Session).Oid)
            };
            gridDanhSachNghiHuu.DataSource = SystemContainer.Resolver<INhacViec<ThongTinNghiKhongHuongLuong>>().GetData(obs, param);
            layoutControl1.Invalidate();
        }

        private void gridViewDanhSachNghiHuu_DoubleClick(object sender, EventArgs e)
        {
            ThongTinNghiKhongHuongLuong obj = GetThongTinNghiKhongHuongLuong();
            if (obj != null)
            {
                obs = app.CreateObjectSpace();
                obj = HamDungChung.Copy<ThongTinNghiKhongHuongLuong>(((XPObjectSpace)obs).Session, obj);
                app.ShowView<ThongTinNghiKhongHuongLuong>(obs, obj);
            }
        }
    }
}
