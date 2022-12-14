using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThanhLapHoiDongKyLuatController : ViewController
    {
        public MailMerge_QuyetDinhThanhLapHoiDongKyLuatController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThanhLapHoiDongKyLuat>();
            QuyetDinhThanhLapHoiDongKyLuat quyetDinh;
            foreach (object item in View.SelectedObjects)
            {
                quyetDinh = (QuyetDinhThanhLapHoiDongKyLuat)item;
                if (quyetDinh != null)
                    list.Add(quyetDinh);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhThanhLapHoiDongKyLuat>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
