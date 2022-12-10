using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.TapSu;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class DaoTao_ChonCanBoLapQuyetDinhChoPhepNghiHocController : ViewController
    {
        private IObjectSpace obs;
        private QuyetDinhChoPhepNghiHoc quyetDinh;
        private HoSo_ChonNhanVien list;

        public DaoTao_ChonCanBoLapQuyetDinhChoPhepNghiHocController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DaoTao_ChonCanBoLapQuyetDinhChoPhepNghiHocController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();

            quyetDinh = View.CurrentObject as QuyetDinhChoPhepNghiHoc;
            if (quyetDinh != null
                && quyetDinh.QuyetDinhDaoTao != null)
            {
                list = obs.CreateObject<HoSo_ChonNhanVien>();
                HoSo_NhanVienItem chiTiet;
                foreach (ChiTietDaoTao item in quyetDinh.QuyetDinhDaoTao.ListChiTietDaoTao)
                {
                    chiTiet = obs.CreateObject<HoSo_NhanVienItem>();
                    chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    list.ListNhanVien.Add(chiTiet);
                }

                e.View = Application.CreateDetailView(obs, list);
            }
            else
                HamDungChung.ShowWarningMessage("Chưa chọn quyết định đào tạo");
        }

        private void DaoTao_ChonCanBoLapQuyetDinhChoPhepNghiHocController_Activated(object sender, EventArgs e)
        {
            //popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhChoPhepNghiHoc>();
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            obs = View.ObjectSpace;
            foreach (var item in list.ListNhanVien)
            {
                if (item.Chon)
                {
                    if (!TapSuHelper.IsExits(quyetDinh, item.ThongTinNhanVien))
                    {
                        quyetDinh.CreateListChiTietQuyetDinhChoPhepNghiHoc(item);
                    }
                }
            }
            View.Refresh();
        }
    }
}
