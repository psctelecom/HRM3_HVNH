using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Security;
using DevExpress.Utils;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using System.Windows.Forms;
using System.Data;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using System.Text;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DoanDang;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using PSC_HRM.Module.Controllers;
using PSC_HRM.Module;
using PSC_HRM.Module.ChotThongTinTinhLuong;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThongTinLuong_CapNhatThongTinTinhLuongController : ViewController
    {
        private ThongTinLuong_CapNhatThongTin _objDLU;
        private IObjectSpace _obs;
        private BangChotThongTinTinhLuong _currentBangChot;

        public ThongTinLuong_CapNhatThongTinTinhLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ThongTinLuong_CapNhatThongTinTinhLuongController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("QNU"))
            {
                popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangChotThongTinTinhLuong>();
            }
            else
            {
                popupWindowShowAction1.Active["TruyCap"] = false;
            }
        }

        private void ThongTinLuong_CapNhatThongTinTinhLuongController_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            _currentBangChot = View.CurrentObject as BangChotThongTinTinhLuong; 
            //
            if (_currentBangChot != null)
            {
                if (TruongConfig.MaTruong.Equals("QNU"))
                {
                    _objDLU = _obs.CreateObject<ThongTinLuong_CapNhatThongTin>();
                    e.View = Application.CreateDetailView(_obs, _objDLU);
                }
            }
        }

        private void ThongTinLuong_CapNhatThongTinTinhLuongController_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("QNU"))
            {
                #region Số ký điện và nước
                if (_objDLU.LoaiThongTinLuong == LoaiThongTinLuongEnum.SoThangThanhToan)
                {
                    //
                    ThongTinLuong_CapNhatThongTinLuong.SoThangThanhToan(_obs, _currentBangChot.Oid);
                }
                #endregion      
            }

            View.ObjectSpace.Refresh();
            View.Refresh();
        }
    }
}
