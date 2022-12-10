using System;
using System.Collections.Generic;
using PSC_HRM.Module.Win.QuyTrinh.Common;
using System.Threading.Tasks;
using System.Data;
using DevExpress.Xpo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    public partial class ChonDanhSachBoPhanController : BaseController
    {
        public BoPhanList DataSource { get; set; }
        public Session Session { get; set; }

        public ChonDanhSachBoPhanController()
        {
            InitializeComponent();
        }

        public ChonDanhSachBoPhanController(BoPhanList dataSource)
        {
            InitializeComponent();

            DataSource = dataSource;
        }

        private void ChonDanhSachNhanVienController_Load(object sender, EventArgs e)
        {
            gridViewBoPhan.InitGridView(true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewBoPhan.ShowField(new string[] { "Chon", "BoPhan" },
                new string[] { "Chọn", "Đơn vị" },
                new int[] { 50, 200 });
            gridViewBoPhan.ReadOnlyColumns(new string[] { "BoPhan" });
            gridViewBoPhan.SortField(DevExpress.Data.ColumnSortOrder.Descending, "BoPhan");

            if (DataSource == null)
                LoadDefaultData();

            bsBoPhan.DataSource = DataSource;
            layoutControl1.Invalidate();
        }

        public void RefreshData()
        {
            bsBoPhan.DataSource = DataSource;
            layoutControl1.Invalidate();
        }

        public List<Guid> GetBoPhanList()
        {
            if (DataSource != null)
                return DataSource.GetBoPhanList();
            return null;
        }

        private void miCheckAll_Click(object sender, EventArgs e)
        {
            XuLy1(true);
        }

        private void miCheckSelected_Click(object sender, EventArgs e)
        {
            XuLy2(true);
        }

        private void miUncheckAll_Click(object sender, EventArgs e)
        {
            XuLy1(false);
        }

        private void miUncheckSelected_Click(object sender, EventArgs e)
        {
            XuLy2(false);
        }

        private void LoadDefaultData()
        {
            DataSource = new BoPhanList();

            if (Session != null)
            {
                string query = "Select Oid, TenBoPhan as BoPhan From dbo.BoPhan Where GCRecord Is Null And LoaiBoPhan In (1, 2) Order By TenBoPhan";
                using (DataTable dt = DataProvider.GetDataTable(query, CommandType.Text))
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        DataSource.Add(new BoPhanItem
                        {
                            Oid = (Guid)item["Oid"],
                            BoPhan = item["BoPhan"].ToString()
                        });
                    }
                }
            }
        }

        private void XuLy1(bool state)
        {
            foreach (BoPhanItem item in DataSource)
            {
                if (item.Chon != state)
                    item.Chon = state;
            }
            gridBoPhan.RefreshDataSource();
            layoutControl1.Invalidate();
        }

        private void XuLy2(bool state)
        {
            int[] selectedRows = gridViewBoPhan.GetSelectedRows();
            BoPhanItem boPhan;
            foreach (int item in selectedRows)
            {
                boPhan = gridViewBoPhan.GetRow(item) as BoPhanItem;
                if (boPhan != null && boPhan.Chon != state)
                    boPhan.Chon = state;
            }
            gridBoPhan.RefreshDataSource();
            layoutControl1.Invalidate();
        }
    }
}
