using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuaTrinh
{
    public static class QuaTrinhHelper
    {
        /// <summary>
        /// Create qua trinh cong tac
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="noiDung"></param>
        public static void CreateQuaTrinhCongTac(Session session, QuyetDinhCaNhan quyetDinh, string noiDung)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and QuyetDinh=?",
                    quyetDinh.ThongTinNhanVien, quyetDinh);
            QuaTrinhCongTac quaTrinhCongTac = session.FindObject<QuaTrinhCongTac>(filter);
            if (quaTrinhCongTac == null)
            {
                quaTrinhCongTac = new QuaTrinhCongTac(session);
                quaTrinhCongTac.HoSo = quyetDinh.ThongTinNhanVien;
                quaTrinhCongTac.QuyetDinh = quyetDinh;
            }
            quaTrinhCongTac.TuNam = quyetDinh.NgayHieuLuc.ToString("d");
            quaTrinhCongTac.NoiDung = noiDung;
        }

        public static void CreateQuaTrinhCongTac(Session session, ThongTinNhanVien nhanVien, QuyetDinh.QuyetDinh quyetDinh)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and QuyetDinh=?",
                    nhanVien, quyetDinh);
            QuaTrinhCongTac quaTrinhCongTac = session.FindObject<QuaTrinhCongTac>(filter);
            if (quaTrinhCongTac == null)
            {
                quaTrinhCongTac = new QuaTrinhCongTac(session);
                quaTrinhCongTac.HoSo = nhanVien;
                quaTrinhCongTac.QuyetDinh = quyetDinh;
            }
            quaTrinhCongTac.TuNam = quyetDinh.NgayHieuLuc.ToString("d");
            quaTrinhCongTac.NoiDung = quyetDinh.NoiDung;
        }

        /// <summary>
        /// Update qua trinh cong tac
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void UpdateQuaTrinhCongTac(Session session, QuyetDinhCaNhan quyetDinh, DateTime ngay)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and QuyetDinh=?",
                    quyetDinh.ThongTinNhanVien, quyetDinh);
            QuaTrinhCongTac qtCongTac = session.FindObject<QuaTrinhCongTac>(filter);
            if (qtCongTac != null)
            {
                qtCongTac.DenNam = ngay.AddDays(-1).ToString("d");
            }
        }

        /// <summary>
        /// reset qua trinh cong tac
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void ResetQuaTrinhCongTac(Session session, QuyetDinhCaNhan quyetDinh)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and QuyetDinh=?",
                    quyetDinh.ThongTinNhanVien, quyetDinh);
            QuaTrinhCongTac qtCongTac = session.FindObject<QuaTrinhCongTac>(filter);
            if (qtCongTac != null)
            {
                qtCongTac.DenNam = null;
            }
        }


        /// <summary>
        /// Create qua trinh bo nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="chucVu"></param>
        /// <param name="hspcChucVu"></param>
        /// <param name="ngayHuong"></param>
        public static void CreateQuaTrinhBoNhiem(Session session, QuyetDinhCaNhan quyetDinh, ChucVu chucVu, decimal hspcChucVu, DateTime ngayHuong)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?",
                    quyetDinh, quyetDinh.ThongTinNhanVien);
            QuaTrinhBoNhiem quaTrinhBoNhiem = session.FindObject<QuaTrinhBoNhiem>(filter);
            if (quaTrinhBoNhiem == null)
            {
                quaTrinhBoNhiem = new QuaTrinhBoNhiem(session);
                quaTrinhBoNhiem.QuyetDinh = quyetDinh;
                quaTrinhBoNhiem.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
            }
            quaTrinhBoNhiem.ChucVu = chucVu;
            quaTrinhBoNhiem.HeSoPhuCapChucVu = hspcChucVu;
            quaTrinhBoNhiem.NgayHuongHeSo = ngayHuong;
            quaTrinhBoNhiem.TuNam = quyetDinh.NgayHieuLuc.ToString("d");
        }

        /// <summary>
        /// Update qua trinh bo nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void UpdateQuaTrinhBoNhiem(Session session, QuyetDinhCaNhan quyetDinh, DateTime ngay)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?",
                    quyetDinh, quyetDinh.ThongTinNhanVien);
            QuaTrinhBoNhiem quaTrinhBoNhiem = session.FindObject<QuaTrinhBoNhiem>(filter);
            if (quaTrinhBoNhiem != null)
            {
                quaTrinhBoNhiem.DenNam = ngay.AddDays(-1).ToString("d");
            }
        }

        /// <summary>
        /// Reset qua trinh bo nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void ResetQuaTrinhBoNhiem(Session session, QuyetDinhCaNhan quyetDinh)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?",
                    quyetDinh, quyetDinh.ThongTinNhanVien);
            QuaTrinhBoNhiem quaTrinhBoNhiem = session.FindObject<QuaTrinhBoNhiem>(filter);
            if (quaTrinhBoNhiem != null)
            {
                quaTrinhBoNhiem.DenNam = null;
            }
        }

        /// <summary>
        /// Create qua trinh dao tao
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="truong"></param>
        public static void CreateQuaTrinhDaoTao(Session session, QuyetDinhDaoTao quyetDinh, ThongTinNhanVien nhanVien)
        {
            QuaTrinhDaoTao quaTrinhDaoTao;
            CriteriaOperator filter;
            if (quyetDinh != null)
            {
                ChiTietDaoTao item = DaoTaoHelper.GetChiTietDaoTao(session, quyetDinh, nhanVien);
                if (item != null)
                {
                    filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh=?",
                        item.ThongTinNhanVien, quyetDinh);
                    quaTrinhDaoTao = session.FindObject<QuaTrinhDaoTao>(filter);
                    if (quaTrinhDaoTao == null)
                    {
                        quaTrinhDaoTao = new QuaTrinhDaoTao(session);
                        quaTrinhDaoTao.ThongTinNhanVien = item.ThongTinNhanVien;
                        quaTrinhDaoTao.QuyetDinh = quyetDinh;
                    }

                    quaTrinhDaoTao.NamNhapHoc = quyetDinh.KhoaDaoTao != null ? quyetDinh.KhoaDaoTao.TuNam : quyetDinh.TuNgay.Year;
                    quaTrinhDaoTao.NamTotNghiep = quyetDinh.KhoaDaoTao != null ? quyetDinh.KhoaDaoTao.DenNam : quyetDinh.DenNgay.Year;

                    filter = CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?",
                        quyetDinh.Oid, item.ThongTinNhanVien.Oid);
                    QuyetDinhChuyenTruongDaoTao qdChuyenTruong = session.FindObject<QuyetDinhChuyenTruongDaoTao>(filter);
                    if (qdChuyenTruong != null)
                        quaTrinhDaoTao.TruongDaoTao = qdChuyenTruong.TruongDaoTaoMoi;
                    else
                        quaTrinhDaoTao.TruongDaoTao = quyetDinh.TruongDaoTao;
                    quaTrinhDaoTao.HinhThucDaoTao = quyetDinh.HinhThucDaoTao;
                    quaTrinhDaoTao.ChuyenMonDaoTao = item.ChuyenMonDaoTao;
                }
            }
            else
            {
                quaTrinhDaoTao = new QuaTrinhDaoTao(session);
                quaTrinhDaoTao.ThongTinNhanVien = nhanVien;
                quaTrinhDaoTao.QuyetDinh = null;
            }
        }

        public static void CreateQuaTrinhDaoTao(Session session, ThongTinNhanVien nhanVien,
            TrinhDoChuyenMon trinhDoChuyenMon, ChuyenMonDaoTao chuyenMonDaoTao, TruongDaoTao truongDaoTao,
            HinhThucDaoTao hinhThucDaoTao, int namTotNghiep, DateTime ngayCapBang)
        {
            QuaTrinhDaoTao quaTrinhDaoTao;
            CriteriaOperator filter;
            if (trinhDoChuyenMon != null)
            {
                filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TrinhDoChuyenMon=? and ChuyenMonDaoTao=? and TruongDaoTao=?",
                    nhanVien, trinhDoChuyenMon, chuyenMonDaoTao, truongDaoTao);
                quaTrinhDaoTao = session.FindObject<QuaTrinhDaoTao>(filter);
                if (quaTrinhDaoTao == null)
                {
                    quaTrinhDaoTao = new QuaTrinhDaoTao(session);
                    quaTrinhDaoTao.ThongTinNhanVien = nhanVien;
                }
                quaTrinhDaoTao.NamTotNghiep = namTotNghiep;

                quaTrinhDaoTao.TruongDaoTao = truongDaoTao;
                quaTrinhDaoTao.HinhThucDaoTao = hinhThucDaoTao;
                quaTrinhDaoTao.ChuyenMonDaoTao = chuyenMonDaoTao;
            }
        }

        public static void DeleteQuaTrinhDaoTao(Session session, ThongTinNhanVien nhanVien,
            TrinhDoChuyenMon trinhDoChuyenMon, ChuyenMonDaoTao chuyenMonDaoTao, TruongDaoTao truongDaoTao,
            HinhThucDaoTao hinhThucDaoTao, int namTotNghiep, DateTime ngayCapBang)
        {
            QuaTrinhDaoTao quaTrinhDaoTao;
            CriteriaOperator filter;
            if (trinhDoChuyenMon != null)
            {
                filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TrinhDoChuyenMon=? and ChuyenMonDaoTao=? and TruongDaoTao=?",
                    nhanVien, trinhDoChuyenMon, chuyenMonDaoTao, truongDaoTao);
                quaTrinhDaoTao = session.FindObject<QuaTrinhDaoTao>(filter);
                if (quaTrinhDaoTao == null)
                {
                    session.Delete(quaTrinhDaoTao);
                    session.Save(quaTrinhDaoTao);
                }
            }
        }

        /// <summary>
        /// tạo quá trình khen thưởng
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="quyetDinh"></param>
        public static void CreateQuaTrinhKhenThuong(Session session, ThongTinNhanVien nhanVien, QuyetDinhKhenThuong quyetDinh)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and HoSo=?",
                    quyetDinh, nhanVien);
            QuaTrinhKhenThuong quaTrinhKhenThuong = session.FindObject<QuaTrinhKhenThuong>(filter);
            if (quaTrinhKhenThuong == null)
            {
                quaTrinhKhenThuong = new QuaTrinhKhenThuong(session);
                quaTrinhKhenThuong.QuyetDinh = quyetDinh;
                quaTrinhKhenThuong.HoSo = nhanVien;
            }
            quaTrinhKhenThuong.DanhHieuKhenThuong = quyetDinh.DanhHieuKhenThuong;
            quaTrinhKhenThuong.LyDo = quyetDinh.LyDo;
            quaTrinhKhenThuong.NgayKhenThuong = quyetDinh.NgayHieuLuc;
            quaTrinhKhenThuong.NamHoc = quyetDinh.NamHoc;
        }

        /// <summary>
        /// Create qua trinh di nuoc ngoai
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="quocGia"></param>
        public static void CreateQuaTrinhDiNuocNgoai(Session session, QuyetDinh.QuyetDinh quyetDinh, ThongTinNhanVien nhanVien, QuocGia quocGia, DateTime tuNgay, DateTime denNgay)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh=?",
                nhanVien, quyetDinh);
            QuaTrinhDiNuocNgoai quaTrinhDiNuocNgoai = session.FindObject<QuaTrinhDiNuocNgoai>(filter);
            if (quaTrinhDiNuocNgoai == null)
            {
                quaTrinhDiNuocNgoai = new QuaTrinhDiNuocNgoai(session);

                quaTrinhDiNuocNgoai.ThongTinNhanVien = nhanVien;
                quaTrinhDiNuocNgoai.QuyetDinh = quyetDinh;
                quaTrinhDiNuocNgoai.QuocGia = quocGia;
                quaTrinhDiNuocNgoai.TuNgay = tuNgay;
                quaTrinhDiNuocNgoai.DenNgay = denNgay;
            }
            quaTrinhDiNuocNgoai.LyDo = quyetDinh.NoiDung;
        }

        /// <summary>
        /// Create qua trinh di nuoc ngoai
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="quocGia"></param>
        public static void CreateQuaTrinhDiNuocNgoai(Session session, QuyetDinhCaNhan quyetDinh, QuocGia quocGia, DateTime tuNgay, DateTime denNgay)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh=?",
                quyetDinh.ThongTinNhanVien, quyetDinh);
            QuaTrinhDiNuocNgoai quaTrinhDiNuocNgoai = session.FindObject<QuaTrinhDiNuocNgoai>(filter);
            if (quaTrinhDiNuocNgoai == null)
            {
                quaTrinhDiNuocNgoai = new QuaTrinhDiNuocNgoai(session);

                quaTrinhDiNuocNgoai.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
                quaTrinhDiNuocNgoai.QuyetDinh = quyetDinh;
                quaTrinhDiNuocNgoai.QuocGia = quocGia;
                quaTrinhDiNuocNgoai.TuNgay = tuNgay;
                quaTrinhDiNuocNgoai.DenNgay = denNgay;
            }
            quaTrinhDiNuocNgoai.LyDo = quyetDinh.NoiDung;
        }

        /// <summary>
        /// Update qua trinh di nuoc ngoai
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void UpdateQuaTrinhDiNuocNgoai(Session session, QuyetDinh.QuyetDinh quyetDinh, ThongTinNhanVien nhanVien, DateTime ngay)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh=?",
                        nhanVien, quyetDinh);
            QuaTrinhDiNuocNgoai dbl = session.FindObject<QuaTrinhDiNuocNgoai>(filter);
            if (dbl != null)
                dbl.DenNgay = ngay;
        }

        /// <summary>
        /// Reset qua trinh di nuoc ngoai
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void ResetQuaTrinhDiNuocNgoai(Session session, QuyetDinh.QuyetDinh quyetDinh, ThongTinNhanVien nhanVien)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh=?",
                        nhanVien, quyetDinh);
            QuaTrinhDiNuocNgoai dbl = session.FindObject<QuaTrinhDiNuocNgoai>(filter);
            if (dbl != null)
                dbl.DenNgay = DateTime.MinValue;
        }

        /// <summary>
        /// create quyet dinh boi duong
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void CreateQuaTrinhBoiDuong(Session session, QuyetDinhBoiDuong quyetDinh, ThongTinNhanVien nhanVien)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?",
                    quyetDinh, nhanVien);
            QuaTrinhBoiDuong quaTrinhBoiDuong = session.FindObject<QuaTrinhBoiDuong>(filter);
            if (quaTrinhBoiDuong == null)
            {
                quaTrinhBoiDuong = new QuaTrinhBoiDuong(session);
                quaTrinhBoiDuong.QuyetDinh = quyetDinh;
                quaTrinhBoiDuong.ThongTinNhanVien = nhanVien;
            }
            quaTrinhBoiDuong.TuNgay = quyetDinh.TuNgay.ToString("d");
            quaTrinhBoiDuong.DenNgay = quyetDinh.DenNgay.ToString("d");
            if (quyetDinh.ChuongTrinhBoiDuong != null)
            {
                quaTrinhBoiDuong.NoiBoiDuong = quyetDinh.ChuongTrinhBoiDuong.DonViToChuc;
                quaTrinhBoiDuong.NoiDungBoiDuong = quyetDinh.ChuongTrinhBoiDuong.TenChuongTrinhBoiDuong;
            }
            quaTrinhBoiDuong.LoaiHinhBoiDuong = quyetDinh.LoaiHinhBoiDuong;
            quaTrinhBoiDuong.ChungChi = quyetDinh.ChungChi;
        }

        /// <summary>
        /// create qua trinh tham gia bhxh
        /// </summary>
        /// <param name="session"></param>
        /// <param name="hoSoBaoHiem"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="ngay"></param>
        public static void CreateQuaTrinhThamGiaBHXH(Session session, HoSoBaoHiem hoSoBaoHiem, QuyetDinhCaNhan quyetDinh, DateTime ngay)
        {
            if (quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.PhanLoai == ThongTinLuongEnum.LuongHeSo
                || quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.PhanLoai == ThongTinLuongEnum.LuongKhoanBaoHiemLuongHeSo
                || quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.PhanLoai == ThongTinLuongEnum.LuongKhoanCoBHXH)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoSoBaoHiem=?",
                            hoSoBaoHiem);
                SortProperty sort = new SortProperty("TuNam", SortingDirection.Descending);
                using (XPCollection<QuaTrinhThamGiaBHXH> qtDongBHXHList = new XPCollection<QuaTrinhThamGiaBHXH>(session, filter, sort))
                {
                    qtDongBHXHList.TopReturnedObjects = 1;
                    if (qtDongBHXHList.Count == 1)
                    {
                        qtDongBHXHList[0].DenNam = ngay.AddMonths(-1);
                    }
                }

                filter = CriteriaOperator.Parse("HoSoBaoHiem=? and QuyetDinh=?",
                            hoSoBaoHiem, quyetDinh);
                QuaTrinhThamGiaBHXH qtDongBHXH = session.FindObject<QuaTrinhThamGiaBHXH>(filter);
                if (qtDongBHXH == null)
                {
                    qtDongBHXH = new QuaTrinhThamGiaBHXH(session);
                    qtDongBHXH.HoSoBaoHiem = hoSoBaoHiem;
                    qtDongBHXH.TuNam = ngay;
                    qtDongBHXH.QuyetDinh = quyetDinh;
                }
                if (quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.PhanLoai == ThongTinLuongEnum.LuongKhoanCoBHXH)
                    qtDongBHXH.HeSoLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan;
                else
                {
                    qtDongBHXH.HeSoLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
                    qtDongBHXH.PhuCapChucVu = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu;
                    qtDongBHXH.VuotKhung = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.HSPCVuotKhung;
                    qtDongBHXH.ThamNienGiangDay = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien;
                }
                qtDongBHXH.NoiLamViec = HamDungChung.NoiLamViec(session, quyetDinh.ThongTinNhanVien, quyetDinh.BoPhan);
            }
        }

        /// <summary>
        /// create qua trinh tham gia bhxh
        /// </summary>
        /// <param name="session"></param>
        /// <param name="hoSoBaoHiem"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="ngay"></param>
        /// 
        public static void CreateQuaTrinhThamGiaBHXH(Session session, QuyetDinh.QuyetDinh quyetDinh, HoSoBaoHiem hoSoBaoHiem, DateTime ngay)
        {
            if (hoSoBaoHiem.ThongTinNhanVien.NhanVienThongTinLuong.PhanLoai == ThongTinLuongEnum.LuongHeSo
                || hoSoBaoHiem.ThongTinNhanVien.NhanVienThongTinLuong.PhanLoai == ThongTinLuongEnum.LuongKhoanBaoHiemLuongHeSo
                || hoSoBaoHiem.ThongTinNhanVien.NhanVienThongTinLuong.PhanLoai == ThongTinLuongEnum.LuongKhoanCoBHXH)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoSoBaoHiem=?",
                            hoSoBaoHiem);
                SortProperty sort = new SortProperty("TuNam", SortingDirection.Descending);
                using (XPCollection<QuaTrinhThamGiaBHXH> qtDongBHXHList = new XPCollection<QuaTrinhThamGiaBHXH>(session, filter, sort))
                {
                    qtDongBHXHList.TopReturnedObjects = 1;
                    if (qtDongBHXHList.Count == 1)
                    {
                        qtDongBHXHList[0].DenNam = ngay.AddMonths(-1);
                    }
                }

                filter = CriteriaOperator.Parse("HoSoBaoHiem=? and QuyetDinh=?",
                            hoSoBaoHiem, quyetDinh);
                QuaTrinhThamGiaBHXH qtDongBHXH = session.FindObject<QuaTrinhThamGiaBHXH>(filter);
                if (qtDongBHXH == null)
                {
                    qtDongBHXH = new QuaTrinhThamGiaBHXH(session);
                    qtDongBHXH.HoSoBaoHiem = hoSoBaoHiem;
                    qtDongBHXH.TuNam = ngay;
                    qtDongBHXH.QuyetDinh = quyetDinh;
                }
                if (hoSoBaoHiem.ThongTinNhanVien.NhanVienThongTinLuong.PhanLoai == ThongTinLuongEnum.LuongKhoanCoBHXH)
                    qtDongBHXH.LuongKhoan = hoSoBaoHiem.ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan;// sửa lại theo lương khoán
                else
                {
                    qtDongBHXH.HeSoLuong = hoSoBaoHiem.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
                    qtDongBHXH.VuotKhung = hoSoBaoHiem.ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung;
                    qtDongBHXH.ThamNienGiangDay = hoSoBaoHiem.ThongTinNhanVien.NhanVienThongTinLuong.ThamNien;
                    // thêm sau
                    qtDongBHXH.ChucDanh = hoSoBaoHiem.ThongTinNhanVien.ChucDanh;

                }
                qtDongBHXH.NoiLamViec = HamDungChung.NoiLamViec(session, hoSoBaoHiem.ThongTinNhanVien, hoSoBaoHiem.ThongTinNhanVien.BoPhan);
            }
        }

  
        /// <summary>
        /// Create diễn biến lương
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="ngay"></param>
        public static void CreateDienBienLuong(Session session, QuyetDinh.QuyetDinh quyetDinh, ThongTinNhanVien nhanVien, DateTime ngay,Object objectDetail)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh.Oid =? and ThongTinNhanVien.Oid=?",
                    quyetDinh.Oid, nhanVien.Oid);
            DienBienLuong dienBienLuong = session.FindObject<DienBienLuong>(filter);
            if (dienBienLuong == null)
            {
                dienBienLuong = new DienBienLuong(session);
                dienBienLuong.ThongTinNhanVien = nhanVien;
                dienBienLuong.TuNgay = ngay;
                dienBienLuong.QuyetDinh = quyetDinh;
            }
            dienBienLuong.LyDo = quyetDinh.NoiDung;

            //Cập nhật một số thông tin về lương
            if (objectDetail != null)
            {
                if (objectDetail is ChiTietQuyetDinhTuyenDung)
                {
                    dienBienLuong.NgachLuong = ((ChiTietQuyetDinhTuyenDung)objectDetail).NgachLuong;
                    dienBienLuong.BacLuong = ((ChiTietQuyetDinhTuyenDung)objectDetail).BacLuong;
                    dienBienLuong.HeSoLuong = ((ChiTietQuyetDinhTuyenDung)objectDetail).HeSoLuong;
                    dienBienLuong.Huong85PhanTramLuong = ((ChiTietQuyetDinhTuyenDung)objectDetail).Huong85PhanTramLuong;
                }

                if (objectDetail is ChiTietQuyetDinhBoNhiemNgach)
                {
                    //
                    dienBienLuong.NgachLuong = ((ChiTietQuyetDinhBoNhiemNgach)objectDetail).NgachLuong;
                    dienBienLuong.BacLuong = ((ChiTietQuyetDinhBoNhiemNgach)objectDetail).BacLuong;
                    dienBienLuong.HeSoLuong = ((ChiTietQuyetDinhBoNhiemNgach)objectDetail).HeSoLuong;
                }

                else if (objectDetail is ChiTietQuyetDinhChuyenNgach)
                {
                    //
                    dienBienLuong.NgachLuong = ((ChiTietQuyetDinhChuyenNgach)objectDetail).NgachLuongMoi;
                    dienBienLuong.BacLuong = ((ChiTietQuyetDinhChuyenNgach)objectDetail).BacLuongMoi;
                    dienBienLuong.HeSoLuong = ((ChiTietQuyetDinhChuyenNgach)objectDetail).HeSoLuongMoi;
                }
                if (objectDetail is ChiTietQuyetDinhNangLuong)
                {
                    //
                    dienBienLuong.NgachLuong = ((ChiTietQuyetDinhNangLuong)objectDetail).NgachLuong;
                    dienBienLuong.BacLuong = ((ChiTietQuyetDinhNangLuong)objectDetail).BacLuongMoi;
                    dienBienLuong.HeSoLuong = ((ChiTietQuyetDinhNangLuong)objectDetail).HeSoLuongMoi;
                    dienBienLuong.NangLuongTruocHan = ((ChiTietQuyetDinhNangLuong)objectDetail).NangLuongTruocHan;
                }
                if (objectDetail is ChiTietQuyetDinhNangNgach)
                {
                    //
                    dienBienLuong.NgachLuong = ((ChiTietQuyetDinhNangNgach)objectDetail).NgachLuongMoi;
                    dienBienLuong.BacLuong = ((ChiTietQuyetDinhNangNgach)objectDetail).BacLuongMoi;
                    dienBienLuong.HeSoLuong = ((ChiTietQuyetDinhNangNgach)objectDetail).HeSoLuongMoi;
                }
                if (objectDetail is ChiTietQuyetDinhNangPhuCapThamNienNhaGiao)
                {
                    //
                    dienBienLuong.ThamNien = ((ChiTietQuyetDinhNangPhuCapThamNienNhaGiao)objectDetail).ThamNienMoi;
                }
                if (objectDetail is ChiTietQuyetDinhNangPhuCap)
                {
                    dienBienLuong.PhuCapDienThoai = ((ChiTietQuyetDinhNangPhuCap)objectDetail).PhuCapDienThoaiMoi;
                }
            }
            else
            {
                if (quyetDinh is QuyetDinhBoNhiem)
                {
                    //
                    dienBienLuong.HSPCChucVu = ((QuyetDinhBoNhiem)quyetDinh).HSPCChucVuMoi;
                }
                if (quyetDinh is QuyetDinhBoNhiemKiemNhiem)
                {
                    //
                    dienBienLuong.HSPCKiemNhiem = ((QuyetDinhBoNhiemKiemNhiem)quyetDinh).HSPCKiemNhiemMoi;
                }
                if (quyetDinh is QuyetDinhThoiChuc)
                {
                    //
                    dienBienLuong.HSPCChucVu = ((QuyetDinhThoiChuc)quyetDinh).HSPCChucVuMoi;
                    dienBienLuong.HSPCKiemNhiem = ((QuyetDinhThoiChuc)quyetDinh).HSPCKiemNhiemMoi;
                }
                if (quyetDinh is QuyetDinhHuongDanTapSu)
                {
                    //
                    dienBienLuong.HSPCTrachNhiem = ((QuyetDinhHuongDanTapSu)quyetDinh).HSPCTrachNhiem;
                }
            }
        }

        /// <summary>
        /// Update dien bien luong su dung trong quyet dinh mien nhiem, mien nhiem kiem nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void UpdateDienBienLuong(Session session, QuyetDinh.QuyetDinh quyetDinh, ThongTinNhanVien nhanVien, DateTime ngay)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh != ?",
                        nhanVien, quyetDinh);
            SortProperty sort = new SortProperty("TuNgay", SortingDirection.Descending);
            XPCollection<DienBienLuong> list = new XPCollection<DienBienLuong>(session, filter, sort);
            list.TopReturnedObjects = 1;
            if (list.Count == 1)
                list[0].DenNgay = ngay.AddDays(-1);
        }

        /// <summary>
        /// Reset dien bien luong su dung trong quyet dinh mien nhiem, mien nhiem kiem nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void ResetDienBienLuong(Session session, ThongTinNhanVien nhanVien)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                        nhanVien);
            SortProperty sort = new SortProperty("TuNgay", SortingDirection.Descending);
            XPCollection<DienBienLuong> list = new XPCollection<DienBienLuong>(session, filter, sort);
            list.TopReturnedObjects = 1;
            if (list.Count == 1)
                list[0].DenNgay = DateTime.MinValue;
        }

        /// <summary>
        /// Create qua trinh bo nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="chucVu"></param>
        /// <param name="hspcChucVu"></param>
        /// <param name="ngayHuong"></param>
        public static void CreateQuaTrinhLuanChuyen(Session session, QuyetDinhCaNhan quyetDinh, BoPhan boPhanCu, BoPhan boPhanMoi, ChucVu chucVuMoi, DateTime tuNgay)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?",
                    quyetDinh, quyetDinh.ThongTinNhanVien);
            QuaTrinhLuanChuyen quaTrinhLuanChuyen = session.FindObject<QuaTrinhLuanChuyen>(filter);
            if (quaTrinhLuanChuyen == null)
            {
                quaTrinhLuanChuyen = new QuaTrinhLuanChuyen(session);
                quaTrinhLuanChuyen.QuyetDinh = quyetDinh;
                quaTrinhLuanChuyen.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
            }
            quaTrinhLuanChuyen.ChucVu = chucVuMoi;
            quaTrinhLuanChuyen.BoPhan = boPhanMoi;
            quaTrinhLuanChuyen.BoPhanCu = boPhanCu;
            quaTrinhLuanChuyen.TuNgay = quyetDinh.NgayHieuLuc;
        }

        /// <summary>
        /// Update QuaTrinhLuanChuyen
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void UpdateQuaTrinhLuanChuyen(Session session, QuyetDinhCaNhan quyetDinh, DateTime ngay)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?",
                    quyetDinh, quyetDinh.ThongTinNhanVien);
            QuaTrinhLuanChuyen quaTrinhLuanChuyen = session.FindObject<QuaTrinhLuanChuyen>(filter);
            if (quaTrinhLuanChuyen != null)
            {
                quaTrinhLuanChuyen.DenNgay = ngay.AddDays(-1);
            }
        }

        /// <summary>
        /// Reset QuaTrinhLuanChuyen
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void ResetQuaTrinhLuanChuyen(Session session, QuyetDinhCaNhan quyetDinh)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?",
                    quyetDinh, quyetDinh.ThongTinNhanVien);
            QuaTrinhLuanChuyen quaTrinhLuanChuyen = session.FindObject<QuaTrinhLuanChuyen>(filter);
            if (quaTrinhLuanChuyen != null)
            {
                quaTrinhLuanChuyen.DenNgay = DateTime.MinValue;
            }
        }


        /// <summary>
        /// Xóa quá trình
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="quyetDinh"></param>
        public static void DeleteQuaTrinhHoSo<T>(Session session, ThongTinNhanVien nhanVien, QuyetDinh.QuyetDinh quyetDinh) where T : BaseObject
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? && HoSo=?",
                    quyetDinh, nhanVien);
            T quaTrinh = session.FindObject<T>(filter);
            if (quaTrinh != null)
            {
                session.Delete(quaTrinh);
                session.Save(quaTrinh);
            }
        }

        /// <summary>
        /// Xóa quá trình
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="quyetDinh"></param>
        public static void DeleteQuaTrinhNhanVien<T>(Session session, QuyetDinhCaNhan quyetDinh) where T : BaseObject
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? && ThongTinNhanVien=?",
                    quyetDinh, quyetDinh.ThongTinNhanVien);
            T quaTrinh = session.FindObject<T>(filter);
            if (quaTrinh != null)
            {
                session.Delete(quaTrinh);
                session.Save(quaTrinh);
            }
        }

        /// <summary>
        /// Xóa quá trình
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="quyetDinh"></param>
        public static void DeleteQuaTrinh<T>(Session session, CriteriaOperator filter) where T : BaseObject
        {
            T quaTrinh = session.FindObject<T>(filter);
            if (quaTrinh != null)
            {
                session.Delete(quaTrinh);
                session.Save(quaTrinh);
            }
        }
    }
}
