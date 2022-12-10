using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;


namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Đơn giá phân loại")]
    public class DonGiaTheoPhanLoai : BaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private DanhMucLoaiKhongPhanThoiKhoaBieu _DanhMucPhanLoai;
        private decimal _DonGia;
        private string _GhiChu;


        [ModelDefault("Caption", "Năm học")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "Phân loại")]
        public DanhMucLoaiKhongPhanThoiKhoaBieu DanhMucPhanLoai
        {
            get { return _DanhMucPhanLoai; }
            set { SetPropertyValue("DanhMucPhanLoai", ref _DanhMucPhanLoai, value); }
        }


        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Đơn giá")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }

        [ModelDefault("Caption", "GhiChu")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        public DonGiaTheoPhanLoai(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
