using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.SinhNhat;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.XuLyQuyTrinh.SinhNhat;
using PSC_HRM.Module;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.Controllers
{
    public partial class SinhNhat_GuiMailChucMungSinhNhatController : ViewController
    {
        public SinhNhat_GuiMailChucMungSinhNhatController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("SinhNhat_GuiMailChucMungSinhNhatController");
        }

        private void SinhNhat_GuiMailChucMungSinhNhatController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<DanhSachSinhNhatCanBo>() &&
                HamDungChung.IsCreateGranted<SinhNhatCanBo>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            int successful = 0;
            int notsuccessful = 0;
            //Lấy danh sách cán bộ hiện tại
            DanhSachSinhNhatCanBo danhSachSinhNhatCanBo = View.CurrentObject as DanhSachSinhNhatCanBo;
            if (danhSachSinhNhatCanBo != null)
            {
                StringBuilder mainLog = new StringBuilder();

                using (DialogUtil.AutoWait())
                {
                    foreach (SinhNhatCanBo item in danhSachSinhNhatCanBo.ListSinhNhatCanBo)
                    {
                        StringBuilder detailLog = new StringBuilder();

                        if (item.Chon)
                        {
                            string subject = "Happy Birth Day";
                            string tenChucVu = string.Empty;
                            string tenChucDanh = string.Empty;
                            string danhXung = string.Empty;

                            if (item.ThongTinNhanVien.ChucVu != null){tenChucVu = item.ThongTinNhanVien.ChucVu.TenChucVu;}
                            if (item.ThongTinNhanVien.ChucDanh != null){tenChucDanh = item.ThongTinNhanVien.ChucDanh.TenChucDanh;}
                            if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam){danhXung = "Ông";}
                            else{danhXung = "Bà";}
                            //
                            string body = string.Format("Chúc mừng sinh nhật {0} {1} {2} {3}", danhXung, item.ThongTinNhanVien.HoTen, tenChucVu != string.Empty ? tenChucVu : tenChucDanh, item.ThongTinNhanVien.BoPhan.TenBoPhan);
                            //
                            try
                            {
                                if (SendMailHappyBirthDay(item.ThongTinNhanVien.Email, item.ThongTinNhanVien.HoTen, subject, body))
                                {
                                    successful += 1;
                                }
                                else
                                {
                                    detailLog.AppendLine(string.Format("+ Email [{0}] của cán bộ {1} không tồn tại. ", item.Email,item.ThongTinNhanVien.HoTen));
                                }
                            }
                            catch (Exception ex)
                            {
                                detailLog.AppendLine(string.Format("+ Lỗi xảy ra trong quá trình gửi mail. Vui lòng kiểm tra đường truyền mạng, email và pass người gửi."));
                            }

                            if(detailLog.Length > 0)
                            {
                                mainLog.AppendLine(string.Format("- Không gửi mail cho cán bộ [{0}] được:", item.ThongTinNhanVien.HoTen));
                                mainLog.AppendLine(detailLog.ToString());
                                //
                                notsuccessful += 1;
                            }
                        }
                    }
                }
                //
                if (mainLog.Length > 0)
                {
                    if (DialogUtil.ShowYesNo("Có "+ notsuccessful+" cán bộ không thể gửi mail. Bạn có muốn xuất danh sách?") == DialogResult.Yes)
                    {
                        using (SaveFileDialog saveFile = new SaveFileDialog())
                        {
                            saveFile.Filter = "Text files (*.txt)|*.txt";

                            if (saveFile.ShowDialog() == DialogResult.OK)
                            {
                                HamDungChung.WriteLog(saveFile.FileName, mainLog.ToString());
                            }
                        }
                    }
                    //
                    DialogUtil.ShowInfo(string.Format("Đã gửi mail chúc mừng sinh nhật cho {0} cán bộ.", successful));
                }
                else
                {
                    DialogUtil.ShowInfo(string.Format("Đã gửi mail chúc mừng sinh nhật cho {0} cán bộ.", successful));
                }
            }
        }

        private bool SendMailHappyBirthDay(string mailTo, string displayName, string subject, string bodyContent)
        {
            bool successful = true;
            //
            int SMTPPort = HamDungChung.CauHinhChung.CauHinhEmail.Port;
            string SMTPUser = HamDungChung.CauHinhChung.CauHinhEmail.Email;
            string SMTPPassword = HamDungChung.CauHinhChung.CauHinhEmail.Pass;
            string SMTPServer = HamDungChung.CauHinhChung.CauHinhEmail.Server;
            //
            UserMailer userMailer = new UserMailer();
            if (!userMailer.SendMail(SMTPUser, displayName, SMTPPassword, SMTPPort, SMTPServer, mailTo, subject, bodyContent, mailTo))
            {
                successful = false;
            }

            return successful;
        }

    }
}
