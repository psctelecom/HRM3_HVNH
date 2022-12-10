using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.CauHinh
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình quyết định")]
    public class CauHinhQuyetDinh : BaseObject
    {
        // Fields...
        private string _QuyetDinhXepLuong;
        private string _QuyetDinhTuyenDung;
        private string _QuyetDinhTiepNhanVaXepLuong;
        private string _QuyetDinhTiepNhan;
        private string _QuyetDinhThoiViec;
        private string _QuyetDinhThoiChucKiemNhiem;
        private string _QuyetDinhThoiChuc;
        private string _QuyetDinhThoiChucVuDoan;//QNU
        private string _QuyetDinhThayCanBoHuongDanTapSu;
        private string _QuyetDinhThanhLapHoiDongTuyenDung;
        private string _QuyetDinhThanhLapHoiDongKhenThuong;
        private string _QuyetDinhThanhLapHoiDongKyLuat;
        private string _QuyetDinhThanhLapDonVi;
        private string _QuyetDinhTrucLe;
        private string _QuyetDinhTamHoanTapSu;
        private string _QuyetDinhSapNhapDonVi;
        private string _QuyetDinhNghiKhongHuongLuong;
        private string _QuyetDinhNghiHuu;
        private string _QuyetDinhNangPhuCapThamNienNhaGiao;
        private string _QuyetDinhNangPhuCapThamNienHanhChinh;
        private string _QuyetDinhNangNgach;
        private string _QuyetDinhNangLuong;
        private string _QuyetDinhMienNhiemKiemNhiem;
        private string _QuyetDinhMienNhiem;
        private string _QuyetDinhLuanChuyen;
        private string _QuyetDinhKyLuat;
        private string _QuyetDinhKhenThuong;
        private string _QuyetDinhKeoDaiThoiGianCongTac;
        private string _QuyetDinhHuongDanTapSu;
        private string _QuyetDinhHopDong;
        private string _QuyetDinhThinhGiang;
        private string _QuyetDinhGiaHanThinhGiang;

        private string _QuyetDinhGiaiTheDonVi;
        private string _QuyetDinhGiaHanTapSu;
        private string _QuyetDinhGiaHanDaoTao;
        private string _QuyetDinhGiaHanBoiDuong;
        private string _QuyetDinhHuyBoiDuong; //NEU
        private string _QuyetDinhHuyDaoTao;//NEU
        private string _QuyetDinhHuyDiNuocNgoai;//NEU
        private string _QuyetDinhDoiTenDonVi;
        private string _QuyetDinhDiNuocNgoai;
        private string _QuyetDinhDieuChinhThoiGianDiNuocNgoai;//NEU
        private string _QuyetDinhDiCongTac;
        private string _QuyetDinhDaoTao;
        private string _QuyetDinhDenBuChiPhiDaoTao; // QNU
        private string _QuyetDinhCongNhanDaoTao;
        private string _QuyetDinhCongNhanBoiDuong;
        private string _QuyetDinhChuyenNgach;
        private string _QuyetDinhChuyenTruongDaoTao;
        private string _QuyetDinhChuyenTruongBoiDuong;
        private string _QuyetDinhChuyenNganhDaoTaoMoi;
        private string _QuyetDinhChuyenCongTac;
        private string _QuyetDinhChiaTachDonVi;
        private string _QuyetDinhChamDutHopDong;
        private string _QuyetDinhBoNhiemNgach;
        private string _QuyetDinhBoNhiemKiemNhiem;
        private string _QuyetDinhBoNhiem;
        private string _QuyetDinhBoNhiemChucVuDoan; // QNU
        private string _QuyetDinhBoiDuong;
        private string _QuyetDinhTiepNhanVienChucDiNuocNgoai;
        private string _QuyetDinhCongNhanHocHam;
        private string _QuyetDinhThamDuHoiThiTayNgheTre;
        private string _QuyetDinhChiTienThuongTienSi;
        private string _QuyetDinhCongNhanHetHanTapSu;

        private string _QuyetDinhTiepNhanNghiThaiSan;
        private string _QuyetDinhThamGiaKhoaHocNghiepVu;
        private string _QuyetDinhThanhLapHoiDongNangLuong;

        private string _QuyetDinhHuongPhuCapTrachNhiem;
        private string _QuyetDinhThoiHuongPhuCapTrachNhiem;

        private ThongTinNhanVien _NguoiKyTen;
        private string _GhiChu;

        [ModelDefault("Caption", "Người ký")]
        [DataSourceProperty("NguoiKyTenList", DataSourcePropertyIsNullMode.SelectAll)]
        public ThongTinNhanVien NguoiKyTen
        {
            get
            {
                return _NguoiKyTen;
            }
            set
            {
                SetPropertyValue("NguoiKyTen", ref _NguoiKyTen, value);
            }
        }

        [ModelDefault("Caption", "QĐ bồi dưỡng")]
        public string QuyetDinhBoiDuong
        {
            get
            {
                return _QuyetDinhBoiDuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoiDuong", ref _QuyetDinhBoiDuong, value);
            }
        }

        [ModelDefault("Caption", "QĐ bổ nhiệm")]
        public string QuyetDinhBoNhiem
        {
            get
            {
                return _QuyetDinhBoNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiem", ref _QuyetDinhBoNhiem, value);
            }
        }

        [ModelDefault("Caption", "QĐ bổ nhiệm chức vụ đoàn")]
        public string QuyetDinhBoNhiemChucVuDoan
        {
            get
            {
                return _QuyetDinhBoNhiemChucVuDoan;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiemChucVuDoan", ref _QuyetDinhBoNhiemChucVuDoan, value);
            }
        }

        [ModelDefault("Caption", "QĐ bổ nhiệm kiêm nhiệm")]
        public string QuyetDinhBoNhiemKiemNhiem
        {
            get
            {
                return _QuyetDinhBoNhiemKiemNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiemKiemNhiem", ref _QuyetDinhBoNhiemKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "QĐ bổ nhiệm ngạch")]
        public string QuyetDinhBoNhiemNgach
        {
            get
            {
                return _QuyetDinhBoNhiemNgach;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiemNgach", ref _QuyetDinhBoNhiemNgach, value);
            }
        }

        [ModelDefault("Caption", "QĐ chấm dứt hợp đồng")]
        public string QuyetDinhChamDutHopDong
        {
            get
            {
                return _QuyetDinhChamDutHopDong;
            }
            set
            {
                SetPropertyValue("QuyetDinhChamDutHopDong", ref _QuyetDinhChamDutHopDong, value);
            }
        }

        [ModelDefault("Caption", "QĐ chia tách đơn vị")]
        public string QuyetDinhChiaTachDonVi
        {
            get
            {
                return _QuyetDinhChiaTachDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhChiaTachDonVi", ref _QuyetDinhChiaTachDonVi, value);
            }
        }

        [ModelDefault("Caption", "QĐ chuyển công tác")]
        public string QuyetDinhChuyenCongTac
        {
            get
            {
                return _QuyetDinhChuyenCongTac;
            }
            set
            {
                SetPropertyValue("QuyetDinhChuyenCongTac", ref _QuyetDinhChuyenCongTac, value);
            }
        }

        [ModelDefault("Caption", "QĐ chuyển ngạch")]
        public string QuyetDinhChuyenNgach
        {
            get
            {
                return _QuyetDinhChuyenNgach;
            }
            set
            {
                SetPropertyValue("QuyetDinhChuyenNgach", ref _QuyetDinhChuyenNgach, value);
            }
        }

        [ModelDefault("Caption", "QĐ chuyển trường đào tạo")]
        public string QuyetDinhChuyenTruongDaoTao
        {
            get
            {
                return _QuyetDinhChuyenTruongDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhChuyenTruongDaoTao", ref _QuyetDinhChuyenTruongDaoTao, value);
            }
        }
        [ModelDefault("Caption", "QĐ chuyển trường bồi dưỡng")]
        public string QuyetDinhChuyenTruongBoiDuong
        {
            get
            {
                return _QuyetDinhChuyenTruongBoiDuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhChuyenTruongBoiDuong", ref _QuyetDinhChuyenTruongBoiDuong, value);
            }
        }

        [ModelDefault("Caption", "QĐ chuyển nganh đào tạo mới")]
        public string QuyetDinhChuyenNganhDaoTaoMoi
        {
            get
            {
                return _QuyetDinhChuyenNganhDaoTaoMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhChuyenNganhDaoTaoMoi", ref _QuyetDinhChuyenNganhDaoTaoMoi, value);
            }
        }
        [ModelDefault("Caption", "QĐ công nhận đào tạo")]
        public string QuyetDinhCongNhanDaoTao
        {
            get
            {
                return _QuyetDinhCongNhanDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhCongNhanDaoTao", ref _QuyetDinhCongNhanDaoTao, value);
            }
        }
        [ModelDefault("Caption", "QĐ công nhận bồi dưỡng")]
        public string QuyetDinhCongNhanBoiDuong
        {
            get
            {
                return _QuyetDinhCongNhanBoiDuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhCongNhanBoiDuong", ref _QuyetDinhCongNhanBoiDuong, value);
            }
        }
        [ModelDefault("Caption", "QĐ đền bù chi phí đào tạo")]
        public string QuyetDinhDenBuChiPhiDaoTao
        {
            get
            {
                return _QuyetDinhDenBuChiPhiDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhDenBuChiPhiDaoTao", ref _QuyetDinhDenBuChiPhiDaoTao, value);
            }
        }
        [ModelDefault("Caption", "QĐ thỉnh giảng")]
        public string QuyetDinhThinhGiang
        {
            get
            {
                return _QuyetDinhThinhGiang;
            }
            set
            {
                SetPropertyValue("QuyetDinhThinhGiang", ref _QuyetDinhThinhGiang, value);
            }
        }
        [ModelDefault("Caption", "QĐ gia hạn thinh giảng")]
        public string QuyetDinhGiaHanThinhGiang
        {
            get
            {
                return _QuyetDinhGiaHanThinhGiang;
            }
            set
            {
                SetPropertyValue("QuyetDinhGiaHanThinhGiang", ref _QuyetDinhGiaHanThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "QĐ chi tiền tiến sĩ")]
        public string QuyetDinhChiTienThuongTienSi
        {
            get
            {
                return _QuyetDinhChiTienThuongTienSi;
            }
            set
            {
                SetPropertyValue("QuyetDinhChiTienThuongTienSi", ref _QuyetDinhChiTienThuongTienSi, value);
            }
        }

        [ModelDefault("Caption", "QĐ đào tạo")]
        public string QuyetDinhDaoTao
        {
            get
            {
                return _QuyetDinhDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhDaoTao", ref _QuyetDinhDaoTao, value);
            }
        }

        [ModelDefault("Caption", "QĐ đi công tác")]
        public string QuyetDinhDiCongTac
        {
            get
            {
                return _QuyetDinhDiCongTac;
            }
            set
            {
                SetPropertyValue("QuyetDinhDiCongTac", ref _QuyetDinhDiCongTac, value);
            }
        }

        [ModelDefault("Caption", "QĐ đi nước ngoài")]
        public string QuyetDinhDiNuocNgoai
        {
            get
            {
                return _QuyetDinhDiNuocNgoai;
            }
            set
            {
                SetPropertyValue("QuyetDinhDiNuocNgoai", ref _QuyetDinhDiNuocNgoai, value);
            }
        }
        [ModelDefault("Caption", "QĐ điều chỉnh thời gian đi nước ngoài")]
        public string QuyetDinhDieuChinhThoiGianDiNuocNgoai
        {
            get
            {
                return _QuyetDinhDieuChinhThoiGianDiNuocNgoai;
            }
            set
            {
                SetPropertyValue("QuyetDinhDieuChinhThoiGianDiNuocNgoai", ref _QuyetDinhDieuChinhThoiGianDiNuocNgoai, value);
            }
        }

        [ModelDefault("Caption", "QĐ đổi tên đơn vị")]
        public string QuyetDinhDoiTenDonVi
        {
            get
            {
                return _QuyetDinhDoiTenDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhDoiTenDonVi", ref _QuyetDinhDoiTenDonVi, value);
            }
        }

        [ModelDefault("Caption", "QĐ gia hạn đào tạo")]
        public string QuyetDinhGiaHanDaoTao
        {
            get
            {
                return _QuyetDinhGiaHanDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhGiaHanDaoTao", ref _QuyetDinhGiaHanDaoTao, value);
            }
        }
        [ModelDefault("Caption", "QĐ hủy đào tạo")]
        public string QuyetDinhHuyDaoTao
        {
            get
            {
                return _QuyetDinhHuyDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuyDaoTao", ref _QuyetDinhHuyDaoTao, value);
            }
        }
        [ModelDefault("Caption", "QĐ hủy đi nước ngoài")]
        public string QuyetDinhHuyDiNuocNgoai
        {
            get
            {
                return _QuyetDinhHuyDiNuocNgoai;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuyDiNuocNgoai", ref _QuyetDinhHuyDiNuocNgoai, value);
            }
        }
        [ModelDefault("Caption", "QĐ hủy bồi dưỡng")]
        public string QuyetDinhHuyBoiDuong
        {
            get
            {
                return _QuyetDinhHuyBoiDuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuyBoiDuong", ref _QuyetDinhHuyBoiDuong, value);
            }
        }
        [ModelDefault("Caption", "QĐ gia hạn bồi dưỡng")]
        public string QuyetDinhGiaHanBoiDuong
        {
            get
            {
                return _QuyetDinhGiaHanBoiDuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhGiaHanBoiDuong", ref _QuyetDinhGiaHanBoiDuong, value);
            }
        }

        [ModelDefault("Caption", "QĐ gia hạn tập sự")]
        public string QuyetDinhGiaHanTapSu
        {
            get
            {
                return _QuyetDinhGiaHanTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhGiaHanTapSu", ref _QuyetDinhGiaHanTapSu, value);
            }
        }

        [ModelDefault("Caption", "QĐ giải thể đơn vị")]
        public string QuyetDinhGiaiTheDonVi
        {
            get
            {
                return _QuyetDinhGiaiTheDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhGiaiTheDonVi", ref _QuyetDinhGiaiTheDonVi, value);
            }
        }

        [ModelDefault("Caption", "QĐ hợp đồng")]
        public string QuyetDinhHopDong
        {
            get
            {
                return _QuyetDinhHopDong;
            }
            set
            {
                SetPropertyValue("QuyetDinhHopDong", ref _QuyetDinhHopDong, value);
            }
        }
        [ModelDefault("Caption", "QĐ thỉnh giảng")]
     

        [ModelDefault("Caption", "QĐ hướng dẫn tập sự")]
        public string QuyetDinhHuongDanTapSu
        {
            get
            {
                return _QuyetDinhHuongDanTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuongDanTapSu", ref _QuyetDinhHuongDanTapSu, value);
            }
        }

        [ModelDefault("Caption", "QĐ kéo dài thời gian công tác")]
        public string QuyetDinhKeoDaiThoiGianCongTac
        {
            get
            {
                return _QuyetDinhKeoDaiThoiGianCongTac;
            }
            set
            {
                SetPropertyValue("QuyetDinhKeoDaiThoiGianCongTac", ref _QuyetDinhKeoDaiThoiGianCongTac, value);
            }
        }

        [ModelDefault("Caption", "QĐ khen thưởng")]
        public string QuyetDinhKhenThuong
        {
            get
            {
                return _QuyetDinhKhenThuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhKhenThuong", ref _QuyetDinhKhenThuong, value);
            }
        }

        [ModelDefault("Caption", "QĐ kỷ luật")]
        public string QuyetDinhKyLuat
        {
            get
            {
                return _QuyetDinhKyLuat;
            }
            set
            {
                SetPropertyValue("QuyetDinhKyLuat", ref _QuyetDinhKyLuat, value);
            }
        }

        [ModelDefault("Caption", "QĐ luân chuyển")]
        public string QuyetDinhLuanChuyen
        {
            get
            {
                return _QuyetDinhLuanChuyen;
            }
            set
            {
                SetPropertyValue("QuyetDinhLuanChuyen", ref _QuyetDinhLuanChuyen, value);
            }
        }

        [ModelDefault("Caption", "QĐ miễn nhiệm")]
        public string QuyetDinhMienNhiem
        {
            get
            {
                return _QuyetDinhMienNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhMienNhiem", ref _QuyetDinhMienNhiem, value);
            }
        }

        [ModelDefault("Caption", "QĐ miễn nhiệm kiêm nhiệm")]
        public string QuyetDinhMienNhiemKiemNhiem
        {
            get
            {
                return _QuyetDinhMienNhiemKiemNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhMienNhiemKiemNhiem", ref _QuyetDinhMienNhiemKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "QĐ nâng lương")]
        public string QuyetDinhNangLuong
        {
            get
            {
                return _QuyetDinhNangLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangLuong", ref _QuyetDinhNangLuong, value);
            }
        }

        [ModelDefault("Caption", "QĐ nâng ngạch")]
        public string QuyetDinhNangNgach
        {
            get
            {
                return _QuyetDinhNangNgach;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangNgach", ref _QuyetDinhNangNgach, value);
            }
        }

        [ModelDefault("Caption", "QĐ nâng phụ cấp thâm niên nhà giáo")]
        public string QuyetDinhNangPhuCapThamNienNhaGiao
        {
            get
            {
                return _QuyetDinhNangPhuCapThamNienNhaGiao;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangPhuCapThamNienNhaGiao", ref _QuyetDinhNangPhuCapThamNienNhaGiao, value);
            }
        }

        [ModelDefault("Caption", "QĐ nâng phụ cấp thâm niên hành chính")]
        public string QuyetDinhNangPhuCapThamNienHanhChinh
        {
            get
            {
                return _QuyetDinhNangPhuCapThamNienHanhChinh;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangPhuCapThamNienHanhChinh", ref _QuyetDinhNangPhuCapThamNienHanhChinh, value);
            }
        }

        [ModelDefault("Caption", "QĐ nghỉ hưu")]
        public string QuyetDinhNghiHuu
        {
            get
            {
                return _QuyetDinhNghiHuu;
            }
            set
            {
                SetPropertyValue("QuyetDinhNghiHuu", ref _QuyetDinhNghiHuu, value);
            }
        }

        [ModelDefault("Caption", "QĐ nghỉ không hưởng lương")]
        public string QuyetDinhNghiKhongHuongLuong
        {
            get
            {
                return _QuyetDinhNghiKhongHuongLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhNghiKhongHuongLuong", ref _QuyetDinhNghiKhongHuongLuong, value);
            }
        }

        [ModelDefault("Caption", "QĐ sáp nhập đơn vị")]
        public string QuyetDinhSapNhapDonVi
        {
            get
            {
                return _QuyetDinhSapNhapDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhSapNhapDonVi", ref _QuyetDinhSapNhapDonVi, value);
            }
        }

        [ModelDefault("Caption", "QĐ tạm hoãn tập sự")]
        public string QuyetDinhTamHoanTapSu
        {
            get
            {
                return _QuyetDinhTamHoanTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhTamHoanTapSu", ref _QuyetDinhTamHoanTapSu, value);
            }
        }

        [ModelDefault("Caption", "QĐ thành lập đơn vị")]
        public string QuyetDinhThanhLapDonVi
        {
            get
            {
                return _QuyetDinhThanhLapDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapDonVi", ref _QuyetDinhThanhLapDonVi, value);
            }
        }

        [ModelDefault("Caption", "QĐ thành lập hội đồng khen thưởng")]
        public string QuyetDinhThanhLapHoiDongKhenThuong
        {
            get
            {
                return _QuyetDinhThanhLapHoiDongKhenThuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapHoiDongKhenThuong", ref _QuyetDinhThanhLapHoiDongKhenThuong, value);
            }
        }

        [ModelDefault("Caption", "QĐ thành lập hội đồng kỷ luật")]
        public string QuyetDinhThanhLapHoiDongKyLuat
        {
            get
            {
                return _QuyetDinhThanhLapHoiDongKyLuat;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapHoiDongKyLuat", ref _QuyetDinhThanhLapHoiDongKyLuat, value);
            }
        }
        [ModelDefault("Caption", "QĐ trực lễ")]
        public string QuyetDinhTrucLe
        {
            get
            {
                return _QuyetDinhTrucLe;
            }
            set
            {
                SetPropertyValue("QuyetDinhTrucLe", ref _QuyetDinhTrucLe, value);
            }
        }
        
        [ModelDefault("Caption", "QĐ thành lập hội đồng tuyển dụng")]
        public string QuyetDinhThanhLapHoiDongTuyenDung
        {
            get
            {
                return _QuyetDinhThanhLapHoiDongTuyenDung;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapHoiDongTuyenDung", ref _QuyetDinhThanhLapHoiDongTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "QĐ thay cán bộ hướng dẫn tập sự")]
        public string QuyetDinhThayCanBoHuongDanTapSu
        {
            get
            {
                return _QuyetDinhThayCanBoHuongDanTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhThayCanBoHuongDanTapSu", ref _QuyetDinhThayCanBoHuongDanTapSu, value);
            }
        }

        [ModelDefault("Caption", "QĐ thôi chức")]
        public string QuyetDinhThoiChuc
        {
            get
            {
                return _QuyetDinhThoiChuc;
            }
            set
            {
                SetPropertyValue("QuyetDinhThoiChuc", ref _QuyetDinhThoiChuc, value);
            }
        }
        [ModelDefault("Caption", "QĐ thôi chức vụ đoàn")]
        public string QuyetDinhThoiChucVuDoan
        {
            get
            {
                return _QuyetDinhThoiChucVuDoan;
            }
            set
            {
                SetPropertyValue("QuyetDinhThoiChucVuDoan", ref _QuyetDinhThoiChucVuDoan, value);
            }
        }

        [ModelDefault("Caption", "QĐ thôi chức kiêm nhiệm")]
        public string QuyetDinhThoiChucKiemNhiem
        {
            get
            {
                return _QuyetDinhThoiChucKiemNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhThoiChucKiemNhiem", ref _QuyetDinhThoiChucKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "QĐ thôi việc")]
        public string QuyetDinhThoiViec
        {
            get
            {
                return _QuyetDinhThoiViec;
            }
            set
            {
                SetPropertyValue("QuyetDinhThoiViec", ref _QuyetDinhThoiViec, value);
            }
        }

        [ModelDefault("Caption", "QĐ tiếp nhận")]
        public string QuyetDinhTiepNhan
        {
            get
            {
                return _QuyetDinhTiepNhan;
            }
            set
            {
                SetPropertyValue("QuyetDinhTiepNhan", ref _QuyetDinhTiepNhan, value);
            }
        }

        [ModelDefault("Caption", "QĐ tiếp nhận và xếp lương")]
        public string QuyetDinhTiepNhanVaXepLuong
        {
            get
            {
                return _QuyetDinhTiepNhanVaXepLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhTiepNhanVaXepLuong", ref _QuyetDinhTiepNhanVaXepLuong, value);
            }
        }

        [ModelDefault("Caption", "QĐ tuyển dụng")]
        public string QuyetDinhTuyenDung
        {
            get
            {
                return _QuyetDinhTuyenDung;
            }
            set
            {
                SetPropertyValue("QuyetDinhTuyenDung", ref _QuyetDinhTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "QĐ xếp lương")]
        public string QuyetDinhXepLuong
        {
            get
            {
                return _QuyetDinhXepLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhXepLuong", ref _QuyetDinhXepLuong, value);
            }
        }

        [ModelDefault("Caption", "QĐ tiếp nhận viên chức đi nước ngoài")]
        public string QuyetDinhTiepNhanVienChucDiNuocNgoai
        {
            get
            {
                return _QuyetDinhTiepNhanVienChucDiNuocNgoai;
            }
            set
            {
                SetPropertyValue("QuyetDinhTiepNhanVienChucDiNuocNgoai", ref _QuyetDinhTiepNhanVienChucDiNuocNgoai, value);
            }
        }

        [ModelDefault("Caption", "QĐ công nhận học hàm")]
        public string QuyetDinhCongNhanHocHam
        {
            get
            {
                return _QuyetDinhCongNhanHocHam;
            }
            set
            {
                SetPropertyValue("QuyetDinhCongNhanHocHam", ref _QuyetDinhCongNhanHocHam, value);
            }
        }

        [ModelDefault("Caption", "QĐ tham dự hội thi tay nghề trẻ")]
        public string QuyetDinhThamDuHoiThiTayNgheTre
        {
            get
            {
                return _QuyetDinhThamDuHoiThiTayNgheTre;
            }
            set
            {
                SetPropertyValue("QuyetDinhThamDuHoiThiTayNgheTre", ref _QuyetDinhThamDuHoiThiTayNgheTre, value);
            }
        }

        [ModelDefault("Caption", "QĐ công nhận hết hạn tập sự")]
        public string QuyetDinhCongNhanHetHanTapSu
        {
            get
            {
                return _QuyetDinhCongNhanHetHanTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhCongNhanHetHanTapSu", ref _QuyetDinhCongNhanHetHanTapSu, value);
            }
        }

        [ModelDefault("Caption", "QĐ tiếp nhận nghỉ thai sản")]
        public string QuyetDinhTiepNhanNghiThaiSan
        {
            get
            {
                return _QuyetDinhTiepNhanNghiThaiSan;
            }
            set
            {
                SetPropertyValue("QuyetDinhTiepNhanNghiThaiSan", ref _QuyetDinhTiepNhanNghiThaiSan, value);
            }
        }

        [ModelDefault("Caption", "QĐ tham gia khóa học nghiệp vụ")]
        public string QuyetDinhThamGiaKhoaHocNghiepVu
        {
            get
            {
                return _QuyetDinhThamGiaKhoaHocNghiepVu;
            }
            set
            {
                SetPropertyValue("QuyetDinhThamGiaKhoaHocNghiepVu", ref _QuyetDinhThamGiaKhoaHocNghiepVu, value);
            }
        }

        [ModelDefault("Caption", "QĐ thành lập hội đồng nâng lương")]
        public string QuyetDinhThanhLapHoiDongNangLuong
        {
            get
            {
                return _QuyetDinhThanhLapHoiDongNangLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapHoiDongNangLuong", ref _QuyetDinhThanhLapHoiDongNangLuong, value);
            }
        }

        [ModelDefault("Caption", "QĐ Hưởng phụ cấp trách nhiệm")]
        public string QuyetDinhHuongPhuCapTrachNhiem
        {
            get
            {
                return _QuyetDinhHuongPhuCapTrachNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuongPhuCapTrachNhiem", ref _QuyetDinhHuongPhuCapTrachNhiem, value);
            }
        }

        [ModelDefault("Caption", "QĐ Thôi hưởng phụ cấp trách nhiệm")]
        public string QuyetDinhThoiHuongPhuCapTrachNhiem
        {
            get
            {
                return _QuyetDinhThoiHuongPhuCapTrachNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhThoiHuongPhuCapTrachNhiem", ref _QuyetDinhThoiHuongPhuCapTrachNhiem, value);
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

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyTenList { get; set; }


        public void UpdateNguoiKyTenList()
        {
            if (NguoiKyTenList == null)
                NguoiKyTenList = new XPCollection<ThongTinNhanVien>(Session);
            NguoiKyTenList.Criteria = CriteriaOperator.Parse("ChucVu.TenChucVu like ? or ChucVu.TenChucVu like ?", "%Hiệu trưởng%", "%Phó hiệu trưởng%");
        }

        public CauHinhQuyetDinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            QuyetDinhBoiDuong = "cử viên chức đi bồi dưỡng";
            QuyetDinhBoNhiem = "bổ nhiệm chức vụ viên chức";
            QuyetDinhBoNhiemKiemNhiem = "bổ nhiệm chức vụ viên chức";
            QuyetDinhBoNhiemNgach = "bổ nhiệm ngạch viên chức";
            QuyetDinhChamDutHopDong = "chấm dứt hợp đồng";
            QuyetDinhChiaTachDonVi = "chia tách đơn vị";
            QuyetDinhChuyenCongTac = "chuyển công tác viên chức";
            QuyetDinhChuyenNgach = "chuyển ngạch viên chức";
            QuyetDinhChuyenTruongDaoTao = "chuyển trường đào tạo viên chức";
            QuyetDinhChuyenTruongBoiDuong = "chuyển trường bồi dưỡng viên chức";
            QuyetDinhCongNhanDaoTao = "công nhận đào tạo viên chức";
            QuyetDinhCongNhanBoiDuong = "công nhận bồi dưỡng viên chức";
            QuyetDinhDaoTao = "đào tạo viên chức";
            QuyetDinhDenBuChiPhiDaoTao = "đền bù chi phí đào tạo của viên chức";
            QuyetDinhDiCongTac = "cử viên chức đi công tác";
            QuyetDinhDiNuocNgoai = "cử viên chức đi nước ngoài";
            QuyetDinhDieuChinhThoiGianDiNuocNgoai = "điều chỉnh thời gian đi nước ngoài";
            QuyetDinhDoiTenDonVi = "đổi tên đơn vị";
            QuyetDinhGiaHanDaoTao = "gia hạn thời gian đào tạo";
            QuyetDinhGiaHanBoiDuong = "gia hạn thời gian bồi dưỡng";
            QuyetDinhGiaHanTapSu = "gia hạn thời gian tập sự";
            QuyetDinhGiaiTheDonVi = "giải thể đơn vị";
            QuyetDinhHopDong = "hợp đồng viên chức";
            QuyetDinhThinhGiang = "quyết định thỉnh giảng";
            QuyetDinhGiaHanThinhGiang = "quyết định  gia hạn thỉnh giảng";
            QuyetDinhTrucLe = "cử công chức, viên chức và nhân viên trực bảo vệ Nhà trường";
            QuyetDinhHuyBoiDuong = "hủy Quyết định cử viên chức đi đào tạo, bồi dưỡng ngắn hạn";

   
            QuyetDinhHuongDanTapSu = "hướng dẫn tập sự";
            QuyetDinhKeoDaiThoiGianCongTac = "kéo dài thời gian công tác";
            QuyetDinhKhenThuong = "khen thưởng";
            QuyetDinhKyLuat = "kỷ luật viên chức";
            QuyetDinhLuanChuyen = "điều động viên chức";
            QuyetDinhMienNhiem = "miễn nhiệm chức vụ";
            QuyetDinhMienNhiemKiemNhiem = "miễn nhiệm chức vụ";
            QuyetDinhNangLuong = "nâng lương viên chức";
            QuyetDinhNangNgach = "nâng ngạch viên chức";
            QuyetDinhNangPhuCapThamNienNhaGiao = "nâng phụ cấp thâm niên đối với nhà giáo";
            QuyetDinhNghiHuu = "cho nghỉ việc để hưởng chế độ BHXH";
            QuyetDinhNghiKhongHuongLuong = "viên chức nghỉ không hưởng lương";
            QuyetDinhSapNhapDonVi = "sáp nhập đơn vị";
            QuyetDinhTamHoanTapSu = "tạm hoãn tập sự";
            QuyetDinhThanhLapDonVi = "thành lập đơn vị";
            QuyetDinhThanhLapHoiDongKhenThuong = "thành lập hội đồng khen thưởng";
            QuyetDinhThanhLapHoiDongKyLuat = "thành lập hội đồng kỷ luật";
            QuyetDinhThanhLapHoiDongTuyenDung = "thành lập hội đồng tuyển dụng";
            QuyetDinhThayCanBoHuongDanTapSu = "thay viên chức hướng dẫn tập sự";
            QuyetDinhThoiChuc = "thôi chức vụ viên chức quản lý";
            QuyetDinhThoiChucKiemNhiem = "thôi chức vụ viên chức quản lý";
            QuyetDinhThoiViec = "thôi việc viên chức";
            QuyetDinhTiepNhan = "tiếp nhận viên chức";
            QuyetDinhTiepNhanVaXepLuong = "tiếp nhận và xếp lương viên chức";
            QuyetDinhTuyenDung = "tuyển dụng viên chức";
            QuyetDinhXepLuong = "xếp lương viên chức";
            QuyetDinhTiepNhanVienChucDiNuocNgoai = "tiếp nhận viên chức đi nước ngoài";
            QuyetDinhCongNhanHocHam = "công nhận học hàm";
            QuyetDinhThamDuHoiThiTayNgheTre = "cử Đoàn cán bộ, thí sinh tham dự Hội thi Tay nghề trẻ thành phố Hồ Chí Minh";
            QuyetDinhCongNhanHetHanTapSu = "công nhận hết hạn tập sự";
            QuyetDinhNangPhuCapThamNienHanhChinh = "nâng phụ cấp thâm niên hành chính";


            QuyetDinhHuongPhuCapTrachNhiem = "hưởng phụ cấp trách nhiệm công việc";
            QuyetDinhThoiHuongPhuCapTrachNhiem = "thôi hưởng phụ cấp trách nhiệm công việc";
            QuyetDinhThanhLapHoiDongNangLuong = "Thành lập Hội đồng xét nâng bậc lương";
            //
            UpdateNguoiKyTenList();
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNguoiKyTenList();
        }

      
    }

}
