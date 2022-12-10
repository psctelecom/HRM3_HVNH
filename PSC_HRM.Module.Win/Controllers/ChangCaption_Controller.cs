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
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Win.Layout;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraGrid.Views.Grid;

namespace PSC_HRM.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ChangCaption_Controller : ViewController 
    {
        public ChangCaption_Controller()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        void ChangCaption_Controller_Activated(object sender, System.EventArgs e)
        {
            View.ControlsCreated += View_ControlsCreated;
        }

        void View_ControlsCreated(object sender, EventArgs e)
        {
            string idObjectSpace = View.ObjectSpace.ToString();
            //
            if (View == null || idObjectSpace.Equals("DevExpress.ExpressApp.NonPersistentObjectSpace"))
                return;
            //
            XPObjectSpace objectSpace = (XPObjectSpace)View.ObjectSpace;
            if (objectSpace == null)
                return;
            string MaTruong = TruongConfig.MaTruong;

            if (MaTruong == "HVNH")
            {
                ChangCaption_HVNH();
            }
            else
                if (MaTruong == "DNU")
                {
                    ChangCaption_DNU();
                }
                else
                    if(MaTruong =="UEL")
                    {
                        ChangCaption_UEL();
                    }
                    else 
                        if(MaTruong=="VHU")
                        {
                            ChangCaption_VHU();
                        }
        }
        void ChangCaption_HVNH()
        {
            if (View.Id.Contains("KhoiLuongGiangDay"))
            {
                //
                if (!View.Id.Contains("KhoiLuongGiangDay_ChiTietKhoiLuongGiangDay"))
                {
                    ChangePropertyCaption("GioQuyDoiChamBaiTNTH", "Giờ quy đổi chấm kiểm tra");
                    ChangePropertyCaption("HeSo_NgonNgu", "Hệ số Tiếng Anh");
                }
            }
            else
                if (View.Id == "BangChotThuLao_ListThongTinBangChot_ListView")
                {
                    ChangePropertyCaption("GioNghiaVu", "Định mức giảng dạy");
                }
            //
            if (View.Id.Contains("DanhSachTre"))
            {
                //Thay đổi caption của group
                ChangeGroupCaption("DanhSachTre", "Danh sách trẻ");
                //Caption view
                View.Caption = "Danh sách trẻ";
            }
        }
        void ChangCaption_DNU()
        {
            if (View.Id == "KhoiLuongGiangDay_ListChiTietKhoiLuongGiangDay_ListView")
            {
                ChangePropertyCaption("TongGioLyThuyetThaoLuan", "Tổng giờ lý thuyết");
                ChangePropertyCaption("TongGioTNTH_DA_BTL", "Tổng giờ thực hành");
            }     
            if(View.Id=="QuanLyGioChuan_DetailView")
            {

            }
        }
        void ChangCaption_UEL()
        {
            if (View.Id == "BangChotThuLao_DetailView")
            {
                ChangePropertyCaption("KyTinhPMS", "Đợt chi trả thù lao");
            }
            else
                if (View.Id == "ChonThongTinBangChot_DetailView")
                {
                    ChangePropertyCaption("KyTinhPMS", "Đợt chi trả thù lao");
                }
        }
        void ChangCaption_VHU()
        {
            if (View.Id == "CauHinhChung_DetailView")
            {
                ChangePropertyCaption("SoGioChuan_Khac", "Hoạt động quản lý");
            }
            else
                if(View.Id=="BangChotThuLao_DetailView")
                {
                    ChangePropertyCaption("SoTietKhac", "Hoạt động quản lý");
                }
                else
                    if (View.Id == "QuanLyGioChuan_ListDinhMucChucVuNhanVien_ListView")
                    {
                        ChangePropertyCaption("SoGioDinhMuc_Khac", "Định mức giờ chuẩn TGQL");
                        ChangePropertyCaption("SoGioDinhMuc", "Định mức giờ giảng dạy");
                        ChangePropertyCaption("SoGioDinhMuc_NCHK", "Định mức giờ chuẩn NCKH");
                    }
        }
        void ChangePropertyCaption(string propertyName, string propertyCaption)
        {
            /* Lưu ý chỉ có tác dụng đổi với Detailview */

            //
            IModelMember property = ((IModelObjectView)View.Model).ModelClass.FindMember(propertyName);  
            //var property = View.Model.AsObjectView.ModelClass.FindMember(propertyName);
            if (property != null)
            {
                property.Caption = propertyCaption;
            }
        }

        void ChangeGroupCaption(string groupName, string groupCaption)
        {
            //
            DetailView detailView = View as DetailView;
            if (detailView == null)
                return;

            //foreach (var group in ((WinLayoutManager)((DetailView)View).LayoutManager).item)
            //{
            //    //
            //    if (group.Value.Model.Id.Equals(groupName))
            //    {
            //        ((IModelLayoutGroup)((LayoutGroupTemplateContainer)group.Value).Model).Caption = groupCaption;
            //    }
            //}
        }

        void VisibleGroupDetailView(string groupName)
        {
            //
            DetailView detailView = View as DetailView;
            if (detailView == null)
                return;
            //
            //foreach (var group in ((WinLayoutManager)((DetailView)View).LayoutManager).Items)
            //{
            //    //
            //    if (group.Value.Model.Id.Equals("Main"))
            //    {
            //        foreach (var item in group.Value.LayoutManager.Items)
            //        {
            //            if (item.Value.Model.Id.Equals(groupName))
            //            {
            //                item.Value.Visible = false;
            //            }
            //        }
            //    }
            //}
        }

        //void ObjectSpace_Committed(object sender, EventArgs e)
        //{
        //    ASPxPopupControl popup = new ASPxPopupControl();
        //    popup.ID = popup.ClientInstanceName = popup.HeaderText = "Nhắc nhở";
        //    popup.Modal = true;
        //    popup.AllowDragging = true;
        //    ((Control)View.Control).Controls.Add(popup);
        //    WebWindow.CurrentRequestWindow.RegisterStartupScript("showPopup", "popup.Show();");
        //}
    }
}
