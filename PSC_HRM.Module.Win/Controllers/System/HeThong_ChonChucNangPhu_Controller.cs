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
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.BusinessObjects.NonPersistentObjects.System;
using PSC_HRM.Module.BusinessObjects.BaoMat;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class HeThong_ChonChucNangPhu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        ChonChucNangPhu _source;
        Session _ses;
        PhanQuyenChucNangPhu _PhanQuyenChucNangPhu;
        public HeThong_ChonChucNangPhu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "PhanQuyenChucNangPhu_DetailView";
        }
        private void popDongBo_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            _PhanQuyenChucNangPhu = View.CurrentObject as PhanQuyenChucNangPhu;
            string sqlApp = "SELECT AppObject, Caption, KeyObject,AppMenu.Oid";
            sqlApp += " FROM dbo.AppMenu";
            sqlApp += " JOIN dbo.AppObject ON AppObject.Oid = AppMenu.AppObject";
            sqlApp += " WHERE AppMenu.GCRecord IS NULL";
            sqlApp += " AND AppObject.GCRecord IS NULL";
            sqlApp += " AND ISNULL(SuDung,0)=1";
            DataTable dt = DataProvider.GetDataTable(sqlApp, CommandType.Text);

            if (_PhanQuyenChucNangPhu != null)
            {
                _ses = ((XPObjectSpace)_obs).Session;
                collectionSource = new CollectionSource(_obs, typeof(ChonChucNangPhu));
                //XPCollection<AppObject> listObject = new XPCollection<AppObject>(_ses);
                if (dt != null)
                {
                    string sql;
                    object _phanHe;
                    AppMenu appMenu = null;
                    AppObject appObject = null;
                    using (DialogUtil.AutoWait("Đang lấy danh sách chức năng phụ"))
                    {
                        foreach (DataRow item in dt.Rows)
                        //foreach (AppObject item in listObject)
                        {
                            sql = "SELECT TOP 1 CASE WHEN AppMenu.PhanHe = 0 THEN 'NhanSu'";
                            sql += " WHEN AppMenu.PhanHe = 1 THEN 'TaiChinh'";
                            sql += " WHEN AppMenu.PhanHe = 2 THEN 'DanhMuc'";
                            sql += " WHEN AppMenu.PhanHe = 3 THEN 'HeThong'";
                            sql += " WHEN AppMenu.PhanHe = 4 THEN 'BanLamViec'";
                            sql += " WHEN AppMenu.PhanHe = 5 THEN 'PMS'";
                            sql += " END TenPhanHe";
                            sql += " FROM dbo.AppMenu AppMenu WHERE AppObject ='" + item["AppObject"].ToString() + "'";

                            Type objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == item["KeyObject"].ToString());
                            if (objecttype != null)
                            {
                                var i = objecttype.GetCustomAttributes(typeof(NonPersistentAttribute), false).Length != 0;
                                if (i)
                                {
                                    _phanHe = DataProvider.GetObject(sql, CommandType.Text);
                                    appMenu = _ses.FindObject<AppMenu>(CriteriaOperator.Parse("AppObject =?", item["AppObject"].ToString()));
                                    appObject = _ses.FindObject<AppObject>(CriteriaOperator.Parse("Oid =?", item["AppObject"].ToString()));
                                    if (_phanHe != null && appMenu != null && appMenu.SuDung && appObject != null)
                                    {
                                        _source = new ChonChucNangPhu(_ses);
                                        _source.Key = appObject.KeyObject;
                                        _source.Caption = appObject.Caption;
                                        _source.OidObject = appObject.Oid;
                                        _source.PhanHe = _phanHe.ToString();
                                        collectionSource.Add(_source);
                                    }
                                }
                            }
                        }
                    }
                }
                e.View = Application.CreateListView("ChonChucNangPhu_ListView", collectionSource, true);
            }
        }
        private void popDongBo_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            _PhanQuyenChucNangPhu = View.CurrentObject as PhanQuyenChucNangPhu;
            if (collectionSource != null && _PhanQuyenChucNangPhu != null)
            {

                foreach (ChonChucNangPhu item in collectionSource.List)
                {
                    if (item.Chon)
                    {
                        DanhSachChucNangPhu ChucNang = new DanhSachChucNangPhu(_PhanQuyenChucNangPhu.Session);
                        ChucNang.AppMenu = _PhanQuyenChucNangPhu.Session.FindObject<AppMenu>(CriteriaOperator.Parse("AppObject =?", item.OidObject));
                        ChucNang.PhanQuyenChucNangPhu = _PhanQuyenChucNangPhu;
                    }
                }
                View.ObjectSpace.CommitChanges();
            }
        }
    }
}