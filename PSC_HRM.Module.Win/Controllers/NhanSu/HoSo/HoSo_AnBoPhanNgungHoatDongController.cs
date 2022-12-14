using System;

using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using PSC_HRM.Module.BaoMat;

using PSC_HRM.Module;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using System.Data.SqlClient;
using PSC_HRM.Module.Win.Editors;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class HoSo_AnBoPhanNgungHoatDongController : ViewController
    {
        public HoSo_AnBoPhanNgungHoatDongController()
        {
            InitializeComponent();
            RegisterActions(components);
            //
            //TargetObjectType = typeof(IBoPhan);
        }

        private void HoSo_AnBoPhanNgungHoatDongController_Activated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;

            if (listView!= null && listView.Editor is CustomCategorizedListEditor)
            {
                //// Chức năng này dùng để ẩn những bộ phận ngừng hoạt động đi
                HamDungChung.CapNhatBoPhanNgungHoatDong(false);
            }
            else
            {
                //// Chức năng này dùng để hiện những bộ phận ngừng hoạt động lên
                HamDungChung.CapNhatBoPhanNgungHoatDong(true);
            }
        }

       
    }
}
