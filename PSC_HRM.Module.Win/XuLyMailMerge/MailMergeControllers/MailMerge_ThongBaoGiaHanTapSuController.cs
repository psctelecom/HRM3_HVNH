using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_ThongBaoGiaHanTapSuController : ViewController
    {
        public MailMerge_ThongBaoGiaHanTapSuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<ThongBaoGiaHanTapSu>();
            ThongBaoGiaHanTapSu qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (ThongBaoGiaHanTapSu)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<ThongBaoGiaHanTapSu>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
