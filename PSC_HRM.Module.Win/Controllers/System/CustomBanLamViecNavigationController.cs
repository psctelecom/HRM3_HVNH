using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class CustomBanLamViecNavigationController : WindowController
    {
        private ShowNavigationItemController nav;

        public CustomBanLamViecNavigationController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnFrameAssigned()
        {
            UnsubscribeFromEvents();
            base.OnFrameAssigned();

            nav = Frame.GetController<ShowNavigationItemController>();
            if (nav != null)
            {
                nav.NavigationItemCreated += nav_NavigationItemCreated;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (nav != null)
            {
                nav.NavigationItemCreated -= nav_NavigationItemCreated;
                nav = null;
            }
        }

        private void nav_NavigationItemCreated(object sender, NavigationItemCreatedEventArgs e)
        {
            if (e.NavigationItem.Id == "NhanSu"
                || e.NavigationItem.Id == "TaiChinh")
            {
                NguoiSuDung nguoiSuDung = HamDungChung.CurrentUser();
                if (nguoiSuDung != null)
                {
                    if (nguoiSuDung.UserName != "admin"
                        && (!nguoiSuDung.DuocSuDungBanLamViec
                        || (e.NavigationItem.Id == "NhanSu"
                        && nguoiSuDung.PhanLoai != AccountTypeEnum.ToChucCanBo)
                        || (e.NavigationItem.Id == "TaiChinh"
                        && nguoiSuDung.PhanLoai != AccountTypeEnum.KeHoachTaiChinh)))
                    {
                        ChoiceActionItem item = GetMenuItems(e.NavigationItem);
                        if (item != null)
                            item.Active["TruyCap"] = false;
                    }
                }
                else
                {
                    ChoiceActionItem item = GetMenuItems(e.NavigationItem);
                    if (item != null)
                        item.Active["TruyCap"] = false;
                }
            }
        }

        private ChoiceActionItem GetMenuItems(ChoiceActionItem root)
        {
            foreach (ChoiceActionItem item in root.Items)
            {
                if (item.Id.Contains("BanLamViec"))
                    return item;
            }
            return null;
        }
    }
}
