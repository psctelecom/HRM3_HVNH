using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using DevExpress.XtraEditors;
using PSC_HRM.Module.PMS.NonPersistent;
using PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects;
using PSC_HRM.Module.PMS.BusinessObjects.DanhMuc;
using System.Data;
using System.Data.SqlClient;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class BangChotThuLao_ChinhSua_TienGiang_Controller : ViewController
    {
        IObjectSpace _obs;
        Session ses;
        CollectionSource collectionSource;
        ChiTietThuLao_NhanVien_Update _source;  
        public BangChotThuLao_ChinhSua_TienGiang_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ChiTietThuLaoNhanVien_Update_DetailView";
        }
        void BangChotThuLao_ChinhSua_TienGiang_Controller_Activated(object sender, System.EventArgs e)
        {
            //if (TruongConfig.MaTruong == "QNU")
            //    btnChinhSua.Active["TruyCap"] = false;
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
             _obs = View.ObjectSpace;
            Session ses = ((XPObjectSpace)_obs).Session;
            ChiTietThuLaoNhanVien_Update obj = View.CurrentObject as ChiTietThuLaoNhanVien_Update;
            if (obj != null)
            {
                View.ObjectSpace.CommitChanges();
                using (DialogUtil.AutoWait("Đang cập nhật dữ liệu"))
                {

                    foreach (dsChiTietThuLao_NhanVien_Update item in _source.listChiTietThuLao)
                    {
                        if (item.Chon)
                        {
                            SqlParameter[] param = new SqlParameter[7];
                            param[0] = new SqlParameter("@OidChiTietBangChotThuLaoGiangDay", item.OidChiTietBangChotThuLaoGiangDay);
                            param[1] = new SqlParameter("@OidThongTinBangChot", item.OidThongTinBangChot);
                            param[2] = new SqlParameter("@TongGioA1", item.TongGioA2);
                            param[3] = new SqlParameter("@TongGioA2", item.TongNo);
                            param[4] = new SqlParameter("@SoTienThanhToan", item.SoTienThanhToan);
                            param[5] = new SqlParameter("@TongNo", item.TongNo);
                            param[6] = new SqlParameter("@CongTru", item.CongTru);
                            DataProvider.ExecuteNonQuery("spd_pms_KeKhaiNhanVienThuLao", CommandType.StoredProcedure, param);
                        }
                    }
                    View.ObjectSpace.Refresh();

                }
            }
        }
            
        private void btnChinhSua_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(dsChiTietThuLao_NhanVien_Update));
            ChiTietThuLaoNhanVien_Update obj = View.CurrentObject as ChiTietThuLaoNhanVien_Update;
            if (obj != null)
            {
                using (DialogUtil.AutoWait("Load danh sách giảng viên"))
                {
                    collectionSource = new CollectionSource(_obs, typeof(ChiTietThuLao_NhanVien_Update));
                    _source = new ChiTietThuLao_NhanVien_Update(ses);
                    e.View = Application.CreateDetailView(_obs, _source);
                }
            }


        }
    }
}
