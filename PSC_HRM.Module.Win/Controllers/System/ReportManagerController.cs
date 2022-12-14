using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.ExpressApp.SystemModule;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.Report;
using DevExpress.XtraEditors;
using System.Text;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
namespace PSC_HRM.Module.Win.Controllers
{
    public partial class ReportManagerController : WindowController
    {
        public ReportManagerController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void actReportManager_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if (e.SelectedChoiceActionItem.Caption == "Export")
                ExportReport(e);
            else
                ImportReport(e);
        }


        private void ImportReport(SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();

            ImportReport source = obs.CreateObject<ImportReport>();
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, source);
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;

            DialogController ctrl = new DialogController();
            ctrl.AcceptAction.Execute += ImportAction_Execute;
            e.ShowViewParameters.Controllers.Add(ctrl);
        }

        private void ExportReport(SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();

            ExportReport source = obs.CreateObject<ExportReport>();
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, source);
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;

            DialogController ctrl = new DialogController();
            ctrl.AcceptAction.Execute += ExportAction_Execute;
            e.ShowViewParameters.Controllers.Add(ctrl);
        }

        void ExportAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Report Data|*.report|All file|*.*";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportReport export = e.CurrentObject as ExportReport;
                    DataTable dt = new DataTable("ReportTemplate", "PSC_HRM");
                    List<string> reports = new List<string>();
                    StringBuilder sReport = new StringBuilder("Những báo cáo sau đã được export:");
                    bool state = false;
                    string query = "SELECT a.Oid, a.ObjectTypeName, a.Content, a.Name, a.IsInplaceReport, b.TargetTypeName, b.NhomBaoCao, c.TenNhom, b.HinhAnh FROM dbo.ReportData a INNER JOIN dbo.HRMReport b On a.Oid = b.Oid INNER JOIN dbo.GroupReport c On b.NhomBaoCao=c.Oid Where a.GCRecord IS NULL AND b.Oid=@Oid";
                    SqlCommand cmd = new SqlCommand(query, (SqlConnection)export.Session.Connection);
                    cmd.Parameters.Add("@Oid", SqlDbType.Int);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    foreach (ChonBaoCao item in export.BaoCaoList)
                    {
                        if (item.Chon)
                        {
                            cmd.Parameters["@Oid"].Value = item.BaoCao.Oid;
                            da.Fill(dt);
                            reports.Add(item.BaoCao.Oid.ToString());
                            sReport.AppendLine(" + " + item.BaoCao.ReportName);
                            state = true;
                        }
                    }
                    //ghi dữ liệu vào table -> xml
                    if (state)
                    {
                        dt.WriteXml(saveFileDialog.FileName, XmlWriteMode.WriteSchema);
                        XtraMessageBox.Show(String.Format("Đã xuất dữ liệu {0} báo cáo thành công.\r\n{1}", dt.Rows.Count, sReport), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        XtraMessageBox.Show("Không có báo cáo nào được chọn để export.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void ImportAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Filter = "Report Data|*.report|All file|*.*";
                    dlg.AddExtension = true;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (MessageBox.Show(String.Format("Bạn có muốn import mẫu báo cáo mới từ file {0} không?", dlg.FileName), "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                        //load dữ liệu
                        ImportReport import = e.CurrentObject as ImportReport;
                        DataTable tbl = null;

                        using (DataTable tbl1 = new DataTable("ReportTemplate", "PSC_ERP"))
                        {
                            tbl1.Columns.Add("Oid", typeof(int));
                            tbl1.Columns.Add("ObjectTypeName", typeof(string));
                            tbl1.Columns.Add("Content", typeof(byte[]));
                            tbl1.Columns.Add("Name", typeof(string));
                            tbl1.Columns.Add("IsInplaceReport", typeof(bool));
                            tbl1.Columns.Add("Group", typeof(string));
                            tbl1.Columns.Add("TargetTypeName", typeof(string));
                            tbl1.Columns.Add("TenNhom", typeof(string));
                            tbl1.ReadXml(dlg.FileName);

                            //update hoặc insert dữ liệu, khi insert có thể insert group và insert nhóm quyền xem báo cáo 'Xem tất cả báo cáo'
                            DevExpress.ExpressApp.IObjectSpace obs = Application.CreateObjectSpace();
                            Report.HRMReport report = null;
                            Report.GroupReport group;
                            //Security.NhomQuyenSuDung permission = null;
                            SqlConnection cn = (SqlConnection)((XPObjectSpace)obs).Session.Connection;
                            SqlCommand cm = new SqlCommand("", cn);
                            string sReport = "";
                            int rptCount = 0;
                            foreach (DataRow row in tbl1.Rows)
                            {
                                report = obs.GetObjectByKey<Report.HRMReport>(row["Oid"]);
                                {
                                    //insert
                                    //kiểm tra chưa có nhóm báo cáo thì tạo mới
                                    group = obs.FindObject<Report.GroupReport>(CriteriaOperator.Parse("Oid=?", row["Group"]));
                                    if (group == null)
                                    {
                                        //tạo nhóm báo cáo mới
                                        cm.CommandText = "Insert into GroupReport (Oid, TenNhom) Values (@Oid, @TenNhom)";
                                        cm.Parameters.Clear();
                                        cm.Parameters.AddWithValue("@Oid", row["Group"]);
                                        cm.Parameters.AddWithValue("@TenNhom", row["TenNhom"]);
                                        cm.ExecuteNonQuery();
                                        //thêm phân quyền vào nhóm báo cáo vừa tạo
                                        //if (permission == null)
                                        //{
                                        //    permission = obs.FindObject<Security.NhomQuyenSuDung>(CriteriaOperator.Parse("Name=?", "Xem tất cả báo cáo"));
                                        //    if (permission == null)
                                        //    {
                                        //        permission = new Security.NhomQuyenSuDung(obs.Session);
                                        //        permission.Name = "Xem tất cả báo cáo";
                                        //        //permission.Save();
                                        //        //obs.Session.Save(permission);
                                        //        obs.CommitChanges();
                                        //    }
                                        //}
                                        //cm.CommandText = "Insert into GroupReportPermission (Oid, GroupReport, NhomQuyenSuDung, Permission) Values (NewID(), @GroupReport, @NhomQuyenSuDung, 0)";
                                        //cm.Parameters.Clear();
                                        //cm.Parameters.AddWithValue("@GroupReport", row["Group"]);
                                        //cm.Parameters.AddWithValue("@NhomQuyenSuDung", permission.Oid);
                                        //cm.ExecuteNonQuery();
                                    }
                                    //insert báo cáo
                                    cm.CommandText = "SET IDENTITY_INSERT ReportData ON";
                                    cm.Parameters.Clear();
                                    cm.ExecuteNonQuery();
                                    XPObjectType xpobj = ((XPObjectSpace)obs).Session.FindObject<XPObjectType>(CriteriaOperator.Parse("TypeName=?", "PSC_HRM.Module.Report.HRMReport"));
                                    if (xpobj == null)
                                    {
                                        cm.CommandText = "insert into XPObjectType(TypeName,AssemblyName)values(@p1,@p2)";
                                        cm.Parameters.Clear();
                                        cm.Parameters.AddWithValue("@p1", "PSC_HRM.Module.Report.HRMReport");
                                        cm.Parameters.AddWithValue("@p2", "PSC_HRM.Module");
                                        cm.ExecuteNonQuery();
                                        xpobj = ((XPObjectSpace)obs).Session.FindObject<XPObjectType>(CriteriaOperator.Parse("TypeName=?", "PSC_HRM.Module.Report.HRMReport"));
                                        //xpobj = new XPObjectType(obs.Session);
                                        //xpobj.AssemblyName = "PSC_HRM.Module";
                                        //xpobj.TypeName = "PSC_HRM.Module.Report.HRMReport";
                                        //int Oid=(int) obs.Session.Evaluate(typeof(XPObjectType), CriteriaOperator.Parse("Max(Oid)"),CriteriaOperator.Parse(""));
                                        //list = obs.Session.FindObject<XPObjectType>(CriteriaOperator.Parse(""));
                                        //xpobj.Oid = Oid+1;
                                        //obs.Session.Save(xpobj);
                                        //obs.CommitChanges();
                                    }
                                    cm.CommandText = String.Format("Insert into ReportData (Oid, ObjectTypeName, Content, Name, IsInplaceReport, ObjectType) Values (@Oid, @ObjectTypeName, @Content, @Name, @IsInplaceReport, {0})", xpobj.Oid);
                                    cm.Parameters.Clear();
                                    cm.Parameters.AddWithValue("@Content", row["Content"]);
                                    cm.Parameters.AddWithValue("@IsInplaceReport", row["IsInplaceReport"]);
                                    cm.Parameters.AddWithValue("@Name", row["Name"]);
                                    cm.Parameters.AddWithValue("@ObjectTypeName", row["ObjectTypeName"]);
                                    cm.Parameters.AddWithValue("@Oid", row["Oid"]);
                                    cm.ExecuteNonQuery();
                                    cm.CommandText = "Insert into HRMReport (Oid, NhomBaoCao, TargetTypeName) Values (@Oid, @Group, @TargetTypeName)";
                                    cm.Parameters.Clear();
                                    cm.Parameters.AddWithValue("@Group", row["Group"]);
                                    cm.Parameters.AddWithValue("@TargetTypeName", row["TargetTypeName"]);
                                    cm.Parameters.AddWithValue("@Oid", row["Oid"]);
                                    cm.ExecuteNonQuery();
                                }
                                sReport += "\r\n + " + (string)row["Name"];
                                rptCount++;
                            }
                            obs.CommitChanges();
                            //DungChung.HamDungChung.ShowInfo(this.Application, e.ShowViewParameters, String.Format("Đã cập nhật {0} báo cáo thành công.\r\nCác báo cáo được cập nhật:{1}", rptCount, sReport));

                        }

                        DataSet ds = new DataSet();
                        ds.ReadXml(dlg.FileName,XmlReadMode.ReadSchema);

                        if (ds.Tables.Count > 0)
                            tbl = ds.Tables[0];
                        if (tbl != null)
                        {
                            //update hoặc insert dữ liệu, khi insert có thể insert group và insert nhóm quyền xem báo cáo 'Xem tất cả báo cáo'
                            IObjectSpace obs = Application.CreateObjectSpace();
                            HRMReport report;
                            GroupReport group;
                            SqlConnection cn = (SqlConnection)((XPObjectSpace)Application.CreateObjectSpace()).Session.Connection;
                            SqlCommand cm = new SqlCommand("", cn);
                            string sReport = "";
                            int rptCount = 0;
                            foreach (DataRow row in tbl.Rows)
                            {
                                if (import.NhomBaoCao == null)
                                {
                                    //insert group
                                    group = obs.FindObject<GroupReport>(CriteriaOperator.Parse("Oid=?", row["Group"]));
                                    if (group == null)
                                    {
                                        //tạo nhóm báo cáo mới
                                        cm.CommandText = "INSERT INTO dbo.GroupReport (Oid, TenNhom) Values (@Oid, @TenNhom)";
                                        cm.Parameters.Clear();
                                        cm.Parameters.AddWithValue("@Oid", row["Group"]);
                                        cm.Parameters.AddWithValue("@TenNhom", row["TenNhom"]);
                                        cm.ExecuteNonQuery();
                                    }
                                    group = obs.FindObject<GroupReport>(CriteriaOperator.Parse("Oid=?", row["Group"]));
                                }
                                else
                                    group = obs.GetObjectByKey<GroupReport>(import.NhomBaoCao.Oid);

                                if (!import.GhiDe)
                                {
                                    //insert object type
                                    XPObjectType xpobj = obs.FindObject<XPObjectType>(CriteriaOperator.Parse("TypeName=?", "PSC_HRM.Module.Report.HRMReport"));
                                    if (xpobj == null)
                                    {
                                        cm.CommandText = "INSERT INTO XPObjectType(TypeName,AssemblyName) values(@p1,@p2)";
                                        cm.Parameters.Clear();
                                        cm.Parameters.AddWithValue("@p1", "PSC_HRM.Module.Report.HRMReport");
                                        cm.Parameters.AddWithValue("@p2", "PSC_HRM.Module");
                                        cm.ExecuteNonQuery();
                                        xpobj = obs.FindObject<XPObjectType>(CriteriaOperator.Parse("TypeName=?", "PSC_HRM.Module.Report.HRMReport"));
                                    }

                                    //Lấy id lớn nhất của reportdata
                                    cm.CommandText = "SELECT MAX(Oid) FROM dbo.ReportData";
                                    cm.Parameters.Clear();
                                    object oid = cm.ExecuteScalar();

                                    if (oid != null)
                                    {

                                        ////set identity on
                                        //cm.CommandText = "SET IDENTITY_INSERT dbo.ReportData ON";
                                        cm.CommandText = string.Format("DBCC CHECKIDENT ('dbo.ReportData', RESEED, {0})", oid);
                                        cm.Parameters.Clear();
                                        cm.ExecuteNonQuery();

                                        //insert reportdata
                                        cm.CommandText = "INSERT INTO dbo.ReportData (ObjectTypeName, Content, Name, IsInplaceReport, ObjectType) Values (@ObjectTypeName, @Content, @Name, @IsInplaceReport, @ObjectType)";
                                        cm.Parameters.Clear();
                                        cm.Parameters.AddWithValue("@Content", row["Content"]);
                                        cm.Parameters.AddWithValue("@IsInplaceReport", row["IsInplaceReport"]);
                                        cm.Parameters.AddWithValue("@Name", row["Name"]);
                                        cm.Parameters.AddWithValue("@ObjectTypeName", row["ObjectTypeName"]);
                                        cm.Parameters.AddWithValue("@ObjectType", xpobj.Oid);
                                        cm.ExecuteNonQuery();

                                        //set identity on
                                        cm.CommandText = "SELECT MAX(Oid) FROM dbo.ReportData";
                                        cm.Parameters.Clear();
                                        object oidNew = cm.ExecuteScalar();

                                        if (oidNew != null)
                                        {
                                            //insert hrmreport
                                            cm.CommandText = "INSERT INTO dbo.HRMReport (Oid, NhomBaoCao, TargetTypeName,MaTruong,HinhAnh) Values (@Oid, @NhomBaoCao, @TargetTypeName,@MaTruong,@HinhAnh)";
                                            cm.Parameters.Clear();
                                            cm.Parameters.AddWithValue("@Oid", oidNew);
                                            cm.Parameters.AddWithValue("@NhomBaoCao", group.Oid);
                                            cm.Parameters.AddWithValue("@TargetTypeName", row["TargetTypeName"]);
                                            cm.Parameters.AddWithValue("@MaTruong", TruongConfig.MaTruong);
                                            cm.Parameters.AddWithValue("@HinhAnh", row["HinhAnh"]);
                                            cm.ExecuteNonQuery();
                                        }
                                    }
                                }
                                else
                                {
                                    //tìm report id 
                                    report = obs.GetObjectByKey<HRMReport>(row["Oid"]);
                                    if (report != null)
                                    {
                                        //update, nếu đã bị xóa thì phục hồi lại (GCRecord=NULL)
                                        cm.CommandText = "UPDATE dbo.ReportData SET Content = @Content, IsInplaceReport = @IsInplaceReport, Name = @Name, ObjectTypeName = @ObjectTypeName, GCRecord=NULL WHERE Oid = @Oid";
                                        cm.Parameters.Clear();
                                        cm.Parameters.AddWithValue("@Content", row["Content"]);
                                        cm.Parameters.AddWithValue("@IsInplaceReport", row["IsInplaceReport"]);
                                        cm.Parameters.AddWithValue("@Name", row["Name"]);
                                        cm.Parameters.AddWithValue("@ObjectTypeName", row["ObjectTypeName"]);
                                        cm.Parameters.AddWithValue("@Oid", row["Oid"]);
                                        cm.ExecuteNonQuery();
                                        cm.CommandText = "UPDATE dbo.HRMReport SET TargetTypeName = @TargetTypeName, NhomBaoCao = @NhomBaoCao,MaTruong = @MaTruong, HinhAnh = @HinhAnh WHERE Oid = @Oid";
                                        cm.Parameters.Clear();
                                        cm.Parameters.AddWithValue("@TargetTypeName", row["TargetTypeName"]);
                                        cm.Parameters.AddWithValue("@NhomBaoCao", group.Oid);
                                        cm.Parameters.AddWithValue("@Oid", row["Oid"]);
                                        cm.Parameters.AddWithValue("@MaTruong", TruongConfig.MaTruong);
                                        cm.Parameters.AddWithValue("@HinhAnh", row["HinhAnh"]);
                                        cm.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        //insert object type
                                        XPObjectType xpobj = obs.FindObject<XPObjectType>(CriteriaOperator.Parse("TypeName=?", "PSC_HRM.Module.Report.HRMReport"));
                                        if (xpobj == null)
                                        {
                                            cm.CommandText = "INSERT INTO XPObjectType(TypeName,AssemblyName) values(@p1,@p2)";
                                            cm.Parameters.Clear();
                                            cm.Parameters.AddWithValue("@p1", "PSC_HRM.Module.Report.HRMReport");
                                            cm.Parameters.AddWithValue("@p2", "PSC_HRM.Module");
                                            cm.ExecuteNonQuery();
                                            xpobj = obs.FindObject<XPObjectType>(CriteriaOperator.Parse("TypeName=?", "PSC_HRM.Module.Report.HRMReport"));
                                        }

                                        //Lấy id lớn nhất của reportdata
                                        cm.CommandText = "SELECT MAX(Oid) FROM dbo.ReportData";
                                        cm.Parameters.Clear();
                                        object oid = cm.ExecuteScalar();

                                        if (oid != null)
                                        {

                                            ////set identity on
                                            //cm.CommandText = "SET IDENTITY_INSERT dbo.ReportData ON";
                                            cm.CommandText = string.Format("DBCC CHECKIDENT ('dbo.ReportData', RESEED, {0})", oid);
                                            cm.Parameters.Clear();
                                            cm.ExecuteNonQuery();


                                            //insert reportdata
                                            cm.CommandText = String.Format("INSERT INTO dbo.ReportData (ObjectTypeName, Content, Name, IsInplaceReport, ObjectType) Values (@ObjectTypeName, @Content, @Name, @IsInplaceReport, {0})", xpobj.Oid);
                                            cm.Parameters.Clear();
                                            cm.Parameters.AddWithValue("@Content", row["Content"]);
                                            cm.Parameters.AddWithValue("@IsInplaceReport", row["IsInplaceReport"]);
                                            cm.Parameters.AddWithValue("@Name", row["Name"]);
                                            cm.Parameters.AddWithValue("@ObjectTypeName", row["ObjectTypeName"]);
                                            //cm.Parameters.AddWithValue("@Oid", row["Oid"]);
                                            cm.ExecuteNonQuery();

                                            //Lấy id lớn nhất của reportdata
                                            cm.CommandText = "SELECT MAX(Oid) FROM dbo.ReportData";
                                            cm.Parameters.Clear();
                                            object oidNew = cm.ExecuteScalar();

                                            if (oidNew != null)
                                            {
                                                //insert hrmreport
                                                cm.CommandText = "INSERT INTO dbo.HRMReport (Oid, NhomBaoCao, TargetTypeName,MaTruong,HinhAnh) Values (@Oid, @NhomBaoCao, @TargetTypeName,@MaTruong,@HinhAnh)";
                                                cm.Parameters.Clear();
                                                cm.Parameters.AddWithValue("@Oid", oidNew);
                                                cm.Parameters.AddWithValue("@TargetTypeName", row["TargetTypeName"]);
                                                cm.Parameters.AddWithValue("@NhomBaoCao", group.Oid);
                                                cm.Parameters.AddWithValue("@MaTruong", TruongConfig.MaTruong);
                                                cm.Parameters.AddWithValue("@HinhAnh", row["HinhAnh"]);
                                                cm.ExecuteNonQuery();
                                            }
                                        }

                                        ////set identity on
                                        //cm.CommandText = "SET IDENTITY_INSERT dbo.ReportData OFF";
                                        //cm.Parameters.Clear();
                                        //cm.ExecuteNonQuery();
                                    }
                                }
                                sReport += "\r\n + " + (string)row["Name"];
                                rptCount++;
                            }
                            obs.CommitChanges();
                            XtraMessageBox.Show(String.Format("Đã cập nhật {0} báo cáo thành công.\r\nCác báo cáo được cập nhật:{1}", rptCount, sReport), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                }
            }
        }

    }
}
