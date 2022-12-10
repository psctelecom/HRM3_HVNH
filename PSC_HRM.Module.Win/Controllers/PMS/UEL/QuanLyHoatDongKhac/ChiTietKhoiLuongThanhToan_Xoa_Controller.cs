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
using PSC_HRM.Module.PMS.NghiepVu.ThanhToan;
using DevExpress.Xpo;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Xpo;
using System.Data;

namespace PSC_HRM.Module.Win.Controllers.PMS.UEL
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ChiTietKhoiLuongThanhToan_Xoa_Controller : ViewController
    {
        private QuanLyHoatDongKhac _qly;
        private IObjectSpace _obs;
        private Session _session;
        public ChiTietKhoiLuongThanhToan_Xoa_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            RegisterActions(components);
            if (TruongConfig.MaTruong == "UEL")
            {
                TargetViewId = "QuanLyHoatDongKhac_ListThanhToanKLGD_ListView";
            }
            else if (TruongConfig.MaTruong != "UEL")
            {
                TargetViewId = "NULL";
            }                 
        }      
        private void btnXoa_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _qly = ((DetailView)((View.ObjectSpace).Owner)).CurrentObject as QuanLyHoatDongKhac;
            _obs = Application.CreateObjectSpace();
            _session = ((XPObjectSpace)_obs).Session;
            if (_qly != null)
            {
                if (XtraMessageBox.Show("Bạn có chắc muốn xóa những dòng đã chọn? ", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (DialogUtil.Wait("Đang xóa dữ liệu đồng bộ", "Thông báo"))
                    {
                        string StringDelete_ChiTietKhoiLuongGiangDay = "";
                        string StringDelete_ChiTietHoatDongKhac = "";
                        foreach (ChiTietThanhToanKhoiLuongGiangDay item in View.SelectedObjects)
                        {
                            StringDelete_ChiTietKhoiLuongGiangDay += item.Oid_ChiTietKhoiLuongGiangDay.ToString() + ";";
                            StringDelete_ChiTietHoatDongKhac += item.Oid.ToString() + ";";
                        }

                        SqlParameter[] param = new SqlParameter[4];
                        param[0] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName.ToString());
                        param[1] = new SqlParameter("@StringDelete_ChiTietKhoiLuongGiangDay", StringDelete_ChiTietKhoiLuongGiangDay);
                        param[2] = new SqlParameter("@StringDelete_ChiTietHoatDongKhac", StringDelete_ChiTietHoatDongKhac);
                        param[3] = new SqlParameter("@KQ", SqlDbType.NVarChar, 200);
                        param[3].Direction = ParameterDirection.Output;
                        DataProvider.ExecuteNonQuery("spd_PMS_XoaChiTietThanhToanKhoiLuong_UEL", System.Data.CommandType.StoredProcedure, param);
                        string KQ = param[3].Value.ToString();
                        if(KQ == "SUCCESS")
                        {
                            XtraMessageBox.Show("Xóa thành công ","THÀNH CÔNG");
                        }
                        else
                        {
                            XtraMessageBox.Show(KQ, "LỖI");
                        }
                        
                        (View as DevExpress.ExpressApp.ListView).CollectionSource.Reload();
                    }
                }
            }
        }
    }
}
