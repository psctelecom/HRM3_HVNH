using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using PSC_HRM.Module.ThuNhap.ThuLao;
using System.Windows.Forms;
using PSC_HRM.Module.PMS.NghiepVu.NCKH;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.PMS.BaoCao;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class BaoCao_LayDuLieuNCKH_XacNhan_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        QuanLyNCKH_Non _HoatDong;
        public BaoCao_LayDuLieuNCKH_XacNhan_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyNCKH_Non_DetailView";
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _HoatDong = View.CurrentObject as QuanLyNCKH_Non;
            if (_HoatDong != null)
            {
                _obs = View.ObjectSpace;
                session = ((XPObjectSpace)_obs).Session;
                DevExpress.ExpressApp.DC.ITypeInfo type;
                //Bắt đầu xuất report
                int OIDReport = 10;

                type = ObjectSpace.TypesInfo.FindTypeInfo("PSC_HRM.Module.PMS.BaoCao.Report_PMS_XuatDuLieuNCKH_XacNhan");
                if (type != null)
                {
                    _obs = Application.CreateObjectSpace();
                    StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type.Type);

                    HRMReport report = _obs.FindObject<HRMReport>(CriteriaOperator.Parse("Oid =?", OIDReport));
                    if (report != null)
                    {
                        //Truyền parameter
                        ((Report_PMS_XuatDuLieuNCKH_XacNhan)rpt).NamHoc = _obs.FindObject<NamHoc>(CriteriaOperator.Parse("Oid = ?", _HoatDong.NamHoc.Oid));
                        ((Report_PMS_XuatDuLieuNCKH_XacNhan)rpt).BoPhan = HamDungChung.GetPhanQuyenBoPhan();
                        //
                        StoreProcedureReport.Param = rpt;
                        Frame.GetController<ReportServiceController>().ShowPreview(report);
                    }
                }
            }
        }
    }
}
