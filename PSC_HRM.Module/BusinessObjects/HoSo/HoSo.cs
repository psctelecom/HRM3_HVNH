using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.TaoMaQuanLy;

namespace PSC_HRM.Module.HoSo
{
    [ImageName("BO_Resume")]
    [DefaultProperty("CapTion")]
    [ModelDefault("Caption", "Hồ sơ")]
    [Appearance("HoSo.TruongNuoc", TargetItems = "SoHoChieu;NgayCapHoChieu;NoiCapHoChieu;NgayHetHan", Visibility = ViewItemVisibility.Hide, Criteria = "QuocTich.TenQuocGia like '%Việt Nam%'")]
    [Appearance("HoSo.NgoaiNuoc", TargetItems = "CMND;NgayCap;NoiCap", Visibility = ViewItemVisibility.Hide, Criteria = "QuocTich.TenQuocGia not like '%Việt Nam%'")]
    [RuleCombinationOfPropertiesIsUnique("HoSo.Unique1", DefaultContexts.Save, "OidHoSoCha;CMND", "Nhân viên đã tồn tại trong hệ thống. Liên hệ quản trị hệ thống HRM")]
    [RuleCombinationOfPropertiesIsUnique("HoSo.Unique2", DefaultContexts.Save, "OidHoSoCha;SoHoChieu", "Nhân viên đã tồn tại trong hệ thống. Liên hệ quản trị hệ thống HRM")]
    [Appearance("KhacUEL", TargetItems = "MaQuanLy", Enabled = false, Criteria = "MaTruong != 'UEL' and MaTruong != 'HVNH'")]
    [Appearance("KhacNEU", TargetItems = "CaChamCong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'NEU'")]
    
    public class HoSo : BaseObject
    {
        private string _MaTruong;
        //[NonPersistent]
        //[Browsable(false)]
        //public string MaTruong
        //{
        //    get
        //    {
        //        return _MaTruong;
        //    }
        //    set
        //    {
        //        SetPropertyValue("MaTruong", ref _MaTruong, value);
        //    }
        //}

        [VisibleInListView(false)]
        public string CapTion
        {
            get
            {
                if (MaTruong == "QNU")
                    return MaQuanLy + " - " + HoTen;
                else
                    return HoTen;
            }
        }
        [NonPersistent]
        [Browsable(false)]
        public static HoSo CurrentHoSo { get; set; }


        private DateTime _NgayHetHan;
        private string _NoiCapHoChieu;
        private DateTime _NgayCapHoChieu;
        private string _SoHoChieu;
        private string _MaQuanLy;
        private string _MaUIS;
        private QuocGia _QuocTich;
        private string _Ho;
        private string _Ten;
        private string _TenGoiKhac;
        private GioiTinhEnum _GioiTinh;
        private DateTime _NgaySinh;
        private DiaChi _NoiSinh;
        private string _CMND;
        private DateTime _NgayCap;
        private TinhThanh _NoiCap;
        private DiaChi _QueQuan;
        private DiaChi _DiaChiThuongTru;
        private DiaChi _NoiOHienNay;
        private string _Email;
        private string _DienThoaiDiDong;
        private string _DienThoaiNhaRieng;
        private TinhTrangHonNhan _TinhTrangHonNhan;
        private DanToc _DanToc;
        private TonGiao _TonGiao;
        private HinhThucTuyenDungEnum _HinhThucTuyenDung;
        private LoaiHoSoEnum _LoaiHoSo;
        private string _GhiChu;
        private int _IDNhanSu_ChamCong;
        private CC_CaChamCong _CaChamCong;// thêm vào 23072020

        //Khóa ngoại để biết hồ sơ được copy từ hồ sơ cha nào
        private Guid _OidHoSoCha;
        private bool _DaLuu;

        //Chỉ dành cho hồ sơ ứng viên và giảng viên thỉnh giảng
        //Đối với cán bộ trong trường thì sử dụng số hiệu công chức hoặc số hồ sơ
        //[ModelDefault("AllowEdit", "False")] //Không sử dụng rule cho UEL
        [ModelDefault("Caption", "Mã quản lý")]
        //[RuleUniqueValue("", DefaultContexts.Save, TargetCriteria = "MaTruong != 'UEL'")]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }
        [ModelDefault("Caption", "Mã UIS")]
        [Browsable(false)]
        public string MaUIS
        {
            get
            {
                return _MaUIS;
            }
            set
            {
                SetPropertyValue("MaUIS", ref _MaUIS, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Họ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Ho
        {
            get
            {
                return _Ho;
            }
            set
            {
                SetPropertyValue("Ho", ref _Ho, value);
                OnChanged("HoTen");
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Ten
        {
            get
            {
                return _Ten;
            }
            set
            {
                SetPropertyValue("Ten", ref _Ten, value);
                OnChanged("HoTen");
            }
        }

        [Persistent]
        [ModelDefault("Caption", "Họ và tên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string HoTen
        {
            get
            {
                return String.Format("{0} {1}", Ho, Ten);
            }
        }

        [ModelDefault("Caption", "Tên gọi khác")]
        public string TenGoiKhac
        {
            get
            {
                return _TenGoiKhac;
            }
            set
            {
                SetPropertyValue("TenGoiKhac", ref _TenGoiKhac, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'HUFLIT'")]
        public DateTime NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Nơi sinh")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiSinh
        {
            get
            {
                return _NoiSinh;
            }
            set
            {
                SetPropertyValue("NoiSinh", ref _NoiSinh, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return _GioiTinh;
            }
            set
            {
                SetPropertyValue("GioiTinh", ref _GioiTinh, value);
                if (!IsLoading)
                    AfterGioiTinhChanged();
            }
        }

        [ModelDefault("Caption", "Số CMND")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "QuocTich.TenQuocGia like '%Việt Nam%' and MaTruong != 'DNU' and MaTruong != 'HUFLIT' and MaTruong != 'HVNH'", SkipNullOrEmptyValues = false)]
        public string CMND
        {
            get
            {
                return _CMND;
            }
            set
            {
                SetPropertyValue("CMND", ref _CMND, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap
        {
            get
            {
                return _NgayCap;
            }
            set
            {
                SetPropertyValue("NgayCap", ref _NgayCap, value);
            }
        }

        [ModelDefault("Caption", "Nơi cấp")]
        public TinhThanh NoiCap
        {
            get
            {
                return _NoiCap;
            }
            set
            {
                SetPropertyValue("NoiCap", ref _NoiCap, value);
            }
        }

        [ModelDefault("Caption", "Số hộ chiếu")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "QuocTich.TenQuocGia not like '%Việt Nam%' and MaTruong != 'HUFLIT'", SkipNullOrEmptyValues = false)]
        public string SoHoChieu
        {
            get
            {
                return _SoHoChieu;
            }
            set
            {
                SetPropertyValue("SoHoChieu", ref _SoHoChieu, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp hộ chiếu")]
        public DateTime NgayCapHoChieu
        {
            get
            {
                return _NgayCapHoChieu;
            }
            set
            {
                SetPropertyValue("NgayCapHoChieu", ref _NgayCapHoChieu, value);
            }
        }

        [ModelDefault("Caption", "Nơi cấp hộ chiếu")]
        public string NoiCapHoChieu
        {
            get
            {
                return _NoiCapHoChieu;
            }
            set
            {
                SetPropertyValue("NoiCapHoChieu", ref _NoiCapHoChieu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn")]
        public DateTime NgayHetHan
        {
            get
            {
                return _NgayHetHan;
            }
            set
            {
                SetPropertyValue("NgayHetHan", ref _NgayHetHan, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Quê quán")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi QueQuan
        {
            get
            {
                return _QueQuan;
            }
            set
            {
                SetPropertyValue("QueQuan", ref _QueQuan, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Địa chỉ thường trú")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi DiaChiThuongTru
        {
            get
            {
                return _DiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("DiaChiThuongTru", ref _DiaChiThuongTru, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Nơi ở hiện nay")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiOHienNay
        {
            get
            {
                return _NoiOHienNay;
            }
            set
            {
                SetPropertyValue("NoiOHienNay", ref _NoiOHienNay, value);
            }
        }

        [ModelDefault("Caption", "Email (*)")]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                SetPropertyValue("Email", ref _Email, value);
            }
        }

        [ModelDefault("Caption", "Điện thoại di động (*)")]
        public string DienThoaiDiDong
        {
            get
            {
                return _DienThoaiDiDong;
            }
            set
            {
                SetPropertyValue("DienThoaiDiDong", ref _DienThoaiDiDong, value);
            }
        }

        [ModelDefault("Caption", "Điện thoại nhà riêng (*)")]
        public string DienThoaiNhaRieng
        {
            get
            {
                return _DienThoaiNhaRieng;
            }
            set
            {
                SetPropertyValue("DienThoaiNhaRieng", ref _DienThoaiNhaRieng, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng hôn nhân")]
        public TinhTrangHonNhan TinhTrangHonNhan
        {
            get
            {
                return _TinhTrangHonNhan;
            }
            set
            {
                SetPropertyValue("TinhTrangHonNhan", ref _TinhTrangHonNhan, value);
            }
        }

        [ModelDefault("Caption", "Dân tộc")]
        public DanToc DanToc
        {
            get
            {
                return _DanToc;
            }
            set
            {
                SetPropertyValue("DanToc", ref _DanToc, value);
            }
        }

        [ModelDefault("Caption", "Tôn giáo")]
        public TonGiao TonGiao
        {
            get
            {
                return _TonGiao;
            }
            set
            {
                SetPropertyValue("TonGiao", ref _TonGiao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Quốc tịch")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuocGia QuocTich
        {
            get
            {
                return _QuocTich;
            }
            set
            {
                SetPropertyValue("QuocTich", ref _QuocTich, value);
            }
        }

        [ModelDefault("Caption", "Hình thức tuyển dụng")]
        public HinhThucTuyenDungEnum HinhThucTuyenDung
        {
            get
            {
                return _HinhThucTuyenDung;
            }
            set
            {
                SetPropertyValue("HinhThucTuyenDung", ref _HinhThucTuyenDung, value);
            }
        }

        [Size(300)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [ModelDefault("Caption", "Id máy chấm công")]
        public int IDNhanSu_ChamCong
        {
            get
            {
                return _IDNhanSu_ChamCong;
            }
            set
            {
                SetPropertyValue("IDNhanSu_ChamCong", ref _IDNhanSu_ChamCong, value);
                //if(!IsLoading)
                //    if(MaTruong=="PSC")
                //    {
                //        if (MaQuanLy != null)
                //            MaQuanLy = IDNhanSu_ChamCong.ToString();
                //    }
            }
        }

        [ModelDefault("Caption", "Ca chấm công")]
        public CC_CaChamCong CaChamCong
        {
            get
            {
                return _CaChamCong;
            }
            set
            {
                SetPropertyValue("CaChamCong", ref _CaChamCong, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Loại hồ sơ")]
        public LoaiHoSoEnum LoaiHoSo
        {
            get
            {
                return _LoaiHoSo;
            }
            set
            {
                SetPropertyValue("LoaiHoSo", ref _LoaiHoSo, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Oid Hồ sơ cha")]
        public Guid OidHoSoCha
        {
            get
            {
                return _OidHoSoCha;
            }
            set
            {
                SetPropertyValue("OidHoSoCha", ref _OidHoSoCha, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Đã lưu")]
        public bool DaLuu
        {
            get
            {
                return _DaLuu;
            }
            set
            {
                SetPropertyValue("DaLuu", ref _DaLuu, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Giấy tờ")]
        [Association("HoSo-ListGiayToHoSo")]
        public XPCollection<GiayToHoSo> ListGiayToHoSo
        {
            get
            {
                return GetCollection<GiayToHoSo>("ListGiayToHoSo");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách văn bằng")]
        [Association("HoSo-ListVanBang")]
        public XPCollection<VanBang> ListVanBang
        {
            get
            {
                return GetCollection<VanBang>("ListVanBang");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách chứng chỉ")]
        [Association("HoSo-ListChungChi")]
        public XPCollection<ChungChi> ListChungChi
        {
            get
            {
                return GetCollection<ChungChi>("ListChungChi");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách ngoại ngữ")]
        [Association("HoSo-ListNgoaiNgu")]
        public XPCollection<TrinhDoNgoaiNguKhac> ListNgoaiNgu
        {
            get
            {
                return GetCollection<TrinhDoNgoaiNguKhac>("ListNgoaiNgu");
            }
        }

        [NonPersistent]
        [Browsable(false)]
        //private string MaTruong { get; set; }
        public string MaTruong
        {
            get
            {
                return _MaTruong;
            }
            set
            {
                SetPropertyValue("MaTruong", ref _MaTruong, value);
            }
        }

        public HoSo(Session session) : base(session) { }

        void LoadIDChamCong()
        {
            if (TruongConfig.MaTruong == "PSC")
            {
                try
                {
                    string sql = "SELECT ISNULL((SELECT TOP 1 SSN"
                                + " FROM MITA.dbo.USERINFO"
                                + " ORDER BY CONVERT(INT,SSN) DESC)"
                                + " ,0)";
                    object kq = null;
                    kq = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                    if (kq != null)
                    {
                        IDNhanSu_ChamCong = Convert.ToInt32(kq) + 1;
                    }
                    else
                        IDNhanSu_ChamCong = 1;
                }
                catch (Exception)
                {
                }
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
            NoiSinh = new DiaChi(Session);
            QueQuan = new DiaChi(Session);
            DiaChiThuongTru = new DiaChi(Session);
            NoiOHienNay = new DiaChi(Session);
            GioiTinh = GioiTinhEnum.Nam;
            DanToc = Session.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc like ?", "%Kinh%"));
            TonGiao = Session.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao like ?", "Không"));
            if (MaTruong != "VHU")
            {
                if (HamDungChung.CauHinhChung != null && HamDungChung.CauHinhChung.QuocGia != null)
                    QuocTich = Session.GetObjectByKey<QuocGia>(HamDungChung.CauHinhChung.QuocGia.Oid);
                else
                    QuocTich = Session.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia like ?", "Việt Nam"));
            }
            HinhThucTuyenDung = HinhThucTuyenDungEnum.ThiTuyen;     
            //LoadIDChamCong();

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            MaTruong = TruongConfig.MaTruong;
        }

        public void HoSoOnLoaded()
        {
            MaTruong = TruongConfig.MaTruong;
        }

        protected virtual void AfterGioiTinhChanged()
        { }

        protected virtual void AfterBoPhanChanged()
        { }


        private string GetMaQuanLyNhanVien()
        {
            string maQuanLy = string.Empty;

            if (string.IsNullOrWhiteSpace(MaQuanLy))
                maQuanLy = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.MaNhanVien);

            return maQuanLy;
        }


        protected override void OnSaving()
        {
            base.OnSaving();
            if(TruongConfig.MaTruong == "VHU" && DaLuu == false)
            {
                MaQuanLy = GetMaQuanLyNhanVien();
                DaLuu = true;
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if (TruongConfig.MaTruong == "DNU" || TruongConfig.MaTruong == "HUFLIT" || TruongConfig.MaTruong == "VHU" || TruongConfig.MaTruong == "HVNH")
            {
                SqlParameter[] param = new SqlParameter[1]; /*Số parameter trên Store Procedure*/
                param[0] = new SqlParameter("@Oid_NhanVien", this.Oid);
                DataProvider.ExecuteNonQuery("spd_DNU_DongBoDuLieu_NhanVien_UIS", System.Data.CommandType.StoredProcedure, param);
            }
        }
    }
}
