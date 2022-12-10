using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraTreeList;
using DevExpress.ExpressApp.TreeListEditors.Win;
using PSC_HRM.Module.Win.Common;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Utils;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class CustomPhanQuyenPropertyOfDetailViewController : ViewController
    {

        public CustomPhanQuyenPropertyOfDetailViewController()
        {
            InitializeComponent();
            //
            RegisterActions(components);
        }

        private void CustomPhanQuyenPropertyOfDetailViewController_Activated(object sender, EventArgs e)
        {
            //Cài đặt detailview ở đây
           // View.ControlsCreated += View_ControlsCreated;
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {

           if (TruongConfig.MaTruong.Equals("UTE"))
            {
                if (View.Id == "ThongTinNhanVien_DetailView")
                {
                    //Phân quyền thông tin lương nhà nước
                    if (View.CurrentObject != null)
                    {
                        foreach (ViewItem item in ((DetailView)View).Items)
                        {
                            if ((item is ControlDetailItem) & (item.CurrentObject as ThongTinNhanVien).NhanVienThongTinLuong.PhuCapUuDai != null) 
                            {
                                item.Dispose();
                            }
                        }
                    }
                }
            }
        }


    }
}
