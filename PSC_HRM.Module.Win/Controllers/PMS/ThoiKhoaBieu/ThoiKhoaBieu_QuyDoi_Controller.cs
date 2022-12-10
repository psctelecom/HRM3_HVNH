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
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using DevExpress.Xpo;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Win.Controllers.PMS.ThoiKhoaBieu
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ThoiKhoaBieu_QuyDoi_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session _Session;
        ThoiKhoaBieu_KhoiLuongGiangDay TKB;
        public ThoiKhoaBieu_QuyDoi_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewId = "ThoiKhoaBieu_KhoiLuongGiangDay_DetailView";
        }
        private void btQuyDoi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            _Session = ((XPObjectSpace)_obs).Session;
            TKB = View.CurrentObject as ThoiKhoaBieu_KhoiLuongGiangDay;
            if (TKB != null)
            {
                if (!TKB.Khoa)
                {
                    using (DialogUtil.AutoWait("Đang quy đổi dữ liệu"))
                    {
                        SqlParameter[] pQuyDoi = new SqlParameter[1];
                        pQuyDoi[0] = new SqlParameter("@ThoiKhoaBieu", TKB.Oid);
                        //DataProvider.ExecuteNonQuery("spd_PMS_QuyDoiGio_ThoiKhoaBieu", CommandType.StoredProcedure, pQuyDoi);
                        object kq = DataProvider.GetValueFromDatabase("spd_PMS_QuyDoiGio_ThoiKhoaBieu", CommandType.StoredProcedure, pQuyDoi);
                        if (kq != null)
                            XtraMessageBox.Show(kq.ToString(), "Thông báo!");
                        View.ObjectSpace.Refresh();
                    }
                }
                else
                    System.Windows.Forms.MessageBox.Show("Đã khóa dữ liệu - Không thể quy đổi", "Thông báo");
            }
        }
    }
}
