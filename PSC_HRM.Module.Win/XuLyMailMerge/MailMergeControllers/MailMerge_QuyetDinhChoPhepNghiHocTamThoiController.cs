using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhChoPhepNghiHocTamThoiController : ViewController
    {
        public MailMerge_QuyetDinhChoPhepNghiHocTamThoiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhChoPhepNghiHocTamThoi>();
            QuyetDinhChoPhepNghiHocTamThoi qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhChoPhepNghiHocTamThoi)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhChoPhepNghiHocTamThoi>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
