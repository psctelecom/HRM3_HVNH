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

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class Clone_PMS_Controller : ViewController
    {
        IObjectSpace _obs = null;
        QuanLyHeSo qlyHeSo;
        CollectionSource collectionSource;
        ThongTinClone _source;
        public Clone_PMS_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyHeSo_DetailView";
        }

        void Clone_PMS_Controller_Activated(object sender, System.EventArgs e)
        {
            btClone.Active["TruyCap"] = false;
        }
        private void btClone_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            qlyHeSo = View.CurrentObject as QuanLyHeSo;
            if (qlyHeSo != null)
            {
                using (DialogUtil.AutoWait("Hệ thống đang qui đổi khối lượng giảng dạy"))
                {
                    //SqlParameter[] pQuyDoi = new SqlParameter[1];
                    //pQuyDoi[0] = new SqlParameter("@KhoiLuongGiangDay", KhoiLuong.Oid);
                    //DataProvider.GetValueFromDatabase("spd_PMS_QuyDoiKhoiLuongGiangDay", CommandType.StoredProcedure, pQuyDoi);
                    //View.ObjectSpace.Refresh();
                    //XtraMessageBox.Show("Qui đổi dữ liệu thành công!", "Thông báo");
                }
            }
        }

        private void popClone_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(ThongTinClone));

            collectionSource = new CollectionSource(_obs, typeof(ThongTinClone));
            _source = new ThongTinClone(ses);
            //Đông thêm chỗ điều kiện trường UEL (trước khi thêm mặc định có 1 dòng _source.An = true;)
            if (TruongConfig.MaTruong != "UEL")
            {
                _source.An = true;
            }
            else
            {
                _source.An = false;              
            }
           
            e.View = Application.CreateDetailView(_obs, _source);
        }
        private void popClone_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            qlyHeSo = View.CurrentObject as QuanLyHeSo;
            if (qlyHeSo != null)
            {
                View.ObjectSpace.CommitChanges();
                if (TruongConfig.MaTruong != "HUFLIT" && TruongConfig.MaTruong != "UEL")
                {
                    SqlParameter[] pDongBo = new SqlParameter[4];
                    pDongBo[0] = new SqlParameter("@ThongTinTruong", _source.ThongTinTruong.Oid);
                    pDongBo[1] = new SqlParameter("@NamHoc", _source.NamHoc.Oid);
                    pDongBo[2] = new SqlParameter("@Loai", "QuanLyHeSo");
                    pDongBo[3] = new SqlParameter("@Oid", qlyHeSo.Oid);
                    object kq = DataProvider.GetValueFromDatabase("spd_PMS_Clone", CommandType.StoredProcedure, pDongBo);
                    if (kq != null)
                        XtraMessageBox.Show(kq.ToString(), "Thông báo!");
                }
                else
                {       
                    //Đông thêm điều kiện kiểm tra null năm học và học kỳ
                        SqlParameter[] pDongBo = new SqlParameter[5];
                        pDongBo[0] = new SqlParameter("@ThongTinTruong", _source.ThongTinTruong.Oid);
                        pDongBo[1] = new SqlParameter("@NamHoc",_source.NamHoc!=null ? _source.NamHoc.Oid:Guid.Empty);
                        pDongBo[2] = new SqlParameter("@HocKy",_source.HocKy!=null? _source.HocKy.Oid:Guid.Empty);
                        pDongBo[3] = new SqlParameter("@Loai", "QuanLyHeSo");
                        pDongBo[4] = new SqlParameter("@Oid", qlyHeSo.Oid);
                        object kq = DataProvider.GetValueFromDatabase("spd_PMS_Clone", CommandType.StoredProcedure, pDongBo);
                        if (kq != null)
                            XtraMessageBox.Show(kq.ToString(), "Thông báo!");
                        //SqlCommand cmd = new SqlCommand("spd_PMS_Clone", DataProvider.GetConnection());
                        //cmd.Parameters.AddWithValue("@ThongTinTruong", _source.ThongTinTruong.Oid);
                        //cmd.Parameters.AddWithValue("@NamHoc", _source.NamHoc.Oid);
                        //cmd.Parameters.AddWithValue("@HocKy", _source.HocKy.Oid);
                        //cmd.Parameters.AddWithValue("@Loai", "QuanLyHeSo");
                        //cmd.Parameters.AddWithValue("@Oid", qlyHeSo.Oid);
                        //cmd.ExecuteNonQuery();
                    
                }
                //Hương test cho trường Huflit 
                //SqlParameter[] pDongBo = new SqlParameter[5];
                //pDongBo[0] = new SqlParameter("@ThongTinTruong", _source.ThongTinTruong.Oid);
                //pDongBo[1] = new SqlParameter("@NamHoc", _source.NamHoc.Oid);
                //pDongBo[2] = new SqlParameter("@HocKy", _source.HocKy.Oid);
                //pDongBo[3] = new SqlParameter("@Loai", "QuanLyHeSo");
                //pDongBo[4] = new SqlParameter("@Oid", qlyHeSo.Oid);
                //object kq = DataProvider.GetValueFromDatabase("spd_PMS_Clone", CommandType.StoredProcedure, pDongBo);
                //if (kq != null)
                //    XtraMessageBox.Show(kq.ToString(), "Thông báo!");
            }
        }
    }
}