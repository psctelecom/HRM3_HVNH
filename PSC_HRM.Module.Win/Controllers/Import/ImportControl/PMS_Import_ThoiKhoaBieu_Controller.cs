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
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.Win.Controllers.Import.ImportClass;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.NonPersistent;

namespace PSC_HRM.Module.Win.Controllers.Import.ImportControl
{
    public partial class PMS_Import_ThoiKhoaBieu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session _ses;
        CollectionSource collectionSource;
        ChonChuyenNganhDaoTao _source;
        public PMS_Import_ThoiKhoaBieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyThoiKhoaBieu_DetailView";
        }     
        private void ImportThoiKB_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            QuanLyThoiKhoaBieu _qlyTKB = View.CurrentObject as QuanLyThoiKhoaBieu;
            if (_qlyTKB != null)
            {
                _obs = Application.CreateObjectSpace();
                _ses = ((XPObjectSpace)_obs).Session;
                collectionSource = new CollectionSource(_obs, typeof(ChonChuyenNganhDaoTao));

                _source = new ChonChuyenNganhDaoTao(_ses);
                e.View = Application.CreateDetailView(_obs, _source);
            }         
        }
        private void ImportThoiKB_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            try
            {
                QuanLyThoiKhoaBieu OidQuanLy = View.CurrentObject as QuanLyThoiKhoaBieu;
                if (OidQuanLy != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa dữ liệu cũ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    //Nếu bấm Yes thì xóa hết dữ liệu cũ sau đó Import dữ liệu mới. Nếu bấm No vẫn Import
                    if (dialogResult == DialogResult.Yes)
                    {
                        SqlParameter[] pXoa = new SqlParameter[1];
                        pXoa[0] = new SqlParameter("@ThoiKhoaBieu", OidQuanLy.Oid);
                        DataProvider.ExecuteNonQuery("spd_PMS_XoaDuLieuThoiKhoaBieu", CommandType.StoredProcedure, pXoa);
                    }
                    _obs = View.ObjectSpace;
                    View.ObjectSpace.CommitChanges();
                    if (_source.ChuyenNganhDaoTao != null)
                    {
                        Imp_ThoiKhoaBieu.XuLy(_obs, OidQuanLy, _source.ChuyenNganhDaoTao.TenChuyenNganh);
                    }
                    else
                    {
                        MessageBox.Show("Thông Báo", "Phải Chọn Chuyên Ngành Đào Tạo");
                    }
                    _obs.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
