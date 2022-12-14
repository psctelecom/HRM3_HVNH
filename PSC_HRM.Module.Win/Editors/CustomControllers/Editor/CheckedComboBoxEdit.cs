using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.CustomControllers.Editor
{
    public class CheckedComboBoxEditEditor : IEditor
    {
        public System.Windows.Forms.Control Control
        {
            get
            {
                return new CheckedComboBoxEdit();
            }
        }

        public RepositoryItem RepositoryItem
        {
            get
            {
                return new RepositoryItemComboBox();
            }
        }
    }
}
