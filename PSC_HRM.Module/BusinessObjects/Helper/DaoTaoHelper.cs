using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DiNuocNgoai;

namespace PSC_HRM.Module.DaoTao
{
    public static class DaoTaoHelper
    {
        /// <summary>
        /// Create van bang
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        /// <param name="namTotNghiep"></param>
        public static void CreateVanBang(Session session, QuyetDinhDaoTao quyetDinh, ThongTinNhanVien nhanVien, int namTotNghiep)
        {
            if (quyetDinh != null)
            {
                ChiTietDaoTao chiTietDaoTao = session.FindObject<ChiTietDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?", quyetDinh.Oid, nhanVien.Oid));
                if (chiTietDaoTao != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and TruongDaoTao=? and TrinhDoChuyenMon=? and ChuyenMonDaoTao=?",
                        nhanVien, quyetDinh.TruongDaoTao, quyetDinh.TrinhDoChuyenMon, chiTietDaoTao.ChuyenMonDaoTao);
                    VanBang bc = session.FindObject<VanBang>(filter);
                    if (bc == null)
                    {
                        bc = new VanBang(session);
                        bc.HoSo = nhanVien;
                        bc.TrinhDoChuyenMon = quyetDinh.TrinhDoChuyenMon;

                        filter = CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?",
                            quyetDinh.Oid, nhanVien.Oid);
                        QuyetDinhChuyenTruongDaoTao qdChuyenTruong = session.FindObject<QuyetDinhChuyenTruongDaoTao>(filter);
                        if (qdChuyenTruong != null)
                            bc.TruongDaoTao = qdChuyenTruong.TruongDaoTaoMoi;
                        else
                            bc.TruongDaoTao = quyetDinh.TruongDaoTao;

                        bc.ChuyenMonDaoTao = chiTietDaoTao.ChuyenMonDaoTao;

                        bc.HinhThucDaoTao = quyetDinh.HinhThucDaoTao;
                        bc.NamTotNghiep = namTotNghiep;
                    }
                }
            }
            else
            {
                VanBang bc = new VanBang(session);
                bc.HoSo = nhanVien;
            }
        }

        public static void DeleteVanBang(Session session, QuyetDinhDaoTao quyetDinh, ThongTinNhanVien nhanVien)
        {
            ChiTietDaoTao chiTietDaoTao = session.FindObject<ChiTietDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?", quyetDinh.Oid, nhanVien.Oid));
            if (chiTietDaoTao != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and TrinhDoChuyenMon=? and TruongDaoTao=? and ChuyenMonDaoTao=?",
                    nhanVien.Oid, quyetDinh.TrinhDoChuyenMon, quyetDinh.TruongDaoTao, chiTietDaoTao.ChuyenMonDaoTao);
                VanBang bangCap = session.FindObject<VanBang>(filter);
                if (bangCap != null)
                {
                    session.Delete(bangCap);
                    session.Save(bangCap);
                }
            }
        }

        public static void CreateVanBang(Session session, ThongTinNhanVien nhanVien, 
            TrinhDoChuyenMon trinhDoChuyenMon, ChuyenMonDaoTao chuyenMonDaoTao, TruongDaoTao truongDaoTao,
            HinhThucDaoTao hinhThucDaoTao, int namTotNghiep, DateTime ngayCapBang)
        {
            if (trinhDoChuyenMon != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and TrinhDoChuyenMon=? and ChuyenMonDaoTao=? and TruongDaoTao=?",
                    nhanVien, trinhDoChuyenMon, chuyenMonDaoTao, truongDaoTao);
                VanBang bc = session.FindObject<VanBang>(filter);
                if (bc == null)
                {
                    bc = new VanBang(session);
                }
                bc.HoSo = nhanVien;
                bc.TrinhDoChuyenMon = trinhDoChuyenMon;
                bc.ChuyenMonDaoTao = chuyenMonDaoTao;
                bc.TruongDaoTao = truongDaoTao;
                bc.HinhThucDaoTao = hinhThucDaoTao;
                bc.NamTotNghiep = namTotNghiep;
                bc.NgayCapBang = ngayCapBang;
            }
        }


        public static void DeleteVanBang(Session session, ThongTinNhanVien nhanVien,
            TrinhDoChuyenMon trinhDoChuyenMon, ChuyenMonDaoTao chuyenMonDaoTao, TruongDaoTao truongDaoTao,
            HinhThucDaoTao hinhThucDaoTao, int namTotNghiep, DateTime ngayCapBang)
        {
            if (trinhDoChuyenMon != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and TrinhDoChuyenMon=? and ChuyenMonDaoTao=? and TruongDaoTao=?",
                        nhanVien, trinhDoChuyenMon, chuyenMonDaoTao, truongDaoTao);
                VanBang bc = session.FindObject<VanBang>(filter);
                if (bc != null)
                {
                    session.Delete(bc);
                    session.Save(bc);
                }
            }
        }

        /// <summary>
        /// Create dang theo hoc
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="trinhDo"></param>
        public static void CreateDangTheoHoc(Session session, ThongTinNhanVien nhanVien, TrinhDoChuyenMon trinhDo, QuocGia quocGia)
        {
            string tenChuongTrinhHoc;
            //
            if (trinhDo.TenTrinhDoChuyenMon.ToLower().Contains("tiến"))
                tenChuongTrinhHoc = "Nghiên cứu sinh";
            else if (trinhDo.TenTrinhDoChuyenMon.ToLower().Contains("thạc"))
                tenChuongTrinhHoc = "Cao học";
            else
                tenChuongTrinhHoc = trinhDo.TenTrinhDoChuyenMon;


            CriteriaOperator filter = CriteriaOperator.Parse("TenChuongTrinhHoc like ?",
                tenChuongTrinhHoc);
            ChuongTrinhHoc chuongTrinhHoc = session.FindObject<ChuongTrinhHoc>(filter);
            if (chuongTrinhHoc == null)
            {
                chuongTrinhHoc = new ChuongTrinhHoc(session);
                chuongTrinhHoc.MaQuanLy = tenChuongTrinhHoc;
                chuongTrinhHoc.TenChuongTrinhHoc = tenChuongTrinhHoc;
            }
            //
            {
                nhanVien.NhanVienTrinhDo.ChuongTrinhHoc = chuongTrinhHoc;
                //
                nhanVien.NhanVienTrinhDo.QuocGiaHoc = quocGia;
            }
        }

        /// <summary>
        /// Reset dang theo hoc
        /// </summary>
        /// <param name="nhanVien"></param>
        public static void ResetDangTheoHoc(ThongTinNhanVien nhanVien)
        {
            nhanVien.NhanVienTrinhDo.ChuongTrinhHoc = null;
            nhanVien.NhanVienTrinhDo.QuocGiaHoc = null;
        }

        /// <summary>
        /// Get chi tiet dao tao
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static ChiTietDaoTao GetChiTietDaoTao(Session session, QuyetDinhDaoTao quyetDinh, ThongTinNhanVien nhanVien)
        {
            if (quyetDinh != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?", quyetDinh.Oid, nhanVien.Oid);
                ChiTietDaoTao chiTiet = session.FindObject<ChiTietDaoTao>(filter);
                return chiTiet;
            }
            else
                return null;
        }


        public static TinhTrang GetTinhTrang(Session session, QuocGia quocGia, bool duocHuongLuong)
        {
            TinhTrang tinhTrang = null;

            if (DiNuocNgoaiHelper.IsNgoaiNuoc(quocGia))
            {
                if (!duocHuongLuong)
                {
                    tinhTrang = HoSoHelper.DiHocNgoaiNuocKhongLuong(session);
                }
                else
                {
                    tinhTrang = HoSoHelper.DiHocNgoaiNuocCoLuong(session);
                }
            }
            else
            {
                if (!duocHuongLuong)
                {
                    tinhTrang = HoSoHelper.DiHocTrongNuocKhongLuong(session);
                }
                else
                {
                    tinhTrang = HoSoHelper.DiHocTrongNuocCoLuong(session);
                }
            }

            return tinhTrang;
        }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static bool IsExits(QuyetDinhDaoTao quyetDinh, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in quyetDinh.ListChiTietDaoTao
                          where d.ThongTinNhanVien.Oid == nhanVien.Oid
                          select d).SingleOrDefault();
            return exists != null;
        }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="duyetDK"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static bool IsExits(DuyetDangKyDaoTao duyetDK, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in duyetDK.ListChiTietDuyetDangKyDaoTao
                          where d.ThongTinNhanVien.Oid == nhanVien.Oid
                          select d).SingleOrDefault();
            return exists != null;
        }
    }
}
