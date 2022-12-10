using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.XuLyQuyTrinh.NghiHuu;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module;
using PSC_HRM.Module.NghiPhep;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_ThongBaoNghiPhepController : ViewController
    {
        public MailMerge_ThongBaoNghiPhepController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<ChiTietThongTinNghiPhep>();
            ChiTietThongTinNghiPhep qd;

            foreach (object item in View.SelectedObjects)
            {
                qd = (ChiTietThongTinNghiPhep)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<ChiTietThongTinNghiPhep>>>().Merge(Application.CreateObjectSpace(), list);
        }
        private void MailMerge_ThongBaoNghiPhepController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = (HamDungChung.IsWriteGranted<ThongTinNghiPhep>());
             

        }
    }
}
