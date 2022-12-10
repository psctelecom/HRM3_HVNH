using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.Win.CustomControllers.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.Win.Editors.PMS
{
    [PropertyEditor(typeof(String), false)]
    public class chkComboxEdit_BoPhan_SoGio : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.CheckedComboBoxEdit);
        CheckedComboBoxEdit checkedListBox;
        object _obj;

        public chkComboxEdit_BoPhan_SoGio(Type objectType, IModelMemberViewItem model)
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
                    if (_obj != null)
                    {
                        //Thêm các item vào
                        AddItemComboBoxEdit(checkedListBoxItemList);
                    }
                }
                foreach (CheckedListBoxItem item in checkedListBoxItemList)
                {
                    if ((_obj as PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.ChonBoPhan_SoGio) != null
                        && !string.IsNullOrEmpty((_obj as PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.ChonBoPhan_SoGio).BoPhan)
                        && (_obj as PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.ChonBoPhan_SoGio).BoPhan.Contains(string.Format("{0}", item.Value)))
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

                checkedListBox.Properties.Items.AddRange(checkedListBoxItemList.ToArray());
                checkedListBox.Properties.SeparatorChar = ';';

                //checkedListBox.CheckAll();//Check tất cả nhân viên

                checkedListBox.EditValueChanged += SetValueOfComboBox;
                checkedListBox.Refresh();
            }
            return checkedListBox;
        }

        private void AddItemComboBoxEdit(List<CheckedListBoxItem> checkedListBoxItemList)
        {
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
            if ((_obj as PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.ChonBoPhan_SoGio) != null && checkedListBox != null)
            {
                (_obj as PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.ChonBoPhan_SoGio).BoPhan = checkedListBox.EditValue.ToString().Trim().Replace(" ", "");
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
