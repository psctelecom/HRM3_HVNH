using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    public partial class ChonBoPhanController : BaseController
    {
        private InOperator filter1;
        private InOperator filter2;

        public ChonBoPhanController()
        {
            InitializeComponent();
        }

        public ChonBoPhanController(IObjectSpace obs, List<Guid> boPhanList)
        {
            InitializeComponent();

            unitOfWork = new DevExpress.Xpo.UnitOfWork(((XPObjectSpace)obs).Session.DataLayer);
            listDonVi.Session = unitOfWork;

            filter1 = new InOperator("Oid", boPhanList ?? new List<Guid>());
            filter2 = new InOperator("Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(((XPObjectSpace)obs).Session));

            listDonVi.Criteria = (boPhanList == null || boPhanList.Count == 0) ? filter2 : filter1;
        }

        private void ChonNhanVienController_Load(object sender, EventArgs e)
        {
            gridBoPhan.InitGridLookUp(true, true, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            gridBoPhan.InitPopupFormSize(gridBoPhan.Width, 300);
            gridViewBoPhan.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewBoPhan.ShowField(new string[] { "TenBoPhan" }, new string[] { "Đơn vị" });
        }

        public BoPhan GetBoPhan()
        {
            BoPhan boPhan = gridViewBoPhan.GetFocusedRow() as BoPhan;
            return boPhan;
        }

        private void ceTatCaBoPhan_CheckedChanged(object sender, EventArgs e)
        {
            if (ceTatCaNhanVien.Checked)
            {
                listDonVi.Criteria = filter2;
            }
            else
            {
                listDonVi.Criteria = filter1;
            }

            listDonVi.Reload();
            layoutControl1.Invalidate();
        }

        private void gridBoPhan_Resize(object sender, EventArgs e)
        {
            gridBoPhan.InitPopupFormSize(gridBoPhan.Width, 300);
        }
    }
}
