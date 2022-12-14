using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhDieuChinhThoiGianDiNuocNgoaiController : ViewController
    {
        public MailMerge_QuyetDinhDieuChinhThoiGianDiNuocNgoaiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhDieuChinhThoiGianDiNuocNgoai>();
            QuyetDinhDieuChinhThoiGianDiNuocNgoai qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhDieuChinhThoiGianDiNuocNgoai)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhDieuChinhThoiGianDiNuocNgoai>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
