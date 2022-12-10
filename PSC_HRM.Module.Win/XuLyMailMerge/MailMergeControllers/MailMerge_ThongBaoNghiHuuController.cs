using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.XuLyQuyTrinh.NghiHuu;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_ThongBaoNghiHuuController : ViewController
    {
        public MailMerge_ThongBaoNghiHuuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list1 = new List<QuyetDinhNghiHuu>();
            var list2 = new List<ThongTinNghiHuu>();
            QuyetDinhNghiHuu qd;
            ThongTinNghiHuu tt;
            foreach (object item in View.SelectedObjects)
            {
                if (item is QuyetDinhNghiHuu)
                {
                    qd = (QuyetDinhNghiHuu)item;
                    if (qd != null)
                        list1.Add(qd);
                }
                else if (item is ThongTinNghiHuu)
                {
                    tt = (ThongTinNghiHuu)item;
                    if (tt != null)
                        list2.Add(tt);
                }
            }
            if (list1.Count > 0)
                SystemContainer.Resolver<IMailMerge<IList<QuyetDinhNghiHuu>>>("ThongBaoNghiHuu1").Merge(Application.CreateObjectSpace(), list1);
            if (list2.Count > 0)
                SystemContainer.Resolver<IMailMerge<IList<ThongTinNghiHuu>>>().Merge(Application.CreateObjectSpace(), list2);
        }

        private void MailMerge_ThongBaoNghiHuuController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = (HamDungChung.IsWriteGranted<ThongTinNghiHuu>())
                || (HamDungChung.IsWriteGranted<QuyetDinhNghiHuu>());

        }
    }
}
