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
using PSC_HRM.Module.PMS.NonPersistentObjects.NEU;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.Win.Controllers.PMS.ThoiKhoaBieu
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ThoiKhoaBieu_PhanCongGiangDay_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        PhanCongGiangDay_ThoiKhoiBieu _source;
        Session ses;
        public ThoiKhoaBieu_PhanCongGiangDay_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetViewId = "ThoiKhoaBieu_KhoiLuongGiangDay_DetailView";
        }

        private void popPhanCongGiangDay_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(PhanCongGiangDay_ThoiKhoiBieu));

            ThoiKhoaBieu_KhoiLuongGiangDay obj = View.CurrentObject as ThoiKhoaBieu_KhoiLuongGiangDay;
            if (obj != null)
            //if (obj.BangChotThuLao != null)
            //{
            //    XtraMessageBox.Show("Đã chốt khối lượng - không thể phân công giảng dạy!");
            //    return;
            //}
            //else
            {
                using (DialogUtil.AutoWait("Load danh sách phân công giảng dạy"))
                {
                    collectionSource = new CollectionSource(_obs, typeof(PhanCongGiangDay_ThoiKhoiBieu));
                    _source = new PhanCongGiangDay_ThoiKhoiBieu(ses);
                    _source.ThoiKhoaBieu_KhoiLuongGiangDay = obj;
                    e.View = Application.CreateDetailView(_obs, _source);
                }
            }
        }

        private void popPhanCongGiangDay_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            List<ChiTietPhanCong> listChiTiet = (from d in _source.ListChiTiet
                                                 where d.NhanVien != null
                                                 select d).ToList();
            string sql = "";
            if (listChiTiet != null)
            {
                foreach (ChiTietPhanCong item in listChiTiet)
                {
                    sql += " Union All Select '" + item.NhanVien.Oid.ToString() + "' as NhanVien, '"
                        + item.OidChiTiet + "' as OidChiTiet";
                }
                if (sql != "")
                {
                    SqlCommand cmd = new SqlCommand("spd_PMS_ThoiKhoaBieu_PhanCongGiangDay", DataProvider.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@String", sql.Substring(11));
                    cmd.Parameters.AddWithValue("@User", HamDungChung.CurrentUser().UserName.ToString());
                    cmd.ExecuteNonQuery();
                }
                View.ObjectSpace.Refresh();
            }
        }
    }
}