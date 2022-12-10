using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSC_HRM.Module.Win.CustomControllers.Editor
{
    public class TextEditor : IEditor
    {
        public System.Windows.Forms.Control Control
        {
            get
            {
                return new TextEdit();
            }
        }

        public RepositoryItem RepositoryItem
        {
            get
            {
                return new RepositoryItemTextEdit();
            }
        }
    }
}
