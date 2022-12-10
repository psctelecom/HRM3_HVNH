using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo.DB;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class CreateReportNavigationController : WindowController
    {
        private ShowNavigationItemController nav;
        private IObjectSpace obs;
        private Session _ses;

        public CreateReportNavigationController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnFrameAssigned()
        {
            UnsubscribeFromEvents();
            base.OnFrameAssigned();

            nav = Frame.GetController<ShowNavigationItemController>();
            if (nav != null)
            {
                nav.NavigationItemCreated += nav_NavigationItemCreated;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (nav != null)
            {
                nav.NavigationItemCreated -= nav_NavigationItemCreated;
                nav = null;
            }
        }

        private void nav_NavigationItemCreated(object sender, NavigationItemCreatedEventArgs e)
        {
            if (e.NavigationItem.Id == "BaoCao")
            {
                //lấy danh sách group
                obs = Application.CreateObjectSpace();
                _ses = ((XPObjectSpace)obs).Session;
                XPCollection<GroupReport> groups;
                GroupReport gr = null;
                //var groups = obs.GetObjects<GroupReport>().OrderBy(p => p.STT);
                object ktraStore;
                ktraStore = DataProvider.GetObject("SELECT COUNT(*) FROM sys.sql_modules WHERE definition LIKE N'%spd_HeThong_Get_Report%'", CommandType.Text);


                if (Convert.ToInt32(ktraStore) == 0)
                {
                    //groups = new XPCollection<GroupReport>(_ses);
                    //SortingCollection sortCollection = new SortingCollection();
                    //sortCollection.Add(new SortProperty("STT", SortingDirection.Ascending));
                    //groups.Sorting = sortCollection;
                    DataProvider.ExecuteNonQuery("CREATE PROC [dbo].[spd_HeThong_Get_Report]"+ " @User NVARCHAR(max) = ''," + " @isAdmin INT = 1"
                                                        + "   AS" + "   BEGIN"+ "   set @IsAdmin = (SELECT TOP 1 IsAdministrative"+ "   FROM dbo.SecuritySystemUser systemuser"
                                                        + "   JOIN dbo.NguoiSuDung ON NguoiSuDung.Oid = systemuser.Oid"
                                                        + "   JOIN dbo.SecuritySystemUserUsers_SecuritySystemRoleRoles ON SecuritySystemUserUsers_SecuritySystemRoleRoles.Users = systemuser.Oid"
                                                        + "   JOIN dbo.SecuritySystemRole ON SecuritySystemRole.Oid = SecuritySystemUserUsers_SecuritySystemRoleRoles.Roles"
                                                        + "   WHERE systemuser.UserName = @User)"
                                                        + "   IF(@isAdmin = 0)"
                                                        + "   BEGIN"
                                                        + "   DECLARE @PhanQuyenBaoCao UNIQUEIDENTIFIER = ("
                                                        + "   SELECT PhanQuyenBaoCao"
                                                        + "   FROM dbo.SecuritySystemUser"
                                                        + "   JOIN dbo.NguoiSuDung ON NguoiSuDung.Oid = SecuritySystemUser.Oid"
                                                        + "   JOIN dbo.PhanQuyenBaoCao ON PhanQuyenBaoCao.Oid = NguoiSuDung.PhanQuyenBaoCao"
                                                        + "   WHERE UserName = @User"
                                                        + "   AND PhanQuyenBaoCao.GCRecord IS NULL"
                                                        + "   AND SecuritySystemUser.GCRecord IS NULL)"
                                                        + "   SELECT NhomBaoCao, ReportData.OID"
                                                        + "   FROM dbo.HRMReport"
                                                        + "   JOIN dbo.ReportData ON ReportData.OID = HRMReport.OID"
                                                        + "   JOIN dbo.GroupReport ON GroupReport.Oid = HRMReport.NhomBaoCao"
                                                        + "   JOIN(SELECT VALUE FROM  dbo.func_SplitString(((SELECT Quyen"
                                                        + "   FROM dbo.PhanQuyenBaoCao"
                                                        + "   WHERE Oid = @PhanQuyenBaoCao)),';')) PhanQuyen ON PhanQuyen.VALUE = ReportData.OID"
                                                        + "   WHERE ReportData.GCRecord IS NULL"
                                                        + "   END"
                                                        + "   ELSE"
                                                        + "   BEGIN"
                                                        + "   SELECT NhomBaoCao, ReportData.OID"
                                                        + "   FROM dbo.HRMReport"
                                                        + "   JOIN dbo.ReportData ON ReportData.OID = HRMReport.OID"
                                                        + "   JOIN dbo.GroupReport ON GroupReport.Oid = HRMReport.NhomBaoCao"
                                                        + "   WHERE GroupReport.GCRecord IS NULL"
                                                        + "   AND ReportData.GCRecord IS NULL"
                                                        + "   END"
                                                        + "   END ", CommandType.Text);
                }

                var User = HamDungChung.CurrentUser().UserName;
                groups = new XPCollection<GroupReport>(_ses, false);
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@User", User);
                param[1] = new SqlParameter("@IsAdmin", HamDungChung.CheckAdministrator());
                DataTable dsach = DataProvider.GetDataTable("spd_HeThong_Get_Report", System.Data.CommandType.StoredProcedure, param);
                foreach (DataRow item in dsach.Rows)
                {
                    gr = _ses.FindObject<GroupReport>(CriteriaOperator.Parse("Oid = ?", item["NhomBaoCao"].ToString()));
                    if (gr != null)
                    {
                        groups.Add(gr);
                    }
                }

                if (groups.Count() > 0)
                {
                    ChoiceActionItem navItem = e.NavigationItem;
                    PhanQuyenBaoCao phanQuyenBaoCao = HamDungChung.GetPhanQuyenBaoCao();
                    foreach (GroupReport item in groups)
                    {
                        bool isAdmin = HamDungChung.CheckAdministrator();
                        //
                        if (isAdmin || (!isAdmin && phanQuyenBaoCao != null && !String.IsNullOrEmpty(phanQuyenBaoCao.Quyen) && phanQuyenBaoCao.IsExists(item)))
                        {
                            //Create ListView
                            IModelListView listViewNode = (IModelListView)Application.Model.Views[string.Format("HRMReport_{0}_ListView", item.Oid)];
                            if (listViewNode == null)
                            {
                                listViewNode = Application.Model.Views.AddNode<IModelListView>(string.Format("HRMReport_{0}_ListView", item.Oid));
                                listViewNode.ModelClass = Application.Model.BOModel.GetClass(typeof(HRMReport));
                                listViewNode.Caption = string.Format("Báo cáo {0}", item.TenNhom);
                                listViewNode.MasterDetailMode = MasterDetailMode.ListViewAndDetailView;
                                listViewNode.DetailView = Application.Model.Views["HRMReport_Short_DetailView"] as IModelDetailView;
                                listViewNode.MasterDetailView = Application.Model.Views["HRMReport_Short_DetailView"] as IModelDetailView;
                                listViewNode.ImageName = "BO_Report";
                                listViewNode.AllowNew = false;
                            }
                            listViewNode.Criteria = string.Format("NhomBaoCao.Oid='{0}' and MaTruong='{1}'", item.Oid, TruongConfig.MaTruong);
                            IModelColumn column = listViewNode.Columns["ReportName"];
                            if (column == null)
                            {
                                column = listViewNode.Columns.AddNode<IModelColumn>("ReportName");
                                column.PropertyName = "ReportName";
                                column.Caption = "Tên báo cáo";
                                column.SortIndex = 0;
                                column.Width = 200;
                                column.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                                column.PropertyEditorType = typeof(StringPropertyEditor);
                            }

                            //Create navigation
                            ViewShortcut shortcut = new ViewShortcut(typeof(HRMReport), null, String.Format("HRMReport_{0}_ListView", item.Oid));
                            ChoiceActionItem subItem = new ChoiceActionItem(item.Oid.ToString(), item.TenNhom, shortcut)
                            {
                                ImageName = "BO_Report"
                            };

                            navItem.Items.Add(subItem);
                        }
                    }
                }
            }
        }
    }
}
