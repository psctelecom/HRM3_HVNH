using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhGiaHanBoiDuongController : ViewController
    {
        public MailMerge_QuyetDinhGiaHanBoiDuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhGiaHanBoiDuong>();
            QuyetDinhGiaHanBoiDuong qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhGiaHanBoiDuong)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhGiaHanBoiDuong>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
