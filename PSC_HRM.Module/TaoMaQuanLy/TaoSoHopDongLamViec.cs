using PSC_HRM.Module.CauHinh;
using PSC_HRM.Module;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.TaoMaQuanLy
{
    public class TaoSoHopDongLamViec : MaQuanLyBase
    {
        public override string TaoMaQuanLy(params System.Data.SqlClient.SqlParameter[] args)
        {
            CauHinhChung cauHinh = HamDungChung.CauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhHopDong != null
                && cauHinh.CauHinhHopDong.TuDongTaoSoHopDong
                && cauHinh.CauHinhHopDong.MauSoHopDongLamViec.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetObject("spd_System_TaoSoHopDongLamViec", System.Data.CommandType.StoredProcedure, args);
                  if (!TruongConfig.MaTruong.Equals("HBU"))
                  {
                    if (obj != null)
                    {
                        string mau = obj.ToString();
                        string so = mau.GetNumberFromTemplate();
                        if (!string.IsNullOrWhiteSpace(so)
                            && int.TryParse(so, out soThuTu))
                            soThuTu++;
                        else
                            soThuTu = cauHinh.CauHinhHopDong.SoBatDau;
                    }
                    else
                        soThuTu = cauHinh.CauHinhHopDong.SoBatDau;
                    return cauHinh.CauHinhHopDong.MauSoHopDongLamViec.CreateTemplate(soThuTu);
                }
                else
                    return obj.ToString();
            }
            return string.Empty;
        }
    }
}
