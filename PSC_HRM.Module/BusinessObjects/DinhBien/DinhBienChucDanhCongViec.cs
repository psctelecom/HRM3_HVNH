using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base.General;

namespace PSC_HRM.Module.DinhBien
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Định biên chức danh công việc")]
    [RuleCombinationOfPropertiesIsUnique("DinhBienChucVu.Unique", DefaultContexts.Save, "BoPhan;ChucDanhCongViec")]
    public class DinhBienChucDanhCongViec : BaseObject
    {
        private BoPhan _BoPhan;
        private CongViec _ChucDanhCongViec;
        private int _SoLuong;
        //
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private TrinhDoNgoaiNgu _TrinhDoNgoaiNgu;
        private TrinhDoTinHoc _TrinhDoTinHoc;
        private HocHam _HocHam;
        private LyLuanChinhTri _LyLuanChinhTri;
        private LoaiNhanSu _LoaiNhanSu;
        private DanToc _DanToc;
        private TonGiao _TonGiao;
        private decimal _SoNamKinhNghiem;
        //
        private bool _DangVien;
        private bool _DoanVien;
        private bool _BienChe;
        //
        private bool _ShowTrinhDoNgoaiNgu;
        private bool _ShowTrinhDoChuyenMon;
        private bool _ShowTrinhDoTinHoc;
        private bool _ShowHocHam;
        private bool _ShowLyLuanChinhTri;
        private bool _ShowLoaiNhanSu;
        private bool _ShowDanToc;
        private bool _ShowTonGiao;
        private bool _ShowDangVien;
        private bool _ShowDoanVien;
        private bool _ShowBienChe;
        private bool _ShowSoNamKinhNghiem;


        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Bộ phận")]
        [ModelDefault("AllowEdit","False")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Chức danh công việc")]
        [ImmediatePostData]
        public CongViec ChucDanhCongViec
        {
            get
            {
                return _ChucDanhCongViec;
            }
            set
            {
                SetPropertyValue("ChucDanhCongViec", ref _ChucDanhCongViec, value);
                if (!IsLoading)
                {
                    TrinhDoChuyenMon = null;
                    TrinhDoNgoaiNgu = null;
                    TrinhDoTinHoc = null;
                    HocHam = null;
                    LyLuanChinhTri = null;
                    LoaiNhanSu = null;
                    DanToc = null;
                    TonGiao = null;
                    DangVien = false;
                    DoanVien = false;
                    BienChe = false;
                    //
                    ShowTrinhDoChuyenMon = ShowPropertyName("TrinhDoChuyenMon");
                    ShowTrinhDoNgoaiNgu = ShowPropertyName("TrinhDoNgoaiNgu");
                    ShowTrinhDoTinHoc = ShowPropertyName("TrinhDoTinHoc");
                    ShowHocHam = ShowPropertyName("HocHam");
                    ShowLyLuanChinhTri = ShowPropertyName("LyLuanChinhTri");
                    ShowLoaiNhanSu = ShowPropertyName("LoaiBienChe");
                    ShowDanToc = ShowPropertyName("DanToc");
                    ShowTonGiao = ShowPropertyName("TonGiao");
                    ShowDangVien =  ShowPropertyName("DangVien");
                    ShowDoanVien =  ShowPropertyName("DoanVien");
                    ShowBienChe = ShowPropertyName("BienChe");
                    ShowSoNamKinhNghiem = ShowPropertyName("SoNamKinhNghiem");
                }
            }
        }

        [ModelDefault("Caption", "Số lượng")]
        [RuleRange(0, 100)]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int SoLuong
        {
            get
            {
                return _SoLuong;
            }
            set
            {
                SetPropertyValue("SoLuong", ref _SoLuong, value);
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "ShowTrinhDoChuyenMon")]
        [ImmediatePostData]
        [Appearance("TrinhDoChuyenMon", TargetItems = "TrinhDoChuyenMon", Visibility = ViewItemVisibility.Hide, Criteria = "!ShowTrinhDoChuyenMon")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Trình độ ngoại ngữ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "ShowTrinhDoNgoaiNgu")]
        [ImmediatePostData]
        [Appearance("TrinhDoNgoaiNgu", TargetItems = "TrinhDoNgoaiNgu", Visibility = ViewItemVisibility.Hide, Criteria = "!ShowTrinhDoNgoaiNgu")]
        public TrinhDoNgoaiNgu TrinhDoNgoaiNgu
        {
            get
            {
                return _TrinhDoNgoaiNgu;
            }
            set
            {
                SetPropertyValue("TrinhDoNgoaiNgu", ref _TrinhDoNgoaiNgu, value);
            }
        }

        [ModelDefault("Caption", "Trình độ tin học")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "ShowTrinhDoTinHoc")]
        [ImmediatePostData]
        [Appearance("TrinhDoTinHoc", TargetItems = "TrinhDoTinHoc", Visibility = ViewItemVisibility.Hide, Criteria = "!ShowTrinhDoTinHoc")]
        public TrinhDoTinHoc TrinhDoTinHoc
        {
            get
            {
                return _TrinhDoTinHoc;
            }
            set
            {
                SetPropertyValue("TrinhDoTinHoc", ref _TrinhDoTinHoc, value);
            }
        }
        [ModelDefault("Caption", "Học hàm")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "ShowHocHam")]
        [ImmediatePostData]
        [Appearance("HocHam", TargetItems = "HocHam", Visibility = ViewItemVisibility.Hide, Criteria = "!ShowHocHam")]
        public HocHam HocHam
        {
            get
            {
                return _HocHam;
            }
            set
            {
                SetPropertyValue("HocHam", ref _HocHam, value);
            }
        }
        [ModelDefault("Caption", "Lý luận chính trị")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "ShowLyLuanChinhTri")]
        [ImmediatePostData]
        [Appearance("LyLuanChinhTri", TargetItems = "LyLuanChinhTri", Visibility = ViewItemVisibility.Hide, Criteria = "!ShowLyLuanChinhTri")]
        public LyLuanChinhTri LyLuanChinhTri
        {
            get
            {
                return _LyLuanChinhTri;
            }
            set
            {
                SetPropertyValue("LyLuanChinhTri", ref _LyLuanChinhTri, value);
            }
        }
        [ModelDefault("Caption", "Loại nhân sự")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "ShowLoaiNhanSu")]
        [ImmediatePostData]
        [Appearance("LoaiNhanSu", TargetItems = "LoaiNhanSu", Visibility = ViewItemVisibility.Hide, Criteria = "!ShowLoaiNhanSu")]
        public LoaiNhanSu LoaiNhanSu
        {
            get
            {
                return _LoaiNhanSu;
            }
            set
            {
                SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value);
            }
        }
       
        [ModelDefault("Caption", "Dân tộc")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "ShowDanToc")]
        [ImmediatePostData]
        [Appearance("DanToc", TargetItems = "DanToc", Visibility = ViewItemVisibility.Hide, Criteria = "!ShowDanToc")]
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
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "ShowTonGiao")]
        [ImmediatePostData]
        [Appearance("TonGiao", TargetItems = "TonGiao", Visibility = ViewItemVisibility.Hide, Criteria = "!ShowTonGiao")]
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
        [ModelDefault("Caption", "Số năm kinh nghiệm")]
        [ImmediatePostData]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [Appearance("SoNamKinhNghiem", TargetItems = "SoNamKinhNghiem", Visibility = ViewItemVisibility.Hide, Criteria = "!ShowSoNamKinhNghiem")]
        public decimal SoNamKinhNghiem
        {
            get
            {
                return _SoNamKinhNghiem;
            }
            set
            {
                SetPropertyValue("SoNamKinhNghiem", ref _SoNamKinhNghiem, value);
            }
        }
        [ModelDefault("Caption", "Đảng viên")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "ShowDangVien")]
        [ImmediatePostData]
        [Appearance("DangVien", TargetItems = "DangVien", Visibility = ViewItemVisibility.Hide, Criteria = "!ShowDangVien")]
        public bool DangVien
        {
            get
            {
                return _DangVien;
            }
            set
            {
                SetPropertyValue("DangVien", ref _DangVien, value);
            }
        }
        [ModelDefault("Caption", "Đoàn viên")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "ShowDoanVien")]
        [ImmediatePostData]
        [Appearance("DoanVien", TargetItems = "DoanVien", Visibility = ViewItemVisibility.Hide, Criteria = "!ShowDoanVien")]
        public bool DoanVien
        {
            get
            {
                return _DoanVien;
            }
            set
            {
                SetPropertyValue("DoanVien", ref _DoanVien, value);
            }
        }
        [ModelDefault("Caption", "Biên chế")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "ShowBienChe")]
        [ImmediatePostData]
        [Appearance("BienChe", TargetItems = "BienChe", Visibility = ViewItemVisibility.Hide, Criteria = "!ShowBienChe")]
        public bool BienChe
        {
            get
            {
                return _BienChe;
            }
            set
            {
                SetPropertyValue("BienChe", ref _BienChe, value);
            }
        }


        [NonPersistent]
        [Browsable(false)]
        public bool ShowDangVien
        {
            get
            {
                return _ShowDangVien;
            }
            set
            {
                SetPropertyValue("ShowDangVien", ref _ShowDangVien, value);
            }
        }
        [NonPersistent]
        [Browsable(false)]
        public bool ShowDoanVien
        {
            get
            {
                return _ShowDoanVien;
            }
            set
            {
                SetPropertyValue("ShowDoanVien", ref _ShowDoanVien, value);
            }
        }
        [NonPersistent]
        [Browsable(false)]
        public bool ShowBienChe
        {
            get
            {
                return _ShowBienChe;
            }
            set
            {
                SetPropertyValue("ShowBienChe", ref _ShowBienChe, value);
            }
        }
        [NonPersistent]
        [Browsable(false)]
        public bool ShowSoNamKinhNghiem
        {
            get
            {
                return _ShowSoNamKinhNghiem;
            }
            set
            {
                SetPropertyValue("ShowSoNamKinhNghiem", ref _ShowSoNamKinhNghiem, value);
            }
        }
        [NonPersistent]
        [Browsable(false)]
        public bool ShowTonGiao
        {
            get
            {
                return _ShowTonGiao;
            }
            set
            {
                SetPropertyValue("ShowTonGiao", ref _ShowTonGiao, value);
            }
        }
        [NonPersistent]
        [Browsable(false)]
        public bool ShowDanToc
        {
            get
            {
                return _ShowDanToc;
            }
            set
            {
                SetPropertyValue("ShowDanToc", ref _ShowDanToc, value);
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public bool ShowLoaiNhanSu
        {
            get
            {
                return _ShowLoaiNhanSu;
            }
            set
            {
                SetPropertyValue("ShowLoaiNhanSu", ref _ShowLoaiNhanSu, value);
            }
        }
        [NonPersistent]
        [Browsable(false)]
        public bool ShowLyLuanChinhTri
        {
            get
            {
                return _ShowLyLuanChinhTri;
            }
            set
            {
                SetPropertyValue("ShowLyLuanChinhTri", ref _ShowLyLuanChinhTri, value);
            }
        }
        [NonPersistent]
        [Browsable(false)]
        public bool ShowTrinhDoTinHoc
        {
            get
            {
                return _ShowTrinhDoTinHoc;
            }
            set
            {
                SetPropertyValue("ShowTrinhDoTinHoc", ref _ShowTrinhDoTinHoc, value);
            }
        }
        [NonPersistent]
        [Browsable(false)]
        public bool ShowHocHam
        {
            get
            {
                return _ShowHocHam;
            }
            set
            {
                SetPropertyValue("ShowHocHam", ref _ShowHocHam, value);
            }
        }
        [NonPersistent]
        [Browsable(false)]
        public bool ShowTrinhDoChuyenMon
        {
            get
            {
                return _ShowTrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("ShowTrinhDoChuyenMon", ref _ShowTrinhDoChuyenMon, value);
            }
        }
        //
        [NonPersistent]
        [Browsable(false)]
        [ImmediatePostData]
        public bool ShowTrinhDoNgoaiNgu
        {
            get
            {
                return _ShowTrinhDoNgoaiNgu;
            }
            set
            {
                SetPropertyValue("ShowTrinhDoNgoaiNgu", ref _ShowTrinhDoNgoaiNgu, value);
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            if (ChucDanhCongViec!= null)
            {
                ShowTrinhDoChuyenMon = ShowPropertyName("TrinhDoChuyenMon");
                ShowTrinhDoNgoaiNgu = ShowPropertyName("TrinhDoNgoaiNgu");
                ShowTrinhDoTinHoc = ShowPropertyName("TrinhDoTinHoc");
                ShowHocHam = ShowPropertyName("HocHam");
                ShowLyLuanChinhTri = ShowPropertyName("LyLuanChinhTri");
                ShowLoaiNhanSu = ShowPropertyName("LoaiBienChe");
                ShowDanToc = ShowPropertyName("DanToc");
                ShowTonGiao = ShowPropertyName("TonGiao");
                ShowDangVien = ShowPropertyName("DangVien");
                ShowDoanVien = ShowPropertyName("DoanVien");
                ShowBienChe = ShowPropertyName("BienChe");
                ShowSoNamKinhNghiem = ShowPropertyName("SoNamKinhNghiem");
            }
        }
        private bool ShowPropertyName(string name)
        { 
           //
            if (ChucDanhCongViec != null && ChucDanhCongViec.DieuKienDinhBien.Contains(name))
           {
               return  true;
           }
           return false;
        }
        public DinhBienChucDanhCongViec(Session session) : base(session) { }
    }

}
