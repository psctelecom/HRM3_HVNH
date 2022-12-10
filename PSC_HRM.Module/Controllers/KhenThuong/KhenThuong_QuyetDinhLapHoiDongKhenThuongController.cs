using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.KhenThuong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class KhenThuong_QuyetDinhLapHoiDongKhenThuongController : ViewController
    {
        public KhenThuong_QuyetDinhLapHoiDongKhenThuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void KhenThuong_QuyetDinhLapHoiDongKhenThuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["Allow"] = HamDungChung.IsCreateGranted<QuyetDinhThanhLapHoiDongKhenThuong>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            QuanLyKhenThuong quanLy = View.CurrentObject as QuanLyKhenThuong;
            if (quanLy != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                QuyetDinhThanhLapHoiDongKhenThuong quyetDinh = obs.FindObject<QuyetDinhThanhLapHoiDongKhenThuong>(CriteriaOperator.Parse("QuanLyKhenThuong=?", quanLy.Oid));
                if (quyetDinh == null)
                {
                    quanLy = obs.GetObjectByKey<QuanLyKhenThuong>(quanLy.Oid);
                    quyetDinh = obs.CreateObject<QuyetDinhThanhLapHoiDongKhenThuong>();
                    quyetDinh.QuanLyKhenThuong = quanLy;
                }

                e.ShowViewParameters.Context = TemplateContext.View;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, quyetDinh);
                obs.Committed += obs_Committed;
            }
        }

        void obs_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.Refresh();
            View.Refresh();
        }
    }
}
