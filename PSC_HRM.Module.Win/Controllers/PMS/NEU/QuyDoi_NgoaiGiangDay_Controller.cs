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
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;

namespace PSC_HRM.Module.Win.Controllers.PMS.NEU
{
    public partial class QuyDoi_NgoaiGiangDay_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session ses;
        KeKhai_CacHoatDong_ThoiKhoaBieu _KeKhai;
        public QuyDoi_NgoaiGiangDay_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "KeKhai_CacHoatDong_ThoiKhoaBieu_DetailView";
        }

        private void btQuyDoi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _KeKhai = View.CurrentObject as KeKhai_CacHoatDong_ThoiKhoaBieu;
            if (_KeKhai != null)
            {
                if (!_KeKhai.Khoa)
                {
                    using (DialogUtil.AutoWait("Đang quy đổi dữ liệu"))
                    {
                        SqlParameter[] pQuyDoi = new SqlParameter[2];
                        pQuyDoi[0] = new SqlParameter("@KeKhai", _KeKhai.Oid);
                        pQuyDoi[1] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName.ToString());
                        object kq = DataProvider.GetValueFromDatabase("spd_PMS_QuyDoi_KeKhai_NgoaiGiangDay", CommandType.StoredProcedure, pQuyDoi);
                        if (kq != null)
                        {
                            XtraMessageBox.Show(kq.ToString(), "Thông báo!");
                            //XtraMessageBox.Show("Quy đổi giờ thành công!", "Thông báo!");
                        }
                        View.ObjectSpace.Refresh();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Bảng kê khai đã khóa - Không thể quy đổi giờ!", "Thông báo!");
                }
            }

        }
    }
}