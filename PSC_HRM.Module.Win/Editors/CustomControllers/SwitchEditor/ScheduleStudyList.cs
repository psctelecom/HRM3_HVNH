using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.CustomControllers.SwitchEditor
{
    internal class ScheduleStudyList : List<ScheduleStudy>
    {
        public ScheduleStudyList()
        {
            LoadData();
        }

        public ScheduleStudyList(int capacity)
            : base(capacity)
        {
            LoadData();
        }

        public ScheduleStudyList(IEnumerable<ScheduleStudy> collection)
            : base(collection)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("PSC_UIS.bin")))
            {
                if (cnn.State != ConnectionState.Open)
                    cnn.Open();

                string query = @"SELECT SchedulestudyunitID AS ID, 
	                                ScheduleStudyUnitAlias AS Alias,
                                dbo.fn_psc_Sch_ClassStudentFromScheduleStudyUnitID(ScheduleStudyUnitID) as Name
                                FROM psc_ScheduleStudyUnits a 
	                                INNER JOIN dbo.psc_StudyUnits b ON a.StudyUnitID = b.StudyUnitID 
	                                INNER JOIN dbo.psc_Curriculums c ON b.CurriculumID = c.CurriculumID 
                                WHERE b.YearStudy LIKE @NamHoc 
	                                AND b.TermID LIKE @HocKy
                                    AND c.CurriculumName LIKE @MonHoc";

                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@NamHoc", HopDong.HopDong.NamHoc);
                param[1] = new SqlParameter("@HocKy", HopDong.HopDong.HocKy);
                param[2] = new SqlParameter("@MonHoc", "");

                using (DataTable dt = DataProvider.GetDataTable(cnn, query, CommandType.Text, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            Add(new ScheduleStudy() { ID = row["ID"].ToString(), Alias = row["Alias"].ToString(), Name = row["Name"].ToString() });
                        }
                    }
                }
            }
        }
    }
}
