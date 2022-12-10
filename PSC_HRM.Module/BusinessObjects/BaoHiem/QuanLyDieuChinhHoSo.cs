using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.BaoHiem
{
    [DefaultClassOptions]
    [DefaultProperty("Caption")]
    [ImageName("BO_DieuChinhHoSo")]
    [ModelDefault("Caption", "Quản lý điều chỉnh hồ sơ")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;ThoiGian;Dot")]
    public class QuanLyDieuChinhHoSo : BaoMatBaseObject
    {
        // Fields...
        private int _Dot = 1;
        private DateTime _ThoiGian = DateTime.Today;

        [ModelDefault("Caption", "Thời gian")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime ThoiGian
        {
            get
            {
                return _ThoiGian;
            }
            set
            {
                SetPropertyValue("ThoiGian", ref _ThoiGian, value);
            }
        }

        [ModelDefault("Caption", "Đợt")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Dot
        {
            get
            {
                return _Dot;
            }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
            }
        }

        [Browsable(false)]
        public string Caption
        {
            get
            {
                if (ThoiGian != DateTime.MinValue && Dot > 0)
                    return ObjectFormatter.Format("Tháng {ThoiGian:MM/yyyy} (Đợt {Dot})", this);
                return "";
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách điều chỉnh")]
        [Association("QuanLyDieuChinhHoSo-ListDieuChinhHoSo")]
        public XPCollection<DieuChinhHoSo> ListDieuChinhHoSo
        {
            get
            {
                return GetCollection<DieuChinhHoSo>("ListDieuChinhHoSo");
            }
        }

        public QuanLyDieuChinhHoSo(Session session) : base(session) { }
    }

}
