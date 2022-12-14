using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhCongNhanBoiDuongController : ViewController
    {
        public MailMerge_QuyetDinhCongNhanBoiDuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhCongNhanBoiDuong>();
            QuyetDinhCongNhanBoiDuong qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhCongNhanBoiDuong)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhCongNhanBoiDuong>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
