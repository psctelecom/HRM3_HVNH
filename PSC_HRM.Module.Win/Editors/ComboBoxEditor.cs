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
    public class ComboBoxEditor : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.CheckedComboBoxEdit);
        CheckedComboBoxEdit checkedListBox;
        ChuyenKhoanLuongNhanVien _chungTuChuyenKhoan;
        ChiTMLuongNhanVien _chungTuTienMat;

        public ComboBoxEditor(Type objectType, IModelMemberViewItem model)
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

                //Lấy chứng từ hiện tại
                if (View != null && View.Id.Contains("ChuyenKhoanLuongNhanVien_DetailView"))
                {
                    _chungTuChuyenKhoan = View.CurrentObject as ChuyenKhoanLuongNhanVien;
                    //
                    if (_chungTuChuyenKhoan != null)
                    {
                        //Thêm các item vào
                        AddItemComboBoxEdit(checkedListBoxItemList);
                    }
                }
                //Lấy chứng từ hiện tại
                if (View != null && View.Id.Contains("ChiTMLuongNhanVien_DetailView"))
                {
                    _chungTuTienMat = View.CurrentObject as ChiTMLuongNhanVien;
                    //
                    if (_chungTuTienMat != null)
                    {
                        //Thêm các item vào
                        AddItemComboBoxEdit(checkedListBoxItemList);
                    }

                }

                //Check các item
                if (TruongConfig.MaTruong == "IUH")
                {
                    foreach (CheckedListBoxItem item in checkedListBoxItemList)
                    {
                        if ((_chungTuChuyenKhoan != null && !string.IsNullOrEmpty(_chungTuChuyenKhoan.LoaiChi) && _chungTuChuyenKhoan.LoaiChi.Equals(string.Format("{0}", item.Value)))
                            || (_chungTuTienMat != null && !string.IsNullOrEmpty(_chungTuTienMat.LoaiChi) && _chungTuTienMat.LoaiChi.Equals(string.Format("{0}", item.Value))))
                        {
                            item.CheckState = CheckState.Checked;
                        }
                        else
                        { item.CheckState = CheckState.Unchecked; }
                    }
                }
                else
                {
                    foreach (CheckedListBoxItem item in checkedListBoxItemList)
                    {
                        if ((_chungTuChuyenKhoan != null && !string.IsNullOrEmpty(_chungTuChuyenKhoan.LoaiChi) && _chungTuChuyenKhoan.LoaiChi.Contains(string.Format("{0}", item.Value)))
                            || (_chungTuTienMat != null && !string.IsNullOrEmpty(_chungTuTienMat.LoaiChi) && _chungTuTienMat.LoaiChi.Contains(string.Format("{0}", item.Value))))
                        {
                            item.CheckState = CheckState.Checked;
                        }
                        else
                        { item.CheckState = CheckState.Unchecked; }
                    }
                }

                checkedListBox.Properties.Items.AddRange(checkedListBoxItemList.ToArray());
                checkedListBox.Properties.SeparatorChar = ';';
                checkedListBox.EditValueChanged += SetValueOfComboBox;
                checkedListBox.Refresh();

            }
            return checkedListBox;
        }

        private static void AddItemComboBoxEdit(List<CheckedListBoxItem> checkedListBoxItemList)
        {
            //Thêm các item 
            if (TruongConfig.MaTruong == "IUH")
            {
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.LuongKy1, "Lương kỳ 1", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.LuongKy2, "Lương kỳ 2", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.PhuCap, "Lương tiến sĩ", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.LuongThuViec, "Lương thử việc", CheckState.Unchecked, true));
                //checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.ThuNhapKhac, "Thu nhập khác", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.BoSungLuongKy1, "Bổ sung lương kỳ 1", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.BoSungPhuCapTrachNhiem, "Bổ sung phụ cấp trách nhiệm", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.BoSungPhuCapThamNien, "Bổ sung phụ cấp thâm niên", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.BoSungLuongKy2, "Bổ sung lương kỳ 2", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.BoSungNangLuongKy1, "Bổ sung nâng lương kỳ 1", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.BoSungNangLuongKy2, "Bổ sung nâng lương kỳ 2", CheckState.Unchecked, true));
            }           
            else if (TruongConfig.MaTruong == "QNU")
            {
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.LuongVaPhuCap, "Lương và phụ cấp", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.ThuNhapTangThem, "Thu nhập tăng thêm", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.NgoaiGio, "Ngoài giờ", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.KhauTruLuong, "Khấu trừ lương", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.TruyLinh, "Truy lĩnh", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.ThuNhapKhac, "Thu nhập khác", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.TruyThu, "Truy thu", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.PhucVuDaoTao, "Tiền phục vụ đào tạo", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.TrachNhiemQuanLy, "Tiền trách nhiệm quản lý", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.DienThoai, "Tiền điện thoại công vụ", CheckState.Unchecked, true));
            }            
            else 
            {
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.LuongKy1, "Lương nhân viên", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.TruyLinh, "Truy lĩnh", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.ThuNhapTangThem, "Thu nhập tăng thêm", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.NgoaiGio, "Ngoài giờ", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.KhauTruLuong, "Khấu trừ lương", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.PhuCap, "Phụ cấp", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.KhenThuong, "Khen thưởng", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.ThuNhapKhac, "Thu nhập khác", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.TruyThu, "Truy thu", CheckState.Unchecked, true));
                checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.ThuLao, "Thù lao", CheckState.Unchecked, true));
            }
        }

        private void SetValueOfComboBox(object sender, EventArgs e)
        {
            //Lấy chứng từ hiện tại
            if (View != null && View.Id.Contains("ChuyenKhoanLuongNhanVien_DetailView") && checkedListBox != null)
            {
                _chungTuChuyenKhoan = View.CurrentObject as ChuyenKhoanLuongNhanVien;
                _chungTuChuyenKhoan.LoaiChi = (string)checkedListBox.EditValue.ToString().Trim();

                //Lấy ngân hàng chuyển tiền
                if (TruongConfig.MaTruong.Equals("IUH"))
                {
                    if (   _chungTuChuyenKhoan.LoaiChi.Contains(string.Format("{0}", LoaiChiEnum.LuongKy1))
                           || _chungTuChuyenKhoan.LoaiChi.Contains(string.Format("{0}", LoaiChiEnum.BoSungLuongKy1))
                           || _chungTuChuyenKhoan.LoaiChi.Contains(string.Format("{0}", LoaiChiEnum.BoSungPhuCapThamNien))
                           || _chungTuChuyenKhoan.LoaiChi.Contains(string.Format("{0}", LoaiChiEnum.BoSungPhuCapTrachNhiem))
                           || _chungTuChuyenKhoan.LoaiChi.Contains(string.Format("{0}", LoaiChiEnum.BoSungNangLuongKy1))
                       
                        )
                    {
                        CriteriaOperator filter = CriteriaOperator.Parse("TenNganHang like ?", "Ngân hàng TMCP Công Thương Việt Nam Chi nhánh 7 - Tp. Hồ Chí Minh");
                        NganHang nganHang = View.ObjectSpace.FindObject<NganHang>(filter);
                        if (nganHang != null)
                            _chungTuChuyenKhoan.NganHang = nganHang;
                    }
                    else 
                    {
                        CriteriaOperator filter = CriteriaOperator.Parse("TenNganHang like ?", "Ngân hàng Nông nghiệp và Phát triển Nông thôn Chi nhánh Sài Gòn");
                        NganHang nganHang = View.ObjectSpace.FindObject<NganHang>(filter);
                        if (nganHang != null)
                            _chungTuChuyenKhoan.NganHang = nganHang;
                    }
                }  
            }
            if (View != null && View.Id.Contains("ChiTMLuongNhanVien_DetailView") && checkedListBox != null)
            {
                _chungTuTienMat = View.CurrentObject as ChiTMLuongNhanVien;
                _chungTuTienMat.LoaiChi = (string)checkedListBox.EditValue.ToString().Trim();
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
