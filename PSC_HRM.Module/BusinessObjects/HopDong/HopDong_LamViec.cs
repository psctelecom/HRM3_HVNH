using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Text;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.TaoMaQuanLy;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_Contract")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng làm việc")]
    [Appearance("HopDongLamViecKhacLanDau", TargetItems = "TapSuTuNgay;TapSuDenNgay;NgheNghiepTruocKhiDuocTuyen", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoai!=0")]
    [Appearance("HopDongLamViecKhongThoiHan", TargetItems = "DieuKhoanHopDong.Huong85PhanTramMucLuong;HinhThucHopDong;DenNgay", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoai=2")]
    [Appearance("HopDongLamViecTapSu", TargetItems = "MocNangLuong", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoai=0")]
    
    public class HopDong_LamViec : HopDong_NhanVien
    {
        // Fields...
        private HopDongLamViecEnum _PhanLoai;
        private DieuKhoanHopDong _DieuKhoanHopDong;
        private DateTime _MocNangLuong;
        private DateTime _NgayBatDauDongBaoHiem;

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại")]
        public HopDongLamViecEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
                if (!IsLoading)
                {
                    if (value == HopDongLamViecEnum.HopDongLanDau)
                        LoaiHopDong = "Hợp đồng làm việc lần đầu";
                    else if (value == HopDongLamViecEnum.CoThoiHan)
                        LoaiHopDong = "Hợp đồng làm việc có thời hạn";
                    else
                    {
                        LoaiHopDong = "Hợp đồng làm việc không thời hạn";
                        DenNgay = DateTime.MinValue;
                    }
                    
                }
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương")]
        public DateTime MocNangLuong
        {
            get
            {
                return _MocNangLuong;
            }
            set
            {
                SetPropertyValue("MocNangLuong", ref _MocNangLuong, value);
            }
        }

        [Aggregated]
        [ImmediatePostData]
        [ModelDefault("Caption", "Điều khoản hợp đồng")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DieuKhoanHopDong DieuKhoanHopDong
        {
            get
            {
                return _DieuKhoanHopDong;
            }
            set
            {
                SetPropertyValue("DieuKhoanHopDong", ref _DieuKhoanHopDong, value);
                if (!IsLoading && value != null)
                {
                    TaoTrichYeu();
                }
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu đóng bảo hiểm")]
        public DateTime NgayBatDauDongBaoHiem
        {
            get
            {
                return _NgayBatDauDongBaoHiem;
            }
            set
            {
                SetPropertyValue("NgayBatDauDongBaoHiem", ref _NgayBatDauDongBaoHiem, value);
            }
        }

        public HopDong_LamViec(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DieuKhoanHopDong = new DieuKhoanHopDong(Session);
            DieuKhoanHopDong.OnDieuKhoanChanged += DieuKhoanHopDong_OnDieuKhoanChanged;
            PhanLoai = HopDongLamViecEnum.HopDongLanDau;
            UpdateNhanVienList();
            MaTruong = TruongConfig.MaTruong;
        }

        protected override void AfterNhanVienChanged()
        {
            if (NhanVien != null)
            {
                ////chức danh chuyên môn
                CriteriaOperator filter = CriteriaOperator.Parse("Oid=? and LoaiNhanSu is not null", NhanVien.Oid);
                ThongTinNhanVien thongTinNhanVien = Session.FindObject<ThongTinNhanVien>(filter);
                //if (thongTinNhanVien != null && thongTinNhanVien.LoaiNhanSu != null)
                //    ChucDanhChuyenMon = thongTinNhanVien.LoaiNhanSu.TenLoaiNhanSu;

                //Chức vụ
                if (thongTinNhanVien != null && thongTinNhanVien.ChucVu != null)
                    ChucVu = thongTinNhanVien.ChucVu;
                
                //
                DieuKhoanHopDong.NgachLuong = NhanVien.NhanVienThongTinLuong.NgachLuong;
                DieuKhoanHopDong.BacLuong = NhanVien.NhanVienThongTinLuong.BacLuong;
                DieuKhoanHopDong.HeSoLuong = NhanVien.NhanVienThongTinLuong.HeSoLuong;
                DieuKhoanHopDong.VuotKhung = NhanVien.NhanVienThongTinLuong.VuotKhung;
                if (NhanVien.NhanVienThongTinLuong.NgachLuong != null)
                    ChucDanhChuyenMon = NhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong;
                MocNangLuong = NhanVien.NhanVienThongTinLuong.MocNangLuong;

                //quốc tịch
                QuocTich = NhanVien.QuocTich;
                NgaySinh = NhanVien.NgaySinh;
                NoiSinh = NhanVien.NoiSinh;
                CMND = NhanVien.CMND;
                NgayCap = NhanVien.NgayCap;
                NoiCap = NhanVien.NoiCap;
                NoiLamViec = BoPhan.TenBoPhan;

            }
        }

        protected override void TaoSoHopDong()
        {
            SqlParameter param = new SqlParameter("@QuanLyHopDong", QuanLyHopDong.Oid);
            SoHopDong = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoHopDongLamViec, param);
        }

        protected override void TaoTrichYeu()
        {
            StringBuilder sb = new StringBuilder();
            if (PhanLoai == HopDongLamViecEnum.HopDongLanDau)
                sb.Append("Hợp đồng làm việc lần đầu");
            else if (PhanLoai == HopDongLamViecEnum.CoThoiHan)
                sb.Append("Hợp đồng làm việc có thời hạn");
            else
                sb.Append("Hợp đồng làm việc không thời hạn");

            if (TuNgay != DateTime.MinValue)
                sb.Append("; Từ ngày " + TuNgay.ToString("d"));
            if (DenNgay != DateTime.MinValue)
                sb.Append(" đến ngày " + DenNgay.ToString("d"));

            sb.Append("; Hưởng " + (DieuKhoanHopDong.Huong85PhanTramMucLuong ? "85%" : "100%"));
            sb.Append(ObjectFormatter.Format(" ngạch {NgachLuong.TenNgachLuong} ({NgachLuong.MaQuanLy}) bậc {BacLuong.MaQuanLy} hệ số {HeSoLuong:N2};", DieuKhoanHopDong, EmptyEntriesMode.RemoveDelimeterWhenEntryIsEmpty));

            if (GiayToHoSo != null)
                GiayToHoSo.TrichYeu = sb.ToString();
        }

        private void DieuKhoanHopDong_OnDieuKhoanChanged(object sender, EventArgs e)
        {
            TaoTrichYeu();
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && !HopDongCu )
            {
                ThongTinNhanVien nhanVien = Session.GetObjectByKey<ThongTinNhanVien>(NhanVien.Oid);
                if (nhanVien != null)
                {
                    CriteriaOperator filter = null;
                    if (PhanLoai == HopDongLamViecEnum.HopDongLanDau)
                    {
                        filter = CriteriaOperator.Parse("TenLoaiNhanVien like ?", "%tập sự%");
                    }
                    else if (PhanLoai == HopDongLamViecEnum.CoThoiHan)
                    {
                        filter = CriteriaOperator.Parse("TenLoaiNhanVien like ? or TenLoaiNhanVien like ?", "%có thời hạn%", "%có xác định%");
                    }
                    else if (PhanLoai == HopDongLamViecEnum.KhongThoiHan)
                    {
                        filter = CriteriaOperator.Parse("TenLoaiNhanVien like ? or TenLoaiNhanVien like ?", "%không thời hạn%", "%không xác định%");
                    }
                    nhanVien.LoaiNhanVien = Session.FindObject<LoaiNhanVien>(filter);
                }
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            MaTruong = TruongConfig.MaTruong;
            //
            if (!string.IsNullOrEmpty(this.SoHopDong))
            {
                this.SoHopDongFull = SoHopDong;
            }
            //
        }
    }
}
