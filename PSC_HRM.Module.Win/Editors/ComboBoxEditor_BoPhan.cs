//Chú ý : khi sử dụng editor này cần viết thêm lệnh gán ObjectType vào bên dưới khi user click chọn field dữ liệu
using System;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.Win.CustomControllers.Editor;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using PSC_HRM.Module.ThuNhap;
using DevExpress.XtraCharts.Native;
using PSC_HRM.Module.ThuNhap.ChungTu;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.ReportClass;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp;
using DevExpress.Xpo.DB;

namespace PSC_HRM.Module.Win.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class ComboBoxEditor_BoPhan : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.CheckedComboBoxEdit);
        CheckedComboBoxEdit checkedListBox;
        //Report_ThongKe_DanhSachTongHopDonVi _Report;

        public ComboBoxEditor_BoPhan(Type objectType, IModelMemberViewItem model)
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
                //_Report = View.CurrentObject as Report_ThongKe_DanhSachTongHopDonVi;
                //
                //if (_Report != null)
                //{
                //    //Thêm các item vào
                //    AddItemComboBoxEdit(checkedListBoxItemList,_Report);
                //}
                
                //Check các item
                if (TruongConfig.MaTruong == "BUH")
                {
                    foreach (CheckedListBoxItem item in checkedListBoxItemList)
                    {
                        //if ((_Report != null && !string.IsNullOrEmpty(_Report.BoPhan) && _Report.BoPhan.Equals(string.Format("{0}", item.Value))))
                        //{
                        //    item.CheckState = CheckState.Checked;
                        //}
                        //else
                        {
                            item.CheckState = CheckState.Unchecked; 
                        }
                    }
                }
               
                checkedListBox.Properties.Items.AddRange(checkedListBoxItemList.ToArray());
                checkedListBox.Properties.SeparatorChar = ';';
                checkedListBox.EditValueChanged += SetValueOfComboBox;
                checkedListBox.Refresh();

            }
            return checkedListBox;
        }

        private static void AddItemComboBoxEdit(List<CheckedListBoxItem> checkedListBoxItemList,Report_ThongKe_DanhSachTongHopDonVi report)
        {
            //Thêm các item 
            if (TruongConfig.MaTruong == "BUH")
            {
                XPCollection<BoPhan> ListBoPhan = new XPCollection<BoPhan>(report.Session);
                ListBoPhan.Sorting.Add(new SortProperty("STT", SortingDirection.Ascending));
                foreach (var item in ListBoPhan)
                {
                    checkedListBoxItemList.Add(new CheckedListBoxItem(item.TenBoPhan, item.TenBoPhan, CheckState.Unchecked, true));
                }
            }
        }

        private void SetValueOfComboBox(object sender, EventArgs e)
        {
            _Report = View.CurrentObject as Report_ThongKe_DanhSachTongHopDonVi;
            _Report.BoPhan = (string)checkedListBox.EditValue.ToString().Trim();
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
