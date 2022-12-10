using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn đơn vị")]
    public class DanhSachPhuCapThamNien : BaseObject
    {
        public XPCollection<ChiTietPhuCapThamNien> ListChiTietPhuCapThamNien { get; set; }

        public DanhSachPhuCapThamNien(Session session) : base(session) { }

        public void LoadData()
        {

            List<Guid> oid = new List<Guid>();
            //
            SqlParameter[] param = new SqlParameter[0];
            ListChiTietPhuCapThamNien = new XPCollection<ChiTietPhuCapThamNien>(Session, false);
            DataTable dt = DataProvider.GetDataTable("spd_XuLy_DanhSachCanBoCoThamNien", CommandType.StoredProcedure, param);
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        oid.Add((Guid)item["Oid"]);
                    }
                }

                CriteriaOperator filter = new InOperator("Oid", oid);
                using (XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(Session, filter))
                {
                    ChiTietPhuCapThamNien chiTiet;
                    foreach (ThongTinNhanVien nv in nvList)
                    {
                        chiTiet = new ChiTietPhuCapThamNien(Session);
                        chiTiet.BoPhan = nv.BoPhan;
                        chiTiet.ThongTinNhanVien = nv;
                        ListChiTietPhuCapThamNien.Add(chiTiet);
                    }
                }
            }
        }

        public void XuLy()
        {
            foreach (ChiTietPhuCapThamNien item in ListChiTietPhuCapThamNien)
            {
                item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNienTrongTruong = item.HSPCThamNien;
            }
        }
    }

}
