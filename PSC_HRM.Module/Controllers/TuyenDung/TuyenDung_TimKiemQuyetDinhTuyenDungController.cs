﻿using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.TapSu;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class TuyenDung_TimKiemQuyetDinhTuyenDungController : ViewController
    {
        private IObjectSpace _obs;
        QuyetDinh_TimNhanVien _view;

        public TuyenDung_TimKiemQuyetDinhTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TuyenDung_TimKiemQuyetDinhTuyenDungController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();

            _view = _obs.CreateObject<QuyetDinh_TimNhanVien>();
            //
            e.View = Application.CreateDetailView(_obs, _view);
        }

        private void TuyenDung_TimKiemQuyetDinhTuyenDungController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhTuyenDung>();
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //
            if (_view != null && _view.ThongTinNhanVien != null)
            {
                ListView listView = View as ListView;
                CriteriaOperator filter = CriteriaOperator.Parse("ListChiTietQuyetDinhTuyenDung[ThongTinNhanVien.Oid=?]", _view.ThongTinNhanVien.Oid);
                listView.CollectionSource.Criteria["Filter"] = filter;
                
            }
            else
            {
                ListView listView = View as ListView;
                listView.CollectionSource.Criteria["Filter"] = null;
            }
            
            

        }
    }
}
