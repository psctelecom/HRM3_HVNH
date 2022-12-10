using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;

namespace PSC_HRM.Module.Win.CustomControllers.ButtonEdit
{
    public class CustomButtonEdit : DevExpress.XtraEditors.ButtonEdit
    {
        static CustomButtonEdit()
        {
            RepositoryItemCustomButtonEdit.RegisterCustomButtonEdit();
        }

        public CustomButtonEdit()
        { }

        protected override void UpdateMaskBoxProperties(bool always)
        {
            base.UpdateMaskBoxProperties(always);

            if (MaskBox == null)
                return;
            if (always || !MaskBox.Multiline)
                MaskBox.Multiline = true;
            if (always || !MaskBox.WordWrap)
                MaskBox.WordWrap = true;
            if (always || !MaskBox.AcceptsTab)
                MaskBox.AcceptsTab = true;
            if (always || !MaskBox.AcceptsReturn)
                MaskBox.AcceptsReturn = true;
            MaskBox.ReadOnly = false;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            bool result = base.IsInputKey(keyData);
            if (result)
                return true;
            if (keyData == Keys.Enter)
                return true;
            if (keyData == Keys.Tab)
                return true;
            return result;
        }

        public override string EditorTypeName
        {
            get
            {
                return "CustomButtonEdit";
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemCustomButtonEdit Properties
        {
            get
            {
                return base.Properties as RepositoryItemCustomButtonEdit;
            }
        }

        protected override bool AcceptsReturn
        {
            get
            {
                return true;
            }
        }

        protected override bool AcceptsTab
        {
            get
            {
                return true;
            }
        }
    }
}
