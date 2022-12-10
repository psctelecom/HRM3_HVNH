using System;
using DevExpress.Data.Filtering;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using DevExpress.ExpressApp;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using System.Diagnostics;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    public class Export05AK_TNCN
    {
        public void XuLy(IObjectSpace obs, ToKhaiQuyetToanThueTNCN toKhai)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Excel 1997-2003 file (*.xls)|*.xls";
                dialog.FileName = "Bang_Ke_05AK.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("TargetTypeName=?", "PSC_HRM.Module.ThuNhap.Thue.BangKeThueTNCNNhanVien");
                    BieuMau bieuMau = obs.FindObject<BieuMau>(filter);
                    if (bieuMau != null)
                    {
                        //Ghi file
                        FileStream stream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                        bieuMau.File.SaveToStream(stream);
                        stream.Flush();
                        stream.Close();
                        stream.Dispose();

                        //Đưa dữ liệu ra
                        using (DataTable dt = DataProvider.GetDataTable("spd_TaiChinh_ThueTNCN_LayDanhSach05A_BK_TNCN", CommandType.StoredProcedure, new SqlParameter("@ToKhaiQuyetToanThueTNCN", toKhai.Oid)))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                Excel.Application excel = new Excel.Application();
                                excel.Workbooks.Open(dialog.FileName);
                                int rowIndex = 5;

                                foreach (DataRow item in dt.Rows)
                                {
                                    for (int i = 1; i < 14; i++)
                                    {
                                        excel.Cells[rowIndex, i] = item[i - 1];
                                    }
                                    rowIndex++;
                                } 
                                excel.Quit();
                                if (XtraMessageBox.Show("Đã xuất dữ liệu ra file 05A/BK-TNCN thành công.\r\nBạn có muốn mở file không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    Process.Start(new ProcessStartInfo(dialog.FileName));
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
