using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThanhLapHoiCanBoTrucLeController : ViewController
    {
        public MailMerge_QuyetDinhThanhLapHoiCanBoTrucLeController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhTrucLe>();
            QuyetDinhTrucLe quyetDinh;
            foreach (object item in View.SelectedObjects)
            {
                quyetDinh = (QuyetDinhTrucLe)item;
                if (quyetDinh != null)
                    list.Add(quyetDinh);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhTrucLe>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
