using System;


using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.Win.CustomControllers.Editor;

namespace PSC_HRM.Module.Win.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class DictioanryEditor : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.ComboBoxEditor);
        public DictioanryEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
            ControlBindingProperty = "Value";
        }

        protected override object CreateControlCore()
        {
            ComboBoxEdit control = editor.Control as ComboBoxEdit;
            if (control != null)
            {
                control.Properties.Sorted = true;
                var BO = View.Model.Application.BOModel;
                foreach (var item in BO)
                {
                    if (item.Name.Contains("DanhMuc"))
                    {
                        control.Properties.Items.Add(new ComboBoxItem(item.Caption));
                    }
                }
            }
            return editor.Control;
        }

        protected override RepositoryItem CreateRepositoryItem()
        {
            return editor.RepositoryItem;
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
