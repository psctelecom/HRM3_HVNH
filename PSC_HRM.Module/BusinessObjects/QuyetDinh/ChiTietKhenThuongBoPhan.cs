using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.KhenThuong;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Chi tiết khen thưởng đơn vị")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhKhenThuong;BoPhan")]
    [Appearance("Hide_GTVT", TargetItems = "DanhHieuKhenThuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong !='GTVT'")]
    public class ChiTietKhenThuongBoPhan : BaseObject, IBoPhan
    {
        private QuyetDinhKhenThuong _QuyetDinhKhenThuong;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private DanhHieuKhenThuong _DanhHieuKhenThuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định khen thưởng")]
        [Association("QuyetDinhKhenThuong-ListChiTietKhenThuongBoPhan")]
        public QuyetDinhKhenThuong QuyetDinhKhenThuong
        {
            get
            {
                return _QuyetDinhKhenThuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhKhenThuong", ref _QuyetDinhKhenThuong, value);                
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("",DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if(!IsLoading && value!=null)
                {
                    BoPhanText = value.TenBoPhan;
                }
            }
        }
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Bộ phận")]
       
        public string BoPhanText
        {
            get
            {
                return _BoPhanText;
            }
            set
            {
                SetPropertyValue("BoPhanText", ref _BoPhanText, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Danh hiệu")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong ='GTVT'")]
        public DanhHieuKhenThuong DanhHieuKhenThuong
        {
            get
            {
                return _DanhHieuKhenThuong;
            }
            set
            {
                SetPropertyValue("DanhHieuKhenThuong", ref _DanhHieuKhenThuong, value);
            }
        }

        [NonPersistent]
        [Browsable(false)]
        private string MaTruong { get; set; }

        public ChiTietKhenThuongBoPhan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
            if (QuyetDinhKhenThuong != null)
            {
                DanhHieuKhenThuong = this.QuyetDinhKhenThuong.DanhHieuKhenThuong;
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            } 
        }
    }

}
