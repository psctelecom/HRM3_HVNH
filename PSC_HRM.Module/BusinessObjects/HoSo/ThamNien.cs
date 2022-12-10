using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.BusinessObjects.HoSo
{
    [DefaultProperty("TenThamNien")]
    [ModelDefault("Caption", "Thâm niên")]
    public class ThamNien : BaseObject
    {
        private string _TenThamNien;

        [ModelDefault("Caption", "Thâm niên")]
        public string TenThamNien
        {
            get
            {
                return String.Format(" {0} {1} {2} ", "Từ " + TuNam.ToString(), "năm đến", DenNam.ToString() + " năm");
            }
        }
        private int _TuNam;

        [ModelDefault("Caption", "Từ (năm)")]
        [VisibleInListView(false)]
        [ImmediatePostData]
        public int TuNam
        {
            get { return _TuNam; }
            set
            {
                _TuNam = value;
                if (!IsLoading)
                    DenNam = TuNam + 2;
            }
        }
        private int _DenNam;

        [ModelDefault("Caption", "Đến (năm)")]
        [VisibleInListView(false)]
        public int DenNam
        {
            get { return _DenNam; }
            set { _DenNam = value; }
        }
        public ThamNien(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            TuNam = 1;
        }
    }

}