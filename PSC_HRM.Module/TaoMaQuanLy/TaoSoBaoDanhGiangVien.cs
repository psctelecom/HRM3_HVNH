using PSC_HRM.Module.CauHinh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using PSC_HRM.Module;

namespace PSC_HRM.Module.TaoMaQuanLy
{
    public class TaoSoBaoDanhGiangVien : MaQuanLyBase
    {
        public override string TaoMaQuanLy(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = HamDungChung.CauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhHoSo != null
                && cauHinh.CauHinhTuyenDung.TuDongTaoSoBaoDanh
                && cauHinh.CauHinhTuyenDung.MauSoBaoDanhGiangVien.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetObject("spd_System_TaoSoBaoDanh", System.Data.CommandType.StoredProcedure, args);
                if (obj != null)
                {
                    string mau = obj.ToString();
                    string so = mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so)
                        && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = 0;
                }
                else
                    soThuTu = 0;
                return cauHinh.CauHinhTuyenDung.MauSoBaoDanhGiangVien.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }
    }
}
