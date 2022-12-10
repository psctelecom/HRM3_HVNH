using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class DaoTao_LapQuyetDinhCongNhanDaoTaoController : ViewController
    {
        private IObjectSpace obs;
        private QuyetDinhCongNhanDaoTao quyetDinh;
        private HoSo_ChonNhanVien danhSach;

        public DaoTao_LapQuyetDinhCongNhanDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DaoTao_LapQuyetDinhCongNhanDaoTaoController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();

            quyetDinh = View.CurrentObject as QuyetDinhCongNhanDaoTao;
            if (quyetDinh != null
                && quyetDinh.QuyetDinhDaoTao != null)
            {
                danhSach = obs.CreateObject<HoSo_ChonNhanVien>();
                HoSo_NhanVienItem chiTiet;
                foreach (ChiTietDaoTao item in quyetDinh.QuyetDinhDaoTao.ListChiTietDaoTao)
                {
                    chiTiet = obs.CreateObject<HoSo_NhanVienItem>();
                    chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    danhSach.ListNhanVien.Add(chiTiet);
                }

                e.View = Application.CreateDetailView(obs, danhSach);
            }
            else
                HamDungChung.ShowWarningMessage("Chưa chọn quyết định đào tạo");
        }

        private void DanhGia_CopyBangQuyDoiThangController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhCongNhanDaoTao>();
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            ChiTietCongNhanDaoTao chiTiet;
            obs = View.ObjectSpace;
            foreach (var item in danhSach.ListNhanVien)
            {
                if (item.Chon)
                {
                    chiTiet = obs.FindObject<ChiTietCongNhanDaoTao>(CriteriaOperator.Parse("QuyetDinhCongNhanDaoTao=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                    if (chiTiet == null)
                    {
                        chiTiet = obs.CreateObject<ChiTietCongNhanDaoTao>();
                        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        quyetDinh.ListChiTietCongNhanDaoTao.Add(chiTiet);
                    }
                }
            }
            View.Refresh();
        }
    }
}
