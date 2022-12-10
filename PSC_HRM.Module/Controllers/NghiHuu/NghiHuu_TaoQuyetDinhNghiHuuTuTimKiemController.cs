using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.NghiHuu;

namespace PSC_HRM.Module.Controllers
{
    public partial class NghiHuu_TaoQuyetDinhNghiHuuTuTimKiemController : ViewController
    {
        private IObjectSpace obs;
        private DanhSachDenTuoiNghiHuu dsDenTuoiNghiHuu;

        public NghiHuu_TaoQuyetDinhNghiHuuTuTimKiemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NghiHuu_TaoQuyetDinhNghiHuuTuTimKiemController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //lưu danh sách đến hạn nâng lương
            View.ObjectSpace.CommitChanges();
            dsDenTuoiNghiHuu = View.CurrentObject as DanhSachDenTuoiNghiHuu;

            if (dsDenTuoiNghiHuu != null)
            {
                obs = Application.CreateObjectSpace();
                int dem = 0;
                QuyetDinhNghiHuu quyetDinh;
                //Lấy danh sách cán bộ đã tìm được
                foreach (DenTuoiNghiHuu item in dsDenTuoiNghiHuu.DenTuoiNghiHuuList)
                {
                    if (item.Chon)
                    {
                        using (DialogUtil.AutoWaitForSave())
                        {
                            //Tạo quyết định nâng lương cho 1 người
                            quyetDinh = obs.CreateObject<QuyetDinhNghiHuu>();
                            //quyetDinh.SoQuyetDinh
                            quyetDinh.ThongTinTruong = obs.GetObjectByKey<ThongTinTruong>(HamDungChung.CauHinhChung.ThongTinTruong.Oid);

                            quyetDinh.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                            quyetDinh.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                            quyetDinh.NghiViecTuNgay = item.NgaySeNghiHuu;
                        }
                        e.View = Application.CreateDetailView(obs, quyetDinh);
                        e.Context = TemplateContext.View;
                        return;
                    }
                }
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (dsDenTuoiNghiHuu != null)
                dsDenTuoiNghiHuu.LoadData();
        }

        //reload listview after save object in detailvew
        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void NghiHuu_TaoQuyetDinhNghiHuuTuTimKiemController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active.Clear();
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<DanhSachDenTuoiNghiHuu>()
                && HamDungChung.IsWriteGranted<QuyetDinh.QuyetDinhNghiHuu>()
                && HamDungChung.IsWriteGranted<NhanVienThongTinLuong>();
        }
    }
}
