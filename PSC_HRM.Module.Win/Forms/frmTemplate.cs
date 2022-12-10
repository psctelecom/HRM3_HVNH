using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using System.Windows.Forms;
using PSC_HRM.Module.MailMerge;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.Forms
{
    public partial class frmTemplate : XtraForm
    {
        public event EventHandler<TemplateEventArgs> DefaultTemplateChanged;
        private MailMergeTemplate _Template;

        public frmTemplate(IObjectSpace obs, string templateName)
        {
            InitializeComponent();

            unitOfWork1 = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer);
            if (string.IsNullOrEmpty(templateName))
                Close();

            listTemplate.Session = unitOfWork1;
            listTemplate.Criteria = CriteriaOperator.Parse("MaQuanLy=?", templateName);
        }

        private void frmTemplate_Load(object sender, EventArgs e)
        {
            GridViewHelper.InitGridView(gridView1, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            GridViewHelper.ShowField(gridView1, new string[] { "TenTaiLieu", "SuDungMacDinh" },
                new string[] { "Tên biểu mẫu", "Sử dụng mặc định" },
                new int[] { 300, 100 });
            GridViewHelper.ReadOnlyColumns(gridView1, "SuDungMacDinh");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MailMergeTemplate template = gridView1.GetFocusedRow() as MailMergeTemplate;
            if (template != null)
            {
                frmCreateTemplate frm = new frmCreateTemplate(unitOfWork1, template);
                frm.TemplateSaved += frm_TemplateSaved;
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    XpoDefault.Session.DropIdentityMap();
                    listTemplate.Reload();
                }
            }
        }

        void frm_TemplateSaved(object sender, TemplateEventArgs e)
        {
            _Template = e.Template;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0
                && gridView1.GetSelectedRows().Length > 0)
            {
                if (XtraMessageBox.Show("Bạn có muốn xóa dòng được chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Module.MailMerge template = gridView1.GetFocusedRow() as Module.MailMerge;
                    //if (template != null)
                    //    unitOfWork1.Delete(template);
                    gridView1.DeleteSelectedRows();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            gridView1.FocusedRowHandle = -1;
            gridView1.UpdateCurrentRow();
            BindingContext[listTemplate].EndCurrentEdit();
            unitOfWork1.CommitChanges();

            OnDefaultTemplateChanged();
            HamDungChung.ShowSuccessMessage("Lưu biểu mẫu thành công");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnDefaultTemplateChanged()
        {
            if (DefaultTemplateChanged != null)
                DefaultTemplateChanged(this, new TemplateEventArgs(_Template));
        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            MailMergeTemplate template = gridView1.GetFocusedRow() as MailMergeTemplate;
            if (template != null)
            {
                foreach (MailMergeTemplate item in listTemplate)
                {
                    item.SuDungMacDinh = false;
                }
                template.SuDungMacDinh = true;
                _Template = template;
            }
        }
    }
}