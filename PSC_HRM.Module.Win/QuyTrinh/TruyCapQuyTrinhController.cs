using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.SystemModule;
using PSC_HRM.Module.XuLyQuyTrinh;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    public partial class TruyCapQuyTrinhController : WindowController
    {
        private ShowNavigationItemController nav;

        public TruyCapQuyTrinhController()
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
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhNghiHuu_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhNghiHuu;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhKhenThuong_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhKhenThuong;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhTapSu_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhTapSu;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhThuViec_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhThuViec;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhDaoTao_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhDaoTao;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhBoiDuong_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhBoiDuong;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhDiNuocNgoai_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhDiNuocNgoai;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhThoiViec_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhThoiViec;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhNangLuong_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhNangLuong;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhNangNgach_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhNangNgach;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhThoiViec_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhThoiViec;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhChuyenNgach_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhChuyenNgach;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhBoNhiem_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhBoNhiem;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhXepLoaiLaoDong_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhXepLoaiLaoDong;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhXuLyViPham_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhXuLyViPham;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhNangThamNien_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhNangThamNien;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhTuyenDung_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhTuyenDung;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhNghiKhongHuongLuong_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhNghiKhongHuongLuong;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhChuyenCongTac_DashboardView")
            {
                QuyTrinhFactory.Type = ThucHienQuyTrinhTypeEnum.QuyTrinhChuyenCongTac;
            }
        }
    }
}
