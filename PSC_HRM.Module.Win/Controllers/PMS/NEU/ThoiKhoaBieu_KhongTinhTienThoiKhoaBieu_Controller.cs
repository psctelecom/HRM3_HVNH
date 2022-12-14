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
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class ThoiKhoaBieu_KhongTinhTienThoiKhoaBieu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        QuanLyKhongTinhTienTKB _source;
        ThoiKhoaBieu_KhoiLuongGiangDay _TKB;
        Session ses;
        public ThoiKhoaBieu_KhongTinhTienThoiKhoaBieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ThoiKhoaBieu_KhoiLuongGiangDay_DetailView";
        }

        void ThoiKhoaBieu_KhongTinhTienThoiKhoaBieu_Controller_Activated(object sender, System.EventArgs e)
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

                    if (_TKB != null)
                    {
                        _source = new QuanLyKhongTinhTienTKB(ses);
                        _source.LoadDuLieu();
                        _source.NamHoc = ses.GetObjectByKey<NamHoc>(_TKB.NamHoc.Oid);
                        _source.HocKy = ses.GetObjectByKey<HocKy>(_TKB.HocKy.Oid);
                        _source.KhongTinhTien = true;
                        e.View = Application.CreateDetailView(_obs, _source);
                    }
                }
            }
        }
        private void popDongBoTK_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if(_source.LisDanhSach.Count > 0)
            {
                foreach(var item in _source.LisDanhSach)
                {
                    if(item.Chon)
                    {
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@Oid", item.Oid);
                        param[1] = new SqlParameter("@Khoa", _source.KhongTinhTien.GetHashCode());
                        DataProvider.ExecuteNonQueryTimeOut("spd_PMS_KhongTinhTien_CheckDuLieu", System.Data.CommandType.StoredProcedure, param);
                    }
                }
            }
           
        }
    }
}