using System;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using System.Data;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.ThuNhap.Thuong;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap.Luong;
using System.Collections.Generic;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;


namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public class TinhKhenThuongPhucLoi : ITaiChinh
    {
        public void XuLy(IObjectSpace obs, BaseObject obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList)
        {
            BangThuongNhanVien bangThuong = obj as BangThuongNhanVien;
            if (bangThuong != null)
            {
                using (DevExpress.Utils.WaitDialogForm dialog = new DevExpress.Utils.WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
                {
                    //xóa dữ liệu cũ
                    Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_Thuong_XoaChiTietThuongNhanVienTheoBangThuongNhanVien", CommandType.StoredProcedure, new SqlParameter("@BangThuongNhanVien", bangThuong.Oid));

                    //phan quyen
                    Utils.PhanQuyenDonVi(((XPObjectSpace)obs).Session, bangThuong.ThongTinTruong);

                    string dieuKienNhanVien;
                    foreach (CongThucKhenThuongPhucLoi ctItem in bangThuong.LoaiKhenThuongPhucLoi.CongThucList)
                    {
                        dieuKienNhanVien = ctItem.DieuKienNhanVien.XuLyDieuKien(obs, false,
                            bangThuong.KyTinhLuong.TuNgay, bangThuong.KyTinhLuong.DenNgay);

                        SqlParameter[] param = new SqlParameter[4];
                        param[0] = new SqlParameter("@BangThuongNhanVien", bangThuong.Oid);
                        param[1] = new SqlParameter("@CongThuc", ctItem.Oid);
                        param[2] = new SqlParameter("@DieuKienNhanVien", dieuKienNhanVien);
                        param[3] = new SqlParameter("@ThongTinTruong", bangThuong.ThongTinTruong.Oid);

                        Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_Thuong_TinhKhenThuongPhucLoi", CommandType.StoredProcedure, param);
                    }
                }
            }
        }
    }

}
