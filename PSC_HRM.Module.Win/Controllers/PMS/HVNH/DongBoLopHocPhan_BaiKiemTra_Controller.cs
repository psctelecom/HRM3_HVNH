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
using PSC_HRM.Module.PMS.NghiepVu.HVNH;

namespace PSC_HRM.Module.Win.Controllers.PMS.HVNH
{
    public partial class DongBoLopHocPhan_BaiKiemTra_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session ses;
        QuanLyBaiKiemTra _KeKhai;
        public DongBoLopHocPhan_BaiKiemTra_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyBaiKiemTra_DetailView";
        }

        private void btQuyDoi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _KeKhai = View.CurrentObject as QuanLyBaiKiemTra;
            if (_KeKhai != null)
            {
                if (_KeKhai.BangChotThuLao == Guid.Empty)
                {
                    using (DialogUtil.AutoWait("Đang đồng bộ dữ liệu"))
                    {
                        SqlParameter[] pQuyDoi = new SqlParameter[5];
                        pQuyDoi[0] = new SqlParameter("@ThongTinTruong", _KeKhai.ThongTinTruong.Oid);
                        pQuyDoi[1] = new SqlParameter("@NamHoc", _KeKhai.NamHoc.Oid);
                        pQuyDoi[2] = new SqlParameter("@HocKy", _KeKhai.HocKy.Oid);
                        pQuyDoi[3] = new SqlParameter("@QuanLyBaiKiemTra", _KeKhai.Oid);
                        pQuyDoi[4] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName.ToString());
                        DataProvider.GetValueFromDatabase("spd_PMS_DongBoLopHocPhanBaiKiemTra", CommandType.StoredProcedure, pQuyDoi);
                        View.ObjectSpace.Refresh();
                    }
                    XtraMessageBox.Show("Đồng bộ thành công!", "Thông báo!");
                }
                else
                {
                    XtraMessageBox.Show("Đã chốt dữ liệu không thể đồng bộ lại!", "Thông báo!");
                }
            }

        }
    }
}