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
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;
using System.Windows.Forms;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class QuanLyGioGiang_DongBo_BangChot_Controller : ViewController
    {
        IObjectSpace _obs = null;
        QuanLyGioGiang giogiang;
        Session ses = null;
        public QuanLyGioGiang_DongBo_BangChot_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyGioGiang_DetailView";
        }

        private void btQuiDoi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)_obs).Session;
            giogiang = View.CurrentObject as QuanLyGioGiang;
            DialogResult dialogResult = DialogResult.No;
            if (giogiang != null)
            {
                CriteriaOperator fchitiet= CriteriaOperator.Parse("DongBo = true and NhanVien_GioGiang.QuanLyGioGiang = ?", giogiang.Oid);
                XPCollection<ChiTietGioGiang> dschitiet = new XPCollection<ChiTietGioGiang>(ses, fchitiet);

                if (dschitiet!=null)
                {
                    dialogResult = MessageBox.Show("Dữ liệu củ được đồng bộ sẽ bị xóa, bạn chắc muốn đồng bộ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);             
                }
                else
                {
                    dialogResult = DialogResult.Yes;
                }
                if (dialogResult == DialogResult.Yes)
                {
                    using (DialogUtil.AutoWait("Hệ thống đang đồng bộ dữ liệu"))
                    {
                        SqlParameter[] pQuyDoi = new SqlParameter[2];
                        pQuyDoi[0] = new SqlParameter("@NamHoc", giogiang.NamHoc.Oid);
                        pQuyDoi[1] = new SqlParameter("@QuanLyGioGiang", giogiang.Oid);
                        DataProvider.GetValueFromDatabase("spd_PMS_DongBoDuLieu_QuanLyGioGiang", CommandType.StoredProcedure, pQuyDoi);
                        View.ObjectSpace.Refresh();
                        XtraMessageBox.Show("Đồng bộ dữ liệu thành công!", "Thông báo");
                    }
                }
                else
                {
                    XtraMessageBox.Show("Đồng bộ dữ liệu không thành công!", "Thông báo");
                }
            }
        }
        
    }
}