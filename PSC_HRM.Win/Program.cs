using System;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using PSC_HRM.Module;
//using PSC_HRM.Module.Win.Forms;

namespace PSC_HRM.Win
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.  
        /// </summary>
        [STAThread]
        static void Main()
        {
#if EASYTEST
			DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");

            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            Tracing.LogName = @"E:\Logs\CustomLogFile";
            PSC_HRMWindowsFormsApplication winApplication = new PSC_HRMWindowsFormsApplication();
            winApplication.ConnectionString = DataProvider.GetConnectionString(DataProvider.DataBase);
            winApplication.DatabaseUpdateMode = DatabaseUpdateMode.Never;

       

            
#if EASYTEST
			if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
				winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
			}
#endif
            try
             {
                SystemContainer.Install();
                winApplication.Setup();
                winApplication.Start();
            }
            catch (Exception e)
            {
                winApplication.HandleException(e);
            }
        }
    }

}

