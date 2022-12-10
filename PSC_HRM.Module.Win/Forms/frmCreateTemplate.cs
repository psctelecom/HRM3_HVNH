using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using PSC_HRM.Module;
using DevExpress.Xpo;
using PSC_HRM.Module.MailMerge;

namespace PSC_HRM.Module.Win.Forms
{
    public partial class frmCreateTemplate : XtraForm
    {
        public event EventHandler<TemplateEventArgs> TemplateSaved;

        private MailMergeTemplate _Template;
        private MailMergeTemplate _NewTemplate;

        public frmCreateTemplate(UnitOfWork uow, MailMergeTemplate template)
        {
            InitializeComponent();

            unitOfWork1 = uow;
            _Template = template;
            _NewTemplate = HamDungChung.Copy<MailMergeTemplate>(uow, template);

            if (_NewTemplate == null)
                Close();

            obj.Session = uow;
            obj.Criteria = CriteriaOperator.Parse("Oid=?", Guid.Empty);
            if (obj.Count == 0)
                obj.Add(_NewTemplate);
            layoutControl1.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            unitOfWork1.CommitChanges();
            OnTemplateSaved();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnTemplateSaved()
        {
            if (TemplateSaved != null)
                TemplateSaved(this, new TemplateEventArgs(_NewTemplate));
        }
    }

    public class TemplateEventArgs : EventArgs
    {
        public MailMergeTemplate Template { get; private set; }

        public TemplateEventArgs(MailMergeTemplate template)
        {
            Template = template;
        }
    }
}