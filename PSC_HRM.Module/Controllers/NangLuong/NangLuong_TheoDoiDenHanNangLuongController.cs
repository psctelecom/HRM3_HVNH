using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using PSC_HRM.Module.TapSu;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.NangLuong;

namespace PSC_HRM.Module.Controllers
{
    public partial class NangLuong_TheoDoiDenHanNangLuongController : ViewController
    {
        public NangLuong_TheoDoiDenHanNangLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void NangLuong_TheoDoiDenHanNangLuongController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            //ObjectSpace obs = Application.CreateObjectSpace();
            if (view != null)
            {
                DanhSachDenHanNangLuong danhSachDenHanNangLuong = view.CurrentObject as DanhSachDenHanNangLuong;
                if (danhSachDenHanNangLuong != null)
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
                                            danhSachDenHanNangLuong.LoadData();
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
