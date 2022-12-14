using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.KhenThuong
{
    [DefaultProperty("BoPhan")]
    [ImageName("BO_QuanLyKhenThuong")]
    [ModelDefault("Caption", "Chi tiết tập thể đề nghị khen thưởng")]
    public class ChiTietTapTheDeNghiKhenThuong : BaseObject, IBoPhan
    {
        private BoPhan _BoPhan;
        private string _GhiChu;
        private ChiTietDeNghiKhenThuong _ChiTietDeNghiKhenThuong;
        private string _SoPhieuBoMon;
        private string _SoPhieuDonVi;
        private string _SoPhieuHoiDong;
        private string _ThanhTichNCKH;
        private string _ThanhTichKhac;
        private HinhThucViPham _HinhThucViPham;

        [Browsable(false)]
        [ModelDefault("Caption", "Chi tiết đề nghị khen thưởng")]
        [Association("ChiTietDeNghiKhenThuong-ListChiTietTapTheDeNghiKhenThuong")]
        public ChiTietDeNghiKhenThuong ChiTietDeNghiKhenThuong
        {
            get
            {
                return _ChiTietDeNghiKhenThuong;
            }
            set
            {
                SetPropertyValue("ChiTietDeNghiKhenThuong", ref _ChiTietDeNghiKhenThuong, value);
            }
        }
        
        [ModelDefault("Caption", "Tập thể")]
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

        [ModelDefault("Caption", "Số phiếu bộ môn")]
        public string SoPhieuBoMon
        {
            get
            {
                return _SoPhieuBoMon;
            }
            set
            {
                SetPropertyValue("SoPhieuBoMon", ref _SoPhieuBoMon, value);
            }
        }

        [ModelDefault("Caption", "Số phiếu đơn vị")]
        public string SoPhieuDonVi
        {
            get
            {
                return _SoPhieuDonVi;
            }
            set
            {
                SetPropertyValue("SoPhieuDonVi", ref _SoPhieuDonVi, value);
            }
        }

        [ModelDefault("Caption", "Số phiếu Hội đồng cơ sở")]
        public string SoPhieuHoiDong
        {
            get
            {
                return _SoPhieuHoiDong;
            }
            set
            {
                SetPropertyValue("SoPhieuHoiDong", ref _SoPhieuHoiDong, value);
            }
        }

        [ModelDefault("Caption", "Thành tích NCKH")]
        public string ThanhTichNCKH
        {
            get
            {
                return _ThanhTichNCKH;
            }
            set
            {
                SetPropertyValue("ThanhTichNCKH", ref _ThanhTichNCKH, value);
            }
        }

        [ModelDefault("Caption", "Thành tích khác")]
        public string ThanhTichKhac
        {
            get
            {
                return _ThanhTichKhac;
            }
            set
            {
                SetPropertyValue("ThanhTichKhac", ref _ThanhTichKhac, value);
            }
        }

        [ModelDefault("Caption", "Loại vi phạm")]
        public HinhThucViPham HinhThucViPham
        {
            get
            {
                return _HinhThucViPham;
            }
            set
            {
                SetPropertyValue("HinhThucViPham", ref _HinhThucViPham, value);
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

        public ChiTietTapTheDeNghiKhenThuong(Session session) : base(session) { }
    }

}
