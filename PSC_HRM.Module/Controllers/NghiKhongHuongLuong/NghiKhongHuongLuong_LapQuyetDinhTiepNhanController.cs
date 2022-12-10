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
using DevExpress.Utils;

namespace PSC_HRM.Module.Controllers
{
    public partial class NghiKhongHuongLuong_LapQuyetDinhTiepNhanController : ViewController
    {
        private IObjectSpace obs;
        private QuyetDinhNghiKhongHuongLuong qdNghiKhongHuongLuong;
        private QuyetDinhTiepNhan qdTiepNhan;

        public NghiKhongHuongLuong_LapQuyetDinhTiepNhanController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NghiKhongHuongLuong_LapQuyetDinhTiepNhanController");
        }

        private void NghiKhongHuongLuong_LapQuyetDinhTiepNhanController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNghiKhongHuongLuong>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý...", "Vui lòng chờ"))
            {
                qdNghiKhongHuongLuong = View.CurrentObject as QuyetDinhNghiKhongHuongLuong;
                if (qdNghiKhongHuongLuong != null)
                {
                    obs = Application.CreateObjectSpace();
                    qdTiepNhan = obs.FindObject<QuyetDinhTiepNhan>(CriteriaOperator.Parse("QuyetDinhNghiKhongHuongLuong=?", qdNghiKhongHuongLuong.Oid));

                    if (qdTiepNhan == null)
                    {
                        qdTiepNhan = obs.CreateObject<QuyetDinhTiepNhan>();
                        qdTiepNhan.QuyetDinhNghiKhongHuongLuong = obs.GetObjectByKey<QuyetDinhNghiKhongHuongLuong>(qdNghiKhongHuongLuong.Oid);
                        qdTiepNhan.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(qdNghiKhongHuongLuong.ThongTinNhanVien.Oid);
                        qdTiepNhan.BoPhan = obs.GetObjectByKey<BoPhan>(qdNghiKhongHuongLuong.BoPhan.Oid);

                        e.View = Application.CreateDetailView(obs, qdTiepNhan);

                    }
                    else
                    {
                        HamDungChung.ShowErrorMessage("Cán bộ đã tiếp nhận từ quyết định này.");
                    }
                }
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            obs.CommitChanges();
            View.Refresh();
        }
    }
}
