using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.NangLuong
{
    [DefaultProperty("ThangText")]
    [ImageName("BO_DeNghiNangLuong")]
    [ModelDefault("Caption", "Đề nghị nâng lương")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyNangLuong;Quy")]
    [Appearance("Hide_UFM", TargetItems = "Thang", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'QNU'")]
    [Appearance("Hide_QNU", TargetItems = "Quy", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]

    public class DeNghiNangLuong : BaseObject
    {
        // Fields...
        private DateTime _Thang;
        private QuanLyNangLuong _QuanLyNangLuong;
        private QuyEnum _Quy;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý nâng lương")]
        [Association("QuanLyNangLuong-ListDeNghiNangLuong")]
        public QuanLyNangLuong QuanLyNangLuong
        {
            get
            {
                return _QuanLyNangLuong;
            }
            set
            {
                SetPropertyValue("QuanLyNangLuong", ref _QuanLyNangLuong, value);
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

        [Browsable(false)]
        public string Caption
        {
            get
            {
                if (Thang != DateTime.MinValue)
                    return "Tháng " + Thang.ToString("MM/yyyy");
                return "";
            }
        }

        [ModelDefault("Caption", "Quý")]
        public QuyEnum Quy
        {
            get
            {
                return _Quy;
            }
            set
            {
                SetPropertyValue("Quy", ref _Quy, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("DeNghiNangLuong-ListChiTietDeNghiNangLuong")]
        public XPCollection<ChiTietDeNghiNangLuong> ListChiTietDeNghiNangLuong
        {
            get
            {
                return GetCollection<ChiTietDeNghiNangLuong>("ListChiTietDeNghiNangLuong");
            }
        }


        [NonPersistent]
        [Browsable(false)]
        private string MaTruong { get; set; }
        public DeNghiNangLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            MaTruong = TruongConfig.MaTruong;
            Thang = HamDungChung.GetServerTime();
            Quy = HamDungChung.GetCurrentQuy();
        }
    }

}
