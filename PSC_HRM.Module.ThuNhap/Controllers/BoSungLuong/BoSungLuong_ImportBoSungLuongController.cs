using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using PSC_HRM.Module.ThuNhap.BoSungLuong;
using PSC_HRM.Module.ThuNhap.NonPersistentThuNhap;
using PSC_HRM.Module.ThuNhap.Import;

namespace PSC_HRM.Module.ThuNhap.Controllers.BoSungLuong
{
    public partial class BoSungLuong_ImportBoSungLuongController : ViewController
    {
        private IObjectSpace _obs;
        private BoSungLuongNhanVien _boSungLuongNhanVien;
        private BoSungLuong_LoaiBoSungLuong _chonLoaiBoSungLuong;

        public BoSungLuong_ImportBoSungLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //lưu bổ sung lương nhân viên lại
            View.ObjectSpace.CommitChanges();
            _obs = Application.CreateObjectSpace();

            _boSungLuongNhanVien = View.CurrentObject as BoSungLuongNhanVien;

             if (_boSungLuongNhanVien != null)
             {
                 if (_boSungLuongNhanVien.KyTinhLuong.KhoaSo)
                     DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", _boSungLuongNhanVien.KyTinhLuong.TenKy));
                 //else if (_boSungLuongNhanVien.ChungTuLuongKy1 != null || )
                 //    DialogUtil.ShowWarning("Bảng lương đã được lập chứng từ chi tiền.");
                 else if (_boSungLuongNhanVien.NgayLap < _boSungLuongNhanVien.KyTinhLuong.TuNgay || _boSungLuongNhanVien.NgayLap > _boSungLuongNhanVien.KyTinhLuong.DenNgay)
                 {
                     DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                 }
                 else
                 {
                     _chonLoaiBoSungLuong = _obs.CreateObject<BoSungLuong_LoaiBoSungLuong>();
                     e.View = Application.CreateDetailView(_obs, _chonLoaiBoSungLuong);
                 }
             }        
        }

        private void BoSungLuong_ImportBoSungLuongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active.Clear();
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BoSungLuongNhanVien>();
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            bool daLapChungTu = true;
            //Save view hiện tại
            e.PopupWindow.View.ObjectSpace.CommitChanges();
            //
            switch (_chonLoaiBoSungLuong.LoaiBoSungLuong)
            {
                case LoaiBoSungLuongEnum.LuongKy1:
                    if (_boSungLuongNhanVien.ChungTuLuongKy1 == null)
                    {
                        ImportChiBoSungLuongKy1.XuLy(_obs, _boSungLuongNhanVien);
                        //
                        daLapChungTu = false;
                    }
                    break;
                case LoaiBoSungLuongEnum.PhuCapUuDai:
                    if (_boSungLuongNhanVien.ChungTuLuongKy1 == null)
                    {
                        ImportChiBoSungPhuCapUuDai.XuLy(_obs, _boSungLuongNhanVien);
                        //
                        daLapChungTu = false;
                    }
                    break;
                case LoaiBoSungLuongEnum.PhuCapTrachNhiem:
                    if (_boSungLuongNhanVien.ChungTuPhuCapTrachNhiem == null)
                    {
                        ImportChiBoSungPhuCapTrachNhiem.XuLy(_obs, _boSungLuongNhanVien);
                        //
                        daLapChungTu = false;
                    }
                    break;
                case LoaiBoSungLuongEnum.PhuCapThamNien:
                    if (_boSungLuongNhanVien.ChungTuPhuCapThamNien == null)
                    {
                        ImportChiBoSungPhuCapThamNien.XuLy(_obs, _boSungLuongNhanVien);
                        //
                        daLapChungTu = false;
                    }
                    break;
                case LoaiBoSungLuongEnum.LuongKy2:
                    if (_boSungLuongNhanVien.ChungTuLuongKy2 == null)
                    {
                        ImportChiBoSungLuongKy2.XuLy(_obs, _boSungLuongNhanVien);
                        //
                        daLapChungTu = false;
                    }
                    break;
                case LoaiBoSungLuongEnum.PhuCapTienSi:
                    if (_boSungLuongNhanVien.ChungTuTienSi == null)
                    {
                        ImportChiBoSungPhuCapTienSi.XuLy(_obs, _boSungLuongNhanVien);
                        //
                        daLapChungTu = false;
                    }
                    break;
                case LoaiBoSungLuongEnum.NangLuongKy1:
                    if (_boSungLuongNhanVien.ChungTuNangLuongKy1 == null)
                    {
                        ImportChiBoSungNangLuongKy1.XuLy(_obs, _boSungLuongNhanVien);
                        //
                        daLapChungTu = false;
                    }
                    break;
                case LoaiBoSungLuongEnum.NangLuongKy2:
                    if (_boSungLuongNhanVien.ChungTuNangLuongKy2 == null)
                    {
                        ImportChiBoSungNangLuongKy2.XuLy(_obs, _boSungLuongNhanVien);
                        //
                        daLapChungTu = false;
                    }
                    break;
                default:
                    break;

            }

            if (daLapChungTu)
            {
                DialogUtil.ShowWarning("Bảng lương đã lập chứng từ chi tiền. Vui lòng kiểm tra lại.");
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
