using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ChamCong
{
    [DefaultClassOptions]
    [DefaultProperty("ThoiGian")]
    [ImageName("BO_QuanLyChamCong")]
    [ModelDefault("Caption", "Quản lý chấm công nhân viên")]
    [RuleCombinationOfPropertiesIsUnique("QuanLyChamCongNhanVien", DefaultContexts.Save, "ThongTinTruong;KyTinhLuong")]//;LoaiLuong
    [Appearance("QuanLyChamCongNhanVien", TargetItems = "*", Enabled = false, Criteria = "KyTinhLuong is not null and KyTinhLuong.KhoaSo")]   
    public class QuanLyChamCongNhanVien : BaoMatBaseObject, IThongTinTruong
    {
        // Fields...
        private ThongTinTruong _ThongTinTruong;
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;
        private bool _KhoaChamCong;
        //private LoaiLuongEnum _LoaiLuong;

        [ImmediatePostData]
        [DataSourceCriteria("!KhoaSo")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Kỳ tính lương")]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
                if (!IsLoading)
                {
                    //update ky tinh luong
                    KyTinhLuong = null;
                    //
                    KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and !KhoaSo", NgayLap.Month, NgayLap.Year));
                }
            }
        }

        [ModelDefault("Caption", "Khóa chấm công")]
        public bool KhoaChamCong
        {
            get
            {
                return _KhoaChamCong;
            }
            set
            {
                SetPropertyValue("KhoaChamCong", ref _KhoaChamCong, value);
            }
        }

        //[ImmediatePostData]
        //[ModelDefault("Caption", "Loại lương")]
        //public LoaiLuongEnum LoaiLuong
        //{
        //    get
        //    {
        //        return _LoaiLuong;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LoaiLuong", ref _LoaiLuong, value);
        //    }
        //}

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuanLyChamCongNhanVien-ChiTietChamCongNhanVienList")]
        public XPCollection<ChiTietChamCongNhanVien> ChiTietChamCongNhanVienList
        {
            get
            {
                return GetCollection<ChiTietChamCongNhanVien>("ChiTietChamCongNhanVienList");
            }
        }

        public QuanLyChamCongNhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLap = HamDungChung.GetServerTime();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);           
        }  
    }

}
