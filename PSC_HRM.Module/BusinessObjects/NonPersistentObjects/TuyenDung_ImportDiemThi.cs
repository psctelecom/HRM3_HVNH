using System;

using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.TuyenDung;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using System.Text;
using System.IO;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Import điểm thi của ứng viên từ file Excel")]
    public class TuyenDung_ImportDiemThi : BaseObject
    {
        // Fields...
 
        public TuyenDung_ImportDiemThi(Session session) : base(session) { }

        public void XuLy(IObjectSpace obs, DanhSachThi dsThi)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.FileName = "";
                dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A3:E]"))
                    {
                        if (dt != null)
                        {
                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                uow.BeginTransaction();
                                var mainLog = new StringBuilder();                               
                                UngVien ungVien;
                                ThiSinh thiSinh;   

                                int soThuTu = 0;                            
                                int soBaoDanh = 1;
                                int Ho = 2;
                                int Ten = 3;       
                                int diemThi = 4;
                                using (DialogUtil.AutoWait())
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        String soThuTuText = item[soThuTu].ToString();
                                        String soBaoDanhText = item[soBaoDanh].ToString();
                                        String HoText = item[Ho].ToString().Trim();
                                        String TenText = item[Ten].ToString().Trim();
                                        String diemThiText = item[diemThi].ToString().Trim();

                                        var errorLog = new StringBuilder();

                                        ungVien = Session.FindObject<UngVien>(CriteriaOperator.Parse("SoBaoDanh=? and HoTen=?", soBaoDanhText, HoText + " " + TenText));
                                        if (ungVien == null)
                                        {
                                            errorLog.AppendLine(string.Format("+ Sai thông tin ứng viên"));
                                        }

                                        else
                                        {
                                            #region Kiểm tra dữ liệu import

                                            XPCollection<ThiSinh> listThiSinh = new XPCollection<ThiSinh>(uow, CriteriaOperator.Parse("DanhSachThi =?", dsThi.Oid));
                                            CriteriaOperator filter = (CriteriaOperator.Parse("UngVien.SoBaoDanh =?", soBaoDanhText));
                                            listThiSinh.Filter = filter;

                                            if (listThiSinh.Count > 0)
                                            {
                                                thiSinh = listThiSinh[0];
                                                if (!string.IsNullOrEmpty(diemThiText))
                                                {
                                                    if (diemThiText.ToLower() == "m")
                                                        thiSinh.MienThi = true;
                                                    else if (diemThiText.ToLower() == "v")
                                                        thiSinh.VangThi = true;
                                                    else
                                                    {
                                                        try
                                                        {
                                                             thiSinh.DiemSo = Convert.ToInt32(diemThiText);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            errorLog.AppendLine(string.Format("+ Sai thông tin điểm thi"));
                                                        }
                                               
                                                    }

                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(string.Format("+ Thiếu thông tin điểm thi"));
                                                }
                                            }
                                            else
                                                errorLog.AppendLine(string.Format("+ Không tìm thấy thí sinh trong danh sách thi"));
                                            #endregion
                                        }

                                        #region Ghi File log
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + soThuTuText + " - Họ Tên: " + HoText + " " + TenText);
                                                mainLog.AppendLine(errorLog.ToString());
                                            }
                                        }
                                        #endregion

                                    }//end foreach
                                }
                                
                                if (mainLog.Length > 0)
                                {
                                    uow.RollbackTransaction();
                                    if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                                    {
                                        string tenFile = "Import_Log.txt";
                                        //FileStream fileStream = File.Open(tenFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                                        StreamWriter writer = new StreamWriter(tenFile);                                       
                                        writer.WriteLine(mainLog.ToString());
                                        writer.Flush();
                                        writer.Close();
                                        writer.Dispose();
                                        HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                        System.Diagnostics.Process.Start(tenFile);
                                    }
                                }
                                else
                                {
                                    //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                    uow.CommitChanges();
                                    //hoàn tất giao tác
                                    //transaction.Complete();
                                    DialogUtil.ShowSaveSuccessful("Import Thành Công tất cả!");
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
