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
    [ModelDefault("Caption", "Chi tiết bậc - Hệ đào tạo")]
    public class Bac_HeDaoTao : BaseObject
    {
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        private bool _TinhKhoiLuong;
        private bool _NgungSuDung;

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
        [ModelDefault("Caption", "Ngưng sử dụng")]
        [ImmediatePostData]
        public bool NgungSuDung
        {
            get { return _NgungSuDung; }
            set
            {
                SetPropertyValue("NgungSuDung", ref _NgungSuDung, value);
            }
        }
        [ModelDefault("Caption", "Tính khối lượng")]
        [ImmediatePostData]
        public bool TinhKhoiLuong
        {
            get { return _TinhKhoiLuong; }
            set
            {
                SetPropertyValue("TinhKhoiLuong", ref _TinhKhoiLuong, value);
            }
        }
        [VisibleInDetailView(false)]
        [NonPersistent]

        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format(" {0} {1}", HeDaoTao != null ? HeDaoTao.TenHeDaoTao : "", BacDaoTao != null ? " - " + BacDaoTao.TenBacDaoTao : "");
            }
        }

        public Bac_HeDaoTao(Session session) : base(session) { }

    }
}
