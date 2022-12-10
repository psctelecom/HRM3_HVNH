using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class HopDong_HopDongMoiController : ViewController
    {
        public HopDong_HopDongMoiController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_HopDongMoiController");
        }

        private void HopDong_HopDongMoiController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            //ObjectSpace obs = Application.CreateObjectSpace();
            if (view != null)
            {
                DanhSachHopDongMoi hopDongMoi = view.CurrentObject as DanhSachHopDongMoi;
                if (hopDongMoi != null)
                {
                    foreach (ControlDetailItem item in view.GetItems<ControlDetailItem>())
                    {
                        if (item.Id == "CustomControl")
                        {
                            SimpleButton button = item.Control as SimpleButton;
                            if (button != null)
                            {
                                button.Text = "Tìm kiếm";
                                button.Click += (se, ea) =>
                                    {

                                        using (DialogUtil.AutoWait())
                                        {
                                            hopDongMoi.LoadData();
                                        }
                                    };
                            }
                        }
                    }
                }
            }
        }
    }
}
