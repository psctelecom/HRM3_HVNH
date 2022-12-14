using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThanhLapHoiDongKhenThuongController : ViewController
    {
        public MailMerge_QuyetDinhThanhLapHoiDongKhenThuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThanhLapHoiDongKhenThuong>();
            QuyetDinhThanhLapHoiDongKhenThuong qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThanhLapHoiDongKhenThuong)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhThanhLapHoiDongKhenThuong>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
