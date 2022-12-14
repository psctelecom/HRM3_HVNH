using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThanhLapHoiDongHetTapSuController : ViewController
    {
        public MailMerge_QuyetDinhThanhLapHoiDongHetTapSuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThanhLapHoiDongDanhGiaHetTapSu>();
            QuyetDinhThanhLapHoiDongDanhGiaHetTapSu qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThanhLapHoiDongDanhGiaHetTapSu)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhThanhLapHoiDongDanhGiaHetTapSu>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
