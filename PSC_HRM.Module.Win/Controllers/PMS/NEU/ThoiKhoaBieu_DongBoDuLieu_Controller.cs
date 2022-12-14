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

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class ThoiKhoaBieu_DongBoDuLieu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        ChonNhanVien _source;
        ThoiKhoaBieu_KhoiLuongGiangDay _TKB;
        Session ses;
        public ThoiKhoaBieu_DongBoDuLieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ThoiKhoaBieu_KhoiLuongGiangDay_DetailView";
        }

        void ThoiKhoaBieu_DongBoDuLieu_Controller_Activated(object sender, System.EventArgs e)
        {
            //if (TruongConfig.MaTruong == "HVNH")
            //    popDongBo.Active["TruyCap"] = false;
        }


        private void popDongBoTK_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
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
                    collectionSource = new CollectionSource(_obs, typeof(dsThongTinNhanVien));

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
                                collectionSource = new CollectionSource(_obs, typeof(ChonNhanVien));
                                _source = new ChonNhanVien(ses);
                                e.View = Application.CreateDetailView(_obs, _source);
                            }
                        }
                }
        }
        private void popDongBoTK_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait("Đang đồng bộ dữ liệu từ UIS"))
            {
                if (_TKB != null)
                {
                    if (_source != null)
                    {
                        string listNhanVien = "";
                        if (!_source.TatCa)
                        {
                            List<dsThongTinNhanVien> ds = (from d in _source.listNhanVien
                                                           where d.Chon
                                                           select d).ToList();
                            foreach (dsThongTinNhanVien item in ds)
                            {
                                listNhanVien += " Union All select '" + item.OidThongTinNhanVien + "' as ThongTinNhanVien";
                            }
                        }

                        #region
                        //SqlCommand cmd = new SqlCommand("spd_PMS_ThoiKhoaBIeu_DongBoDuLieu", DataProvider.GetConnection());
                        //cmd.CommandTimeout = 1800;
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@listNhanVien", listNhanVien != "" ? listNhanVien.Substring(11) : "");
                        //cmd.Parameters.AddWithValue("@ThoiKhoaBieu", _TKB.Oid);
                        //cmd.Parameters.AddWithValue("@TatCa", _source.TatCa);
                        //cmd.Parameters.AddWithValue("@User", HamDungChung.CurrentUser().UserName);
                        //cmd.ExecuteNonQuery();

                        SqlParameter[] param = new SqlParameter[4];
                        param[0] = new SqlParameter("@listNhanVien", listNhanVien != "" ? listNhanVien.Substring(11) : "");
                        param[1] = new SqlParameter("@ThoiKhoaBieu", _TKB.Oid);
                        param[2] = new SqlParameter("@TatCa", _source.TatCa);
                        param[3] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                        object kq = DataProvider.GetValueFromDatabase("spd_PMS_ThoiKhoaBIeu_DongBoDuLieu", CommandType.StoredProcedure, param);
                        if (kq != null)
                            XtraMessageBox.Show(kq.ToString(), "Thông báo!");

                        View.ObjectSpace.Refresh();
                        #endregion
                    }
                }

            }
        }
    }
}