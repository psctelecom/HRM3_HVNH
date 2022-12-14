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

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class KhoiLuongGiangDay_DongBoDuLieu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        ChonNhanVien _source;
        public KhoiLuongGiangDay_DongBoDuLieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "KhoiLuongGiangDay_DetailView";
        }

        void KhoiLuongGiangDay_DongBoDuLieu_Controller_Activated(object sender, System.EventArgs e)
        {
            //if (TruongConfig.MaTruong == "HVNH")
            //    popDongBo.Active["TruyCap"] = false;
        }

        private void popDongBo_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(dsThongTinNhanVien));

            KhoiLuongGiangDay obj = View.CurrentObject as KhoiLuongGiangDay;
            if (obj != null)
                if (obj.BangChotThuLao != null)
                {
                    XtraMessageBox.Show("Đã chốt khối lượng - không thể đồng bộ!");
                    return;
                }
                else
                {
                    if (TruongConfig.MaTruong == "HUFLIT")
                    {
                        if (obj.HocKy == null)
                        {
                            XtraMessageBox.Show("Vui lòng chọn học kỳ!","Thông báo");
                            return;
                        }
                    }
                    using (DialogUtil.AutoWait("Load danh sách giảng viên"))
                    {
                        collectionSource = new CollectionSource(_obs, typeof(ChonNhanVien));
                        _source = new ChonNhanVien(ses);
                        e.View = Application.CreateDetailView(_obs, _source);
                    }
                }
        }
        private void popDongBo_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            _obs = View.ObjectSpace;
            Session ses = ((XPObjectSpace)_obs).Session;
            KhoiLuongGiangDay obj = View.CurrentObject as KhoiLuongGiangDay;
            if (obj != null)
            {
                if (TruongConfig.MaTruong == "HUFLIT")
                {
                    SqlCommand cmd = new SqlCommand("spd_PMS_Save_KhoiLuongGiangDay_Detail", DataProvider.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 1800;
                    cmd.Parameters.AddWithValue("@Oid",obj.Oid);
                    cmd.Parameters.AddWithValue("@ThongTinTruong",obj.ThongTinTruong.Oid);
                    cmd.Parameters.AddWithValue("@NamHoc",obj.NamHoc!=null?obj.NamHoc.Oid:Guid.Empty);
                    cmd.Parameters.AddWithValue("@HocKy",obj.HocKy!=null?obj.HocKy.Oid:Guid.Empty);
                    cmd.Parameters.AddWithValue("@KyTinhPMS",obj.KyTinhPMS!=null?obj.KyTinhPMS.Oid:Guid.Empty);
                    cmd.Parameters.AddWithValue("@BacDaoTao", obj.BacDaoTao != null ? obj.BacDaoTao.Oid : Guid.Empty);
                    cmd.Parameters.AddWithValue("@SuDungListMoi", obj.SuDungListMoi);
                    cmd.ExecuteNonQuery();
                }
                //else
                //    View.ObjectSpace.CommitChanges();
                string listNhanVien = "";
                using (DialogUtil.AutoWait("Đang đồng bộ dữ liệu"))
                {
                    if (_source.TatCa)
                        listNhanVien = "";
                    else
                        foreach (dsThongTinNhanVien item in _source.listNhanVien)
                        {
                            listNhanVien += item.OidThongTinNhanVien.ToString() + ";";
                        }

                    if (obj.ListChiTietKhoiLuongGiangDay != null)
                    {

                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@KhoiLuongGiangDay", obj.Oid);
                        param[1] = new SqlParameter("@listNhanVien", listNhanVien);
                        param[2] = new SqlParameter("@TatCa", _source.TatCa);
                        DataProvider.ExecuteNonQuery("spd_PMS_XoaDuLieu_KhoiLuongGiangDay", CommandType.StoredProcedure, param);
                    }

                    SqlParameter[] pDongBo = new SqlParameter[3];
                    pDongBo[0] = new SqlParameter("@KhoiLuongGiangDay", obj.Oid);
                    pDongBo[1] = new SqlParameter("@listNhanVien", listNhanVien);
                    pDongBo[2] = new SqlParameter("@TatCa", _source.TatCa.GetHashCode());
                    object kq = DataProvider.GetValueFromDatabase("spd_PMS_DongBoDuLieu_KhoiLuongGiangDay", CommandType.StoredProcedure, pDongBo);
                    if (kq != null)
                        XtraMessageBox.Show("Đồng bộ thành công " + kq.ToString() + " dòng dữ liệu");
                    else
                        XtraMessageBox.Show("Đồng bộ không thành công thành công ");
                    if (TruongConfig.MaTruong != "HVNH")
                        View.ObjectSpace.Refresh();

                }
                if(TruongConfig.MaTruong=="HVNH")
                {
                    using (DialogUtil.AutoWait("Đang qui đổi khối lượng giảng dạy"))
                    {
                        SqlParameter[] pQuyDoi = new SqlParameter[1];
                        pQuyDoi[0] = new SqlParameter("@KhoiLuongGiangDay", obj.Oid);
                        DataProvider.GetValueFromDatabase("spd_PMS_QuyDoiKhoiLuongGiangDay", CommandType.StoredProcedure, pQuyDoi);
                        View.ObjectSpace.Refresh();
                        XtraMessageBox.Show("Qui đổi dữ liệu thành công!", "Thông báo");
                    }
                }
            }
        }
    }
}