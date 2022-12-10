using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp;
using PSC_HRM.Module.BaoHiem;

namespace PSC_HRM.Module.Win.QuyTrinh.NghiHuu
{
    public partial class GuiHoSoBaoHiemController : ChonNhanVienController
    {
        public GuiHoSoBaoHiemController(IObjectSpace obs, List<Guid> nhanVienList)
            : base(obs, nhanVienList)
        {
            InitializeComponent();

            listQuanLyBienDong.Session = unitOfWork;
        }

        private void GuiHoSoBaoHiemController_Load(object sender, EventArgs e)
        {
            gridQuanLyBienDong.InitGridLookUp(true, true, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            gridQuanLyBienDong.InitPopupFormSize(gridQuanLyBienDong.Width, 300);
            gridViewQuanLyBienDong.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewQuanLyBienDong.ShowField(new string[] { "Caption" }, new string[] { "Kỳ biến động" });
        }

        public QuanLyBienDong GetQuanLyBienDong()
        {
            QuanLyBienDong data = gridViewQuanLyBienDong.GetFocusedRow() as QuanLyBienDong;
            return data;
        }

        private void gridQuanLyBienDong_Resize(object sender, EventArgs e)
        {
            gridQuanLyBienDong.InitPopupFormSize(gridQuanLyBienDong.Width, 300);
        }
    }
}
