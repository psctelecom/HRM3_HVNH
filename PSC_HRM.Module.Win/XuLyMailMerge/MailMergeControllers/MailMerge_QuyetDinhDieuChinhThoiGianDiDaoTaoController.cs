using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhDieuChinhThoiGianDiDaoTaoController : ViewController
    {
        public MailMerge_QuyetDinhDieuChinhThoiGianDiDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhDieuChinhThoiGianDiDaoTao>();
            QuyetDinhDieuChinhThoiGianDiDaoTao qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhDieuChinhThoiGianDiDaoTao)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhDieuChinhThoiGianDiDaoTao>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
