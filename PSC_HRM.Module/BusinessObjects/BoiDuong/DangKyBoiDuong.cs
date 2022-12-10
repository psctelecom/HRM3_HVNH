using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.BoiDuong
{
    [ImageName("BO_QuanLyBoiDuong")]
    [DefaultProperty("ChuongTrinhBoiDuong")]
    [ModelDefault("Caption", "Đăng ký bồi dưỡng")]
    [RuleCombinationOfPropertiesIsUnique("DangKyBoiDuong.Unique", DefaultContexts.Save, "QuanLyBoiDuong;ChuongTrinhBoiDuong")]
    public class DangKyBoiDuong : BaseObject
    {
        private QuocGia _QuocGia;
        private ChuongTrinhBoiDuong _ChuongTrinhBoiDuong;
        private QuanLyBoiDuong _QuanLyBoiDuong;
        private string _GhiChu;
        private NguonKinhPhi _NguonKinhPhi;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý bồi dưỡng")]
        [Association("QuanLyBoiDuong-ListDangKyBoiDuong")]
        public QuanLyBoiDuong QuanLyBoiDuong
        {
            get
            {
                return _QuanLyBoiDuong;
            }
            set
            {
                SetPropertyValue("QuanLyBoiDuong", ref _QuanLyBoiDuong, value);
            }
        }

        [ModelDefault("Caption", "Quốc gia")]
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

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Chương trình bồi dưỡng")]
        public ChuongTrinhBoiDuong ChuongTrinhBoiDuong
        {
            get
            {
                return _ChuongTrinhBoiDuong;
            }
            set
            {
                SetPropertyValue("ChuongTrinhBoiDuong", ref _ChuongTrinhBoiDuong, value);
            }
        }

        [ModelDefault("Caption", "Nguồn kinh phí")]
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

        [Size(300)]
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ đăng ký")]
        [Association("DangKyBoiDuong-ListChiTietDangKyBoiDuong")]
        public XPCollection<ChiTietDangKyBoiDuong> ListChiTietDangKyBoiDuong
        {
            get
            {
                return GetCollection<ChiTietDangKyBoiDuong>("ListChiTietDangKyBoiDuong");
            }
        }

        public DangKyBoiDuong(Session session) : base(session) { }
    }

}
