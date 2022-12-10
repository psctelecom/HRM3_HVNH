using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    public partial class ChonNhanVienController : BaseController
    {
        private GroupOperator filter1;
        private GroupOperator filter2;

        public ChonNhanVienController()
        {
            InitializeComponent();
        }

        public ChonNhanVienController(IObjectSpace obs, List<Guid> nhanVienList)
        {
            InitializeComponent();

            unitOfWork = new DevExpress.Xpo.UnitOfWork(((XPObjectSpace)obs).Session.DataLayer);
            listThongTinNhanVien.Session = unitOfWork;

            filter1 = new GroupOperator();
            filter2 = new GroupOperator();
            if (nhanVienList != null && nhanVienList.Count > 0)
                filter1.Operands.Add(new InOperator("Oid", nhanVienList));
            else
                filter2.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? and TinhTrang.TenTinhTrang not like ?", "Nghỉ hưu", "Nghỉ việc"));

            filter1.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(((XPObjectSpace)obs).Session)));
            filter2.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(((XPObjectSpace)obs).Session)));

            listThongTinNhanVien.Criteria = filter1.Operands.Count == 1 ? filter2 : filter1;
        }

        private void ChonNhanVienController_Load(object sender, EventArgs e)
        {
            gridThongTinNhanVien.InitGridLookUp(true, true, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            gridThongTinNhanVien.InitPopupFormSize(gridThongTinNhanVien.Width, 300);
            gridViewThongTinNhanVien.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewThongTinNhanVien.ShowField(new string[] { "HoTen", "BoPhan.TenBoPhan" }, new string[] { "Họ tên", "Đơn vị" });
        }

        public ThongTinNhanVien GetThongTinNhanVien()
        {
            ThongTinNhanVien nhanVien = gridViewThongTinNhanVien.GetFocusedRow() as ThongTinNhanVien;
            return nhanVien;
        }

        private void ceTatCaNhanVien_CheckedChanged(object sender, EventArgs e)
        {
            if (ceTatCaNhanVien.Checked)
            {
                listThongTinNhanVien.Criteria = filter2;
            }
            else
            {
                listThongTinNhanVien.Criteria = filter1;
            }

            listThongTinNhanVien.Reload();
            layoutControl1.Invalidate();
        }

        private void gridThongTinNhanVien_Resize(object sender, EventArgs e)
        {
            gridThongTinNhanVien.InitPopupFormSize(gridThongTinNhanVien.Width, 300);
        }
    }
}
