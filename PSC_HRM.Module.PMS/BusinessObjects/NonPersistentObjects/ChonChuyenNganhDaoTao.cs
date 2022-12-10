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
namespace PSC_HRM.Module.PMS.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn chuyên ngành đào tạo")]
    public class ChonChuyenNganhDaoTao : BaseObject
    {
        private ChuyenNganhDaoTao _ChuyenNganhDaoTao;

        //
        [ModelDefault("Caption", "Chuyên Ngành Đào Tạo")]
        public ChuyenNganhDaoTao ChuyenNganhDaoTao
        {
            get
            {
                return _ChuyenNganhDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenNganhDaoTao", ref _ChuyenNganhDaoTao, value);
            }
         }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        public ChonChuyenNganhDaoTao(Session session)
            : base(session)
        { }
    }
}
