using PSC_HRM.Module.CauHinh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.TaoMaQuanLy
{
    public class TaoMaNhanVien : MaQuanLyBase
    {
        public override string TaoMaQuanLy(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = HamDungChung.CauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhHoSo != null
                && cauHinh.CauHinhHoSo.TuDongTaoMaNhanVien
                && cauHinh.CauHinhHoSo.MauMaNhanVien.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetObject("spd_System_TaoMaNhanVien", System.Data.CommandType.StoredProcedure, args);
                if (obj != null)
                {
                    string mau = obj.ToString();
                    string so = mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so)
                        && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = cauHinh.CauHinhHoSo.SoBatDauMaNhanVien;
                }
                else
                    soThuTu = cauHinh.CauHinhHoSo.SoBatDauMaNhanVien;
                return cauHinh.CauHinhHoSo.MauMaNhanVien.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }
        public static string Get_NextSoHoSo_DLU()
        {
            string result = string.Empty;

            try
            {
                //Lấy mã quản lý lớn nhất của nhân viên theo bộ phận
                string soHoSoMax = Get_SoHoSoLonNhat_DLU();

                if (!string.IsNullOrEmpty(soHoSoMax))
                {
                    //Lấy mã quản lý kế tiếp
                    int soHoSoNext = Convert.ToInt32(soHoSoMax) + 1;
                    //
                    if (soHoSoNext < 10)
                    {
                        result = string.Format("0000{0}", soHoSoNext);
                    }
                    else if (soHoSoNext < 100 && soHoSoNext >= 10)
                    {
                        result = string.Format("000{0}", soHoSoNext);
                    }
                    else if (soHoSoNext < 1000 && soHoSoNext >= 100)
                    {
                        result = string.Format("00{0}", soHoSoNext);
                    }
                    else if (soHoSoNext < 10000 && soHoSoNext >= 1000)
                    {
                        result = string.Format("0{0}", soHoSoNext);
                    }
                    else if (soHoSoNext < 100000 && soHoSoNext >= 10000)
                    {
                        result = string.Format("0{0}", soHoSoNext);
                    }
                    else
                    {
                        result = string.Format("{0}", soHoSoNext);
                    }
                }
                else
                {
                    result = "000001";
                }

            }
            catch (Exception ex)
            {
                DialogUtil.ShowError("Không thể tạo mã quản lý. Vui lòng liên hệ người quản trị phần mền.");
            }

            return result;
        }

        public static string Get_NextMaQuanLy_DLU()
        {
            string result = string.Empty;

            try
            {

                //Lấy mã quản lý lớn nhất của nhân viên theo bộ phận
                string maQuanLyMax = Get_MaQuanLyLonNhat_DLU();

                if (!string.IsNullOrEmpty(maQuanLyMax))
                {
                    //Lấy mã quản lý kế tiếp
                    int maQuanLyNext = Convert.ToInt32(maQuanLyMax) + 1;
                    //
                    if (maQuanLyNext < 10)
                    {
                        result = string.Format("0000{0}", maQuanLyNext);
                    }
                    else if (maQuanLyNext < 100 && maQuanLyNext >= 10)
                    {
                        result = string.Format("000{0}", maQuanLyNext);
                    }
                    else if (maQuanLyNext < 1000 && maQuanLyNext >= 100)
                    {
                        result = string.Format("00{0}", maQuanLyNext);
                    }
                    else if (maQuanLyNext < 10000 && maQuanLyNext >= 1000)
                    {
                        result = string.Format("0{0}", maQuanLyNext);
                    }
                    else
                    {
                        result = string.Format("{0}", maQuanLyNext);
                    }
                }
                else
                {
                    result = "00001";
                }

            }
            catch (Exception ex)
            {
                DialogUtil.ShowError("Không thể tạo mã quản lý. Vui lòng liên hệ người quản trị phần mền.");
            }

            return result;
        }

        public static long Get_NextMaQuanLy_IUH(BoPhan coSo, BoPhan donVi)
        {
            long maQuanLyNext = 0;

            try
            {
                int chieuDaiMaCoSo = coSo.MaQuanLy.ToString().Trim().Length;
                int chieuDaiMaDonVi = donVi.MaQuanLy.ToString().Trim().Length;
                int tongChieuDai = chieuDaiMaCoSo + chieuDaiMaDonVi;

                //Lấy mã quản lý lớn nhất của nhân viên theo bộ phận
                string maQuanLyMax = Get_MaQuanLyLonNhat_IUH(donVi.Oid, donVi.MaQuanLy, chieuDaiMaCoSo, chieuDaiMaDonVi);

                if (!string.IsNullOrEmpty(maQuanLyMax))
                {
                    //Lấy mã quản lý kế tiếp
                    maQuanLyNext = Convert.ToInt32(maQuanLyMax.Substring(tongChieuDai));

                    maQuanLyNext = Convert.ToInt32(maQuanLyNext) + 1;
                }
                else
                {
                    maQuanLyNext = 1;
                }

            }
            catch (Exception ex)
            {
                DialogUtil.ShowError("Không thể tạo mã quản lý. Vui lòng liên hệ người quản trị phần mền.");
            }

            return maQuanLyNext;
        }

        private static string Get_MaQuanLyLonNhat_IUH(Guid maBoPhan, string maQuanLy, int chieuDaiMaCoSo, int chieuDaiMaDonVi)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MaBoPhan", maBoPhan);
            param[1] = new SqlParameter("@MaQuanLy", maQuanLy);
            param[2] = new SqlParameter("@ChieuDaiMaCoSo", chieuDaiMaCoSo);
            param[3] = new SqlParameter("@ChieuDaiMaDonVi", chieuDaiMaDonVi);

            //Lấy mã quản lý lớn nhất theo bộ phận
            object obj = DataProvider.GetObject("spd_System_TaoMaNhanVien_IUH", System.Data.CommandType.StoredProcedure, param);
            if (obj != null)
            {
                return obj.ToString();
            }
            return string.Empty;
        }
        private static string Get_SoHoSoLonNhat_DLU()
        {
            //Lấy mã quản lý lớn nhất theo bộ phận
            object obj = DataProvider.GetObject("spd_System_SoHoSoLonNhat", System.Data.CommandType.StoredProcedure);
            if (obj != null)
            {
                return obj.ToString();
            }
            return string.Empty;
        }
        private static string Get_MaQuanLyLonNhat_DLU()
        {
            //Lấy mã quản lý lớn nhất theo bộ phận
            object obj = DataProvider.GetObject("spd_System_MaNhanVienLonNhat", System.Data.CommandType.StoredProcedure);
            if (obj != null)
            {
                return obj.ToString();
            }
            return string.Empty;
        }

        public static string TaoMaQuanLy_UTE(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = HamDungChung.CauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhHoSo != null
                && cauHinh.CauHinhHoSo.TuDongTaoMaNhanVien
                && cauHinh.CauHinhHoSo.MauMaNhanVien.IsTemplate())
            {
                object obj = DataProvider.GetObject("spd_HRM_GetLaySoHoSo", System.Data.CommandType.StoredProcedure, args);
                if (obj != null)
                    return obj.ToString();
            }
            return string.Empty;
        }
    }
}
