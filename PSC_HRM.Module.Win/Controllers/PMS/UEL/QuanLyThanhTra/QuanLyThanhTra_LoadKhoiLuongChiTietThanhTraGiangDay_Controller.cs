using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;
using PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.UEL;

namespace PSC_HRM.Module.Win.Controllers.PMS.UEL
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class QuanLyThanhTra_LoadKhoiLuongChiTietThanhTraGiangDay_Controller : ViewController
    {
        private Quanlythanhtra _KhoiLuong;
        private IObjectSpace _obs;
        private Session _session;
        public QuanLyThanhTra_LoadKhoiLuongChiTietThanhTraGiangDay_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            if(TruongConfig.MaTruong == "UEL")
            {
                TargetViewId = "QuanLyThanhTra_Non_UEL_DetailView";
            }
            if(TruongConfig.MaTruong != "UEL")
            {
                TargetViewId="NULL";
            }
        }

        void QuanLyThanhTra_LoadKhoiLuongChiTietThanhTraGiangDay_Controller_ViewControlsCreated(object sender, System.EventArgs e)
        {

            DetailView view = View as DetailView;

            QuanLyThanhTra_Non_UEL qly = View.CurrentObject as QuanLyThanhTra_Non_UEL;
            //
            if (view != null)
            {
                ViewItem item = ((DetailView)View).FindItem("btnLoadData_ThanhTra_UEL") as ViewItem;//ControlViewItem;
                //
                if (item != null)
                {
                    SimpleButton btnSearch = item.Control as SimpleButton;
                    if (btnSearch != null)
                    {
                        btnSearch.Text = "Lấy dữ liệu";
                        btnSearch.Width = 80;
                        btnSearch.Click += (obj, ea) =>
                        {
                            //Gọi hàm createCommand() bên class TestDemo
                            int KQ =  qly.LoadChiTietThanhTra();
                            if(KQ ==0)
                            {
                                XtraMessageBox.Show("Bạn phải nhận từ ngày đến ngày . Không được để trống","Lỗi");
                            }
                            view.Refresh();
                        };
                    }
                }

            }
        }
    }
}
