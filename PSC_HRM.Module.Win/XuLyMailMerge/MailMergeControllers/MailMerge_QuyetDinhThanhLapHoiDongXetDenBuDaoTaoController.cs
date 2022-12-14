using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThanhLapHoiDongXetDenBuDaoTaoController : ViewController
    {
        public MailMerge_QuyetDinhThanhLapHoiDongXetDenBuDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThanhLapHoiDongXetDenBuDaoTao>();
            QuyetDinhThanhLapHoiDongXetDenBuDaoTao qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThanhLapHoiDongXetDenBuDaoTao)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhThanhLapHoiDongXetDenBuDaoTao>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
