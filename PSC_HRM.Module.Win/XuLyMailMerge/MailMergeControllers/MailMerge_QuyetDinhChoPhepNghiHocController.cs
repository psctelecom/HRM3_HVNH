using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhChoPhepNghiHocController : ViewController
    {
        public MailMerge_QuyetDinhChoPhepNghiHocController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhChoPhepNghiHoc>();
            QuyetDinhChoPhepNghiHoc qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhChoPhepNghiHoc)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhChoPhepNghiHoc>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
