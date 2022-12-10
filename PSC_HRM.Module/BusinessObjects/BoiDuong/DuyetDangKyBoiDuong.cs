using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.BoiDuong
{
    [ImageName("BO_QuanLyBoiDuong")]
    [DefaultProperty("DangKyBoiDuong")]
    [ModelDefault("Caption", "Duyệt đăng ký bồi dưỡng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyBoiDuong;DangKyBoiDuong")]
    public class DuyetDangKyBoiDuong : BaseObject
    {
        private QuocGia _QuocGia;
        private DangKyBoiDuong _DangKyBoiDuong;
        private ChuongTrinhBoiDuong _ChuongTrinhBoiDuong;
        private QuanLyBoiDuong _QuanLyBoiDuong;
        private string _GhiChu;
        private NguonKinhPhi _NguonKinhPhi;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý bồi dưỡng")]
        [Association("QuanLyBoiDuong-ListDuyetDangKyBoiDuong")]
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

        //dung de luu vet da duyet chua
        [ImmediatePostData]
        [ModelDefault("Caption", "Đăng ký bồi dưỡng")]
        public DangKyBoiDuong DangKyBoiDuong
        {
            get
            {
                return _DangKyBoiDuong;
            }
            set
            {
                SetPropertyValue("DangKyBoiDuong", ref _DangKyBoiDuong, value);
                if (!IsLoading && value != null)
                {
                    QuocGia = value.QuocGia;
                    ChuongTrinhBoiDuong = value.ChuongTrinhBoiDuong;
                    NguonKinhPhi = value.NguonKinhPhi;
                    TuNgay = value.TuNgay;
                    DenNgay = value.DenNgay;
                }
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

        [Browsable(false)]
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
        [Association("DuyetDangKyBoiDuong-ListChiTietDuyetDangKyBoiDuong")]
        public XPCollection<ChiTietDuyetDangKyBoiDuong> ListChiTietDuyetDangKyBoiDuong
        {
            get
            {
                return GetCollection<ChiTietDuyetDangKyBoiDuong>("ListChiTietDuyetDangKyBoiDuong");
            }
        }
        public void CreateListChiTietDuyetDangKyBoiDuong(HoSo_NhanVienItem item)
        {
            ChiTietDuyetDangKyBoiDuong chiTiet = new ChiTietDuyetDangKyBoiDuong(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietDuyetDangKyBoiDuong.Add(chiTiet);

        }
        public DuyetDangKyBoiDuong(Session session) : base(session) { }
    }

}
