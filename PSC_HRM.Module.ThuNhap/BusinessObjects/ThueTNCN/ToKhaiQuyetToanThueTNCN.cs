using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.CauHinh;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [DefaultClassOptions]
    [ImageName("BO_HoaDon")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "05KK-TNCN")]
    [Appearance("ToKhaiQuyetToanThueTNCN.BoSung", TargetItems = "LanBoSung", Enabled = false,
        Criteria = "!BoSung")]
    [RuleCombinationOfPropertiesIsUnique("ToKhaiQuyetToanThueTNCN.Unique", DefaultContexts.Save, "ThongTinTruong;KyTinhThue")]
    public class ToKhaiQuyetToanThueTNCN : BaseObject, IThongTinTruong
    {
        private MocTinhThueTNCN _MocTinhThueTNCN;
        private DateTime _NgayLap;
        private ThongTinTruong _ThongTinTruong;
        private int _KyTinhThue;
        private bool _BoSung;
        private int _LanBoSung;
        private int _CaNhanCuTru;
        private int _CaNhanKhongCuTru;
        private decimal _TNCTCaNhanCuTruCoHopDong;
        private decimal _TNCTCaNhanCuTruKhongHopDong;
        private decimal _TNCTCaNhanKhongCuTru;
        private decimal _KTTCaNhanCuTruCoHopDong;
        private decimal _KTTCaNhanCuTruKhongCoHopDong;
        private decimal _KTTCaNhanKhongCuTru;
        private decimal _ThueTNCNDaKT_CaNhanCuTruCoHopDong;
        private decimal _ThueTNCNDaKT_CaNhanCuTruKhongHopDong;
        private decimal _ThueTNCNDaKT_CaNhanKhongCuTru;
        private decimal _ThueTNCNPhaiKT_CaNhanCuTruCoHopDong;
        private decimal _ThueTNCNPhaiKT_CaNhanCuTruKhongHopDong;
        private decimal _ThueTNCNPhaiKT_CaNhanKhongCuTru;
        private decimal _TongSoThueTNCNDaNopNSNN;
        private decimal _TongSoThueTNCNConPhaiNopNSNN;
        private decimal _TongSoThueTNCNDaNopThua;
        private int _TongSoCaNhanUyQuyen;
        private decimal _QTT_TongSoThueTNCNDaKhauTru;
        private decimal _QTT_TongSoThueTNCNPhaiNop;
        private decimal _QTT_TongSoThueTNCNConPhaiNopNSNN;
        private decimal _QTT_TongSoThueTNCNNopThua;

        [ModelDefault("Caption", "Ngày lập")]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính thuế: Năm:")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int KyTinhThue
        {
            get
            {
                return _KyTinhThue;
            }
            set
            {
                SetPropertyValue("KyTinhThue", ref _KyTinhThue, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bổ sung")]
        public bool BoSung
        {
            get
            {
                return _BoSung;
            }
            set
            {
                SetPropertyValue("BoSung", ref _BoSung, value);
            }
        }

        [ModelDefault("Caption", "Lần bổ sung")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "BoSung")]
        [RuleValueComparison("", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0, TargetCriteria = "BoSung")]
        public int LanBoSung
        {
            get
            {
                return _LanBoSung;
            }
            set
            {
                SetPropertyValue("LanBoSung", ref _LanBoSung, value);
            }
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public string Caption
        {
            get
            {
                string result;
                if (BoSung)
                {
                    result = ObjectFormatter.Format("Năm {KyTinhThue:####} bổ sung lần {LanBoSung}", this);
                }
                else
                {
                    result = ObjectFormatter.Format("Năm {KyTinhThue:####}", this);
                }
                return result;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị nộp thuế")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
                if (!IsLoading && value != null && value.MocTinhThueTNCN != null)
                    MocTinhThueTNCN = HamDungChung.Copy<MocTinhThueTNCN>(Session, value.MocTinhThueTNCN);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Mốc tính thuế TNCN")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public MocTinhThueTNCN MocTinhThueTNCN
        {
            get
            {
                return _MocTinhThueTNCN;
            }
            set
            {
                SetPropertyValue("MocTinhThueTNCN", ref _MocTinhThueTNCN, value);
            }
        }

        //-------------------------------------------------------------------
        //Tổng số cá nhân đã khấu trừ thuế
        //-------------------------------------------------------------------
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Cá nhân cư trú")]
        public int CaNhanCuTru
        {
            get
            {
                return _CaNhanCuTru;
            }
            set
            {
                SetPropertyValue("CaNhanCuTru", ref _CaNhanCuTru, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Cá nhân không cư trú")]
        public int CaNhanKhongCuTru
        {
            get
            {
                return _CaNhanKhongCuTru;
            }
            set
            {
                SetPropertyValue("CaNhanKhongCuTru", ref _CaNhanKhongCuTru, value);
            }
        }
        //-------------------------------------------------------------------

        //-------------------------------------------------------------------
        //Tổng TNCT trả cho cá nhân
        //-------------------------------------------------------------------
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "TNCT Cá nhân cư trú có hợp đồng lao động")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TNCTCaNhanCuTruCoHopDong
        {
            get
            {
                return _TNCTCaNhanCuTruCoHopDong;
            }
            set
            {
                SetPropertyValue("TNCTCaNhanCuTruCoHopDong", ref _TNCTCaNhanCuTruCoHopDong, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "TNCT Cá nhân cư trú không có hợp đồng lao động")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TNCTCaNhanCuTruKhongHopDong
        {
            get
            {
                return _TNCTCaNhanCuTruKhongHopDong;
            }
            set
            {
                SetPropertyValue("TNCTCaNhanCuTruKhongHopDong", ref _TNCTCaNhanCuTruKhongHopDong, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "TNCT Cá nhân không cư trú")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TNCTCaNhanKhongCuTru
        {
            get
            {
                return _TNCTCaNhanKhongCuTru;
            }
            set
            {
                SetPropertyValue("TNCTCaNhanKhongCuTru", ref _TNCTCaNhanKhongCuTru, value);
            }
        }
        //-------------------------------------------------------------------


        //-------------------------------------------------------------------
        //Tổng TNCT trả cho cá nhận thuộc diện khấu trừ thuế
        //-------------------------------------------------------------------
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "KTT Cá nhân cư trú có hợp đồng lao động")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal KTTCaNhanCuTruCoHopDong
        {
            get
            {
                return _KTTCaNhanCuTruCoHopDong;
            }
            set
            {
                SetPropertyValue("KTTCaNhanCuTruCoHopDong", ref _KTTCaNhanCuTruCoHopDong, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "KTT Cá nhân cư trú không có hợp đồng lao động")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal KTTCaNhanCuTruKhongCoHopDong
        {
            get
            {
                return _KTTCaNhanCuTruKhongCoHopDong;
            }
            set
            {
                SetPropertyValue("KTTCaNhanCuTruKhongCoHopDong", ref _KTTCaNhanCuTruKhongCoHopDong, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "KTT Cá nhân không cư trú")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal KTTCaNhanKhongCuTru
        {
            get
            {
                return _KTTCaNhanKhongCuTru;
            }
            set
            {
                SetPropertyValue("KTTCaNhanKhongCuTru", ref _KTTCaNhanKhongCuTru, value);
            }
        }
        //-------------------------------------------------------------------

        //-------------------------------------------------------------------
        //Tổng số thuế TNCN đã khấu trừ
        //-------------------------------------------------------------------
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Thuế TNCN đã KT Cá nhân cư trú có hợp đồng lao động")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNDaKT_CaNhanCuTruCoHopDong
        {
            get
            {
                return _ThueTNCNDaKT_CaNhanCuTruCoHopDong;
            }
            set
            {
                SetPropertyValue("ThueTNCNDaKT_CaNhanCuTruCoHopDong", ref _ThueTNCNDaKT_CaNhanCuTruCoHopDong, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Thuế TNCN đã KT Cá nhân cư trú không có hợp đồng lao động")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNDaKT_CaNhanCuTruKhongHopDong
        {
            get
            {
                return _ThueTNCNDaKT_CaNhanCuTruKhongHopDong;
            }
            set
            {
                SetPropertyValue("ThueTNCNDaKT_CaNhanCuTruKhongHopDong", ref _ThueTNCNDaKT_CaNhanCuTruKhongHopDong, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Thuế TNCN đã KT Cá nhân không cư trú")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNDaKT_CaNhanKhongCuTru
        {
            get
            {
                return _ThueTNCNDaKT_CaNhanKhongCuTru;
            }
            set
            {
                SetPropertyValue("ThueTNCNDaKT_CaNhanKhongCuTru", ref _ThueTNCNDaKT_CaNhanKhongCuTru, value);
            }
        }
        //-------------------------------------------------------------------

        //-------------------------------------------------------------------
        //Tổng số thuế TNCN phải khấu trừ
        //-------------------------------------------------------------------
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Thuế TNCN phải KT Cá nhân cư trú có hợp đồng lao động")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNPhaiKT_CaNhanCuTruCoHopDong
        {
            get
            {
                return _ThueTNCNPhaiKT_CaNhanCuTruCoHopDong;
            }
            set
            {
                SetPropertyValue("ThueTNCNPhaiKT_CaNhanCuTruCoHopDong", ref _ThueTNCNPhaiKT_CaNhanCuTruCoHopDong, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Thuế TNCN phải KT Cá nhân cư trú không có hợp đồng lao động")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNPhaiKT_CaNhanCuTruKhongHopDong
        {
            get
            {
                return _ThueTNCNPhaiKT_CaNhanCuTruKhongHopDong;
            }
            set
            {
                SetPropertyValue("ThueTNCNPhaiKT_CaNhanCuTruKhongHopDong", ref _ThueTNCNPhaiKT_CaNhanCuTruKhongHopDong, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Thuế TNCN phải KT Cá nhân không cư trú")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNPhaiKT_CaNhanKhongCuTru
        {
            get
            {
                return _ThueTNCNPhaiKT_CaNhanKhongCuTru;
            }
            set
            {
                SetPropertyValue("ThueTNCNPhaiKT_CaNhanKhongCuTru", ref _ThueTNCNPhaiKT_CaNhanKhongCuTru, value);
            }
        }
        //-------------------------------------------------------------------

        //-------------------------------------------------------------------
        //quyết toán
        //-------------------------------------------------------------------
        [ModelDefault("Caption", "Tổng số thuế TNCN đã nộp NSNN")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongSoThueTNCNDaNopNSNN
        {
            get
            {
                return _TongSoThueTNCNDaNopNSNN;
            }
            set
            {
                SetPropertyValue("TongSoThueTNCNDaNopNSNN", ref _TongSoThueTNCNDaNopNSNN, value);                
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Tổng số thuế TNCN còn phải nộp NSNN")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongSoThueTNCNConPhaiNopNSNN
        {
            get
            {
                return _TongSoThueTNCNConPhaiNopNSNN;
            }
            set
            {
                SetPropertyValue("TongSoThueTNCNConPhaiNopNSNN", ref _TongSoThueTNCNConPhaiNopNSNN, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Tổng số thuế TNCN đã nộp thừa")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongSoThueTNCNDaNopThua
        {
            get
            {
                return _TongSoThueTNCNDaNopThua;
            }
            set
            {
                SetPropertyValue("TongSoThueTNCNDaNopThua", ref _TongSoThueTNCNDaNopThua, value);
            }
        }
        //-------------------------------------------------------------------

        //-------------------------------------------------------------------
        //quyết toán thay
        //-------------------------------------------------------------------
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Tổng số cá nhân ủy quyền")]
        public int TongSoCaNhanUyQuyen
        {
            get
            {
                return _TongSoCaNhanUyQuyen;
            }
            set
            {
                SetPropertyValue("TongSoCaNhanUyQuyen", ref _TongSoCaNhanUyQuyen, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "QTT Tổng số thuế TNCN đã khấu trừ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal QTT_TongSoThueTNCNDaKhauTru
        {
            get
            {
                return _QTT_TongSoThueTNCNDaKhauTru;
            }
            set
            {
                SetPropertyValue("QTT_TongSoThueTNCNDaKhauTru", ref _QTT_TongSoThueTNCNDaKhauTru, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "QTT Tổng số thuế TNCN phải nộp")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal QTT_TongSoThueTNCNPhaiNop
        {
            get
            {
                return _QTT_TongSoThueTNCNPhaiNop;
            }
            set
            {
                SetPropertyValue("QTT_TongSoThueTNCNPhaiNop", ref _QTT_TongSoThueTNCNPhaiNop, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "QTT Tổng số thuế TNCN còn phải nộp NSNN")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal QTT_TongSoThueTNCNConPhaiNopNSNN
        {
            get
            {
                return _QTT_TongSoThueTNCNConPhaiNopNSNN;
            }
            set
            {
                SetPropertyValue("QTT_TongSoThueTNCNConPhaiNopNSNN", ref _QTT_TongSoThueTNCNConPhaiNopNSNN, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "QTT Tổng số thuế TNCN đã nộp thừa")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal QTT_TongSoThueTNCNNopThua
        {
            get
            {
                return _QTT_TongSoThueTNCNNopThua;
            }
            set
            {
                SetPropertyValue("QTT_TongSoThueTNCNNopThua", ref _QTT_TongSoThueTNCNNopThua, value);
            }
        }
        //-------------------------------------------------------------------

        [Aggregated]
        [ModelDefault("Caption", "05A/BK-TNCN")]
        [Association("ToKhaiThueTNCN-BangKeThueTNCNNhanVien")]
        public XPCollection<BangKeThueTNCNNhanVien> BangKeThueTNCNNhanVienList
        {
            get
            {
                return GetCollection<BangKeThueTNCNNhanVien>("BangKeThueTNCNNhanVienList");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "05B/BK-TNCN")]
        [Association("ToKhaiThueTNCN-BangKeThueTNCNNgoai")]
        public XPCollection<BangKeThueTNCNNgoai> BangKeThueTNCNNgoaiList
        {
            get
            {
                return GetCollection<BangKeThueTNCNNgoai>("BangKeThueTNCNNgoaiList");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Điều chỉnh 05A/BK-TNCN")]
        [Association("ToKhaiQuyetToanThueTNCN-ListQuanLyDieuChinhThueTNCN")]
        public XPCollection<QuanLyDieuChinhThueTNCN> ListQuanLyDieuChinhThueTNCN
        {
            get
            {
                return GetCollection<QuanLyDieuChinhThueTNCN>("ListQuanLyDieuChinhThueTNCN");
            }
        }

        public ToKhaiQuyetToanThueTNCN(Session session) : base(session) { }
    }

}
