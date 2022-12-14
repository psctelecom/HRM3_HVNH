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
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.PMS.GioChuan;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class Clone_GioChuanPMS_Controller : ViewController
    {
        IObjectSpace _obs = null;
        QuanLyGioChuan qlyHeSo;
        CollectionSource collectionSource;
        ThongTinClone _source;
        public Clone_GioChuanPMS_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyGioChuan_DetailView";
        }

        void Clone_GioChuanPMS_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "UEL")
                popClone.Active["TruyCap"] = false;
        }
        //private void btClone_Execute(object sender, SimpleActionExecuteEventArgs e)
        //{
        //    qlyHeSo = View.CurrentObject as QuanLyGioChuan;
        //    if (qlyHeSo != null)
        //    {
        //        using (DialogUtil.AutoWait("Hệ thống đang qui đổi khối lượng giảng dạy"))
        //        {
        //            //SqlParameter[] pQuyDoi = new SqlParameter[1];
        //            //pQuyDoi[0] = new SqlParameter("@KhoiLuongGiangDay", KhoiLuong.Oid);
        //            //DataProvider.GetValueFromDatabase("spd_PMS_QuyDoiKhoiLuongGiangDay", CommandType.StoredProcedure, pQuyDoi);
        //            //View.ObjectSpace.Refresh();
        //            //XtraMessageBox.Show("Qui đổi dữ liệu thành công!", "Thông báo");
        //        }
        //    }
        //}

        private void popClone_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(ThongTinClone));

            collectionSource = new CollectionSource(_obs, typeof(ThongTinClone));
            _source = new ThongTinClone(ses);
            _source.An = false;
            e.View = Application.CreateDetailView(_obs, _source);
        }
        private void popClone_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            qlyHeSo = View.CurrentObject as QuanLyGioChuan;
            if (qlyHeSo != null)
            {
                View.ObjectSpace.CommitChanges();
                if (TruongConfig.MaTruong == "NEU")
                {
                    SqlParameter[] pDongBo = new SqlParameter[5];
                    pDongBo[0] = new SqlParameter("@ThongTinTruong", _source.ThongTinTruong.Oid);
                    pDongBo[1] = new SqlParameter("@NamHoc", _source.NamHoc.Oid);
                    pDongBo[2] = new SqlParameter("@Loai", "QuanLyGioChuan");
                    pDongBo[3] = new SqlParameter("@Oid", qlyHeSo.Oid);
                    pDongBo[4] = new SqlParameter("@HocKy", _source.HocKy.Oid);
                    object kq = DataProvider.GetValueFromDatabase("spd_Clone_GioChuanPMS", CommandType.StoredProcedure, pDongBo);
                    if (kq != null)
                        XtraMessageBox.Show(kq.ToString(), "Thông báo!");
                }

                if (TruongConfig.MaTruong == "UFM")
                {
                    SqlParameter[] pDongBo = new SqlParameter[5];
                    pDongBo[0] = new SqlParameter("@ThongTinTruong", _source.ThongTinTruong.Oid);
                    pDongBo[1] = new SqlParameter("@NamHoc", _source.NamHoc.Oid);
                    pDongBo[2] = new SqlParameter("@Loai", "QuanLyGioChuan");
                    pDongBo[3] = new SqlParameter("@Oid", qlyHeSo.Oid);
                    pDongBo[4] = new SqlParameter("@HocKy", _source.HocKy != null ? _source.HocKy.Oid : Guid.Empty);
                    object kq = DataProvider.GetValueFromDatabase("spd_Clone_GioChuanPMS", CommandType.StoredProcedure, pDongBo);
                    if (kq != null)
                        XtraMessageBox.Show(kq.ToString(), "Thông báo!");
                }
            }
        }
    }
}