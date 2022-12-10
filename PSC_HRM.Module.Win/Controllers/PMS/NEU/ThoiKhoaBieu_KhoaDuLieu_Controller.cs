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
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.NonPersistentObjects;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class ThoiKhoaBieu_KhoaDuLieu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        QLyTKB_KhoaDuLieu _source;
        ThoiKhoaBieu_KhoiLuongGiangDay _TKB;
        Session ses;
        public ThoiKhoaBieu_KhoaDuLieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ThoiKhoaBieu_KhoiLuongGiangDay_DetailView";
        }
        //Khi bấm nút OK 
        private void popKhoaTKB_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait("Đang đồng bộ dữ liệu từ UIS"))
            {
                if (_TKB != null)
                {
                    string listOidChiTiet = "";
                    if (_source != null)
                    {


                        List<dsChiTietTKB_KhoaDuLieu> ds = (from d in _source.listTKB
                                                       where d.Chon
                                                       select d).ToList();
                        foreach (dsChiTietTKB_KhoaDuLieu item in ds)
                        {
                            listOidChiTiet += " Union All select '" + item.OidTKB_ChiTietKhoiLuongGiangDay + "' as OidChiTietKhoiLuong";
                        }
                    }

                    #region
                    SqlCommand cmd = new SqlCommand("spd_PMS_ThoiKhoaBieu_KhoaDuLieu", DataProvider.GetConnection());
                    cmd.CommandTimeout = 1800;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@listOidChiTiet", listOidChiTiet != "" ? listOidChiTiet.Substring(11) : "");
                    cmd.Parameters.AddWithValue("@ThoiKhoaBieu", _TKB.Oid);
                    cmd.Parameters.AddWithValue("@Khoa", _source.Khoa);
                    cmd.Parameters.AddWithValue("@MoKhoa", _source.MoKhoa);
                    cmd.Parameters.AddWithValue("@User", HamDungChung.CurrentUser().UserName);
                    cmd.ExecuteNonQuery();
                    View.ObjectSpace.Refresh();
                    #endregion
                }
            }

        }
        //Khi bấm nút show Popup 
        private void popKhoaTKB_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _TKB = View.CurrentObject as ThoiKhoaBieu_KhoiLuongGiangDay;
            if (_TKB.BangChotThuLao != null)
            {
                XtraMessageBox.Show("Đã chốt dữ liệu - Không thể đồng bộ", "Thông báo");
                return;
            }
            else
                using (DialogUtil.AutoWait("Đang kiểm tra dữ liệu"))
                {
                    _obs = Application.CreateObjectSpace();
                    ses = ((XPObjectSpace)_obs).Session;
                    collectionSource = new CollectionSource(_obs, typeof(dsChiTietTKB_KhoaDuLieu));

                    if (_TKB != null)
                        if (_TKB.BangChotThuLao != null)
                        {
                            XtraMessageBox.Show("Đã chốt khối lượng - không thể đồng bộ!");
                            return;
                        }
                        else
                        {
                            if (_TKB.HocKy == null)
                            {
                                XtraMessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo");
                                return;
                            }
                            using (DialogUtil.AutoWait("Load danh sách giảng viên"))
                            {
                                collectionSource = new CollectionSource(_obs, typeof(QLyTKB_KhoaDuLieu));
                                _source = new QLyTKB_KhoaDuLieu(ses);
                                e.View = Application.CreateDetailView(_obs, _source);
                                _source.NamHoc = _TKB.NamHoc;
                                _source.HocKy = _TKB.HocKy;
                                _source.OidQuanLyTKB = _TKB.Oid;
                                _source.Load();
                            }
                        }
                }
            
        }
        
    }
}
