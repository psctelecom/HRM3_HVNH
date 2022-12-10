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
    public class DanhSachPhuCapChuyenMon : BaseObject
    {
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<ChiTietPhuCapChuyenMon> ListChiTietPhuCapChuyenMon { get; set; }

        public DanhSachPhuCapChuyenMon(Session session) : base(session) { }

        public override void AfterConstruction()
        {
 	        base.AfterConstruction();
            //
            LoadData();
        }

        public void LoadData()
        {
            List<Guid> oid = new List<Guid>();
            //
            SqlParameter[] param = new SqlParameter[0];
            ListChiTietPhuCapChuyenMon = new XPCollection<ChiTietPhuCapChuyenMon>(Session, false);
            DataTable dt = DataProvider.GetDataTable("spd_XuLy_DanhSachCanBoCoTrinhDo", CommandType.StoredProcedure, param);
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        oid.Add((Guid)item["Oid"]);
                    }
                }
            }
            CriteriaOperator filter = new InOperator("Oid", oid);
            using (XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(Session, filter))
            {
                ChiTietPhuCapChuyenMon chiTiet;
                foreach (ThongTinNhanVien nv in nvList)
                {
                    chiTiet = new ChiTietPhuCapChuyenMon(Session);
                    chiTiet.BoPhan = nv.BoPhan;
                    chiTiet.ThongTinNhanVien = nv;
                    ListChiTietPhuCapChuyenMon.Add(chiTiet);
                }
            }
        }

        public void XuLy()
        {
            foreach (ChiTietPhuCapChuyenMon item in ListChiTietPhuCapChuyenMon)
            {
                item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCChuyenMon = item.HSPCChuyenMon;
            }
        }
    }
}
