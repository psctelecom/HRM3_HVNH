using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using System.Windows.Forms;

namespace PSC_HRM.Module.Win.CustomControllers.ButtonEdit
{
    [UserRepositoryItem("RegisterCustomButtonEdit")]
    public class RepositoryItemCustomButtonEdit : RepositoryItemButtonEdit
    {
        static RepositoryItemCustomButtonEdit()
        {
            RegisterCustomButtonEdit();
        }

        public RepositoryItemCustomButtonEdit()
        { }

        public static void RegisterCustomButtonEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo("CustomButtonEdit",
                typeof(CustomButtonEdit),
                typeof(RepositoryItemCustomButtonEdit),
                typeof(CustomButtonEditViewInfo),
                new DevExpress.XtraEditors.Drawing.ButtonEditPainter(),
                true, null, typeof(DevExpress.Accessibility.PopupEditAccessible)));
        }

        public override string EditorTypeName
        {
            get
            {
                return "CustomButtonEdit";
            }
        }

        protected override bool NeededKeysContains(Keys key)
        {
            if (key == Keys.Enter)
                return true;
            if (key == Keys.Tab)
                return true;
            if (key == Keys.Up)
                return true;
            if (key == Keys.Down)
                return true;
            return base.NeededKeysContains(key);
        }

        public override bool IsNeededKey(Keys keyData)
        {
            if (keyData == (Keys.Enter | Keys.Control))
                return false;
            bool res = base.IsNeededKey(keyData);
            if (res)
                return true;
            if (keyData == Keys.PageUp || keyData == Keys.PageDown)
                return true;
            return false;
        }

        public override bool AutoHeight
        {
            get
            {
                return false;
            }
        }

        protected override bool UseMaskBox
        {
            get
            {
                return false;
            }
        }
    }
}
