using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using PSC_HRM.Module.TapSu;

namespace PSC_HRM.Module.Controllers
{
    public partial class TapSu_TheoHetHanTamHoanTapSuController : ViewController
    {
        public TapSu_TheoHetHanTamHoanTapSuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TapSu_TheoHetHanTamHoanTapSuController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            //ObjectSpace obs = Application.CreateObjectSpace();
            if (view != null)
            {
                DanhSachHetHanTamHoanTapSu theoDoiTapSu = view.CurrentObject as DanhSachHetHanTamHoanTapSu;
                if (theoDoiTapSu != null)
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
                                        using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
                                        {
                                            theoDoiTapSu.XuLy();
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
