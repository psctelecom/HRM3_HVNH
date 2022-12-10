using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ChamCong
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông tin chi tiết")]
    public class ChiTietDanhSachTheoDoiNgayNghi : TruongBaseObject, ISupportController, IBoPhan
    {
        private string _MaQuanLy;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private HinhThucNghi _HinhThucNghi;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private decimal _SoNgayNghi;
        private string _DienGiai;
        private TinhTrang _TinhTrang;

        [ModelDefault("Caption", "Mã nhân sự")]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ImmediatePostData]
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
                    BoPhan = value.BoPhan;
                }
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Hình thức nghỉ")]
        public HinhThucNghi HinhThucNghi
        {
            get
            {
                return _HinhThucNghi;
            }
            set
            {
                SetPropertyValue("HinhThucNghi", ref _HinhThucNghi, value);
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

        [ModelDefault("Caption", "Số ngày nghỉ")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoNgayNghi
        {
            get
            {
                return _SoNgayNghi;
            }
            set
            {
                SetPropertyValue("SoNgayNghi", ref _SoNgayNghi, value);
            }
        }

        [ModelDefault("Caption", "Diễn giải")]
        public string DienGiai
        {
            get
            {
                return _DienGiai;
            }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }

        public ChiTietDanhSachTheoDoiNgayNghi(Session session)
            : base(session)
        { }        
    }
}
