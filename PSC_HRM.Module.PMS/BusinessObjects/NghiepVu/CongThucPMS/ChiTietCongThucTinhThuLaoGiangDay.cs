using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.Enum;

namespace PSC_HRM.Module.PMS.NghiepVu
{
    [ImageName("BO_Expression")]
    [DefaultProperty("DienGiai")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Chi tiết công thức")]
    [Appearance("Khoa_CongThucTinhTNCT", TargetItems = "CongThucTinhTNCT", Enabled = false, Criteria = "!TinhTNCT")]
    [Appearance("Hide_CongThucTinhSoTienTamUng", TargetItems = "CongThucTinhSoTienTamUng", Visibility = ViewItemVisibility.Hide, Criteria = "DKAn <> 'HVNH' AND DKAn <> 'DNU'")]
    [Appearance("ToMau", TargetItems = "CongThucTinhTNCT", FontColor = "Red")]
    public class ChiTietCongThucTinhThuLaoGiangDay : TruongBaseObject
    {
        private bool _NgungSuDung;
        private CongThucTinhThuLaoGiangDay _CongThucTinhThuLaoGiangDay;
        private CongTruPMSEnum _CongTru;
        private string _MaChiTiet;
        private string _DienGiai;
        private string _CongThucTinhSoTien;
        private string _CongThucTinhSoTienTamUng;
        private bool _TinhTNCT;
        private string _CongThucTinhTNCT;
        private string _CongThucTinhBangChu;
        private string _DKAn;
        private string _CongThucTinhHSThamNienNghe;
        private string _CongThucTinhPTHSPhuCap;
        private string _CongThucTinhTongLuong;
        private string _CongThucTinhSoTienMotGio;
        private string _CongThucTinhSoTienHopDong;

        [NonPersistent]
        [Browsable(false)]
        public string DKAn
        {
            get
            {
                return _DKAn;
            }
            set
            {
                SetPropertyValue("MaTruong", ref _DKAn, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Công thức")]
        [Association("CongThucTinhThuLaoGiangDay-ListChiTietCongThuc")]
        public CongThucTinhThuLaoGiangDay CongThucTinhThuLaoGiangDay
        {
            get
            {
                return _CongThucTinhThuLaoGiangDay;
            }
            set
            {
                SetPropertyValue("CongThucTinhThuLaoGiangDay", ref _CongThucTinhThuLaoGiangDay, value);
            }
        }

        [ModelDefault("Caption", "Mã chi tiết")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [VisibleInListView(false)]
        public string MaChiTiet
        {
            get
            {
                return _MaChiTiet;
            }
            set
            {
                SetPropertyValue("MaChiTiet", ref _MaChiTiet, value);
            }
        }

        [ModelDefault("Caption", "Diễn giải")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string DienGiai
        {
            get
            {
                return _DienGiai;
            }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }

        [ModelDefault("Caption", "Ngừng sử dụng")]
        [VisibleInListView(true)]
        public bool NgungSuDung
        {
            get
            {
                return _NgungSuDung;
            }
            set
            {
                SetPropertyValue("NgungSuDung", ref _NgungSuDung, value);
            }
        }

        private string ExpressionType
        {
            get
            {
                return "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS";
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính tiền")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CongThucTinhThuLaoEditor")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public string CongThucTinhSoTien
        {
            get
            {
                return _CongThucTinhSoTien;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTien", ref _CongThucTinhSoTien, value);
                if (!IsLoading)
                    if (!TinhTNCT && DKAn != "DNU")
                        CongThucTinhTNCT = CongThucTinhSoTien;
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính tiền tạm ứng")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CongThucTinhThuLaoEditor")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public string CongThucTinhSoTienTamUng
        {
            get
            {
                return _CongThucTinhSoTienTamUng;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTienTamUng", ref _CongThucTinhSoTienTamUng, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính HS thâm niên nghề")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CongThucTinhThuLaoEditor")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public string CongThucTinhHSThamNienNghe
        {
            get
            {
                return _CongThucTinhHSThamNienNghe;
            }
            set
            {
                SetPropertyValue("CongThucTinhHSThamNienNghe", ref _CongThucTinhHSThamNienNghe, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính % HS phụ cấp")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CongThucTinhThuLaoEditor")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public string CongThucTinhPTHSPhuCap
        {
            get
            {
                return _CongThucTinhPTHSPhuCap;
            }
            set
            {
                SetPropertyValue("CongThucTinhPTHSPhuCap", ref _CongThucTinhPTHSPhuCap, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính tổng lương")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CongThucTinhThuLaoEditor")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public string CongThucTinhTongLuong
        {
            get
            {
                return _CongThucTinhTongLuong;
            }
            set
            {
                SetPropertyValue("CongThucTinhTongLuong", ref _CongThucTinhTongLuong, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính số tiền một giờ")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CongThucTinhThuLaoEditor")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public string CongThucTinhSoTienMotGio
        {
            get
            {
                return _CongThucTinhSoTienMotGio;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTienMotGio", ref _CongThucTinhSoTienMotGio, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính số tiền hợp đồng")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CongThucTinhThuLaoEditor")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public string CongThucTinhSoTienHopDong
        {
            get
            {
                return _CongThucTinhSoTienHopDong;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTienHopDong", ref _CongThucTinhSoTienHopDong, value);
            }
        }
        
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Tính TNCT khác")]
        [VisibleInListView(true)]
        public bool TinhTNCT
        {
            get
            {
                return _TinhTNCT;
            }
            set
            {
                SetPropertyValue("TinhTNCT", ref _TinhTNCT, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính TNCT")]
        //[RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "!TinhTNCT")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CongThucTinhThuLaoEditor")]
        [VisibleInListView(false)]
        public string CongThucTinhTNCT
        {
            get
            {
                return _CongThucTinhTNCT;
            }
            set
            {
                SetPropertyValue("CongThucTinhTNCT", ref _CongThucTinhTNCT, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính bằng chữ")]
        [VisibleInListView(false)]
        public string CongThucTinhBangChu
        {
            get
            {
                return _CongThucTinhBangChu;
            }
            set
            {
                SetPropertyValue("CongThucTinhBangChu", ref _CongThucTinhBangChu, value);
            }
        }

        [ModelDefault("Caption", "Cộng/Trừ")]
        [VisibleInListView(true)]
        public CongTruPMSEnum CongTru
        {
            get
            {
                return _CongTru;
            }
            set
            {
                SetPropertyValue("CongTru", ref _CongTru, value);
            }
        }


        public ChiTietCongThucTinhThuLaoGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TinhTNCT = false;
            CongTru = CongTruPMSEnum.Cong;
            CongThucTinhBangChu = "";
            DKAn = TruongConfig.MaTruong;
        }
        protected override void OnSaving()
        {
            base.OnSaving();
            if (!TinhTNCT)
                CongThucTinhTNCT = CongThucTinhSoTien;
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            DKAn = TruongConfig.MaTruong;
        }
    }
}
