using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhGia;
using DevExpress.Utils;
using PSC_HRM.Module.ChamCong;
using System.Windows.Forms;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.Controllers;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.Controllers
{
    public partial class NangLuong_ImportNangLuongController : ViewController
    {
        public NangLuong_ImportNangLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NangLuong_ImportNangLuongController");
        }

        private void NangLuong_ImportNangLuongController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<DeNghiNangLuong>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            DeNghiNangLuong deNghiNangLuong = View.CurrentObject as DeNghiNangLuong;
            //
            if (deNghiNangLuong != null)
            {
                if (TruongConfig.MaTruong.Equals("NEU"))
                {
                    NangLuong_ImportNangLuong.XuLy_NEU(View.ObjectSpace, deNghiNangLuong);
                }              
                else
                {
                    NangLuong_ImportNangLuong.XuLy(View.ObjectSpace, deNghiNangLuong);
                }
            }
        }
    }
}
