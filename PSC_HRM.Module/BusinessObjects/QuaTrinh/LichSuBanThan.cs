using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuaTrinh
{
    /// <summary>
    /// Hay còn gọi là lịch sử bản thân
    /// </summary>
    [ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Lịch sử bản thân")]
    public class LichSuBanThan : BaseObject
    {
        private int _STT;
        private HoSo.HoSo _HoSo;
        private string _TuNam;
        private string _DenNam;
        private string _NoiDung;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        public HoSo.HoSo HoSo 
        {
            get
            {
                return _HoSo;
            }
            set
            {
                SetPropertyValue("HoSo", ref _HoSo, value);
                if (!IsLoading && value != null)
                {
                    object obj = Session.Evaluate<LichSuBanThan>(CriteriaOperator.Parse("MAX(STT)"), CriteriaOperator.Parse("HoSo=?", value));
                    if (obj != null)
                        STT = (int)obj + 1;
                    else
                        STT = 1;
                }
            }
        }

        [ModelDefault("Caption", "Số thứ tự")]
        public int STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }
     
        [ModelDefault("Caption", "Từ ngày")]
        public string TuNam
        {
            get
            {
                return _TuNam;
            }
            set
            {
                SetPropertyValue("TuNam", ref _TuNam, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public string DenNam
        {
            get
            {
                return _DenNam;
            }
            set
            {
                SetPropertyValue("DenNam", ref _DenNam, value);
            }
        }

        [Size(8000)]
        [ModelDefault("Caption", "Nội dung")]
        public string NoiDung
        {
            get
            {
                return _NoiDung;
            }
            set
            {
                SetPropertyValue("NoiDung", ref _NoiDung, value);
            }
        }

        public LichSuBanThan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Module.HoSo.HoSo.CurrentHoSo != null)
                HoSo = Session.GetObjectByKey<HoSo.HoSo>(Module.HoSo.HoSo.CurrentHoSo.Oid);
        }
    }

}
