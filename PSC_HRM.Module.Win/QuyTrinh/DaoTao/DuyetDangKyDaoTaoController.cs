using System;
using System.Collections.Generic;
using PSC_HRM.Module.DaoTao;
using System.Linq;

namespace PSC_HRM.Module.Win.QuyTrinh.DaoTao
{
    public partial class DuyetDangKyDaoTaoController : BaseController
    {
        private List<DuyetDangKyDaoTaoItem> _DataSource;
        public DuyetDangKyDaoTaoController()
        {
            InitializeComponent();
        }

        public DuyetDangKyDaoTaoController(QuanLyDaoTao quanLyDaoTao)
        {
            InitializeComponent();

            _DataSource = new List<DuyetDangKyDaoTaoItem>();

            foreach (DangKyDaoTao item in quanLyDaoTao.ListDangKyDaoTao)
            {
                _DataSource.Add(new DuyetDangKyDaoTaoItem { DangKyDaoTao = item });
            }

            bindingSource.DataSource = typeof(List<DuyetDangKyDaoTaoItem>);
            bindingSource.DataSource = _DataSource;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();
        }

        private void DangKyThiDuaController_Load(object sender, EventArgs e)
        {
            gridView1.InitGridView(true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridView1.ShowField(new string[] { "Chon", "DangKyDaoTao.TrinhDoChuyenMon.TenTrinhDoChuyenMon", "DangKyDaoTao.ChuyenMonDaoTao.TenChuyenMonDaoTao", "DangKyDaoTao.TruongDaoTao.TenTruongDaoTao" },
                new string[] { "Chọn", "Trình độ chuyên môn", "Chuyên môn đào tạo", "Trường đào tạo" },
                new int[] { 40, 80, 150, 200 });
            gridView1.ReadOnlyColumns(new string[] { "DangKyDaoTao.TrinhDoChuyenMon.TenTrinhDoChuyenMon", "DangKyDaoTao.ChuyenMonDaoTao.TenChuyenMonDaoTao", "DangKyDaoTao.TruongDaoTao.TenTruongDaoTao" });
        }

        public List<DangKyDaoTao> GetDangKyDaoTao()
        {
                var result = from c in _DataSource
                             where c.Chon == true
                             select c.DangKyDaoTao;
                return result.ToList();
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

        private void XuLy1(bool state)
        {
            foreach (DuyetDangKyDaoTaoItem nhanVienItem in _DataSource)
            {
                if (nhanVienItem.Chon != state)
                    nhanVienItem.Chon = state;
            }
            gridControl1.RefreshDataSource();
        }

        private void XuLy2(bool state)
        {
            int[] selectedRows = gridView1.GetSelectedRows();
            DuyetDangKyDaoTaoItem nhanVien;
            foreach (int item in selectedRows)
            {
                nhanVien = gridView1.GetRow(item) as DuyetDangKyDaoTaoItem;
                if (nhanVien != null && nhanVien.Chon != state)
                    nhanVien.Chon = state;
            }
            gridControl1.RefreshDataSource();
        }
    }

    public class DuyetDangKyDaoTaoItem
    {
        [System.ComponentModel.DisplayName("Chọn")]
        public bool Chon { get; set; }

        [System.ComponentModel.DisplayName("Đăng ký đào tạo")]
        public DangKyDaoTao DangKyDaoTao { get; set; }
    }
}
