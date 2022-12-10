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
using PSC_HRM.Module.PMS.GioChuan;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.CauHinh;
using PSC_HRM.Module.PMS.BaoCao;
using PSC_HRM.Module.Report;

namespace PSC_HRM.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class Report_Onload_Contronller : ViewController
    {
        IObjectSpace _obs = null;
        Session _Session;
        public Report_Onload_Contronller()
        {
            InitializeComponent();
            RegisterActions(components);

            TargetViewId = "Report_BaoCaoTheoGio_DetailView";
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            _obs = Application.CreateObjectSpace();
            _Session = ((XPObjectSpace)_obs).Session;
            DevExpress.ExpressApp.DC.ITypeInfo type;
            DetailView _DetailView = View as DetailView;
            if (_DetailView != null)
            {
                if (_DetailView.ToString().Contains("Report_BaoCaoTheoGio_DetailView"))
                {
                    type = ObjectSpace.TypesInfo.FindTypeInfo("PSC_HRM.Module.PMS.BaoCao.Report_BaoCaoTheoGio");

                    StoreProcedureReport storeReport = (StoreProcedureReport)_obs.CreateObject(type.Type);
                    Report_BaoCaoTheoGio rpt = View.CurrentObject as Report_BaoCaoTheoGio;
                    if (rpt != null)
                    {
                        if(rpt.Oid.ToString()=="{6e10669a-f7f6-498a-9b90-aa7256b1eeb0}")
                        { 
                            rpt.LoaiGioThanhToan = PSC_HRM.Module.PMS.Enum.LoaiGioThanhToanEnum.TatCa;
                        }
                        //CauHinhChung cauHinhChung = null;
                        //PSC_HRM.Module.BaoMat.ThongTinTruong tt = HamDungChung.ThongTinTruong(_Session);
                        //try
                        //{
                        //    if (tt != null)
                        //        cauHinhChung = _Session.FindObject<CauHinhChung>(CriteriaOperator.Parse("ThongTinTruong =?", tt.Oid));
                        //    if (cauHinhChung != null)
                        //        dinhMuc.DinhMucGioChuan = cauHinhChung.SoGioChuan;
                        //}
                        //catch (Exception)
                        //{
                        //}
                    }
                }
            }
        }
    }
}
