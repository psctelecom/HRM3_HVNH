using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.TapSu;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_ThongBaoHetHanTapSuController : ViewController
    {
        public MailMerge_ThongBaoHetHanTapSuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            DetailView detailView = View as DetailView;

            DanhSachHetHanTapSu obj = detailView.CurrentObject as DanhSachHetHanTapSu;
            if (obj.ListHetHanTapSu.Count > 0)
            {
                var list = new List<DanhSachHetHanTapSu>();
                DanhSachHetHanTapSu qd;
                foreach (object item in View.SelectedObjects)
                {
                    qd = (DanhSachHetHanTapSu)item;
                    if (qd != null)
                        list.Add(qd);
                }
                SystemContainer.Resolver<IMailMerge<IList<DanhSachHetHanTapSu>>>().Merge(Application.CreateObjectSpace(), list);
            }
        }
    }
}
