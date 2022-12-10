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
    public partial class HoSo_CapNhatThongTinHoSoController : ViewController
    {
        private HoSo_CapNhatThongTinHoSoUTE _objUTE;
        private HoSo_CapNhatThongTinHoSoDLU _objDLU;
        private IObjectSpace _obs;

        public HoSo_CapNhatThongTinHoSoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HoSo_CapNhatThongTinHoSoController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("DLU") || TruongConfig.MaTruong.Equals("UTE"))
            {
                popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ThongTinNhanVien>();
            }
            else
            {
                popupWindowShowAction1.Active["TruyCap"] = false;
            }
        }

        private void HoSo_CapNhatThongTinHoSoController_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            //
            if (TruongConfig.MaTruong.Equals("UTE"))
            {
                _objUTE = _obs.CreateObject<HoSo_CapNhatThongTinHoSoUTE>();
                e.View = Application.CreateDetailView(_obs, _objUTE);
            } 
            if (TruongConfig.MaTruong.Equals("DLU"))
            {
                _objDLU = _obs.CreateObject<HoSo_CapNhatThongTinHoSoDLU>();
                e.View = Application.CreateDetailView(_obs, _objDLU);
            }        
        }

        private void HoSo_CapNhatThongTinHoSoController_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {

            if (TruongConfig.MaTruong.Equals("UTE"))
            {
                #region Thông tin sức khỏe
                if (_objUTE.LoaiThongTinHoSo == LoaiThongTinHoSoUTE.ThongTinSucKhoe)
                {
                    //
                    HoSo_ImportThongTinHoSo.ThongTinSucKhoe(_obs);
                }
                #endregion
            }

            if (TruongConfig.MaTruong.Equals("DLU"))
            {
                #region Công việc hiện nay
                if (_objDLU.LoaiThongTinHoSo == LoaiThongTinHoSoDLU.CongViecHienNay)
                {
                    //
                    HoSo_ImportThongTinHoSo.CongViecHienNay(_obs);
                }
                #endregion 
                
                #region Tình trạng
                if (_objDLU.LoaiThongTinHoSo == LoaiThongTinHoSoDLU.TinhTrang)
                {
                    //
                    HoSo_ImportThongTinHoSo.CongViecHienNay(_obs);
                }
                #endregion 

                #region Loại hợp đồng
                if (_objDLU.LoaiThongTinHoSo == LoaiThongTinHoSoDLU.LoaiHopDong)
                {
                    //
                    HoSo_ImportThongTinHoSo.LoaiHopDong(_obs);
                }
                #endregion 

                #region Loại nhân viên
                if (_objDLU.LoaiThongTinHoSo == LoaiThongTinHoSoDLU.LoaiNhanSu)
                {
                    //
                    HoSo_ImportThongTinHoSo.LoaiNhanSu(_obs);
                }
                #endregion 

                #region Chức vụ
                if (_objDLU.LoaiThongTinHoSo == LoaiThongTinHoSoDLU.ChucVu)
                {
                    //
                    HoSo_ImportThongTinHoSo.LoaiHopDong(_obs);
                }
                #endregion 
            }
        }
    }
}
