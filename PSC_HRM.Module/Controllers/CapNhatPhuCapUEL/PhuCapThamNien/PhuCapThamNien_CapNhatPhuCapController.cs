using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.XuLyQuyTrinh.NangThamNienCongTac;
using PSC_HRM.Module;
using PSC_HRM.Module.PhuCapTruong;
using PSC_HRM.Module.NonPersistentObjects;

namespace PSC_HRM.Module.Controller
{
    public partial class PhuCapThamNien_CapNhatPhuCapController : ViewController
    {
        public PhuCapThamNien_CapNhatPhuCapController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            DanhSachPhuCapThamNien obj = View.CurrentObject as DanhSachPhuCapThamNien;
            if (obj != null)
            {
                obj.XuLy();
                View.Refresh();
                DialogUtil.ShowInfo("Cập nhật HSPC thâm niên thành công.", "Thông báo");
            }
        }
    }
}
