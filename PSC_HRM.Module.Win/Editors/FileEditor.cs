using System;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors.Controls;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.Win.Forms;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using PSC_HRM.Module.GiayTo;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using DevExpress.Xpo;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.ExpressApp;

namespace PSC_HRM.Module.Win.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class FileEditor : DXPropertyEditor
    {
        public FileEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
            ControlBindingProperty = "Text";
        }

        protected override object CreateControlCore()
        {
            ButtonEdit ctrl = new ButtonEdit();
            ctrl.BackColor = Color.White;
            ctrl.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            ctrl.Properties.Buttons.Clear();
            EditorButton button1 = new EditorButton(ButtonPredefines.Ellipsis);
            button1.ToolTip = "Mở file";
            EditorButton button2 = new EditorButton(ButtonPredefines.Undo);
            button2.ToolTip = "Xem file";
            EditorButton button3 = new EditorButton(ButtonPredefines.Delete);
            button3.ToolTip = "Xóa file";
            ctrl.Properties.Buttons.Add(button1);
            ctrl.Properties.Buttons.Add(button2);
            ctrl.Properties.Buttons.Add(button3);
            ctrl.Properties.ButtonClick += ctrl_ButtonClick2;

            return ctrl;
        }

        protected override RepositoryItem CreateRepositoryItem()
        {
            return new RepositoryItemButtonEdit();
        }

        private void ctrl_ButtonClick2(object sender, ButtonPressedEventArgs e)
        {
            if (TruongConfig.MaTruong == "UEL")
            {
                #region Share file không tạo thư mục tự động
                ButtonEdit edit = (ButtonEdit)sender;

                if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    using (OpenFileDialog dialog = new OpenFileDialog())
                    {
                        dialog.Filter = "Pdf files (*.pdf)|*.pdf";
                        dialog.DefaultExt = "Pdf files (*.pdf)|*.pdf";
                        dialog.Multiselect = false;
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            edit.Text = Path.GetFileName(dialog.FileName);
                            FileInfo file = new FileInfo(HamDungChung.NoiLuuTruGiayTo + edit.Text);
                            if (!file.Exists)
                            {
                                file = new FileInfo(dialog.FileName);
                                file.CopyTo(HamDungChung.NoiLuuTruGiayTo + edit.Text, false);
                            }
                            else
                            {
                                DialogResult result = XtraMessageBox.Show("Giấy tờ hồ sơ đã tồn tại trên máy chủ. Bạn có muốn ghi đè không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                                if (result == DialogResult.Yes)
                                {
                                    file = new FileInfo(dialog.FileName);
                                    file.CopyTo(HamDungChung.NoiLuuTruGiayTo + edit.Text, true);
                                }
                            }
                        }
                    }
                }
                else if (e.Button.Kind == ButtonPredefines.Undo)
                {
                    //FileInfo file = new FileInfo(HamDungChung.NoiLuuTruGiayTo + edit.Text);
                    //if (file.Exists)
                    //{
                    //    Process.Start(new ProcessStartInfo(HamDungChung.NoiLuuTruGiayTo + edit.Text));
                    //}
                    //else
                    //{
                    //    XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}

                    try
                    {
                        GiayToHoSo giayToHoso = View.CurrentObject as GiayToHoSo;
                        byte[] data = FptProvider.DownloadFile(HamDungChung.NoiLuuTruGiayTo + giayToHoso.LuuTru, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password);
                        if (data != null)
                        {
                            string strTenFile = "TempFile.pdf";
                            //Lưu file vào thư mục bin\Debug
                            SaveFilePDF(data, strTenFile);

                            if (TruongConfig.MaTruong.Equals("BUH"))
                            {
                                //Mở bằng web theo đường dẫn ftp
                                //Process.Start(new ProcessStartInfo(giayToHoso.DuongDanFile));
                                //Mở file bằng chương trình
                                Process.Start(new ProcessStartInfo("TempFile.pdf"));
                            }
                            else
                            {
                                //Đọc file pdf
                                frmGiayToViewer viewer = new frmGiayToViewer("TempFile.pdf");
                                viewer.Show();
                            }

                        }
                        else
                            XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch
                    {
                        XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                #endregion
            }
            else if (TruongConfig.SoHoaTaiLieu.Equals("SharedFile"))
            {
                #region Shared file tạo thư mục tự động
                //Khởi tạo
                string tenLoaiGiayTo = string.Empty;
                string fileName_Next = string.Empty;
                ButtonEdit edit = (ButtonEdit)sender;

                //Lấy giấy tờ hồ sơ hiện tại
                GiayToHoSo giayToHoso = View.CurrentObject as GiayToHoSo;

                if (giayToHoso == null)
                    return;

                //Lấy tên loại giấy tờ
                if (giayToHoso.GiayTo != null)
                    if (TruongConfig.MaTruong != "UEL") //UEL không chia giấy tờ theo thư mục
                        tenLoaiGiayTo = LayTenLoaiGiayTo(giayToHoso);
                    else
                    {
                        DialogUtil.ShowError("Chưa chọn loại giấy tờ");
                        return;
                    }

                //Lấy đường dẫn máy chủ theo tên loại giấy tờ
                string filePath = string.Format("{0}{1}", HamDungChung.NoiLuuTruGiayTo, tenLoaiGiayTo);
                //string filePath = HamDungChung.NoiLuuTruGiayTo;

                if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    using (OpenFileDialog dialog = new OpenFileDialog())
                    {
                        dialog.Filter = "Pdf files (*.pdf)|*.pdf";
                        dialog.DefaultExt = "Pdf files (*.pdf)|*.pdf";
                        dialog.Multiselect = false;
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            edit.Text = Path.GetFileName(dialog.FileName);
                            {// Tạo thư mục theo loại giấy tờ trên máy chủ
                                FptProvider.CreateForder(filePath);
                            }

                            FileInfo file = new FileInfo((filePath + "\\" + edit.Text));
                            if (!file.Exists)
                            {
                                file = new FileInfo(dialog.FileName);
                                file.CopyTo(filePath + "\\" + edit.Text, false);
                            }
                            else
                            {
                                DialogResult result = XtraMessageBox.Show("Giấy tờ hồ sơ đã tồn tại trên máy chủ. Bạn có muốn ghi đè không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                                if (result == DialogResult.Yes)
                                {
                                    file = new FileInfo(dialog.FileName);
                                    file.CopyTo(filePath + "\\" + edit.Text, true);
                                }
                            }
                        }
                    }
                }
                else if (e.Button.Kind == ButtonPredefines.Undo)
                {
                    FileInfo file = new FileInfo(filePath + "\\" + edit.Text);
                    if (file.Exists)
                    {
                        Process.Start(new ProcessStartInfo(file.FullName));
                    }
                    else
                    {
                        XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                #endregion
            }
            else
            {               
                    #region FTP tạo thư mục tự động
                    //Khởi tạo
                    string tenLoaiGiayTo = string.Empty;
                    string fileName_Next = string.Empty;
                    ButtonEdit edit = (ButtonEdit)sender;

                    #region Giấy tờ hồ sơ
                    //Lấy giấy tờ hồ sơ hiện tại
                    GiayToHoSo giayToHoso = View.CurrentObject as GiayToHoSo;
                    //
                    if (giayToHoso != null)
                    {
                        //Lấy tên loại giấy tờ
                        if (giayToHoso.GiayTo != null)
                            tenLoaiGiayTo = LayTenLoaiGiayTo(giayToHoso);
                        else
                        {
                            DialogUtil.ShowError("Chưa chọn loại giấy tờ");
                            return;
                        }

                        //Lấy đường dẫn máy chủ theo tên loại giấy tờ
                        string filePath = string.Format("{0}{1}/", HamDungChung.NoiLuuTruGiayTo, tenLoaiGiayTo);
                        //
                        if (e.Button.Kind == ButtonPredefines.Ellipsis)
                        {
                            using (OpenFileDialog dialog = new OpenFileDialog())
                            {
                                dialog.Filter = "Pdf files (*.pdf)|*.pdf";
                                dialog.DefaultExt = "Pdf files (*.pdf)|*.pdf";
                                dialog.Multiselect = false;
                                if (dialog.ShowDialog() == DialogResult.OK)
                                {
                                    try
                                    {
                                        {// Tạo thư mục theo loại giấy tờ trên máy chủ
                                            FptProvider.CreateForder(filePath, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password);
                                        }

                                        {//Lấy tên file tự dộng
                                         //Lấy tên file gốc
                                            fileName_Next = Path.GetFileName(dialog.FileName).Replace(".pdf", String.Empty);
                                            //Xử lý dấu tiếng việt
                                            fileName_Next = StringHelper.ReplaceVietnameseChar(StringHelper.ToTitleCase(fileName_Next)).Replace(" ", String.Empty);
                                            //Lấy thêm số thứ tự file và tên loại file để tránh bị trùng
                                            fileName_Next = FptProvider.GetFileName_Next(tenLoaiGiayTo, giayToHoso.GiayTo.LoaiGiayTo.Oid, fileName_Next);

                                            if (!string.IsNullOrEmpty(fileName_Next))
                                            {
                                                fileName_Next = string.Format("{0}.pdf", fileName_Next);
                                            }
                                            else
                                            {
                                                DialogUtil.ShowError("Không thể lấy tên file mới nhất. Vui lòng liên lạc với quản trị phần mềm...");
                                            }
                                        }

                                        {//Tiến hành upload dữ liệu
                                            FptProvider.UploadFile(filePath, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password, dialog.FileName, fileName_Next);
                                        }

                                        {//Nếu upload thành công
                                            giayToHoso.DuongDanFile = string.Format("{0}{1}", filePath, fileName_Next);
                                            giayToHoso.LuuTru = string.Format("{0}/{1}", tenLoaiGiayTo, fileName_Next);
                                            giayToHoso.NgayLap = HamDungChung.GetServerTime();
                                        }

                                        DialogUtil.ShowInfo("Upload file thành công.");
                                    }
                                    catch (Exception ex)
                                    {
                                        DialogUtil.ShowError("Upload file không thành công: " + ex.Message);
                                    }
                                }
                            }
                        }
                        else if (e.Button.Kind == ButtonPredefines.Undo)
                        {
                            try
                            {
                                byte[] data = FptProvider.DownloadFile(giayToHoso.DuongDanFile, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password);
                                if (data != null)
                                {
                                    string strTenFile = "TempFile.pdf";
                                    //Lưu file vào thư mục bin\Debug
                                    SaveFilePDF(data, strTenFile);

                                    if (TruongConfig.MaTruong.Equals("BUH"))
                                    {
                                        //Mở bằng web theo đường dẫn ftp
                                        //Process.Start(new ProcessStartInfo(giayToHoso.DuongDanFile));
                                        //Mở file bằng chương trình
                                        Process.Start(new ProcessStartInfo("TempFile.pdf"));
                                    }
                                    else
                                    {
                                    //Đọc file pdf
                                    //frmGiayToViewer viewer = new frmGiayToViewer("TempFile.pdf");
                                    //viewer.Show();
                                    Process.Start(new ProcessStartInfo("TempFile.pdf"));
                                }

                                }
                                else
                                    XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            catch
                            {
                                XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else if (e.Button.Kind == ButtonPredefines.Delete)
                        {
                            try
                            {
                                using (DialogUtil.AutoWait())
                                {
                                    //Xóa file trên server
                                    if (!string.IsNullOrEmpty(giayToHoso.DuongDanFile))
                                    {
                                        //
                                        FptProvider.DeleteFileOnServer(giayToHoso.DuongDanFile, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password);
                                    }
                                    //
                                    DataProvider.ExecuteNonQuery("spd_GiayToHoSo_XoaGiayToHoSo", CommandType.StoredProcedure, new SqlParameter("@Oid", giayToHoso.Oid));
                                }
                                //
                                DialogUtil.ShowInfo("Xóa tập tin thành công!!!");
                            }
                            catch (Exception ex)
                            {

                                DialogUtil.ShowError("Không thể xóa file trên máy chủ...");
                            }
                        }
                    }
                    #endregion                   

                    #endregion               
            }
        }

        private void SaveFilePDF(byte[] File, string strTenFile)
        {
            try
            {
                FileStream fs = new FileStream(strTenFile, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter w = new BinaryWriter(fs);
                w.Flush();
                w.Write(File);
                w.Close();
            }
            catch
            {
                throw;
            }
        }

        private string LayTenLoaiGiayTo(GiayToHoSo giayToHoSo)
        {
            string tenLoaiGiayTo = string.Empty;

            if (giayToHoSo.GiayTo.LoaiGiayTo.TenLoaiGiayTo.Contains("Hồ sơ"))
            {
                tenLoaiGiayTo = "HoSo";
            }
            else if (giayToHoSo.GiayTo.LoaiGiayTo.TenLoaiGiayTo.Contains("Quyết định"))
            {
                tenLoaiGiayTo = "QuyetDinh";
            }
            else if (giayToHoSo.GiayTo.LoaiGiayTo.TenLoaiGiayTo.Contains("Văn bằng, chứng chỉ"))
            {
                tenLoaiGiayTo = "VanBangChungChi";
            }
            else if (giayToHoSo.GiayTo.LoaiGiayTo.TenLoaiGiayTo.Contains("Giấy tờ tùy thân"))
            {
                tenLoaiGiayTo = "GiayToTuyThan";
            }
            else if (giayToHoSo.GiayTo.LoaiGiayTo.TenLoaiGiayTo.Contains("Công nhận chức danh giáo sư, phó giáo sư"))
            {
                tenLoaiGiayTo = "CongNhanChucDanh";
            }
            else if (giayToHoSo.GiayTo.LoaiGiayTo.TenLoaiGiayTo.Contains("Hợp đồng lao động"))
            {
                tenLoaiGiayTo = "HopDongLaoDong";
            }
            else
            {
                tenLoaiGiayTo = "GiayToKhac";
            }

            return tenLoaiGiayTo;
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
            }
        }
       
    }

}
