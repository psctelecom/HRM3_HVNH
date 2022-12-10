using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DanhGia
{
    [DefaultProperty("ThoiGian")]
    [ImageName("BO_BangTheoDoiViPham")]
    [ModelDefault("Caption", "Bảng theo dõi vi phạm")]
    public class BangTheoDoiViPham : BaseObject
    {
        // Fields...
        private QuanLyViPham _QuanLyViPham;
        private DateTime _ThoiGian;

        [ModelDefault("Caption", "Quản lý vi phạm")]
        [Association("QuanLyViPham-ListBangTheoDoiViPham")]
        public QuanLyViPham QuanLyViPham
        {
            get
            {
                return _QuanLyViPham;
            }
            set
            {
                SetPropertyValue("QuanLyViPham", ref _QuanLyViPham, value);
            }
        }

        [ModelDefault("Caption", "Thời gian")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime ThoiGian
        {
            get
            {
                return _ThoiGian;
            }
            set
            {
                SetPropertyValue("ThoiGian", ref _ThoiGian, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangTheoDoiViPham-ListChiTietViPham")]
        public XPCollection<ChiTietViPham> ListChiTietViPham
        {
            get
            {
                return GetCollection<ChiTietViPham>("ListChiTietViPham");
            }
        }

        public BangTheoDoiViPham(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThoiGian = HamDungChung.GetServerTime();
        }
    }

}
