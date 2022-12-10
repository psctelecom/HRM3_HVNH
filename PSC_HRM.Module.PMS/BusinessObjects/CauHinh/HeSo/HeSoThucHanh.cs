using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{
    //Mới Thêm Class Này
    [ModelDefault("Caption", "Hệ số thực hành")]
    [DefaultProperty("Caption")]
    [Appearance("Hide_HeSo_UFM", TargetItems = "TuKhoan; DenKhoan"
                                               , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHeSo.ThongTinTruong.TenVietTat = 'UFM'")]
    public class HeSoThucHanh : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoThucHanh")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }
        private int _TuKhoan;
        private int _DenKhoan;
        private decimal _HeSo_ThucHanh;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;

        [ModelDefault("Caption", "Từ khoản")]
        public int TuKhoan
        {
            get { return _TuKhoan; }
            set { SetPropertyValue("TuKhoan", ref _TuKhoan, value); }
        }

        [ModelDefault("Caption", "Đến khoản")]
        public int DenKhoan
        {
            get { return _DenKhoan; }
            set { SetPropertyValue("DenKhoan", ref _DenKhoan, value); }
        }
        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSo_ThucHanhs", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_ThucHanh
        {
            get { return _HeSo_ThucHanh; }
            set { SetPropertyValue("HeSo_ThucHanh", ref _HeSo_ThucHanh, value); }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set { SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value); }
        }
        public HeSoThucHanh(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            HeSo_ThucHanh = 1;
            TuKhoan = 0;
            DenKhoan = 0;
        }
    }
}
