using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Actions;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhNghiThaiSanController : ViewController
    {
        public MailMerge_QuyetDinhNghiThaiSanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhNghiThaiSan>();
            QuyetDinhNghiThaiSan qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhNghiThaiSan)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhNghiThaiSan>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
