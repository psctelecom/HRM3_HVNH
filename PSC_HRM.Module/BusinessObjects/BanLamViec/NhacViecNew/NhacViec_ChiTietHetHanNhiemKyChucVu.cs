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
    public class NhacViec_ChiTietHetHanNhiemKyChucVu : BaseObject
    {
        private QuyetDinh.QuyetDinh _QuyetDinh;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private ChucVu _ChucVu;
        private DateTime _NgayBoNhiemChucVu;
        private DateTime _NgayHetHanNhiemKy;
        private string _GhiChu;

        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinh.QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
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

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm chức vụ")]
        [ModelDefault("DisplayFormat","dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayBoNhiemChucVu
        {
            get
            {
                return _NgayBoNhiemChucVu;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemChucVu", ref _NgayBoNhiemChucVu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn nhiệm kỳ")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayHetHanNhiemKy
        {
            get
            {
                return _NgayHetHanNhiemKy;
            }
            set
            {
                SetPropertyValue("NgayHetHanNhiemKy", ref _NgayHetHanNhiemKy, value);
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

        public NhacViec_ChiTietHetHanNhiemKyChucVu(Session session) : base(session) { }

    }

}
