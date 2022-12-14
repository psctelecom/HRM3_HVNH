using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThamDuHoiThiTayNgheTreController : ViewController
    {
        public MailMerge_QuyetDinhThamDuHoiThiTayNgheTreController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThamDuHoiThiTayNgheTre>();
            QuyetDinhThamDuHoiThiTayNgheTre qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThamDuHoiThiTayNgheTre)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<MailMerge_QuyetDinhThamDuHoiThiTayNgheTre>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
