using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ChuyenNgach
{
    [ImageName("BO_ChuyenNgach")]
    [DefaultProperty("ThangText")]
    [ModelDefault("Caption", "Đề nghị chuyển ngạch")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyChuyenNgach;Thang")]
    public class DeNghiChuyenNgach : BaseObject
    {
        // Fields...
        private DateTime _Thang;
        private QuanLyChuyenNgach _QuanLyChuyenNgach;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý nâng ngạch")]
        [Association("QuanLyChuyenNgach-ListDeNghiChuyenNgach")]
        public QuanLyChuyenNgach QuanLyChuyenNgach
        {
            get
            {
                return _QuanLyChuyenNgach;
            }
            set
            {
                SetPropertyValue("QuanLyChuyenNgach", ref _QuanLyChuyenNgach, value);
            }
        }

        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.StartMonth);
                ThangText = value.ToString("MM/yyyy");
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Tháng")]
        public string ThangText { get; set; }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("DeNghiChuyenNgach-ListChiTietDeNghiChuyenNgach")]
        public XPCollection<ChiTietDeNghiChuyenNgach> ListChiTietDeNghiChuyenNgach
        {
            get
            {
                return GetCollection<ChiTietDeNghiChuyenNgach>("ListChiTietDeNghiChuyenNgach");
            }
        }

        public DeNghiChuyenNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Thang = HamDungChung.GetServerTime();
        }
    }

}
