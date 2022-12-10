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
    public partial class KhenThuong_QuyetDinhKhenThuongController : ViewController
    {
        public KhenThuong_QuyetDinhKhenThuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void KhenThuong_QuyetDinhKhenThuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["Allow"] = HamDungChung.IsCreateGranted<QuyetDinhKhenThuong>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            ChiTietDeNghiKhenThuong deNghi = View.CurrentObject as ChiTietDeNghiKhenThuong;
            if (deNghi != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                QuyetDinhKhenThuong quyetDinh = obs.FindObject<QuyetDinhKhenThuong>(CriteriaOperator.Parse("DeNghiKhenThuong=?", deNghi.Oid));
                if (quyetDinh == null)
                {
                    deNghi = obs.GetObjectByKey<ChiTietDeNghiKhenThuong>(deNghi.Oid);
                    quyetDinh = obs.CreateObject<QuyetDinhKhenThuong>();
                    quyetDinh.NamHoc = deNghi.QuanLyKhenThuong.NamHoc;
                    quyetDinh.DanhHieuKhenThuong = deNghi.DanhHieuKhenThuong;
                    quyetDinh.DeNghiKhenThuong = deNghi;
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
