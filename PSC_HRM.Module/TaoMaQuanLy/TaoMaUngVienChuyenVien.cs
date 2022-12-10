using PSC_HRM.Module.CauHinh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using PSC_HRM.Module;

namespace PSC_HRM.Module.TaoMaQuanLy
{
    public class TaoMaUngVienChuyenVien : MaQuanLyBase
    {
        public override string TaoMaQuanLy(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = HamDungChung.CauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhTuyenDung != null
                && cauHinh.CauHinhTuyenDung.TuDongTaoSoBaoDanh
                && cauHinh.CauHinhTuyenDung.MaSoBaoDanhChuyenVien.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetObject("spd_System_TaoMaUngVienChuyenVien", System.Data.CommandType.StoredProcedure, args);
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
                return cauHinh.CauHinhTuyenDung.MaSoBaoDanhChuyenVien.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }
    }
}
