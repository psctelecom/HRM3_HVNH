using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Chi tiết Bậc - Hệ đào tạo")]
    public class ChiTietNhomBac_HeDaoTao : BaseObject
    {
        private DanhMucNhomBacHe _DanhMucNhomBacHe;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;


        [Association("DanhMucNhomBacHe-ListChiTietNhomBac_HeDaoTao")]
        [ModelDefault("Caption", "Quản lý")]
        [Browsable(false)]
        public DanhMucNhomBacHe DanhMucNhomBacHe
        {
            get
            {
                return _DanhMucNhomBacHe;
            }
            set
            {
                SetPropertyValue("DanhMucNhomBacHe", ref _DanhMucNhomBacHe, value);
            }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]

        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set
            {
                SetPropertyValue("BacDaoTao",ref _BacDaoTao, value);
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

        public ChiTietNhomBac_HeDaoTao(Session session) : base(session) { }

    }
}
