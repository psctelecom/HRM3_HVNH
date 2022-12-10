using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.XuLyQuyTrinh.NangThamNienCongTac;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controller
{
    public partial class ThamNienCongTac_CapNhatThamNienCongTacController : ViewController
    {
        public ThamNienCongTac_CapNhatThamNienCongTacController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThamNienCongTac_CapNhatThamNienCongTacController");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThamNienCongTac thamNien = View.CurrentObject as ThamNienCongTac;

            if (thamNien != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();                 
                ThongTinNhanVien nv = obs.GetObjectByKey<ThongTinNhanVien>(thamNien.ThongTinNhanVien.Oid);
                nv.NhanVienThongTinLuong.ThamNienCongTac = thamNien.ThamNienMoi;
                obs.CommitChanges();
                HamDungChung.ShowSuccessMessage("Cập nhật thành công!");
            }
        }
    }
}
