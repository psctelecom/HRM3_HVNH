using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Data.SqlClient;
using PSC_HRM.Module.PMS.NghiepVu.ThanhToan;
using PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.UEL;
using System.Data;

namespace PSC_HRM.Module.Win.Controllers.PMS.UEL
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class LoadDuLieu_TinhThuLao_Controller : ViewController
    {       
        private IObjectSpace _obs;
        Session _ses;       
        CollectionSource collectionSource;
        QuanLyNV_ThanhToanThuLao _source;
        private QuanLyHoatDongKhac _qly;
        public LoadDuLieu_TinhThuLao_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            if (TruongConfig.MaTruong == "UEL")
            {
                TargetViewId = "QuanLyHoatDongKhac_DetailView";
            }
            else if (TruongConfig.MaTruong != "UEL")
            {
                TargetViewId = "NULL";
            }          
        }    
        private void popupTinhThuLao_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _qly = View.CurrentObject as QuanLyHoatDongKhac;
            if (_qly != null)
            {
                _obs = Application.CreateObjectSpace();
                _ses = ((XPObjectSpace)_obs).Session;
                collectionSource = new CollectionSource(_obs, typeof(dsQuanLyNV_ThanhToanThuLao));
                _source = new QuanLyNV_ThanhToanThuLao(_ses);
                _source.QuanLyHoatDongKhac = _qly.Oid;
                _source.ThongTinTruong = _qly.ThongTinTruong;
                _source.NamHoc = _qly.NamHoc;               
                //_source.KyTinhPMS = _qly.KyTinhPMS;
                _source.UpdateNV();
                _source.User = HamDungChung.CurrentUser().UserName.ToString();
                e.View = Application.CreateDetailView(_obs, _source);
            }         
        }

        private void popupTinhThuLao_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            string StringSelect_OidChiTietKhoiLuongGiangDay_UEL = "";
            foreach(dsQuanLyNV_ThanhToanThuLao item in this._source.listBangChot)
            {
                if(item.Chon == true)
                {
                    StringSelect_OidChiTietKhoiLuongGiangDay_UEL += item.Oid_ChiTiet.ToString() + ";";
                }
            }


            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName.ToString());
            param[1] = new SqlParameter("@String_OidChiTietKhoiLuongGiangDay_UEL", StringSelect_OidChiTietKhoiLuongGiangDay_UEL);
            param[2] = new SqlParameter("@KQ", SqlDbType.NVarChar, 200);
            param[2].Direction = ParameterDirection.Output;
            param[3] = new SqlParameter("@QuanLyHoatDongKhac", _qly.Oid);
            DataProvider.ExecuteNonQuery("spd_PMS_QuanLyHoatDongKhac_TinhChiTietHoatDongKhac_UEL", System.Data.CommandType.StoredProcedure, param);
            string KQ = param[2].Value.ToString();
            if (KQ == "SUCCESS")
            {
                XtraMessageBox.Show("Lấy dữ liệu từ khối lượng giảng dạy thành công", "THÀNH CÔNG");
            }
            else
            {
                XtraMessageBox.Show(KQ, "LỖI");
            }
            View.Refresh();
            View.ObjectSpace.Refresh();
        }
    }
}
