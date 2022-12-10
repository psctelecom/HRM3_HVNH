using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DiNuocNgoai;


namespace PSC_HRM.Module.BoiDuong
{
    public static class BoiDuongHelper
    {
        /// <summary>
        /// Create chung chi
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        public static void CreateChungChi(Session session, QuyetDinhBoiDuong quyetDinh, ThongTinNhanVien nhanVien)
        {
            if (quyetDinh.ChungChi != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and LoaiChungChi=? and NgayCap=?",
                    nhanVien, quyetDinh.ChungChi, quyetDinh.DenNgay);
                ChungChi chungChi = session.FindObject<ChungChi>(filter);
                if (chungChi == null)
                {
                    chungChi = new ChungChi(session);
                    chungChi.HoSo = nhanVien;
                    chungChi.LoaiChungChi = quyetDinh.ChungChi;
                }
                chungChi.TenChungChi = quyetDinh.ChuongTrinhBoiDuong.TenChuongTrinhBoiDuong;
                chungChi.NoiCap = quyetDinh.ChuongTrinhBoiDuong.DonViToChuc;
                chungChi.NgayCap = quyetDinh.DenNgay;
            }
        }

        /// <summary>
        /// Delete chung chi
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        public static void DeleteChungChi(Session session, QuyetDinhBoiDuong quyetDinh, ThongTinNhanVien nhanVien)
        {
            if (quyetDinh.ChungChi != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and LoaiChungChi=? and NgayCap=?",
                    nhanVien, quyetDinh.ChungChi, quyetDinh.DenNgay);
                ChungChi chungChi = session.FindObject<ChungChi>(filter);
                if (chungChi != null)
                {
                    session.Delete(chungChi);
                    session.Save(chungChi);
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
        public static ChiTietBoiDuong GetChiTietBoiDuong(Session session, QuyetDinhBoiDuong quyetDinh, ThongTinNhanVien nhanVien)
        {
            if (quyetDinh != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinhBoiDuong=? and ThongTinNhanVien=?", quyetDinh.Oid, nhanVien.Oid);
                ChiTietBoiDuong chiTiet = session.FindObject<ChiTietBoiDuong>(filter);
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
        /// <param name="duyetDK"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static bool IsExits(DuyetDangKyBoiDuong duyetDK, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in duyetDK.ListChiTietDuyetDangKyBoiDuong
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
        public static bool IsExits(QuyetDinhBoiDuong quyetDinh, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in quyetDinh.ListChiTietBoiDuong
                          where d.ThongTinNhanVien.Oid == nhanVien.Oid
                          select d).SingleOrDefault();
            return exists != null;
        }
    }
}
