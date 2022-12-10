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
using PSC_HRM.Module.PMS.NonPersistentObjects.ThanhToan;
using PSC_HRM.Module.PMS.NghiepVu.ThanhToan;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class QuanLyThanhToanBoSung_Controller : ViewController
    {
        QuanLyThanhToanBoSung _quanLy;
        IObjectSpace _obs;
        Session ses;
        Chon_ThanhToanBoSung _source;
        public QuanLyThanhToanBoSung_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyThanhToanBoSung_DetailView";
        }

        void QuanLyThanhToanBoSung_Controller_Activated(object sender, System.EventArgs e)
        {
        }
        private void btnChinhSua_CustomizePopupWindowParams_1(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _quanLy = View.CurrentObject as QuanLyThanhToanBoSung;
            if (_quanLy != null)
            {
                _obs = Application.CreateObjectSpace();
                ses = ((XPObjectSpace)_obs).Session;
                {
                    _source = new Chon_ThanhToanBoSung(ses);
                    _source.NamHoc = ses.FindObject<NamHoc>(CriteriaOperator.Parse("Oid =?", _quanLy.NamHoc.Oid));

                    e.View = Application.CreateDetailView(_obs, _source);
                }
                //e.View = Application.CreateListView("BangCongNo_InCongNoChiTiet_ListView", collectionSource, true);
            }
        }

        private void btnChinhSua_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            List<dsChiTietThanhToanThuLaoBoSung> listChiTiet = (from d in _source.listChiTiet
                                                                where d.Chon == true
                                                                select d).ToList();
            string sql = "";
            string GuidEmpty = "'" + Guid.Empty.ToString() + "'";
            if (listChiTiet != null)
            {
                using (DialogUtil.AutoWait("Đang tạo chi tiết thanh toán bổ sung"))
                {
                    foreach (dsChiTietThanhToanThuLaoBoSung item in listChiTiet)
                    {
                        string Oid = item.OidChiTietBangChotThuLaoGiangDay == null ? GuidEmpty : ("CONVERT(UNIQUEIDENTIFIER,'" + item.OidChiTietBangChotThuLaoGiangDay.ToString() + "')");
                        sql += " UNION ALL " + "SELECT "
                                        + Oid + " as OidChiTiet, CONVERT(DECIMAL(18,2), '"
                                        + item.TongGioA1.ToString().Replace(",", ".") + " ') as TongGioA1, CONVERT(DECIMAL(18,2), '"
                                        + item.TongGioA2.ToString().Replace(",", ".") + " ') as TongGioA2, CONVERT(DECIMAL(18,2), '"
                                        + item.TongGio.ToString().Replace(",", ".") + " ') as TongGio, CONVERT(DECIMAL(18,2), '"
                                        + item.SoTienThanhToan.ToString().Replace(",", ".") + " ') as SoTienThanhToan,'"
                                        + item.CongTru.GetHashCode() + "' as CongTru, '"
                                        + item.GhiChu.Replace(",", ".") + "' as GhiChu";
                    }
                    if (sql != "")
                    {
                        SqlParameter[] p = new SqlParameter[2];
                        p[0] = new SqlParameter("@QuanLy", _quanLy.Oid);
                        p[1] = new SqlParameter("@String", sql.Substring(11));
                        DataProvider.ExecuteNonQuery("spd_PMS_Tao_ThanhToanBoSung", CommandType.StoredProcedure, p);
                        View.ObjectSpace.Refresh();
                    }
                }
            }
        }
    }
}
