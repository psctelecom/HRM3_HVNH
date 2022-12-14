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

namespace ERP.Module.Win.Editors.PMS
{
    [PropertyEditor(typeof(String), false)]
    public class chkComboxEdit_DinhMucChucVu : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.CheckedComboBoxEdit);
        CheckedComboBoxEdit checkedListBox;
        object _obj;

        public chkComboxEdit_DinhMucChucVu(Type objectType, IModelMemberViewItem model)
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
                    if ((_obj as DinhMucChucVu_NhanVien) != null)
                    {
                        //Thêm các item vào
                        AddItemComboBoxEdit(checkedListBoxItemList);
                    }
                }

                //Check các item
                foreach (CheckedListBoxItem item in checkedListBoxItemList)
                {
                    if ((_obj as DinhMucChucVu_NhanVien) != null && !string.IsNullOrEmpty((_obj as DinhMucChucVu_NhanVien).DinhMucChucVu) && (_obj as DinhMucChucVu_NhanVien).DinhMucChucVu.Contains(string.Format("{0}", item.Value)))
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
            XPCollection<DinhMucChucVu> listLoaiPhi = new XPCollection<DinhMucChucVu>(((XPObjectSpace)View.ObjectSpace).Session);
            //listLoaiPhi.Criteria = CriteriaOperator.Parse("NhomDinhPhi <> ? and NgungSuDung <> 1", ERP.Module.Enum.HocPhi.NhomDinhPhiEnum.DongPhucHocPham);
            //listLoaiPhi.Sorting.Add(new SortProperty("DoUuTien", DevExpress.Xpo.DB.SortingDirection.Ascending));
            foreach (DinhMucChucVu item in listLoaiPhi)
            {
                checkedListBoxItemList.Add(new CheckedListBoxItem(item.Oid, item.ChucVu != null ? item.ChucVu.TenChucVu + " " + item.GhiChu : item.GhiChu, CheckState.Unchecked, true));
            }
        }

        private void SetValueOfComboBox(object sender, EventArgs e)
        {
            //Lấy
            if ((_obj as DinhMucChucVu_NhanVien) != null && checkedListBox != null)
            {
                (_obj as DinhMucChucVu_NhanVien).DinhMucChucVu = checkedListBox.EditValue.ToString().Trim().Replace(" ", "");
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
