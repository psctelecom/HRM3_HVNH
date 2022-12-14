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
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using PSC_HRM.Module.PMS.NonPersistentObjects.TaiChinh;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class ThongKe_LayDuLieuKhaoThi_Contronler : ViewController
    {
        IObjectSpace _obs = null;
        Session ses;
        CollectionSource collectionSource;
        ThanhToanPhuLuc01 _source;
        QuanlyTienCongKhaoThi obj;
        public ThongKe_LayDuLieuKhaoThi_Contronler()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanlyTienCongKhaoThi_DetailView";
        }

        void ThongKe_LayDuLieuKhaoThi_Contronler_Activated(object sender, System.EventArgs e)
        {
            //_QuanLy = View.CurrentObject as QuanLyHeSo;
            //if (_QuanLy != null)
            //    if (_QuanLy.ThongTinTruong.MaQuanLy != "DNU")
            //        btDongBo_HeSo.Active["TruyCap"] = false;
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
             _obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(dsThanhToanPhuLuc01));
            obj = View.CurrentObject as QuanlyTienCongKhaoThi;
            if (obj != null)
            {
                using (DialogUtil.AutoWait("Load dữ liệu"))
                {
                    collectionSource = new CollectionSource(_obs, typeof(dsThanhToanPhuLuc01));
                    _source = new ThanhToanPhuLuc01(ses);
                    e.View = Application.CreateDetailView(_obs, _source);
                }
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_obs != null)
            {
                View.ObjectSpace.CommitChanges();
                using (DialogUtil.AutoWait("Đang cập nhật dữ liệu"))
                {
                    string dsQuanLyKhaoThi = "";
                    foreach (dsThanhToanPhuLuc01 item in _source.listChiTiet)
                    {
                        if (item.Chon)
                        {
                            dsQuanLyKhaoThi += item.QuanLyKhaoThi.ToString() + ";";
                        }
                    }

                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@NamHoc", _source.NamHoc.Oid);
                    param[1] = new SqlParameter("@QuanLyKhaoThi", dsQuanLyKhaoThi);
                    param[2] = new SqlParameter("@QuanLyTienCongKhaoThi", obj.Oid);
                    DataProvider.ExecuteNonQuery("spd_PMS_BangThanhToanPhuLuc01", CommandType.StoredProcedure, param);
                    View.ObjectSpace.Refresh();
                }
            }
        }
    }
}