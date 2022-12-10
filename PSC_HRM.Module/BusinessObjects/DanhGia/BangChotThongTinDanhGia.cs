using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DanhGia
{
    [ModelDefault("Caption", "Bảng chốt thông tin đánh giá")]
    [DefaultProperty("Caption")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ABC_EvaluationBoard;")]
    
    public class BangChotThongTinDanhGia : TruongBaseObject
    {
        // Fields...
        private ABC_EvaluationBoard _ABC_EvaluationBoard;
        
        [ModelDefault("Caption", "Bảng đánh giá")]
        public ABC_EvaluationBoard ABC_EvaluationBoard
        {
            get
            {
                return _ABC_EvaluationBoard;
            }
            set
            {
                SetPropertyValue("ABC_EvaluationBoard", ref _ABC_EvaluationBoard, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Thông tin đánh giá")]
        [Association("BangChotThongTinDanhGia-ListThongTinDanhGia")]
        public XPCollection<ThongTinDanhGia> ListThongTinDanhGia
        {
            get
            {
                return GetCollection<ThongTinDanhGia>("ListThongTinDanhGia");
            }
        }

        public BangChotThongTinDanhGia(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
