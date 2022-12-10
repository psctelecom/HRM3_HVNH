using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.NonPersistentObjects;

namespace PSC_HRM.Module.Controllers
{
    public partial class BaoHiem_CapNhatBHYTController : ViewController
    {
        private IObjectSpace obs;
        private BaoHiem_CapNhatBHYT bhyt;
        public BaoHiem_CapNhatBHYTController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void BaoHiem_CapNhatBHYTController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = false;
            //popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<HoSoBaoHiem>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            bhyt = obs.CreateObject<BaoHiem_CapNhatBHYT>();
            e.View = Application.CreateDetailView(obs, bhyt);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            obs = View.ObjectSpace;
            CriteriaOperator filter = CriteriaOperator.Parse("TrangThai!=2");
            using (XPCollection<HoSoBaoHiem> baoHiem = new XPCollection<HoSoBaoHiem>(((XPObjectSpace)obs).Session, filter))
            {
                foreach (HoSoBaoHiem item in baoHiem)
                {
                    if (item.BoPhan == null)
                        item.BoPhan = item.ThongTinNhanVien.BoPhan;
                    item.TuNgay = bhyt.TuNgay;
                    item.DenNgay = bhyt.DenNgay;
                    item.Save();
                }
            }
            obs.CommitChanges();
            obs.Refresh();
        }
    }
}
