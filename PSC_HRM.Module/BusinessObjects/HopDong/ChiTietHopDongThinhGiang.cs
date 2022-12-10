using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.HopDong
{
    [DefaultProperty("Lop")]
    [ImageName("BO_Contract")]
    [ModelDefault("Caption", "Chi tiết hợp đồng thỉnh giảng")]
    public class ChiTietHopDongThinhGiang : TruongBaseObject
    {
        // Fields...
        private decimal _SoTiet;
        private decimal _SoTietTH;
        private decimal _SoTietLT;
        private HopDong_ThinhGiang _HopDongThinhGiang;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
      
        private BoPhan _BoMon;
        private BoPhan _TaiKhoa;
        private string _MonHoc;
        private string _Lop;
        private decimal _SiSo;
        

     
        [ImmediatePostData]
        [ModelDefault("Caption", "Môn dạy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CurriculumEditor")]
        public string MonHoc
        {
            get
            {
                return _MonHoc;
            }
            set
            {
                SetPropertyValue("MonHoc", ref _MonHoc, value);
            }
        }


        [ModelDefault("Caption", "Tại khoa")]
        public BoPhan TaiKhoa
        {
            get
            {
                return _TaiKhoa;
            }
            set
            {
                SetPropertyValue("TaiKhoa", ref _TaiKhoa, value);
            }
        }
     

        [ModelDefault("Caption", "Bộ môn")]
        [DataSourceProperty("TaiKhoa.ListBoPhanCon", DataSourcePropertyIsNullMode.SelectAll)]
        public BoPhan BoMon
        {
            get
            {
                return _BoMon;
            }
            set
            {
                SetPropertyValue("BoMon", ref _BoMon, value);
            }
        }

        [ModelDefault("Caption", "Số tiết")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal SoTiet
        {
            get
            {
                return _SoTiet;
            }
            set
            {
                SetPropertyValue("SoTiet", ref _SoTiet, value);
            }
        }
      

        [ImmediatePostData]
        [ModelDefault("Caption", "Lớp")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.ScheduleStudyEditor")]
        public string Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
            }
        }

        [ModelDefault("Caption", "Số tiết thực hành")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoTietTH
        {
            get
            {
                return _SoTietTH;
            }
            set
            {
                SetPropertyValue("SoTietTH", ref _SoTietTH, value);
            }
        }

        [ModelDefault("Caption", "Số tiết lý thuyết")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoTietLT
        {
            get
            {
                return _SoTietLT;
            }
            set
            {
                SetPropertyValue("SoTietLT", ref _SoTietLT, value);
            }
        }

        [ModelDefault("Caption", "Sĩ số")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SiSo
        {
            get
            {
                return _SiSo;
            }
            set
            {
                SetPropertyValue("SiSo", ref _SiSo, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
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

      
        public ChiTietHopDongThinhGiang(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            MaTruong = TruongConfig.MaTruong;
        }

   
       

        [Browsable(false)]
        [ModelDefault("Caption", "Hợp đồng thỉnh giảng")]
        [Association("HopDong_ThinhGiang-ListChiTietHopDongThinhGiang")]
        public HopDong_ThinhGiang HopDongThinhGiang
        {
            get
            {
                return _HopDongThinhGiang;
            }
            set
            {
                SetPropertyValue("HopDongThinhGiang", ref _HopDongThinhGiang, value);
                TuNgay = HopDongThinhGiang.TuNgay;
                DenNgay = HopDongThinhGiang.DenNgay;

            }
        }


        [Aggregated]
        [ModelDefault("Caption", "Chi Tiết Thanh Toán Hợp Đồng")]
        [Association("ChiTietHopDongThinhGiang-ListChiTietHopDongThinhGiang")]
        public XPCollection<ChiTietThanhToanHopDong> ListChiTietThanhToanHopDong
        {
            get
            {
                return GetCollection<ChiTietThanhToanHopDong>("ListChiTietThanhToanHopDong");
            }
        }

    }

}
