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
    public class NhacViec_ChiTietHetHanNghiThaiSan : BaseObject
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private TinhTrang _TinhTrang;
        private DateTime _NgayNghiThaiSan;
        private DateTime _NgayTroLaiLamViec;
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

        [ModelDefault("Caption", "Ngày bắt đầu nghỉ")]
        [ModelDefault("DisplayFormat","dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayNghiThaiSan
        {
            get
            {
                return _NgayNghiThaiSan;
            }
            set
            {
                SetPropertyValue("NgayNghiThaiSan", ref _NgayNghiThaiSan, value);
            }
        }

        [ModelDefault("Caption", "Ngày trở lại làm việc")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayTroLaiLamViec
        {
            get
            {
                return _NgayTroLaiLamViec;
            }
            set
            {
                SetPropertyValue("NgayTroLaiLamViec", ref _NgayTroLaiLamViec, value);
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

        public NhacViec_ChiTietHetHanNghiThaiSan(Session session) : base(session) { }

    }

}
