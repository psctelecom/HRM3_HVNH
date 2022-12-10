using System;
using System.Collections.Generic;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using System.Windows.Forms;
using System.Data;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Text;
using PSC_HRM.Module.ThuNhap.TruyThu;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using System.IO;

namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    [ModelDefault("Caption", "Import truy thu")]
    public class ImportTruyThu : ImportBase
    {
        public ImportTruyThu(Session session)
            : base(session)
        { }

        public override void XuLy(IObjectSpace obs, object obj)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                //import
                using (OpenFileDialog dialog = new OpenFileDialog())
                {

                    dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";
                    dialog.Multiselect = false;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {

                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:F]"))
                        {
                            BangTruyThu bangTruyThu = obj as BangTruyThu;

                            uow.BeginTransaction();
                            ThongTinNhanVien nhanVien;
                            var mainLog = new StringBuilder();
                            var errorLog = new StringBuilder();

                            TruyThuNhanVien truyThu = null;
                            ChiTietTruyThu chiTiet = null;

                            const int sttIdx = 0;
                            const int boPhanIdx = 1;
                            const int maQuanLyIdx = 2;
                            const int hoTenIdx = 3;
                            const int soTienIdx = 4;
                            const int soTienChiuThueIdx = 5;

                            foreach (DataRow item in dt.Rows)
                            {
                                String sttText = item[sttIdx].ToString();
                                String tenBoPhan = item[boPhanIdx].ToString().Trim();
                                String maQuanLy = item[maQuanLyIdx].ToString().Trim();
                                String hoTenNhanVien = item[hoTenIdx].ToString().Trim();
                                String soTien = item[soTienIdx].ToString().Trim();
                                String soTienChiuThue = item[soTienChiuThueIdx].ToString().Trim();

                                nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy = ? && HoTen = ? ", maQuanLy, hoTenNhanVien));
                                if (nhanVien == null)
                                {
                                    errorLog.AppendLine(string.Format("+ Mã quản lý: {0}, {1} chưa tồn tại trong hệ thống", maQuanLy, hoTenNhanVien));
                                }
                                else
                                {
                                    truyThu = new TruyThuNhanVien(uow);
                                    truyThu.BangTruyThu = uow.FindObject<BangTruyThu>(CriteriaOperator.Parse("Oid = ?", bangTruyThu.Oid)); 

                                    BoPhan boPhan;
                                    boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan Like ?", tenBoPhan));
                                    if(boPhan != null)
                                        truyThu.BoPhan = boPhan;

                                    truyThu.ThongTinNhanVien = nhanVien;
                                        
                                    chiTiet = new ChiTietTruyThu(uow);
                                    chiTiet.CongTru = CongTruEnum.Tru;
                                    chiTiet.TruyThuNhanVien = truyThu;

                                    try
                                    {
                                        chiTiet.SoTien = Convert.ToDecimal(soTien);
                                    }
                                    catch (Exception ex)
                                    {
                                        errorLog.AppendLine("+ Sai thông tin số tiền");
                                    }

                                    try
                                    {
                                        chiTiet.SoTienChiuThue = Convert.ToDecimal(soTienChiuThue);
                                    }
                                    catch (Exception ex)
                                    {
                                            errorLog.AppendLine("+ Sai thông tin số tiền chịu thuế");
                                    }
                                }


                                if (errorLog.Length > 0)
                                {
                                    mainLog.AppendLine("- STT: " + sttText + " - Họ Tên:" + hoTenNhanVien);
                                    mainLog.AppendLine(errorLog.ToString());
                                }
                            }

                            if (mainLog.Length > 0)
                            {
                                uow.RollbackTransaction();
                                if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi") == DialogResult.Yes)
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
                                uow.CommitChanges();
                                DialogUtil.ShowInfo("Import Thành công !");
                            }
                        }
                    }
                }
            }
        }
    }
}
