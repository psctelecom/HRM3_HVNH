using System;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Editors;
using System.Windows.Forms;
using DevExpress.ExpressApp.Model.Core;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class DisablePropertyEditorOfDetailViewController : ViewController
    {
        public DisablePropertyEditorOfDetailViewController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void DisablePropertyEditorOfDetailViewController_Activated(object sender, EventArgs e)
        {
            //Cài đặt lưới ở đây
            View.ControlsCreated += View_ControlsCreated;
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {
            DetailView detailView = View as DetailView;
            if (detailView != null)
            {
                if (detailView.Id.Contains("ThongTinNhanVien_DetailView"))
                {
                    //if (TruongConfig.MaTruong.Equals("IUH"))
                    //{
                    //    DisablePropertyByName(detailView,new string[] {"NhanVienTrinhDo.NgayHuongCheDo"},false);
                    //}
                    //if (TruongConfig.MaTruong.Equals("UTE"))
                    //{
                    //    DisablePropertyByName(detailView, new string[] { "NhanVienTrinhDo.NgayCongTac","MaQuanLy" }, false);
                    //}
                }
            }
        }
        private void DisablePropertyByName(DetailView detailView,string[] targetPropertyName, bool value)
        {
            for (int i = 0; i < targetPropertyName.Length; i++)
            {
                foreach (PropertyEditor item in detailView.GetItems<PropertyEditor>())
                {
                    if (item.PropertyName == targetPropertyName[i])
                    {
                        item.Model.Remove();
                        //detailView.RemoveItem(targetPropertyName[i]);
                    }
                }
            }
            detailView.Refresh();
           
        }
    }
}
