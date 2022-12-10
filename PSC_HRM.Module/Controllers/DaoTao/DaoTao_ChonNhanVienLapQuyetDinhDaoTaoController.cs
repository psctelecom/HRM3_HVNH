using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module;
using PSC_HRM.Module.DaoTao;

namespace PSC_HRM.Module.Controllers
{
    public partial class DaoTao_ChonNhanVienLapQuyetDinhDaoTaoController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonNhanVien danhSach;
        private QuyetDinhDaoTao qd;

        public DaoTao_ChonNhanVienLapQuyetDinhDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DaoTao_ChonNhanVienLapQuyetDinhDaoTaoController");
        }

        private void DanhGia_ImportChamCongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhDaoTao>()
                && HamDungChung.IsWriteGranted<ChiTietDaoTao>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            qd = View.CurrentObject as QuyetDinhDaoTao;
            if (qd != null && qd.DuyetDangKyDaoTao != null)
            {
                obs = Application.CreateObjectSpace();
                danhSach = obs.CreateObject<HoSo_ChonNhanVien>();
                HoSo_NhanVienItem nhanVien;
                foreach (var item in qd.DuyetDangKyDaoTao.ListChiTietDuyetDangKyDaoTao)
                {
                    nhanVien = obs.CreateObject<HoSo_NhanVienItem>();
                    nhanVien.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    nhanVien.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    danhSach.ListNhanVien.Add(nhanVien);
                }
                e.View = Application.CreateDetailView(obs, danhSach);
            }
            else
                HamDungChung.ShowWarningMessage("Chưa nhập dữ liệu cho mục Đăng ký đào tạo");
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            foreach (var item in danhSach.ListNhanVien)
            {
                if (!DaoTaoHelper.IsExits(qd, item.ThongTinNhanVien))
                {
                    qd.CreateListChiTietDaoTao(item);
                }
            }
            //View.ObjectSpace.CommitChanges();
            //View.ObjectSpace.Refresh();
            View.Refresh();
        }
    }
}
