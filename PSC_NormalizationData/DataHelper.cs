using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PSC_NormalizationData
{
    internal class DataHelper
    {
        const Int32 commandTimeOut = 60 * 5;
        internal static List<DictionaryNormalizationData> GetDictionaries(SqlConnection connection)
        {
            List<DictionaryNormalizationData> result = new List<DictionaryNormalizationData>();
            DataTable dt = GetTable(connection
                , @"Select Oid as Id
                    , dic.Name
                    , dic.TABLE_NAME
                    , dic.TABLE_SCHEMA
                    ,(SELECT KCU1.column_name
	                    FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU1
	                    WHERE OBJECTPROPERTY(OBJECT_ID(KCU1.TABLE_SCHEMA+'.'+KCU1.constraint_name), 'IsPrimaryKey') = 1
		                    AND KCU1.table_name LIKE RTRIM(LTrim(ISNULL(dic.TABLE_NAME,''))) 
		                    AND 
		                    (
			                    KCU1.TABLE_SCHEMA LIKE RTRIM(LTrim(ISNULL(dic.TABLE_SCHEMA,''))) 
			                    OR RTRIM(LTrim(ISNULL(dic.TABLE_SCHEMA,'')))=''
		                    )
	                ) AS PrimaryKey
                    , dic.ColumnName
                    , dic.ColumnCode
                    , dic.Predicate 
                    from AppDictionaryNormalizationData AS dic
                    where dic.GCRecord is null
                    order by dic.Name asc
                    "
                , CommandType.Text);

            foreach (DataRow dr in dt.Rows)
            {
                DictionaryNormalizationData dic = new DictionaryNormalizationData()
                {
                    Id = dr["Id"],
                    Name = (dr["Name"] as String ?? "").Trim(),
                    TABLE_NAME = (dr["TABLE_NAME"] as String ?? "").Trim(),
                    TABLE_SCHEMA = (dr["TABLE_SCHEMA"] as String ?? "").Trim(),
                    PrimaryKey = (dr["PrimaryKey"] as String ?? "").Trim(),
                    ColumnName = (dr["ColumnName"] as String ?? "").Trim(),
                    ColumnCode = (dr["ColumnCode"] as String ?? "").Trim(),
                    Predicate = (dr["Predicate"] as String ?? "").Trim()

                };

                result.Add(dic);
            }

            return result;
        }

        internal static List<DanhMuc> GetDanhMucList(SqlConnection connection, DictionaryNormalizationData dic)
        {
            List<DanhMuc> result = new List<DanhMuc>();
            String schema = "";
            if (!String.IsNullOrWhiteSpace(dic.TABLE_SCHEMA))
                schema = dic.TABLE_SCHEMA + ".";
            String where = "";
            if (!String.IsNullOrWhiteSpace(dic.Predicate))
                where = "where " + dic.Predicate;
            string comandText = String.Format("Select {0} as Id, {1} as Name, {2} as Code from {3}{4} {5}"

                                                    , dic.PrimaryKey
                                                    , dic.ColumnName
                                                    , dic.ColumnCode
                                                    , schema
                                                    , dic.TABLE_NAME
                                                    , where);
            DataTable dt = GetTable(connection
                , comandText
                , CommandType.Text
                );
            foreach (DataRow dr in dt.Rows)
            {
                DanhMuc danhMuc = new DanhMuc()
                {
                    PrimaryKey = dic.PrimaryKey,
                    Id = dr["Id"],
                    Name = dr["Name"] as String ?? "",
                    Code = dr["Code"] as String ?? "",
                    TABLE_NAME = dic.TABLE_NAME,
                    TABLE_SCHEMA = dic.TABLE_SCHEMA,

                };
                result.Add(danhMuc);
            }

            return result;
        }
        internal static List<Foreign> GetForeignList(SqlConnection connection, DictionaryNormalizationData dic, DanhMuc danhMuc)
        {
            List<Foreign> result = new List<Foreign>();
            DataTable dt = GetTable(connection as SqlConnection
                , String.Format(@"
                                    SELECT  
                                        KCU1.TABLE_NAME AS TABLE_NAME
                                        --,C.COLUMN_NAME AS IdColumn
                                        ,KCU1.TABLE_SCHEMA AS TABLE_SCHEMA
                                        ,KCU1.COLUMN_NAME AS ForeignColumn 
                                    FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC 
                                    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU1 
                                        ON KCU1.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG  
                                        AND KCU1.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA 
                                        AND KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME 
                                    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU2 
                                        ON KCU2.CONSTRAINT_CATALOG = RC.UNIQUE_CONSTRAINT_CATALOG  
                                        AND KCU2.CONSTRAINT_SCHEMA = RC.UNIQUE_CONSTRAINT_SCHEMA 
                                        AND KCU2.CONSTRAINT_NAME = RC.UNIQUE_CONSTRAINT_NAME 
                                        AND KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION 
                                    --INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS pk ON KCU1.TABLE_NAME = pk.TABLE_NAME 
                                    --inner join INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS C ON C.TABLE_NAME = pk.TABLE_NAME 
                                        --AND C.CONSTRAINT_NAME = pk.CONSTRAINT_NAME AND pk.CONSTRAINT_TYPE = 'PRIMARY KEY'
                                    WHERE 
                                    (KCU2.TABLE_NAME LIKE @TenBangDanhMuc
                                        AND 
                                        (
                                            ISNULL(@TABLE_SCHEMA,'')='' OR (KCU2.TABLE_SCHEMA LIKE @TABLE_SCHEMA)
                                        )
                                    )
                                ")
                , CommandType.Text
                , new SqlParameter("@TABLE_SCHEMA", dic.TABLE_SCHEMA.Trim())
                , new SqlParameter("@TenBangDanhMuc", dic.TABLE_NAME.Trim())
                );
            foreach (DataRow dr in dt.Rows)
            {
                Foreign foreign = new Foreign()
                {
                    TABLE_NAME = dr["TABLE_NAME"] as String ?? "",
                    TABLE_SCHEMA = dr["TABLE_SCHEMA"] as String ?? "",
                    ForeignColumn = dr["ForeignColumn"] as String ?? ""
                };
                result.Add(foreign);
            }

            return result;
        }

        internal static DataTable GetTable(SqlConnection cnn, string query, CommandType type, params SqlParameter[] param)
        {
            if (cnn.State != ConnectionState.Open)
                cnn.Open();
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {
                cmd.CommandTimeout = commandTimeOut;
                cmd.CommandType = type;
                if (param != null)
                    cmd.Parameters.AddRange(param);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }

        internal static void FixWrongItem(SqlConnection cnn, DictionaryNormalizationData dic, DanhMuc danhMuc, Foreign foreign, Object newValue)
        {
            if (cnn.State != ConnectionState.Open)
                cnn.Open();


            //
            using (SqlCommand cmd = new SqlCommand())
            {
                String schema = "";
                if (!String.IsNullOrWhiteSpace(foreign.TABLE_SCHEMA))
                    schema = foreign.TABLE_SCHEMA + ".";

                cmd.CommandTimeout = commandTimeOut;
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = String.Format("update {0}{1} set {2} = @newValue where {2} = @OldValue", schema, foreign.TABLE_NAME, foreign.ForeignColumn);


                cmd.Parameters.AddWithValue("@OldValue", danhMuc.Id);
                cmd.Parameters.AddWithValue("@newValue", newValue);
                int result = cmd.ExecuteNonQuery();
                //System.Windows.Forms.MessageBox.Show(cmd.CommandText);
            }


        }

        internal static void DeleteWrongItem(SqlConnection cnn, DanhMuc danhMuc)
        {

            if (cnn.State != ConnectionState.Open)
                cnn.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                String schema = "";
                if (!String.IsNullOrWhiteSpace(danhMuc.TABLE_SCHEMA))
                    schema = danhMuc.TABLE_SCHEMA + ".";

                cmd.CommandTimeout = commandTimeOut;
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = String.Format("delete {0}{1} where {2} = @idValue", schema, danhMuc.TABLE_NAME, danhMuc.PrimaryKey);
                cmd.Parameters.AddWithValue("@idValue", danhMuc.Id);

                //System.Windows.Forms.MessageBox.Show(cmd.CommandText);
                int result = cmd.ExecuteNonQuery();
            }
        }
    }
}
