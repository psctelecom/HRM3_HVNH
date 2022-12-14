using System;
using System.ComponentModel;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.Report;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoMat
{
    [DefaultClassOptions]
    [DefaultProperty("Ten")]
    [ImageName("BO_PhanQuyenBaoCao")]
    [ModelDefault("Caption", "Phân quyền báo cáo")]
    public class PhanQuyenBaoCao : BaseObject
    {
        private string _Ten;

        [ModelDefault("Caption", "Tên")]
        public string Ten
        {
            get
            {
                return _Ten;
            }
            set
            {
                SetPropertyValue("Ten", ref _Ten, value);
            }
        }

        private string _Quyen;
        [ModelDefault("Caption", "Quyền")]
        [Browsable(false)]
        [Size(-1)]
        public string Quyen
        {
            get
            {
                return _Quyen;
            }
            set
            {
                SetPropertyValue("Quyen", ref _Quyen, value);
            }
        }

        public PhanQuyenBaoCao(Session session) : base(session) { }

        /// <summary>
        /// Get group report from report
        /// </summary>
        /// <returns></returns>
        public List<Guid> GetGroupReports()
        {
            List<Guid> result = new List<Guid>();

            using (DataTable dt = DataProvider.GetDataTable("spd_System_GetGroupReport", CommandType.StoredProcedure, new SqlParameter("@Report", Quyen))) 
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (!item.IsNull(0))
                        result.Add(new Guid(item[0].ToString()));
                }
            }
            return result;
        }

        /// <summary>
        /// Exists group report
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public bool IsExists(GroupReport g)
        {
            var data = GetGroupReports();
            var exists = (from d in data
                          where d == g.Oid
                          select d).SingleOrDefault();
            return exists != Guid.Empty;
        }
    }

}
