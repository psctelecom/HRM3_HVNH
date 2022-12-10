using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSC_HRM.Module.CauHinh;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.TaoMaQuanLy
{
    public class TaoMaGiangVienThinhGiang : MaQuanLyBase
    {
        public override string TaoMaQuanLy(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = HamDungChung.CauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhHoSo != null
                && cauHinh.CauHinhHoSo.TuDongTaoMaGiangVienThinhGiang
                && cauHinh.CauHinhHoSo.MauMaGiangVienThinhGiang.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetObject("spd_System_TaoMaGiangVienThinhGiang", System.Data.CommandType.StoredProcedure, args);
                if (obj != null)
                {
                    string mau = obj.ToString();
                    string so = mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so)
                        && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = 1;
                }
                else
                    soThuTu = 1;
                return cauHinh.CauHinhHoSo.MauMaGiangVienThinhGiang.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }

        public static string Get_MaThinhGiang_CYD(BoPhan donVi)
        {
            int maNext = 0;
            CauHinhChung cauHinh = HamDungChung.CauHinhChung;
            try
            {

                //Lấy mã quản lý lớn nhất của nhân viên theo bộ phận
                string maMax = Set_MaThinhGiang_CYD(donVi.Oid);
                if (!string.IsNullOrEmpty(maMax) && maMax != "00001")
                {
                    //Lấy mã quản lý kế tiếp
                    maNext = Convert.ToInt32(maMax);
                }
                else
                {
                    string Tam = donVi.MaQuanLy + "001";
                    maNext = Convert.ToInt32(Tam);
                }
            }
            catch (Exception ex)
            {
                DialogUtil.ShowError("Không thể tạo số hiệu viên chức. Vui lòng liên hệ người quản trị phần mền.");
            }
            return cauHinh.CauHinhHoSo.MauSoHieuCongChuc.CreateTemplate(maNext);
        }
        public static string Set_MaThinhGiang_CYD(Guid maBoPhan)
        {

            CauHinhChung cauHinh = HamDungChung.CauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhHoSo != null
                && cauHinh.CauHinhHoSo.TuDongTaoSoHieuCongChuc
                && cauHinh.CauHinhHoSo.MauSoHieuCongChuc.IsTemplate())
            {
                int soThuTu;
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@BoPhan", maBoPhan);
                object obj = DataProvider.GetObject("spd_System_MaThinhGiang_CYD", System.Data.CommandType.StoredProcedure, param);
                if (obj != null)
                {
                    string mau = obj.ToString();
                    string so = mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so) && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = cauHinh.CauHinhHoSo.SoBatDauSoHieuCongChuc;
                }
                else
                    soThuTu = cauHinh.CauHinhHoSo.SoBatDauSoHieuCongChuc;
                return cauHinh.CauHinhHoSo.MauSoHieuCongChuc.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }

    }
}
