using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.CauHinh;

namespace PSC_HRM.Module.TaoMaQuanLy
{
    public static class MaQuanLyFactory
    {
        /// <summary>
        /// Tạo mã quản lý, số, ...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string TaoMaQuanLy(MaQuanLyTypeEnum type, params SqlParameter[] args)
        {
            MaQuanLyBase maQuanLy = CreateMaQuanLy(type);
            return maQuanLy.TaoMaQuanLy(args);
        }

        private static MaQuanLyBase CreateMaQuanLy(MaQuanLyTypeEnum type)
        {
            switch (type)
            {
                case MaQuanLyTypeEnum.MaNhanVien:
                    return new TaoMaNhanVien();
                case MaQuanLyTypeEnum.MaGiangVienThinhGiang:
                    return new TaoMaGiangVienThinhGiang();
                case MaQuanLyTypeEnum.MaUngVienChuyenVien:
                    return new TaoMaUngVienChuyenVien();
                case MaQuanLyTypeEnum.MaUngVienGiangVien:
                    return new TaoMaUngVienGiangVien();
                case MaQuanLyTypeEnum.SoHieuCongChuc:
                    return new TaoSoHieuCongChuc();
                case MaQuanLyTypeEnum.SoHopDongChamBaoCao:
                    return new TaoSoHopDongChamBaoCao();
                case MaQuanLyTypeEnum.SoThanhLyHopDongThinhGiangChatLuongCao:
                    return new TaoSoHopDongThanhLyThinhGiangChatLuongCao();
                case MaQuanLyTypeEnum.SoHopDongCoVanHocTap:
                    return new TaoSoHopDongCoVanHocTap();
                case MaQuanLyTypeEnum.SoHopDongKhoan:
                    return new TaoSoHopDongKhoan();
                case MaQuanLyTypeEnum.SoHopDongLamViec:
                    return new TaoSoHopDongLamViec();
                case MaQuanLyTypeEnum.SoHopDongLaoDong:
                    return new TaoSoHopDongLaoDong();
                case MaQuanLyTypeEnum.SoThanhLyHopDongThinhGiang:
                    return new TaoSoHopDongThanhLyThinhGiang();
                case MaQuanLyTypeEnum.SoHopDongThinhGiang:
                    return new TaoSoHopDongThinhGiang();
                case MaQuanLyTypeEnum.SoBaoDanhGiangVien:
                    return new TaoSoBaoDanhGiangVien();
                case MaQuanLyTypeEnum.SoBaoDanhNhanVien:
                    return new TaoSoBaoDanhNhanVien();
                case MaQuanLyTypeEnum.SoThuMoiNhanViec:
                    return new TaoSoThuMoiNhanViec();
                default:
                    return new TaoMaNhanVien();
            }
        }

        public static string CreateMaQuanLyNhanVien(BoPhan coSo, BoPhan donVi)
        {
            string maQuanLy = string.Empty;

            if (TaoMaNhanVien.Get_NextMaQuanLy_IUH(coSo, donVi) != 0)
            {
                //Tạo mã quản lý nhân viên gồm (Mã cơ sở,mã đơn vị, mã quản lý cao nhất)
                maQuanLy = string.Format("{0}{1}{2}", coSo.MaQuanLy.ToString().Trim(), donVi.MaQuanLy.ToString().Trim(), TaoMaNhanVien.Get_NextMaQuanLy_IUH(coSo, donVi));
            }
            return maQuanLy;
        }

        public static string CreateSoHieuNhanVien(BoPhan donVi)
        {
            string soHieu = string.Empty;
            soHieu = TaoSoHieuCongChuc.Get_SoHieuCongChuc_CYD(donVi).ToString();
            return soHieu;
        }
        public static string CreateMaThinhGiang(BoPhan donVi)
        {
            string ma = string.Empty;
            ma = TaoSoHieuCongChuc.Get_SoHieuCongChuc_CYD(donVi).ToString();
            return ma;
        }
       
        public static string CreateMaQuanLyNhanVien(BoPhan donVi)
        {
            string result = string.Empty;
            //
            string maDonVi = string.Empty;
            if (donVi.STT < 10)
            {
                maDonVi = "00" + donVi.STT.ToString();
            }
            else
            {
                maDonVi = "0" + donVi.STT.ToString();
            }
           // string maQuanLyNext = TaoMaNhanVien.Get_NextMaQuanLy_DLU();

            //
            //result = string.Format("011.{0}.{1}", maDonVi, maQuanLyNext);
            //
            return result;
        }

        public static string CreateSoHoSoNhanVien(BoPhan donVi)
        {
            string result = string.Empty;
            //
            string maDonVi = string.Empty;
            if (donVi.STT < 10)
            {
                maDonVi = "00" + donVi.STT.ToString();
            }
            else
            {
                maDonVi = "0" + donVi.STT.ToString();
            }
            //string maQuanLyNext = TaoMaNhanVien.Get_NextSoHoSo_DLU();

            //
            //result = string.Format("D241.{0}.{1}", maDonVi, maQuanLyNext);
            //
            return result;
        }
        
      
       
    }
}
