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
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class HeThong_AutoCommitMasterContrller : ViewController
    {
        private DetailView _detailView;

        public HeThong_AutoCommitMasterContrller()
        {
            InitializeComponent();
            //
            RegisterActions(components);
        }

        private void HeThong_AutoCommitMasterContrller_Activated(object sender, EventArgs e)
        {
            //Cài đặt detailview ở đây
        }

        private void ObjectSpace_Committed(object sender, EventArgs e)
        {
            if (this.View != null)
            {
                if (this.View.Id == "NhanVien_GioGiang_DetailView" || this.View.Id == "ChiTietGioGiang_DetailView")
                {
                    IObjectSpace objectSpace = this.View.ObjectSpace;
                    IObjectSpace parentObjectSpace = null;
                    try
                    {
                        if (objectSpace is XPNestedObjectSpace)
                        {
                            parentObjectSpace = ((XPNestedObjectSpace)objectSpace).ParentObjectSpace;
                            LinkToListViewController linkToListViewController = Frame.GetController<LinkToListViewController>();
                            if (linkToListViewController != null
                                && linkToListViewController.Link != null
                                && linkToListViewController.Link.ListView != null
                                && linkToListViewController.Link.ListView.CollectionSource is PropertyCollectionSource
                                && !objectSpace.IsNewObject(((PropertyCollectionSource)linkToListViewController.Link.ListView.CollectionSource).MasterObject))
                            {
                                parentObjectSpace.CommitChanges();
                            }

                        }
                    }
                    catch (Exception)
                    {
                        if (parentObjectSpace != null)
                        {
                            parentObjectSpace.Rollback();
                        }
                        throw;
                    }
                }
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            ObjectSpace.Committed += ObjectSpace_Committed;
        }

        protected override void OnDeactivated()
        {
            ObjectSpace.Committed -= ObjectSpace_Committed;
            base.OnDeactivated();
        }
    }
}
