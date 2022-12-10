using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using PSC_HRM.Module.TapSu;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.NangThamNien;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThamNien_TheoDoiDenHanNangPCThamNienController : ViewController
    {
        public ThamNien_TheoDoiDenHanNangPCThamNienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ThamNien_TheoDoiDenHanNangPCThamNienController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            //ObjectSpace obs = Application.CreateObjectSpace();
            if (view != null)
            {
                DanhSachDenHanNangPhuCapThamNien danhSachDenHanNangPCThamNien = view.CurrentObject as DanhSachDenHanNangPhuCapThamNien;
                if (danhSachDenHanNangPCThamNien != null)
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
                                            danhSachDenHanNangPCThamNien.LoadData();
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
