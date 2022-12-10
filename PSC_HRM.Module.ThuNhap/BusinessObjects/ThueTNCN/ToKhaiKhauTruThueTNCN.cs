using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [DefaultClassOptions]
    [ImageName("BO_HoaDon")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "02KK-TNCN")]
    [Appearance("ToKhaiKhauTruThue.Thang", TargetItems = "Quy", Enabled = false,
        Criteria = "PhanLoai=0")]
    [Appearance("ToKhaiKhauTruThue.Quy", TargetItems = "Thang", Enabled = false,
        Criteria = "PhanLoai=1")]
    [Appearance("ToKhaiKhauTruThue.BoSung", TargetItems = "LanBoSung", Enabled = false,
        Criteria = "!BoSung")]
    public class ToKhaiKhauTruThueTNCN : BaseObject, IThongTinTruong
    {
        private DateTime _NgayLap;
        private KhauTruThueEnum _PhanLoai;
        private int _Thang = 1;
        private QuyEnum _Quy;
        private int _Nam;
        private bool _BoSung;
        private int _LanBoSung;
        private ThongTinTruong _ThongTinTruong;
        private int _CaNhanCuTru;
        private int _CaNhanKhongCuTru;
        private decimal _TNCTCaNhanCuTruCoHopDong;
        private decimal _TNCTCaNhanCuTruKhongCoHopDong;
        private decimal _TNCTCaNhanKhongCuTru;
        private decimal _KTTCaNhanCuTruCoHopDong;
        private decimal _KTTCaNhanCuTruKhongCoHopDong;
        private decimal _KTTCaNhanKhongCuTru;
        private decimal _ThueTNCNCaNhanCuTruCoHopDong;
        private decimal _ThueTNCNCaNhanCuTruKhongCoHopDong;
        private decimal _ThueTNCNCaNhanKhongCuTru;

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
                if (!IsLoading && value != DateTime.MinValue)
                    Nam = value.Year;
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
                    if (PhanLoai == KhauTruThueEnum.TheoThang)
                        result = ObjectFormatter.Format("Tháng {Thang} năm {Nam:####} bổ sung lần {LanBoSung}", this);
                    else
                        result = ObjectFormatter.Format("{Quy} năm {Nam:####} bổ sung lần {LanBoSung}", this);
                }
                else
                {
                    if (PhanLoai == KhauTruThueEnum.TheoThang)
                        result = ObjectFormatter.Format("Tháng {Thang} năm {Nam:####}", this);
                    else
                        result = ObjectFormatter.Format("{Quy} năm {Nam:####}", this);
                }
                return result;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại")]
        public KhauTruThueEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
                if (!IsLoading && NgayLap != DateTime.MinValue)
                {
                    if (value == KhauTruThueEnum.TheoThang)
                        Thang = NgayLap.Month;
                    if (value == KhauTruThueEnum.TheoQuy)
                    {
                        if (NgayLap.Month <= 4)
                            Quy = QuyEnum.QuyI;
                        else if (NgayLap.Month <= 7)
                            Quy = QuyEnum.QuyII;
                        else if (NgayLap.Month <= 10)
                            Quy = QuyEnum.QuyIII;
                        else if (NgayLap.Month <= 12)
                            Quy = QuyEnum.QuyIV;
                        else
                            Quy = QuyEnum.QuyI;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Tháng")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria="PhanLoai=0")]
        [RuleRange("", DefaultContexts.Save, 1, 12, TargetCriteria="PhanLoai=0")]
        public int Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }

        [ModelDefault("Caption", "Quý")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria="PhanLoai=1")]
        public QuyEnum Quy
        {
            get
            {
                return _Quy;
            }
            set
            {
                SetPropertyValue("Quy", ref _Quy, value);
            }
        }

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
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
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria="BoSung")]
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

        [ModelDefault("Caption", "Đơn vị trả thu nhập")]
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
            }
        }

        //số lượng
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

        //số lượng
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

        [Persistent]
        [ModelDefault("Caption", "Tổng số cá nhân đã khấu trừ thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int TongCaNhanKhauTruThue
        {
            get
            {
                return CaNhanCuTru + CaNhanKhongCuTru;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cá nhân cư trú có hợp đồng lao động")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Cá nhân cư trú không hợp đồng lao động")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TNCTCaNhanCuTruKhongCoHopDong
        {
            get
            {
                return _TNCTCaNhanCuTruKhongCoHopDong;
            }
            set
            {
                SetPropertyValue("TNCTCaNhanCuTruKhongCoHopDong", ref _TNCTCaNhanCuTruKhongCoHopDong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cá nhân không cư trú")]
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

        [Persistent]
        [ModelDefault("Caption", "Tổng thu nhập chịu thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongTNCT
        {
            get
            {
                return TNCTCaNhanCuTruCoHopDong + TNCTCaNhanCuTruKhongCoHopDong + TNCTCaNhanKhongCuTru;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Khấu trừ Cá nhân cư trú có hợp đồng lao động")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Khấu trừ Cá nhân cư trú không hợp đồng lao động")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Khấu trừ Cá nhân không cư trú")]
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

        [Persistent]
        [ModelDefault("Caption", "Tổng khấu trừ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongKTT
        {
            get
            {
                return KTTCaNhanCuTruCoHopDong + KTTCaNhanCuTruKhongCoHopDong + KTTCaNhanKhongCuTru;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thuế TNCN Cá nhân cư trú có hợp đồng lao động")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNCaNhanCuTruCoHopDong
        {
            get
            {
                return _ThueTNCNCaNhanCuTruCoHopDong;
            }
            set
            {
                SetPropertyValue("ThueTNCNCaNhanCuTruCoHopDong", ref _ThueTNCNCaNhanCuTruCoHopDong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thuế TNCN Cá nhân cư trú không hợp đồng lao động")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNCaNhanCuTruKhongCoHopDong
        {
            get
            {
                return _ThueTNCNCaNhanCuTruKhongCoHopDong;
            }
            set
            {
                SetPropertyValue("ThueTNCNCaNhanCuTruKhongCoHopDong", ref _ThueTNCNCaNhanCuTruKhongCoHopDong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thuế TNCN Cá nhân không cư trú")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNCaNhanKhongCuTru
        {
            get
            {
                return _ThueTNCNCaNhanKhongCuTru;
            }
            set
            {
                SetPropertyValue("ThueTNCNCaNhanKhongCuTru", ref _ThueTNCNCaNhanKhongCuTru, value);
            }
        }

        [Persistent]
        [ModelDefault("Caption", "Tổng thuế TNCN")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongThueTNCN
        {
            get
            {
                return ThueTNCNCaNhanCuTruCoHopDong + ThueTNCNCaNhanCuTruKhongCoHopDong + ThueTNCNCaNhanKhongCuTru;
            }
        }

        public ToKhaiKhauTruThueTNCN(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLap = HamDungChung.GetServerTime();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }
    }

}
