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
    public partial class HopDong_HetHanHopDongController : ViewController
    {
        public HopDong_HetHanHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_HetHanHopDongController");
        }

        private void BoNhiem_TheoDoiBoNhiemController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            //ObjectSpace obs = Application.CreateObjectSpace();
            if (view != null)
            {
                DanhSachHetHanHopDong hetHanHD = view.CurrentObject as DanhSachHetHanHopDong;
                if (hetHanHD != null)
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
                                            hetHanHD.LoadData();
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
