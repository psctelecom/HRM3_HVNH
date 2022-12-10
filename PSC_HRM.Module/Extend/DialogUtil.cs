using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;

namespace PSC_HRM.Module
{
    public class DialogUtil
    {
        public static AutoWaitForm AutoWait(string message = "Đang xử lý...")
        {//
            return new AutoWaitForm(message, "Vui lòng chờ!");
        }
        public static WaitDialogForm Wait(String message, String title)
        {
            return new WaitDialogForm(message, title);
        }
        public static WaitDialogForm WaitForSave(String message = "")
        {
            return new WaitDialogForm(String.Format("Đang lưu {0}...", message), "Vui lòng chờ!");
        }

        public static AutoWaitForm AutoWaitForSave(String message = "")
        {//
            return new AutoWaitForm(String.Format("Đang lưu {0}...", message), "Vui lòng chờ!");
        }

        public static WaitDialogForm WaitForDelete(String message = "")
        {
            return new WaitDialogForm(String.Format("Đang xóa {0}...", message), "Vui lòng chờ!");
        }
        public static AutoWaitForm AutoWaitForDelete(String message = "")
        {//
            return new AutoWaitForm(String.Format("Đang xóa {0}...", message), "Vui lòng chờ!");
        }

        public static DialogResult ShowYesNo(String message, String title = "Hỏi ý kiến")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult ShowOK(String message, String title = "Thông tin")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult ShowOKCancel(String message, String title = "Hỏi ý kiến")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult ShowYesNoCancel(String message, String title = "Hỏi ý kiến")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult ShowInfo(String message, String title = "Thông tin")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult ShowWarning(String message, String title = "Cảnh báo")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult ShowError(String message, String title = "Thông báo lỗi")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult ShowSaveSuccessful(String message = "Lưu thành công")
        {
            return ShowInfo(message);
        }

        public static DialogResult ShowNotSaveSuccessful(String message = "Không lưu được")
        {
            return ShowError(message);
        }

        public static DialogResult ShowDelete(Form owner, String message = "Bạn muốn xóa [{0}]?")
        {
            return ShowYesNoCancel(String.Format(message, owner.Text));
        }

        public static DialogResult ShowDeleteSuccessful(String message = "Đã xóa thành công")
        {
            return ShowInfo(message);
        }
        public static DialogResult ShowNotDeleteSuccessful(String message = "Không xóa được")
        {
            return ShowError(message);
        }
        public static DialogResult ShowSaveRequireOptions(Form owner, String message = "Bạn cần lưu [{0}]?")
        {
            return ShowYesNoCancel(String.Format(message, owner.Text));
        }
        public static DialogResult ShowSaveRequireOptionsOnFormClosing(Form owner, String message = "Bạn cần lưu [{0}] trước khi đóng?")
        {
            return ShowYesNoCancel(String.Format(message, owner.Text));
        }
    }
}
