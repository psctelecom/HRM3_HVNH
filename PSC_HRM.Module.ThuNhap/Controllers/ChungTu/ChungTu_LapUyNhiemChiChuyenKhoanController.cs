using System;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.ThuNhap.ChungTu;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ChungTu_LapUyNhiemChiChuyenKhoanController : ViewController
    {
        private IObjectSpace obs;
        private UyNhiemChi uyNhiemChi;
        private ChuyenKhoanLuongNhanVien chuyenKhoanLuong;

        public ChungTu_LapUyNhiemChiChuyenKhoanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ChungTu_LapUyNhiemChiChuyenKhoanController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = false;//HamDungChung.IsWriteGranted<ChuyenKhoanLuongNhanVien>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            chuyenKhoanLuong = View.CurrentObject as ChuyenKhoanLuongNhanVien;
            uyNhiemChi = obs.CreateObject<UyNhiemChi>();
            if (chuyenKhoanLuong != null)
            {
                uyNhiemChi.SoTien = chuyenKhoanLuong.SoTien;
                uyNhiemChi.SoTienBangChu = chuyenKhoanLuong.SoTienBangChu;
            }
            e.Context = TemplateContext.View;
            e.View = Application.CreateDetailView(obs, uyNhiemChi);
            obs.Committing += obs_Committing;
        }

        private void obs_Committing(object sender, CancelEventArgs e)
        {
            chuyenKhoanLuong.UyNhiemChi = uyNhiemChi;
            chuyenKhoanLuong.Save();
        }
    }
}
