using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using PSC_HRM.Module.TapSu;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.NghiHuu;
using PSC_HRM.Module.BoNhiem;

namespace PSC_HRM.Module.Controllers
{
    public partial class BoNhiem_TheoDoiHetHanBoNhiemController : ViewController
    {
        public BoNhiem_TheoDoiHetHanBoNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void BoNhiem_TheoDoiHetHanBoNhiemController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            //ObjectSpace obs = Application.CreateObjectSpace();
            if (view != null)
            {
                DanhSachHetHanBoNhiem danhSachHetHanBoNhiem = view.CurrentObject as DanhSachHetHanBoNhiem;
                if (danhSachHetHanBoNhiem != null)
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
                                            danhSachHetHanBoNhiem.LoadData();
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
