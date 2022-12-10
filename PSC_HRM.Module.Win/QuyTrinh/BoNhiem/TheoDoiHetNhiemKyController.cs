using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DevExpress.ExpressApp;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.BanLamViec;
using PSC_HRM.Module.BoNhiem;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.XuLyQuyTrinh.BoNhiem;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.BoNhiem
{
    public partial class TheoDoiHetNhiemKyController : TheoDoiBaseController
    {
        public TheoDoiHetNhiemKyController()
        {
            InitializeComponent();
        }

        public TheoDoiHetNhiemKyController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
        }

        private void TheoDoiNghiHuuController_Load(object sender, EventArgs e)
        {
            gridViewDanhSachNghiHuu.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewDanhSachNghiHuu.ReadOnlyGridView();
            gridViewDanhSachNghiHuu.ShowField(
                new string[] { "BoPhan.TenBoPhan", "ThongTinNhanVien.HoTen", "QuyetDinh.SoQuyetDinh", "ChucVu.TenChucVu", "NgayBoNhiem", "Ngay" },
                new string[] { "Đơn vị", "Cán bộ", "Số quyết định", "Chức vụ", "Ngày bổ nhiệm", "Ngày hết nhiệm kỳ" },
                new int[] { 150, 150, 80, 100, 80, 100 });

            deDenNgay.DateTime = HamDungChung.GetServerTime();
            GetDuLieu();
        }

        private void btnLapDeNghiBoNhiemLai_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<DeNghiBoNhiem>())
            {
                ThongTinBoNhiem obj = GetThongTinBoNhiem();
                if (obj != null)
                {
                    IObjectSpace obs = app.CreateObjectSpace();
                    DeNghiBoNhiem deNghi = obs.CreateObject<DeNghiBoNhiem>();
                    deNghi.BoPhan = obs.GetObjectByKey<BaoMat.BoPhan>(obj.BoPhan.Oid);
                    deNghi.ThongTinNhanVien = obs.GetObjectByKey<HoSo.ThongTinNhanVien>(obj.ThongTinNhanVien.Oid);
                    if (obj.ChucVu != null)
                        deNghi.ChucVu = obs.GetObjectByKey<ChucVu>(obj.ChucVu.Oid);
                    deNghi.KiemNhiem = obj.KiemNhiem;
                    if (obj.TaiBoPhan != null)
                        deNghi.TaiBoPhan = obs.GetObjectByKey<BoPhan>(obj.TaiBoPhan.Oid);
                    
                    app.ShowView<DeNghiBoNhiem>(obs, deNghi);
                }
            }
            else
                HamDungChung.ShowWarningMessage("Bạn không được cấp quyền");
        }

        private void btnQDMienNhiem_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhMienNhiem>() &&
                HamDungChung.IsWriteGranted<QuyetDinhMienNhiemKiemNhiem>())
            {
                ThongTinBoNhiem obj = GetThongTinBoNhiem();
                if (obj != null)
                {
                    IObjectSpace obs = app.CreateObjectSpace();
                    if (obj.KiemNhiem)
                    {
                        QuyetDinhMienNhiemKiemNhiem quyetDinh = obs.CreateObject<QuyetDinhMienNhiemKiemNhiem>();
                        quyetDinh.BoPhan = obs.GetObjectByKey<BaoMat.BoPhan>(obj.BoPhan.Oid);
                        quyetDinh.ThongTinNhanVien = obs.GetObjectByKey<HoSo.ThongTinNhanVien>(obj.ThongTinNhanVien.Oid);
                        quyetDinh.QuyetDinhBoNhiemKiemNhiem = obs.GetObjectByKey<QuyetDinhBoNhiemKiemNhiem>(obj.QuyetDinh.Oid);

                        app.ShowView<QuyetDinhMienNhiemKiemNhiem>(obs, quyetDinh);
                    }
                    else
                    {
                        QuyetDinhMienNhiem quyetDinh = obs.CreateObject<QuyetDinhMienNhiem>();
                        quyetDinh.BoPhan = obs.GetObjectByKey<BaoMat.BoPhan>(obj.BoPhan.Oid);
                        quyetDinh.ThongTinNhanVien = obs.GetObjectByKey<HoSo.ThongTinNhanVien>(obj.ThongTinNhanVien.Oid);
                        quyetDinh.QuyetDinhBoNhiem = obs.GetObjectByKey<QuyetDinhBoNhiem>(obj.QuyetDinh.Oid);

                        app.ShowView<QuyetDinhMienNhiem>(obs, quyetDinh);
                    }
                }
            }
            else
                HamDungChung.ShowWarningMessage("Bạn không được cấp quyền");
        }

        private ThongTinBoNhiem GetThongTinBoNhiem()
        {
            return gridViewDanhSachNghiHuu.GetFocusedRow() as ThongTinBoNhiem;
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
            gridDanhSachNghiHuu.DataSource = SystemContainer.Resolver<INhacViec<ThongTinBoNhiem>>().GetData(obs, param);
            layoutControl1.Invalidate();
        }

        private void gridViewDanhSachNghiHuu_DoubleClick(object sender, EventArgs e)
        {
            ThongTinBoNhiem obj = GetThongTinBoNhiem();
            if (obj != null)
            {
                obs = app.CreateObjectSpace();
                obj = HamDungChung.Copy<ThongTinBoNhiem>(((XPObjectSpace)obs).Session, obj);
                app.ShowView<ThongTinBoNhiem>(obs, obj);
            }
        }
    }
}
