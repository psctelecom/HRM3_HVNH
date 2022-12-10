using System;
using System.Collections.Generic;
using PSC_HRM.Module.Win.QuyTrinh.Common;
using System.Threading.Tasks;
using System.Data;
using DevExpress.Xpo;
using System.Data.SqlClient;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    public partial class ChonDanhSachNhanVienController : BaseController
    {
        public NhanVienList DataSource { get; set; }
        public Session Session { get; set; }

        public ChonDanhSachNhanVienController()
        {
            InitializeComponent();
        }

        public ChonDanhSachNhanVienController(NhanVienList dataSource)
        {
            InitializeComponent();

            DataSource = dataSource;
        }

        public ChonDanhSachNhanVienController(Session session)
        {
            InitializeComponent();

            Session = session;
        }

        private void ChonDanhSachNhanVienController_Load(object sender, EventArgs e)
        {
            gridViewNhanVien.InitGridView(true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewNhanVien.ShowField(new string[] { "Chon", "Ho", "Ten", "BoPhan" },
                new string[] { "Chọn", "Họ", "Tên", "Đơn vị" },
                new int[] { 50, 100, 50, 200 });
            gridViewNhanVien.ReadOnlyColumns(new string[] { "Ho", "Ten", "BoPhan" });
            gridViewNhanVien.GroupField("BoPhan");
            gridViewNhanVien.SortField(DevExpress.Data.ColumnSortOrder.Ascending, new string[] { "BoPhan", "Ten", "Ho" });

            if (DataSource == null)
                LoadDefaultData();

            bsNhanVien.DataSource = DataSource;

            layoutControl1.Invalidate();
        }

        public List<Guid> GetNhanVienList()
        {
            if (DataSource != null)
                return DataSource.GetNhanVienList();
            return null;
        }

        public void RefreshData()
        {
            bsNhanVien.DataSource = DataSource;
            layoutControl1.Invalidate();
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
            DataSource = new NhanVienList();
            
            if (Session != null)
            {
                string query = "spd_System_GetNhanVien";
                using (DataTable dt = DataProvider.GetDataTable(query, CommandType.StoredProcedure, new SqlParameter("@BoPhan", HamDungChung.GetPhanQuyenBoPhan())))
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        DataSource.Add(new NhanVienItem
                        {
                            Oid = (Guid)item["Oid"],
                            Ho = item["Ho"].ToString(),
                            Ten = item["Ten"].ToString(),
                            BoPhan = item["BoPhan"].ToString()
                        });
                    }
                }
            }
        }

        private void XuLy1(bool state)
        {
            foreach (NhanVienItem nhanVienItem in DataSource)
            {
                if (nhanVienItem.Chon != state)
                    nhanVienItem.Chon = state;
            }
            gridNhanVien.RefreshDataSource();
            layoutControl1.Invalidate();
        }

        private void XuLy2(bool state)
        {
            int[] selectedRows = gridViewNhanVien.GetSelectedRows();
            NhanVienItem nhanVien;
            foreach (int item in selectedRows)
            {
                nhanVien = gridViewNhanVien.GetRow(item) as NhanVienItem;
                if (nhanVien != null && nhanVien.Chon != state)
                    nhanVien.Chon = state;
            }
            gridNhanVien.RefreshDataSource();
            layoutControl1.Invalidate();
        }
    }
}
