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
    public partial class DaoTao_LapQuyetDinhChiTienThuongTienSiController : ViewController
    {
        private IObjectSpace obs;
        private QuyetDinhChiTienThuongTienSi quyetDinh;
        private HoSo_ChonNhanVien danhSach;

        public DaoTao_LapQuyetDinhChiTienThuongTienSiController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DaoTao_LapQuyetDinhChiTienThuongTienSiController.");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            quyetDinh = View.CurrentObject as QuyetDinhChiTienThuongTienSi;
            if (quyetDinh != null
                && quyetDinh.QuyetDinhCongNhanDaoTao != null)
            {
                obs = Application.CreateObjectSpace();
                //
                danhSach = obs.CreateObject<HoSo_ChonNhanVien>();
                HoSo_NhanVienItem chiTiet;
                foreach (ChiTietCongNhanDaoTao item in quyetDinh.QuyetDinhCongNhanDaoTao.ListChiTietCongNhanDaoTao)
                {
                    chiTiet = obs.CreateObject<HoSo_NhanVienItem>();
                    chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    danhSach.ListNhanVien.Add(chiTiet);
                }

                e.View = Application.CreateDetailView(obs, danhSach);
            }
            else
            {
                DialogUtil.ShowWarning("Chưa chọn quyết định công nhận đào tạo");
                return;
            }
        }

        private void DaoTao_LapQuyetDinhChiTienThuongTienSiController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhChiTienThuongTienSi>();
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            ChiTietChiTienThuongTienSi chiTiet;
            obs = View.ObjectSpace;
            foreach (var item in danhSach.ListNhanVien)
            {
                if (item.Chon)
                {
                    chiTiet = obs.FindObject<ChiTietChiTienThuongTienSi>(CriteriaOperator.Parse("QuyetDinhChiTienThuongTienSi=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                    if (chiTiet == null)
                    {
                        chiTiet = obs.CreateObject<ChiTietChiTienThuongTienSi>();
                        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        quyetDinh.ListChiTietChiTienThuongTienSi.Add(chiTiet);
                    }
                }
            }
            View.Refresh();
        }
    }
}
