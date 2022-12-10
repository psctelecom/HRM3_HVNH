using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Thời Khóa Biểu")]

    public class ThoiKhoaBieu : BaseObject
    {
        private HocPhan _HocPhan;
        private decimal _SoTinChi;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private string _BuoiSangThu;
        private string _BuoiChieuThu;
        private HinhThucThi _TenHinhThucThi;
        public string _PhongHoc;
        private NhanVien _GiangVien;
        private BoPhan _BoPhan;
        private string _GhiChu;
        private ChuyenNganhDaoTao _ChuyenNganhDaoTao;
        private QuanLyThoiKhoaBieu _QuanLyThoiKhoaBieu;

        [ModelDefault("Caption", "Tên Học Phần")]
        public HocPhan HocPhan
        {
            get
            {
                return _HocPhan;
            }
            set
            {
                SetPropertyValue("HocPhan", ref _HocPhan, value);
                //Nếu HocPhan thay đổi thì cập nhật SoTinChi theo HocPhan
                if (!IsLoading && value != null)
                {
                    SoTinChi = value.SoTinChi;
                }
            }
        }
        [ModelDefault("Caption", "Số tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AlowEdit", "false")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal SoTinChi
        {
            get
            {
                return _SoTinChi;
            }
            set
            {
                SetPropertyValue("SoTinChi", ref _SoTinChi, value);
            }
        }
        [ModelDefault("Caption", "Từ Ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
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
        [ModelDefault("Caption", "Đến Ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
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
        [ModelDefault("Caption", "Buổi Sáng Thứ")]
        public string BuoiSangThu
        {
            get
            {
                return _BuoiSangThu;
            }
            set
            {
                SetPropertyValue("BuoiSangThu", ref _BuoiSangThu, value);
            }
        }
        [ModelDefault("Caption", "Buổi Chiều Thứ")]
        public string BuoiChieuThu
        {
            get
            {
                return _BuoiChieuThu;
            }
            set
            {
                SetPropertyValue("BuoiChieuThu", ref _BuoiChieuThu, value);
            }
        }
        [ModelDefault("Caption", "Phòng Học")]//Chỉ có PhongThi không có PhongHoc nên để kiểu string
        public string PhongHoc
        {
            get
            {
                return _PhongHoc;
            }
            set
            {
                SetPropertyValue("PhongHoc", ref _PhongHoc, value);
            }
        }
        [ModelDefault("Caption", "Hình Thức Thi")]
        public HinhThucThi TenHinhThucThi
        {
            get
            {
                return _TenHinhThucThi;
            }
            set
            {
                SetPropertyValue("TenHinhThucThi", ref _TenHinhThucThi, value);
            }
        }
        [ModelDefault("Caption", "Giảng Viên Phụ Trách")]
        [RuleRequiredField("", DefaultContexts.Save, "Không bỏ trống Nhân Viên")]
        public NhanVien GiangVien
        {
            get
            {
                return _GiangVien;
            }
            set
            {
                SetPropertyValue("GiangVien", ref _GiangVien, value);
            }
        }
        [ModelDefault("Caption", "Đơn Vị Công Tác")]
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
        [ModelDefault("Caption", "Ghi Chú (Lớp Ghép)")]
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
        [ModelDefault("Caption", "Chuyên Ngành Đào Tạo")]
        public ChuyenNganhDaoTao ChuyenNganhDaoTao
        {
            get
            {
                return _ChuyenNganhDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenNganhDaoTao", ref _ChuyenNganhDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Quản lý thời khóa biểu")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyThoiKhoaBieu-ListThoiKhoaBieu")]
        public QuanLyThoiKhoaBieu QuanLyThoiKhoaBieu
        {
            get
            {
                return _QuanLyThoiKhoaBieu;
            }
            set
            {
                SetPropertyValue("QuanLyThoiKhoaBieu", ref _QuanLyThoiKhoaBieu, value);
            }
        }

        public ThoiKhoaBieu(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
    }
}
