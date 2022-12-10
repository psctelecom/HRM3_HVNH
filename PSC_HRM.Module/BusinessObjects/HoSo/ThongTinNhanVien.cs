using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.Website;
using PSC_HRM.Module.TaoMaQuanLy;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.DoanDang;
using DevExpress.ExpressApp.Editors;
using System.Data;
using System.Data.SqlClient;

namespace PSC_HRM.Module.HoSo
{
    [DefaultClassOptions]
    [ImageName("BO_Resume")]
    [DefaultProperty("HoTen")]
    [ModelDefault("Caption", "Thông tin cán bộ")]
    [ModelDefault("EditorTypeName", "PSC_HRM.Module.Win.Editors.CustomCategorizedListEditor")]
    [Appearance("ThongTinNhanVien.KhoaHoSo", TargetItems = "*", Enabled = false, Criteria = "KhoaHoSo")]
    [Appearance("ThongTinNhanVien.KhoaHoSo1", TargetItems = "NhanVienThongTinLuong.PhanLoai;NhanVienThongTinLuong.NgachLuong;NhanVienThongTinLuong.NgayBoNhiemNgach;NhanVienThongTinLuong.NgayHuongLuong;NhanVienThongTinLuong.BacLuong;NhanVienThongTinLuong.HeSoLuong;NhanVienThongTinLuong.KhongCuTru;NhanVienThongTinLuong.HSPCChucVu;NhanVienThongTinLuong.NgayHuongHSPCChucVu;NhanVienThongTinLuong.HSPCDocHai;NhanVienThongTinLuong.HSPCTrachNhiem;NhanVienThongTinLuong.HSPCKhuVuc;NhanVienThongTinLuong.HSPCKiemNhiem;NhanVienThongTinLuong.HSPCLuuDong;NhanVienThongTinLuong.HSPCUuDai;NhanVienThongTinLuong.HSPCKhac;NhanVienThongTinLuong.PhuCapUuDai;NhanVienThongTinLuong.PhuCapKhac;NhanVienThongTinLuong.PhuCapDocHai;NhanVienThongTinLuong.PhuCapDacThu;NhanVienThongTinLuong.PhuCapThuHut;NhanVienThongTinLuong.PhuCapDacBiet;NhanVienThongTinLuong.ChenhLechBaoLuuLuong;NhanVienThongTinLuong.VuotKhung;NhanVienThongTinLuong.HSPCVuotKhung;NhanVienThongTinLuong.ThamNien;NhanVienThongTinLuong.HSPCThamNien;NhanVienThongTinLuong.LuongKhoan;NhanVienThongTinLuong.SoNguoiPhuThuoc;NhanVienThongTinLuong.SoThangGiamTru;NhanVienThongTinLuong.MocNangLuong;NhanVienThongTinLuong.Huong85PhanTram;NhanVienThongTinLuong.MaSoThue;NhanVienThongTinLuong.CoQuanThue;NhanVienTrinhDo.TrinhDoVanHoa;NhanVienTrinhDo.TrinhDoChuyenMon;NhanVienTrinhDo.ChuyenMonDaoTao;NhanVienTrinhDo.NganhDaoTao;NhanVienTrinhDo.TruongDaoTao;NhanVienTrinhDo.HinhThucDaoTao;NhanVienTrinhDo.NamTotNghiep;NhanVienTrinhDo.ChuongTrinhHoc;NhanVienTrinhDo.TrinhDoTinHoc;NhanVienTrinhDo.NgoaiNgu;NhanVienTrinhDo.TrinhDoNgoaiNgu;NhanVienTrinhDo.HocHam;NhanVienTrinhDo.NamCongNhanHocHam;NhanVienTrinhDo.DanhHieuCaoNhat;NhanVienTrinhDo.NgayPhongDanhHieu;NhanVienTrinhDo.LyLuanChinhTri;NhanVienTrinhDo.QuanLyGiaoDuc;NhanVienTrinhDo.QuanLyNhaNuoc;NhanVienTrinhDo.QuanLyKinhTe;NhanVienTrinhDo.TrinhDoSuPham", Enabled = false, Criteria = "KhoaHoSo")]
    [Appearance("ThongTinNhanVien.NgoaiBienChe", TargetItems = "NgayVaoBienChe", Enabled = false, Criteria = "!BienChe")]
    [Appearance("ThongTinNhanVien.LuongKhoan", TargetItems = "NhanVienThongTinLuong.Huong85PhanTramLuong;NhanVienThongTinLuong.LyDoDieuChinh;NhanVienThongTinLuong.MocNangLuongDieuChinh;NhanVienThongTinLuong.MocNangLuong;NhanVienThongTinLuong.NgayBoNhiemNgach;NhanVienThongTinLuong.NgayHuongLuong;NhanVienThongTinLuong.BacLuong;NhanVienThongTinLuong.HeSoLuong", Enabled = false, Criteria = "NhanVienThongTinLuong.PhanLoai=1 or NhanVienThongTinLuong.PhanLoai=2 or NhanVienThongTinLuong.PhanLoai=3 ")]
    [Appearance("ThongTinNhanVien.ThamGiaGiangDay", TargetItems = "TaiBoPhan", Enabled = false, Criteria = "!ThamGiaGiangDay")]
    [Appearance("Hide_PhuongThucTinhThue", TargetItems = "NhanVienThongTinLuong.PhuongThucTinhThue", Visibility = ViewItemVisibility.Hide, Criteria = "NhanVienThongTinLuong.TinhThueTNCNMacDinh")]
    [Appearance("Hide_LuongNet", TargetItems = "NhanVienThongTinLuong.PhuongThucTinhThue;NhanVienThongTinLuong.TinhThueTNCNMacDinh", Visibility = ViewItemVisibility.Hide, Criteria = "NhanVienThongTinLuong.LuongNET")]
    [Appearance("Hide_LuongNgachBac", TargetItems = "NhanVienThongTinLuong.LuongKhoan", Visibility = ViewItemVisibility.Hide, Criteria = "NhanVienThongTinLuong.PhanLoai=0")]

    //[Appearance("HideHSLuong_VLU", TargetItems = "NhanVienThongTinLuong.HeSoLuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'VLU'")]
    [Appearance("LoaiNhanSu_VHU", TargetItems = "LoaiNhanSu;GiangDayMonChung;NganhGiangDayDaiHoc;NganhGiangDayThacSi;NganhGiangDayTienSi;NgayHetHanTapSu;NgayBatDauNCS;NgayKetThucNCS;NgayBatDauChuongTrinhDaiHan;NgayKetThucChuongTrinhDaiHan;ChucVuDoan;NgayKetNapDoan;NgayBoNhiemDoan;NgayHetNhiemKyDoan;ChucVuDoanThe;NgayBoNhiemDoanThe;NgayHetNhiemKyDoanThe;ChucVuDang;NgayBoNhiemDang;NgayHetNhiemKyDang", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'VHU'")]
    [Appearance("HideKhac_QNU", TargetItems = "KhoiNganh;ChuyenNganhGiangDay", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'QNU'")]
    [Appearance("HideKhac_HVNH", TargetItems = "LoaiPhanVien", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'HVNH'")]
    #region Ẩn hiện theo mã trường
    [Appearance("Hide_UFM", TargetItems = "NgayBatDauChuongTrinhDaiHan;NgayKetThucChuongTrinhDaiHan;NganhGiangDayDaiHoc;GiangDayMonChung;NgayHetHanTapSu;NgayBatDauNCS;NgayKetThucNCS;NganhGiangDayThacSi;NganhGiangDayTienSi;NhanVienThongTinLuong.PhanTramKhoiHC;NhanVienThongTinLuong.KhoanPhuongTien;NhanVienThongTinLuong.HSPCChucVuBaoLuu;NhanVienThongTinLuong.KhoanTienNgoaiGio;NhanVienThongTinLuong.TienTroCap;NhanVienThongTinLuong.HSPCKhoiHanhChinh;NhanVienThongTinLuong.HSPCThamNienHC;NhanVienThongTinLuong.PhanTramThamNienHC;NhanVienThongTinLuong.NgayHuongThamNienHC;NhanVienThongTinLuong.HSPCLanhDao;NhanVienThongTinLuong.HSPCThamNienTrongTruong;NhanVienThongTinLuong.HSPCKiemNhiemTrongTruong;NhanVienThongTinLuong.HSPCGiangDay;NhanVienThongTinLuong.HSHoanThanhCV;NhanVienThongTinLuong.HSChung;NhanVienThongTinLuong.PhuCapTienDocHai;BanThoiGian;NhanVienThongTinLuong.HSPCTrachNhiem6;NhanVienTrinhDo.TrinhDoSuPham;MST;NhanVienThongTinLuong.HSPCChuyenMon;NhanVienThongTinLuong.ThamNienCongTac;NhanVienThongTinLuong.PhuCapTangThem;NhanVienThongTinLuong.NgayHuongPhuCap;NhanVienThongTinLuong.HSPCChucVu1;NhanVienThongTinLuong.HSPCChucVu2;NhanVienThongTinLuong.HSPCChucVu3;NhanVienThongTinLuong.HSPCKhuVuc;NhanVienThongTinLuong.KhongTinhSinhNhat;NhanVienThongTinLuong.HeSoThamNienTNTT;NhanVienThongTinLuong.HeSoBangCap;NhanVienThongTinLuong.HeSoTNTT;NhanVienThongTinLuong.HeSoTCDLD;NhanVienThongTinLuong.PhuCapTPBHLD;NhanVienThongTinLuong.HSPCThamNienBaoHiem;NhanVienThongTinLuong.HSPCChucVuBaoHiem;NgayVaoDangDuBi;NgayVaoDangChinhThuc;NhanVienThongTinLuong.LuongNET;NhanVienThongTinLuong.SoNgayLamViec;NhanVienThongTinLuong.HSLTangThemTheoThamNien;NhanVienThongTinLuong.SoNamCongTac;NhanVienThongTinLuong.SoNamLamChuyenVien;NhanVienThongTinLuong.MucHuongTNTT;NhanVienThongTinLuong.TiLeTNTTTheoMucPCUD;NhanVienThongTinLuong.HeSoH1;NhanVienThongTinLuong.HeSoH2;NhanVienThongTinLuong.HeSoH3;NhanVienThongTinLuong.SoKyDien;NhanVienThongTinLuong.SoKyNuoc;NhanVienThongTinLuong.KhongDongBHXH;NhanVienThongTinLuong.KhongDongBHYT;NhanVienThongTinLuong.KhongDongBHTN;NhanVienThongTinLuong.PhuCapBanAnNinh;NhanVienThongTinLuong.PhuCapBanTuVe;NhanVienThongTinLuong.PhuongThucTinhThue;NhanVienThongTinLuong.TinhThueTNCNMacDinh;NhanVienThongTinLuong.TiLeTangThem;NhanVienThongTinLuong.HSLTangThem;NhanVienThongTinLuong.ThangNamTNCongTac;NhanVienThongTinLuong.HSPCChucVuBaoLuu;NgayBoNhiemDoanThe;NhanVienThongTinLuong.PhuCapKhac;NhanVienThongTinLuong.HSPCTrachNhiem1;NhanVienThongTinLuong.HSPCTrachNhiem2;NhanVienThongTinLuong.HSPCTrachNhiem3;NhanVienThongTinLuong.HSPCTrachNhiem4;NhanVienThongTinLuong.HSPCTrachNhiem5;NhanVienThongTinLuong.SoThangKhongTinhThamNien;NhanVienThongTinLuong.PhuCapLaiXe;NhanVienThongTinLuong.NgayBatDauDongBaoHiem;NhanVienThongTinLuong.PhuCapKiemNhiem;NhanVienThongTinLuong.NgayDieuChinhMucThuNhap;NhanVienThongTinLuong.TienTroCapChucVu;NhanVienThongTinLuong.NgayHuongTienTroCapChucVu;NhanVienThongTinLuong.ThuongHieuQuaTheoThang,NhanVienThongTinLuong.MucLuong;NhanVienThongTinLuong.DaBoNhiemNgach;NhanVienTrinhDo.NgayCongTac;NhanVienTrinhDo.NgayHuongCheDo;CaChamCong;BoPhanTinhLuong,NhanVienThongTinLuong.PhanTramTietChuan,NhanVienThongTinLuong.TamGiuLuong,BangCapDaKiemDuyet;SoHoSo;SoHieuCongChuc", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UFM'")]
    [Appearance("Hide_GTVT", TargetItems = "NgayBatDauChuongTrinhDaiHan;NgayKetThucChuongTrinhDaiHan;NganhGiangDayDaiHoc;GiangDayMonChung;NgayHetHanTapSu;NgayBatDauNCS;NgayKetThucNCS;NganhGiangDayThacSi;NganhGiangDayTienSi;NhanVienThongTinLuong.PhanTramKhoiHC;NhanVienThongTinLuong.KhoanPhuongTien;NhanVienThongTinLuong.HSPCChucVuBaoLuu;NhanVienThongTinLuong.KhoanTienNgoaiGio;NhanVienThongTinLuong.TienTroCap;NhanVienThongTinLuong.HSPCKhoiHanhChinh;NhanVienThongTinLuong.HSPCThamNienHC;NhanVienThongTinLuong.PhanTramThamNienHC;NhanVienThongTinLuong.NgayHuongThamNienHC;NhanVienThongTinLuong.HSPCLanhDao;NhanVienThongTinLuong.HSPCThamNienTrongTruong;NhanVienThongTinLuong.HSPCKiemNhiemTrongTruong;NhanVienThongTinLuong.HSPCGiangDay;NhanVienThongTinLuong.HSHoanThanhCV;NhanVienThongTinLuong.HSChung;NhanVienThongTinLuong.PhuCapTienDocHai;BanThoiGian;NhanVienThongTinLuong.HSPCTrachNhiem6;NhanVienTrinhDo.TrinhDoSuPham;MST;BoPhanCu;CongViecHienNay;CongViecDuocGiao;BangCapDaKiemDuyet;LanBoNhiemChucVu;NgayBoNhiemDoanThe;ChucVuDoanThe;NgayBoNhiemDang;ChucVuDang;CaChamCong;SoHieuCongChuc;NhanVienTrinhDo.QuanLyKinhTe;NhanVienTrinhDo.QuanLyGiaoDuc;NhanVienTrinhDo.NgayCongTac;NhanVienTrinhDo.NgayHuongCheDo;NhanVienTrinhDo.ChuongTrinhHoc;NhanVienTrinhDo.QuocGiaHoc;NhanVienThongTinLuong.NgayHuongPhuCap;NhanVienThongTinLuong.PhuCapTienAn;NhanVienThongTinLuong.PhuCapLaiXe;NhanVienThongTinLuong.SoNamCongTac;NhanVienThongTinLuong.PhuCapTPBHLD;NhanVienThongTinLuong.HSPCThamNienBaoHiem;NhanVienThongTinLuong.HSPCChucVuBaoHiem;NhanVienThongTinLuong.LuongNET;NhanVienThongTinLuong.SoNgayLamViec;NhanVienThongTinLuong.SoNamLamChuyenVien;NhanVienThongTinLuong.MucHuongTNTT;NhanVienThongTinLuong.TiLeTNTTTheoMucPCUD;NhanVienThongTinLuong.HeSoH1;NhanVienThongTinLuong.HeSoH2;NhanVienThongTinLuong.SoKyDien;NhanVienThongTinLuong.SoKyNuoc;NhanVienThongTinLuong.PhuCapBanAnNinh;NhanVienThongTinLuong.PhuCapBanTuVe;NhanVienThongTinLuong.PhuCapKhac;NhanVienThongTinLuong.HSPCTrachNhiem1;NhanVienThongTinLuong.HSPCTrachNhiem2;NhanVienThongTinLuong.HSPCTrachNhiem3;NhanVienThongTinLuong.HSPCTrachNhiem4;NhanVienThongTinLuong.HSPCTrachNhiem5;NhanVienThongTinLuong.DaBoNhiemNgach;NhanVienThongTinLuong.PhuCapTrachNhiemCongViec;NhanVienThongTinLuong.ThamNienCongTac;NhanVienThongTinLuong.HSPCChucVu3;NhanVienThongTinLuong.HSPCChucVu2;NhanVienThongTinLuong.HSPCChucVu1;NhanVienThongTinLuong.PhuCapTangThem;NhanVienThongTinLuong.PhuCapTienXang;NhanVienThongTinLuong.PhuCapDienThoai;NhanVienThongTinLuong.HSPCChucVuCongDoan;NhanVienThongTinLuong.HSPCChucVuDoan;NhanVienThongTinLuong.HSPCChucVuDang;NhanVienThongTinLuong.PhuCapThuHut;NhanVienThongTinLuong.HSPCThiDua;NhanVienThongTinLuong.HSPCChuyenMon;NhanVienThongTinLuong.KhongCuTru;NhanVienThongTinLuong.HSPCKhuVuc;NhanVienThongTinLuong.HSPCKiemNhiem;NhanVienThongTinLuong.HSPCLuuDong;NhanVienThongTinLuong.ChenhLechBaoLuuLuong;NhanVienThongTinLuong.PhuCapDacThu;NhanVienThongTinLuong.PhuCapDacBiet;NhanVienThongTinLuong.PhuCapDocHai;NhanVienThongTinLuong.HSPCKhoiHanhChinh;NhanVienThongTinLuong.HSLTangThem;NhanVienThongTinLuong.HSPCTrachNhiem1;NhanVienThongTinLuong.HSPCTrachNhiem2;NhanVienThongTinLuong.HSPCTrachNhiem3;NhanVienThongTinLuong.HSPCTrachNhiem4;NhanVienThongTinLuong.HSPCTrachNhiem5;NhanVienThongTinLuong.TiLeTangThem;NhanVienThongTinLuong.TamGiuLuong;NhanVienThongTinLuong.PhanTramTietChuan;NhanVienThongTinLuong.NgayHuongThamNienHC;NhanVienThongTinLuong.ThuongHieuQuaTheoThang;NhanVienThongTinLuong.MucLuong;NhanVienThongTinLuong.TienTroCapChucVu;NhanVienThongTinLuong.NgayHuongTienTroCapChucVu;NhanVienThongTinLuong.NgayBatDauDongBaoHiem;NhanVienThongTinLuong.NgayDieuChinhMucThuNhap;NhanVienThongTinLuong.ThuongHieuQuaTheoThangSauDieuChinh;NhanVienThongTinLuong.ThuongHieuQuaTheoThangTruocDieuChinh;NhanVienThongTinLuong.PhuongThucTinhThue;NhanVienThongTinLuong.TinhThueTNCNMacDinh;NhanVienThongTinLuong.HSPCKhac;NhanVienThongTinLuong.PhucCapKhac;NhanVienThongTinLuong.HSPCVuotKhung;NhanVienThongTinLuong.HSPCThamNien;NhanVienThongTinLuong.HSPCUuDai;NhanVienThongTinLuong.HeSoThamNienTNTT;NhanVienThongTinLuong.HeSoBangCap;NhanVienThongTinLuong.HeSoTNTT;NhanVienThongTinLuong.HeSoTCDLD;NhanVienThongTinLuong.HSLTangThemTheoThamNien;NhanVienThongTinLuong.HeSoH3;NhanVienThongTinLuong.HSPCChucVuBaoLuu;NhanVienThongTinLuong.NgayHuongHSPCChucVu;NhanVienThongTinLuong.NgayHuongPhuCap;NhanVienThongTinLuong.KhongTinhSinhNhat;NhanVienThongTinLuong.KhongDongBaoHiem;NgayVaoNganhNganHang;LaDangVien;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'GTVT'")]
    [Appearance("Hide_QNU", TargetItems = "NgayBatDauChuongTrinhDaiHan;NgayKetThucChuongTrinhDaiHan;NganhGiangDayDaiHoc;NgayHetHanTapSu;NgayBatDauNCS;NgayKetThucNCS;NganhGiangDayThacSi;NganhGiangDayTienSi;NhanVienThongTinLuong.PhanTramKhoiHC;NhanVienThongTinLuong.HSPCKhoiHanhChinh;NhanVienThongTinLuong.HSPCThamNienHC;NhanVienThongTinLuong.PhanTramThamNienHC;NhanVienThongTinLuong.NgayHuongThamNienHC;NhanVienThongTinLuong.HSPCLanhDao;NhanVienThongTinLuong.HSPCThamNienTrongTruong;NhanVienThongTinLuong.HSPCKiemNhiemTrongTruong;NhanVienThongTinLuong.HSPCGiangDay;NhanVienThongTinLuong.HSHoanThanhCV;NhanVienThongTinLuong.HSChung;BanThoiGian;NhanVienThongTinLuong.HSPCTrachNhiem6;NhanVienTrinhDo.TrinhDoSuPham;MST;NhanVienThongTinLuong.HSPCChuyenMon;NhanVienThongTinLuong.ThamNienCongTac;NhanVienThongTinLuong.PhuCapTangThem;NhanVienThongTinLuong.NgayHuongPhuCap;NhanVienThongTinLuong.HSPCChucVu1;NhanVienThongTinLuong.HSPCChucVu2;NhanVienThongTinLuong.HSPCChucVu3;NhanVienThongTinLuong.HSPCKhuVuc;NhanVienThongTinLuong.KhongTinhSinhNhat;NhanVienThongTinLuong.HeSoThamNienTNTT;NhanVienThongTinLuong.HeSoBangCap;NhanVienThongTinLuong.HeSoTNTT;NhanVienThongTinLuong.HeSoTCDLD;NhanVienThongTinLuong.PhuCapTPBHLD;NhanVienThongTinLuong.HSPCThamNienBaoHiem;NhanVienThongTinLuong.HSPCChucVuBaoHiem;NgayVaoDangDuBi;NgayVaoDangChinhThuc;NhanVienThongTinLuong.LuongNET;NhanVienThongTinLuong.SoNgayLamViec;NhanVienThongTinLuong.HSLTangThemTheoThamNien;NhanVienThongTinLuong.SoNamCongTac;NhanVienThongTinLuong.SoNamLamChuyenVien;NhanVienThongTinLuong.MucHuongTNTT;NhanVienThongTinLuong.TiLeTNTTTheoMucPCUD;NhanVienThongTinLuong.HeSoH1;NhanVienThongTinLuong.HeSoH2;NhanVienThongTinLuong.HeSoH3;NhanVienThongTinLuong.SoKyDien;NhanVienThongTinLuong.SoKyNuoc;NhanVienThongTinLuong.KhongDongBHXH;NhanVienThongTinLuong.KhongDongBHYT;NhanVienThongTinLuong.KhongDongBHTN;NhanVienThongTinLuong.PhuCapBanAnNinh;NhanVienThongTinLuong.PhuCapBanTuVe;NhanVienThongTinLuong.PhuongThucTinhThue;NhanVienThongTinLuong.TinhThueTNCNMacDinh;NhanVienThongTinLuong.TiLeTangThem;NhanVienThongTinLuong.HSLTangThem;NhanVienThongTinLuong.ThangNamTNCongTac;NgayBoNhiemDoanThe;NhanVienThongTinLuong.PhuCapKhac;NhanVienThongTinLuong.HSPCTrachNhiem1;NhanVienThongTinLuong.HSPCTrachNhiem2;NhanVienThongTinLuong.HSPCTrachNhiem3;NhanVienThongTinLuong.HSPCTrachNhiem4;NhanVienThongTinLuong.HSPCTrachNhiem5;NhanVienThongTinLuong.HSPCThamNienHC;NhanVienThongTinLuong.SoThangKhongTinhThamNien;NhanVienThongTinLuong.PhuCapLaiXe;NhanVienThongTinLuong.NgayBatDauDongBaoHiem;NhanVienThongTinLuong.PhuCapKiemNhiem;NhanVienThongTinLuong.NgayDieuChinhMucThuNhap;NhanVienThongTinLuong.TienTroCapChucVu;NhanVienThongTinLuong.NgayHuongTienTroCapChucVu;NhanVienThongTinLuong.ThuongHieuQuaTheoThang,NhanVienThongTinLuong.MucLuong;NhanVienThongTinLuong.DaBoNhiemNgach;NhanVienTrinhDo.NgayCongTac;NhanVienTrinhDo.NgayHuongCheDo;CaChamCong;BoPhanTinhLuong,NhanVienThongTinLuong.PhanTramTietChuan,NhanVienThongTinLuong.TamGiuLuong,NhanVienThongTinLuong.SoSoBHXH,BBangCapDaKiemDuyet;SoHoSo", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]
    [Appearance("Hide_UEL", TargetItems = "NgayBatDauChuongTrinhDaiHan;NgayKetThucChuongTrinhDaiHan;NganhGiangDayDaiHoc;GiangDayMonChung;NgayHetHanTapSu;NgayBatDauNCS;NgayKetThucNCS;NganhGiangDayThacSi;NganhGiangDayTienSi;NhanVienThongTinLuong.PhanTramKhoiHC;NhanVienThongTinLuong.KhoanPhuongTien;NhanVienThongTinLuong.HSPCKhoiHanhChinh;NhanVienThongTinLuong.KhoanTienNgoaiGio;NhanVienThongTinLuong.TienTroCap;NhanVienThongTinLuong.HSPCThamNienHC;NhanVienThongTinLuong.PhanTramThamNienHC;NhanVienThongTinLuong.NgayHuongThamNienHC;NhanVienThongTinLuong.HSPCChucVuCongDoan;NhanVienThongTinLuong.HSPCChucVuDang;NhanVienThongTinLuong.PhuCapDienThoai;NhanVienThongTinLuong.PhuCapTienAn;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UEL'")]
    [Appearance("Hide_NEU", TargetItems = "NhanVienThongTinLuong.HSPCLanhDao;NhanVienThongTinLuong.KhoanPhuongTien;NhanVienThongTinLuong.HSPCThamNienTrongTruong;NhanVienThongTinLuong.HSPCChucVuBaoLuu;NhanVienThongTinLuong.KhoanTienNgoaiGio;NhanVienThongTinLuong.TienTroCap;NhanVienThongTinLuong.HSPCKiemNhiemTrongTruong;NhanVienThongTinLuong.HSPCGiangDay;NhanVienThongTinLuong.HSHoanThanhCV;NhanVienThongTinLuong.HSChung;NhanVienThongTinLuong.PhuCapTienDocHai;BanThoiGian;NhanVienThongTinLuong.HSPCTrachNhiem6;NhanVienTrinhDo.TrinhDoSuPham;MST;NhanVienThongTinLuong.HSPCChuyenMon;NhanVienThongTinLuong.ThamNienCongTac;NhanVienThongTinLuong.PhuCapTangThem;NhanVienThongTinLuong.NgayHuongPhuCap;NhanVienThongTinLuong.HSPCChucVu1;NhanVienThongTinLuong.HSPCChucVu2;NhanVienThongTinLuong.HSPCChucVu3;NhanVienThongTinLuong.HSPCKhuVuc;NhanVienThongTinLuong.KhongTinhSinhNhat;NhanVienThongTinLuong.HeSoThamNienTNTT;NhanVienThongTinLuong.HeSoBangCap;NhanVienThongTinLuong.HeSoTNTT;NhanVienThongTinLuong.HeSoTCDLD;NhanVienThongTinLuong.PhuCapTPBHLD;NhanVienThongTinLuong.HSPCThamNienBaoHiem;NhanVienThongTinLuong.HSPCChucVuBaoHiem;NgayVaoDangDuBi;NgayVaoDangChinhThuc;NhanVienThongTinLuong.LuongNET;NhanVienThongTinLuong.SoNgayLamViec;NhanVienThongTinLuong.HSLTangThemTheoThamNien;NhanVienThongTinLuong.SoNamCongTac;NhanVienThongTinLuong.SoNamLamChuyenVien;NhanVienThongTinLuong.MucHuongTNTT;NhanVienThongTinLuong.TiLeTNTTTheoMucPCUD;NhanVienThongTinLuong.HeSoH1;NhanVienThongTinLuong.HeSoH2;NhanVienThongTinLuong.HeSoH3;NhanVienThongTinLuong.SoKyDien;NhanVienThongTinLuong.SoKyNuoc;NhanVienThongTinLuong.KhongDongBHXH;NhanVienThongTinLuong.KhongDongBHYT;NhanVienThongTinLuong.KhongDongBHTN;NhanVienThongTinLuong.PhuCapBanAnNinh;NhanVienThongTinLuong.PhuCapBanTuVe;NhanVienThongTinLuong.PhuongThucTinhThue;NhanVienThongTinLuong.TinhThueTNCNMacDinh;NhanVienThongTinLuong.TiLeTangThem;NhanVienThongTinLuong.HSLTangThem;NhanVienThongTinLuong.ThangNamTNCongTac;NhanVienThongTinLuong.HSPCChucVuBaoLuu;NgayBoNhiemDoanThe;NhanVienThongTinLuong.PhuCapKhac;NhanVienThongTinLuong.HSPCTrachNhiem1;NhanVienThongTinLuong.HSPCTrachNhiem2;NhanVienThongTinLuong.HSPCTrachNhiem3;NhanVienThongTinLuong.HSPCTrachNhiem4;NhanVienThongTinLuong.HSPCTrachNhiem5;NhanVienThongTinLuong.SoThangKhongTinhThamNien;NhanVienThongTinLuong.PhuCapLaiXe;NhanVienThongTinLuong.NgayBatDauDongBaoHiem;NhanVienThongTinLuong.PhuCapKiemNhiem;NhanVienThongTinLuong.NgayDieuChinhMucThuNhap;NhanVienThongTinLuong.TienTroCapChucVu;NhanVienThongTinLuong.NgayHuongTienTroCapChucVu;NhanVienThongTinLuong.ThuongHieuQuaTheoThang,NhanVienThongTinLuong.MucLuong;NhanVienThongTinLuong.DaBoNhiemNgach;NhanVienTrinhDo.NgayCongTac;NhanVienTrinhDo.NgayHuongCheDo;NhanVienThongTinLuong.PhanTramTietChuan;NhanVienThongTinLuong.TamGiuLuong,NhanVienThongTinLuong.SoSoBHXH,BangCapDaKiemDuyet;SoHoSo", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
    [Appearance("Hide_CDY", TargetItems = "NgayBatDauChuongTrinhDaiHan;NgayKetThucChuongTrinhDaiHan;NganhGiangDayDaiHoc;GiangDayMonChung;NgayHetHanTapSu;NgayBatDauNCS;NgayKetThucNCS;NganhGiangDayThacSi;NganhGiangDayTienSi;NhanVienThongTinLuong.PhanTramKhoiHC;NhanVienThongTinLuong.KhoanPhuongTien;NhanVienThongTinLuong.HSPCKhoiHanhChinh;NhanVienThongTinLuong.HSPCChucVuBaoLuu;NhanVienThongTinLuong.KhoanTienNgoaiGio;NhanVienThongTinLuong.TienTroCap;NhanVienThongTinLuong.HSPCThamNienHC;NhanVienThongTinLuong.PhanTramThamNienHC;NhanVienThongTinLuong.NgayHuongThamNienHC;NhanVienThongTinLuong.HSPCLanhDao;NhanVienThongTinLuong.HSPCThamNienTrongTruong;NhanVienThongTinLuong.HSPCKiemNhiemTrongTruong;NhanVienThongTinLuong.HSPCGiangDay;NhanVienThongTinLuong.HSHoanThanhCV;NhanVienThongTinLuong.HSChung;NhanVienThongTinLuong.PhuCapTienDocHai;BanThoiGian;NhanVienThongTinLuong.HSPCTrachNhiem6;NhanVienTrinhDo.TrinhDoSuPham;MST;NhanVienThongTinLuong.HSPCChuyenMon;NhanVienThongTinLuong.ThamNienCongTac;NhanVienThongTinLuong.PhuCapTangThem;NhanVienThongTinLuong.NgayHuongPhuCap;NhanVienThongTinLuong.HSPCChucVu1;NhanVienThongTinLuong.HSPCChucVu2;NhanVienThongTinLuong.HSPCChucVu3;NhanVienThongTinLuong.HSPCKhuVuc;NhanVienThongTinLuong.KhongTinhSinhNhat;NhanVienThongTinLuong.HeSoThamNienTNTT;NhanVienThongTinLuong.HeSoBangCap;NhanVienThongTinLuong.HeSoTNTT;NhanVienThongTinLuong.HeSoTCDLD;NhanVienThongTinLuong.PhuCapTPBHLD;NhanVienThongTinLuong.HSPCThamNienBaoHiem;NhanVienThongTinLuong.HSPCChucVuBaoHiem;NgayVaoDangDuBi;NgayVaoDangChinhThuc;NhanVienThongTinLuong.LuongNET;NhanVienThongTinLuong.SoNgayLamViec;NhanVienThongTinLuong.HSLTangThemTheoThamNien;NhanVienThongTinLuong.SoNamCongTac;NhanVienThongTinLuong.SoNamLamChuyenVien;NhanVienThongTinLuong.MucHuongTNTT;NhanVienThongTinLuong.TiLeTNTTTheoMucPCUD;NhanVienThongTinLuong.HeSoH1;NhanVienThongTinLuong.HeSoH2;NhanVienThongTinLuong.HeSoH3;NhanVienThongTinLuong.SoKyDien;NhanVienThongTinLuong.SoKyNuoc;NhanVienThongTinLuong.KhongDongBHXH;NhanVienThongTinLuong.KhongDongBHYT;NhanVienThongTinLuong.KhongDongBHTN;NhanVienThongTinLuong.PhuCapBanAnNinh;NhanVienThongTinLuong.PhuCapBanTuVe;NhanVienThongTinLuong.PhuongThucTinhThue;NhanVienThongTinLuong.TinhThueTNCNMacDinh;NhanVienThongTinLuong.TiLeTangThem;NhanVienThongTinLuong.HSLTangThem;NhanVienThongTinLuong.ThangNamTNCongTac;NhanVienThongTinLuong.HSPCChucVuBaoLuu;NgayBoNhiemDoanThe;NhanVienThongTinLuong.PhuCapKhac;NhanVienThongTinLuong.HSPCTrachNhiem1;NhanVienThongTinLuong.HSPCTrachNhiem2;NhanVienThongTinLuong.HSPCTrachNhiem3;NhanVienThongTinLuong.HSPCTrachNhiem4;NhanVienThongTinLuong.HSPCTrachNhiem5;NhanVienThongTinLuong.SoThangKhongTinhThamNien;NhanVienThongTinLuong.PhuCapLaiXe;NhanVienThongTinLuong.NgayBatDauDongBaoHiem;NhanVienThongTinLuong.PhuCapKiemNhiem;NhanVienThongTinLuong.NgayDieuChinhMucThuNhap;NhanVienThongTinLuong.TienTroCapChucVu;NhanVienThongTinLuong.NgayHuongTienTroCapChucVu;NhanVienThongTinLuong.ThuongHieuQuaTheoThang,NhanVienThongTinLuong.MucLuong;NhanVienThongTinLuong.DaBoNhiemNgach;NhanVienTrinhDo.NgayCongTac;NhanVienTrinhDo.NgayHuongCheDo;CaChamCong;BoPhanTinhLuong,NhanVienThongTinLuong.PhanTramTietChuan,NhanVienThongTinLuong.TamGiuLuong,BangCapDaKiemDuyet;SoHoSo;SoHieuCongChuc", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'CDY'")]
    #endregion

    #region Phân quyền Loại tài khoản
    //Nếu là tài chính 
    //[Appearance("NotEdit_TaiChinh", TargetItems = "NhanVienThongTinLuong.VuotKhung,NhanVienThongTinLuong.HSPCVuotKhung,NhanVienThongTinLuong.HSPCChucVu,NhanVienThongTinLuong.NgayHuongHSPCChucVu,NhanVienThongTinLuong.PhuCapUuDai,NhanVienThongTinLuong.HSPCUuDai,NhanVienThongTinLuong.HSPCDocHai,NhanVienThongTinLuong.HSPCKhac,NhanVienThongTinLuong.ThamNien,NhanVienThongTinLuong.HSPCThamNien,NhanVienThongTinLuong.NgayHuongThamNien,NgayTinhThamNienNhaGiao,NhanVienThongTinLuong.PhanTramThamNienHC,NhanVienThongTinLuong.HSPCThamNienHC,NhanVienThongTinLuong.PhuCapDocHai,NhanVienThongTinLuong.PhuCapKhac", Enabled = false, Criteria = "AcountType='TaiChinh' And MaTruong='UTE'")]
    //[Appearance("Edit_TaiChinh", TargetItems = "NhanVienThongTinLuong.HSPCTrachNhiem6;NhanVienThongTinLuong.HSPCTrachNhiem1,NhanVienThongTinLuong.HSPCTrachNhiem2,NhanVienThongTinLuong.HSPCTrachNhiem3,NhanVienThongTinLuong.HSPCTrachNhiem4,NhanVienThongTinLuong.HSPCTrachNhiem5,NhanVienThongTinLuong.HSPCChucVuDoan,NhanVienThongTinLuong.HSPCChucVuDang,NhanVienThongTinLuong.HSPCChucVuCongDoan,NhanVienThongTinLuong.HSPCKhoiHanhChinh,NhanVienThongTinLuong.HSLTangThem,NhanVienThongTinLuong.TiLeTangThem", Enabled = true, Criteria = "AcountType='TaiChinh' And MaTruong='UTE'")]
    //Nếu là tổ chức 
    //[Appearance("Edit_ToChuc", TargetItems = "NhanVienThongTinLuong.HSPCDocHai,NhanVienThongTinLuong.VuotKhung,NhanVienThongTinLuong.HSPCVuotKhung,NhanVienThongTinLuong.HSPCChucVu,NhanVienThongTinLuong.NgayHuongHSPCChucVu,NhanVienThongTinLuong.PhuCapUuDai,NhanVienThongTinLuong.HSPCUuDai,NhanVienThongTinLuong.HSPCKhac,NhanVienThongTinLuong.ThamNien,NhanVienThongTinLuong.HSPCThamNien,NhanVienThongTinLuong.NgayHuongThamNien,NgayTinhThamNienNhaGiao,NhanVienThongTinLuong.PhanTramThamNienHC,NhanVienThongTinLuong.HSPCThamNienHC,,NhanVienThongTinLuong.PhuCapDocHai,NhanVienThongTinLuong.PhuCapKhac", Enabled = true, Criteria = "AcountType='ToChuc' And MaTruong='UTE'")]
    //[Appearance("NotEdit_ToChuc", TargetItems = "NhanVienThongTinLuong.HSPCTrachNhiem6;NhanVienThongTinLuong.HSPCTrachNhiem1,NhanVienThongTinLuong.HSPCTrachNhiem2,NhanVienThongTinLuong.HSPCTrachNhiem3,NhanVienThongTinLuong.HSPCTrachNhiem4,NhanVienThongTinLuong.HSPCTrachNhiem5,NhanVienThongTinLuong.HSPCChucVuDoan,NhanVienThongTinLuong.HSPCChucVuDang,NhanVienThongTinLuong.HSPCChucVuCongDoan,NhanVienThongTinLuong.HSPCKhoiHanhChinh,NhanVienThongTinLuong.HSLTangThem,NhanVienThongTinLuong.TiLeTangThem", Enabled = false, Criteria = "AcountType='ToChuc' And MaTruong='UTE'")]
    #endregion
    [RuleCombinationOfPropertiesIsUnique("ThongTinNhanVien.Unique1", DefaultContexts.Save, "SoHieuCongChuc", "Số hiệu đã tồn tại trong hệ thống. Liên hệ quản trị hệ thống HRM")]

    public class ThongTinNhanVien : NhanVien
    {
        [NonPersistent]
        [Browsable(false)]
        public static ThongTinNhanVien NhanVien { get; set; }

        private bool _KhoaHoSo;
        private string _SoHoSo;
        private string _SoHieuCongChuc;
        private string _Password;
        
        private LoaiNhanSu _LoaiNhanSu;       
        private LoaiNhanVien _LoaiNhanVien;
        private LoaiLuongChinhEnum _LoaiLuongChinh;
        
        private bool _ChamCong;
        private string _DienThoaiCoQuan;

        private bool _ThamGiaGiangDay;
        private BoPhan _TaiBoMon;
        private DateTime _NgayTinhThamNienNhaGiao;
        private DateTime _NgayNghiHuu;
                
        private SucKhoe _TinhTrangSucKhoe;
        private int _CanNang;
        private int _ChieuCao;
        private NhomMau _NhomMau;
                
        private bool _BienChe;
        private DateTime _NgayVaoBienChe;
        private ChucVu _ChucVu;
        private int _LanBoNhiemChucVu;
        private ChucVu _ChucVuKiemNhiem;
        private DateTime _NgayBoNhiemKiemNhiem;
        private ChucVu _ChucVuCoQuanCaoNhat;
        private DateTime _NgayBoNhiem;
        private ChucVuDoan _ChucVuDoan;
        private DateTime _NgayKetNapDoan;
        private DateTime _NgayBoNhiemDoan;
        private DateTime _NgayHetNhiemKyDoan;
        private ChucVuDoanThe _ChucVuDoanThe;
        private DateTime _NgayBoNhiemDoanThe;
        private DateTime _NgayHetNhiemKyDoanThe;
        private DateTime _NgayVaoDangDuBi;
        private DateTime _NgayVaoDangChinhThuc;
        private ChucVuDang _ChucVuDang;
        private DateTime _NgayBoNhiemDang;
        private DateTime _NgayHetNhiemKyDang;       
        private bool _Create = false;

        //NEU
        private bool _GiangDayMonChung;
        private NganhDaoTao _NganhGiangDayDaiHoc;
        private NganhDaoTao _NganhGiangDayThacSi;
        private NganhDaoTao _NganhGiangDayTienSi;
        private DateTime _NgayHetHanTapSu;
        private DateTime _NgayBatDauNCS;
        private DateTime _NgayKetThucNCS;
        private DateTime _NgayBatDauChuongTrinhDaiHan;
        private DateTime _NgayKetThucChuongTrinhDaiHan;
        //QNU
        private KhoiNganh _KhoiNganh;
        private ChuyenNganhGiangDay _ChuyenNganhGiangDay;
        //HVNH
        private LoaiPhanVien _LoaiPhanVien;


        [Browsable(false)]
        [ImmediatePostData]
        public bool KhoaHoSo
        {
            get

            {
                return _KhoaHoSo;
            }
            set
            {
                SetPropertyValue("KhoaHoSo", ref _KhoaHoSo, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Biên chế")]
        public bool BienChe
        {
            get
            {
                return _BienChe;
            }
            set
            {
                SetPropertyValue("BienChe", ref _BienChe, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Ngày nghỉ hưu")]
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

        [ImmediatePostData]
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
                if (!IsLoading)
                {
                    if (TruongConfig.MaTruong.Equals("QNU"))
                    {
                        if (value != null)
                        {
                            this.NhanVienThongTinLuong.HSPCChucVu = value.HSPCChucVu;
                            this.NhanVienThongTinLuong.HSPCTrachNhiem = value.HSPCTrachNhiem_QNU;
                        }
                        else
                        {
                            this.NhanVienThongTinLuong.HSPCChucVu = 0;
                            this.NhanVienThongTinLuong.HSPCTrachNhiem = 0;
                        }
                    }
                    else {
                        if (value != null)
                        {
                            this.NhanVienThongTinLuong.HSPCChucVu = value.HSPCChucVu;
                            //this.NhanVienThongTinLuong.HSPCTrachNhiem = value.HSPCTrachNhiem_QNU;
                        }
                        else
                        {
                            this.NhanVienThongTinLuong.HSPCChucVu = 0;
                            //this.NhanVienThongTinLuong.HSPCTrachNhiem = 0;
                        }
                    }
                }
            }
        }

        [ModelDefault("Caption", "Lần bổ nhiệm chức vụ")]
        public int LanBoNhiemChucVu
        {
            get
            {
                return _LanBoNhiemChucVu;
            }
            set
            {
                SetPropertyValue("LanBoNhiemChucVu", ref _LanBoNhiemChucVu, value);
            }
        }

        [Browsable(false)]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                SetPropertyValue("Password", ref _Password, value);
            }
        }
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
        public ChucVu ChucVuKiemNhiem
        {
            get
            {
                return _ChucVuKiemNhiem;
            }
            set
            {
                SetPropertyValue("ChucVuKiemNhiem", ref _ChucVuKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm kiêm nhiệm")]
        public DateTime NgayBoNhiemKiemNhiem
        {
            get
            {
                return _NgayBoNhiemKiemNhiem;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemKiemNhiem", ref _NgayBoNhiemKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "Loại lương chính")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public LoaiLuongChinhEnum LoaiLuongChinh
        {
            get
            {
                return _LoaiLuongChinh;
            }
            set
            {
                SetPropertyValue("LoaiLuongChinh", ref _LoaiLuongChinh, value);
            }
        }

        [ModelDefault("Caption", "Loại hợp đồng")]    
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong == 'BUH' or MaTruong == 'VHU'")]
        public LoaiNhanVien LoaiNhanVien
        {
            get
            {
                return _LoaiNhanVien;
            }
            set
            {
                SetPropertyValue("LoaiNhanVien", ref _LoaiNhanVien, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại nhân sự")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'GTVT' and MaTruong != 'DNU' and MaTruong != 'HUFLIT' and MaTruong != 'VHU'")]
        public LoaiNhanSu LoaiNhanSu
        {
            get
            {
                return _LoaiNhanSu;
            }
            set
            {
                SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value);
                if (!IsLoading && value != null && value.TenLoaiNhanSu.ToLower().Contains("giảng viên"))
                    ThamGiaGiangDay = true;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tham gia giảng dạy")]
        public bool ThamGiaGiangDay
        {
            get
            {
                return _ThamGiaGiangDay;
            }
            set
            {
                SetPropertyValue("ThamGiaGiangDay", ref _ThamGiaGiangDay, value);
                if (!IsLoading && value)
                {
                    TaiBoMon = BoPhan;
                   
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tại Khoa/Bộ môn")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "ThamGiaGiangDay and MaTruong != 'BUH' and MaTruong != 'GTVT'")]
        public BoPhan TaiBoMon
        {
            get
            {
                return _TaiBoMon;
            }
            set
            {
                SetPropertyValue("TaiBoMon", ref _TaiBoMon, value);
                //if (!IsLoading && value != null && (TruongConfig.MaTruong.Equals("IUH"))
                //{
                //    TaoSoHieuCongChuc();
                //} 
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số hiệu công chức/Mã nhân sự")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong == 'QNU'")]
        public string SoHieuCongChuc
        {
            get
            {
                return _SoHieuCongChuc;
            }
            set
            {
                SetPropertyValue("SoHieuCongChuc", ref _SoHieuCongChuc, value);
             
              
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Số hồ sơ")]
        public string SoHoSo
        {
            get
            {
                return _SoHoSo;
            }
            set
            {
                SetPropertyValue("SoHoSo", ref _SoHoSo, value);
            }
        }     

        [ModelDefault("Caption", "Điện thoại cơ quan")]
        public string DienThoaiCoQuan
        {
            get
            {
                return _DienThoaiCoQuan;
            }
            set
            {
                SetPropertyValue("DienThoaiCoQuan", ref _DienThoaiCoQuan, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ cao nhất")]
        public ChucVu ChucVuCoQuanCaoNhat
        {
            get
            {
                return _ChucVuCoQuanCaoNhat;
            }
            set
            {
                SetPropertyValue("ChucVuCoQuanCaoNhat", ref _ChucVuCoQuanCaoNhat, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm")]
        public DateTime NgayBoNhiem
        {
            get
            {
                return _NgayBoNhiem;
            }
            set
            {
                SetPropertyValue("NgayBoNhiem", ref _NgayBoNhiem, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào biên chế")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "BienChe and MaTruong != 'HUFLIT'")]
        public DateTime NgayVaoBienChe
        {
            get
            {
                return _NgayVaoBienChe;
            }
            set
            {
                SetPropertyValue("NgayVaoBienChe", ref _NgayVaoBienChe, value);
            }
        }

        [ModelDefault("Caption", "Ngày tính thâm niên nhà giáo")]
        public DateTime NgayTinhThamNienNhaGiao
        {
            get
            {
                return _NgayTinhThamNienNhaGiao;
            }
            set
            {
                SetPropertyValue("NgayTinhThamNienNhaGiao", ref _NgayTinhThamNienNhaGiao, value);
            }
        }

        [ModelDefault("Caption", "Nhóm máu")]
        public NhomMau NhomMau
        {
            get
            {
                return _NhomMau;
            }
            set
            {
                SetPropertyValue("NhomMau", ref _NhomMau, value);
            }
        }

        [ModelDefault("Caption", "Chiều cao (Cm)")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int ChieuCao 
        {
            get
            {
                return _ChieuCao;
            }
            set
            {
                SetPropertyValue("ChieuCao", ref _ChieuCao, value);
            }
        }

        [ModelDefault("Caption", "Cân nặng (Kg)")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int CanNang
        {
            get
            {
                return _CanNang;
            }
            set
            {
                SetPropertyValue("CanNang", ref _CanNang, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng sức khỏe")]
        public SucKhoe TinhTrangSucKhoe
        {
            get
            {
                return _TinhTrangSucKhoe;
            }
            set
            {
                SetPropertyValue("TinhTrangSucKhoe", ref _TinhTrangSucKhoe, value);
            }
        }

        [ModelDefault("Caption", "Giảng dạy môn chung")]
        public bool GiangDayMonChung
        {
            get
            {
                return _GiangDayMonChung;
            }
            set
            {
                SetPropertyValue("GiangDayMonChung", ref _GiangDayMonChung, value);
            }
        }

        [ModelDefault("Caption", "Ngành giảng dạy đại học")]
        public NganhDaoTao NganhGiangDayDaiHoc
        {
            get
            {
                return _NganhGiangDayDaiHoc;
            }
            set
            {
                SetPropertyValue("NganhGiangDayDaiHoc", ref _NganhGiangDayDaiHoc, value);
            }
        }

        [ModelDefault("Caption", "Ngành giảng dạy thạc sĩ")]
        public NganhDaoTao NganhGiangDayThacSi
        {
            get
            {
                return _NganhGiangDayThacSi;
            }
            set
            {
                SetPropertyValue("NganhGiangDayThacSi", ref _NganhGiangDayThacSi, value);
            }
        }

        [ModelDefault("Caption", "Ngành giảng dạy tiến sĩ")]
        public NganhDaoTao NganhGiangDayTienSi
        {
            get
            {
                return _NganhGiangDayTienSi;
            }
            set
            {
                SetPropertyValue("NganhGiangDayTienSi", ref _NganhGiangDayTienSi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại phân viện")]
        public LoaiPhanVien LoaiPhanVien
        {
            get
            {
                return _LoaiPhanVien;
            }
            set
            {
                SetPropertyValue("LoaiPhanVien", ref _LoaiPhanVien, value);
                if(!IsLoading && value != null)
                {
                    PhanVien = value;
                    LoadMaGVCH_HVNH();
                }
            }
        }

        [Aggregated]
        [Association("ThongTinNhanVien-ListQuanHeGiaDinh")]
        [ModelDefault("Caption", "Quan hệ gia đình")]
        public XPCollection<QuanHeGiaDinh> ListQuanHeGiaDinh
        {
            get
            {
                return GetCollection<QuanHeGiaDinh>("ListQuanHeGiaDinh");
            }
        }

        [Aggregated]
        [Association("ThongTinNhanVien-ListGiamTruGiaCanh")]
        [ModelDefault("Caption", "Giảm trừ gia cảnh")]
        public XPCollection<GiamTruGiaCanh> ListGiamTruGiaCanh
        {
            get
            {
                return GetCollection<GiamTruGiaCanh>("ListGiamTruGiaCanh");
            }
        }

        [Aggregated]
        [Association("ThongTinNhanVien-ListChucVuKiemNhiem")]
        [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
        public XPCollection<ChucVuKiemNhiem> ListChucVuKiemNhiem
        {
            get
            {
                return GetCollection<ChucVuKiemNhiem>("ListChucVuKiemNhiem");
            }
        }
        [Aggregated]
        [Association("ThongTinNhanVien-ListHoSoBaoHiem")]
        [ModelDefault("Caption", "Hồ sơ bảo hiểm")]
        public XPCollection<HoSoBaoHiem> ListHoSoBaoHiem
        {
            get
            {
                return GetCollection<HoSoBaoHiem>("ListHoSoBaoHiem");
            }
        }

        [NonPersistent]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Chức vụ Đảng")]
        public ChucVuDang ChucVuDang
        {
            get
            {
                if (Session.FindObject<DangVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)) != null)
                    _ChucVuDang = Session.FindObject<DangVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)).ChucVuDang;
                return _ChucVuDang;
            }
            set
            {
                SetPropertyValue("ChucVuDang", ref _ChucVuDang, value);
            }
        }

        [NonPersistent]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Ngày bổ nhiệm Đảng")]
        public DateTime NgayBoNhiemDang
        {
            get
            {
                if (Session.FindObject<DangVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)) != null)
                    _NgayBoNhiemDang = Session.FindObject<DangVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)).NgayBoNhiem;
                return _NgayBoNhiemDang;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemDang", ref _NgayBoNhiemDang, value);
            }
        }

        [NonPersistent]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Ngày hết nhiệm kỳ Đảng")]
        public DateTime NgayHetNhiemKyDang
        {
            get
            {
                if (Session.FindObject<DangVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)) != null)
                    _NgayHetNhiemKyDang = Session.FindObject<DangVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)).NgayHetNhiemKy;
                return _NgayHetNhiemKyDang;
            }
            set
            {
                SetPropertyValue("NgayHetNhiemKyDang", ref _NgayHetNhiemKyDang, value);
            }
        }

        [NonPersistent]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Chức vụ Đoàn thể")]
        public ChucVuDoanThe ChucVuDoanThe
        {
            get
            {
                if (Session.FindObject<DoanThe>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)) != null)
                    _ChucVuDoanThe = Session.FindObject<DoanThe>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)).ChucVuDoanThe;
                return _ChucVuDoanThe;
            }
        }

        [NonPersistent]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Ngày bổ nhiệm Đoàn thể")]
        public DateTime NgayBoNhiemDoanThe
        {
            get
            {
                if (Session.FindObject<DoanThe>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)) != null)
                    _NgayBoNhiemDoanThe = Session.FindObject<DoanThe>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)).NgayBoNhiem;
                return _NgayBoNhiemDoanThe;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemDoanThe", ref _NgayBoNhiemDoanThe, value);
            }
        }

        [NonPersistent]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Ngày hết nhiệm kỳ Đoàn thể")]
        public DateTime NgayHetNhiemKyDoanThe
        {
            get
            {
                if (Session.FindObject<DoanThe>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)) != null)
                    _NgayHetNhiemKyDoanThe = Session.FindObject<DoanThe>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)).NgayHetNhiemKy;
                return _NgayHetNhiemKyDoanThe;
            }
            set
            {
                SetPropertyValue("NgayHetNhiemKyDoanThe", ref _NgayHetNhiemKyDoanThe, value);
            }
        }

        [NonPersistent]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Chức vụ Đoàn")]
        public ChucVuDoan ChucVuDoan
        {
            get
            {
                if (Session.FindObject<DoanVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)) != null)
                    _ChucVuDoan = Session.FindObject<DoanVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)).ChucVuDoan;
                return _ChucVuDoan;
            }
        }

        [NonPersistent]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Ngày bổ nhiệm Đoàn")]
        public DateTime NgayBoNhiemDoan
        {
            get
            {
                if (Session.FindObject<DoanVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)) != null)
                    _NgayBoNhiemDoan = Session.FindObject<DoanVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)).NgayBoNhiem;
                return _NgayBoNhiemDoan;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemDoan", ref _NgayBoNhiemDoan, value);
            }
        }

        [NonPersistent]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Ngày hết nhiệm kỳ Đoàn")]
        public DateTime NgayHetNhiemKyDoan
        {
            get
            {
                if (Session.FindObject<DoanVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)) != null)
                    _NgayHetNhiemKyDoan = Session.FindObject<DoanVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)).NgayHetNhiemKy;
                return _NgayHetNhiemKyDoan;
            }
            set
            {
                SetPropertyValue("NgayHetNhiemKyDoan", ref _NgayHetNhiemKyDoan, value);
            }
        }

        [NonPersistent]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Ngày kết nạp Đoàn")]
        public DateTime NgayKetNapDoan
        {
            get
            {
                if (Session.FindObject<DoanVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)) != null)
                    _NgayKetNapDoan = Session.FindObject<DoanVien>(CriteriaOperator.Parse("ThongTinNhanVien = ?", Oid)).NgayKetNap;
                return _NgayKetNapDoan;
            }
        }

        [ModelDefault("Caption", "Chấm công")]
        public bool ChamCong
        {
            get
            {
                return _ChamCong;
            }
            set
            {
                SetPropertyValue("ChamCong", ref _ChamCong, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào Đảng chính thức")]
        public DateTime NgayVaoDangChinhThuc
        {
            get
            {
                return _NgayVaoDangChinhThuc;
            }
            set
            {
                SetPropertyValue("NgayVaoDangChinhThuc", ref _NgayVaoDangChinhThuc, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào Đảng dự bị")]
        public DateTime NgayVaoDangDuBi
        {
            get
            {
                return _NgayVaoDangDuBi;
            }
            set
            {
                SetPropertyValue("NgayVaoDangDuBi", ref _NgayVaoDangDuBi, value);
            }
        }
        [ModelDefault("Caption", "Ngày hết hạn tập sự")]
        public DateTime NgayHetHanTapSu
        {
            get
            {
                return _NgayHetHanTapSu;
            }
            set
            {
                SetPropertyValue("NgayHetHanTapSu", ref _NgayHetHanTapSu, value);
            }
        }
        [ModelDefault("Caption", "Ngày bắt đầu Nghiêm cứu sinh")]
        public DateTime NgayBatDauNCS
        {
            get
            {
                return _NgayBatDauNCS;
            }
            set
            {
                SetPropertyValue("NgayBatDauNCS", ref _NgayBatDauNCS, value);
            }
        }
        [ModelDefault("Caption", "Ngày kết thúc Nghiêm cứu sinh")]
        public DateTime NgayKetThucNCS
        {
            get
            {
                return _NgayKetThucNCS;
            }
            set
            {
                SetPropertyValue("NgayKetThucNCS", ref _NgayKetThucNCS, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu học chương trình dài hạn")]
        public DateTime NgayBatDauChuongTrinhDaiHan
        {
            get
            {
                return _NgayBatDauChuongTrinhDaiHan;
            }
            set
            {
                SetPropertyValue("NgayBatDauChuongTrinhDaiHan", ref _NgayBatDauChuongTrinhDaiHan, value);
            }
        }
        [ModelDefault("Caption", "Ngày kết thúc chương trình dại hạn")]
        public DateTime NgayKetThucChuongTrinhDaiHan
        {
            get
            {
                return _NgayKetThucChuongTrinhDaiHan;
            }
            set
            {
                SetPropertyValue("NgayKetThucChuongTrinhDaiHan", ref _NgayKetThucChuongTrinhDaiHan, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Khối ngành")]
        public KhoiNganh KhoiNganh
        {
            get
            {
                return _KhoiNganh;
            }
            set
            {
                SetPropertyValue("KhoiNganh", ref _KhoiNganh, value);
            }
        }
        [ModelDefault("Caption", "Chuyên ngành giảng dạy")]
        public ChuyenNganhGiangDay ChuyenNganhGiangDay
        {
            get
            {
                return _ChuyenNganhGiangDay;
            }
            set
            {
                SetPropertyValue("ChuyenNganhGiangDay", ref _ChuyenNganhGiangDay, value);
                if(!IsLoading && value != null && MaTruong.Equals("QNU"))
                {
                    KhoiNganh = value.KhoiNganh;
                }
            }
        }

        //HBU hỗ trợ phân quyền chỉ xem MST ở ThôngTinLương
        [NonPersistent]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Mã số thuế")]
        public string MST
        {
            get
            {
                return NhanVienThongTinLuong.MaSoThue;
            }
        }

        [Browsable(false)]
        public bool HopDongLaoDong { get; set; }
        [Browsable(false)]
        public bool HopDongKhoan { get; set; }

        //[NonPersistent]
        //[Browsable(false)]
        //private string MaTruong { get; set; }

        public ThongTinNhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //Lấy mã trường hiện tại dùng để phân quyền
            MaTruong = TruongConfig.MaTruong;
            this.NhanVienThongTinLuong.TinhThueTNCNMacDinh = true;

            if (string.IsNullOrWhiteSpace(MaQuanLy))
                MaQuanLy = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.MaNhanVien);
            //Thảo thêm mới
            if(TruongConfig.MaTruong.Equals("QNU"))
            {
                if (string.IsNullOrWhiteSpace(SoHieuCongChuc))
                    SoHieuCongChuc = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoHieuCongChuc);
            }
            //

            if (TruongConfig.MaTruong.Equals("HUFLIT"))
            {
                object obj = DataProvider.GetObject("spd_HUFLIT_MaCoHuu", System.Data.CommandType.StoredProcedure);
                string mau = obj.ToString();
                SoHieuCongChuc = mau;
            }
            if(TruongConfig.MaTruong.Equals("UEL"))
            {
                object obj = DataProvider.GetObject("spd_System_TaoMaUIS", System.Data.CommandType.StoredProcedure);
                string mau = obj.ToString();
                MaUIS = mau;
            }

            if (TruongConfig.MaTruong.Equals("HVNH"))
            {
                LoaiPhanVien = Session.FindObject<LoaiPhanVien>(CriteriaOperator.Parse("Oid = ?", "1E823662-777F-443F-AFED-440F5126D542"));
                LoadMaGVCH_HVNH();
            }
            
            //Loại hồ sơ
            LoaiHoSo = LoaiHoSoEnum.NhanVien;        
            //
            _Create = true;
        }

        public void LoadMaGVCH_HVNH()
        {
            if (TruongConfig.MaTruong.Equals("HVNH"))
            {
                if (LoaiPhanVien != null)
                {
                    object kq = null;
                    SqlParameter[] param = new SqlParameter[1]; /*Số parameter trên Store Procedure*/
                    param[0] = new SqlParameter("@LoaiPhanVien", LoaiPhanVien.Oid);
                    kq = DataProvider.GetValueFromDatabase("spd_NhanSu_TaoMaGiangVienHVNH_CH", System.Data.CommandType.StoredProcedure, param);
                    if (kq != null)
                    {
                        MaQuanLy = kq.ToString();
                    }
                }
            }
        }

        [NonPersistent]
        [Browsable(false)]
        private string AcountType { get; set; }

        protected override void AfterBoPhanChanged()
        {
            SoHieuCongChuc = null;
            base.AfterBoPhanChanged();
            if (TruongConfig.MaTruong.Equals("CYD") && BoPhan != null && string.IsNullOrWhiteSpace(SoHieuCongChuc))
            {
                SoHieuCongChuc = GetSoHieuNhanVien();
            }                      
        }  
              
        public void onloadTTNV()
        {
            //Lấy mã trường hiện tại dùng để phân quyền
            MaTruong = TruongConfig.MaTruong;
            //
           
        }
         private string GetSoHieuNhanVien()
         {
             string soHieu = string.Empty;

             if (string.IsNullOrWhiteSpace(SoHieuCongChuc))
                 soHieu = MaQuanLyFactory.CreateSoHieuNhanVien(this.BoPhan);


             return soHieu;
         }
           
        private string GetMaQuanLyNhanVien()
        {
            string maQuanLy = string.Empty;

            if (string.IsNullOrWhiteSpace(MaQuanLy))
                maQuanLy = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.MaNhanVien);           

            return maQuanLy;
        }
             
        protected override void OnSaving()
        {      
            base.OnSaving();

            if (!IsDeleted)
            {
                
                //Đánh mã quản lý tự động nếu chưa có mã
                if (_Create == true && string.IsNullOrWhiteSpace(MaQuanLy))
                {                    
                    MaQuanLy = GetMaQuanLyNhanVien();
                }
                //Kiểm tra lại mã quản lý lúc lưu
                //if (TruongConfig.MaTruong.Equals("VHU"))
                //{

                //}

                //Đánh số hồ sơ tự động nếu chưa có số
                //if (string.IsNullOrWhiteSpace(SoHoSo))
                //{
                //    if (MaTruong.Equals("DLU"))
                //    {
                //        SoHoSo = GetSoHoSoNhanVien();
                //    }
                //}
                //

                if (HamDungChung.CauHinhChung != null)
                {                
                    if (HamDungChung.CauHinhChung.CauHinhBaoHiem != null
                        && HamDungChung.CauHinhChung.CauHinhBaoHiem.TuDongTaoHoSoBaoHiem)
                    {
                        HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse(".n=?", Oid));
                        if (hoSoBaoHiem == null)
                        {
                            hoSoBaoHiem = new HoSoBaoHiem(Session);
                            hoSoBaoHiem.ThongTinNhanVien = this;
                        }
                    }
                }
                //Cập nhật hệ số chức vụ qua ThongTinLuong
                if (TruongConfig.MaTruong.Equals("QNU"))
                {
                    if (ChucVu != null)
                    {
                        NhanVienThongTinLuong.HSPCChucVu = ChucVu.HSPCChucVu;
                        NhanVienThongTinLuong.HSPCTrachNhiem = ChucVu.HSPCTrachNhiem_QNU;
                    }
                    else
                    {
                        NhanVienThongTinLuong.HSPCChucVu = 0;
                        NhanVienThongTinLuong.HSPCTrachNhiem = 0;
                    }
                }
                else
                {
                    if (ChucVu != null)
                    {
                        NhanVienThongTinLuong.HSPCChucVu = ChucVu.HSPCChucVu;
                        //NhanVienThongTinLuong.HSPCTrachNhiem = ChucVu.HSPCTrachNhiem_QNU;
                    }
                    else
                    {
                        NhanVienThongTinLuong.HSPCChucVu = 0;
                        //NhanVienThongTinLuong.HSPCTrachNhiem = 0;
                    }
                }

                if (TruongConfig.MaTruong.Equals("UEL"))
                {
                    //Cập nhật bộ phận chấm công theo ngày nếu bộ phận thay đổi trong tháng
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@ThongTinNhanVien", this.Oid);
                    param[1] = new SqlParameter("@BoPhan", this.BoPhan.Oid);
                    param[2] = new SqlParameter("@NgayCapNhat", HamDungChung.GetServerTime().Date);

                    DataProvider.ExecuteNonQuery("spd_WebChamCong_CapNhatBoPhanChamCongTheoNgay", CommandType.StoredProcedure, param);
                }
                if (TruongConfig.MaTruong.Equals("HVNH"))
                {
                    LoadMaGVCH_HVNH();
                }
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            //
            //Cập nhật tài khoản web
            if (TruongConfig.MaTruong.Equals("UEL") || (TruongConfig.MaTruong.Equals("NEU") && !LoaiNhanSu.TenLoaiNhanSu.Contains("Biệt phái")))
            {
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@NhanVien", Oid);
                DataProvider.ExecuteNonQuery("spd_Web_TaoTaiKhoan_WebHRM", CommandType.StoredProcedure, parameter);
            }

            if (TruongConfig.MaTruong == "DNU")
            {
                if (ThamGiaGiangDay)
                {
                    SqlParameter[] parameter = new SqlParameter[2];
                    parameter[0] = new SqlParameter("@NhanVien", Oid);
                    parameter[1] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                    DataProvider.ExecuteNonQuery("spd_PMS_DNU_TaoKiemGiang", CommandType.StoredProcedure, parameter);
                }
            }

            if (TruongConfig.MaTruong == "QNU")
            {
                if (OidHoSoCha == Guid.Empty && HamDungChung.CauHinhChung.DongBoTaiKhoan)
                {
                    CapNhatTaiKhoanWebHRM();
                    //CapNhatTaiKhoanWinERP();
                }
            }
        }

        public void CapNhatTaiKhoanWebHRM()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@NhanVien", Oid);
            //
            DataProvider.ExecuteNonQuery("spd_Web_TaoTaiKhoan_WebHRM", CommandType.StoredProcedure, parameter);
            //
        }

        public void CapNhatTaiKhoanWinERP()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@NhanVien", Oid);
            //
            DataProvider.ExecuteNonQuery("spd_HeThong_TaoTaiKhoanWin", CommandType.StoredProcedure, parameter);
            //
        }
    }

}
