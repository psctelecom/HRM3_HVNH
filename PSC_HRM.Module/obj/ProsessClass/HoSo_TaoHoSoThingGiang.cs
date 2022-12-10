using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.HoSo;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.ChamCong;
using PSC_HRM.Module;
using System.Text;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.TaoMaQuanLy;

namespace PSC_HRM.Module.Controllers
{
    public class HoSo_TaoHoSoThingGiang
    {
        public static void XuLy(IObjectSpace obs, ThongTinNhanVien thongTinNhanVien)
        {
            using (DialogUtil.AutoWait())
            {
                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();
                    //
                    GiangVienThinhGiang thinhGiang = uow.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("HoTen like ? and NgaySinh=?", thongTinNhanVien.HoTen, thongTinNhanVien.NgaySinh));
                    if (thinhGiang == null)
                    {
                        try
                        {
                            thinhGiang = new GiangVienThinhGiang(uow);

                            #region Thông tin cơ bản

                            if (TruongConfig.MaTruong.Equals("UFM"))
                            {
                                thinhGiang.MaQuanLy = String.Concat("CHTG", thongTinNhanVien.MaQuanLy.ToString());
                            }
                            else
                            {
                                thinhGiang.MaQuanLy = thongTinNhanVien.MaQuanLy.ToString();
                            }

                            thinhGiang.OidHoSoCha = thongTinNhanVien.Oid;
                            //thinhGiang.HinhAnh = thongTinNhanVien.HinhAnh;
                            thinhGiang.Ho = thongTinNhanVien.Ho;
                            thinhGiang.Ten = thongTinNhanVien.Ten;
                            thinhGiang.TenGoiKhac = thongTinNhanVien.TenGoiKhac;
                            thinhGiang.GioiTinh = thongTinNhanVien.GioiTinh;
                            thinhGiang.NgaySinh = thongTinNhanVien.NgaySinh;
                            thinhGiang.NoiSinh = thongTinNhanVien.NoiSinh != null ? uow.GetObjectByKey<DiaChi>(thongTinNhanVien.NoiSinh.Oid) : null;
                            thinhGiang.CMND = thongTinNhanVien.CMND;
                            thinhGiang.SoHoChieu = thongTinNhanVien.SoHoChieu;
                            thinhGiang.NgayCap = thongTinNhanVien.NgayCap;
                            thinhGiang.NoiCap = thongTinNhanVien.NoiCap != null ? uow.GetObjectByKey<TinhThanh>(thongTinNhanVien.NoiCap.Oid) : null;
                            thinhGiang.QuocTich = thongTinNhanVien.QuocTich != null ? uow.GetObjectByKey<QuocGia>(thongTinNhanVien.QuocTich.Oid) : null;
                            thinhGiang.QueQuan = thongTinNhanVien.QueQuan != null ? uow.GetObjectByKey<DiaChi>(thongTinNhanVien.QueQuan.Oid) : null;
                            thinhGiang.DiaChiThuongTru = thongTinNhanVien.DiaChiThuongTru != null ? uow.GetObjectByKey<DiaChi>(thongTinNhanVien.DiaChiThuongTru.Oid) : null;
                            thinhGiang.NoiOHienNay = thongTinNhanVien.NoiOHienNay != null ? uow.GetObjectByKey<DiaChi>(thongTinNhanVien.NoiOHienNay.Oid) : null;
                            thinhGiang.Email = thongTinNhanVien.Email;
                            thinhGiang.DienThoaiDiDong = thongTinNhanVien.DienThoaiDiDong;
                            thinhGiang.DienThoaiNhaRieng = thongTinNhanVien.DienThoaiNhaRieng;
                            thinhGiang.TinhTrangHonNhan = thongTinNhanVien.TinhTrangHonNhan != null ? uow.GetObjectByKey<TinhTrangHonNhan>(thongTinNhanVien.TinhTrangHonNhan.Oid) : null;
                            thinhGiang.DanToc = thongTinNhanVien.DanToc != null ? uow.GetObjectByKey<DanToc>(thongTinNhanVien.DanToc.Oid) : null;
                            thinhGiang.TonGiao = thongTinNhanVien.TonGiao != null ? uow.GetObjectByKey<TonGiao>(thongTinNhanVien.TonGiao.Oid) : null;
                            thinhGiang.HinhThucTuyenDung = thongTinNhanVien.HinhThucTuyenDung;
                            thinhGiang.GhiChu = thongTinNhanVien.GhiChu;
                            thinhGiang.NgayVaoCoQuan = thongTinNhanVien.NgayVaoCoQuan;
                            thinhGiang.ChucDanh = thongTinNhanVien.ChucDanh != null ? uow.GetObjectByKey<ChucDanh>(thongTinNhanVien.ChucDanh.Oid) : null;
                            thinhGiang.LoaiHoSo = LoaiHoSoEnum.GiangVien;
                            //
                            if (TruongConfig.MaTruong.Equals("UFM") || TruongConfig.MaTruong.Equals("DNU"))
                            {
                                thinhGiang.BoPhan = thongTinNhanVien.BoPhan != null ? uow.GetObjectByKey<BoPhan>(thongTinNhanVien.BoPhan.Oid) : uow.GetObjectByKey<BoPhan>(thongTinNhanVien.ThongTinTruong.Oid);
                            }
                            else
                            {
                                thinhGiang.BoPhan = thongTinNhanVien.ThongTinTruong != null ? uow.GetObjectByKey<BoPhan>(thongTinNhanVien.ThongTinTruong.Oid) : null;
                                thinhGiang.TaiBoMon = thongTinNhanVien.TaiBoMon != null ? uow.GetObjectByKey<BoPhan>(thongTinNhanVien.TaiBoMon.Oid) : uow.GetObjectByKey<BoPhan>(thongTinNhanVien.BoPhan.Oid);
                            }
                                //
                            thinhGiang.NgayVaoNganhGiaoDuc = thongTinNhanVien.NgayVaoNganhGiaoDuc;
                            thinhGiang.ThanhPhanXuatThan = thongTinNhanVien.ThanhPhanXuatThan != null ? uow.GetObjectByKey<ThanhPhanXuatThan>(thongTinNhanVien.ThanhPhanXuatThan.Oid) : null;
                            thinhGiang.UuTienGiaDinh = thongTinNhanVien.UuTienGiaDinh != null ? uow.GetObjectByKey<UuTienGiaDinh>(thongTinNhanVien.UuTienGiaDinh.Oid) : null;
                            thinhGiang.UuTienBanThan = thongTinNhanVien.UuTienBanThan != null ? uow.GetObjectByKey<UuTienBanThan>(thongTinNhanVien.UuTienBanThan.Oid) : null;
                            thinhGiang.CongViecHienNay = thongTinNhanVien.CongViecHienNay != null ? uow.GetObjectByKey<CongViec>(thongTinNhanVien.CongViecHienNay.Oid) : null;
                            thinhGiang.NgayTuyenDung = thongTinNhanVien.NgayTuyenDung;
                            thinhGiang.DonViTuyenDung = thongTinNhanVien.DonViTuyenDung;
                            thinhGiang.CongViecTuyenDung = thongTinNhanVien.CongViecTuyenDung;
                            thinhGiang.CongViecDuocGiao = thongTinNhanVien.CongViecDuocGiao != null ? uow.GetObjectByKey<CongViec>(thongTinNhanVien.CongViecDuocGiao.Oid) : null;
                            thinhGiang.TinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
                            #endregion

                            #region Nhân viên thông tin lương
                            if (thongTinNhanVien.NhanVienThongTinLuong != null)
                            {
                                thinhGiang.NhanVienThongTinLuong.MaSoThue = thongTinNhanVien.NhanVienThongTinLuong.MaSoThue;
                                thinhGiang.NhanVienThongTinLuong.CoQuanThue = thongTinNhanVien.NhanVienThongTinLuong.CoQuanThue != null ? uow.GetObjectByKey<CoQuanThue>(thongTinNhanVien.NhanVienThongTinLuong.CoQuanThue.Oid) : null;
                            }
                            #endregion

                            #region Nhân viên trình độ
                            if (thongTinNhanVien.NhanVienTrinhDo != null)
                            {
                                thinhGiang.NhanVienTrinhDo.TrinhDoVanHoa = thongTinNhanVien.NhanVienTrinhDo.TrinhDoVanHoa != null ? uow.GetObjectByKey<TrinhDoVanHoa>(thongTinNhanVien.NhanVienTrinhDo.TrinhDoVanHoa.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.TrinhDoChuyenMon = thongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? uow.GetObjectByKey<TrinhDoChuyenMon>(thongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.ChuyenMonDaoTao = thongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null ? uow.GetObjectByKey<ChuyenMonDaoTao>(thongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.TruongDaoTao = thongTinNhanVien.NhanVienTrinhDo.TruongDaoTao != null ? uow.GetObjectByKey<TruongDaoTao>(thongTinNhanVien.NhanVienTrinhDo.TruongDaoTao.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.HinhThucDaoTao = thongTinNhanVien.NhanVienTrinhDo.HinhThucDaoTao != null ? uow.GetObjectByKey<HinhThucDaoTao>(thongTinNhanVien.NhanVienTrinhDo.HinhThucDaoTao.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.NamTotNghiep = thongTinNhanVien.NhanVienTrinhDo.NamTotNghiep;
                                thinhGiang.NhanVienTrinhDo.HocHam = thongTinNhanVien.NhanVienTrinhDo.HocHam != null ? uow.GetObjectByKey<HocHam>(thongTinNhanVien.NhanVienTrinhDo.HocHam.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.NamCongNhanHocHam = thongTinNhanVien.NhanVienTrinhDo.NamCongNhanHocHam;
                                thinhGiang.NhanVienTrinhDo.DanhHieuCaoNhat = thongTinNhanVien.NhanVienTrinhDo.DanhHieuCaoNhat != null ? uow.GetObjectByKey<DanhHieuDuocPhong>(thongTinNhanVien.NhanVienTrinhDo.DanhHieuCaoNhat.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.NgayPhongDanhHieu = thongTinNhanVien.NhanVienTrinhDo.NgayPhongDanhHieu;
                                thinhGiang.NhanVienTrinhDo.ChuongTrinhHoc = thongTinNhanVien.NhanVienTrinhDo.ChuongTrinhHoc != null ? uow.GetObjectByKey<ChuongTrinhHoc>(thongTinNhanVien.NhanVienTrinhDo.ChuongTrinhHoc.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.QuanLyGiaoDuc = thongTinNhanVien.NhanVienTrinhDo.QuanLyGiaoDuc != null ? uow.GetObjectByKey<QuanLyGiaoDuc>(thongTinNhanVien.NhanVienTrinhDo.QuanLyGiaoDuc.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.QuanLyKinhTe = thongTinNhanVien.NhanVienTrinhDo.QuanLyKinhTe != null ? uow.GetObjectByKey<QuanLyKinhTe>(thongTinNhanVien.NhanVienTrinhDo.QuanLyKinhTe.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.QuanLyNhaNuoc = thongTinNhanVien.NhanVienTrinhDo.QuanLyNhaNuoc != null ? uow.GetObjectByKey<QuanLyNhaNuoc>(thongTinNhanVien.NhanVienTrinhDo.QuanLyNhaNuoc.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.TrinhDoTinHoc = thongTinNhanVien.NhanVienTrinhDo.TrinhDoTinHoc != null ? uow.GetObjectByKey<TrinhDoTinHoc>(thongTinNhanVien.NhanVienTrinhDo.TrinhDoTinHoc.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.NgoaiNgu = thongTinNhanVien.NhanVienTrinhDo.NgoaiNgu != null ? uow.GetObjectByKey<NgoaiNgu>(thongTinNhanVien.NhanVienTrinhDo.NgoaiNgu.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.TrinhDoNgoaiNgu = thongTinNhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu != null ? uow.GetObjectByKey<TrinhDoNgoaiNgu>(thongTinNhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu.Oid) : null;
                            }
                            #endregion

                            #region Tài khoản ngân hàng
                            foreach (TaiKhoanNganHang item in thongTinNhanVien.ListTaiKhoanNganHang)
                            {
                                TaiKhoanNganHang taiKhoan = new TaiKhoanNganHang(uow);
                                taiKhoan.NhanVien = thinhGiang;
                                taiKhoan.SoTaiKhoan = item.SoTaiKhoan;
                                taiKhoan.TaiKhoanChinh = item.TaiKhoanChinh;
                                taiKhoan.ThongTinTruong = item.ThongTinTruong != null ? uow.GetObjectByKey<ThongTinTruong>(item.ThongTinTruong.Oid) : null;
                                taiKhoan.NganHang = item.NganHang != null ? uow.GetObjectByKey<NganHang>(item.NganHang.Oid) : null;
                                //
                                thinhGiang.ListTaiKhoanNganHang.Add(taiKhoan);
                            }
                            #endregion

                            #region Văn bằng
                            using (XPCollection<VanBang> vanBangList = new DevExpress.Xpo.XPCollection<VanBang>(uow, CriteriaOperator.Parse("HoSo=?", thongTinNhanVien.Oid)))
                            {
                                foreach (VanBang item in vanBangList)
                                {
                                    if (item.TrinhDoChuyenMon != null)
                                    {
                                        VanBang vanBang = new VanBang(uow);
                                        vanBang.HoSo = thinhGiang;
                                        vanBang.TrinhDoChuyenMon = item.TrinhDoChuyenMon != null ? uow.GetObjectByKey<TrinhDoChuyenMon>(item.TrinhDoChuyenMon.Oid) : null;
                                        vanBang.ChuyenMonDaoTao = item.ChuyenMonDaoTao != null ? uow.GetObjectByKey<ChuyenMonDaoTao>(item.ChuyenMonDaoTao.Oid) : null;
                                        vanBang.TruongDaoTao = item.TruongDaoTao != null ? uow.GetObjectByKey<TruongDaoTao>(item.TruongDaoTao.Oid) : null;
                                        vanBang.HinhThucDaoTao = item.HinhThucDaoTao != null ? uow.GetObjectByKey<HinhThucDaoTao>(item.HinhThucDaoTao.Oid) : null;
                                        vanBang.DiemTrungBinh = item.DiemTrungBinh;
                                        vanBang.XepLoai = item.XepLoai;
                                        vanBang.NamTotNghiep = item.NamTotNghiep;
                                        vanBang.GiayToHoSo = item.GiayToHoSo!= null ? uow.GetObjectByKey<GiayToHoSo>(item.GiayToHoSo.Oid): null;
                                        vanBang.LuuTruBangDiem = item.GiayToHoSo != null ? uow.GetObjectByKey<GiayToHoSo>(item.GiayToHoSo.Oid) : null;
                                        //
                                        thinhGiang.ListVanBang.Add(vanBang);
                                    }
                                }
                            }
                            #endregion

                            #region Chứng chỉ
                            using (XPCollection<ChungChi> chungChiList = new XPCollection<ChungChi>(uow, CriteriaOperator.Parse("HoSo=?", thongTinNhanVien.Oid)))
                            {
                                foreach (ChungChi item in chungChiList)
                                {
                                    if (item.LoaiChungChi != null)
                                    {
                                        ChungChi chungChi = new ChungChi(uow);
                                        chungChi.HoSo = thinhGiang;
                                        chungChi.TenChungChi = item.TenChungChi;
                                        chungChi.XepLoai = item.XepLoai;
                                        chungChi.NoiCap = item.NoiCap;
                                        chungChi.NgayCap = item.NgayCap;
                                        chungChi.Diem = item.Diem;
                                        chungChi.LoaiChungChi = item.LoaiChungChi != null ? uow.GetObjectByKey<LoaiChungChi>(item.LoaiChungChi.Oid) : null;
                                        chungChi.GiayToHoSo = item.GiayToHoSo != null ? uow.GetObjectByKey<GiayToHoSo>(item.GiayToHoSo.Oid): null;
                                        chungChi.LoaiChungChi = item.LoaiChungChi != null ? uow.GetObjectByKey<LoaiChungChi>(item.LoaiChungChi.Oid) : null;
                                        //
                                        thinhGiang.ListChungChi.Add(chungChi);
                                    }
                                }
                            }
                            #endregion

                            #region Trình độ ngoại ngữ khác
                            using (XPCollection<TrinhDoNgoaiNguKhac> trinhDoNgoaiNguKhacList = new XPCollection<TrinhDoNgoaiNguKhac>(uow, CriteriaOperator.Parse("HoSo=?", thongTinNhanVien.Oid)))
                            {
                                foreach (TrinhDoNgoaiNguKhac item in trinhDoNgoaiNguKhacList)
                                {
                                    if (item.NgoaiNgu != null && item.TrinhDoNgoaiNgu != null)
                                    {
                                        TrinhDoNgoaiNguKhac trinhDoNgoaiNguKhac = new TrinhDoNgoaiNguKhac(uow);
                                        trinhDoNgoaiNguKhac.HoSo = thinhGiang;
                                        trinhDoNgoaiNguKhac.NgoaiNgu = item.NgoaiNgu != null ? uow.GetObjectByKey<NgoaiNgu>(item.NgoaiNgu.Oid): null;
                                        trinhDoNgoaiNguKhac.TrinhDoNgoaiNgu = item.TrinhDoNgoaiNgu != null ? uow.GetObjectByKey<TrinhDoNgoaiNgu>(item.TrinhDoNgoaiNgu.Oid) : null;
                                        trinhDoNgoaiNguKhac.Diem = item.Diem;
                                        trinhDoNgoaiNguKhac.GiayToHoSo = item.GiayToHoSo != null ? uow.GetObjectByKey<GiayToHoSo>(item.GiayToHoSo.Oid) : null;
                                        //
                                        thinhGiang.ListNgoaiNgu.Add(trinhDoNgoaiNguKhac);
                                    }
                                }
                            }
                            #endregion

                            #region Quá trình khen thưởng
                            using (XPCollection<QuaTrinhKhenThuong> quaTrinhKhenThuongList = new XPCollection<QuaTrinhKhenThuong>(uow, CriteriaOperator.Parse("HoSo=?", thongTinNhanVien.Oid)))
                            {
                                foreach (QuaTrinhKhenThuong item in quaTrinhKhenThuongList)
                                {
                                    if (item.NamHoc != null && item.DanhHieuKhenThuong != null)
                                    {
                                        QuaTrinhKhenThuong quaTrinh = new QuaTrinhKhenThuong(uow);
                                        quaTrinh.HoSo = thinhGiang;
                                        quaTrinh.QuyetDinh = item.QuyetDinh != null ? uow.GetObjectByKey<QuyetDinh.QuyetDinhKhenThuong>(item.QuyetDinh.Oid) : null;
                                        quaTrinh.QuyetDinhNgoai = item.QuyetDinhNgoai;
                                        quaTrinh.NamHoc = uow.GetObjectByKey<NamHoc>(item.NamHoc.Oid);
                                        quaTrinh.LyDo = item.LyDo;
                                        quaTrinh.NgayKhenThuong = item.NgayKhenThuong;
                                        quaTrinh.DanhHieuKhenThuong = item.DanhHieuKhenThuong != null ? uow.GetObjectByKey<DanhHieuKhenThuong>(item.DanhHieuKhenThuong.Oid) : null;
                                        //
                                    }
                                }
                            }
                            #endregion

                            #region Lịch sử bản thân
                            using (XPCollection<LichSuBanThan> lichSuBanThanList = new XPCollection<LichSuBanThan>(uow, CriteriaOperator.Parse("HoSo=?", thongTinNhanVien.Oid)))
                            {
                                foreach (LichSuBanThan item in lichSuBanThanList)
                                {
                                    LichSuBanThan lichSuBanThan = new LichSuBanThan(uow);
                                    lichSuBanThan.HoSo = thinhGiang;
                                    lichSuBanThan.STT = item.STT;
                                    lichSuBanThan.TuNam = item.TuNam;
                                    lichSuBanThan.DenNam = item.DenNam;
                                    lichSuBanThan.NoiDung = item.NoiDung;
                                    //
                                }
                            }
                            #endregion

                            #region Quá trình tham gia bảo hiểm xã hội
                            using (XPCollection<QuaTrinhThamGiaHoatDongXaHoi> quaTrinhThamGiaBHXHList = new XPCollection<QuaTrinhThamGiaHoatDongXaHoi>(uow, CriteriaOperator.Parse("HoSo=?", thongTinNhanVien.Oid)))
                            {
                                foreach (QuaTrinhThamGiaHoatDongXaHoi item in quaTrinhThamGiaBHXHList)
                                {
                                    QuaTrinhThamGiaHoatDongXaHoi quaTrinh = new QuaTrinhThamGiaHoatDongXaHoi(uow);
                                    quaTrinh.HoSo = thinhGiang;
                                    quaTrinh.STT = item.STT;
                                    quaTrinh.TuNam = item.TuNam;
                                    quaTrinh.DenNam = item.DenNam;
                                    quaTrinh.NoiDung = item.NoiDung;
                                }
                            }
                            #endregion

                            #region Quá trình nghiên cứu khoa học
                            using (XPCollection<QuaTrinhNghienCuuKhoaHoc> quaTrinhNghienCuuKhoaHocList = new XPCollection<QuaTrinhNghienCuuKhoaHoc>(uow, CriteriaOperator.Parse("HoSo=?", thongTinNhanVien.Oid)))
                            {
                                foreach (QuaTrinhNghienCuuKhoaHoc item in quaTrinhNghienCuuKhoaHocList)
                                {
                                    QuaTrinhNghienCuuKhoaHoc quaTrinh = new QuaTrinhNghienCuuKhoaHoc(uow);
                                    quaTrinh.CapQuanLy = item.CapQuanLy;
                                    quaTrinh.ChucDanhThamGia = item.ChucDanhThamGia;
                                    quaTrinh.CoQuanChuTri = item.CoQuanChuTri;
                                    quaTrinh.DenNam = item.DenNam;
                                    quaTrinh.TuNam = item.TuNam;
                                    quaTrinh.STT = item.STT;
                                    quaTrinh.TenDeTai = item.TenDeTai;
                                    quaTrinh.NgayNghiemThu = item.NgayNghiemThu;
                                    quaTrinh.NoiQuanLyKetQua = item.NoiQuanLyKetQua;
                                    quaTrinh.HoSo = thinhGiang;
                                    //
                                }
                            }
                            #endregion

                            #region Quá trình công tác
                            using (XPCollection<QuaTrinhCongTac> quaTrinhCongTacList = new XPCollection<QuaTrinhCongTac>(uow, CriteriaOperator.Parse("HoSo=?", thongTinNhanVien.Oid)))
                            {
                                foreach (QuaTrinhCongTac item in quaTrinhCongTacList)
                                {
                                    QuaTrinhCongTac quaTrinh = new QuaTrinhCongTac(uow);
                                    quaTrinh.HoSo = thinhGiang;
                                    quaTrinh.TuNam = item.TuNam;
                                    quaTrinh.DenNam = item.DenNam;
                                    quaTrinh.STT = item.STT;
                                    quaTrinh.NoiDung = item.NoiDung;
                                    quaTrinh.QuyetDinh = item.QuyetDinh != null ? uow.GetObjectByKey<QuyetDinh.QuyetDinh>(item.QuyetDinh.Oid) : null;
                                    //
                                }
                            }
                            #endregion

                           
                            //Tiến hành lưu dữ liệu
                            uow.CommitChanges();
                            //
                            DialogUtil.ShowInfo(String.Format("Đã tạo hồ sơ thỉnh giảng cho cán bộ {0}.", thongTinNhanVien.HoTen));
                        }
                        catch (Exception ex)
                        {
                            //
                            uow.RollbackTransaction();
                            //
                            DialogUtil.ShowError(String.Format("Không thể tạo hồ sơ thỉnh giảng của cán bộ [{0}]. Vì {1}", thongTinNhanVien.HoTen, ex.Message));
                        }
                    }
                    else
                    {
                        DialogUtil.ShowError(String.Format("Hồ sơ của cán bộ [{0}] đã tồn tại trong thỉnh giảng.", thongTinNhanVien.HoTen));
                    }
                }
            }
        }
    }

}
