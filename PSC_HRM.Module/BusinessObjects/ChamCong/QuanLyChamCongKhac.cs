using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ChamCong
{
    [DefaultClassOptions]
    [DefaultProperty("ThoKyTinhLuongiGian")]
    [ImageName("BO_QuanLyChamCong")]
    [ModelDefault("Caption", "Bảng chấm công khác")]
    [RuleCombinationOfPropertiesIsUnique("QuanLyChamCongKhac.IsUnique", DefaultContexts.Save, "ThongTinTruong;KyTinhLuong")]
    public class QuanLyChamCongKhac : BaoMatBaseObject
    {
        // Fields...
        private KyTinhLuong _KyTinhLuong;
        private LoaiChamCongKhac _LoaiChamCongKhac;

        [RuleRequiredField(DefaultContexts.Save)]
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

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Loại chấm công")]
        public LoaiChamCongKhac LoaiChamCongKhac
        {
            get
            {
                return _LoaiChamCongKhac;
            }
            set
            {
                SetPropertyValue("LoaiChamCongKhac", ref _LoaiChamCongKhac, value);
            }
        }


        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuanLyChamCongKhac-ChiTietChamCongKhacList")]
        public XPCollection<ChiTietChamCongKhac> ChiTietChamCongKhacList
        {
            get
            {
                return GetCollection<ChiTietChamCongKhac>("ChiTietChamCongKhacList");
            }
        }

        public QuanLyChamCongKhac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DateTime current = HamDungChung.GetServerTime();
            KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and KhoaSo", current.Month, current.Year));            
        }
    }

}
