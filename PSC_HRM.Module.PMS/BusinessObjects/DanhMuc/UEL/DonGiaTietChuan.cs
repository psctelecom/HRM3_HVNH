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
    [ModelDefault("Caption", "Đơn giá tiết chuẩn")]
    public class DonGiaTietChuan : BaseObject
    {
        private BacDaoTao _BacDaoTao;
        private decimal _SoTien;

        [ModelDefault("Caption", "Bậc đào tạo")]

        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set
            {
                SetPropertyValue("BacDaoTao", ref _BacDaoTao, value);
            }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Số tiền")]
        public decimal SoTien
        {
            get { return _SoTien; }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }
        public DonGiaTietChuan(Session session) : base(session) { }

    }
}
