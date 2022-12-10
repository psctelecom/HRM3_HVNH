using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ModelDefault("Caption","Quản lý thời khóa biểu")]
    public class QuanLyThoiKhoaBieu : ThongTinChungPMS
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh Sách Thời Khóa Biểu")]
        [Association("QuanLyThoiKhoaBieu-ListThoiKhoaBieu")]
        public XPCollection<ThoiKhoaBieu> ListThoiKhoaBieu
        {
            get
            {
                return GetCollection<ThoiKhoaBieu>("ListThoiKhoaBieu");
            }
        }
        public QuanLyThoiKhoaBieu(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
    }
}
