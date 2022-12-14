//do list view sử dụng CategorizedEditor bên TreeView không có được filter
using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;


namespace PSC_HRM.Module.Win.Controllers
{
    public partial class FilterTruongHocController : ViewController
    {
        public FilterTruongHocController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(IThongTinTruong);
            Activated += FilterCategoryTruongHoc_Activated;
            Deactivated += FilterCategoryTruongHoc_Deactivated;
        }

        void FilterCategoryTruongHoc_Deactivated(object sender, EventArgs e)
        {
            View.ControlsCreated -= View_ControlsCreated;
        }

        void FilterCategoryTruongHoc_Activated(object sender, EventArgs e)
        {
            View.ControlsCreated += View_ControlsCreated;
        }

        void View_ControlsCreated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;
            if (listView != null)
            {
                ThongTinTruong thongTinTruong = HamDungChung.ThongTinTruong(((XPObjectSpace)View.ObjectSpace).Session);
                if (thongTinTruong != null)
                {
                    bool state = false;
                    InOperator criteria = new InOperator();
                    List<string> bpList = HamDungChung.DanhSachBoPhanDuocPhanQuyen(((XPObjectSpace)View.ObjectSpace).Session);
                    foreach (string item in bpList)
                    {
                        criteria.Operands.Add(new OperandValue(item));
                    }

                    if (listView.ObjectTypeInfo.Type == typeof(ThongTinTruong))
                    {
                        criteria.LeftOperand = new OperandProperty("Oid");
                        state = true;
                    }
                    else if (listView.ObjectTypeInfo.Base.Type == typeof(IThongTinTruong))
                    {
                        criteria.LeftOperand = new OperandProperty("ThongTinTruong.Oid");
                        state = true;
                    }

                    if (state)
                    {
                        listView.CollectionSource.Criteria["PhanQuyenThongTinTruong"] = criteria;
                    }
                }
            }
        }
    }
}
