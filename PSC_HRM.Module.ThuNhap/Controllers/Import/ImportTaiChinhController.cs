using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.ThuNhap.KhauTru;
using PSC_HRM.Module.ThuNhap.NgoaiGio;
using PSC_HRM.Module.ThuNhap.TamUng;
using PSC_HRM.Module.ThuNhap.ThuLao;
using PSC_HRM.Module.ThuNhap.ThuNhapKhac;
using PSC_HRM.Module.ThuNhap.Thuong;
using PSC_HRM.Module.ChamCong;
using PSC_HRM.Module.ThuNhap.Import;
using PSC_HRM.Module.ThuNhap.TruyLuong;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.ThuNhap.TruyThu;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.LuongWeb;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ImportTaiChinhController : ViewController
    {
        private IObjectSpace obs;
        private ImportBase obj;
        private Import_QuyetDinhKhenThuong quyetDinh;

        public ImportTaiChinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ImportTaiChinhController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active[""] =
                (View.Id == "BangKhauTruLuong_DetailView" &&
                HamDungChung.IsWriteGranted<BangKhauTruLuong>() &&
                HamDungChung.IsWriteGranted<ChiTietKhauTruLuong>()) ||
                (View.Id == "BangLuongNgoaiGio_DetailView" &&
                HamDungChung.IsWriteGranted<BangLuongNgoaiGio>() &&
                HamDungChung.IsWriteGranted<ChiTietLuongNgoaiGio>()) ||
                (View.Id == "BangTamUng_DetailView" &&
                HamDungChung.IsWriteGranted<BangTamUng>() &&
                HamDungChung.IsWriteGranted<ChiTietTamUng>()) ||
                //(View.Id == "BangThuLaoNhanVien_DetailView" &&
                //HamDungChung.IsWriteGranted<BangThuLaoNhanVien>() &&
                //HamDungChung.IsWriteGranted<ChiTietThuLaoNhanVien>()) ||
                (View.Id == "BangThuNhapKhac_DetailView" &&
                HamDungChung.IsWriteGranted<BangThuNhapKhac>() &&
                HamDungChung.IsWriteGranted<ChiTietThuNhapKhac>()) ||
                (View.Id == "BangThuongNhanVien_DetailView" &&
                HamDungChung.IsWriteGranted<BangThuongNhanVien>() &&
                HamDungChung.IsWriteGranted<ChiTietThuongNhanVien>()) ||
                (View.Id == "BangChamCongKhoan_DetailView" &&
                HamDungChung.IsWriteGranted<BangChamCongKhoan>() &&
                HamDungChung.IsWriteGranted<ChiTietChamCongKhoan>()) ||
                (View.Id == "BangChamCongNgoaiGio_DetailView" &&
                HamDungChung.IsWriteGranted<BangChamCongNgoaiGio>() &&
                HamDungChung.IsWriteGranted<ChiTietChamCongNgoaiGio>()) ||
                (View.Id == "BangTruyLuong_DetailView" &&
                HamDungChung.IsWriteGranted<BangTruyLuong>() &&
                HamDungChung.IsWriteGranted<TruyLuongNhanVien>() &&
                HamDungChung.IsWriteGranted<ChiTietTruyLuong>()) ||
                (View.Id == "BangTruyLuongNew_DetailView" &&
                HamDungChung.IsWriteGranted<BangTruyLuongNew>() &&
                HamDungChung.IsWriteGranted<TruyLuongNhanVienNew>() &&
                HamDungChung.IsWriteGranted<ChiTietTruyLuongNew>()) ||
                (View.Id == "BangTruyThu_DetailView" &&
                HamDungChung.IsWriteGranted<BangTruyThu>() &&
                HamDungChung.IsWriteGranted<TruyThuNhanVien>() &&
                HamDungChung.IsWriteGranted<ChiTietTruyThu>() ||
                (View.Id == "BangTruyThuKhac_DetailView" &&
                HamDungChung.IsWriteGranted<BangTruyThuKhac>() &&
                HamDungChung.IsWriteGranted<ChiTietTruyThuKhac>()) ||
                (View.Id == "BangTruyLinhKhac_DetailView" &&
                HamDungChung.IsWriteGranted<BangTruyLinhKhac>() &&
                HamDungChung.IsWriteGranted<ChiTietTruyLinhKhac>()) ||
                (View.Id == "BangLuongNhanVienWeb_DetailView" &&
                HamDungChung.IsWriteGranted<BangLuongNhanVienWeb>() &&
                HamDungChung.IsWriteGranted<ChiTietLuongNhanVienWeb>()));

            //simpleAction2.Active["TruyCap"] = (View.Id == "BangThuLaoNhanVien_DetailView" &&
            //    HamDungChung.IsWriteGranted<BangThuLaoNhanVien>() &&
            //    HamDungChung.IsWriteGranted<ChiTietThuLaoNhanVien>());
            simpleAction2.Active["TruyCap"] = false;//Hưng - ẩn nút


            popupWindowShowAction1.Active["TruyCap"] = (View.Id == "BangThuongNhanVien_DetailView" &&
                HamDungChung.IsWriteGranted<BangThuongNhanVien>() &&
                HamDungChung.IsWriteGranted<ChiTietThuongNhanVien>());
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            obs = View.ObjectSpace;
            //
            if (View.Id == "BangKhauTruLuong_DetailView")
            {
                obj = obs.CreateObject<ImportKhauTruLuong>();
            }
            else if (View.Id == "BangLuongNgoaiGio_DetailView")
            {
                obj = obs.CreateObject<ImportNgoaiGio>();
            }
            else if (View.Id == "BangTamUng_DetailView")
            {
                obj = obs.CreateObject<ImportTamUng>();
            }
            //else if (View.Id == "BangThuLaoNhanVien_DetailView")
            //{
            //    obj = obs.CreateObject<ImportThuLaoExcel>();
            //}
            else if (View.Id == "BangThuNhapKhac_DetailView")
            {
                obj = obs.CreateObject<ImportThuNhapKhac>();
            }
            else if (View.Id == "BangThuongNhanVien_DetailView")
            {
                obj = obs.CreateObject<ImportThuong>();
            }
            else if (View.Id == "BangChamCongLuongKhoan_DetailView")
            {
                obj = obs.CreateObject<ImportChamCongLuongKhoan>();
            }
            else if (View.Id == "BangChamCongNgoaiGio_DetailView")
            {
                obj = obs.CreateObject<ImportChamCongNgoaiGio>();
            }
            else if (View.Id == "BangTruyLuong_DetailView")
            {
                if (TruongConfig.MaTruong == "UEL")
                {
                    obj = obs.CreateObject<ImportTruyLuong_UEL>();
                }
                else
                {
                    obj = obs.CreateObject<ImportTruyLuong>();
                }
            }
            else if (View.Id == "BangTruyLuongNew_DetailView")
            {
                obj = obs.CreateObject<ImportTruyLuongNew>();
            }
            else if (View.Id == "BangTruyThu_DetailView")
            {
                obj = obs.CreateObject<ImportTruyThu>();
            }
            else if (View.Id == "BangTruyLinhKhac_DetailView")
            {
                if (TruongConfig.MaTruong == "QNU")
                {
                    obj = obs.CreateObject<ImportTruyLinhKhac_QNU>();
                }
                else
                {
                    obj = obs.CreateObject<ImportTruyLinhKhac>();
                }
            }
            else if (View.Id == "BangTruyThuKhac_DetailView")
            {
                obj = obs.CreateObject<ImportTruyThuKhac>();
            }
            else if (View.Id == "BangLuongNhanVienWeb_DetailView")
            {
                obj = obs.CreateObject<ImportBangLuongNhanVienWeb>();
            }

            if (obj != null)
            {
                obj.XuLy(obs, View.CurrentObject);
                View.ObjectSpace.Refresh();
            }
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            obs = View.ObjectSpace;

            ImportThuLaoPMS obj = obs.CreateObject<ImportThuLaoPMS>();
            obj.XuLy(obs, View.CurrentObject);
            obs.Refresh();
        }
                
        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            quyetDinh = obs.CreateObject<Import_QuyetDinhKhenThuong>();
            e.View = Application.CreateDetailView(obs, quyetDinh);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            obs = View.ObjectSpace;
            ImportThuongTuQuyetDinh obj = obs.CreateObject<ImportThuongTuQuyetDinh>();
            obj.Data = quyetDinh;
            obj.XuLy(obs, View.CurrentObject);
            obs.Refresh();
        }
    }
}
