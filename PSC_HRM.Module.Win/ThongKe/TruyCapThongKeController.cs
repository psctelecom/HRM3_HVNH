using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.SystemModule;
using PSC_HRM.Module.XuLyQuyTrinh;

namespace PSC_HRM.Module.Win.ThongKe
{
    public partial class TruyCapThongKeController : WindowController
    {
        private ShowNavigationItemController nav;

        public TruyCapThongKeController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            nav = Frame.GetController<ShowNavigationItemController>();
            if (nav != null)
            {
                nav.CustomShowNavigationItem += nav_CustomShowNavigationItem;
            }
        }

        protected override void OnDeactivated()
        {
            if (nav != null)
                nav.CustomShowNavigationItem -= nav_CustomShowNavigationItem;

            base.OnDeactivated();
        }

        void nav_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "ThongKeChucVu_DashboardView")
            {
                ThongKeFactory.Type = LoaiThongKeEnum.ThongKeChucVu;
            }
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "ThongKeDoTuoi_DashboardView")
            {
                ThongKeFactory.Type = LoaiThongKeEnum.ThongKeDoTuoi;
            } 
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "ThongKeGioiTinh_DashboardView")
            {
                ThongKeFactory.Type = LoaiThongKeEnum.ThongKeGioiTinh;
            }
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "ThongKeLoaiNhanSu_DashboardView")
            {
                ThongKeFactory.Type = LoaiThongKeEnum.ThongKeLoaiNhanSu;
            } 
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "ThongKeBienChe_DashboardView")
            {
                ThongKeFactory.Type = LoaiThongKeEnum.ThongKeBienChe;
            }
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "ThongKeHocHam_DashboardView")
            {
                ThongKeFactory.Type = LoaiThongKeEnum.ThongKeHocHam;
            }
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "ThongKeLoaiHopDong_DashboardView")
            {
                ThongKeFactory.Type = LoaiThongKeEnum.ThongKeLoaiHopDong;
            }
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "ThongKeNgachLuong_DashboardView")
            {
                ThongKeFactory.Type = LoaiThongKeEnum.ThongKeNgachLuong;
            }
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "ThongKeTrinhDoChuyenMon_DashboardView")
            {
                ThongKeFactory.Type = LoaiThongKeEnum.ThongKeTrinhDoChuyenMon;
            }
           
        }
    }
}
