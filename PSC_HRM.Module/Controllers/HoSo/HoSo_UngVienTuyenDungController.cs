using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using PSC_HRM.Module.TuyenDung;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class HoSo_UngVienTuyenDungController : ViewController
    {
        public HoSo_UngVienTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(UngVien);
            HamDungChung.DebugTrace("HoSo_UngVienTuyenDungController");
        }

        private void HoSo_ThongTinNhanVienAction_ViewControlsCreated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;
            if (listView != null)
            {
                listView.CurrentObjectChanged += listView_CurrentObjectChanged;
            }
        }

        void listView_CurrentObjectChanged(object sender, EventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView != null)
            {
                UngVien ungVien = listView.CurrentObject as UngVien;
                if (ungVien != null)
                {
                    HoSo.HoSo.CurrentHoSo = ungVien;
                }
            }
        }
    }
}
