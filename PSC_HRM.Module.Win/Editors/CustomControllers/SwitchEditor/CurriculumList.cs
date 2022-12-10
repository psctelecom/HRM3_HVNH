using PSC_HRM.Module;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSC_HRM.Module.Win.CustomControllers.SwitchEditor
{
    public class CurriculumList : List<Curriculum>
    {
        public CurriculumList()
        {
            LoadData();
        }

        public CurriculumList(int capacity)
            : base(capacity)
        {
            LoadData();
        }

        public CurriculumList(IEnumerable<Curriculum> collection)
            : base(collection)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("PSC_UIS.bin")))
            {
                if (cnn.State != System.Data.ConnectionState.Open)
                    cnn.Open();
                const string query = "Select Distinct CurriculumName From dbo.psc_Curriculums Order By CurriculumName";

                using (DataTable dt = DataProvider.GetDataTable(cnn, query, CommandType.Text))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            Add(new Curriculum() { Name = row["CurriculumName"].ToString() });
                        }
                    }
                }
            }
        }
    }
}
