using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Security;
using System.Data.SqlClient;
using DevExpress.Utils;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThongTinLuong_ChotThongTinTinhLuongController : ViewController
    {
        BangChotThongTinTinhLuong obj;
        public ThongTinLuong_ChotThongTinTinhLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThongTinLuong_ChotThongTinTinhLuongController");
        }

        private void ThongTinLuong_ChotThongTinTinhLuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangChotThongTinTinhLuong>();
        }

        private void simpleAction1_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            obj = View.CurrentObject as BangChotThongTinTinhLuong;
            if (obj != null)
            {
                if (!obj.KhoaSo)
                {
                    try
                    {
                        //int thang = 0, nam = 0;

                        //Trường hợp IUH: Phải có bảng công tháng trước mới chốt thông tin tính lương
                        //if (TruongConfig.MaTruong.Equals("IUH"))
                        //{     
                        //    thang = obj.Thang.Month;
                        //    nam = obj.Thang.Year;
                        //    //
                        //    if (thang == 1)
                        //    {
                        //        thang = 12;
                        //        nam = nam - 1;
                        //    }
                        //    else
                        //        thang = thang - 1;
                        //    //
                        //    KyTinhLuong kyTinhLuongThangTruoc = View.ObjectSpace.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? And Nam=?", thang, nam));
                        //    if (kyTinhLuongThangTruoc == null)
                        //    {
                        //        DialogUtil.ShowError("Chưa có bảng chấm công tháng trước. Vui lòng kiểm tra lại!");
                        //        return;
                        //    }
                        //    if (!obj.DaCapNhatThamNienCuaThang)
                        //    {
                        //        DialogUtil.ShowWarning("Cập nhật thông tin thâm niên trước khi chốt.");
                        //        return;
                        //    }
                        //}
                        //Trường hợp GTVT: Phải có KyTinhLuong -> lấy ngày đầu kỳ + cuối kỳ => mới chốt thông tin tính lương
                        //if (TruongConfig.MaTruong.Equals("GTVT"))
                        //{
                        //    //
                        //    KyTinhLuong kyTinhLuongThangTruoc = View.ObjectSpace.FindObject<KyTinhLuong>(CriteriaOperator.Parse("BangChotThongTinTinhLuong=?", obj.Oid));
                        //    if (kyTinhLuongThangTruoc == null)
                        //    {
                        //        DialogUtil.ShowError("Chưa có kỳ tính lương. Vui lòng kiểm tra lại!");
                        //        return;
                        //    }
                        //}
                        ///
                        using (DialogUtil.AutoWait())
                        {
                            //Tạo bảng chốt mới
                            TaoBangChotThongTinTinhLuong(obj);

                            DialogUtil.ShowInfo("Chốt thông tin tính lương thành công.");
                        }
                    }
                    catch (Exception ex)
                    {
                        DialogUtil.ShowError("Chốt thông tin tính lương không thành công.");
                    }
                }
                else
                    DialogUtil.ShowWarning("Bảng chốt thông tin tính lương đã được khóa sổ.");
            }
        }

        private void TaoBangChotThongTinTinhLuong(BangChotThongTinTinhLuong obj)
        {

            IObjectSpace obs = Application.CreateObjectSpace();

            //DateTime current;
            //if (obj.ThongTinTruong.ThongTinChung.NgayTinhLuong > 0)
            //    current = new DateTime(obj.Thang.Year, obj.Thang.Month, obj.ThongTinTruong.ThongTinChung.NgayTinhLuong);
            //else
            //    current = DateTime.Today;

            if (TruongConfig.MaTruong.Equals("QNU"))
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@BangChotThongTinTinhLuong", obj.Oid);
                param[1] = new SqlParameter("@NgayTinhLuong", obj.Thang);
                param[2] = new SqlParameter("@ThongTinTruong", obj.ThongTinTruong.Oid);
                param[3] = new SqlParameter("@LoaiLuong", obj.LoaiLuong);

                DataProvider.ExecuteNonQuery("dbo.spd_TaiChinh_ChotThongTinhLuong", System.Data.CommandType.StoredProcedure, param);
            }
            else
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@BangChotThongTinTinhLuong", obj.Oid);
                param[1] = new SqlParameter("@NgayTinhLuong", obj.Thang);
                param[2] = new SqlParameter("@ThongTinTruong", obj.ThongTinTruong.Oid);

                DataProvider.ExecuteNonQuery("dbo.spd_TaiChinh_ChotThongTinhLuong", System.Data.CommandType.StoredProcedure, param);
            }
           // DataProvider.ExecuteNonQuery("dbo.spd_TaiChinh_ChotThongTinhLuong", System.Data.CommandType.StoredProcedure, param);

            View.ObjectSpace.Refresh();
            View.Refresh();
        }
    }
}
