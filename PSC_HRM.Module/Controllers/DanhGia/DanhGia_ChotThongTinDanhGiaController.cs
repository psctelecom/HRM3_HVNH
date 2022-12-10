using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhGia;
using DevExpress.Utils;
using PSC_HRM.Module.HoSo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Controllers
{
    public partial class DanhGia_ChotThongTinDanhGiaController : ViewController
    {
        public DanhGia_ChotThongTinDanhGiaController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DanhGia_ChotThongTinDanhGiaController");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if ((View.CurrentObject as BangChotThongTinDanhGia).Oid == Guid.Empty)
            {
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            }
            //
            BangChotThongTinDanhGia danhGia = View.CurrentObject as BangChotThongTinDanhGia;
            //
            if (danhGia != null && danhGia.ABC_EvaluationBoard != null)
            {
                try
                {
                    using (DialogUtil.AutoWait())
                    {
                        //----------Xóa dữ liệu cũ-------------
                        SqlParameter[] param_xoa = new SqlParameter[1];
                        param_xoa[0] = new SqlParameter("@BangChotThongTinDanhGia", danhGia.Oid);
                        DataProvider.ExecuteNonQuery("dbo.spd_DanhGia_XoaThongTinDanhGiaTheoBangDanhGia", System.Data.CommandType.StoredProcedure, param_xoa);

                        
                        ////--------Tạo dữ liệu mới-----------
                        XPCollection<DoiTuongDanhGia> doiTuongDanhGiaList;
                        doiTuongDanhGiaList = new XPCollection<DoiTuongDanhGia>(((XPObjectSpace)View.ObjectSpace).Session);

                        string dieuKienNhanVien;

                        foreach (DoiTuongDanhGia dt in doiTuongDanhGiaList)
                        {
                            dieuKienNhanVien = dt.DieuKienApDung.XuLyDieuKien(View.ObjectSpace, false, new object[] { });

                            SqlParameter[] param = new SqlParameter[4];
                            param[0] = new SqlParameter("@BangChotThongTinDanhGia", danhGia.Oid);
                            param[1] = new SqlParameter("@DoiTuongDanhGia", dt.Oid);
                            param[2] = new SqlParameter("@DieuKienNhanVien", dieuKienNhanVien);
                            param[3] = new SqlParameter("@ThongTinTruong", HamDungChung.ThongTinTruong(((XPObjectSpace)View.ObjectSpace).Session).Oid);

                            DataProvider.ExecuteNonQuery("dbo.spd_DanhGia_ChotThongTinDanhGia", System.Data.CommandType.StoredProcedure, param);
                        }

                        //View.ObjectSpace.Refresh();
                        //View.Refresh();

                        //Refesh lại dữ liệu
                        View.ObjectSpace.ReloadObject(danhGia);
                        (View as DetailView).Refresh();
                    }
                    DialogUtil.ShowInfo("Chốt thông tin đánh giá thành công!");
                }
                catch (Exception ex)
                {
                    DialogUtil.ShowError("Chốt thông tin đánh giá không thành công.");
                }
            }
            else
            { DialogUtil.ShowError("Chưa chọn bảng đánh giá."); }
        }

        private void DanhGia_ChotThongTinDanhGiaController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangChotThongTinDanhGia>();
        }

    }
}
