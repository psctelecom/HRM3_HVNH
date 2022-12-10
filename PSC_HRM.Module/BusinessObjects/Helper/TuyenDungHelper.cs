using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuaTrinh;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.TuyenDung
{
    public class TuyenDungHelper
    {
        private static TinhTrang GetTinhTrang(IObjectSpace obs)
        {
            TinhTrang obj = obs.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
            if (obj == null)
            {
                obj = obs.CreateObject<TinhTrang>();
                obj.MaQuanLy = "01";
                obj.TenTinhTrang = "Đang làm việc";
            }
            return obj;
        }

        /// <summary>
        /// Create nhan vien from ung vien
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="item"></param>
        /// <param name="tinhTrang"></param>
        /// <returns></returns>
        public static ThongTinNhanVien HoSoNhanVien(IObjectSpace obs, TrungTuyen item)
        {
            ThongTinNhanVien nhanVien = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("HoTen=? and CMND=? and BoPhan=?", item.UngVien.HoTen, item.UngVien.CMND, item.NhuCauTuyenDung.BoPhan.Oid));
            if (nhanVien == null)
            {
                nhanVien = obs.CreateObject<ThongTinNhanVien>();
                nhanVien.BoPhan = obs.GetObjectByKey<BoPhan>(item.NhuCauTuyenDung.BoPhan.Oid);
                nhanVien.MaQuanLy = item.UngVien.SoBaoDanh;

                nhanVien.Ho = item.UngVien.Ho;
                nhanVien.Ten = item.UngVien.Ten;
                nhanVien.GioiTinh = item.UngVien.GioiTinh;
                nhanVien.NgaySinh = item.UngVien.NgaySinh;
                if (item.UngVien.NoiSinh != null)
                    nhanVien.NoiSinh = obs.GetObjectByKey<DiaChi>(item.UngVien.NoiSinh.Oid);
                //if (item.NhuCauTuyenDung.ViTriTuyenDung.PhanLoai == LoaiNhanVienEnum.BienChe)
                //    nhanVien.BienChe = true;
                if (item.NhuCauTuyenDung.ViTriTuyenDung.LoaiTuyenDung.TenLoaiTuyenDung == "Biên chế")
                    nhanVien.BienChe = true;
                nhanVien.LoaiNhanVien = obs.FindObject<LoaiNhanVien>(CriteriaOperator.Parse("TenLoaiNhanVien like ?", "%tập sự%"));
                if (item.UngVien.QueQuan != null)
                    nhanVien.QueQuan = obs.GetObjectByKey<DiaChi>(item.UngVien.QueQuan.Oid);
                if (item.UngVien.DiaChiThuongTru != null)
                    nhanVien.DiaChiThuongTru = obs.GetObjectByKey<DiaChi>(item.UngVien.DiaChiThuongTru.Oid);
                if (item.UngVien.NoiOHienNay != null)
                    nhanVien.NoiOHienNay = HamDungChung.Copy<DiaChi>(((XPObjectSpace)obs).Session, item.UngVien.NoiOHienNay);
                nhanVien.CMND = item.UngVien.CMND_UngVien;
                nhanVien.NgayCap = item.UngVien.NgayCap;
                if (item.UngVien.NoiCap != null)
                    nhanVien.NoiCap = obs.GetObjectByKey<TinhThanh>(item.UngVien.NoiCap.Oid);
                nhanVien.DienThoaiNhaRieng = item.UngVien.DienThoaiNhaRieng;
                nhanVien.DienThoaiDiDong = item.UngVien.DienThoaiDiDong;
                nhanVien.Email = item.UngVien.Email;
                nhanVien.TinhTrang = GetTinhTrang(obs);
                nhanVien.HinhThucTuyenDung = item.UngVien.HinhThucTuyenDung;
                nhanVien.CMND = item.UngVien.CMND_UngVien;
                if (item.NhuCauTuyenDung.ViTriTuyenDung.TenViTriTuyenDung.ToLower().Contains("giảng viên"))
                {
                    nhanVien.CongViecTuyenDung = "Giảng viên";
                    nhanVien.CongViecDuocGiao = obs.FindObject<CongViec>(CriteriaOperator.Parse("TenCongViec like ?", "%Giảng viên%"));
                    nhanVien.CongViecHienNay = nhanVien.CongViecDuocGiao;
                    nhanVien.LoaiNhanSu = obs.FindObject<LoaiNhanSu>(CriteriaOperator.Parse("TenLoaiNhanSu like ?", "%Giảng viên%"));
                }
                else
                {
                    nhanVien.CongViecTuyenDung = "Nhân viên";
                    nhanVien.CongViecDuocGiao = obs.FindObject<CongViec>(CriteriaOperator.Parse("TenCongViec like ?", "%Nhân viên%"));
                    nhanVien.CongViecHienNay = nhanVien.CongViecDuocGiao;
                    nhanVien.LoaiNhanSu = obs.FindObject<LoaiNhanSu>(CriteriaOperator.Parse("TenLoaiNhanSu like ?", "%Nhân viên%"));
                }
                nhanVien.DonViTuyenDung = nhanVien.BoPhan.ThongTinTruong.TenBoPhan;
                if (item.UngVien.TinhTrangHonNhan != null)
                    nhanVien.TinhTrangHonNhan = obs.GetObjectByKey<TinhTrangHonNhan>(item.UngVien.TinhTrangHonNhan.Oid);
                nhanVien.GhiChu = item.UngVien.GhiChu;
                if (item.UngVien.DanToc != null)
                    nhanVien.DanToc = obs.GetObjectByKey<DanToc>(item.UngVien.DanToc.Oid);
                if (item.UngVien.TonGiao != null)
                    nhanVien.TonGiao = obs.GetObjectByKey<TonGiao>(item.UngVien.TonGiao.Oid);
                if (item.UngVien.TrinhDoChuyenMon != null)
                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = obs.GetObjectByKey<TrinhDoChuyenMon>(item.UngVien.TrinhDoChuyenMon.Oid);
                if (item.UngVien.ChuyenMonDaoTao != null)
                    nhanVien.NhanVienTrinhDo.ChuyenMonDaoTao = obs.GetObjectByKey<ChuyenMonDaoTao>(item.UngVien.ChuyenMonDaoTao.Oid);
                if (item.UngVien.TruongDaoTao != null)
                    nhanVien.NhanVienTrinhDo.TruongDaoTao = obs.GetObjectByKey<TruongDaoTao>(item.UngVien.TruongDaoTao.Oid);
                if (item.UngVien.HinhThucDaoTao != null)
                    nhanVien.NhanVienTrinhDo.HinhThucDaoTao = obs.GetObjectByKey<HinhThucDaoTao>(item.UngVien.HinhThucDaoTao.Oid);
                nhanVien.NhanVienTrinhDo.NamTotNghiep = item.UngVien.NamTotNghiep;
                if (item.UngVien.ChuongTrinhHoc != null)
                    nhanVien.NhanVienTrinhDo.ChuongTrinhHoc = obs.GetObjectByKey<ChuongTrinhHoc>(item.UngVien.ChuongTrinhHoc.Oid);
                if (item.UngVien.TrinhDoTinHoc != null)
                    nhanVien.NhanVienTrinhDo.TrinhDoTinHoc = obs.GetObjectByKey<TrinhDoTinHoc>(item.UngVien.TrinhDoTinHoc.Oid);
                if (item.UngVien.NgoaiNgu != null)
                    nhanVien.NhanVienTrinhDo.NgoaiNgu = obs.GetObjectByKey<NgoaiNgu>(item.UngVien.NgoaiNgu.Oid);
                if (item.UngVien.TrinhDoNgoaiNgu != null)
                    nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu = obs.GetObjectByKey<TrinhDoNgoaiNgu>(item.UngVien.TrinhDoNgoaiNgu.Oid);
               
                //copy lịch sử bản thân
                CopyLichSuBanThan(obs, item.UngVien, nhanVien);
                //copy quá trình tham gia hoạt động xã hội
                CopyQuaTrinhThamGiaHoatDongXaHoi(obs, item.UngVien, nhanVien);
                //copy quá trình nghiên cứu khoa học
                CopyQuaTrinhNghienCuuKhoaHoc(obs, item.UngVien, nhanVien);
                //copy quá trình khen thưởng
                CopyQuaTrinhKhenThuong(obs, item.UngVien, nhanVien);
                //copy quá trình công tác
                CopyQuaTrinhCongTac(obs, item.UngVien, nhanVien);
                //copy ngoại ngữ
                CopyTrinhDoNgoaiNguKhac(obs, item.UngVien, nhanVien);
                //copy chứng chỉ
                CopyChungChi(obs, item.UngVien, nhanVien);
                //copy bằng cấp
                CopyVanBang(obs, item.UngVien, nhanVien);
            }
            return nhanVien;
        }

        /// <summary>
        /// Create giang vien thinh giang from ung vien
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="item"></param>
        /// <param name="tinhTrang"></param>
        /// <returns></returns>
        public static GiangVienThinhGiang HoSoGiangVienThinhGiang(IObjectSpace obs, TrungTuyen item)
        {
            GiangVienThinhGiang thinhGiang = obs.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("HoTen=? and CMND=? and BoPhan=?", item.UngVien.HoTen, item.UngVien.CMND, item.NhuCauTuyenDung.BoPhan.Oid));
            if (thinhGiang == null)
            {
                thinhGiang = obs.CreateObject<GiangVienThinhGiang>();
                thinhGiang.BoPhan = obs.GetObjectByKey<BoPhan>(item.NhuCauTuyenDung.BoPhan.Oid);
                thinhGiang.Ho = item.UngVien.Ho;
                thinhGiang.Ten = item.UngVien.Ten;
                thinhGiang.GioiTinh = item.UngVien.GioiTinh;
                thinhGiang.NgaySinh = item.UngVien.NgaySinh;
                if (item.UngVien.NoiSinh != null)
                    thinhGiang.NoiSinh = HamDungChung.Copy<DiaChi>(((XPObjectSpace)obs).Session, item.UngVien.NoiSinh);
                if (item.UngVien.QueQuan != null)
                    thinhGiang.QueQuan = HamDungChung.Copy<DiaChi>(((XPObjectSpace)obs).Session, item.UngVien.QueQuan);
                if (item.UngVien.DiaChiThuongTru != null)
                    thinhGiang.DiaChiThuongTru = HamDungChung.Copy<DiaChi>(((XPObjectSpace)obs).Session, item.UngVien.DiaChiThuongTru);
                if (item.UngVien.NoiOHienNay != null)
                    thinhGiang.NoiOHienNay = HamDungChung.Copy<DiaChi>(((XPObjectSpace)obs).Session, item.UngVien.NoiOHienNay);
                thinhGiang.CMND = item.UngVien.CMND_UngVien;
                thinhGiang.NgayCap = item.UngVien.NgayCap;
                if (item.UngVien.NoiCap != null)
                    thinhGiang.NoiCap = obs.GetObjectByKey<TinhThanh>(item.UngVien.NoiCap.Oid);
                thinhGiang.DienThoaiNhaRieng = item.UngVien.DienThoaiNhaRieng;
                thinhGiang.DienThoaiDiDong = item.UngVien.DienThoaiDiDong;
                thinhGiang.Email = item.UngVien.Email;
                thinhGiang.TinhTrang = GetTinhTrang(obs);
                thinhGiang.HinhThucTuyenDung = item.UngVien.HinhThucTuyenDung;
                thinhGiang.CongViecTuyenDung = "Giảng viên thỉnh giảng";
                thinhGiang.CongViecDuocGiao = obs.FindObject<CongViec>(CriteriaOperator.Parse("TenCongViec like ?", "%Giảng viên%"));
                thinhGiang.CongViecHienNay = thinhGiang.CongViecDuocGiao;
                thinhGiang.DonViTuyenDung = thinhGiang.BoPhan.ThongTinTruong.TenBoPhan;
                if (item.UngVien.TinhTrangHonNhan != null)
                    thinhGiang.TinhTrangHonNhan = obs.GetObjectByKey<TinhTrangHonNhan>(item.UngVien.TinhTrangHonNhan.Oid);
                thinhGiang.GhiChu = item.UngVien.GhiChu;
                if (item.UngVien.DanToc != null)
                    thinhGiang.DanToc = obs.GetObjectByKey<DanToc>(item.UngVien.DanToc.Oid);
                if (item.UngVien.TonGiao != null)
                    thinhGiang.TonGiao = obs.GetObjectByKey<TonGiao>(item.UngVien.TonGiao.Oid);
                if (item.UngVien.TrinhDoChuyenMon != null)
                    thinhGiang.NhanVienTrinhDo.TrinhDoChuyenMon = obs.GetObjectByKey<TrinhDoChuyenMon>(item.UngVien.TrinhDoChuyenMon.Oid);
                if (item.UngVien.ChuyenMonDaoTao != null)
                    thinhGiang.NhanVienTrinhDo.ChuyenMonDaoTao = obs.GetObjectByKey<ChuyenMonDaoTao>(item.UngVien.ChuyenMonDaoTao.Oid);
                if (item.UngVien.TruongDaoTao != null)
                    thinhGiang.NhanVienTrinhDo.TruongDaoTao = obs.GetObjectByKey<TruongDaoTao>(item.UngVien.TruongDaoTao.Oid);
                if (item.UngVien.HinhThucDaoTao != null)
                    thinhGiang.NhanVienTrinhDo.HinhThucDaoTao = obs.GetObjectByKey<HinhThucDaoTao>(item.UngVien.HinhThucDaoTao.Oid);
                thinhGiang.NhanVienTrinhDo.NamTotNghiep = item.UngVien.NamTotNghiep;
                if (item.UngVien.ChuongTrinhHoc != null)
                    thinhGiang.NhanVienTrinhDo.ChuongTrinhHoc = obs.GetObjectByKey<ChuongTrinhHoc>(item.UngVien.ChuongTrinhHoc.Oid);
                if (item.UngVien.TrinhDoTinHoc != null)
                    thinhGiang.NhanVienTrinhDo.TrinhDoTinHoc = obs.GetObjectByKey<TrinhDoTinHoc>(item.UngVien.TrinhDoTinHoc.Oid);
                if (item.UngVien.NgoaiNgu != null)
                    thinhGiang.NhanVienTrinhDo.NgoaiNgu = obs.GetObjectByKey<NgoaiNgu>(item.UngVien.NgoaiNgu.Oid);
                if (item.UngVien.TrinhDoNgoaiNgu != null)
                    thinhGiang.NhanVienTrinhDo.TrinhDoNgoaiNgu = obs.GetObjectByKey<TrinhDoNgoaiNgu>(item.UngVien.TrinhDoNgoaiNgu.Oid);

                //copy lịch sử bản thân
                CopyLichSuBanThan(obs, item.UngVien, thinhGiang);
                //copy quá trình tham gia hoạt động xã hội
                CopyQuaTrinhThamGiaHoatDongXaHoi(obs, item.UngVien, thinhGiang);
                //copy quá trình nghiên cứu khoa học
                CopyQuaTrinhNghienCuuKhoaHoc(obs, item.UngVien, thinhGiang);
                //copy quá trình khen thưởng
                CopyQuaTrinhKhenThuong(obs, item.UngVien, thinhGiang);
                //copy quá trình công tác
                CopyQuaTrinhCongTac(obs, item.UngVien, thinhGiang);
                //copy ngoại ngữ
                CopyTrinhDoNgoaiNguKhac(obs, item.UngVien, thinhGiang);
                //copy chứng chỉ
                CopyChungChi(obs, item.UngVien, thinhGiang);
                //copy bằng cấp
                CopyVanBang(obs, item.UngVien, thinhGiang);
            }
            return thinhGiang;
        }

        /// <summary>
        /// Trúng tuyển
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="qlTuyenDung"></param>
        public static void TrungTuyen(IObjectSpace obs, QuanLyTuyenDung qlTuyenDung)
        {
            if (qlTuyenDung != null)
            {
                XPCollection<NhuCauTuyenDung> lstNhuCau = new XPCollection<NhuCauTuyenDung>(((XPObjectSpace)obs).Session);
                lstNhuCau.Criteria = CriteriaOperator.Parse("QuanLyTuyenDung=?", qlTuyenDung.Oid);

                foreach (NhuCauTuyenDung itemNhuCau in lstNhuCau)
                {
                    TrungTuyen trungTuyen;
                    object obj = ((XPObjectSpace)obs).Session.Evaluate<BuocTuyenDung>(CriteriaOperator.Parse("Max(ThuTu)"), CriteriaOperator.Parse("ChiTietTuyenDung.QuanLyTuyenDung=? and ChiTietTuyenDung.ViTriTuyenDung=?", qlTuyenDung.Oid, itemNhuCau.ViTriTuyenDung));
                    if (obj != null)
                    {
                        CriteriaOperator filter;
                        SortProperty sort = new SortProperty("Diem", DevExpress.Xpo.DB.SortingDirection.Descending);
                        XPCollection<ChiTietVongTuyenDung> ctVongTuyenDung;
                        int soLuong;

                        foreach (NhuCauTuyenDung item in qlTuyenDung.ListNhuCauTuyenDung)
                        {
                            filter = CriteriaOperator.Parse("VongTuyenDung.BuocTuyenDung.ThuTu=? and VongTuyenDung.ChiTietTuyenDung.QuanLyTuyenDung=? and UngVien.NhuCauTuyenDung=? and DuocChuyenQuaVongSau",
                               obj, qlTuyenDung.Oid, item.Oid);
                            ctVongTuyenDung = new XPCollection<ChiTietVongTuyenDung>(((XPObjectSpace)obs).Session, filter, sort);

                            soLuong = 0;
                            foreach (ChiTietVongTuyenDung ctItem in ctVongTuyenDung)
                            {
                                trungTuyen = obs.FindObject<TrungTuyen>(CriteriaOperator.Parse("QuanLyTuyenDung=? and UngVien=?",
                                    qlTuyenDung.Oid, ctItem.UngVien.Oid));
                                if (trungTuyen == null)
                                {
                                    trungTuyen = obs.CreateObject<TrungTuyen>();
                                    trungTuyen.QuanLyTuyenDung = qlTuyenDung;
                                    trungTuyen.UngVien = ctItem.UngVien;
                                }
                                soLuong++;

                                //lấy từ trên xuống dưới, đủ số lượng thì dừng
                                if (soLuong > item.SoLuongTuyen)
                                    break;
                            }
                        }
                        obs.CommitChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Create buoc tuyen dung
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="chiTiet"></param>
        /// <param name="thuTu"></param>
        /// <param name="tenBuocTuyenDung"></param>
        /// <param name="thangDiem"></param>
        public static void CreateBuocTuyenDung(IObjectSpace obs, ChiTietTuyenDung chiTiet, int thuTu, string tenBuocTuyenDung, int thangDiem)
        {
            BuocTuyenDung buocTuyenDung = obs.FindObject<BuocTuyenDung>(CriteriaOperator.Parse("ChiTietTuyenDung=? and TenBuocTuyenDung like ?", chiTiet.Oid, tenBuocTuyenDung));
            if (buocTuyenDung == null)
            {
                buocTuyenDung = obs.CreateObject<BuocTuyenDung>();
                buocTuyenDung.ThuTu = thuTu;
                buocTuyenDung.TenBuocTuyenDung = tenBuocTuyenDung;
                buocTuyenDung.ThangDiem = thangDiem;
                if (tenBuocTuyenDung.ToLower().Contains("thi"))
                    buocTuyenDung.CoToChucThiTuyen = true;
                chiTiet.ListBuocTuyenDung.Add(buocTuyenDung);
            }
        }

        /// <summary>
        /// Create vi tri tuyen dung
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quanLy"></param>
        /// <param name="maQuanLy"></param>
        /// <param name="tenViTriTuyendung"></param>
        /// <param name="phanLoai"></param>
        //public static void CreateViTriTuyenDung(Session session, QuanLyTuyenDung quanLy, string maQuanLy, string tenViTriTuyendung, LoaiNhanVienEnum phanLoai)
        //{
        //    ViTriTuyenDung viTri = session.FindObject<ViTriTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung=? and TenViTriTuyenDung like ? and PhanLoai=?", quanLy.Oid, tenViTriTuyendung, phanLoai));
        //    if (viTri == null)
        //    {
        //        viTri = new ViTriTuyenDung(session);
        //        viTri.QuanLyTuyenDung = quanLy;
        //        viTri.MaQuanLy = maQuanLy;
        //        viTri.TenViTriTuyenDung = tenViTriTuyendung;
        //        viTri.PhanLoai = phanLoai;
        //    }
        //}

        public static void CreateViTriTuyenDung(Session session, QuanLyTuyenDung quanLy, string maQuanLy, string tenViTriTuyendung, LoaiTuyenDung loaiTuyenDung)
        {
            ViTriTuyenDung viTri = session.FindObject<ViTriTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung=? and TenViTriTuyenDung like ? and LoaiTuyenDung=?", quanLy.Oid, tenViTriTuyendung, loaiTuyenDung.Oid));
            if (viTri == null)
            {
                viTri = new ViTriTuyenDung(session);
                viTri.QuanLyTuyenDung = quanLy;
                viTri.MaQuanLy = maQuanLy;
                viTri.TenViTriTuyenDung = tenViTriTuyendung;
                viTri.LoaiTuyenDung = loaiTuyenDung;
            }
        }

        /// <summary>
        /// copy lich su ban than
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="ungVien"></param>
        /// <param name="hoSo"></param>
        private static void CopyLichSuBanThan(IObjectSpace obs, UngVien ungVien, HoSo.HoSo hoSo)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=?", ungVien.Oid);
            using (XPCollection<LichSuBanThan> lichSuBanThan = new XPCollection<LichSuBanThan>(((XPObjectSpace)obs).Session, filter))
            {
                LichSuBanThan obj;
                foreach (LichSuBanThan lsItem in lichSuBanThan)
                {
                    obj = obs.CreateObject<LichSuBanThan>();
                    obj.HoSo = hoSo;
                    obj.STT = lsItem.STT;
                    obj.TuNam = lsItem.TuNam;
                    obj.DenNam = lsItem.DenNam;
                    obj.NoiDung = lsItem.NoiDung;
                }
            }
        }

        private static void CopyQuaTrinhThamGiaHoatDongXaHoi(IObjectSpace obs, UngVien ungVien, HoSo.HoSo hoSo)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=?", ungVien.Oid);
            using (XPCollection<QuaTrinhThamGiaHoatDongXaHoi> data = new XPCollection<QuaTrinhThamGiaHoatDongXaHoi>(((XPObjectSpace)obs).Session, filter))
            {
                QuaTrinhThamGiaHoatDongXaHoi obj;
                foreach (QuaTrinhThamGiaHoatDongXaHoi item in data)
                {
                    obj = obs.CreateObject<QuaTrinhThamGiaHoatDongXaHoi>();
                    obj.HoSo = hoSo;
                    obj.STT = item.STT;
                    obj.TuNam = item.TuNam;
                    obj.DenNam = item.DenNam;
                    obj.NoiDung = item.NoiDung;
                }
            }
        }

        private static void CopyQuaTrinhNghienCuuKhoaHoc(IObjectSpace obs, UngVien ungVien, HoSo.HoSo hoSo)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=?", ungVien.Oid);
            using (XPCollection<QuaTrinhNghienCuuKhoaHoc> data = new XPCollection<QuaTrinhNghienCuuKhoaHoc>(((XPObjectSpace)obs).Session, filter))
            {
                QuaTrinhNghienCuuKhoaHoc obj;
                foreach (QuaTrinhNghienCuuKhoaHoc item in data)
                {
                    obj = obs.CreateObject<QuaTrinhNghienCuuKhoaHoc>();
                    obj.HoSo = hoSo;
                    obj.STT = item.STT;
                    obj.TenDeTai = item.TenDeTai;
                    obj.ChucDanhThamGia = item.ChucDanhThamGia;
                    obj.CoQuanChuTri = item.CoQuanChuTri;
                    obj.CapQuanLy = item.CapQuanLy;
                    obj.TuNam = item.TuNam;
                    obj.DenNam = item.DenNam;
                    obj.NgayNghiemThu = item.NgayNghiemThu;
                    obj.NoiQuanLyKetQua = item.NoiQuanLyKetQua;
                }
            }
        }

        private static void CopyQuaTrinhKhenThuong(IObjectSpace obs, UngVien ungVien, HoSo.HoSo hoSo)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=?", ungVien.Oid);
            using (XPCollection<QuaTrinhKhenThuong> data = new XPCollection<QuaTrinhKhenThuong>(((XPObjectSpace)obs).Session, filter))
            {
                QuaTrinhKhenThuong obj;
                foreach (QuaTrinhKhenThuong item in data)
                {
                    obj = obs.CreateObject<QuaTrinhKhenThuong>();
                    obj.HoSo = hoSo;
                    if (item.DanhHieuKhenThuong != null)
                        obj.DanhHieuKhenThuong = obs.GetObjectByKey<DanhHieuKhenThuong>(item.DanhHieuKhenThuong.Oid);
                    if (item.NamHoc != null)
                        obj.NamHoc = obs.GetObjectByKey<NamHoc>(item.NamHoc.Oid);
                    obj.NgayKhenThuong = item.NgayKhenThuong;
                    obj.LyDo = item.LyDo;
                }
            }
        }

        private static void CopyQuaTrinhCongTac(IObjectSpace obs, UngVien ungVien, HoSo.HoSo hoSo)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=?", ungVien.Oid);
            using (XPCollection<QuaTrinhCongTac> data = new XPCollection<QuaTrinhCongTac>(((XPObjectSpace)obs).Session, filter))
            {
                QuaTrinhCongTac obj;
                foreach (QuaTrinhCongTac item in data)
                {
                    obj = obs.CreateObject<QuaTrinhCongTac>();
                    obj.HoSo = hoSo;
                    obj.STT = item.STT;
                    obj.TuNam = item.TuNam;
                    obj.DenNam = item.DenNam;
                    obj.NoiDung = item.NoiDung;
                }
            }
        }

        private static void CopyTrinhDoNgoaiNguKhac(IObjectSpace obs, UngVien ungVien, HoSo.HoSo hoSo)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=?", ungVien.Oid);
            TrinhDoNgoaiNguKhac obj;
            foreach (TrinhDoNgoaiNguKhac item in ungVien.ListNgoaiNgu)
            {
                obj = obs.CreateObject<TrinhDoNgoaiNguKhac>();
                obj.HoSo = hoSo;
                if (item.NgoaiNgu != null)
                    obj.NgoaiNgu = obs.GetObjectByKey<NgoaiNgu>(item.NgoaiNgu.Oid);
                if (item.TrinhDoNgoaiNgu != null)
                    obj.TrinhDoNgoaiNgu = obs.GetObjectByKey<TrinhDoNgoaiNgu>(item.TrinhDoNgoaiNgu.Oid);
                obj.Diem = item.Diem;
                if (item.GiayToHoSo != null)
                    obj.GiayToHoSo = HamDungChung.Copy<GiayToHoSo>(((XPObjectSpace)obs).Session, item.GiayToHoSo);
            }
        }

        private static void CopyChungChi(IObjectSpace obs, UngVien ungVien, HoSo.HoSo hoSo)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=?", ungVien.Oid);
            ChungChi obj;
            foreach (ChungChi item in ungVien.ListChungChi)
            {
                obj = obs.CreateObject<ChungChi>();
                obj.HoSo = hoSo;
                if (item.LoaiChungChi != null)
                    obj.LoaiChungChi = obs.GetObjectByKey<LoaiChungChi>(item.LoaiChungChi.Oid);
                obj.TenChungChi = item.TenChungChi;
                obj.NoiCap = item.NoiCap;
                obj.NgayCap = item.NgayCap;
                obj.XepLoai = item.XepLoai;
                obj.Diem = item.Diem;
                if (item.GiayToHoSo != null)
                    obj.GiayToHoSo = HamDungChung.Copy<GiayToHoSo>(((XPObjectSpace)obs).Session, item.GiayToHoSo);
            }
        }

        private static void CopyVanBang(IObjectSpace obs, UngVien ungVien, HoSo.HoSo hoSo)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=?", ungVien.Oid);
            VanBang obj;
            foreach (VanBang item in ungVien.ListVanBang)
            {
                obj = obs.CreateObject<VanBang>();
                obj.HoSo = hoSo;
                if (item.TrinhDoChuyenMon != null)
                    obj.TrinhDoChuyenMon = obs.GetObjectByKey<TrinhDoChuyenMon>(item.TrinhDoChuyenMon.Oid);
                if (item.ChuyenMonDaoTao != null)
                    obj.ChuyenMonDaoTao = obs.GetObjectByKey<ChuyenMonDaoTao>(item.ChuyenMonDaoTao.Oid);
                if (item.TruongDaoTao != null)
                    obj.TruongDaoTao = obs.GetObjectByKey<TruongDaoTao>(item.TruongDaoTao.Oid);
                if (item.HinhThucDaoTao != null)
                    obj.HinhThucDaoTao = obs.GetObjectByKey<HinhThucDaoTao>(item.HinhThucDaoTao.Oid);
                obj.NamTotNghiep = item.NamTotNghiep;
                obj.XepLoai = item.XepLoai;
                obj.DiemTrungBinh = item.DiemTrungBinh;
                if (item.LuuTruBangDiem != null)
                    obj.LuuTruBangDiem = HamDungChung.Copy<GiayToHoSo>(((XPObjectSpace)obs).Session, item.LuuTruBangDiem);
                if (item.GiayToHoSo != null)
                    obj.GiayToHoSo = HamDungChung.Copy<GiayToHoSo>(((XPObjectSpace)obs).Session, item.GiayToHoSo);
            }
        }
    }
}
