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
using PSC_HRM.Module.ChuyenNgach;
using PSC_HRM.Module.ChotThongTinTinhLuong;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThongTinLuong_ImportThongTinTinhLuongController : ViewController
    {
        public ThongTinLuong_ImportThongTinTinhLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThongTinLuong_ImportThongTinTinhLuongController");
        }

        private void ThongTinLuong_ImportThongTinTinhLuongController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("QNU"))
            {
                simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangChotThongTinTinhLuong>();
            }
            else
            {
                simpleAction.Active["TruyCap"] = false;
            }
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            BangChotThongTinTinhLuong bangChotThongTinTinhLuong = View.CurrentObject as BangChotThongTinTinhLuong;

            if (bangChotThongTinTinhLuong != null)
            {
                bool oke = false;
                using (DialogUtil.AutoWait())
                {
                    oke = ThongTinLuong_ImportThongTinLuong.XuLy(View.ObjectSpace, bangChotThongTinTinhLuong);
                }

                //Xuất thông báo cho người dùng
                if (oke)
                {
                    DialogUtil.ShowInfo("Import thông tin tính lương thành công.");
                }
                else
                {
                    DialogUtil.ShowError("Import thông tin tính lương không thành công.");
                }
            }
        }
    }
}
