using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.Win.Forms;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.CauHinh;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_GiayThoiTraLuongController : ViewController
    {
        public MailMerge_GiayThoiTraLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhChuyenCongTac>();
            QuyetDinhChuyenCongTac quyetDinh;
            foreach (object item in View.SelectedObjects)
            {
                quyetDinh = (QuyetDinhChuyenCongTac)item;
                if (quyetDinh != null)
                    list.Add(quyetDinh);
            }

            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhChuyenCongTac>>>("GiayThoiTraLuong").Merge(Application.CreateObjectSpace(), list);
        }

        private void MailMerge_HopDongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] =
                (HamDungChung.IsWriteGranted<QuyetDinhChuyenCongTac>());

        }
    }
}
