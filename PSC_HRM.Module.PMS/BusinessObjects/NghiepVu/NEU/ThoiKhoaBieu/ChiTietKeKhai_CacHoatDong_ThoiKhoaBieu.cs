using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.ThoiKhoaBieu
{
    [ModelDefault("Caption", "Chi tiết kê khai các HD khác - thời khóa biểu")]
    public class ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu : BaseObject
    {
        #region Key
        private KeKhai_CacHoatDong_ThoiKhoaBieu _KeKhai_CacHoatDong_ThoiKhoaBieu;
        [ModelDefault("Caption", "Thời khóa biểu")]
        [Association("KeKhai_CacHoatDong_ThoiKhoaBieu-ListChiTiet")]
        [Browsable(false)]
        public KeKhai_CacHoatDong_ThoiKhoaBieu KeKhai_CacHoatDong_ThoiKhoaBieu
        {
            get { return _KeKhai_CacHoatDong_ThoiKhoaBieu; }
            set { SetPropertyValue("KeKhai_CacHoatDong_ThoiKhoaBieu", ref _KeKhai_CacHoatDong_ThoiKhoaBieu, value); }
        }
        #endregion

        #region Khai báo Master
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private string _TenMonHoc;
        private string _LopMonHoc;
        private BoPhan _BoMonQuanLy;
        private Guid _OidChiTiet_ThoiKhoaBieu;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        #endregion

        #region ChiTiet

        private int _SoBaiKiemTra;
        private decimal _GioQuyDoiBaiKiemTra;

        private int _SoBaiThi;
        private decimal _GioQuyDoiBaiThi;

        private int _SoBaiTapLon;
        private decimal _GioQuyDoiBaiTapLon;

        private int _SoBaiTieuLuan;
        private decimal _GioQuyDoiBaiTieuLuan;

        private int _SoDeAnTotNghiep;
        private decimal _GioQuyDoiDeAnTotNghiep;

        private int _SoChuyenDeTotNghiep;
        private decimal _GioQuyDoiChuyenDeTotNghiep;

        private int _SoDeRaDe;
        private decimal _GioQuyDoiDeRaDe;

        private int _SoHDKhac;
        private decimal _GioQuyDoiHDKhac;

        private int _SoSlotHoc;
        private decimal _GioQuyDoiSlotHoc;

        private int _SoTraLoiCauHoiTrenHeThongHocTap;
        private decimal _GioQuyDoiTraLoiCauHoiTrenHeThongHocTap;

        private int _SoTruyCapLopHoc;
        private decimal _GioQuyDoiTruyCapLopHoc;

        private LoaiHoatDong _LoaiHuongDan;
        private int _SoLuongHuongDan;
        private decimal _GioQuyDoiHuongDan;

        private decimal _TongGio;
        private bool _XacNhan;

        private int _SLSVGHEP;

        #endregion
        #region Master
        [ModelDefault("Caption","Đơn vị")]
        //[ModelDefault("AllowEdit","False")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Giảng viên")]
        //[ModelDefault("AllowEdit", "False")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Tên môn học")]
        //[ModelDefault("AllowEdit", "False")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Lớp môn học")]
        //[ModelDefault("AllowEdit", "False")]
        public string LopMonHoc
        {
            get { return _LopMonHoc; }
            set { SetPropertyValue("LopMonHoc", ref _LopMonHoc, value); }
        }
        [ModelDefault("Caption", "Bộ môn quản lý")]
        //[ModelDefault("AllowEdit", "False")]
        public BoPhan BoMonQuanLy
        {
            get { return _BoMonQuanLy; }
            set { SetPropertyValue("BoMonQuanLy", ref _BoMonQuanLy, value); }
        }


        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }


        [Browsable(false)]
        public Guid OidChiTiet_ThoiKhoaBieu
        {
            get { return _OidChiTiet_ThoiKhoaBieu; }
            set { SetPropertyValue("OidChiTiet_ThoiKhoaBieu", ref _OidChiTiet_ThoiKhoaBieu, value); }
        }
        #endregion

        #region chitiet
        [ModelDefault("Caption", "Số lương sinh viên ghép")]
        public int SLSVGHEP
        {
            get { return _SLSVGHEP; }
            set { SetPropertyValue("SLSVGHEP", ref _SLSVGHEP, value); }
        }

        [ModelDefault("Caption","Số bài kiểm tra")]
        public int SoBaiKiemTra
        {
            get { return _SoBaiKiemTra; }
            set { SetPropertyValue("SoBaiKiemTra", ref _SoBaiKiemTra, value); }
        }
        [ModelDefault("Caption", "GioQuyDoiBaiKiemTra")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoiBaiKiemTra
        {
            get { return _GioQuyDoiBaiKiemTra; }
            set { SetPropertyValue("GioQuyDoiBaiKiemTra", ref _GioQuyDoiBaiKiemTra, value); }
        }


        [ModelDefault("Caption", "Số bài thi")]
        public int SoBaiThi
        {
            get { return _SoBaiThi; }
            set { SetPropertyValue("SoBaiThi", ref _SoBaiThi, value); }
        }
        [ModelDefault("Caption", "GioQuyDoiBaiThi")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoiBaiThi
        {
            get { return _GioQuyDoiBaiThi; }
            set { SetPropertyValue("GioQuyDoiBaiThi", ref _GioQuyDoiBaiThi, value); }
        }

        [ModelDefault("Caption", "Số bài tập lớn")]
        public int SoBaiTapLon
        {
            get { return _SoBaiTapLon; }
            set { SetPropertyValue("SoBaiTapLon", ref _SoBaiTapLon, value); }
        }
        [ModelDefault("Caption", "GioQuyDoiBaiTapLon")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoiBaiTapLon
        {
            get { return _GioQuyDoiBaiTapLon; }
            set { SetPropertyValue("GioQuyDoiBaiTapLon", ref _GioQuyDoiBaiTapLon, value); }
        }

        [ModelDefault("Caption", "Số bài tiểu luận")]
        public int SoBaiTieuLuan
        {
            get { return _SoBaiTieuLuan; }
            set { SetPropertyValue("SoBaiTieuLuan", ref _SoBaiTieuLuan, value); }
        }
        [ModelDefault("Caption", "GioQuyDoiBaiTieuLuan")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoiBaiTieuLuan
        {
            get { return _GioQuyDoiBaiTieuLuan; }
            set { SetPropertyValue("GioQuyDoiBaiTieuLuan", ref _GioQuyDoiBaiTieuLuan, value); }
        }

        [ModelDefault("Caption", "Số đề án TN")]
        public int SoDeAnTotNghiep
        {
            get { return _SoDeAnTotNghiep; }
            set { SetPropertyValue("SoDeAnTotNghiep", ref _SoDeAnTotNghiep, value); }
        }
        [ModelDefault("Caption", "GioQuyDoiDeAnTotNghiep")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoiDeAnTotNghiep
        {
            get { return _GioQuyDoiDeAnTotNghiep; }
            set { SetPropertyValue("GioQuyDoiDeAnTotNghiep", ref _GioQuyDoiDeAnTotNghiep, value); }
        }


        [ModelDefault("Caption", "Số chuyên đề TN")]
        public int SoChuyenDeTotNghiep
        {
            get { return _SoChuyenDeTotNghiep; }
            set { SetPropertyValue("SoChuyenDeTotNghiep", ref _SoChuyenDeTotNghiep, value); }
        }
        [ModelDefault("Caption", "GioQuyDoiChuyenDeTotNghiep")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoiChuyenDeTotNghiep
        {
            get { return _GioQuyDoiChuyenDeTotNghiep; }
            set { SetPropertyValue("GioQuyDoiChuyenDeTotNghiep", ref _GioQuyDoiChuyenDeTotNghiep, value); }
        }


        [ModelDefault("Caption", "Số đề ra đề")]
        public int SoDeRaDe
        {
            get { return _SoDeRaDe; }
            set { SetPropertyValue("SoDeRaDe", ref _SoDeRaDe, value); }
        }
        [ModelDefault("Caption", "GioQuyDoiDeRaDe")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoiDeRaDe
        {
            get { return _GioQuyDoiDeRaDe; }
            set { SetPropertyValue("GioQuyDoiDeRaDe", ref _GioQuyDoiDeRaDe, value); }
        }


        [ModelDefault("Caption", "Số HD khác")]
        public int SoHDKhac
        {
            get { return _SoHDKhac; }
            set { SetPropertyValue("SoHDKhac", ref _SoHDKhac, value); }
        }
        [ModelDefault("Caption", "GioQuyDoiHDKhac")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoiHDKhac
        {
            get { return _GioQuyDoiHDKhac; }
            set { SetPropertyValue("GioQuyDoiHDKhac", ref _GioQuyDoiHDKhac, value); }
        }

        [ModelDefault("Caption", "Số slot học")]
        public int SoSlotHoc
        {
            get { return _SoSlotHoc; }
            set { SetPropertyValue("SoSlotHoc", ref _SoSlotHoc, value); }
        }
        [ModelDefault("Caption", "GioQuyDoiSlotHoc")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoiSlotHoc
        {
            get { return _GioQuyDoiSlotHoc; }
            set { SetPropertyValue("GioQuyDoiSlotHoc", ref _GioQuyDoiSlotHoc, value); }
        }


        [ModelDefault("Caption", "Số câu trả lời HT học tập")]
        public int SoTraLoiCauHoiTrenHeThongHocTap
        {
            get { return _SoTraLoiCauHoiTrenHeThongHocTap; }
            set { SetPropertyValue("SoTraLoiCauHoiTrenHeThongHocTap", ref _SoTraLoiCauHoiTrenHeThongHocTap, value); }
        }
        [ModelDefault("Caption", "GioQuyDoiTraLoiCauHoiTrenHeThongHocTap")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoiTraLoiCauHoiTrenHeThongHocTap
        {
            get { return _GioQuyDoiTraLoiCauHoiTrenHeThongHocTap; }
            set { SetPropertyValue("GioQuyDoiTraLoiCauHoiTrenHeThongHocTap", ref _GioQuyDoiTraLoiCauHoiTrenHeThongHocTap, value); }
        }


        [ModelDefault("Caption", "Số câu trả lời HT học tập")]
        public int SoTruyCapLopHoc
        {
            get { return _SoTruyCapLopHoc; }
            set { SetPropertyValue("SoTruyCapLopHoc", ref _SoTruyCapLopHoc, value); }
        }
        [ModelDefault("Caption", "GioQuyDoiTruyCapLopHoc")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoiTruyCapLopHoc
        {
            get { return _GioQuyDoiTruyCapLopHoc; }
            set { SetPropertyValue("GioQuyDoiTruyCapLopHoc", ref _GioQuyDoiTruyCapLopHoc, value); }
        }
        [ModelDefault("Caption", "Loại hướng dẫn")]
        public LoaiHoatDong LoaiHuongDan
        {
            get { return _LoaiHuongDan; }
            set { SetPropertyValue("LoaiHuongDan", ref _LoaiHuongDan, value); }
        }
        [ModelDefault("Caption", "Số lượng hướng dẫn")]
        public int SoLuongHuongDan
        {
            get { return _SoLuongHuongDan; }
            set { SetPropertyValue("SoLuongHuongDan", ref _SoLuongHuongDan, value); }
        }
        [ModelDefault("Caption", "GioQuyDoiHuongDan")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public decimal GioQuyDoiHuongDan
        {
            get { return _GioQuyDoiHuongDan; }
            set { SetPropertyValue("GioQuyDoiHuongDan", ref _GioQuyDoiHuongDan, value); }
        }

        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }
        [ModelDefault("Caption", "Xác nhận")]
        public bool XacNhan
        {
            get { return _XacNhan; }
            set { SetPropertyValue("XacNhan", ref _XacNhan, value); }
        }


        #endregion
        public ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}