using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhChuyenNganhDaoTaoMoiController : ViewController
    {
        public MailMerge_QuyetDinhChuyenNganhDaoTaoMoiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhChuyenNganhDaoTaoMoi>();
            QuyetDinhChuyenNganhDaoTaoMoi qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhChuyenNganhDaoTaoMoi)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhChuyenNganhDaoTaoMoi>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
