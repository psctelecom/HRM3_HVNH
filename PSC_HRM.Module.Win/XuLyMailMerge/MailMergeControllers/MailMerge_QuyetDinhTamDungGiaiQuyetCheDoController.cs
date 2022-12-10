using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Actions;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhTamDungGiaiQuyetCheDoController : ViewController
    {
        public MailMerge_QuyetDinhTamDungGiaiQuyetCheDoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhTamDungGiaiQuyetCheDo>();
            QuyetDinhTamDungGiaiQuyetCheDo qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhTamDungGiaiQuyetCheDo)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhTamDungGiaiQuyetCheDo>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
