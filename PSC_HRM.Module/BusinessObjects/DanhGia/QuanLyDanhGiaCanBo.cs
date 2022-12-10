using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhGia
{
    [DefaultClassOptions]
    [ImageName("list1")]
    [DefaultProperty("Nam")]
    [ModelDefault("Caption", "Quản lý đánh giá cán bộ")]
    public class QuanLyDanhGiaCanBo : BaseObject
    {
        // Fields...
        private int _Nam;

        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("Caption", "Năm đánh giá")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách các tháng")]
        [Association("QuanLyDanhGiaCanBo-ListDanhGiaCanBo")]
        public XPCollection<DanhGiaCanBo> ListDanhGiaCanBo
        {
            get
            {
                return GetCollection<DanhGiaCanBo>("ListDanhGiaCanBo");
            }
        }

        public QuanLyDanhGiaCanBo(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DateTime current = HamDungChung.GetServerTime();
            Nam = current.Year;
        }
    }

}
