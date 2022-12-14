using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThoiViec
{
    [NonPersistent]
    [ImageName("BO_NghiHuu")]
    [ModelDefault("Caption", "Danh sách cán bộ")]
    public class DanhSachCanBoNghiThaiSan : TruongBaseObject, ISupportController
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _DonVi;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        
        [ModelDefault("Caption", "Cán bộ")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    DonVi = value.BoPhan;
                }

            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan DonVi
        {
            get
            {
                return _DonVi;
            }
            set
            {
                SetPropertyValue("DonVi", ref _DonVi, value);
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

        public DanhSachCanBoNghiThaiSan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
