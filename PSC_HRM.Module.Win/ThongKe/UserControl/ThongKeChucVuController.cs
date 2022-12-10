using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using DevExpress.Xpo;

namespace PSC_HRM.Module.Win.ThongKe
{
    public partial class ThongKeChucVuController : ThongKeBaseController
    {
        //
        public ThongKeChucVuController(XafApplication app)
        {
            InitializeComponent();
        }

        private void ThongKeChucVuController_Load(object sender, EventArgs e)
        {
            DataTable dataList = DataProvider.GetDataTable("spd_BieuDo_ThongKeChucVu", CommandType.StoredProcedure);
            //
            chart_Column.DataSource = dataList;
            chart_Circle.DataSource = dataList;
        }

        private void InChart_Circle_Click(object sender, EventArgs e)
        {
            PrintChart(chart_Circle);
            //
        }

        private void InChart_Column_Click(object sender, EventArgs e)
        {
            PrintChart(chart_Column);
        }
    }
}
