using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.NonPersistent;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class KhoiLuongGiangDay_XemKhoiLuongTong_LayDuLieu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        DanhSachDuLieu_KhoiLuongGiangDay_Non _source;
        KhoiLuongGiangDay _TKB;
        Session ses;
        public KhoiLuongGiangDay_XemKhoiLuongTong_LayDuLieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "DanhSachDuLieu_KhoiLuongGiangDay_Non_DetailView";
        }

        void KhoiLuongGiangDay_XemKhoiLuongTong_LayDuLieu_Controller_Activated(object sender, System.EventArgs e)
        {
            //if (TruongConfig.MaTruong == "VHU")
            //    popDongBoTK.Active["TruyCap"] = true;
            //else
            //    popDongBoTK.Active["TruyCap"] = false;
        }

        private void KhoiLuongGiangDay_XemKhoiLuongTong_LayDuLieu_Controller_ViewControlsCreated(object sender, System.EventArgs e)
        {
            IObjectSpace os1 = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)os1).Session;//Session được sử dụng để load và lưu lại  

            DetailView view = View as DetailView;

            DanhSachDuLieu_KhoiLuongGiangDay_Non qly = View.CurrentObject as DanhSachDuLieu_KhoiLuongGiangDay_Non;
            //
            if (view != null)
            {
                ViewItem item = ((DetailView)View).FindItem("btnLoadData") as ViewItem;//ControlViewItem;
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
                            qly.LoadData();
                        };
                    }
                }

            }
        }
     
    }
}