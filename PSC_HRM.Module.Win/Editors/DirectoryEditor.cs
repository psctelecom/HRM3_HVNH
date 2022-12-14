//Chú ý : khi sử dụng editor này cần viết thêm lệnh gán ObjectType vào bên dưới khi user click chọn field dữ liệu
using System;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Win.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class DirectoryEditor: DXPropertyEditor
    {
        public DirectoryEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        { 
            ControlBindingProperty = "Value"; 
        }

        protected override object CreateControlCore()
        {
            ButtonEdit ctrl = new ButtonEdit();
            ctrl.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            ctrl.ButtonClick += ctrl_ButtonClick2;
            
            return ctrl;
        }


        void ctrl_ButtonClick2(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if ((sender as ButtonEdit).Properties.ReadOnly) 
                return;
            
            ButtonEdit edit = (ButtonEdit)sender;
            string txt = edit.Text;
            if (String.IsNullOrEmpty(txt))
            {
                FolderBrowserDialog browser = new FolderBrowserDialog();
                if (browser.ShowDialog() == DialogResult.OK)
                {
                    edit.Text = browser.SelectedPath;
                }
            }
            else
            {
                DirectoryInfo dir = new DirectoryInfo(txt);
                if (dir.Exists)
                {
                    Process.Start(new ProcessStartInfo(txt));
                }
                else
                {
                    switch (MessageBox.Show("Đường dẫn không hợp lệ. Bạn có muốn chọn lại đường dẫn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    { 
                        case DialogResult.Yes:
                            FolderBrowserDialog browser = new FolderBrowserDialog();
                            if (browser.ShowDialog() == DialogResult.OK)
                            {
                                edit.Text = browser.SelectedPath;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
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
                Control.Enabled = AllowEdit;
            }
        }
    }

}
