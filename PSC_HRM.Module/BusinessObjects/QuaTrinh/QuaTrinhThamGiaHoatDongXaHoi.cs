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
    [ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình tham gia hoạt động xã hội")]
    public class QuaTrinhThamGiaHoatDongXaHoi : BaseObject
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
                    object obj = Session.Evaluate<QuaTrinhThamGiaHoatDongXaHoi>(CriteriaOperator.Parse("COUNT()"), CriteriaOperator.Parse("HoSo=?", value));
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

        [ModelDefault("Caption", "Ngày bắt đầu")]
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

        [ModelDefault("Caption", "Ngày kết thúc")]
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

        public QuaTrinhThamGiaHoatDongXaHoi(Session session) : 
            base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Module.HoSo.HoSo.CurrentHoSo != null)
                HoSo = Session.GetObjectByKey<Module.HoSo.HoSo>(Module.HoSo.HoSo.CurrentHoSo.Oid);
        }
    }

}
