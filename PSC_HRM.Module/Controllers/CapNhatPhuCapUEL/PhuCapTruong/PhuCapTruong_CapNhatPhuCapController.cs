using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.XuLyQuyTrinh.NangThamNienCongTac;
using PSC_HRM.Module;
using PSC_HRM.Module.PhuCapTruong;

namespace PSC_HRM.Module.Controller
{
    public partial class PhuCapTruong_CapNhatPhuCapController : ViewController
    {
        public PhuCapTruong_CapNhatPhuCapController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            DanhSachPhuCapTruong trachNhiem = View.CurrentObject as DanhSachPhuCapTruong;
            if (trachNhiem != null)
            {
                trachNhiem.XuLy();
                trachNhiem.LoadDuLieu();
                View.Refresh();
                DialogUtil.ShowInfo("Cập nhật phụ cấp chức vụ cho các cán bộ được chọn thành công");
            }
        }
    }
}
