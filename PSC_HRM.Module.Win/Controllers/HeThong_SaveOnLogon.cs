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
    public partial class HeThong_SaveOnLogon : ViewController
    {
        IObjectSpace _obs;
        string _NewPassword;

        public HeThong_SaveOnLogon()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ChangePasswordOnLogonParameters_DetailView";
        }

        protected override void OnViewControlsCreated()
        {
            _obs = Application.CreateObjectSpace();
            if (View.Id == "ChangePasswordOnLogonParameters_DetailView") //Stick chọn thay đổi mật khẩu khi logon
            {
                SecurityModule SecurityModule = this.Application.Modules.FindModule<SecurityModule>();
                SecurityModule.CustomChangePasswordOnLogon += SecurityModule_CustomChangePasswordOnLogon;
                SecurityModule.ChangePasswordOnLogonAction.ExecuteCompleted += ChangePasswordAction_ExecuteCompleted;
            }
        }

        void SecurityModule_CustomChangePasswordOnLogon(object sender, CustomChangePasswordOnLogonEventArgs e)
        {
            _NewPassword = e.LogonPasswordParameters.NewPassword;
        }

        void ChangePasswordAction_ExecuteCompleted(object sender, ActionBaseEventArgs e)
        {
            _obs.Refresh(); //Bắt buộc phải có thêm cái refesh nếu k sẽ lỗi
            NguoiSuDung user = _obs.GetObjectByKey<NguoiSuDung>(HamDungChung.CurrentUser().Oid);
            user.MatKhau = _NewPassword;
            _obs.CommitChanges();
            //
            if (HamDungChung.CauHinhChung.DongBoTaiKhoan)
            {
                SqlParameter[] parameter = new SqlParameter[2];
                parameter[0] = new SqlParameter("@UserName", user.UserName);
                parameter[1] = new SqlParameter("@Password", _NewPassword);
                DataProvider.ExecuteNonQuery("spd_WebChamCong_CapNhatMatKhau_URM", CommandType.StoredProcedure, parameter);
            }
        }

    }
}
