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
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class KeKhai_KhoiLuongGiangDay_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        KhaiBao_KhoiLuongGiangDay _source;
        KhoiLuongGiangDay _View;
        Session ses;
        public KeKhai_KhoiLuongGiangDay_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ABY_DetailView";
        }

        void KeKhai_KhoiLuongGiangDay_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong != "DNU")
                popKeKhaiSauGiang.Active["TruyCap"] = false;
        }
        private void popKeKhaiSauGiang_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)_obs).Session;
            View.ObjectSpace.CommitChanges();
            _View = View.CurrentObject as KhoiLuongGiangDay;
            if (_View != null && !_View.Khoa)
            {
                collectionSource = new CollectionSource(_obs, typeof(KhaiBao_KeKhaiSauGiang));
                using (DialogUtil.AutoWait("Load danh sách giảng viên"))
                {
                    collectionSource = new CollectionSource(_obs, typeof(KhaiBao_KeKhaiSauGiang));
                    _source = new KhaiBao_KhoiLuongGiangDay(ses);
                    e.View = Application.CreateDetailView(_obs, _source);
                }
            }
            else
            {
                XtraMessageBox.Show("Kỳ tính đã khóa không thể kê khai!", "Thông báo");
            }
        }

        private void popKeKhaiSauGiang_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {           
            string sql_Query = "";
            foreach (ChiTietKhaiBao_KhoiLuongGiangDay ct in _source.listKetKhai)
            {
                if (ct.Chon)
                {
                    sql_Query += " UNION ALL " + "SELECT '"
                       + _View.Oid + "' as KhoiLuongGiangDay,'"
                       + ct.OidNhanVien + "' as NhanVien,"                      
                       + ct.ThamQuan.ToString().Replace(",", ".") + " as ThamQuan,"
                       + ct.DiHoc.ToString().Replace(",", ".") + " as DiHoc,"
                       + ct.KiemNhiem.ToString().Replace(",", ".") + " as KiemNhiem,"
                       + ct.ConNho.ToString().Replace(",", ".") + " as ConNho,"
                       + ct.Khac.ToString().Replace(",", ".") + " as Khac,"
                       + ct.NghienCuuKhoaHoc.ToString().Replace(",", ".") + " as NghienCuuKhoaHoc"
                       ;
                }
            }
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@sql_Query", sql_Query != "" ? sql_Query.Substring(11) : "");
            param[1] = new SqlParameter("@UserName",HamDungChung.CurrentUser().Oid);
            param[2] = new SqlParameter("@KhoiLuongGiangDay", _View.Oid);
            DataProvider.ExecuteNonQuery("spd_PMS_KhoiLuongGiangDay_KeKhai", System.Data.CommandType.StoredProcedure, param);
            View.ObjectSpace.Refresh();
        }
    }
}