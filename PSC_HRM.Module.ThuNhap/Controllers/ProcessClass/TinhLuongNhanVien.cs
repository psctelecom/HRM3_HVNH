using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.ThuNhap.Luong;
using System.Collections.Generic;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public class TinhLuongNhanVien : ITaiChinh
    {
        public void XuLy(IObjectSpace obs, BaseObject obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList)
        {
            BangLuongNhanVien bangLuong = obj as BangLuongNhanVien;
            if (bangLuong != null)
            {
                //xóa dữ liệu cũ
                Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_Luong_XoaLuongNhanVienTheoBangLuong", CommandType.StoredProcedure, new SqlParameter("@BangLuongNhanVien", bangLuong.Oid));

                //phân quyền bộ phận
                Utils.PhanQuyenDonVi(((XPObjectSpace)obs).Session, bangLuong.ThongTinTruong);

                string dieuKienNhanVien;

                foreach (CongThucTinhLuong ct in congThucTinhLuongList)
                {
                    if (!ct.NgungSuDung)
                    {
                        dieuKienNhanVien = ct.DieuKienNhanVien.XuLyDieuKien(obs, false, new object[] { bangLuong.KyTinhLuong.TuNgay, bangLuong.KyTinhLuong.DenNgay });

                        //
                        foreach (ChiTietCongThucTinhLuong ctItem in ct.ListChiTietCongThucTinhLuong)
                        {
                            if (!ctItem.NgungSuDung)
                            {
                                SqlParameter[] param = new SqlParameter[4];
                                param[0] = new SqlParameter("@BangLuongNhanVien", bangLuong.Oid);
                                param[1] = new SqlParameter("@DieuKienNhanVien", dieuKienNhanVien);
                                param[2] = new SqlParameter("@CongThucTinhLuong", ctItem.Oid);
                                param[3] = new SqlParameter("@ThongTinTruong", bangLuong.ThongTinTruong.Oid);

                                Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_Luong_TinhLuong", CommandType.StoredProcedure, param);
                            }
                        }
                    }
                }

                //Cập nhật lương nhân viên
                Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_Luong_CapNhatLuongNhanVien", CommandType.StoredProcedure, new SqlParameter("@BangLuongNhanVien", bangLuong.Oid));
            }
        }
    }
}
