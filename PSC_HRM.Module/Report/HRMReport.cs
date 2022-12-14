using System;
using System.Collections.Generic;

using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using System.Drawing;
using DevExpress.Xpo.Metadata;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [ImageName("BO_Report")]
    [DefaultProperty("ReportName")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("Caption", "Báo cáo")]
    public class HRMReport : ReportData
    {
        private string _MaTruong;
        private string _TargetTypeName;
        private GroupReport _NhomBaoCao;

        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public string MaTruong
        {
            get
            {
                return _MaTruong;
            }
            set
            {
                SetPropertyValue("MaTruong", ref _MaTruong, value);
            }
        }


        [ModelDefault("Caption", "Nhóm báo cáo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public GroupReport NhomBaoCao
        {
            get
            {
                return _NhomBaoCao;
            }
            set
            {
                SetPropertyValue("NhomBaoCao", ref _NhomBaoCao, value);
            }
        }

        [Browsable(false)]
        public string TargetTypeName
        {
            get
            {
                return _TargetTypeName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    SetPropertyValue("TargetTypeName", ref _TargetTypeName, value);
            }
        }

        [NonPersistent]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Hiển thị trên cửa sổ")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.PersistentTypeEditor")]
        public Type TargetType
        {
            get
            {
                if (!String.IsNullOrEmpty(TargetTypeName))
                {
                    ITypeInfo info = XafTypesInfo.Instance.FindTypeInfo(TargetTypeName);
                    if (info != null)
                        return info.Type;
                    return null;
                }
                return null;
            }
            set
            {
                TargetTypeName = (value != null) ? value.FullName : string.Empty;
            }
        }


        [Delayed]
        [ModelDefault("Caption", "Hình ảnh")]
        [Size(SizeAttribute.Unlimited)]
        [ValueConverter(typeof(ImageValueConverter))]
        [VisibleInListView(false)]
        public Image HinhAnh
        {
            get
            {
                return GetDelayedPropertyValue<Image>("HinhAnh");
            }
            set
            {
                SetDelayedPropertyValue<Image>("HinhAnh", value);
            }
        }


        public HRMReport(Session session) : base(session) { }
        public HRMReport(Session session, Type dataType)
            : base(session, dataType)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            MaTruong = TruongConfig.MaTruong;
        }

        public override XafReport LoadXtraReport(IObjectSpace objectSpace)
        {
            CustomXafReport report = (CustomXafReport)base.LoadXtraReport(objectSpace);
            if (report.DataType != null && report.DataType.BaseType == typeof(StoreProcedureReport))
            {
                List<StoreProcedureReport> dataSource = new List<StoreProcedureReport>();
                StoreProcedureReport.Param.FillDataSource();
                dataSource.Add(StoreProcedureReport.Param);
                report.DataSource = dataSource;
            }
            return report;
        }

        protected override XafReport CreateReport()
        {
            CustomXafReport report = new CustomXafReport
            {
                GroupReport = NhomBaoCao,
                TargetType = TargetType
            };            

            return report;
        }
    }
}
