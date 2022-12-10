using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.NghiepVu.KhaoThi
{
    [Appearance("Hide_QuanLyThanhToanKhaoThi_LoaiGV", TargetItems = "HocKy"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "LoaiGiangVien = 0")]
    [ModelDefault("Caption", "Quản lý thanh toán khảo thí")]
    public class QuanLyThanhToanKhaoThi : BaseObject
    {
        private BoPhan _BoPhan;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private LoaiGiangVienEnum? _LoaiGiangVien;

        [ModelDefault("Caption", "Trường")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }


        [ModelDefault("Caption", "Loại GV")]
        public LoaiGiangVienEnum? LoaiGiangVien
        {
            get { return _LoaiGiangVien; }
            set { SetPropertyValue("LoaiGiangVien", ref _LoaiGiangVien, value); }
        }

        [Aggregated]
        [Association("QuanLyThanhToanKhaoThi-ListThongTinThanhToanKhaoThi")]
        [ModelDefault("Caption", "Chi tiết thanh toán")]
        public XPCollection<ThongTinThanhToanKhaoThi> ListThongTinThanhToanKhaoThi
        {
            get
            {
                return GetCollection<ThongTinThanhToanKhaoThi>("ListThongTinThanhToanKhaoThi");
            }
        }

        

        public QuanLyThanhToanKhaoThi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
