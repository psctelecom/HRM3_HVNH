using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhChiTienThuongTienSiController : ViewController
    {
        public MailMerge_QuyetDinhChiTienThuongTienSiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhChiTienThuongTienSi>();
            QuyetDinhChiTienThuongTienSi qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhChiTienThuongTienSi)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<MailMerge_QuyetDinhChiTienThuongTienSi>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
