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
using ERP.Module.Win.Editors.Commons;
using ERP.Module.NghiepVu.HocPhi.DinhPhi;
using ERP.Module.DanhMuc.HocPhi;
using ERP.Module.NonPersistentObjects.HocPhi;
using DevExpress.Data.Filtering;

namespace ERP.Module.Win.Editors.HocPhi
{
    [PropertyEditor(typeof(String), false)]
    public class chkComboxEdit_LoaiPhi : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.CheckedComboBoxEdit);
        CheckedComboBoxEdit checkedListBox;
        object _obj;

        public chkComboxEdit_LoaiPhi(Type objectType, IModelMemberViewItem model)
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
                    if ((_obj as HocPhi_DanhSachChuaDongPhi) != null)
                    {
                        //Thêm các item vào
                        AddItemComboBoxEdit(checkedListBoxItemList);
                    }
                }

                //Check các item
                foreach (CheckedListBoxItem item in checkedListBoxItemList)
                {
                    if ( (_obj as HocPhi_DanhSachChuaDongPhi) != null && !string.IsNullOrEmpty((_obj as HocPhi_DanhSachChuaDongPhi).LoaiPhi) && (_obj as HocPhi_DanhSachChuaDongPhi).LoaiPhi.Contains(string.Format("{0}", item.Value)) )
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
            XPCollection<LoaiPhi> listLoaiPhi = new XPCollection<LoaiPhi>(((XPObjectSpace)View.ObjectSpace).Session);
            listLoaiPhi.Criteria = CriteriaOperator.Parse("NhomDinhPhi <> ? and NgungSuDung <> 1", ERP.Module.Enum.HocPhi.NhomDinhPhiEnum.DongPhucHocPham);
            listLoaiPhi.Sorting.Add(new SortProperty("DoUuTien", DevExpress.Xpo.DB.SortingDirection.Ascending));
            foreach (LoaiPhi item in listLoaiPhi)
            {
                checkedListBoxItemList.Add(new CheckedListBoxItem(item.Oid, item.TenLoaiPhi, CheckState.Unchecked, true));
            }
        }

        private void SetValueOfComboBox(object sender, EventArgs e)
        {
            //Lấy
            if ((_obj as HocPhi_DanhSachChuaDongPhi) != null && checkedListBox != null)
            {
                (_obj as HocPhi_DanhSachChuaDongPhi).LoaiPhi = checkedListBox.EditValue.ToString().Trim().Replace(" ", "");
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
