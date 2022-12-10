using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Khóa luận tốt nghiệp")]
    [ModelDefault("AllowNew", "false")]
    public class KhoaLuanTotNghiep : BaseObject
    {

        #region Khai báo

        #region Hoạt động
        private NhanVien _NhanVien;
        private KhoiLuongGiangDay _KhoiLuongGiangDay;
        #endregion
        #region Kết quả
        /*	Họ tên GV, Ngành, Loại hoạt động(phản biện, hướng dẫn, chấm đồ án), phần tram hướng dẫn*/
        private BoPhan _KhoaQuanLy;
        //
        private int _KTXD10;
        private int _KTXD20;
        private int _KTXD30;
        private int _KTXD40;
        private int _KTXD50;
        private int _KTXD60;
        private int _KTXD70;
        private int _KTXD80;
        private int _KTXD90;
        private int _KTXD100;
        private int _NganhKhac;
        private int _PhanBien;
        private int _ChamBai;
        //

        private int _SLSVHuongDanThucTapTongHop;
        private int _SLSVChamBaoCaoThucTapTongHop;
        private int _SLSVHuongDanThucTapTotNghiep;
        private int _SLSVChamChuyuenDeThucTapTotNghiep;
        //
        private decimal _QD10KTXD;
        private decimal _QD20KTXD;
        private decimal _QD30KTXD;
        private decimal _QD40KTXD;
        private decimal _QD50KTXD;
        private decimal _QD60KTXD;
        private decimal _QD70KTXD;
        private decimal _QD80KTXD;
        private decimal _QD90KTXD;
        private decimal _QD100KTXD;
        private decimal _QDNganhKhac;
        private decimal _QDPhanBien;
        private decimal _QDChamBai;
        //
        private decimal _QDSVHuongDanThucTapTongHop;
        private decimal _QDSVChamBaoCaoThucTapTongHop;
        private decimal _QDSVHuongDanThucTapTotNghiep;
        private decimal _QDSVChamChuyuenDeThucTapTotNghiep;
        //
        private decimal _TongGio;
        #endregion
        #endregion



        #region Hoạt động
        [ModelDefault("Caption", "Nhân viên")]
        [Association("KhoiLuongGiangDay-ListKhoaLuanTotNghiep")]
        [Browsable(false)]
        public KhoiLuongGiangDay KhoiLuongGiangDay
        {
            get { return _KhoiLuongGiangDay; }
            set { SetPropertyValue("KhoiLuongGiangDay", ref _KhoiLuongGiangDay, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Khoa quản lý")]
        public BoPhan KhoaQuanLy
        {
            get { return _KhoaQuanLy; }
            set { SetPropertyValue("KhoaQuanLy", ref _KhoaQuanLy, value); }
        }
        [ModelDefault("Caption", "KTXD 10%")]
        public int KTXD10
        {
            get { return _KTXD10; }
            set { SetPropertyValue("KTXD10", ref _KTXD10, value); }
        }
        [ModelDefault("Caption", "KTXD 20%")]
        public int KTXD20
        {
            get { return _KTXD20; }
            set { SetPropertyValue("KTXD20", ref _KTXD20, value); }
        }
        [ModelDefault("Caption", "KTXD 30%")]
        public int KTXD30
        {
            get { return _KTXD30; }
            set { SetPropertyValue("KTXD30", ref _KTXD30, value); }
        }
        [ModelDefault("Caption", "KTXD 40%")]
        public int KTXD40
        {
            get { return _KTXD40; }
            set { SetPropertyValue("KTXD40", ref _KTXD40, value); }
        }
        [ModelDefault("Caption", "KTXD 50%")]
        public int KTXD50
        {
            get { return _KTXD50; }
            set { SetPropertyValue("KTXD50", ref _KTXD50, value); }
        }

        [ModelDefault("Caption", "KTXD 60%")]
        public int KTXD60
        {
            get { return _KTXD60; }
            set { SetPropertyValue("KTXD60", ref _KTXD60, value); }
        }

        [ModelDefault("Caption", "KTXD 70%")]
        public int KTXD70
        {
            get { return _KTXD70; }
            set { SetPropertyValue("KTXD70", ref _KTXD70, value); }
        }

        [ModelDefault("Caption", "KTXD 80%")]
        public int KTXD80
        {
            get { return _KTXD80; }
            set { SetPropertyValue("KTXD80", ref _KTXD80, value); }
        }

        [ModelDefault("Caption", "KTXD 90%")]
        public int KTXD90
        {
            get { return _KTXD90; }
            set { SetPropertyValue("KTXD90", ref _KTXD90, value); }
        }
        [ModelDefault("Caption", "KTXD 100%")]
        public int KTXD100
        {
            get { return _KTXD100; }
            set { SetPropertyValue("KTXD100", ref _KTXD100, value); }
        }

        [ModelDefault("Caption", "Ngành khác")]
        public int NganhKhac
        {
            get { return _NganhKhac; }
            set { SetPropertyValue("NganhKhac", ref _NganhKhac, value); }
        }
        [ModelDefault("Caption", "Phản biện")]
        public int PhanBien
        {
            get { return _PhanBien; }
            set { SetPropertyValue("PhanBien", ref _PhanBien, value); }
        }
        [ModelDefault("Caption", "Chấm bài")]
        public int ChamBai
        {
            get { return _ChamBai; }
            set { SetPropertyValue("ChamBai", ref _ChamBai, value); }
        }

        [ModelDefault("Caption", "SLSV Hướng dẫn TTTH")]
        public int SLSVHuongDanThucTapTongHop
        {
            get { return _SLSVHuongDanThucTapTongHop; }
            set { SetPropertyValue("SLSVHuongDanThucTapTongHop", ref _SLSVHuongDanThucTapTongHop, value); }
        }

        [ModelDefault("Caption", "SLSV Chấm báo cáo TTTH")]
        public int SLSVChamBaoCaoThucTapTongHop
        {
            get { return _SLSVChamBaoCaoThucTapTongHop; }
            set { SetPropertyValue("SLSVChamBaoCaoThucTapTongHop", ref _SLSVChamBaoCaoThucTapTongHop, value); }
        }

        [ModelDefault("Caption", "SLSV Hướng dẫn TTTN")]
        public int SLSVHuongDanThucTapTotNghiep
        {
            get { return _SLSVHuongDanThucTapTotNghiep; }
            set { SetPropertyValue("SLSVHuongDanThucTapTotNghiep", ref _SLSVHuongDanThucTapTotNghiep, value); }
        }

        [ModelDefault("Caption", "SLSV Chấm chuyên đề TTTN")]
        public int SLSVChamChuyuenDeThucTapTotNghiep
        {
            get { return _SLSVChamChuyuenDeThucTapTotNghiep; }
            set { SetPropertyValue("SLSVChamChuyuenDeThucTapTotNghiep", ref _SLSVChamChuyuenDeThucTapTotNghiep, value); }
        }
        //
        [ModelDefault("Caption", "QĐ KTXD 10%")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QD10KTXD
        {
            get { return _QD10KTXD; }
            set { SetPropertyValue("QD10KTXD", ref _QD10KTXD, value); }
        }
        [ModelDefault("Caption", "QĐ KTXD 20%")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QD20KTXD
        {
            get { return _QD20KTXD; }
            set { SetPropertyValue("QD20KTXD", ref _QD20KTXD, value); }
        }
        [ModelDefault("Caption", "QĐ KTXD 30%")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QD30KTXD
        {
            get { return _QD30KTXD; }
            set { SetPropertyValue("QD30KTXD", ref _QD30KTXD, value); }
        }
        [ModelDefault("Caption", "QĐ KTXD 40%")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QD40KTXD
        {
            get { return _QD40KTXD; }
            set { SetPropertyValue("QD40KTXD", ref _QD40KTXD, value); }
        }
        [ModelDefault("Caption", "QĐ KTXD 50%")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QD50KTXD
        {
            get { return _QD50KTXD; }
            set { SetPropertyValue("_QD50KTXD", ref _QD50KTXD, value); }
        }
        [ModelDefault("Caption", "QĐ KTXD 60%")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QD60KTXD
        {
            get { return _QD60KTXD; }
            set { SetPropertyValue("QD60KTXD", ref _QD60KTXD, value); }
        }
        [ModelDefault("Caption", "QĐ KTXD 70%")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QD70KTXD
        {
            get { return _QD70KTXD; }
            set { SetPropertyValue("QD70KTXD", ref _QD70KTXD, value); }
        }
        [ModelDefault("Caption", "QĐ KTXD 80%")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QD80KTXD
        {
            get { return _QD80KTXD; }
            set { SetPropertyValue("QD80KTXD", ref _QD80KTXD, value); }
        }
        [ModelDefault("Caption", "QĐ KTXD 90%")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QD90KTXD
        {
            get { return _QD90KTXD; }
            set { SetPropertyValue("QD90KTXD", ref _QD90KTXD, value); }
        }
        [ModelDefault("Caption", "QĐ KTXD 100%")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QD100KTXD
        {
            get { return _QD100KTXD; }
            set { SetPropertyValue("QD100KTXD", ref _QD100KTXD, value); }
        }
        [ModelDefault("Caption", "QĐ Ngành khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QDNganhKhac
        {
            get { return _QDNganhKhac; }
            set { SetPropertyValue("QDNganhKhac", ref _QDNganhKhac, value); }
        }
        [ModelDefault("Caption", "QĐ Phản biện")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QDPhanBien
        {
            get { return _QDPhanBien; }
            set { SetPropertyValue("QDPhanBien", ref _QDPhanBien, value); }
        }
        [ModelDefault("Caption", "QĐ Chấm bài")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QDChamBai
        {
            get { return _QDChamBai; }
            set { SetPropertyValue("QDChamBai", ref _QDChamBai, value); }
        }
        [ModelDefault("Caption", "QĐ Hướng dẫn TTTH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QDSVHuongDanThucTapTongHop
        {
            get { return _QDSVHuongDanThucTapTongHop; }
            set { SetPropertyValue("QDSVHuongDanThucTapTongHop", ref _QDSVHuongDanThucTapTongHop, value); }
        }
        [ModelDefault("Caption", "QĐ Chấm báo cáo TTTH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QDSVChamBaoCaoThucTapTongHop
        {
            get { return _QDSVChamBaoCaoThucTapTongHop; }
            set { SetPropertyValue("QDSVChamBaoCaoThucTapTongHop", ref _QDSVChamBaoCaoThucTapTongHop, value); }
        }
        [ModelDefault("Caption", "QĐ Hướng dẫn TTTN")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QDSVHuongDanThucTapTotNghiep
        {
            get { return _QDSVHuongDanThucTapTotNghiep; }
            set { SetPropertyValue("QDSVHuongDanThucTapTotNghiep", ref _QDSVHuongDanThucTapTotNghiep, value); }
        }
        [ModelDefault("Caption", "QĐ Chấm chuyên đề TTTN")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal QDSVChamChuyuenDeThucTapTotNghiep
        {
            get { return _QDSVChamChuyuenDeThucTapTotNghiep; }
            set { SetPropertyValue("QDSVChamChuyuenDeThucTapTotNghiep", ref _QDSVChamChuyuenDeThucTapTotNghiep, value); }
        }
        //
        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }
        #endregion
        public KhoaLuanTotNghiep(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}