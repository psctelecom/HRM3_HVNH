using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.ThuNhap.NgoaiGio;
using PSC_HRM.Module.ThuNhap.Luong;
using System.Collections.Generic;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public class TinhTienNgoaiGio : ITaiChinh
    {
        public void XuLy(IObjectSpace obs, BaseObject obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList)
        {
            BangLuongNgoaiGio bangLuong = obj as BangLuongNgoaiGio;
            if (bangLuong != null)
            {
                using (WaitDialogForm wait = new WaitDialogForm("Hệ thống đang xử lý.", "Vui lòng chờ..."))
                {
                    //xóa dữ liệu cũ
                    Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_NgoaiGio_XoaChiTietLuongNgoaiGioTheoBangLuongNgoaiGio", CommandType.StoredProcedure, new SqlParameter("@BangLuongNgoaiGio", bangLuong.Oid));

                    //Lấy danh sách công thức tính lương
                    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinTruong=?",
                        bangLuong.ThongTinTruong.Oid);
                    using (XPCollection<CongThucTinhNgoaiGio> listCongThuc = new XPCollection<CongThucTinhNgoaiGio>(((XPObjectSpace)obs).Session, filter))
                    {
                        foreach (CongThucTinhNgoaiGio ct in listCongThuc)
                        {
                            SqlParameter[] param = new SqlParameter[2];
                            param[0] = new SqlParameter("@BangLuongNgoaiGio", bangLuong.Oid);
                            param[1] = new SqlParameter("@CongThuc", ct.Oid);

                            Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_NgoaiGio_TinhLuongNgoaiGio", CommandType.StoredProcedure, param);
                        }
                    }
                }
            }
        }
    }
}
