using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;


namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Độ ưu tiên trừ giờ")]
    public class ThuTuTruGioUuTien : BaseObject
    {
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        private bool _DayTiengAnh;
        private int _ThuTu;
        private decimal _HeSo_DaoTao;
        private decimal _HeSoVuotGio;

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }

        [ModelDefault("Caption", "Tiếng anh")]
        public bool DayTiengAnh
        {
            get { return _DayTiengAnh; }
            set { SetPropertyValue("DayTiengAnh", ref _DayTiengAnh, value); }
        }

        [ModelDefault("Caption", "Thứ tự")]
        public int ThuTu
        {
            get { return _ThuTu; }
            set { SetPropertyValue("ThuTu", ref _ThuTu, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_DaoTao
        {
            get { return _HeSo_DaoTao; }
            set { SetPropertyValue("HeSo_DaoTao", ref _HeSo_DaoTao, value); }
        }

        [ModelDefault("Caption", "Hệ số vượt giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoVuotGio
        {
            get { return _HeSoVuotGio; }
            set { SetPropertyValue("HeSoVuotGio", ref _HeSoVuotGio, value); }
        }
        public ThuTuTruGioUuTien(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
