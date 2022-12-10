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
using System.Windows.Forms;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.Controllers;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NghiPhep;

namespace PSC_HRM.Module.Controllers
{
    public partial class NghiPhep_ImportSoNgayPhepNamController : ViewController
    {
        public NghiPhep_ImportSoNgayPhepNamController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NghiPhep_ImportSoNgayPhepNamController");
        }

        private void NghiPhep_ImportSoNgayPhepNamController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyNghiPhep>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            QuanLyNghiPhep quanLyNghiPhep = View.CurrentObject as QuanLyNghiPhep;

            if (quanLyNghiPhep != null)
            {
                bool oke = false;
                using (DialogUtil.AutoWait())
                {
                    oke = NghiPhep_ImportSoNgayPhepNam.XuLy(View.ObjectSpace, quanLyNghiPhep);
                }

                //Xuất thông báo cho người dùng
                if (oke)
                {
                    //
                    DialogUtil.ShowInfo("Import số ngày phép thành công.");
                }
                else
                {
                    DialogUtil.ShowError("Import số ngày phép không thành công.");
                }
                View.ObjectSpace.Refresh();
            }
        }
    }
}
