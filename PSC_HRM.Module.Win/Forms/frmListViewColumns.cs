using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PSC_HRM.Module.Win.Common;

namespace PSC_HRM.Module.Win.Forms
{
    public partial class frmListViewColumns : DevExpress.XtraEditors.XtraForm
    {
        private Type Type;
        private List<ObjectProperty> DataSource;

        public frmListViewColumns(Type type)
        {
            InitializeComponent();

            Type = type;
        }

        private void frmListViewColumns_Load(object sender, EventArgs e)
        {
            gridView1.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridView1.ShowField(new string[] { "Chon", "DisplayName" },
                new string[] { "Chọn", "Cột" },
                new int[] { 30, 300 });
            gridView1.ReadOnlyColumns("DisplayName");

            DataSource = ObjectPropertyHelper.GetDataSource(Type);
            gridControl1.DataSource = DataSource;
        }

        private void ckChonTatCa_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ObjectProperty item in DataSource)
            {
                if (item.Chon != ckChonTatCa.Checked)
                    item.Chon = ckChonTatCa.Checked;
            }
            gridControl1.RefreshDataSource();
        }

        public List<ObjectProperty> GetData()
        {
            var o = (from i in DataSource
                    where i.Chon == true
                    select i).ToList<ObjectProperty>();

            return o;
        }
    }
}