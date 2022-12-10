using System;
using System.Linq;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.Report;
using DevExpress.XtraEditors.Controls;

namespace PSC_HRM.Module.Win.Editors
{
    [PropertyEditor(typeof(Type), false)]
    public class ReportTypeEditor : DXPropertyEditor
    {
        public ReportTypeEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
            ControlBindingProperty = "Value";
        }         

        protected override object CreateControlCore()
        {
            return new ImageComboBoxEdit();
        }

        protected override RepositoryItem CreateRepositoryItem()
        {
            return new RepositoryItemImageComboBox();
        }

        protected override void SetupRepositoryItem(RepositoryItem item)
        {
            var data = from b in Model.Application.BOModel.AsParallel()
                           where b.TypeInfo.Type.Namespace.Contains("PSC_HRM") == true &&
                                b.TypeInfo.Base.Type == typeof(StoreProcedureReport)
                           orderby b.Caption
                           select new
                           {
                               Caption = b.Caption,
                               Type = b.TypeInfo.Type
                           };
            
            ((RepositoryItemComboBox)item).Sorted = true;
            foreach (var i in data)
            {
                ((RepositoryItemImageComboBox)item).Items.Add(new ImageComboBoxItem(i.Caption, i.Type));
            }
        }

        protected override void OnControlCreated()
        {
            base.OnControlCreated();
            UpdateControlEnabled();
        }

        protected override void OnAllowEditChanged()
        {
            base.OnAllowEditChanged();
            UpdateControlEnabled();
        }

        private void UpdateControlEnabled()
        {
            if (Control != null)
            {
                Control.Enabled = AllowEdit;
            }
        }
    }
}
