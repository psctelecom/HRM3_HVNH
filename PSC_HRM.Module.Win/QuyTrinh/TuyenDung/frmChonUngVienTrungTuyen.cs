using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.TuyenDung;

namespace PSC_HRM.Module.Win.QuyTrinh.TuyenDung
{
    public partial class frmChonUngVienTrungTuyen : XtraForm
    {
        private List<TuyenDung_TrungTuyen> ListUngVien;

        public frmChonUngVienTrungTuyen(List<TuyenDung_TrungTuyen> list)
        {
            InitializeComponent();

            ListUngVien = list;
            gridControl1.DataSource = list;
            layoutControl1.Refresh();
        }

        private void frmChonUngVienTrungTuyen_Load(object sender, EventArgs e)
        {
            gridView1.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridView1.ShowField(new string[] { "Chon", "TrungTuyen.UngVien.HoTen", "TrungTuyen.UngVien.NhuCauTuyenDung.Caption" }, new string[] { "Chọn", "Họ tên", "Vị trí ứng tuyển" }, new int[] { 50, 80, 100 });
            gridView1.ReadOnlyColumns(new string[] { "TrungTuyen.UngVien.HoTen", "TrungTuyen.UngVien.NhuCauTuyenDung.Caption" });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public List<TrungTuyen> GetListUngVien()
        {
            var uv = from u in ListUngVien
                     where u.Chon == true
                     select u.TrungTuyen;

            return uv.ToList();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (TuyenDung_TrungTuyen item in ListUngVien)
            {
                if (item.Chon != checkEdit1.Checked)
                    item.Chon = checkEdit1.Checked;
            } 
        }
    }
}