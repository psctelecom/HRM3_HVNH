using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_DonXacNhanBangLuongController : ViewController
    {
        private IObjectSpace obs;
        private ThongTinNhanVien nhanVien;
        private Luong_DonXacNhanBangLuong donXacNhanBangLuong;

        public MailMerge_DonXacNhanBangLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {          
            obs = Application.CreateObjectSpace();            
            nhanVien = View.CurrentObject as ThongTinNhanVien;

            if (nhanVien != null)
            {               
                donXacNhanBangLuong = obs.CreateObject<Luong_DonXacNhanBangLuong>();
                donXacNhanBangLuong.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                e.View = Application.CreateDetailView(obs, donXacNhanBangLuong);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {           
            var list = new List<Luong_DonXacNhanBangLuong>();       
            if (donXacNhanBangLuong != null)
            {
                list.Add(donXacNhanBangLuong);
                SystemContainer.Resolver<IMailMerge<IList<Luong_DonXacNhanBangLuong>>>().Merge(Application.CreateObjectSpace(), list);
            }
        }

        private void MailMerge_DonXacNhanBangLuongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active.Clear();
            if (TruongConfig.MaTruong.Equals("NEU"))
                popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ThongTinNhanVien>();
            else
                popupWindowShowAction1.Active["TruyCap"] = false;
        }
    }
}
