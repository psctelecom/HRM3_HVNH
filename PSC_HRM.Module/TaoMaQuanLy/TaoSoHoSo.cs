using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSC_HRM.Module.CauHinh;
using PSC_HRM.Module;

namespace PSC_HRM.Module.TaoMaQuanLy
{
    public class TaoSoHoSo : MaQuanLyBase
    {
        public override string TaoMaQuanLy(params System.Data.SqlClient.SqlParameter[] args)
        {
            CauHinhChung cauHinh = HamDungChung.CauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhHoSo != null
                && cauHinh.CauHinhHoSo.TuDongTaoSoHoSo
                && cauHinh.CauHinhHoSo.MauSoHoSo.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetObject("spd_System_TaoSoHoSo", System.Data.CommandType.StoredProcedure, args);
                if (obj != null)
                {
                    string mau = obj.ToString();
                    string so = mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so)
                        && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = cauHinh.CauHinhHoSo.SoBatDauSoHoSo;
                }
                else
                    soThuTu = cauHinh.CauHinhHoSo.SoBatDauSoHoSo;
                return cauHinh.CauHinhHoSo.MauSoHoSo.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }
    }
}
