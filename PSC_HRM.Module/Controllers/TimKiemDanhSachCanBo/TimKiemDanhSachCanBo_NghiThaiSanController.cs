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
using System.Data.SqlClient;
using PSC_HRM.Module.ThoiViec;
namespace PSC_HRM.Module.Win.Controllers
{
    public partial class TimKiemDanhSachCanBo_NghiThaiSanController : ViewController
    {
        public TimKiemDanhSachCanBo_NghiThaiSanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TimKiemDanhSachCanBo_NghiThaiSanController_ViewControlsCreated_1(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            if (view != null)
            {
                //Lấy view hiện tại
                TimKiemCanBoNghiThaiSan danhSach = View.CurrentObject as TimKiemCanBoNghiThaiSan;

                if (danhSach != null)
                {
                    foreach (ControlDetailItem item in view.GetItems<ControlDetailItem>())
                    {
                        if (item.Id == "CustomControl4")
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
