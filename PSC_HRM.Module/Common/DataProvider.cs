using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PSC_HRM.Module
{
    public static class DataProvider
    {
        public static readonly string DataBase;

        static DataProvider()
        {
            DataBase = ConfigurationManager.AppSettings["HRMConnect"];
        }

        /// <summary>
        /// Get DataTable from excel file
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string excelFilePath, string tableName)
        {
            string connectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", excelFilePath);
            //string connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;", excelFilePath);
            using (OleDbConnection cnn = new OleDbConnection(connectionString))
            {
                //
                //cnn.CreateCommand().CommandTimeout = 5000;]
                var a = cnn.ConnectionTimeout;
                string query = String.Format("Select * from {0}", tableName);
                using (OleDbDataAdapter da = new OleDbDataAdapter(query, cnn))
                {
                    da.SelectCommand.CommandTimeout = 10000;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// Get DataTable from SQL Server
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(SqlConnection cnn, string query, CommandType type, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = GetCommand(query, type, param);
            cmd.Connection = cnn;
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// Get DataTable from SQL Server
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string query, CommandType type, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = GetCommand(query, type, param))
            {
                cmd.Connection = GetConnection();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                return dt;
            }
        }



        /// <summary>
        /// Get object from SQL Server
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object GetObject(string query, CommandType type, params SqlParameter[] param)
        {
            using (SqlCommand cmd = GetCommand(query, type, param))
            {
                cmd.CommandTimeout = 1800;
                cmd.Connection = GetConnection();
                object obj = cmd.ExecuteScalar();
                return obj;
            }
        }

        /// <summary>
        /// Get object from SQL Server
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object GetObject(SqlConnection cnn, string query, CommandType type, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = GetCommand(query, type, param);
            cmd.Connection = cnn;
            object obj = cmd.ExecuteScalar();
            return obj;
        }

        /// <summary>
        /// Get DataTable from SQL Server
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            if (cmd != null)
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Connection = GetConnection();
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Lỗi xử lý dữ liệu báo cáo từ store : {0}\r\n{1}", cmd.CommandText, ex.Message));
                }
            }

            return ds;
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlConnection cnn, SqlCommand cmd)
        {
            try
            {
                cmd.Connection = cnn;
                cmd.CommandTimeout = 180;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string i = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string query, CommandType type, params SqlParameter[] args)
        {
            try
            {
                SqlCommand cmd = GetCommand(query, type, args);
                cmd.Connection = GetConnection();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static int ExecuteNonQueryTimeOut(string query, CommandType type, params SqlParameter[] args)
        {
            try
            {
                SqlCommand cmd = GetCommand(query, type, args);
                cmd.Connection = GetConnection();
                cmd.CommandTimeout = 3000;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get sql command
        /// </summary>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static SqlCommand GetCommand(string query, CommandType type, params SqlParameter[] param)
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.CommandType = type;
                cmd.CommandTimeout = 3000;
                if (param != null)
                    cmd.Parameters.AddRange(param);

                return cmd;
            }
        }

        /// <summary>
        /// Get sql connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            string connectionString = GetConnectionString(DataBase);
            SqlConnection cnn = new SqlConnection(connectionString);
            if (cnn.State != ConnectionState.Open)
                cnn.Open();
            return cnn;
        }
        public static object GetValueFromDatabase(string query, CommandType type, params SqlParameter[] param)
        {
            using (SqlCommand cmd = GetCommand(query, type, param))
            {
                cmd.CommandTimeout = 10000;
                cmd.Connection = GetConnection();
                object obj = cmd.ExecuteScalar();
                return obj;
            }
        }
        /// <summary>
        /// Lấy chuỗi kết nối
        /// </summary>
        /// <param name="filename">đường dẫn</param>
        /// <returns></returns>
        public static string GetConnectionString(string filename)
        {
            using (FileStream stream = File.Open(String.Format(@"{0}\Configs\{1}\{2}", Application.StartupPath, TruongConfig.MaTruong, filename), FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    string temp = reader.ReadString();

                    if (!String.IsNullOrEmpty(temp))
                    {
                        String connectionString = CrystoProvider.Decrypt(temp, global::PSC_HRM.Module.Properties.Resources.Key, true);
                        return connectionString;
                    }
                    return "";
                }
            }
        }

        /// <summary>
        /// Get DataTable from excel file
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromExcel(string excelFilePath, string tableName)
        {

            OleDbDataReader abc = System.Data.OleDb.OleDbEnumerator.GetRootEnumerator();
            string connectionString = "";
            connectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", excelFilePath);
            //connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0", excelFilePath);


            //
            using (OleDbConnection cnn = new OleDbConnection(connectionString))
            {
                //
                //cnn.CreateCommand().CommandTimeout = 5000;]
                var a = cnn.ConnectionTimeout;
                string query = String.Format("Select * from {0}", tableName);
                using (OleDbDataAdapter da = new OleDbDataAdapter(query, cnn))
                {
                    da.SelectCommand.CommandTimeout = 10000;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

    }
}
