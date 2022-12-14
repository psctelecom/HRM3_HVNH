using System;
using System.ComponentModel;


using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.Reports.Win;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;

namespace PSC_HRM.Module.Win.Common
{
    [DomainComponent]
    public class CustomReportWizardParameters : INewXafReportWizardParameters
    {
        private CustomXafReport _report;
        private HRMReport _data;

        public CustomReportWizardParameters(XafReport report, IReportData data)
        {
            _report = (CustomXafReport)report;
            _data = (HRMReport)data;
        }

        [ModelDefault("Caption", "Tên báo cáo")]
        public string ReportName 
        {
            get
            {
                return _report.ReportName;
            }
            set
            {
                _report.ReportName = value;
            }
        }

        [ModelDefault("Caption", "Nhóm báo cáo")]
        public GroupReport Group
        {
            get
            {
                return _report.GroupReport;
            }
            set
            {
                if (_report != null)
            	    _report.GroupReport = value;
                if (_data != null)
                    _data.NhomBaoCao = value;
            }
        }

        [ModelDefault("Caption", "Kiểu dữ liệu")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.ReportTypeEditor")]
        [TypeConverter(typeof(CustomReportDataTypeConverter))]
        public Type DataType
        {
            get
            {
                return _report.DataType;
            }
            set
            {
                _report.DataType = value;
            }
        }

        [ModelDefault("Caption", "Hiển thị trên cửa sổ")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.PersistentTypeEditor")]
        public Type TargetType
        {
            get
            {
                return _report.TargetType;
            }
            set
            {
                if (_report != null)
                    _report.TargetType = value;
                if (_data != null)
                    _data.TargetType = value;
            }
        }

        [ModelDefault("Caption", "Kiểu báo cáo")]
        public ReportType ReportType { get; set; }

        [Browsable(false)]
        public XafReport Report
        {
            get { return _report; }
        }
        
        [Browsable(false)]
        public IReportData ReportData
        {
            get { return _data; }
        }

        public void AssignTo(IReportData reportData)
        {
            _data = (reportData as HRMReport);
            if (_data != null)
            {
                _data.NhomBaoCao = _data.Session.GetObjectByKey<GroupReport>(Group.Oid);
                _data.ReportName = ReportName;
                _data.TargetType = TargetType;
            }

            if (DataType.BaseType == typeof(StoreProcedureReport))
            {
                StoreProcedureReport.Param = ReflectionHelper.CreateObject(DataType.Name, _data.Session) as StoreProcedureReport;
            }
        }
    }

}