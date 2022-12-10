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
using PSC_HRM.Module.ChamCong;
namespace PSC_HRM.Module.Win.Controllers
{
    public partial class TimKiemDanhSachCanBo_TheoHinhThucNghiController : ViewController
    {
        public TimKiemDanhSachCanBo_TheoHinhThucNghiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TimKiemDanhSachCanBo_TheoHinhThucNghiController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            if (view != null)
            {
                //Lấy view hiện tại
                TimKiemCanBoTheoHinhThucNghi danhSach = View.CurrentObject as TimKiemCanBoTheoHinhThucNghi;

                if (danhSach != null)
                {
                    foreach (ControlDetailItem item in view.GetItems<ControlDetailItem>())
                    {
                        if (item.Id == "CustomControl1")
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
