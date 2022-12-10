using DevExpress.ExpressApp;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.Win.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public class MailMergeHelper
    {
        public static void ShowEditor<T>(List<T> obj, IObjectSpace obs, params MailMergeTemplate[] args) where T : IMailMergeBase
        {
            frmEditor<T> editor = new frmEditor<T>();
            editor.Show();
            if (args != null)
                editor.LoadData(obj, obs, args);
        }
    }
}
