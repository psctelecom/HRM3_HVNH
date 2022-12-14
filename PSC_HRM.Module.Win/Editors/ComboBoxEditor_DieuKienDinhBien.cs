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

namespace PSC_HRM.Module.Win.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class ComboBoxEditor_DieuKienDinhBien : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.CheckedComboBoxEdit);
        CheckedComboBoxEdit _checkedListBox;
        CongViec _congViec;

        public ComboBoxEditor_DieuKienDinhBien(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
            ControlBindingProperty = "Value";
        }

        protected override object CreateControlCore()
        {
            _checkedListBox = editor.Control as CheckedComboBoxEdit;
            //
            if (_checkedListBox != null)
            {
                _checkedListBox.Properties.SelectAllItemCaption = "Tất cả";
                _checkedListBox.Properties.TextEditStyle = TextEditStyles.Standard;
                _checkedListBox.Properties.Items.Clear();

                List<CheckedListBoxItem> checkedListBoxItemList = new List<CheckedListBoxItem>();

                //Lấy công việc hiện tại
                if (View != null && View.Id.Contains("CongViec_DetailView"))
                {
                    _congViec = View.CurrentObject as CongViec;
                    //
                    if (_congViec != null)
                    {
                        //Thêm các item vào
                        AddItemComboBoxEdit(checkedListBoxItemList);
                    }
                }
                //
                _checkedListBox.Properties.Items.AddRange(checkedListBoxItemList.ToArray());
                _checkedListBox.Properties.SeparatorChar = ';';
                _checkedListBox.EditValueChanged += SetValueOfComboBox;
                _checkedListBox.CustomDisplayText += CustomDisplayText;
                _checkedListBox.Refresh();

            }
            return _checkedListBox;
        }
        private void CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            //e.DisplayText = _congViec.DieuKienDinhBien;
        }
        private void AddItemComboBoxEdit(List<CheckedListBoxItem> checkedListBoxItemList)
        {
                //
                if (_congViec != null && !string.IsNullOrEmpty(_congViec.DieuKienDinhBien) && _congViec.DieuKienDinhBien.Contains("TrinhDoChuyenMon"))
                     checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.TrinhDoChuyenMon, "Trình độ chuyên môn", CheckState.Checked, true));
                else
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.TrinhDoChuyenMon, "Trình độ chuyên môn", CheckState.Unchecked, true));
                //
                if (_congViec != null && !string.IsNullOrEmpty(_congViec.DieuKienDinhBien) && _congViec.DieuKienDinhBien.Contains("TrinhDoNgoaiNgu"))
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.TrinhDoNgoaiNgu, "Trình độ ngoại ngữ", CheckState.Checked, true));
                else
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.TrinhDoNgoaiNgu, "Trình độ ngoại ngữ", CheckState.Unchecked, true));
                //
                if (_congViec != null && !string.IsNullOrEmpty(_congViec.DieuKienDinhBien) && _congViec.DieuKienDinhBien.Contains("TrinhDoTinHoc"))
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.TrinhDoTinHoc, "Trình độ tin học", CheckState.Checked, true));
                else
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.TrinhDoTinHoc, "Trình độ tin học", CheckState.Unchecked, true));
                //
                if (_congViec != null && !string.IsNullOrEmpty(_congViec.DieuKienDinhBien) && _congViec.DieuKienDinhBien.Contains("HocHam"))
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.HocHam, "Học hàm", CheckState.Checked, true));
                else
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.HocHam, "Học hàm", CheckState.Unchecked, true));
                //
                if (_congViec != null && !string.IsNullOrEmpty(_congViec.DieuKienDinhBien) && _congViec.DieuKienDinhBien.Contains("LyLuanChinhTri"))
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.LyLuanChinhTri, "Lý luận chính trị", CheckState.Checked, true));
                else
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.LyLuanChinhTri, "Lý luận chính trị", CheckState.Unchecked, true));
                //
                if (_congViec != null && !string.IsNullOrEmpty(_congViec.DieuKienDinhBien) && _congViec.DieuKienDinhBien.Contains("LoaiNhanSu"))
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.LoaiBienChe, "Loại biên chế", CheckState.Checked, true));
                else
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.LoaiBienChe, "Loại biên chế", CheckState.Unchecked, true));
                //
                if (_congViec != null && !string.IsNullOrEmpty(_congViec.DieuKienDinhBien) && _congViec.DieuKienDinhBien.Contains("DanToc"))
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.DanToc, "Dân tộc", CheckState.Checked, true));
                else
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.DanToc, "Dân tộc", CheckState.Unchecked, true));
                //
                if (_congViec != null && !string.IsNullOrEmpty(_congViec.DieuKienDinhBien) && _congViec.DieuKienDinhBien.Contains("TonGiao"))
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.TonGiao, "Tôn giáo", CheckState.Checked, true));
                else
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.TonGiao, "Tôn giáo", CheckState.Unchecked, true));
                //
                if (_congViec != null && !string.IsNullOrEmpty(_congViec.DieuKienDinhBien) && _congViec.DieuKienDinhBien.Contains("DangVien"))
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.DangVien, "Đảng viên", CheckState.Checked, true));
                else
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.DangVien, "Đảng viên", CheckState.Unchecked, true));
                //
                if (_congViec != null && !string.IsNullOrEmpty(_congViec.DieuKienDinhBien) && _congViec.DieuKienDinhBien.Contains("DoanVien"))
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.DoanVien, "Đoàn viên", CheckState.Checked, true));
                else
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.DoanVien, "Đoàn viên", CheckState.Unchecked, true));
                //
                if (_congViec != null && !string.IsNullOrEmpty(_congViec.DieuKienDinhBien) && _congViec.DieuKienDinhBien.Contains("BienChe"))
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.BienChe, "Biên chế", CheckState.Checked, true));
                else
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.BienChe, "Biên chế", CheckState.Unchecked, true));
                //
                if (_congViec != null && !string.IsNullOrEmpty(_congViec.DieuKienDinhBien) && _congViec.DieuKienDinhBien.Contains("SoNamKinhNghiem"))
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.SoNamKinhNghiem, "Số năm kinh nghiệm", CheckState.Checked, true));
                else
                    checkedListBoxItemList.Add(new CheckedListBoxItem(DieuKienDinhBienEnum.SoNamKinhNghiem, "Số năm kinh nghiệm", CheckState.Unchecked, true));
           
        }

        private void SetValueOfComboBox(object sender, EventArgs e)
        {
            //Lấy chứng từ hiện tại
            if (View != null && View.Id.Contains("CongViec_DetailView") && _checkedListBox != null)
            {
                _congViec = View.CurrentObject as CongViec;
                _congViec.DieuKienDinhBien = (string)_checkedListBox.EditValue.ToString().Trim();
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
