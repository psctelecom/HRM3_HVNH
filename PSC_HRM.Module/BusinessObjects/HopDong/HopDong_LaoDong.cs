using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Text;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.TaoMaQuanLy;
using System.Data.SqlClient;


namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_Contract")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng lao động")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyHopDong;SoHopDong")]
    [Appearance("Hide_UFM", TargetItems = "DieuKhoanHopDong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UFM'")]
   // [Appearance("HopDongLaoDongKhacThuViec", TargetItems = "TapSuTuNgay;TapSuDenNgay", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoai!=0")]
    [Appearance("HopDongLaoDongKhongThoiHan", TargetItems = "HinhThucHopDong;DenNgay", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoai=2")]

    public class HopDong_LaoDong : HopDong_NhanVien
    {
        private HinhThucThanhToanEnum _HinhThucThanhToan;
        private HopDongLaoDongEnum _PhanLoai;
        private DieuKhoanHopDong _DieuKhoanHopDong;
        private DateTime _MocNangLuong;
        private bool _LuongChinhHopDongTrongChiTieuBienChe;
        //QNU
        private decimal _MucLuong;
        private decimal _PhuCapTienAn;
        private decimal _PhuCapDocHai;

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại")]
        public HopDongLaoDongEnum PhanLoai
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
                    if (value == HopDongLaoDongEnum.TapSuThuViec)
                        LoaiHopDong = "Hợp đồng lao động thử việc";
                    else if (value == HopDongLaoDongEnum.CoThoiHan)
                        LoaiHopDong = "Hợp đồng lao động có thời hạn";
                    else
                    {
                        LoaiHopDong = "Hợp đồng lao động không thời hạn";
                        DenNgay = DateTime.MinValue;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Lương chính hợp đồng trong chỉ tiêu biên chế")]
        public bool LuongChinhHopDongTrongChiTieuBienChe
        {
            get
            {
                return _LuongChinhHopDongTrongChiTieuBienChe;
            }
            set
            {
                SetPropertyValue("LuongChinhHopDongTrongChiTieuBienChe", ref _LuongChinhHopDongTrongChiTieuBienChe, value);
            }
        }

        [ModelDefault("Caption", "Hình thức thanh toán")]
        public HinhThucThanhToanEnum HinhThucThanhToan
        {
            get
            {
                return _HinhThucThanhToan;
            }
            set
            {
                SetPropertyValue("HinhThucThanhToan", ref _HinhThucThanhToan, value);
            }
        }

        [Aggregated]
        [ImmediatePostData]
        [ModelDefault("Caption", "Điều khoản hợp đồng")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
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

        [ModelDefault("Caption", "Mức lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal MucLuong
        {
            get
            {
                return _MucLuong;
            }
            set
            {
                SetPropertyValue("MucLuong", ref _MucLuong, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền ăn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienAn
        {
            get
            {
                return _PhuCapTienAn;
            }
            set
            {
                SetPropertyValue("PhuCapTienAn", ref _PhuCapTienAn, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp độc hại")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapDocHai
        {
            get
            {
                return _PhuCapDocHai;
            }
            set
            {
                SetPropertyValue("PhuCapDocHai", ref _PhuCapDocHai, value);
            }
        }

        public HopDong_LaoDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            PhanLoai = HopDongLaoDongEnum.TapSuThuViec;
            DieuKhoanHopDong = new DieuKhoanHopDong(Session);
            DieuKhoanHopDong.OnDieuKhoanChanged += DieuKhoanHopDong_OnDieuKhoanChanged;
            UpdateNhanVienList();
            MaTruong = TruongConfig.MaTruong;
        }

        protected override void OnLoaded()
        {
            base.OnLoading();
            MaTruong = TruongConfig.MaTruong;
            //
            if (!string.IsNullOrEmpty(this.SoHopDong))
            {
                this.SoHopDongFull = SoHopDong;
            }
            //
        }

        protected override void AfterNhanVienChanged()
        {
            if (NhanVien != null)
            {
                //
                DieuKhoanHopDong.NgachLuong = NhanVien.NhanVienThongTinLuong.NgachLuong;
                DieuKhoanHopDong.BacLuong = NhanVien.NhanVienThongTinLuong.BacLuong;
                DieuKhoanHopDong.HeSoLuong = NhanVien.NhanVienThongTinLuong.HeSoLuong;
                DieuKhoanHopDong.VuotKhung = NhanVien.NhanVienThongTinLuong.VuotKhung;    
                if (NhanVien.NhanVienThongTinLuong.NgachLuong != null)
                    ChucDanhChuyenMon = NhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong;
                MocNangLuong = NhanVien.NhanVienThongTinLuong.MocNangLuong;
                MucLuong = NhanVien.NhanVienThongTinLuong.LuongKhoan;

                //chức danh chuyên môn
                CriteriaOperator filter = CriteriaOperator.Parse("Oid=? and LoaiNhanSu is not null", NhanVien.Oid);
                ThongTinNhanVien thongTinNhanVien = Session.FindObject<ThongTinNhanVien>(filter);
                if (thongTinNhanVien != null && thongTinNhanVien.LoaiNhanSu != null)
                {
                    //ChucDanhChuyenMon = thongTinNhanVien.LoaiNhanSu.TenLoaiNhanSu;
                    
                    //Chức vụ
                    ChucVu = thongTinNhanVien.ChucVu;
                }
                //quốc tịch
                QuocTich = NhanVien.QuocTich;
                NgaySinh = NhanVien.NgaySinh;
                NoiSinh = NhanVien.NoiSinh;
                CMND = NhanVien.CMND;
                NgayCap = NhanVien.NgayCap;
                NoiCap = NhanVien.NoiCap;
                if (TruongConfig.MaTruong.Equals("QNU"))
                    NoiLamViec = ThongTinTruong.DiaChi.FullDiaChi;
                else
                    NoiLamViec = BoPhan.TenBoPhan;
                CongViecTuyenDung = NhanVien.CongViecTuyenDung;
            }
        }

        protected override void TaoSoHopDong()
        {
            SqlParameter param = new SqlParameter("@QuanLyHopDong", QuanLyHopDong.Oid);
            SoHopDong = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoHopDongLaoDong, param);
        }

        protected override void TaoTrichYeu()
        {
            StringBuilder sb = new StringBuilder();
            if (PhanLoai == HopDongLaoDongEnum.TapSuThuViec)
                sb.Append("Hợp đồng lao động thử việc");
            else if (PhanLoai == HopDongLaoDongEnum.CoThoiHan)
                sb.Append("Hợp đồng lao động có thời hạn");
            else
                sb.Append("Hợp đồng lao động không thời hạn");

            if (TuNgay != DateTime.MinValue)
                sb.Append("; Từ ngày " + TuNgay.ToString("d"));
            if (DenNgay != DateTime.MinValue)
                sb.Append(" đến ngày " + DenNgay.ToString("d"));

            sb.Append("; Hưởng " + (DieuKhoanHopDong != null ? (DieuKhoanHopDong.Huong85PhanTramMucLuong ? "85%" : "100%") : ""));
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
                if (TruongConfig.MaTruong.Equals("QNU"))
                {
                    ThongTinNhanVien nhanVien = Session.GetObjectByKey<ThongTinNhanVien>(NhanVien.Oid);
                    if (nhanVien != null)
                    {
                        CriteriaOperator filter = null;
                        if (PhanLoai == HopDongLaoDongEnum.TapSuThuViec)
                        {
                            filter = CriteriaOperator.Parse("TenLoaiNhanVien like ?", "%tập sự%");
                        }
                        else if (PhanLoai == HopDongLaoDongEnum.CoThoiHan)
                        {
                            filter = CriteriaOperator.Parse("TenLoaiNhanVien like ? or TenLoaiNhanVien like ?", "%lao động có thời hạn%", "%lao động xác định%");
                        }
                        else if (PhanLoai == HopDongLaoDongEnum.KhongThoiHan)
                        {
                            filter = CriteriaOperator.Parse("TenLoaiNhanVien like ? or TenLoaiNhanVien like ?", "%không thời hạn%", "%không xác định%");
                        }
                        nhanVien.LoaiNhanVien = Session.FindObject<LoaiNhanVien>(filter);


                        //Cập nhật thông tin lương QNU
                        if (TuNgay <= HamDungChung.GetServerTime())
                        {
                            NhanVien.NhanVienThongTinLuong.LuongKhoan = MucLuong;
                            NhanVien.NhanVienThongTinLuong.PhuCapTienAn = PhuCapTienAn;
                            NhanVien.NhanVienThongTinLuong.PhuCapTienDocHai = PhuCapDocHai;
                        }
                    }
                }
                else
                {

                    ThongTinNhanVien nhanVien = Session.GetObjectByKey<ThongTinNhanVien>(NhanVien.Oid);
                    if (nhanVien != null)
                    {
                        CriteriaOperator filter = null;
                        if (PhanLoai == HopDongLaoDongEnum.TapSuThuViec)
                        {
                            filter = CriteriaOperator.Parse("TenLoaiNhanVien like ?", "%tập sự%");
                        }
                        else if (PhanLoai == HopDongLaoDongEnum.CoThoiHan)
                        {
                            filter = CriteriaOperator.Parse("TenLoaiNhanVien like ? or TenLoaiNhanVien like ?", "%có thời hạn%", "%xác định%");
                        }
                        else if (PhanLoai == HopDongLaoDongEnum.KhongThoiHan)
                        {
                            filter = CriteriaOperator.Parse("TenLoaiNhanVien like ? or TenLoaiNhanVien like ?", "%không thời hạn%", "%không xác định%");
                        }
                        nhanVien.LoaiNhanVien = Session.FindObject<LoaiNhanVien>(filter);
                    }

                }
            }



        }
    }

}
