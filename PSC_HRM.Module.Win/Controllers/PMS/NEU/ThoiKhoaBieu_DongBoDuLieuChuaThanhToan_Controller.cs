using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.NonPersistent;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using PSC_HRM.Module.PMS.NonPersistentObjects.NEU;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class ThoiKhoaBieu_DongBoDuLieuChuaThanhToan_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        QuanLyDuLieuChuaThanhToan _source;
        ThoiKhoaBieu_KhoiLuongGiangDay _TKB;
        Session ses;
        string _Text = "";
        public ThoiKhoaBieu_DongBoDuLieuChuaThanhToan_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ThoiKhoaBieu_KhoiLuongGiangDay_DetailView";
        }

        void ThoiKhoaBieu_DongBoDuLieuChuaThanhToan_Controller_Activated(object sender, System.EventArgs e)
        {
            //if (TruongConfig.MaTruong == "HVNH")
            //    popDongBo.Active["TruyCap"] = false;
        }


        private void popDongBoTK_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _TKB = View.CurrentObject as ThoiKhoaBieu_KhoiLuongGiangDay;
            if (_TKB != null)
            {
                using (DialogUtil.AutoWait("Đang kiểm tra dữ liệu"))
                {
                    _obs = Application.CreateObjectSpace();
                    ses = ((XPObjectSpace)_obs).Session;
                    _source = new QuanLyDuLieuChuaThanhToan(ses);
                    _source.LoadDuLieu();
                    e.View = Application.CreateDetailView(_obs, _source);
                }
            }
        }
        private void popDongBoTK_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            _TKB = View.CurrentObject as ThoiKhoaBieu_KhoiLuongGiangDay;
            _Text = "";
            if (_TKB != null)
            {
                using (DialogUtil.AutoWait("Đang thực hiện"))
                {
                   if(_source != null)
                   {
                       if(_source.LisDanhSach != null)
                       {
                           foreach(ChiTietDuLieuChuaThanhToan item in _source.LisDanhSach)
                           {
                               if(item.Chon)
                               {
                                   _Text += item.Oid.ToString() + ";";
                               }
                           }

                            SqlParameter[] param = new SqlParameter[2];
                            param[0] = new SqlParameter("@ThoiKhoaBieu", _TKB.Oid);
                            param[1] = new SqlParameter("@StringOid", _Text);
                            DataProvider.ExecuteNonQuery("spd_PMS_DongBo_DuLieuChuaThanhToan", CommandType.StoredProcedure, param);
                       }
                   }
                }
            }
            View.Refresh();
        }
    }
}