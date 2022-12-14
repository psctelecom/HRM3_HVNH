using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.DC;
using PSC_HRM.Module.Report;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.BoiDuong;

namespace PSC_HRM.Module.Controllers
{
    public partial class BoiDuong_InDanhSachDangKyBoiDuongController : ViewController
    {
        public BoiDuong_InDanhSachDangKyBoiDuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void BoiDuong_InDanhSachDangKyBoiDuongController_Activated(object sender, EventArgs e)
        {
            if(TruongConfig.MaTruong == "NEU")
                simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<DangKyBoiDuong>();
            else
                simpleAction1.Active["TruyCap"] = false;
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ITypeInfo type;
            IObjectSpace obs = Application.CreateObjectSpace();

            var list = new List<DangKyBoiDuong>();
            DangKyBoiDuong dangKyBoiDuong;
            foreach (object item in View.SelectedObjects)
            {
                dangKyBoiDuong = (DangKyBoiDuong)item;
                if (dangKyBoiDuong != null)
                    list.Add(dangKyBoiDuong);

                //In report danh sách kèm theo
                type = obs.TypesInfo.FindTypeInfo("PSC_HRM.Module.Report.Report_BoiDuong_DanhSachDangKyBoiDuong");
                if (type != null)
                {
                    StoreProcedureReport rpt = (StoreProcedureReport)obs.CreateObject(type.Type);
                    if (rpt != null)
                    {
                        HRMReport report = obs.FindObject<HRMReport>(CriteriaOperator.Parse("Oid =?", "3514"));
                        if (report != null)
                        {
                            //Truyền parameter
                            ((Report_BoiDuong_DanhSachDangKyBoiDuong)rpt).DangKyBoiDuong = obs.GetObjectByKey<DangKyBoiDuong>(dangKyBoiDuong.Oid);
                            //     
                            StoreProcedureReport.Param = rpt;
                            Frame.GetController<ReportServiceController>().ShowPreview(report);
                        }
                    }
                }
            }
        }
    }
}
