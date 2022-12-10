using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PSC_HRM.Module.Win.Controllers.PMS.UEL
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class KhoiLuongGiangDay_Xoa_Controller : ViewController
    {
        private KhoiLuongGiangDay_UEL _qly;
        private IObjectSpace _obs;
        private Session _session;
        public KhoiLuongGiangDay_Xoa_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "KhoiLuongGiangDay_UEL_ListChiTietKhoiLuongGiangDay_UEL_ListView";
        }
    

        private void btnXoaKhoiLuongGiangDay_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _qly = ((DetailView)((View.ObjectSpace).Owner)).CurrentObject as KhoiLuongGiangDay_UEL;
            _obs = Application.CreateObjectSpace();
            _session = ((XPObjectSpace)_obs).Session;
            if (_qly != null)
            {
                if (XtraMessageBox.Show("Bạn có chắc muốn xóa những dòng đã chọn? ", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (DialogUtil.Wait("Đang xóa dữ liệu đồng bộ", "Thông báo"))
                    {
                        string s = "", oid = "";
                        foreach (ChiTietKhoiLuongGiangDay_UEL item in View.SelectedObjects)
                        {
                            if(item.DaLayDuLieu == false)
                            {
                                oid += item.Oid + ";";
                                s += item.Oid_ChiTietThanhTraGiangDay + ";";  
                            }
                        }
                      
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@OidStrKhoiLuong", oid);
                        param[1] = new SqlParameter("@OidStrThanhTra", s);
                        param[2] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName.ToString());

                        DataProvider.ExecuteNonQuery("spd_PMS_KhoiLuongGiangDay_Xoa_UEL", System.Data.CommandType.StoredProcedure, param);
                        (View as DevExpress.ExpressApp.ListView).CollectionSource.Reload();
                        XtraMessageBox.Show("Xóa thành công những dòng chưa lấy dữ liệu","Thông tin");                        
                    }
                }    
            }
                      
        }
    }
}
