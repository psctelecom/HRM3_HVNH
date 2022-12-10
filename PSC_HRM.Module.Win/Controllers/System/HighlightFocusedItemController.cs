using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.XtraLayout;
using DevExpress.ExpressApp.Win.Layout;

namespace PSC_HRM.Module.Win
{
    public partial class HighlightFocusedItemController : ViewController
    {
        public HighlightFocusedItemController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.DetailView;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            DetailView lDet = View as DetailView;
            if (lDet != null)
            {
                ((LayoutControl)(((WinLayoutManager)(lDet.LayoutManager)).Container)).OptionsView.HighlightFocusedItem = true;
                ((LayoutControl)(((WinLayoutManager)(lDet.LayoutManager)).Container)).Appearance.ControlFocused.BackColor = System.Drawing.Color.FromArgb(255, 201, 100);
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}
