using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.DanhGiaKPI
{
    //xét thi đua
    [DefaultClassOptions]
    [DefaultProperty("TenQuanLyDanhGia")]
    [ImageName("BO_DanhGiaCanBo")]
    [ModelDefault("Caption", "Quản lý đánh giá KPI")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "KyTinhLuong")]
    [Appearance("QuanLyDanhGiaKPI", TargetItems = "KyTinhLuong", Enabled = false, Criteria = "KyTinhLuong.KhoaSo = True")]
    public class QuanLyDanhGiaKPI : BaseObject
    {
        // Fields...
        private KyTinhLuong _KyTinhLuong;
        private string _TenQuanLyDanhGia;
        private DateTime _NgayLap;

        [ModelDefault("Caption", "Quản lý đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenQuanLyDanhGia
        {
            get
            {
                return _TenQuanLyDanhGia;
            }
            set
            {
                SetPropertyValue("TenQuanLyDanhGia", ref _TenQuanLyDanhGia, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
                if (!IsLoading && value != null)
                    TenQuanLyDanhGia = value.TenKy;
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách nhân viên")]
        [Association("QuanLyDanhGiaKPI-ListDanhGiaKPI")]
        public XPCollection<DanhGiaKPI> ListDanhGiaKPI
        {
            get
            {
                return GetCollection<DanhGiaKPI>("ListDanhGiaKPI");
            }
        }

        public QuanLyDanhGiaKPI(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLap = HamDungChung.GetServerTime();
            KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("TuNgay<=? and DenNgay>=? and !KhoaSo", NgayLap, NgayLap));
        }
    }

}
