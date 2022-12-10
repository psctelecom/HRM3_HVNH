using System;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap;
using System.Collections.Generic;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.ThuNhap.Import;

namespace PSC_HRM.Module.Win.Forms
{
    public partial class frmChonNhanVienExport : XtraForm
    {
        private List<ExportItem> dataSource;
        private Session session;

        public frmChonNhanVienExport()
        {
            InitializeComponent();
        }

        public frmChonNhanVienExport(Session session)
        {
            InitializeComponent();
            this.session = session;
        }

        private void frmChonNhanVienExport_Load(object sender, EventArgs e)
        {
            GridViewHelper.InitGridView(gridView1, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            GridViewHelper.ShowField(gridView1, new string[] { "Chon", "SoHieuCongChuc", "Ho", "Ten", "BoPhan" },
                new string[] { "Chọn", "Số hiệu công chức", "Họ", "Tên", "Đơn vị" },
                new int[] { 30, 70, 70, 50, 150 });
            GridViewHelper.ReadOnlyColumns(gridView1, new string[] { "SoHieuCongChuc", "Ho", "Ten", "BoPhan" });
            GridViewHelper.GroupField(gridView1, "BoPhan");

            dataSource = ExportTemplateHelper.LoadData();
            gridControl1.DataSource = dataSource;
        }

        public List<ExportItem> GetData()
        {
            return dataSource.FindAll(p => p.Chon == true);
        }
    }
}