using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Validation;

namespace PSC_HRM.Module.CauHinh
{
    [DefaultClassOptions]
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình chung")]
    public class CauHinhChung : TruongBaseObject
    {
        // Fields...
        private CauHinhNhacViec _CauHinhNhacViec;
        private CauHinhBaoHiem _CauHinhBaoHiem;
        private CauHinhWebsite _CauHinhWebsite;
        private CauHinhTuyenDung _CauHinhTuyenDung;
        private CauHinhHopDong _CauHinhHopDong;
        private CauHinhHoSo _CauHinhHoSo;
        private ThongTinTruong _ThongTinTruong;
        private string _NoiLuuTruGiayTo;
        private HocKy _HocKy;
        private NamHoc _NamHoc; 
        private QuocGia _QuocGia;
        private string _Username;
        private string _Password;
        private CauHinhQuyetDinh _CauHinhQuyetDinh;
        private CauHinhEmail _CauHinhEmail;
        private CauHinhThongTinLuong _CauHinhThongTinLuong;
        private bool _jobStop;
        private int _SoThangTrongNam;
        private int _SoGioChuan;
        private int _SoGioChuan_NCHK;
        private int _SoGioChuan_Khac;
        private int _SoGioChuan_CoVanHocTap;
        private int _SoGioChuan_KiemGiang;
        private bool _DongBoTaiKhoan;

        [ModelDefault("Caption", "Thông tin trường")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }

        [ModelDefault("Caption", "Nơi lưu trữ giấy tờ")]
        public string NoiLuuTruGiayTo
        {
            get
            {
                return _NoiLuuTruGiayTo;
            }
            set
            {
                SetPropertyValue("NoiLuuTruGiayTo", ref _NoiLuuTruGiayTo, value);
            }
        }

        [ModelDefault("Caption", "Username")]
        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                SetPropertyValue("Username", ref _Username, value);
            }
        }

        [ModelDefault("IsPassword", "True")]
        [ModelDefault("Caption", "Password")]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                SetPropertyValue("Password", ref _Password, value);
            }
        }

        [ModelDefault("Caption", "Quốc gia")]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học hiện tại")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null)
                {
                    DateTime current = HamDungChung.GetServerTime();
                    HocKy = Session.FindObject<HocKy>(CriteriaOperator.Parse("NamHoc=? and TuNgay<=? and DenNgay>=?", value.Oid, current, current));
                }
            }
        }

        [ModelDefault("Caption", "Học kỳ hiện tại")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        public HocKy HocKy
        {
            get
            {
                return _HocKy;
            }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình nhắc việc")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhNhacViec CauHinhNhacViec
        {
            get
            {
                return _CauHinhNhacViec;
            }
            set
            {
                SetPropertyValue("CauHinhNhacViec", ref _CauHinhNhacViec, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình hồ sơ")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhHoSo CauHinhHoSo
        {
            get
            {
                return _CauHinhHoSo;
            }
            set
            {
                SetPropertyValue("CauHinhHoSo", ref _CauHinhHoSo, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình hợp đồng")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhHopDong CauHinhHopDong
        {
            get
            {
                return _CauHinhHopDong;
            }
            set
            {
                SetPropertyValue("CauHinhHopDong", ref _CauHinhHopDong, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình quyết định")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhQuyetDinh CauHinhQuyetDinh
        {
            get
            {
                return _CauHinhQuyetDinh;
            }
            set
            {
                SetPropertyValue("CauHinhQuyetDinh", ref _CauHinhQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình email")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhEmail CauHinhEmail
        {
            get
            {
                return _CauHinhEmail;
            }
            set
            {
                SetPropertyValue("CauHinhEmail", ref _CauHinhEmail, value);
            }
        }
        //
        [ModelDefault("Caption", "Cấu hình thông tin lương")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhThongTinLuong CauHinhThongTinLuong
        {
            get
            {
                return _CauHinhThongTinLuong;
            }
            set
            {
                SetPropertyValue("CauHinhThongTinLuong", ref _CauHinhThongTinLuong, value);
            }
        }
        //
        [ModelDefault("Caption", "Cấu hình tuyển dụng")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhTuyenDung CauHinhTuyenDung
        {
            get
            {
                return _CauHinhTuyenDung;
            }
            set
            {
                SetPropertyValue("CauHinhTuyenDung", ref _CauHinhTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình Website")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhWebsite CauHinhWebsite
        {
            get
            {
                return _CauHinhWebsite;
            }
            set
            {
                SetPropertyValue("CauHinhWebsite", ref _CauHinhWebsite, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình bảo hiểm")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhBaoHiem CauHinhBaoHiem
        {
            get
            {
                return _CauHinhBaoHiem;
            }
            set
            {
                SetPropertyValue("CauHinhBaoHiem", ref _CauHinhBaoHiem, value);
            }
        }

        private string ExpressionType
        {
            get
            {
                return "PSC_HRM.Module.ThuNhap.Luong.LuongNhanVien";
            }
        }

        [ModelDefault("Caption", "Dừng tất cả các job")]
        public bool JobStop
        {
            get
            {
                return _jobStop;
            }
            set
            {
                SetPropertyValue("JobStop", ref _jobStop, value);
            }
        }
        [ModelDefault("Caption", "Số giờ chuẩn")]
        [RuleRange("CHC_SoGioChuan", DefaultContexts.Save, 0.00, 10000, "Số giờ chuẩn > 0")]
        [VisibleInListView(false)]
        public int SoGioChuan
        {
            get { return _SoGioChuan; }
            set { SetPropertyValue("SoGioChuan", ref _SoGioChuan, value); }
        }

        [ModelDefault("Caption", "Số tháng trong năm")]
        [RuleRange("CHC_SoThangChuan", DefaultContexts.Save, 0.00, 10000, "Số tháng > 0")]
        [VisibleInListView(false)]
        public int SoThangTrongNam
        {
            get { return _SoThangTrongNam; }
            set { SetPropertyValue("SoThangTrongNam", ref _SoThangTrongNam, value); }
        }
        [ModelDefault("Caption", "Số giờ chuẩn (NCKH)")]
        [RuleRange("CHC_SoGioChuan_NCHK", DefaultContexts.Save, 0.00, 10000, "Số giờ chuẩn > 0")]
        [VisibleInListView(false)]
        public int SoGioChuan_NCHK
        {
            get { return _SoGioChuan_NCHK; }
            set { SetPropertyValue("SoGioChuan_NCHK", ref _SoGioChuan_NCHK, value); }
        }
        [ModelDefault("Caption", "Số giờ chuẩn(Khác)")]
        [RuleRange("CHC_SoGioChuan_Khac", DefaultContexts.Save, 0.00, 10000, "Số giờ chuẩn > 0")]
        [VisibleInListView(false)]
        public int SoGioChuan_Khac
        {
            get { return _SoGioChuan_Khac; }
            set { SetPropertyValue("SoGioChuan_Khac", ref _SoGioChuan_Khac, value); }
        }

        [ModelDefault("Caption", "Số giờ chuẩn cố vấn học tập")]
        [RuleRange("CHC_SoGioChuan_CVHT", DefaultContexts.Save, 0.00, 10000, "Số giờ chuẩn > 0")]
        [VisibleInListView(false)]
        public int SoGioChuan_CoVanHocTap
        {
            get { return _SoGioChuan_CoVanHocTap; }
            set { SetPropertyValue("SoGioChuan_CoVanHocTap", ref _SoGioChuan_CoVanHocTap, value); }
        }

        [ModelDefault("Caption", "Số giờ chuẩn kiêm giảng")]
        [RuleRange("CHC_SoGioChuan_KG", DefaultContexts.Save, 0.00, 10000, "Số giờ chuẩn > 0")]
        [VisibleInListView(false)]
        public int SoGioChuan_KiemGiang
        {
            get { return _SoGioChuan_KiemGiang; }
            set { SetPropertyValue("SoGioChuan_KiemGiang", ref _SoGioChuan_KiemGiang, value); }
        }

        [ModelDefault("Caption", "Đồng bộ tài khoản")]
        public bool DongBoTaiKhoan
        {
            get
            {
                return _DongBoTaiKhoan;
            }
            set
            {
                SetPropertyValue("DongBoTaiKhoan", ref _DongBoTaiKhoan, value);
            }
        }

        public CauHinhChung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            KhoiTaoCauHinh();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            //KhoiTaoCauHinh();
        }

        private void KhoiTaoCauHinh()
        {
            if(string.IsNullOrEmpty(NoiLuuTruGiayTo))
            NoiLuuTruGiayTo = "ftp://Ten FTP server/PSC_HRM_GiayToHoSo/";
            if (string.IsNullOrEmpty(Username))
            Username = "pschrm";
            if (QuocGia == null)
            QuocGia = Session.GetObjectByKey<QuocGia>(new Guid("5F08956D-2497-4800-8EBA-D36236ADEFF9"));
            //if (NamHoc == null)
            //NamHoc = Session.GetObjectByKey<NamHoc>(new Guid("386887F4-0E79-4C2C-BCE7-22D86F919D3A"));

            if (CauHinhNhacViec == null)
                CauHinhNhacViec = new CauHinhNhacViec(Session);
            if (CauHinhTuyenDung == null)
            CauHinhTuyenDung = new CauHinhTuyenDung(Session);
            if (CauHinhHoSo==null)
            CauHinhHoSo = new CauHinhHoSo(Session);
            if (CauHinhHopDong == null)
            CauHinhHopDong = new CauHinhHopDong(Session);
            if (CauHinhQuyetDinh == null)
            CauHinhQuyetDinh = new CauHinhQuyetDinh(Session);
            if (CauHinhEmail == null)
            CauHinhEmail = new CauHinhEmail(Session);
            if (CauHinhThongTinLuong == null)
            CauHinhThongTinLuong = new CauHinhThongTinLuong(Session);
            //
            if (CauHinhEmail == null)
            {
                CauHinhEmail = new CauHinhEmail(Session);
                CauHinhEmail.Server = "smtp.gmail.com";
                CauHinhEmail.Port = 587;
                CauHinhEmail.Email = "pscerp@gmail.com";
                CauHinhEmail.Pass = "pscvietnam";
            }
            if(CauHinhThongTinLuong==null)
            {
               CauHinhThongTinLuong = new CauHinhThongTinLuong(Session);
            }
            if (string.IsNullOrEmpty(CauHinhThongTinLuong.CongThucTinhHSPCVuotKhung))
            {
                CauHinhThongTinLuong.CongThucTinhHSPCVuotKhung = "([HeSoLuong] * [VuotKhung]) / 100";
            }
            if (string.IsNullOrEmpty(CauHinhThongTinLuong.CongThucTinhHSPCThamNien))
            {
                CauHinhThongTinLuong.CongThucTinhHSPCThamNien = "([HeSoLuong] + [HSPCChucVu] + [HSPCVuotKhung]) * [ThamNien] / 100";
            }
            if (string.IsNullOrEmpty(CauHinhThongTinLuong.CongThucTinhHSPCThamNienHanhChinh))
            {
                CauHinhThongTinLuong.CongThucTinhHSPCThamNienHanhChinh = "([HeSoLuong] + [HSPCChucVu] + [HSPCVuotKhung]) * [PhanTramThamNienHC] / 100";
            }
            if (string.IsNullOrEmpty(CauHinhThongTinLuong.CongThucTinhHSPCUuDai))
            {
                CauHinhThongTinLuong.CongThucTinhHSPCUuDai = "([HeSoLuong] + [HSPCChucVu] + [HSPCVuotKhung]) * [PhuCapUuDai] / 100";
            }
            if (string.IsNullOrEmpty(CauHinhThongTinLuong.CongThucTinhHSPCTrachNhiem))
            {
                CauHinhThongTinLuong.CongThucTinhHSPCTrachNhiem = "([HeSoLuong] + [HSPCChucVu] + [HSPCVuotKhung]) * [PhuCapTrachNhiem] / 100";
            }
            if (string.IsNullOrEmpty(CauHinhThongTinLuong.CongThucTinhHSPCDocHai))
            {
                CauHinhThongTinLuong.CongThucTinhHSPCDocHai = "[HSPCDocHai]";
            }
        }
    }

}
