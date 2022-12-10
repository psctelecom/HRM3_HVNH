using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSC_HRM.Module.CauHinh;
using PSC_HRM.Module;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.TaoMaQuanLy
{
    public class TaoSoHieuCongChuc : MaQuanLyBase
    {
        public override string TaoMaQuanLy(params System.Data.SqlClient.SqlParameter[] args)
        {
            CauHinhChung cauHinh = HamDungChung.CauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhHoSo != null
                && cauHinh.CauHinhHoSo.TuDongTaoSoHieuCongChuc
                && cauHinh.CauHinhHoSo.MauSoHieuCongChuc.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetObject("spd_System_TaoSoHieuCongChuc", System.Data.CommandType.StoredProcedure, args);
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

        public static string Get_SoHieuCongChuc_CYD(BoPhan donVi)
        {
            int soHieuNext = 0;
            CauHinhChung cauHinh = HamDungChung.CauHinhChung;
            try
            {
                
                //Lấy mã quản lý lớn nhất của nhân viên theo bộ phận
                string soHieuMax = Set_SoHieuCongChuc_CYD(donVi.Oid);
                if (!string.IsNullOrEmpty(soHieuMax) && soHieuMax !="00001")
                {
                    //Lấy mã quản lý kế tiếp
                    soHieuNext = Convert.ToInt32(soHieuMax);
                }
                else
                {
                    string Tam = donVi.MaQuanLy + "001";
                    soHieuNext = Convert.ToInt32(Tam);
                }
            }
            catch (Exception ex)
            {
                DialogUtil.ShowError("Không thể tạo số hiệu viên chức. Vui lòng liên hệ người quản trị phần mền.");
            }
            return cauHinh.CauHinhHoSo.MauSoHieuCongChuc.CreateTemplate(soHieuNext);
        }
        public static string Set_SoHieuCongChuc_CYD(Guid maBoPhan)
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
                object obj = DataProvider.GetObject("spd_System_TaoSoHieuCongChuc_CYD", System.Data.CommandType.StoredProcedure, param);
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
