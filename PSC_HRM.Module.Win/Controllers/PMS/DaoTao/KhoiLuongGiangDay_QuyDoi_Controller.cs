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

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class KhoiLuongGiangDay_QuyDoi_Controller : ViewController
    {
        IObjectSpace _obs = null;
        KhoiLuongGiangDay KhoiLuong;
        public KhoiLuongGiangDay_QuyDoi_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "KhoiLuongGiangDay_DetailView";
        }

        void KhoiLuongGiangDay_QuyDoi_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "HVNH")
                btQuiDoi.Active["TruyCap"] = false;
        }
        private void btQuiDoi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            KhoiLuong = View.CurrentObject as KhoiLuongGiangDay;
            if (KhoiLuong != null)
            {
                if (KhoiLuong.BangChotThuLao == null)
                {
                    if (!KhoiLuong.Khoa)
                    {
                        using (DialogUtil.AutoWait("Đang qui đổi khối lượng giảng dạy"))
                        {
                            SqlParameter[] pQuyDoi = new SqlParameter[1];
                            pQuyDoi[0] = new SqlParameter("@KhoiLuongGiangDay", KhoiLuong.Oid);
                            DataProvider.GetValueFromDatabase("spd_PMS_QuyDoiKhoiLuongGiangDay", CommandType.StoredProcedure, pQuyDoi);
                            View.ObjectSpace.Refresh();
                            XtraMessageBox.Show("Qui đổi dữ liệu thành công!", "Thông báo");
                        }
                    }
                    else
                        XtraMessageBox.Show("Đã khóa dữ liệu - không thể quy đổi!", "Thông báo");
                }
                else
                    XtraMessageBox.Show("Đã chốt khối lượng - không thể quy đổi!", "Thông báo");
            }
        }
        
    }
}