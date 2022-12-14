
using System;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using DevExpress.ExpressApp.Model;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraCharts.Native;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using PSC_HRM.Module.Win.CustomControllers.Editor;
using PSC_HRM.Module.PMS.BusinessObjects.DanhMuc;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.NonPersistent;

namespace ERP.Module.Win.Editors.PMS
{
    [PropertyEditor(typeof(String), false)]
    public class chkComboxEdit_DonVi : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.CheckedComboBoxEdit);
        CheckedComboBoxEdit checkedListBox;
        object _obj;

        public chkComboxEdit_DonVi(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
            ControlBindingProperty = "Value";
        }

        protected override object CreateControlCore()
        {
            checkedListBox = editor.Control as CheckedComboBoxEdit;
            //
            if (checkedListBox != null)
            {
                checkedListBox.Properties.SelectAllItemCaption = "Tất cả";
                checkedListBox.Properties.TextEditStyle = TextEditStyles.Standard;
                checkedListBox.Properties.Items.Clear();

                List<CheckedListBoxItem> checkedListBoxItemList = new List<CheckedListBoxItem>();
                
                //Lấy
                if (View != null)
                {
                    _obj = View.CurrentObject;
                    //
                    if ((_obj as ChonNhanVien_PhanQuyenPMS) != null)
                    {
                        //Thêm các item vào
                        AddItemComboBoxEdit(checkedListBoxItemList);
                    }
                }

                //Check các item
                foreach (CheckedListBoxItem item in checkedListBoxItemList)
                {
                    if ((_obj as ChonNhanVien_PhanQuyenPMS) != null && !string.IsNullOrEmpty((_obj as ChonNhanVien_PhanQuyenPMS).BoPhanImport))
                    {
                        item.CheckState = CheckState.Checked;
                    }
                    else
                    { item.CheckState = CheckState.Unchecked; }
                }
                //
                checkedListBox.Properties.Items.AddRange(checkedListBoxItemList.ToArray());
                checkedListBox.Properties.SeparatorChar = ';';
                checkedListBox.EditValueChanged += SetValueOfComboBox;
                checkedListBox.Refresh();
            }
            return checkedListBox;
        }

        private void AddItemComboBoxEdit(List<CheckedListBoxItem> checkedListBoxItemList)
        {
            //Thêm các item 
            XPCollection<BoPhan> listDonVi = new XPCollection<BoPhan>(((XPObjectSpace)View.ObjectSpace).Session);
            //listDonVi.CriteriaString = "LoaiTruong = 1";
            foreach (BoPhan item in listDonVi)
            {
                checkedListBoxItemList.Add(new CheckedListBoxItem(item.Oid, item.TenBoPhan, CheckState.Unchecked, true));
            }
        }

        private void SetValueOfComboBox(object sender, EventArgs e)
        {
            //Lấy
            if ((_obj as ChonNhanVien_PhanQuyenPMS) != null && checkedListBox != null)
            {
                (_obj as ChonNhanVien_PhanQuyenPMS).BoPhanImport = checkedListBox.EditValue.ToString().Trim().Replace(" ", "");
            }
            
        }

        protected override DevExpress.XtraEditors.Repository.RepositoryItem CreateRepositoryItem()
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
                Control.Enabled = true;
                Control.Properties.ReadOnly = false;
            }
        }
    }

}
