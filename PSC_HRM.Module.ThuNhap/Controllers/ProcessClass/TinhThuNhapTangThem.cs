using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.ThuNhap.ThuNhapTangThem;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public class TinhThuNhapTangThem : ITaiChinh
    {
        public void XuLy(IObjectSpace obs, BaseObject obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList)
        {
            BangThuNhapTangThem bangThuNhapTangThem = obj as BangThuNhapTangThem;
            if (bangThuNhapTangThem != null)
            {
                //xóa dữ liệu cũ
                Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_ThuNhapTangThem_XoaChiTietThuNhapTangThemTheoBangThuNhapTangThem", CommandType.StoredProcedure, new SqlParameter("@BangThuNhapTangThem", bangThuNhapTangThem.Oid));

                //Lấy danh sách công thức tính lương
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinTruong=?",
                    bangThuNhapTangThem.ThongTinTruong.Oid);
                using (XPCollection<CongThucTinhThuNhapTangThem> listCongThuc = new XPCollection<CongThucTinhThuNhapTangThem>(((XPObjectSpace)obs).Session, filter))
                {
                    string dieuKienNhanVien;

                    //phân quyền bộ phận
                    Utils.PhanQuyenDonVi(((XPObjectSpace)obs).Session, bangThuNhapTangThem.ThongTinTruong);

                    //tinh thu nhap tang them
                    foreach (CongThucTinhThuNhapTangThem ct in listCongThuc)
                    {
                        if (!ct.NgungSuDung)
                        {
                            dieuKienNhanVien = ct.DieuKienNhanVien.XuLyDieuKien(obs, false,
                                bangThuNhapTangThem.KyTinhLuong.TuNgay, bangThuNhapTangThem.KyTinhLuong.DenNgay);

                            SqlParameter[] param = new SqlParameter[4];
                            param[0] = new SqlParameter("@BangThuNhapTangThem", bangThuNhapTangThem.Oid);
                            param[1] = new SqlParameter("@DieuKienNhanVien", dieuKienNhanVien);
                            param[2] = new SqlParameter("@CongThucTinhThuNhapTangThem", ct.Oid);
                            param[3] = new SqlParameter("@ThongTinTruong", HamDungChung.ThongTinTruong(((XPObjectSpace)obs).Session).Oid);

                            Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_ThuNhapTangThem_TinhThuNhapTangThem", CommandType.StoredProcedure, param);
                        }
                    }
                }
            }
        }
    }
}
