using System;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Model;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.Win.CustomControllers.Editor;
using PSC_HRM.Module.PMS.GioChuan;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using DevExpress.Xpo.DB;

namespace ERP.Module.Win.Editors.PMS
{
    [PropertyEditor(typeof(String), false)]
    public class chkComboxEdit_ChonBoPhan : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.CheckedComboBoxEdit);
        CheckedComboBoxEdit checkedListBox;
        object _obj;

        public chkComboxEdit_ChonBoPhan(Type objectType, IModelMemberViewItem model)
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
                    if ((_obj as ChonBoPhan_SoGio) != null)
                    {
                        //Thêm các item vào
                        AddItemComboBoxEdit(checkedListBoxItemList);
                    }
                }

                //Check các item
                foreach (CheckedListBoxItem item in checkedListBoxItemList)
                {
                    if ((_obj as ChonBoPhan_SoGio) != null)
                    {
                        item.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        item.CheckState = CheckState.Unchecked;
                    }
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
            XPCollection<BoPhan> listBoPhan = new XPCollection<BoPhan>(((XPObjectSpace)View.ObjectSpace).Session);
            listBoPhan.Criteria = CriteriaOperator.Parse("LoaiBoPhan <> ? and LoaiBoPhan =? ", LoaiBoPhanEnum.Truong, LoaiBoPhanEnum.PhongBan);
            if (listBoPhan != null)
            {
                SortingCollection sortCollection = new SortingCollection();
                sortCollection.Add(new SortProperty("TenBoPhan", SortingDirection.Ascending));
                listBoPhan.Sorting = sortCollection;
                foreach (BoPhan bp in listBoPhan)
                {
                    checkedListBoxItemList.Add(new CheckedListBoxItem(bp.Oid, bp.TenBoPhan, CheckState.Checked, true));
                    (_obj as PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.ChonBoPhan_SoGio).BoPhan += bp.Oid + ";";
                }
            }
        }

        private void SetValueOfComboBox(object sender, EventArgs e)
        {
            //Lấy
            if ((_obj as ChonBoPhan_SoGio) != null && checkedListBox != null)
            {
                (_obj as ChonBoPhan_SoGio).BoPhan = checkedListBox.EditValue.ToString().Trim().Replace(" ", "");
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
