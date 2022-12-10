using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DiNuocNgoai
{
    [ImageName("BO_QuanLyDiNuocNgoai")]
    [DefaultProperty("QuocGia")]
    [ModelDefault("Caption", "Đăng ký đi nước ngoài")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyDiNuocNgoai;QuocGia;TuNgay")]
    public class DangKyDiNuocNgoai : BaseObject
    {
        // Fields...
        private string _TruongHoTro;
        private string _LyDo;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private NguonKinhPhi _NguonKinhPhi;
        private QuocGia _QuocGia;
        private QuanLyDiNuocNgoai _QuanLyDiNuocNgoai;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý đi nước ngoài")]
        [Association("QuanLyDiNuocNgoai-ListDangKyDiNuocNgoai")]
        public QuanLyDiNuocNgoai QuanLyDiNuocNgoai
        {
            get
            {
                return _QuanLyDiNuocNgoai;
            }
            set
            {
                SetPropertyValue("QuanLyDiNuocNgoai", ref _QuanLyDiNuocNgoai, value);
            }
        }

        [ModelDefault("Caption", "Quốc gia")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
            }
        }

        [ModelDefault("Caption", "Nguồn kinh phí")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NguonKinhPhi NguonKinhPhi
        {
            get
            {
                return _NguonKinhPhi;
            }
            set
            {
                SetPropertyValue("NguonKinhPhi", ref _NguonKinhPhi, value);
            }
        }

        [ModelDefault("Caption", "Trường hỗ trợ")]
        public string TruongHoTro
        {
            get
            {
                return _TruongHoTro;
            }
            set
            {
                SetPropertyValue("TruongHoTro", ref _TruongHoTro, value);
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

        [Size(500)]
        [ModelDefault("Caption", "Lý do")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("DangKyDiNuocNgoai-ListChiTietDangKyDiNuocNgoai")]
        public XPCollection<ChiTietDangKyDiNuocNgoai> ListChiTietDangKyDiNuocNgoai
        {
            get
            {
                return GetCollection<ChiTietDangKyDiNuocNgoai>("ListChiTietDangKyDiNuocNgoai");
            }
        }

        public DangKyDiNuocNgoai(Session session) : base(session) { }
    }

}
