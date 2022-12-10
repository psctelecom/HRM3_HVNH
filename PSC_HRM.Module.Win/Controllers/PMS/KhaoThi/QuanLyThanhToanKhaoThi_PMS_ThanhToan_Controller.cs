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
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using System.Data;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
   public partial class QuanLyThanhToanKhaoThi_PMS_ThanhToan_Controller : ViewController
    {
        public QuanLyThanhToanKhaoThi_PMS_ThanhToan_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyThanhToanKhaoThi_DetailView";
        }
        //Thực thi 
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            QuanLyThanhToanKhaoThi qly = View.CurrentObject as QuanLyThanhToanKhaoThi;
            if (qly != null)
            {
                using (DialogUtil.Wait("Đang đồng bộ dữ liệu", "Thông báo"))
                {

                    SqlParameter[] pDongBo = new SqlParameter[2];
                    pDongBo[0] = new SqlParameter("@QuanLyThanhToanKhaoThi", qly.Oid);
                    pDongBo[1] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);

                    int kq = DataProvider.ExecuteNonQuery("spd_PMS_QuanLyThanhToanKhaoThi_TinhThanhToan", CommandType.StoredProcedure, pDongBo);
                    if (kq != -1)
                        XtraMessageBox.Show("Tính thanh toán thành công");
                    else
                        XtraMessageBox.Show("Tính thanh toán thành công");

                    View.ObjectSpace.Refresh();
                }
                View.ObjectSpace.Refresh(); 
            }
        }
        
        
    }
}
