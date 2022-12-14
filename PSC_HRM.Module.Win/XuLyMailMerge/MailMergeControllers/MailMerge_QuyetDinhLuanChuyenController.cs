using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhLuanChuyenController : ViewController
    {
        public MailMerge_QuyetDinhLuanChuyenController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhLuanChuyen>();
            QuyetDinhLuanChuyen qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhLuanChuyen)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhLuanChuyen>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
