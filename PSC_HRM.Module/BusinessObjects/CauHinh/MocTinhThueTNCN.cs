using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.CauHinh
{
    [DefaultProperty("Caption")]
    [ImageName("Action_ReportTemplate")]
    [ModelDefault("Caption", "Mốc tính thuế TNCN")]
    public class MocTinhThueTNCN : BaseObject
    {
        private decimal _MucThuNhapTinhThue;
        private int _ThueSuat7;
        private int _ThueSuat6;
        private int _ThueSuat5;
        private int _ThueSuat4;
        private int _ThueSuat3;
        private int _ThueSuat2;
        private int _ThueSuat1;
        private int _CoMaSoThue;
        private int _KhongCoMaSoThue;
        private int _KhongCuTru;
        private int _Moc1;
        private decimal _Tru1;
        private int _Moc2;
        private decimal _Tru2;
        private int _Moc3;
        private decimal _Tru3;
        private int _Moc4;
        private decimal _Tru4;
        private int _Moc5;
        private decimal _Tru5;
        private int _Moc6;
        private decimal _Tru6;
        private int _Moc7;
        private decimal _Tru7;

        [Browsable(false)]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("Mốc 1(5%): {Moc1:N} - Mốc 2(10%): {Moc2:N} ...", this);
            }
        }

        //moc 1
        [ModelDefault("Caption", "Từ")]
        public int Moc1
        {
            get
            {
                return _Moc1;
            }
            set
            {
                SetPropertyValue("Moc1", ref _Moc1, value);
            }
        }

        [ModelDefault("Caption", "Đến")]
        [ImmediatePostData]
        public int Moc2
        {
            get
            {
                return _Moc2;
            }
            set
            {
                SetPropertyValue("Moc2", ref _Moc2, value);
                OnChanged("Moc2Mirror");
            }
        }

        [ModelDefault("Caption", "Thuế suất (%)")]
        public int ThueSuat1
        {
            get
            {
                return _ThueSuat1;
            }
            set
            {
                SetPropertyValue("ThueSuat1", ref _ThueSuat1, value);
            }
        }

        [ModelDefault("Caption", "Trừ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru1
        {
            get
            {
                return _Tru1;
            }
            set
            {
                SetPropertyValue("Tru1", ref _Tru1, value);
            }
        }

        //moc 2
        [ModelDefault("Caption", "Từ2")]
        [NonPersistent]
        public int Moc2Mirror
        {
            get
            {
                return _Moc2;
            }
        }

        [ModelDefault("Caption", "Đến2")]
        [ImmediatePostData]
        public int Moc3
        {
            get
            {
                return _Moc3;
            }
            set
            {
                SetPropertyValue("Moc3", ref _Moc3, value);
                OnChanged("Moc3Mirror");
            }
        }

        [ModelDefault("Caption", "Thuế suất (%)2")]
        public int ThueSuat2
        {
            get
            {
                return _ThueSuat2;
            }
            set
            {
                SetPropertyValue("ThueSuat2", ref _ThueSuat2, value);
            }
        }

        [ModelDefault("Caption", "Trừ2")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru2
        {
            get
            {
                return _Tru2;
            }
            set
            {
                SetPropertyValue("Tru2", ref _Tru2, value);
            }
        }

        //moc 3
        [ModelDefault("Caption", "Từ3")]
        [NonPersistent]
        public int Moc3Mirror
        {
            get
            {
                return _Moc3;
            }
        }

        [ModelDefault("Caption", "Đến3")]
        [ImmediatePostData]
        public int Moc4
        {
            get
            {
                return _Moc4;
            }
            set
            {
                SetPropertyValue("Moc4", ref _Moc4, value);
                OnChanged("Moc4Mirror");
            }
        }

        [ModelDefault("Caption", "Thuế suất (%)3")]
        public int ThueSuat3
        {
            get
            {
                return _ThueSuat3;
            }
            set
            {
                SetPropertyValue("ThueSuat3", ref _ThueSuat3, value);
            }
        }

        [ModelDefault("Caption", "Trừ3")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru3
        {
            get
            {
                return _Tru3;
            }
            set
            {
                SetPropertyValue("Tru3", ref _Tru3, value);
            }
        }

        //moc 4
        [ModelDefault("Caption", "Từ4")]
        [NonPersistent]
        public int Moc4Mirror
        {
            get
            {
                return _Moc4;
            }
        }

        [ModelDefault("Caption", "Đến4")]
        [ImmediatePostData]
        public int Moc5
        {
            get
            {
                return _Moc5;
            }
            set
            {
                SetPropertyValue("Moc5", ref _Moc5, value);
                OnChanged("Moc5Mirror");
            }
        }

        [ModelDefault("Caption", "Thuế suất (%)4")]
        public int ThueSuat4
        {
            get
            {
                return _ThueSuat4;
            }
            set
            {
                SetPropertyValue("ThueSuat4", ref _ThueSuat4, value);
            }
        }

        [ModelDefault("Caption", "Trừ4")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru4
        {
            get
            {
                return _Tru4;
            }
            set
            {
                SetPropertyValue("Tru4", ref _Tru4, value);
            }
        }

        //moc 5
        [ModelDefault("Caption", "Từ5")]
        [NonPersistent]
        public int Moc5Mirror
        {
            get
            {
                return _Moc5;
            }
        }

        [ModelDefault("Caption", "Đến5")]
        [ImmediatePostData]
        public int Moc6
        {
            get
            {
                return _Moc6;
            }
            set
            {
                SetPropertyValue("Moc6", ref _Moc6, value);
                OnChanged("Moc6Mirror");
            }
        }

        [ModelDefault("Caption", "Thuế suất (%)5")]
        public int ThueSuat5
        {
            get
            {
                return _ThueSuat5;
            }
            set
            {
                SetPropertyValue("ThueSuat5", ref _ThueSuat5, value);
            }
        }

        [ModelDefault("Caption", "Trừ5")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru5
        {
            get
            {
                return _Tru5;
            }
            set
            {
                SetPropertyValue("Tru5", ref _Tru5, value);
            }
        }

        //moc 6
        [ModelDefault("Caption", "Từ6")]
        [NonPersistent]
        public int Moc6Mirror
        {
            get
            {
                return _Moc6;
            }
        }

        [ModelDefault("Caption", "Đến6")]
        [ImmediatePostData]
        public int Moc7
        {
            get
            {
                return _Moc7;
            }
            set
            {
                SetPropertyValue("Moc7", ref _Moc7, value);
                OnChanged("Moc7Mirror");
            }
        }

        [ModelDefault("Caption", "Thuế suất (%)6")]
        public int ThueSuat6
        {
            get
            {
                return _ThueSuat6;
            }
            set
            {
                SetPropertyValue("ThueSuat6", ref _ThueSuat6, value);
            }
        }

        [ModelDefault("Caption", "Trừ6")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru6
        {
            get
            {
                return _Tru6;
            }
            set
            {
                SetPropertyValue("Tru6", ref _Tru6, value);
            }
        }

        //moc 7
        [ModelDefault("Caption", "Từ7")]
        [NonPersistent]
        public int Moc7Mirror
        {
            get
            {
                return _Moc7;
            }
        }

        [ModelDefault("Caption", "Thuế suất (%)7")]
        public int ThueSuat7
        {
            get
            {
                return _ThueSuat7;
            }
            set
            {
                SetPropertyValue("ThueSuat7", ref _ThueSuat7, value);
            }
        }

        [ModelDefault("Caption", "Trừ7")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru7
        {
            get
            {
                return _Tru7;
            }
            set
            {
                SetPropertyValue("Tru7", ref _Tru7, value);
            }
        }

        [ModelDefault("Caption", "Mức thu nhập tính thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal MucThuNhapTinhThue
        {
            get
            {
                return _MucThuNhapTinhThue;
            }
            set
            {
                SetPropertyValue("MucThuNhapTinhThue", ref _MucThuNhapTinhThue, value);
            }
        }

        [ModelDefault("Caption", "% khấu trừ thuế TNCN (Có MST)")]
        public int CoMaSoThue
        {
            get
            {
                return _CoMaSoThue;
            }
            set
            {
                SetPropertyValue("CoMaSoThue", ref _CoMaSoThue, value);
            }
        }

        [ModelDefault("Caption", "% khấu trừ thuế TNCN (Không MST)")]
        public int KhongCoMaSoThue
        {
            get
            {
                return _KhongCoMaSoThue;
            }
            set
            {
                SetPropertyValue("KhongCoMaSoThue", ref _KhongCoMaSoThue, value);
            }
        }

        [ModelDefault("Caption", "% khấu trừ thuế TNCN (Không cư trú)")]
        public int KhongCuTru
        {
            get
            {
                return _KhongCuTru;
            }
            set
            {
                SetPropertyValue("KhongCuTru", ref _KhongCuTru, value);
            }
        }

        public MocTinhThueTNCN(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Moc1 = 0;
            Moc2 = 5000000;
            Moc3 = 10000000;
            Moc4 = 18000000;
            Moc5 = 32000000;
            Moc6 = 52000000;
            Moc7 = 80000000;
            ThueSuat1 = 5;
            ThueSuat2 = 10;
            ThueSuat3 = 15;
            ThueSuat4 = 20;
            ThueSuat5 = 25;
            ThueSuat6 = 30;
            ThueSuat7 = 35;
            Tru1 = 0;
            Tru2 = 250000;
            Tru3 = 750000;
            Tru4 = 1650000;
            Tru5 = 3250000;
            Tru6 = 5850000;
            Tru7 = 9850000;

            MucThuNhapTinhThue = 1000000;
            CoMaSoThue = 10;
            KhongCoMaSoThue = 20;
            KhongCuTru = 20;
        }
    }

}
