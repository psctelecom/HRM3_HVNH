using System;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using System.Data;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.ThuNhap.KhauTru;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap.Luong;
using System.Collections.Generic;
using PSC_HRM.Module.ThuNhap;


namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public class TinhKhauTruLuong : ITaiChinh
    {
        public void XuLy(IObjectSpace obs, BaseObject obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList)
        {
            BangKhauTruLuong bangKhauTruLuong = obj as BangKhauTruLuong;
            if (bangKhauTruLuong != null)
            {
                using (DevExpress.Utils.WaitDialogForm dialog = new DevExpress.Utils.WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
                {
                    //xóa dữ liệu cũ
                    Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_KhauTruLuong_XoaChiTietKhauTruLuongTheoBangKhauTruLuong", CommandType.StoredProcedure, new SqlParameter("@BangKhauTruLuong", bangKhauTruLuong.Oid));

                    //phân quyền bộ phận
                    Utils.PhanQuyenDonVi(((XPObjectSpace)obs).Session, bangKhauTruLuong.ThongTinTruong);

                    //tính khấu trừ
                    string dieuKienNhanVien = bangKhauTruLuong.LoaiKhauTruLuong.CongThucTinhKhauTru.DieuKienNhanVien.XuLyDieuKien(obs, false,
                            bangKhauTruLuong.KyTinhLuong.TuNgay, bangKhauTruLuong.KyTinhLuong.DenNgay);

                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@BangKhauTruLuong", bangKhauTruLuong.Oid);
                    param[1] = new SqlParameter("@CongThuc", bangKhauTruLuong.LoaiKhauTruLuong.CongThucTinhKhauTru.Oid);
                    param[2] = new SqlParameter("@DieuKienNhanVien", dieuKienNhanVien);
                    param[3] = new SqlParameter("@ThongTinTruong", bangKhauTruLuong.ThongTinTruong.Oid);
                    Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_KhauTruLuong_TinhKhauTruLuong", CommandType.StoredProcedure, param);

                }
            }
        }
    }

}
