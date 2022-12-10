using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.GiayTo
{
    public static class GiayToHoSoHelper
    {
        /// <summary>
        /// Tạo giấy tờ cho quyết định
        /// </summary>
        /// <param name="session"></param>
        /// <param name="soGiayTo"></param>
        /// <param name="nhanVien"></param>
        /// <param name="ngayBanHanh"></param>
        /// <param name="luuTru"></param>
        /// <param name="trichYeu"></param>
        public static void CreateGiayToQuyetDinh(Session session, string soGiayTo, ThongTinNhanVien nhanVien, DateTime ngayBanHanh,
            string luuTru, string trichYeu)
        {
            DanhMuc.GiayTo giayTo = session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định"));
            DangLuuTru dangLuuTru = session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            CreateGiayToHoSo(session, soGiayTo, nhanVien, ngayBanHanh, giayTo, dangLuuTru, luuTru, trichYeu);
        }

        /// <summary>
        /// Tạo giấy tờ cho hợp đồng
        /// </summary>
        /// <param name="session"></param>
        /// <param name="soGiayTo"></param>
        /// <param name="nhanVien"></param>
        /// <param name="ngayBanHanh"></param>
        /// <param name="luuTru"></param>
        /// <param name="trichYeu"></param>
        public static void CreateGiayToHopDong(Session session, string soGiayTo, ThongTinNhanVien nhanVien, DateTime ngayBanHanh,
            string luuTru, string trichYeu)
        {
            DanhMuc.GiayTo giayTo = session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "%Hợp đồng%"));
            DangLuuTru dangLuuTru = session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            CreateGiayToHoSo(session, soGiayTo, nhanVien, ngayBanHanh, giayTo, dangLuuTru, luuTru, trichYeu);
        }

        /// <summary>
        /// Tạo giấy tờ hồ sơ
        /// </summary>
        /// <param name="session"></param>
        /// <param name="soGiayTo"></param>
        /// <param name="nhanVien"></param>
        /// <param name="ngayBanHanh"></param>
        /// <param name="giayTo"></param>
        /// <param name="dangLuuTru"></param>
        /// <param name="luuTru"></param>
        /// <param name="trichYeu"></param>
        public static void CreateGiayToHoSo(Session session, string soGiayTo, ThongTinNhanVien nhanVien, DateTime ngayBanHanh,
            DanhMuc.GiayTo giayTo, DangLuuTru dangLuuTru, string luuTru, string trichYeu)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and SoGiayTo=?",
                    nhanVien, soGiayTo);
            GiayToHoSo giayToHoSo = session.FindObject<GiayToHoSo>(filter);
            if (giayToHoSo == null)
            {
                giayToHoSo = new GiayToHoSo(session);
                giayToHoSo.HoSo = nhanVien;
                giayToHoSo.SoGiayTo = soGiayTo;
                giayToHoSo.SoBan = 1;
            }
            giayToHoSo.GiayTo = giayTo;
            giayToHoSo.DangLuuTru = dangLuuTru;
            giayToHoSo.NgayBanHanh = ngayBanHanh;
            giayToHoSo.LuuTru = luuTru;
            giayToHoSo.TrichYeu = trichYeu;
        }

        /// <summary>
        /// Xóa giấy tờ hồ sơ
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="soGiayTo"></param>
        public static void DeleteGiayToHoSo(Session session, ThongTinNhanVien nhanVien, string soGiayTo)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and SoGiayTo=?",
                    nhanVien, soGiayTo);
            GiayToHoSo giayToHoSo = session.FindObject<GiayToHoSo>(filter);
            if (giayToHoSo != null)
            {
                session.Delete(giayToHoSo);
                session.Save(giayToHoSo);
            }
        }

        /// <summary>
        /// Xóa giấy tờ hồ sơ
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="soGiayTo"></param>
        public static void DeleteGiayToHoSoTheoQuyetDinh(Session session, ThongTinNhanVien nhanVien, QuyetDinh.QuyetDinh quyetDinh)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and QuyetDinh=?",
                    nhanVien, quyetDinh);
            GiayToHoSo giayToHoSo = session.FindObject<GiayToHoSo>(filter);
            if (giayToHoSo != null)
            {
                session.Delete(giayToHoSo);
                session.Save(giayToHoSo);
            }
        }
    }
}
