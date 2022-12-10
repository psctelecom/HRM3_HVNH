using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Updating;

namespace PSC_HRM.Module.Win
{
    [ToolboxItemFilter("Xaf.Platform.Win")]
    public sealed partial class PSC_HRMWindowsFormsModule : ModuleBase
    {
        public PSC_HRMWindowsFormsModule()
        {
            InitializeComponent();
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            return ModuleUpdater.EmptyModuleUpdaters;
        }

        //public override void AddGeneratorUpdaters(ModelNodesGeneratorUpdaters updaters)
        //{
        //    base.AddGeneratorUpdaters(updaters);
        //    updaters.Add(new CreateReportListViewController());
        //}
    }
}
