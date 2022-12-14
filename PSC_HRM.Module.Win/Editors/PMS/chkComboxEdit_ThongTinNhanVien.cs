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
using PSC_HRM.Module.HoSo;
using DevExpress.Xpo.DB;
//using PSC_HRM.Module.PMS.NghiepVu;

namespace ERP.Module.Win.Editors.PMS
{
    [PropertyEditor(typeof(String), false)]
    public class chkComboxEdit_ThongTinNhanVien : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.CheckedComboBoxEdit);
        CheckedComboBoxEdit checkedListBox;
        object _obj;

        public chkComboxEdit_ThongTinNhanVien(Type objectType, IModelMemberViewItem model)
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
            //CriteriaOperator f = CriteriaOperator.Parse("LoaiNhanSu.MaQuanLy = 'GV'");
            //XPCollection<ThongTinNhanVien> listThongTinNhanVien = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)View.ObjectSpace).Session, f);
            //SortingCollection sortCollection = new SortingCollection();
            //sortCollection.Add(new SortProperty("Ten", SortingDirection.Ascending));
            //listThongTinNhanVien.Sorting = sortCollection;
            //foreach (ThongTinNhanVien ttnv in listThongTinNhanVien)
            //{
            //    checkedListBoxItemList.Add(new CheckedListBoxItem(ttnv.Oid, ttnv.HoTen, CheckState.Checked, true));
            //    (_obj as KhoiLuongGiangDay).NhanVien += ttnv.Oid + ";";
            //}           
        }

        private void SetValueOfComboBox(object sender, EventArgs e)
        {
            //var key = _obj;
            //if (checkedListBox != null)
            //{
            //    (_obj as KhoiLuongGiangDay).NhanVien = checkedListBox.EditValue.ToString().Trim().Replace(" ", "");               
            //}
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
