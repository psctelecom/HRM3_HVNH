using System;


using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.XtraEditors.Repository;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.Win.CustomControllers.Editor;
using PSC_HRM.Module.Win.CustomControllers.SwitchEditor;
using System.Drawing;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace PSC_HRM.Module.Win.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class CurriculumEditor : DXPropertyEditor
    {
        //private readonly IEditor control = CurriculumFactory.CreateControl();

        //public CurriculumEditor(Type objectType, IModelMemberViewItem model)
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

        public CurriculumEditor(Type objectType, IModelMemberViewItem info)
            : base(objectType, info)
        { }

        protected override object CreateControlCore()
        {
            GridLookUpEdit control = new GridLookUpEdit();
            MonFactory mon = new MonFactory();
            mon.LoadData();
            control.Properties.DataSource = mon.Mons;
            control.Properties.ValueMember = "ID";
            control.Properties.NullText = "";
            HamDungChung.InitGridLookUp(control, true, true, TextEditStyles.Standard);
            HamDungChung.InitGridView(control.Properties.View, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);

            control.Properties.DisplayMember = "Name";
            HamDungChung.ShowField(control.Properties.View, new string[] { "ID", "Name" }, new string[] { "Mã môn", "Tên môn" });

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

    internal class MonFactory
    {
        public List<Mon> Mons { get; set; }

        public MonFactory()
        {
            Mons = new List<Mon>();
        }

        public void LoadData()
        {
            using (SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("PSC_UIS.bin")))
            {
                if (cnn.State != ConnectionState.Open)
                    cnn.Open();

                if (!TruongConfig.MaTruong.Equals("HBU"))
                {
                    string query = @"Select CurriculumId as ID, CurriculumName as Name From dbo.psc_Curriculums Order By CurriculumName";
                    using (DataTable dt = DataProvider.GetDataTable(cnn, query, CommandType.Text))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Mon item;
                            foreach (DataRow row in dt.Rows)
                            {
                                item = new Mon() { ID = row["ID"].ToString(), Name = row["Name"].ToString() };
                                Mons.Add(item);
                            }
                        }
                    }
                }
                else
                {
                    string query = @"SELECT DISTINCT psc_Curriculums.CurriculumID as ID, CurriculumName as Name
                                    FROM psc_Curriculums 
                                    JOIN dbo.psc_StudyUnits ON psc_StudyUnits.CurriculumID = psc_Curriculums.CurriculumID
                                    WHERE YearStudy LIKE @NamHoc 
                                    AND TermID LIKE @HocKy";

                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@NamHoc", HopDong.HopDong.NamHoc.Replace(" ", ""));
                    param[1] = new SqlParameter("@HocKy", HopDong.HopDong.HocKy);

                    using (DataTable dt = DataProvider.GetDataTable(cnn, query, CommandType.Text, param))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Mon item;
                            foreach (DataRow row in dt.Rows)
                            {
                                item = new Mon() { ID = row["ID"].ToString(), Name = row["Name"].ToString() };
                                Mons.Add(item);
                            }
                        }
                    }
                }

                
            }
        }
    }

    internal class Mon
    {
        [DisplayName("Mã môn")]
        public string ID { get; set; }

        [DisplayName("Tên môn")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
