using System;


using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.Win.CustomControllers.Editor;

namespace PSC_HRM.Module.Win.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class NamespaceEditor : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.ComboBoxEditor);

        public NamespaceEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
            ControlBindingProperty = "Value";
        }

        protected override object CreateControlCore()
        {
            List<string> name = new List<string>();
            ComboBoxEdit control = editor.Control as ComboBoxEdit;
            if (control != null)
            {
                control.Properties.Sorted = true;
                var BO = View.Model.Application.BOModel;
                foreach (var b in BO)
                {
                    if (b.Name.StartsWith("PSC_HRM"))
                    {
                        if (!name.Contains(b.TypeInfo.Type.Namespace))
                            name.Add(b.TypeInfo.Type.Namespace);
                    }
                }

                foreach (string i in name)
                    control.Properties.Items.Add(new ComboBoxItem(i));
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
