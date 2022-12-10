using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhGia;
using DevExpress.Utils;
using PSC_HRM.Module.ChamCong;
using System.Windows.Forms;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.Controllers;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.PMS.NghiepVu.ThanhToan;
using ERP.Module.Win.Controllers.Import.ImportClass;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;
using PSC_HRM.Module.PMS.NonPersistent;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_ImportTKBGiangDay_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session _Session;
        Chon_HeDaoTao_BacDaoTao_Import _source;
        CollectionSource collectionSource;
        ThoiKhoaBieu_KhoiLuongGiangDay TKB;
        public PMS_ImportTKBGiangDay_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ThoiKhoaBieu_KhoiLuongGiangDay_DetailView";
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        private void pop_Import_TKB_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            _Session = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(Chon_HeDaoTao_BacDaoTao_Import));
            _source = new Chon_HeDaoTao_BacDaoTao_Import(_Session);
            e.View = Application.CreateDetailView(_obs, _source);
        }

        private void pop_Import_TKB_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            TKB = View.CurrentObject as ThoiKhoaBieu_KhoiLuongGiangDay;
            if (TKB != null)
            {
                View.ObjectSpace.CommitChanges();
                string TenChuongTrinh = String.Format(" {0} {1} {2}", _source.BoPhan != null ? _source.BoPhan.TenBoPhan : "", _source.BacDaoTao != null ? _source.BacDaoTao.TenBacDaoTao : "", _source.HeDaoTao != null ? " - " + _source.HeDaoTao.TenHeDaoTao : "");
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa dữ liệu cũ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlParameter[] pXoa = new SqlParameter[5];
                    pXoa[0] = new SqlParameter("@TKB", TKB.Oid);
                    pXoa[1] = new SqlParameter("@BoMonQuanLyGiangDay", _source.BoPhan != null ? _source.BoPhan.Oid : Guid.Empty);
                    pXoa[2] = new SqlParameter("@BacDaoTao", _source.BacDaoTao != null ? _source.BacDaoTao.Oid : Guid.Empty);
                    pXoa[3] = new SqlParameter("@HeDaoTao", _source.HeDaoTao != null ? _source.HeDaoTao.Oid : Guid.Empty);

                    pXoa[4] = new SqlParameter("@TKB", TKB.Oid);
                    DataProvider.ExecuteNonQuery("spd_PMS_XoaDuLieuTKB", CommandType.StoredProcedure, pXoa);
                }
                using (DialogUtil.AutoWait("Đang import thời khóa biểu" + TenChuongTrinh))
                {
                    _obs = View.ObjectSpace;
                    Imp_ThoiKhoaBieu.XuLy(_obs, TKB.Oid, _source.BoPhan != null ? _source.BoPhan.Oid : Guid.Empty, _source.BacDaoTao != null ? _source.BacDaoTao.Oid : Guid.Empty, _source.HeDaoTao != null ? _source.HeDaoTao.Oid : Guid.Empty);
                    _obs.Refresh();
                }
            }
        }

        void PMS_ImportTKBGiangDay_Controller_Activated(object sender, System.EventArgs e)
        {
            pop_Import_TKB.Active["TruyCap"] = false;
            if (HamDungChung.CurrentUser().UserName != "psc")
            {
                btImportTKB.Active["TruyCap"] = false;
            }
        }
        private void btImportTKB_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            TKB = View.CurrentObject as ThoiKhoaBieu_KhoiLuongGiangDay;
            if (TKB != null)
            {
                if (TKB.BangChotThuLao == null || TKB.Khoa)
                {
                    View.ObjectSpace.CommitChanges();
                    //DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa dữ liệu cũ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    //if (dialogResult == DialogResult.Yes)
                    //{
                    //    SqlParameter[] pXoa = new SqlParameter[1];
                    //    pXoa[0] = new SqlParameter("@TKB", TKB.Oid);
                    //    DataProvider.ExecuteNonQuery("spd_PMS_XoaTBK", CommandType.StoredProcedure, pXoa);
                    //}
                    using (DialogUtil.AutoWait("Đang import thời khóa biểu"))
                    {
                        _obs = View.ObjectSpace;
                        Imp_ThoiKhoaBieu.XuLy(_obs, TKB.Oid, Guid.Empty, Guid.Empty, Guid.Empty);
                        _obs.Refresh();
                    }
                }
                else
                    XtraMessageBox.Show("Dữ liệu đã chốt - Không thể import", "Thông báo!");
            }
        }
    }
}