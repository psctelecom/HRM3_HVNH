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

namespace PSC_HRM.Module.Controllers
{
    public partial class HoSo_CapNhatHeSoNhanVienThongTinLuongController : ViewController
    {
        private HoSo_CapNhatHeSoIUH _objIUH;
        private HoSo_CapNhatHeSoUTE _objUTE;
        private HoSo_CapNhatHeSoDLU _objDLU;
        private IObjectSpace _obs;

        public HoSo_CapNhatHeSoNhanVienThongTinLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HoSo_CapNhatHeSoNhanVienThongTinLuongController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("IUH") || TruongConfig.MaTruong.Equals("UTE")
                || TruongConfig.MaTruong.Equals("DLU"))
            {
                popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ThongTinNhanVien>();
            }
            else
            {
                popupWindowShowAction1.Active["TruyCap"] = false;
            }
        }

        private void HoSo_CapNhatHeSoNhanVienThongTinLuongController_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            if (TruongConfig.MaTruong.Equals("IUH"))
            {
                _objIUH = _obs.CreateObject<HoSo_CapNhatHeSoIUH>();
                e.View = Application.CreateDetailView(_obs, _objIUH);
            }
            if (TruongConfig.MaTruong.Equals("UTE"))
            {
                _objUTE = _obs.CreateObject<HoSo_CapNhatHeSoUTE>();
                e.View = Application.CreateDetailView(_obs, _objUTE);
            }
            if (TruongConfig.MaTruong.Equals("DLU"))
            {
                _objDLU = _obs.CreateObject<HoSo_CapNhatHeSoDLU>();
                e.View = Application.CreateDetailView(_obs, _objDLU);
            }        
        }

        private void HoSo_CapNhatHeSoNhanVienThongTinLuongController_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("UTE"))
            {
                #region Phụ cấp ưu đãi
                if (_objUTE.LoaiHeSo == LoaiHeSoEnumUTE.PhuCapUuDai)
                {
                    //
                    HoSo_ImportHeSoNhaVienThongTinLuong.PhuCapUuDai(_obs);
                }
                #endregion

                #region HSL tăng thêm
                if (_objUTE.LoaiHeSo == LoaiHeSoEnumUTE.HSLTangThem)
                {
                    //
                    HoSo_ImportHeSoNhaVienThongTinLuong.HSLTangThem_UTE(_obs);
                }
                #endregion
            }
            if (TruongConfig.MaTruong.Equals("DLU"))
            {
                #region Hệ số độc hại
                if (_objDLU.LoaiHeSo == LoaiHeSoEnumDLU.HSPCDocHai)
                {
                    //
                    HoSo_ImportHeSoNhaVienThongTinLuong.HSPCDocHai_DLU(_obs);
                }
                #endregion
                
                #region Hệ số trách nhiệm
                if (_objDLU.LoaiHeSo == LoaiHeSoEnumDLU.HSPCTrachNhiem)
                {
                    //
                    HoSo_ImportHeSoNhaVienThongTinLuong.HSPCTrachNhiem_DLU(_obs);
                }
                #endregion

                #region Hệ số khu vực
                if (_objDLU.LoaiHeSo == LoaiHeSoEnumDLU.HSPCKhuVuc)
                {
                    //
                    HoSo_ImportHeSoNhaVienThongTinLuong.HSPCKhuVuc_DLU(_obs);
                }
                #endregion

                #region Phụ cấp ưu đãi
                if (_objDLU.LoaiHeSo == LoaiHeSoEnumDLU.PhuCapUuDai)
                {
                    //
                    HoSo_ImportHeSoNhaVienThongTinLuong.PhuCapUuDai(_obs);
                }
                #endregion
            }
        }
    }
}
