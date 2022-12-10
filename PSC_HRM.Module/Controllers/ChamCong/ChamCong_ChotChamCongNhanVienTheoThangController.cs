using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Utils;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.ChamCong;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.Controllers
{
    public partial class ChamCong_ChotChamCongNhanVienTheoThangController : ViewController
    {
        public ChamCong_ChotChamCongNhanVienTheoThangController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ChamCong_ChotChamCongNhanVienTheoThangController");
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            //Lấy bảng chấm công hiện tại
            QuanLyChamCongNhanVien quanLyChamCong = View.CurrentObject as QuanLyChamCongNhanVien;
            if (quanLyChamCong != null)
            {
                if (DialogUtil.ShowYesNo("Bạn thật sự muốn chốt châm công tháng?") == System.Windows.Forms.DialogResult.Yes)
                {
                    using (DialogUtil.AutoWait())
                    {
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@QuanLyChamCongNhanVien", quanLyChamCong.Oid);
                        param[1] = new SqlParameter("@NgayLap", quanLyChamCong.KyTinhLuong.TuNgay);
                        DataProvider.ExecuteNonQuery("spd_ChamCong_ChotChamCongNhanVienTheoThang", CommandType.StoredProcedure, param);
                        //
                        View.ObjectSpace.Refresh();
                    }
                    DialogUtil.ShowInfo("Chốt chấm công tháng thành công.");
                }
            }

        }

        private void ChamCong_ChotChamCongNhanVienTheoThangController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("UTE") || TruongConfig.MaTruong.Equals("DLU"))
            {
                simpleAction2.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyChamCongNhanVien>();
            }
            else
            {
                simpleAction2.Active["TruyCap"] = false;
            }

        }
    }
}
