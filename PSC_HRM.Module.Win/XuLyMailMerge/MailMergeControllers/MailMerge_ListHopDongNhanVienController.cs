using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using System.Collections.Generic;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_ListHopDongNhanVienController : ViewController
    {
        public MailMerge_ListHopDongNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var listHDK = new List<HopDong_Khoan>();
            var listPLHDK = new List<HopDong_Khoan>();
            var listHDLV = new List<HopDong_LamViec>();
            var listHDLD = new List<HopDong_LaoDong>();

            foreach (object item in View.SelectedObjects)
            {
                if (item is HopDong_Khoan)
                {
                    HopDong_Khoan hd = item as HopDong_Khoan;
                    if (hd.HopDongKhoan == null)
                        listHDK.Add(hd);
                    else
                        listPLHDK.Add(hd);
                }
                if (item is HopDong_LamViec)
                    listHDLV.Add(item as HopDong_LamViec);
                if (item is HopDong_LaoDong)
                    listHDLD.Add(item as HopDong_LaoDong);
            }
            if (listHDK.Count > 0)
                SystemContainer.Resolver<IMailMerge<IList<HopDong_Khoan>>>("HopDongKhoan").Merge(Application.CreateObjectSpace(), listHDK);
            if (listPLHDK.Count > 0)
                SystemContainer.Resolver<IMailMerge<IList<HopDong_Khoan>>>("PhuLucHopDongKhoan").Merge(Application.CreateObjectSpace(), listPLHDK);
            if (listHDLV.Count > 0)
                SystemContainer.Resolver<IMailMerge<IList<HopDong_LamViec>>>().Merge(Application.CreateObjectSpace(), listHDLV);
            if (listHDLD.Count > 0)
                SystemContainer.Resolver<IMailMerge<IList<HopDong_LaoDong>>>().Merge(Application.CreateObjectSpace(), listHDLD);
        }

        private void MailMerge_HopDongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<HopDong_Khoan>();
        }
    }
}
