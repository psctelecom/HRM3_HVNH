using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class HopDong_TaoPhuLucHopDongKhoanController : ViewController
    {
        public HopDong_TaoPhuLucHopDongKhoanController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_TaoPhuLucHopDongKhoanController");
        }

        private void popupWindowShowAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            IObjectSpace obs = Application.CreateObjectSpace();

            HopDong_Khoan hopDong = View.CurrentObject as HopDong_Khoan;
            if (hopDong != null 
                && hopDong.HopDongKhoan == null
                && hopDong.PhanLoai == HopDongKhoanEnum.ChinhThuc)
            {
                HopDong_Khoan phuLuc;
                if (hopDong.HopDongKhoan == null)
                {
                    phuLuc = obs.CreateObject<HopDong_Khoan>();
                    phuLuc.QuanLyHopDong = obs.GetObjectByKey<QuanLyHopDong>(hopDong.QuanLyHopDong.Oid);
                    phuLuc.HopDongKhoan = obs.GetObjectByKey<HopDong_Khoan>(hopDong.Oid);
                }
                else
                    phuLuc = obs.FindObject<HopDong_Khoan>(CriteriaOperator.Parse("HopDongKhoan=?", hopDong.Oid));

                Application.ShowView<HopDong_Khoan>(obs, phuLuc);
            }
            else
            {
                HamDungChung.ShowWarningMessage("Vui lòng chọn hợp đồng khoán chính thức để tạo phục hợp đồng.");
            }
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<HopDong_Khoan>()
                && HamDungChung.IsWriteGranted<HopDong_NhanVien>()
                && HamDungChung.IsWriteGranted<HopDong.HopDong>();
        }
    }
}
