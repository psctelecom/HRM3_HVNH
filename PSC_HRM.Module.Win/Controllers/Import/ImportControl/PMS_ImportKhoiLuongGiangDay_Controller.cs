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

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_ImportKhoiLuongGiangDay_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session _Session;
        Chon_HeDaoTao_BacDaoTao_Import _source;
        CollectionSource collectionSource;
        KhoiLuongGiangDay OidQuanLy;
        public PMS_ImportKhoiLuongGiangDay_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "KhoiLuongGiangDay_DetailView";
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            DetailView _DetailView = View as DetailView;
            if (_DetailView != null)
            {
                if (_DetailView.ToString().Contains("KhoiLuongGiangDay_DetailView"))
                {
                    OidQuanLy = View.CurrentObject as KhoiLuongGiangDay;
                    if (OidQuanLy != null)
                    {
                        if (TruongConfig.MaTruong == "VHU" || TruongConfig.MaTruong == "UFM")
                        {
                            OidQuanLy.SuDungListMoi = true;
                            string sql = "Update KhoiLuongGiangDay set SuDungListMoi=1";
                            DataProvider.ExecuteNonQuery(sql, CommandType.Text);
                        }
                    }
                }
            }
        }
        private void PMS_ImportKhoiLuongGiangDay_Controller_Activated(object sender, System.EventArgs e)
        {
            string MaTruong = TruongConfig.MaTruong;
            if (MaTruong == "HVNH" || MaTruong == "DNU" || MaTruong == "UFM" || MaTruong == "HUFLIT")
                btImportKhoiLuong.Active["TruyCap"] = false;
            if (MaTruong != "UFM")
            {
                pop_Import_KhoiLuongGiangDay.Active["TruyCap"] = false;
                btImport_khoiLuongChung.Active["TruyCap"] = false;
            }
        }
        private void btImportKhoiLuong_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Import_KhoiLuongGiangDay();
        }

        private void pop_Import_KhoiLuongGiangDay_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            _obs = View.ObjectSpace;
            if (TruongConfig.MaTruong == "UFM")
            {
                SqlParameter[] pSave = new SqlParameter[6];
                pSave[0] = new SqlParameter("@ThongTinTruong", OidQuanLy.ThongTinTruong.Oid);
                pSave[1] = new SqlParameter("@NamHoc", OidQuanLy.NamHoc.Oid);
                pSave[2] = new SqlParameter("@HocKy", OidQuanLy.HocKy != null ? OidQuanLy.HocKy.Oid : Guid.Empty);
                pSave[3] = new SqlParameter("@KyTinhPMS", OidQuanLy.KyTinhPMS != null ? OidQuanLy.KyTinhPMS.Oid : Guid.Empty);
                pSave[4] = new SqlParameter("@BacDaoTao", OidQuanLy.BacDaoTao != null ? OidQuanLy.BacDaoTao.Oid : Guid.Empty);
                pSave[5] = new SqlParameter("@KhoiLuongGiangDay", OidQuanLy.Oid);
                DataProvider.GetValueFromDatabase("spd_PMS_Save_KhoiLuongGiangDay", CommandType.StoredProcedure, pSave);
            }
            else
            {
                View.ObjectSpace.CommitChanges();
            }
            Import_KhoiLuongGiangDay();
            //if (TruongConfig.MaTruong == "UFM")
            //{
            //    SqlParameter[] pImport = new SqlParameter[3];
            //    pImport[0] = new SqlParameter("@KhoiLuongGiangDay", OidQuanLy.Oid);
            //    pImport[1] = new SqlParameter("@BacDaoTao", _source.BacDaoTao != null ? _source.BacDaoTao.Oid : Guid.Empty);
            //    pImport[2] = new SqlParameter("@HeDaoTao", _source.HeDaoTao != null ? _source.HeDaoTao.Oid : Guid.Empty);
            //    DataProvider.GetValueFromDatabase("spd_PMS_Update_ChiTietKhoiLuongGiangDay_Moi", CommandType.StoredProcedure, pImport);
            //}
            using (DialogUtil.AutoWait("Đang load dữ liệu khối lượng giảng dạy"))
            {
                _obs.Refresh();
            }
        }

        private void pop_Import_KhoiLuongGiangDay_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            _Session = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(Chon_HeDaoTao_BacDaoTao_Import));
            _source = new Chon_HeDaoTao_BacDaoTao_Import(_Session);
            e.View = Application.CreateDetailView(_obs, _source);
        }
        void Import_KhoiLuongGiangDay()
        {
            //try
            //{
                //OidQuanLy = View.CurrentObject as KhoiLuongGiangDay;
                if (OidQuanLy != null)
                {
                    _obs = Application.CreateObjectSpace();
                    string MaTruong = TruongConfig.MaTruong;
                    if (OidQuanLy.BangChotThuLao == null)
                    {
                        {
                            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa dữ liệu cũ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dialogResult == DialogResult.Yes)
                            {
                                if (MaTruong == "UFM")
                                {
                                    SqlParameter[] pXoa = new SqlParameter[3];
                                    pXoa[0] = new SqlParameter("@KhoiLuongGiangDay", OidQuanLy.Oid);
                                    pXoa[1] = new SqlParameter("@BacDaoTao", _source.BacDaoTao.Oid);
                                    pXoa[2] = new SqlParameter("@HeDaoTao", _source.HeDaoTao.Oid);
                                    DataProvider.ExecuteNonQuery("spd_PMS_XoaDuLieuChinhQuyCD_DH", CommandType.StoredProcedure, pXoa);
                                }
                                else
                                {
                                    SqlParameter[] pXoa = new SqlParameter[1];
                                    pXoa[0] = new SqlParameter("@KhoiLuongGiangDay", OidQuanLy.Oid);
                                    DataProvider.ExecuteNonQuery("spd_PMS_XoaDuLieuChinhQuyCD_DH", CommandType.StoredProcedure, pXoa);
                                }
                            }
                            using (DialogUtil.AutoWait("Đang import dữ liệu khối lượng giảng dạy"))
                            {
                                
                                if (MaTruong == "HVNH")
                                {
                                    Imp_KhoiLuongGiangDay_HVNH.XuLy(_obs, OidQuanLy);
                                }
                                else
                                {
                                    if(_source != null)
                                    {
                                         Imp_KhoiLuongGiangDay.XuLy(_obs, OidQuanLy, _source.BacDaoTao != null ? _source.BacDaoTao.Oid : Guid.Empty, _source.HeDaoTao != null ? _source.HeDaoTao.Oid : Guid.Empty);
                                    }
                                    else
                                    {
                                        Imp_KhoiLuongGiangDay.XuLy(_obs, OidQuanLy, Guid.Empty, Guid.Empty);

                                    }
                                }
                            }
                        }
                    }
                    else
                        XtraMessageBox.Show("Dữ liệu đã chốt - Không thể import", "Thông báo!");
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btImport_khoiLuongChung_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (TruongConfig.MaTruong == "UFM")
            {
                _obs = Application.CreateObjectSpace();
                Imp_LopHocPhan_NhieuGiangVien.XuLy(_obs, new Guid(OidQuanLy.Oid.ToString()));
            }
        }
    }
}