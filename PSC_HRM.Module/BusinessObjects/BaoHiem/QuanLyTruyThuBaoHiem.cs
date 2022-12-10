using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoHiem
{
    [DefaultClassOptions]
    [DefaultProperty("Caption")]
    [ImageName("BO_TruyThuBaoHiem")]
    [ModelDefault("Caption", "Truy thu bảo hiểm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;DenNgay;Dot")]
    public class QuanLyTruyThuBaoHiem : BaoMatBaseObject
    {
        // Fields...
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private int _Dot = 1;
        private decimal _LaiSuatBHXH;
        private decimal _LaiSuatBHYT;

        [ModelDefault("Caption", "Đợt")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Dot
        {
            get
            {
                return _Dot;
            }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("EditMask", "N3")]
        [ModelDefault("DisplayFormat", "N3")]
        [ModelDefault("Caption", "Lãi suất BHXH, BHTN (%)")]
        public decimal LaiSuatBHXH
        {
            get
            {
                return _LaiSuatBHXH;
            }
            set
            {
                SetPropertyValue("LaiSuatBHXH", ref _LaiSuatBHXH, value);
            }
        }

        [ModelDefault("EditMask", "N3")]
        [ModelDefault("DisplayFormat", "N3")]
        [ModelDefault("Caption", "Lãi suất BHYT (%)")]
        public decimal LaiSuatBHYT
        {
            get
            {
                return _LaiSuatBHYT;
            }
            set
            {
                SetPropertyValue("LaiSuatBHYT", ref _LaiSuatBHYT, value);
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public string Caption
        {
            get
            {
                if (DenNgay != DateTime.MinValue)
                    return "Kỳ báo cáo: " + DenNgay.ToString("MM/yyyy");
                return "Kỳ báo cáo: ";
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuanLyTruyThuBaoHiem-ListTruyThuBaoHiem")]
        public XPCollection<TruyThuBaoHiem> ListTruyThuBaoHiem
        {
            get
            {
                return GetCollection<TruyThuBaoHiem>("ListTruyThuBaoHiem");
            }
        }

        public QuanLyTruyThuBaoHiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DenNgay = HamDungChung.GetServerTime();

            using (XPCollection<QuanLyTruyThuBaoHiem> list = new XPCollection<QuanLyTruyThuBaoHiem>(Session, CriteriaOperator.Parse(""), new SortProperty("DenNgay", DevExpress.Xpo.DB.SortingDirection.Descending)))
            {
                list.TopReturnedObjects = 1;
                if (list.Count == 1)
                    TuNgay = list[0].DenNgay.AddDays(1);
                else
                    TuNgay = DenNgay.AddYears(-1);
            }

            ThongTinTruong truong = HamDungChung.ThongTinTruong(Session);
            if (truong != null)
            {
                LaiSuatBHXH = truong.ThongTinChung.LaiSuatBHXH;
                LaiSuatBHYT = truong.ThongTinChung.LaiSuatBHYT;
            }
            else
            {
                LaiSuatBHXH = 1.183m;
                LaiSuatBHYT = 0.75m;
            }
        }
    }

}
