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
    public partial class MailMerge_ThongBaoKeoDaiThoiGianCongTacController : ViewController
    {
        public MailMerge_ThongBaoKeoDaiThoiGianCongTacController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list1 = new List<QuyetDinhKeoDaiThoiGianCongTac>();
            var list2 = new List<ThongTinNghiHuu>();
            QuyetDinhKeoDaiThoiGianCongTac qd;
            ThongTinNghiHuu tt;
            foreach (object item in View.SelectedObjects)
            {
                if (item is QuyetDinhKeoDaiThoiGianCongTac)
                {
                    qd = (QuyetDinhKeoDaiThoiGianCongTac)item;
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
                SystemContainer.Resolver<IMailMerge<IList<QuyetDinhKeoDaiThoiGianCongTac>>>("ThongBaoKeoDaiThoiGianCongTac").Merge(Application.CreateObjectSpace(), list1);
            if (list2.Count > 0)
                SystemContainer.Resolver<IMailMerge<IList<ThongTinNghiHuu>>>().Merge(Application.CreateObjectSpace(), list2);
        }

        private void MailMerge_ThongBaoKeoDaiThoiGianCongTacController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = (HamDungChung.IsWriteGranted<ThongTinNghiHuu>())
                || (HamDungChung.IsWriteGranted<QuyetDinhKeoDaiThoiGianCongTac>());

        }
    }
}
