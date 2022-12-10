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
using PSC_HRM.Module.BaoMat;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Controllers.BaoMat
{
    public partial class BaoMat_UpdateAppObject : ViewController
    {
        public BaoMat_UpdateAppObject()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            //
            XPCollection<AppObject> appObjectList = new XPCollection<AppObject>(((XPObjectSpace)obs).Session);
            if (appObjectList != null)
            {
                //Lấy tất cả object trong solution
                var objectAll = from b in View.Model.Application.BOModel
                           where b.TypeInfo.Type.Namespace.Contains("PSC_HRM") == true
                                 && b.TypeInfo.Type.Namespace.Contains("Report") == false
                           orderby b.Caption
                           select new
                           {
                               Caption = b.Caption,
                               Type = b.TypeInfo.Type
                           };
                //
                using (DialogUtil.AutoWait())
                {
                    foreach (var item in objectAll)
                    {
                        bool exsist = (from o in appObjectList
                                       where o.KeyObject == item.Type.Name
                                       select true).FirstOrDefault();
                        //
                        if (!exsist)
                        {
                            AppObject appObjectNew = new AppObject(((XPObjectSpace)obs).Session);
                            appObjectNew.KeyObject = item.Type.Name;
                            appObjectNew.Caption = item.Caption;
                            appObjectNew.Save();
                        }
                    }
                    //
                    obs.CommitChanges();
                }
            }
        }
    }
}
