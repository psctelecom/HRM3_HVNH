using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using PSC_HRM.Module.SinhNhat;
using System.Data.SqlClient;
using PSC_HRM.Module.NonPersistentObjects;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class NangPhuCap_TimDanhSachNangPhuCapController : ViewController
    {
        public NangPhuCap_TimDanhSachNangPhuCapController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void NangPhuCap_TimDanhSachNangPhuCapController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            if (view != null)
            {
                //Lấy view hiện tại
                NangPhuCap_DanhSachNangPhuCap danhSach = View.CurrentObject as NangPhuCap_DanhSachNangPhuCap;

                if (danhSach != null)
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
                                       //
                                        danhSach.LoadData();
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
