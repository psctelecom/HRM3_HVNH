using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
namespace PSC_HRM.Module.HoSo
{
    [ImageName("BO_GiaDinh")]
    [DefaultProperty("QuanHeGiaDinh")]
    [ModelDefault("Caption", "Giảm trừ gia cảnh")]
    public class GiamTruGiaCanh : BaseObject
    {
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private ThongTinNhanVien _ThongTinNhanVien;
        private QuanHeGiaDinh _QuanHeGiaDinh;
        private LoaiGiamTruGiaCanh _LoaiGiamTruGiaCanh;
        private string _GhiChu;
        private ThongTinNhanVien old;
        private GiayToHoSo _GiayToHoSo;
        private string _CMND;

        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListGiamTruGiaCanh")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                //luu vet de update so nguoi phu thuoc
                if (value == null)
                    old = _ThongTinNhanVien;
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Họ tên người thân")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("ThongTinNhanVien.ListQuanHeGiaDinh")]
        public QuanHeGiaDinh QuanHeGiaDinh
        {
            get
            {
                return _QuanHeGiaDinh;
            }
            set
            {
                SetPropertyValue("QuanHeGiaDinh", ref _QuanHeGiaDinh, value);
            }
        }

        [ModelDefault("Caption", "Tên gia cảnh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiGiamTruGiaCanh LoaiGiamTruGiaCanh
        {
            get
            {
                return _LoaiGiamTruGiaCanh;
            }
            set
            {
                SetPropertyValue("LoaiGiamTruGiaCanh", ref _LoaiGiamTruGiaCanh, value);
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

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [ModelDefault("Caption", "Lưu trữ giấy tờ")]
        [DataSourceProperty("GiayToList")]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
            }
        }


        [ModelDefault("Caption", "Số CMND")]
        public string CMND
        {
            get
            {
                return _CMND;
            }
            set
            {
                SetPropertyValue("CMND", ref _CMND, value);
            }
        }

        public GiamTruGiaCanh(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<GiayToHoSo> GiayToList
        {
            get
            {
                if (ThongTinNhanVien != null)
                    return ThongTinNhanVien.ListGiayToHoSo;
                return null;
            }
        }

        protected override void OnDeleted()
        {
            base.OnDeleted();

            if (old != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("Oid=?", old.Oid);
                object count = Session.Evaluate<ThongTinNhanVien>(CriteriaOperator.Parse("ListGiamTruGiaCanh.Count"), filter);
                if (count != null)
                    old.NhanVienThongTinLuong.SoNguoiPhuThuoc = (int)count > 0 ? (int)count - 1 : 0;
                old.Save();
            }
        }
    }

}
