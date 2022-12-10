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
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data;
using System.Data.SqlClient;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class CapNhat_DaTinhTien_Controller : ViewController
    {
        public CapNhat_DaTinhTien_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "BangChotThuLao_DetailView";
        }
        void CapNhat_DaTinhTien_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "UFM")
            {
                btnCapNhat.Active["TruyCap"] = true;
                btnCapNhatNCKH.Active["TruyCap"] = true;
            }
            else
            {
                btnCapNhat.Active["TruyCap"] = false;
                btnCapNhatNCKH.Active["TruyCap"] = false;
            }
        }
        private void btnCapNhat_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BangChotThuLao _bangChot = View.CurrentObject as BangChotThuLao;
            if (_bangChot != null)
            {
                using (DialogUtil.AutoWait("Đang cập nhật!"))
                {
                    if(_bangChot.ListThongTinBangChot_Moi.Count>0)
                    {
                        foreach(var item in _bangChot.ListThongTinBangChot_Moi)
                        {
                            SqlParameter[] pCapNhat = new SqlParameter[2];
                            pCapNhat[0] = new SqlParameter("@ThongTinBangChot", item.Oid);
                            pCapNhat[1] = new SqlParameter("@DaTinhTien", item.DaChiTien);
                            DataProvider.ExecuteNonQuery("spd_PMS_CapNhat_DaTinhTien_TTBC", CommandType.StoredProcedure, pCapNhat);
                        }
                    }
                    DialogUtil.ShowInfo("Đã cập nhật xong");
                    View.ObjectSpace.Refresh();//Load lại view nhìn
                }
            }
        }

        private void btnCapNhatNCKH_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BangChotThuLao _bangChot = View.CurrentObject as BangChotThuLao;
            if (_bangChot != null)
            {
                using (DialogUtil.AutoWait("Đang cập nhật!"))
                {

                    SqlParameter[] pCapNhat = new SqlParameter[1];
                    pCapNhat[0] = new SqlParameter("@BangChotThuLao", _bangChot.Oid);
                    DataProvider.ExecuteNonQuery("spd_PMS_CapNhat_NghienCuuKhoaHoc", CommandType.StoredProcedure, pCapNhat);
                    DialogUtil.ShowInfo("Đã cập nhật xong");
                    View.ObjectSpace.Refresh();//Load lại view nhìn
                }
            }
        }

    }
}
