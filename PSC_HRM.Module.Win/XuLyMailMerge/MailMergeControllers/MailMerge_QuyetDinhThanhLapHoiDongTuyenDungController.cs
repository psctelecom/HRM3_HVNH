using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThanhLapHoiDongTuyenDungController : ViewController
    {
        public MailMerge_QuyetDinhThanhLapHoiDongTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThanhLapHoiDongTuyenDung>();
            QuyetDinhThanhLapHoiDongTuyenDung qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThanhLapHoiDongTuyenDung)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhThanhLapHoiDongTuyenDung>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
