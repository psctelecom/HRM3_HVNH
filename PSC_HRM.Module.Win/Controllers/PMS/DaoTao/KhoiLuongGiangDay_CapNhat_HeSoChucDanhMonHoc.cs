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
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Win.Controllers.PMS.DaoTao
{
    public partial class KhoiLuongGiangDay_CapNhat_HeSoChucDanhMonHoc : ViewController
    {
        IObjectSpace _obs;
        Session ses;
        CollectionSource collectionSource;
        ChonKhoiLuongGiangDay_HeSoChucDanhMonHoc _source;  
        public KhoiLuongGiangDay_CapNhat_HeSoChucDanhMonHoc()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "KhoiLuongGiangDay_DetailView";
        }

        void KhoiLuongGiangDay_CapNhat_HeSoChucDanhMonHoc_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "HUFLIT")
            {
                popCapNhat.Active["TruyCap"] = true;
                btnMoKhoa_KhoiLuong.Active["TruyCap"] = true;
            }
            else
            {
                popCapNhat.Active["TruyCap"] = false;
                btnMoKhoa_KhoiLuong.Active["TruyCap"] = false;
            }
        }
        //Hiện thông tin 
        private void popCapNhat_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(dsChiTietKhoiLuong_NhanVien));
            KhoiLuongGiangDay obj = View.CurrentObject as KhoiLuongGiangDay;
            if (obj != null && obj.Khoa != true)
            {
                using (DialogUtil.AutoWait("Load danh sách giảng viên"))
                {
                    collectionSource = new CollectionSource(_obs, typeof(ChonKhoiLuongGiangDay_HeSoChucDanhMonHoc));
                    _source = new ChonKhoiLuongGiangDay_HeSoChucDanhMonHoc(ses);
                    _source.NamHoc = obj.NamHoc;
                    _source.HocKy = obj.HocKy;
                    _source.KhoiLuongGiangDay = obj.Oid;
                    _source.UpdatelistKhoiLuong();
                    e.View = Application.CreateDetailView(_obs, _source);

                }
            }
            else XtraMessageBox.Show("Đã khóa khối lượng không thể cập nhật", "Thông báo");
        }
        //Khi bấm thực thi OK
        private void popCapNhat_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            _obs = View.ObjectSpace;
            Session ses = ((XPObjectSpace)_obs).Session;
            KhoiLuongGiangDay obj = View.CurrentObject as KhoiLuongGiangDay;
            if (obj != null)
            {
                using (DialogUtil.AutoWait("Đang cập nhật dữ liệu"))
                {
                    List<dsChiTietKhoiLuong_NhanVien> list = (from d in _source.listKhoiLuong
                                                   where d.Chon == true
                                                   select d).ToList();
                    foreach (dsChiTietKhoiLuong_NhanVien item in list)
                    {
                        SqlParameter[] param = new SqlParameter[7];
                        param[0] = new SqlParameter("@KhoiLuongGiangDay", _source.KhoiLuongGiangDay);
                        param[1] = new SqlParameter("@NhanVien", _source.NhanVien.Oid);
                        param[2] = new SqlParameter("@HeSoChucDanhMonHoc", _source.HeSoChucDanhMonHoc.Oid);
                        param[3] = new SqlParameter("@TenBacDaoTao", item.BacDaoTao.ToString()); 
                        param[4] = new SqlParameter("@MaHocPhan", item.MaHocPhan.ToString());
                        param[5] = new SqlParameter("@TenMonHoc", item.TenMonHoc.ToString());
                        param[6] = new SqlParameter("@MaLopHocPhan", item.LopHocPhan.ToString());
                        
                        DataProvider.ExecuteNonQuery("spd_PMS_CapNhatHeSoChucDanhMonHoc", CommandType.StoredProcedure, param);
                        
                    }
                    View.ObjectSpace.Refresh();

                }
            }
        }

        private void btnMoKhoa_KhoiLuong_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            KhoiLuongGiangDay obj = View.CurrentObject as KhoiLuongGiangDay;
            if (obj != null && obj.BangChotThuLao == null)
            {
                bool Khoa = !obj.Khoa;
                string sql = "UPDATE kl SET kl.Khoa = " + Khoa.GetHashCode();
                sql += " FROM dbo.KhoiLuongGiangDay kl";
                sql += " WHERE kl.Oid = '" + obj.Oid +"'";

                DataProvider.ExecuteNonQuery(sql,CommandType.Text);
                View.ObjectSpace.Refresh();
                        
            }
            else if (obj != null && obj.BangChotThuLao != null)
            {
                XtraMessageBox.Show("Đã có khối lượng không thể mở khóa", "Thông báo");
            }
        }
    }
}
