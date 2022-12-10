using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.HoSo
{
    public static class HoSoHelper
    {
        /// <summary>
        /// set loai nhan su
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="tenLoaiNhanSu"></param>
        /// <param name="tenCongViec"></param>
        public static void SetLoaiNhanSu(Session session, ThongTinNhanVien nhanVien)
        {
            if (nhanVien != null && nhanVien.ChucVu != null && nhanVien.ChucVu.LaQuanLy == true)
            {
                LoaiNhanSu loaiNhanSu = session.FindObject<LoaiNhanSu>(CriteriaOperator.Parse("TenLoaiNhanSu like ?", "Quản lý"));
                if (loaiNhanSu == null)
                {
                    loaiNhanSu = new LoaiNhanSu(session);
                    loaiNhanSu.MaQuanLy = "QL";
                    loaiNhanSu.TenLoaiNhanSu = "Quản lý";
                }
                nhanVien.LoaiNhanSu = loaiNhanSu;
            }
        }

        /// <summary>
        /// Đang làm việc
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static TinhTrang DangLamViec(Session session)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc");
            TinhTrang tinhTrang = session.FindObject<TinhTrang>(filter);
            if (tinhTrang == null)
            {
                using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                {
                    tinhTrang = new TinhTrang(uow);
                    tinhTrang.MaQuanLy = "DLV";
                    tinhTrang.TenTinhTrang = "Đang làm việc";

                    uow.CommitChanges();
                }
                tinhTrang = session.GetObjectByKey<TinhTrang>(tinhTrang.Oid);
            }
            return tinhTrang;
        }

        /// <summary>
        /// Đi nuoc ngoai tren 30 ngay
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static TinhTrang DiNuocNgoaiTren30Ngay(Session session)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Đi nước ngoài trên 30 ngày");
            TinhTrang tinhTrang = session.FindObject<TinhTrang>(filter);
            if (tinhTrang == null)
            {
                using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                {
                    tinhTrang = new TinhTrang(uow);
                    tinhTrang.MaQuanLy = "DHNNT30N";
                    tinhTrang.TenTinhTrang = "Đi nước ngoài trên 30 ngày";

                    uow.CommitChanges();
                }
                tinhTrang = session.GetObjectByKey<TinhTrang>(tinhTrang.Oid);
            }
            return tinhTrang;
        }

        /// <summary>
        /// Tap su
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static LoaiNhanVien TapSu(Session session)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("TenLoaiNhanVien like ?", "Hợp đồng lao động thử việc");
            LoaiNhanVien loaiNhanVien = session.FindObject<LoaiNhanVien>(filter);
            if (loaiNhanVien == null)
            {
                using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                {
                    loaiNhanVien = new LoaiNhanVien(uow);
                    loaiNhanVien.MaQuanLy = "HDLDTV";
                    loaiNhanVien.TenLoaiNhanVien = "Hợp đồng lao động thử việc";

                    uow.CommitChanges();
                }
                loaiNhanVien = session.GetObjectByKey<LoaiNhanVien>(loaiNhanVien.Oid);
            }
            return loaiNhanVien;
        }

        /// <summary>
        /// Tap su
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static LoaiNhanVien HopDongCoThoiHan(Session session)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("TenLoaiNhanVien like ?", "Hợp đồng có thời hạn");
            LoaiNhanVien loaiNhanVien = session.FindObject<LoaiNhanVien>(filter);
            if (loaiNhanVien == null)
            {
                using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                {
                    loaiNhanVien = new LoaiNhanVien(uow);
                    loaiNhanVien.MaQuanLy = "04";
                    loaiNhanVien.TenLoaiNhanVien = "Hợp đồng có thời hạn";

                    uow.CommitChanges();
                }
                loaiNhanVien = session.GetObjectByKey<LoaiNhanVien>(loaiNhanVien.Oid);
            }
            return loaiNhanVien;
        }

        /// <summary>
        /// Đi học ngoài nước có lương
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static TinhTrang DiHocNgoaiNuocCoLuong(Session session)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Đi học ngoài nước có hưởng lương");
            TinhTrang tinhTrang = session.FindObject<TinhTrang>(filter);
            if (tinhTrang == null)
            {
                using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                {
                    tinhTrang = new TinhTrang(uow);
                    tinhTrang.MaQuanLy = "DHNNCHL";
                    tinhTrang.TenTinhTrang = "Đi học ngoài nước có hưởng lương";

                    uow.CommitChanges();
                }
                tinhTrang = session.GetObjectByKey<TinhTrang>(tinhTrang.Oid);
            }
            return tinhTrang;
        }

        /// <summary>
        /// Đi học ngoài nước không lương
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static TinhTrang DiHocNgoaiNuocKhongLuong(Session session)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Đi học ngoài nước không hưởng lương");
            TinhTrang tinhTrang = session.FindObject<TinhTrang>(filter);
            if (tinhTrang == null)
            {
                using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                {
                    tinhTrang = new TinhTrang(uow);
                    tinhTrang.MaQuanLy = "DHNNKHL";
                    tinhTrang.TenTinhTrang = "Đi học ngoài nước không hưởng lương";

                    uow.CommitChanges();
                }
                tinhTrang = session.GetObjectByKey<TinhTrang>(tinhTrang.Oid);
            }
            return tinhTrang;
        }

        /// <summary>
        /// Đi học trong nước có lương
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static TinhTrang DiHocTrongNuocCoLuong(Session session)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Đi học trong nước có hưởng lương");
            TinhTrang tinhTrang = session.FindObject<TinhTrang>(filter);
            if (tinhTrang == null)
            {
                using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                {
                    tinhTrang = new TinhTrang(uow);
                    tinhTrang.MaQuanLy = "DHTNCHL";
                    tinhTrang.TenTinhTrang = "Đi học trong nước có hưởng lương";

                    uow.CommitChanges();
                }
                tinhTrang = session.GetObjectByKey<TinhTrang>(tinhTrang.Oid);
            }
            return tinhTrang;
        }

        /// <summary>
        /// đi học trong nước không lương
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static TinhTrang DiHocTrongNuocKhongLuong(Session session)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Đi học trong nước không hưởng lương");
            TinhTrang tinhTrang = session.FindObject<TinhTrang>(filter);
            if (tinhTrang == null)
            {
                using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                {
                    tinhTrang = new TinhTrang(uow);
                    tinhTrang.MaQuanLy = "DHTNKHL";
                    tinhTrang.TenTinhTrang = "Đi học trong nước không hưởng lương";

                    uow.CommitChanges();
                }
                tinhTrang = session.GetObjectByKey<TinhTrang>(tinhTrang.Oid);
            }
            return tinhTrang;
        }

        /// <summary>
        /// Get HSPC chuyen mon
        /// </summary>
        /// <param name="session"></param>
        /// <param name="trinhDo"></param>
        /// <param name="congViec"></param>
        /// <returns></returns>
        public static decimal HSPCChuyenMon(Session session, TrinhDoChuyenMon trinhDo, CongViec congViec)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("TrinhDoChuyenMon=? and CongViecHienNay=?",
                        trinhDo.Oid, congViec.Oid);
            HeSoChuyenMon heSo = session.FindObject<HeSoChuyenMon>(filter);
            if (heSo != null)
                return heSo.HSPCChuyenMon;
            return 0;
        }

        
        /// <summary>
        /// Get HSL Tang Them UTE
        /// </summary>
        /// <param name="session"></param>
        /// <param name="trinhDo"></param>
        /// <param name="congViec"></param>
        /// <returns></returns>
        public static void CapNhatHSLTangThem(Session session, NhanVienThongTinLuong thongTinLuong, NhanVienTrinhDo trinhDo)
        {
            HeSoLuongTangThem itemHSLTangThem;
            //decimal HSLTangThem = 0;
            NhanVien nhanVien;
            if (thongTinLuong != null)
                nhanVien = session.FindObject<NhanVien>(CriteriaOperator.Parse("NhanVienThongTinLuong = ?", thongTinLuong.Oid));
            else 
                nhanVien = session.FindObject<NhanVien>(CriteriaOperator.Parse("NhanVienTrinhDo = ?", trinhDo.Oid));

            if (nhanVien != null && nhanVien.NhanVienThongTinLuong != null && nhanVien.NhanVienThongTinLuong.NgachLuong != null)
            {
                if (nhanVien.NhanVienThongTinLuong.VuotKhung == 0)
                {
                    if (nhanVien.NhanVienTrinhDo.HocHam == null)
                        itemHSLTangThem = session.FindObject<HeSoLuongTangThem>(CriteriaOperator.Parse("NgachLuong = ? and TuHeSo <= ? and DenHeSo >= ? and VuotKhung = false and HocHam is null", nhanVien.NhanVienThongTinLuong.NgachLuong.Oid, nhanVien.NhanVienThongTinLuong.HeSoLuong, nhanVien.NhanVienThongTinLuong.HeSoLuong));
                    else
                        itemHSLTangThem = session.FindObject<HeSoLuongTangThem>(CriteriaOperator.Parse("NgachLuong = ? and TuHeSo <= ? and DenHeSo >= ? and VuotKhung = false and HocHam = ?", nhanVien.NhanVienThongTinLuong.NgachLuong.Oid, nhanVien.NhanVienThongTinLuong.HeSoLuong, nhanVien.NhanVienThongTinLuong.HeSoLuong, nhanVien.NhanVienTrinhDo.HocHam));
                    if (itemHSLTangThem != null)
                    {
                        if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon!=null && (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.Contains("tiến sĩ") || nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.Contains("tiến sỹ")))
                            nhanVien.NhanVienThongTinLuong.HSLTangThem = itemHSLTangThem.HSLTangThem + itemHSLTangThem.HSTangThemTienSi ;
                        else
                            nhanVien.NhanVienThongTinLuong.HSLTangThem = itemHSLTangThem.HSLTangThem;

                        /*Theo qui chế mới*/
                        //Giảng viên chính và tiền sĩ + 0.5
                        if (nhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy.Equals("15.110") && (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.Contains("Tiến sĩ") || nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.Contains("Tiến sỹ")))
                        {
                            nhanVien.NhanVienThongTinLuong.HSLTangThem += Convert.ToDecimal(0.5);
                        }
                        //Giảng viên và thạc sỹ + 0.3
                        if (nhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy.Equals("15.111") && (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.Contains("Thạc sĩ") || nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.Contains("Thạc sỹ")))
                        {
                            nhanVien.NhanVienThongTinLuong.HSLTangThem += Convert.ToDecimal(0.3);
                        }

                    }
                }
                else
                {
                    if (nhanVien.NhanVienTrinhDo.HocHam == null)
                        itemHSLTangThem = session.FindObject<HeSoLuongTangThem>(CriteriaOperator.Parse("NgachLuong = ? and VuotKhung = true and PhanTramVuotKhung = ? and HocHam is null", nhanVien.NhanVienThongTinLuong.NgachLuong.Oid, nhanVien.NhanVienThongTinLuong.VuotKhung));
                    else
                        itemHSLTangThem = session.FindObject<HeSoLuongTangThem>(CriteriaOperator.Parse("NgachLuong = ? and VuotKhung = true and PhanTramVuotKhung = ? and HocHam = ?", nhanVien.NhanVienThongTinLuong.NgachLuong.Oid, nhanVien.NhanVienThongTinLuong.VuotKhung, nhanVien.NhanVienTrinhDo.HocHam));
                    if (itemHSLTangThem != null)
                    {
                        if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null && (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.Contains("tiến sĩ") || nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.Contains("tiến sỹ")))
                            nhanVien.NhanVienThongTinLuong.HSLTangThem = itemHSLTangThem.HSLTangThem + itemHSLTangThem.HSTangThemTienSi; 
                        else
                            nhanVien.NhanVienThongTinLuong.HSLTangThem = itemHSLTangThem.HSLTangThem;
                    }
                }
            }
        }
    }
}
