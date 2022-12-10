using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;

namespace PSC_HRM.Module.ThuNhap.Import
{
    public static class OleDbHelper
    {
        const string template = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=No;\"";
        public static void ExportData(string filename, string table, List<ExportItem> dataSource)
        {
            string connectionString = String.Format(template, filename);
            using (OleDbConnection cnn = new OleDbConnection(connectionString))
            {
                if (cnn.State != ConnectionState.Open)
                    cnn.Open();

                using (OleDbCommand cmd = new OleDbCommand(String.Format("Insert Into {0} (F1, F2, F3) Values(@MaQuanLy, @HoTen, @BoPhan)", table), cnn))
                {
                    //sort theo ten, ho
                    dataSource.OrderBy(p => p.Ten).ThenBy(p => p.Ho);
                    foreach (var item in dataSource)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@MaQuanLy", item.SoHieuCongChuc);
                        cmd.Parameters.AddWithValue("@HoTen", item.Ho + " " + item.Ten);
                        cmd.Parameters.AddWithValue("@BoPhan", item.BoPhan);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
