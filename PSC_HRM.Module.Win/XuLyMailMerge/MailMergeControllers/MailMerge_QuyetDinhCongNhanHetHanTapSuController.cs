using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhCongNhanHetHanTapSuController : ViewController
    {
        public MailMerge_QuyetDinhCongNhanHetHanTapSuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhCongNhanHetHanTapSu>();
            QuyetDinhCongNhanHetHanTapSu qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhCongNhanHetHanTapSu)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<MailMerge_QuyetDinhCongNhanHetHanTapSu>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
