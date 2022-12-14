using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Reports;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhChuyenXepLuongController : ViewController
    {
        public MailMerge_QuyetDinhChuyenXepLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhChuyenXepLuong>();
            QuyetDinhChuyenXepLuong qd;
            IObjectSpace obs = Application.CreateObjectSpace();

            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhChuyenXepLuong)item;
                if (qd != null)
                    list.Add(qd);   
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhChuyenXepLuong>>>().Merge(obs, list);
        }
    }
}
