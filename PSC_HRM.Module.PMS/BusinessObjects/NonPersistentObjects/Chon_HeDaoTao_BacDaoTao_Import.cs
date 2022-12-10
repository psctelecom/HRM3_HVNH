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
using PSC_HRM.Module.BaoMat;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;


namespace PSC_HRM.Module.PMS.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn")]

    public class Chon_HeDaoTao_BacDaoTao_Import : BaseObject, IBoPhan
    {
        private BoPhan _BoPhan;
        
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;

        [ModelDefault("Caption", "Đơn vị")]
        [DataSourceCriteria("LoaiBoPhan = 3")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }
        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set
            {
                SetPropertyValue("BacDaoTao", ref _BacDaoTao, value);
            }
        }
        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set
            {
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);
            }
        }
        public Chon_HeDaoTao_BacDaoTao_Import(Session session)
            : base(session)
        { }

        [Browsable(false)]
        public XPCollection<BoPhanView> listbp { get; set; }
        public override void AfterConstruction()
        {
            base.AfterConstruction(); 
        }
    }
}
