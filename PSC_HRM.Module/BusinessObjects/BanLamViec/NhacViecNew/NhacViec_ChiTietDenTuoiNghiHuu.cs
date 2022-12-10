using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.BanLamViec
{
    [NonPersistent]
    [DefaultClassOptions]
    [ImageName("BO_Money2")]
    [ModelDefault("Caption", "Nội dung chi tiết")]
    public class NhacViec_ChiTietDenTuoiNghiHuu : BaseObject
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private GioiTinhEnum _GioiTinh;
        private DateTime _NgaySinh;
        private DateTime _NgayNghiHuu;
        private string _GhiChu;

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
            }
        }

        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return _GioiTinh;
            }
            set
            {
                SetPropertyValue("GioiTinh", ref _GioiTinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        [ModelDefault("DisplayFormat","dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày nghỉ hưu")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayNghiHuu
        {
            get
            {
                return _NgayNghiHuu;
            }
            set
            {
                SetPropertyValue("NgayNghiHuu", ref _NgayNghiHuu, value);
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

        public NhacViec_ChiTietDenTuoiNghiHuu(Session session) : base(session) { }

    }

}
