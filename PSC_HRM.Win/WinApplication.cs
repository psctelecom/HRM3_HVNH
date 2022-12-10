using System;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Win;
using System.Collections.Generic;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module;
//using DevExpress.ExpressApp.Security;

namespace PSC_HRM.Win
{
    // You can override various virtual methods and handle corresponding events to manage various aspects of your XAF application UI and behavior.
    public partial class PSC_HRMWindowsFormsApplication : WinApplication
    { // http://documentation.devexpress.com/#Xaf/DevExpressExpressAppWinWinApplicationMembersTopicAll
        public PSC_HRMWindowsFormsApplication()
        {
            InitializeComponent();
            DelayedViewItemsInitialization = true;
        }

        // Override to execute custom code after a logon has been performed, the SecuritySystem object is initialized, logon parameters have been saved and user model differences are loaded.
        protected override void OnLoggedOn(LogonEventArgs args)
        { // http://documentation.devexpress.com/#Xaf/DevExpressExpressAppXafApplication_LoggedOntopic
            base.OnLoggedOn(args);
        }

        // Override to execute custom code after a user has logged off.
        protected override void OnLoggedOff()
        { // http://documentation.devexpress.com/#Xaf/DevExpressExpressAppXafApplication_LoggedOfftopic
            base.OnLoggedOff();
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection);
        }
        private void PSC_HRMWindowsFormsApplication_CustomizeLanguagesList(object sender, CustomizeLanguagesListEventArgs e)
        {
            string userLanguageName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if (userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1)
            {
                e.Languages.Add(userLanguageName);
            }
        }
        private void PSC_HRMWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e)
        {
#if EASYTEST
			e.Updater.Update();
			e.Handled = true;
#else
            if (System.Diagnostics.Debugger.IsAttached)
            {
                e.Updater.Update();
                e.Handled = true;
            }
            else
            {
                throw new InvalidOperationException(
                    "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
                    "This error occurred  because the automatic database update was disabled when the application was started without debugging.\r\n" +
                    "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " +
                    "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
                    "or manually create a database using the 'DBUpdater' tool.\r\n" +
                    "Anyway, refer to the 'Update Application and Database Versions' help topic at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm " +
                    "for more detailed information. If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/");
            }
#endif
        }

        private void PSC_HRMWindowsFormsApplication_CustomizeLanguage(object sender, CustomizeLanguageEventArgs e)
        {
            e.LanguageName = "vi";
        }

        private void PSC_HRMWindowsFormsApplication_CustomizeFormattingCulture(object sender, CustomizeFormattingCultureEventArgs e)
        {
            //format ngày
            e.FormattingCulture.DateTimeFormat.DateSeparator = "/";
            e.FormattingCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            e.FormattingCulture.DateTimeFormat.LongDatePattern = "dddd, 'Ngày' dd 'tháng' MM 'năm' yyyy";

            e.FormattingCulture.DateTimeFormat.AbbreviatedDayNames = new string[] { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };
            e.FormattingCulture.DateTimeFormat.ShortestDayNames = new string[] { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };
            e.FormattingCulture.DateTimeFormat.DayNames = new string[] { "Chủ Nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            e.FormattingCulture.DateTimeFormat.AbbreviatedMonthGenitiveNames = new string[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12", "" };
            e.FormattingCulture.DateTimeFormat.AbbreviatedMonthNames = e.FormattingCulture.DateTimeFormat.AbbreviatedMonthGenitiveNames;
            e.FormattingCulture.DateTimeFormat.MonthGenitiveNames = e.FormattingCulture.DateTimeFormat.AbbreviatedMonthNames;
            e.FormattingCulture.DateTimeFormat.MonthGenitiveNames = e.FormattingCulture.DateTimeFormat.AbbreviatedMonthNames;
            e.FormattingCulture.DateTimeFormat.MonthNames = e.FormattingCulture.DateTimeFormat.AbbreviatedMonthNames;
            e.FormattingCulture.DateTimeFormat.YearMonthPattern = "MMMM / 'năm' yyyy";

            if (TruongConfig.MaTruong.Equals("DLU"))
            {
                //format số
                e.FormattingCulture.NumberFormat.NumberDecimalDigits = 0;
                //e.FormattingCulture.NumberFormat.NumberDecimalSeparator = ",";
                e.FormattingCulture.NumberFormat.NumberDecimalSeparator = ".";
                e.FormattingCulture.NumberFormat.NumberGroupSeparator = ",";
                e.FormattingCulture.NumberFormat.CurrencyDecimalDigits = 0;
                e.FormattingCulture.NumberFormat.CurrencyDecimalSeparator = ",";
                e.FormattingCulture.NumberFormat.CurrencyGroupSeparator = ",";
                e.FormattingCulture.NumberFormat.CurrencySymbol = ",";
            }
            else
            {
                //format số
                e.FormattingCulture.NumberFormat.NumberDecimalDigits = 0;
                e.FormattingCulture.NumberFormat.NumberDecimalSeparator = ",";
                // e.FormattingCulture.NumberFormat.NumberDecimalSeparator = ".";
                e.FormattingCulture.NumberFormat.NumberGroupSeparator = ".";
                e.FormattingCulture.NumberFormat.CurrencyDecimalDigits = 0;
                e.FormattingCulture.NumberFormat.CurrencyDecimalSeparator = ",";
                e.FormattingCulture.NumberFormat.CurrencyGroupSeparator = ".";
                e.FormattingCulture.NumberFormat.CurrencySymbol = ".";
            }
        }

        private void PSC_HRMWindowsFormsApplication_ViewCreated(object sender, ViewCreatedEventArgs e)
        {
            if (e.View is DetailView)
            {
                //Nhớ tạo một StaticImage trên model thì ở đây mới tìm thấy
                StaticImage item = ((DetailView)e.View).FindItem("LogoBaner") as StaticImage;

                if (item != null)
                {
                    string maTruong = TruongConfig.MaTruong;

                    if (maTruong.Equals("NEU"))
                    {
                        item.ImageName = "LoginBaner_NEU";
                    }                    
                    else if (maTruong.Equals("UFM"))
                    {
                        item.ImageName = "LoginBaner_UFM";
                    }
                    else if (maTruong.Equals("UEL"))
                    {
                        item.ImageName = "LoginBaner_UEL";
                    }
                    else if (maTruong.Equals("QNU"))
                    {
                        item.ImageName = "LoginBaner_QNU";
                    }
                    else if (maTruong.Equals("CYD"))
                    {
                        item.ImageName = "LoginBaner_CYD";
                    }
                    else if (maTruong.Equals("VHU"))
                    {
                        item.ImageName = "LoginBaner_VHU";
                    }
                    else
                    {
                        item.ImageName = "LogoPSC";
                    }
                }
            }
        }

        private void PSC_HRMWindowsFormsApplication_CustomizeTemplate(object sender, CustomizeTemplateEventArgs e)
        {
            //đăng ký devexpress bonus skins
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Money Twins";
        }
    }
}
