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
using DevExpress.XtraLayout;
using DevExpress.ExpressApp.Editors;
using DevExpress.XtraLayout.Utils;
using DevExpress.ExpressApp.Layout;
using System.Web.UI;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class CustomDetailViewController : ViewController
    {
        private DetailView _detailView;

        public CustomDetailViewController()
        {
            InitializeComponent();
            //
            RegisterActions(components);
        }

        private void CustomDetailViewController_Activated(object sender, EventArgs e)
        {
            //Cài đặt detailview ở đây
            View.ControlsCreated += View_ControlsCreated;
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {
            //Lấy detailview
            _detailView = View as DetailView;

            if (_detailView != null)
                //Custom định dạng trên đối tượng
                CustomFormatOfObject(_detailView);
        }

        private void CustomFormatOfObject(DetailView detailView)
        {
            if (TruongConfig.MaTruong.Equals("UTE"))
            {
                #region Change Caption
                if (detailView.Id == "ThongTinNhanVien_DetailView")
                {
                    //Set value để xác định loại tài khoản hiện tại
                    HamDungChung.AcountType = HamDungChung.CheckAcountType();
                }
                #endregion
            }
            if (TruongConfig.MaTruong.Equals("NEU"))
            {
                #region Change Caption
                if (detailView.Id == "ThongTinTinhLuong_DetailView")
                {
                    //Định dạng caption
                    DetailUtil.FormatCaption(detailView, "HSPCTrachNhiem", "Hệ số vị trí việc làm");
                }
                if (detailView.Id == "NhanVienThongTinLuong_DetailView")
                {
                    //Định dạng caption
                    DetailUtil.FormatCaption(detailView, "HSPCTrachNhiem.HeSoH3", "Hệ số vị trí việc làm");
                }
                #endregion
            }
            if (TruongConfig.MaTruong.Equals("VLU"))
            {

                if (detailView.Id == "ThongTinNhanVien_DetailView")
                {

                    //Định dạng caption
                    DetailUtil.FormatCaption(detailView, "NhanVienThongTinLuong.P1MucLuongDongBHXH", "Mức lương đóng BHXH (P1)");//OK
                    DetailUtil.FormatCaption(detailView, "NhanVienThongTinLuong.NgachLuong", "Chức danh lương");//OK
                }
                detailView.Refresh();
            }
        }
    }
}
