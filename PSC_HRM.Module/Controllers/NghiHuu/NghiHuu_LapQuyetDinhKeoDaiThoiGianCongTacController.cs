using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.XuLyQuyTrinh.NghiHuu;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class NghiHuu_LapQuyetDinhKeoDaiThoiGianCongTacController : ViewController
    {
        public NghiHuu_LapQuyetDinhKeoDaiThoiGianCongTacController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NghiHuu_LapQuyetDinhKeoDaiThoiGianCongTacController");
        }

        private void DanhGia_CopyBangQuyDoiThangController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNghiHuu>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinNghiHuu thongTinNghiHuu = View.CurrentObject as ThongTinNghiHuu;
            if (thongTinNghiHuu != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                QuyetDinhKeoDaiThoiGianCongTac obj = obs.CreateObject<QuyetDinhKeoDaiThoiGianCongTac>();
                obj.BoPhan = obs.GetObjectByKey<BoPhan>(thongTinNghiHuu.BoPhan.Oid);
                obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thongTinNghiHuu.ThongTinNhanVien.Oid);
                obj.TuNgay = thongTinNghiHuu.Ngay;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, obj);
                e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                e.ShowViewParameters.CreateAllControllers = true;
            }
        }
    }
}
