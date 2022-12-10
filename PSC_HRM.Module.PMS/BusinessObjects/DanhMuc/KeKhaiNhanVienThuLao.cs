using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BusinessObjects.DanhMuc
{
    public class KeKhaiNhanVienThuLao : BaseObject
    {
        private BacDaoTao _BacDaoTao;
        private NhanVien _NhanVien;
        private string _MaQuanLy;
        private string _LopHocPhan;
        private string _TenHoatDong;
        private decimal _TongGio;
        private decimal _TongGioA1;
        private decimal _TongGioA2;
        private decimal _TongNo;
        private decimal _SoTienThanhToan;
        private CongTruPMSEnum _CongTru = 0;
        private ChiTietThuLaoNhanVien_Update _ChiTietThuLaoNhanVien_Update;

      
        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Họ tên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        //[Browsable(false)]
        [ModelDefault("Caption", "Mã lớp học phần")]
        //[Size(-1)]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        //[Browsable(false)]
        [ModelDefault("Caption", "Tên môn")]
        //[Size(-1)]
        public string TenHoatDong
        {
            get { return _TenHoatDong; }
            set { SetPropertyValue("TenHoatDong", ref _TenHoatDong, value); }
        }

        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }

        [ModelDefault("Caption", "Tổng giờ A1")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioA1
        {
            get { return _TongGioA1; }
            set { SetPropertyValue("TongGioA1", ref _TongGioA1, value); }
        }

        [ModelDefault("Caption", "Tổng giờ A2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioA2
        {
            get { return _TongGioA2; }
            set { SetPropertyValue("TongGioA2", ref _TongGioA2, value); }
        }

        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTienThanhToan
        {
            get { return _SoTienThanhToan; }
            set { SetPropertyValue("SoTienThanhToan", ref _SoTienThanhToan, value); }
        }

        [ModelDefault("Caption", "Số tiền nợ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongNo
        {
            get { return _TongNo; }
            set { SetPropertyValue("TongNo", ref _TongNo, value); }
        }

        [ModelDefault("Caption", "Cộng trừ")]
        public CongTruPMSEnum CongTru
        {
            get { return _CongTru; }
            set { SetPropertyValue("CongTru", ref _CongTru, value); }
        }

        //

        [ModelDefault("Caption", "Chi tiết thù lao nhân viên")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("ChiTietThuLaoNhanVien_Update-ListKeKhaiNhanVienThuLao")]
        public ChiTietThuLaoNhanVien_Update ChiTietThuLaoNhanVien_Update
        {
            get
            {
                return _ChiTietThuLaoNhanVien_Update;
            }
            set
            {
                SetPropertyValue("ChiTietThuLaoNhanVien_Update", ref _ChiTietThuLaoNhanVien_Update, value);
            }
        }


        public KeKhaiNhanVienThuLao(Session session)
            : base(session)
        { }
    }
}
