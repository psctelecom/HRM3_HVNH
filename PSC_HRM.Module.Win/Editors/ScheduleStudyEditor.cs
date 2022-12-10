using System;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.XtraEditors.Repository;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.Win.CustomControllers.Editor;
using PSC_HRM.Module.Win.CustomControllers.SwitchEditor;
using System.Drawing;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors.Controls;

namespace PSC_HRM.Module.Win.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class ScheduleStudyEditor : DXPropertyEditor
    {
        //private readonly IEditor control = ScheduleStudyFactory.CreateControl();

        //public ScheduleStudyEditor(Type objectType, IModelMemberViewItem model)
        //    : base(objectType, model)
        //{
        //    ControlBindingProperty = "Value";
        //}

        //protected override object CreateControlCore()
        //{
        //    control.Control.BackColor = Color.White;
        //    return control.Control;
        //}

        //protected override RepositoryItem CreateRepositoryItem()
        //{
        //    return control.RepositoryItem;
        //}

        //protected override void OnControlCreated()
        //{
        //    base.OnControlCreated();
        //    UpdateControlEnabled();
        //}

        //protected override void OnAllowEditChanged()
        //{
        //    base.OnAllowEditChanged();
        //    UpdateControlEnabled();
        //}

        //private void UpdateControlEnabled()
        //{
        //    if (Control != null)
        //    {
        //        Control.Enabled = true;
        //    }
        //}

        public ScheduleStudyEditor(Type objectType, IModelMemberViewItem info)
            : base(objectType, info)
        { }

        protected override object CreateControlCore()
        {
            GridLookUpEdit control = new GridLookUpEdit();
            LopFactory mon = new LopFactory();
            mon.LoadData();
            control.Properties.DataSource = mon.Lops;
            control.Properties.ValueMember = "ID";
            control.Properties.NullText = "";
            HamDungChung.InitGridLookUp(control, true, true, TextEditStyles.Standard);
            HamDungChung.InitGridView(control.Properties.View, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);

            control.Properties.DisplayMember = "Name";
            HamDungChung.ShowField(control.Properties.View, new string[] { "ID", "Name" }, new string[] { "Mã lớp", "Tên lớp" });

            HamDungChung.InitPopupFormSize(control, 400, 400);

            return control;
        }

        protected override RepositoryItem CreateRepositoryItem()
        {
            var item = new RepositoryItemGridLookUpEdit();
            item.DisplayMember = "Name";
            return item;
        }
    }

    internal class LopFactory
    {
        public List<Lop> Lops { get; set; }

        public LopFactory()
        {
            Lops = new List<Lop>();
        }

        public void LoadData()
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
	                                AND b.TermID LIKE @HocKy";

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@NamHoc", HopDong.HopDong.NamHoc.Replace(" ",""));
                param[1] = new SqlParameter("@HocKy", HopDong.HopDong.HocKy);

                using (DataTable dt = DataProvider.GetDataTable(cnn, query, CommandType.Text, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Lop item;
                        foreach (DataRow row in dt.Rows)
                        {
                            item = new Lop()  { ID = row["ID"].ToString(), Name = row["Name"].ToString() };
                            Lops.Add(item);
                        }
                    }
                }
            }
        }
    }

    internal class Lop
    {
        [DisplayName("Mã lớp")]
        public string ID { get; set; }

        [DisplayName("Tên lớp")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    
    }
}