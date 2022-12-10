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
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;
using System.Net;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using System.Data.SqlClient;
using System.Data;

namespace ERP.Module.Controllers.HeThong
{
    public partial class HeThong_SaveOnLogonResetPasswordParameters : ViewController
    {
        IObjectSpace _obs;
        string _NewPassword;

        public HeThong_SaveOnLogonResetPasswordParameters()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "NguoiSuDung_DetailView";
            //TargetViewId = "ChangePasswordOnLogonParameters_DetailView";
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            object kq = null;
            if (TruongConfig.MaTruong == "QNU")
            {
                SqlParameter[] pQuyDoi = new SqlParameter[1];
                pQuyDoi[0] = new SqlParameter("@User", HamDungChung.CurrentUser().Oid);
                kq = DataProvider.GetValueFromDatabase("spd_Ktra_DoiMatKhau", CommandType.StoredProcedure, pQuyDoi);
                int ma = Convert.ToInt32(kq);


                if (HamDungChung.CurrentUser().UserName != "psc" && ma == 0)
                {
                    ResetPasswordController rpc = Frame.GetController<ResetPasswordController>();
                    rpc.Active["TruyCap"] = false;
                }
            }
        }

    }
}
