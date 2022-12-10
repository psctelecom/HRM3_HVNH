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
    public class DanhSachPhuCapLanhDao : BaseObject
    {
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<ChiTietPhuCapLanhDao> ListChiTietPhuCapLanhDao { get; set; }

        public DanhSachPhuCapLanhDao(Session session) : base(session) { }

        public void LoadData()
        {
            List<Guid> oid = new List<Guid>();
            //
            SqlParameter[] param = new SqlParameter[0];
            ListChiTietPhuCapLanhDao = new XPCollection<ChiTietPhuCapLanhDao>(Session, false);
            DataTable dt = DataProvider.GetDataTable("spd_XuLy_DanhSachCanBoCoChucVu", CommandType.StoredProcedure, param);
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
                    ChiTietPhuCapLanhDao chiTiet;
                    foreach (ThongTinNhanVien nv in nvList)
                    {
                        chiTiet = new ChiTietPhuCapLanhDao(Session);
                        chiTiet.BoPhan = nv.BoPhan;
                        chiTiet.ThongTinNhanVien = nv;
                        ListChiTietPhuCapLanhDao.Add(chiTiet);
                    }
                }
            }
        }

        public void XuLy()
        {
            foreach (ChiTietPhuCapLanhDao item in ListChiTietPhuCapLanhDao)
            {
                item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCLanhDao = item.HSPCQuanLy;
            }
        }
    }
}
