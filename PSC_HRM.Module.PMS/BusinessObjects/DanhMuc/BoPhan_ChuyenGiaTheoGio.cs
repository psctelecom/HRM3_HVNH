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
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Bộ phận có chuyên gia tính theo giờ")]
    [DefaultProperty("BoPhan.TenBoPhan")]
    public class BoPhan_ChuyenGiaTheoGio : BaseObject
    {
        private string _MaQuanLy;
        private BoPhan _BoPhan;
        private decimal _SoPhutMotTiet;

        [ModelDefault("Caption", "Mã quản lý")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("AllowEdit","False")]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "tên đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("AllowEdit", "False")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set 
            { 
                SetPropertyValue("BoPhan", ref _BoPhan, value);                 
           }
        }

        [ModelDefault("Caption", "Số phút/tiết")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("AllowEdit", "False")]
        public decimal SoPhutMotTiet
        {
            get { return _SoPhutMotTiet; }
            set
            {
                SetPropertyValue("SoPhutMotTiet", ref _SoPhutMotTiet, value);
            }
        }
        public BoPhan_ChuyenGiaTheoGio(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
