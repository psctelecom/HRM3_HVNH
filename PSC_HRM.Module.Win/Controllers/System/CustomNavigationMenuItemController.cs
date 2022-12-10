using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.Win.Editors;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using System.Windows.Forms;
using DevExpress.ExpressApp.Model.Core;
using PSC_HRM.Module.CauHinh;
using PSC_HRM.Module.BanLamViec;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class CustomNavigationMenuItemController : ViewController
    {
        IObjectSpace _obs = null;
        ShowNavigationItemController _nav;
        Session _ses = null;

        public CustomNavigationMenuItemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            _nav = Frame.GetController<ShowNavigationItemController>();
            if (_nav != null)
            {
                _nav.NavigationItemCreated += nav_NavigationItemCreated;
                _nav.CustomShowNavigationItem += nav_CustomShowNavigationItem;
            }
        }

        void nav_NavigationItemCreated(object sender, NavigationItemCreatedEventArgs e)
        {
            //
            _obs = Application.CreateObjectSpace();
            _ses = ((XPObjectSpace)_obs).Session;

            object ktraStore = DataProvider.GetObject("SELECT COUNT(*) FROM sys.sql_modules WHERE definition LIKE N'%spd_HeThong_GetMenuGroup%'", CommandType.Text);
            if (Convert.ToInt32(ktraStore) == 0)
            {
                DataProvider.ExecuteNonQuery("CREATE PROCEDURE [dbo].[spd_HeThong_GetMenuGroup]"
                                             + "   @User UNIQUEIDENTIFIER,"
                                             + "  @IsAdmin INT = 0"
                                             + "  AS"
                                             + "  BEGIN"
                                             + "  set @IsAdmin = (SELECT TOP 1 IsAdministrative"
                                             + "  FROM dbo.SecuritySystemUser systemuser"
                                             + "  JOIN dbo.NguoiSuDung ON NguoiSuDung.Oid = systemuser.Oid"
                                             + "  JOIN dbo.SecuritySystemUserUsers_SecuritySystemRoleRoles ON SecuritySystemUserUsers_SecuritySystemRoleRoles.Users = systemuser.Oid"
                                             + "  JOIN dbo.SecuritySystemRole ON SecuritySystemRole.Oid = SecuritySystemUserUsers_SecuritySystemRoleRoles.Roles"
                                             + "  WHERE systemuser.Oid = @User)"
                                             + "   IF(@IsAdmin = 0)"
                                             + "   BEGIN"
                                             + "   SELECT DISTINCT AppMenu.PhanHe, CASE WHEN AppMenu.PhanHe = 0 THEN 'NhanSu'"
                                             + "                                   WHEN AppMenu.PhanHe = 1 THEN 'TaiChinh'"
                                             + "                                   WHEN AppMenu.PhanHe = 2 THEN 'DanhMuc'"
                                             + "                                   WHEN AppMenu.PhanHe = 3 THEN 'HeThong'"
                                             + "                                   WHEN AppMenu.PhanHe = 4 THEN 'BanLamViec'"
                                             + "                                  WHEN AppMenu.PhanHe = 5 THEN 'PMS'"
                                             + "                              END TenPhanHe"
                                             + "  FROM dbo.SecuritySystemRole"
                                             + "      JOIN dbo.SecuritySystemUserUsers_SecuritySystemRoleRoles ON SecuritySystemUserUsers_SecuritySystemRoleRoles.Roles = SecuritySystemRole.Oid"
                                             + "      JOIN dbo.SecuritySystemUser ON SecuritySystemUser.Oid = SecuritySystemUserUsers_SecuritySystemRoleRoles.Users"
                                             + "      JOIN dbo.SecuritySystemTypePermissionsObject ON SecuritySystemTypePermissionsObject.Owner = SecuritySystemRole.Oid"
                                             + "     JOIN AppObject ON KeyObject = RIGHT(SecuritySystemTypePermissionsObject.TargetType, CHARINDEX('.', reverse(SecuritySystemTypePermissionsObject.TargetType) + '.') - 1)"
                                             + "      JOIN dbo.AppMenu ON AppMenu.AppObject = AppObject.Oid"
                                             + "   WHERE((SecuritySystemUser.Oid = @User AND @IsAdmin = 0) OR @IsAdmin = 1)"
                                             + "          AND ISNULL(SuDung,0) = 1"
                                             + "         AND AppMenu.GCRecord IS NULL"
                                             + "   UNION ALL"
                                             + "   SELECT DISTINCT AppMenu.PhanHe, CASE WHEN AppMenu.PhanHe = 0 THEN 'NhanSu'"
                                             + "                                                                       WHEN AppMenu.PhanHe = 1 THEN 'TaiChinh'"
                                             + "                                                                       WHEN AppMenu.PhanHe = 2 THEN 'DanhMuc'"
                                             + "                                                                      WHEN AppMenu.PhanHe = 3 THEN 'HeThong'"
                                             + "                                                                     WHEN AppMenu.PhanHe = 4 THEN 'BanLamViec'"
                                             + "                                                                       WHEN AppMenu.PhanHe = 5 THEN 'PMS'"
                                             + "   END TenPhanHe"
                                             + "   FROM dbo.PhanQuyenChucNangPhu"
                                             + "   JOIN dbo.NguoiSuDungChucNangPhu ON NguoiSuDungChucNangPhu.PhanQuyenChucNangPhu = PhanQuyenChucNangPhu.Oid"
                                             + "   JOIN dbo.DanhSachChucNangPhu ON DanhSachChucNangPhu.PhanQuyenChucNangPhu = PhanQuyenChucNangPhu.Oid"
                                             + "   JOIN dbo.AppMenu ON AppMenu.Oid = DanhSachChucNangPhu.AppMenu"
                                             + "   WHERE(NguoiSuDungChucNangPhu.NguoiSuDung = @User OR @IsAdmin = 1)"
                                             + "   AND ISNULL(AppMenu.SuDung, 0) = 1"
                                             + "   END"
                                             + "   ELSE"
                                             + "   BEGIN"
                                             + "   SELECT DISTINCT AppMenu.PhanHe,"
                                             + "                           CASE WHEN AppMenu.PhanHe = 0 THEN 'NhanSu'"
                                             + "                                  WHEN AppMenu.PhanHe = 1 THEN 'TaiChinh'"                                         
                                             + "                                  WHEN AppMenu.PhanHe = 2 THEN 'DanhMuc'"
                                             + "                                   WHEN AppMenu.PhanHe = 3 THEN 'HeThong'"
                                             + "                                   WHEN AppMenu.PhanHe = 4 THEN 'BanLamViec'"
                                             + "                                   WHEN AppMenu.PhanHe = 5 THEN 'PMS'"
                                             + "                               END TenPhanHe"
                                             + "   FROM dbo.AppMenu"
                                             + "   WHERE GCRecord IS NULL"
                                             + "       AND ISNULL(SuDung, 0) = 1"
                                             + "   END"
                                             + "   END", CommandType.Text);
            }

            var User = HamDungChung.CurrentUser().Oid;
            XPCollection<AppMenu> thuMucList;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@User", User);
            param[1] = new SqlParameter("@IsAdmin", HamDungChung.CheckAdministrator());
            DataTable dsach = DataProvider.GetDataTable("spd_HeThong_GetMenuGroup", System.Data.CommandType.StoredProcedure, param);
            foreach (DataRow item in dsach.Rows)
            {
                if (e.NavigationItem.Id.Contains(item["TenPhanHe"].ToString()))
                {
                    object ktraStore1 = DataProvider.GetObject("SELECT COUNT(*) FROM sys.sql_modules WHERE definition LIKE N'%spd_HeThong_GetMenuFolder%'", CommandType.Text);
                    if (Convert.ToInt32(ktraStore1) == 0)
                    {
                        string create = "CREATE PROCEDURE[dbo].[spd_HeThong_GetMenuFolder]"
                                                     + "   ("
                                                     + "        @User UNIQUEIDENTIFIER,"
                                                     + "        @PhanHe INT,"
                                                     + "        @IsAdmin INT = 0"
                                                     + "   )"
                                                     + "   AS"
                                                     + "   BEGIN"
                                                     + "   set @IsAdmin = (SELECT TOP 1 IsAdministrative"
                                                     + "   FROM dbo.SecuritySystemUser systemuser"
                                                     + "   JOIN dbo.NguoiSuDung ON NguoiSuDung.Oid = systemuser.Oid"
                                                     + "   JOIN dbo.SecuritySystemUserUsers_SecuritySystemRoleRoles ON SecuritySystemUserUsers_SecuritySystemRoleRoles.Users = systemuser.Oid"
                                                     + "   JOIN dbo.SecuritySystemRole ON SecuritySystemRole.Oid = SecuritySystemUserUsers_SecuritySystemRoleRoles.Roles"
                                                     + "   WHERE systemuser.Oid = @User)"
                                                     + "   IF(@IsAdmin = 0)"
                                                     + "   BEGIN"
                                                     + "   SELECT DISTINCT thumuc.Oid,"
                                                     + "                   thumuc.PhanHe,"
                                                     + "                   thumuc.SoThuTu"
                                                     + "   FROM dbo.SecuritySystemRole"
                                                     + "       JOIN dbo.SecuritySystemUserUsers_SecuritySystemRoleRoles ON SecuritySystemUserUsers_SecuritySystemRoleRoles.Roles = SecuritySystemRole.Oid"
                                                     + "       JOIN dbo.SecuritySystemUser ON SecuritySystemUser.Oid = SecuritySystemUserUsers_SecuritySystemRoleRoles.Users"
                                                     + "       JOIN dbo.SecuritySystemTypePermissionsObject ON SecuritySystemTypePermissionsObject.Owner = SecuritySystemRole.Oid"
                                                     + "       JOIN AppObject ON KeyObject = RIGHT(SecuritySystemTypePermissionsObject.TargetType, CHARINDEX('.', reverse(SecuritySystemTypePermissionsObject.TargetType) + '.') - 1)"
                                                     + "       JOIN dbo.AppMenu ON AppMenu.AppObject = AppObject.Oid"
                                                     + "       JOIN dbo.AppMenu thumuc ON thumuc.Oid = AppMenu.ThuMucQuanLy"
                                                     + "   WHERE(SecuritySystemUser.Oid = @User OR @IsAdmin = 1)"
                                                     + "           AND ISNULL(AppMenu.SuDung, 0) = 1"
                                                     + "           AND ISNULL(thumuc.LaThuMuc, 0) = 1"
                                                     + "            AND thumuc.PhanHe = @PhanHe"
                                                     + "           AND thumuc.GCRecord IS NULL"
                                                     + "   UNION ALL"
                                                     + "   SELECT DISTINCT thumuc.Oid,"
                                                     + "                  thumuc.PhanHe,"
                                                     + "                  thumuc.SoThuTu"
                                                     + "   FROM dbo.PhanQuyenChucNangPhu"
                                                     + "       JOIN dbo.NguoiSuDungChucNangPhu ON NguoiSuDungChucNangPhu.PhanQuyenChucNangPhu = PhanQuyenChucNangPhu.Oid"
                                                     + "       JOIN dbo.DanhSachChucNangPhu ON DanhSachChucNangPhu.PhanQuyenChucNangPhu = PhanQuyenChucNangPhu.Oid"
                                                     + "       JOIN dbo.AppMenu ON AppMenu.Oid = DanhSachChucNangPhu.AppMenu"
                                                     + "       JOIN dbo.AppMenu thumuc ON thumuc.Oid = AppMenu.ThuMucQuanLy"
                                                     + "   WHERE(NguoiSuDungChucNangPhu.NguoiSuDung = @User OR @IsAdmin = 1)"
                                                     + "           AND ISNULL(AppMenu.SuDung, 0) = 1"
                                                     + "           AND ISNULL(thumuc.LaThuMuc, 0) = 1"
                                                     + "          AND thumuc.PhanHe = @PhanHe"
                                                     + "           AND thumuc.GCRecord IS NULL"
                                                     + "   ORDER BY thumuc.SoThuTu"
                                                     + "   END"
                                                     + "   ELSE"
                                                     + "   BEGIN"
                                                     + "   SELECT DISTINCT thumuc.Oid," 					
                                                     + "                   thumuc.PhanHe,"
                                                     + "                   thumuc.SoThuTu"
                                                     + "   FROM dbo.AppMenu thumuc"
                                                     + "   WHERE ISNULL(thumuc.SuDung, 0) = 1"
                                                     + "           AND ISNULL(thumuc.SuDung, 0) = 1"
                                                     + "           AND ISNULL(thumuc.LaThuMuc, 0) =1 "
                                                     + "           AND thumuc.PhanHe = @PhanHe"
                                                     + "           AND thumuc.GCRecord IS NULL"
                                                     + "   ORDER BY thumuc.SoThuTu"
                                                     + "   END"
                                                     + "   END";
                        DataProvider.ExecuteNonQuery(create, CommandType.Text);
                    }

                    thuMucList = new XPCollection<AppMenu>(_ses, false);
                    SqlParameter[] param1 = new SqlParameter[3];
                    param1[0] = new SqlParameter("@User", User);
                    param1[1] = new SqlParameter("@PhanHe", Convert.ToInt32(item["PhanHe"].ToString()));
                    param1[2] = new SqlParameter("@IsAdmin", HamDungChung.CheckAdministrator());
                    DataTable Folder = DataProvider.GetDataTable("spd_HeThong_GetMenuFolder", System.Data.CommandType.StoredProcedure, param1);
                    foreach (DataRow item1 in Folder.Rows)
                    {
                        AppMenu app = _ses.FindObject<AppMenu>(CriteriaOperator.Parse("Oid = ?", item1["Oid"].ToString()));
                        if (app != null)
                        {
                            thuMucList.Add(app);
                        }
                    }
                    GetNavigationItemfromAppMenu(e, thuMucList, Convert.ToInt32(item["PhanHe"].ToString()));
                }
            }

                    ////Nhân sự
                    //if (e.NavigationItem.Id.Contains("NhanSu"))
                    //{
                    //    CriteriaOperator filter = CriteriaOperator.Parse("SuDung and PhanHe = 0 and LaThuMuc");
                    //    XPCollection<AppMenu> thuMucList = new XPCollection<AppMenu>(((XPObjectSpace)_obs).Session, filter, new SortProperty("SoThuTu", DevExpress.Xpo.DB.SortingDirection.Ascending));
                    //    //
                    //    GetNavigationItemfromAppMenu(e, thuMucList, 0);
                    //}

                    ////Tài chính
                    //if (e.NavigationItem.Id.Contains("TaiChinh"))
                    //{
                    //    CriteriaOperator filter = CriteriaOperator.Parse("SuDung and PhanHe = 1 and LaThuMuc");
                    //    XPCollection<AppMenu> thuMucList = new XPCollection<AppMenu>(((XPObjectSpace)_obs).Session, filter, new SortProperty("SoThuTu", DevExpress.Xpo.DB.SortingDirection.Ascending));
                    //    //
                    //    GetNavigationItemfromAppMenu(e, thuMucList, 1);
                    //}

                    ////Danh mục
                    //if (e.NavigationItem.Id.Contains("DanhMuc"))
                    //{
                    //    CriteriaOperator filter = CriteriaOperator.Parse("SuDung and PhanHe = 2 and LaThuMuc");
                    //    XPCollection<AppMenu> thuMucList = new XPCollection<AppMenu>(((XPObjectSpace)_obs).Session, filter, new SortProperty("SoThuTu", DevExpress.Xpo.DB.SortingDirection.Ascending));
                    //    //
                    //    GetNavigationItemfromAppMenu(e, thuMucList, 2);
                    //}

                    ////Hệ thống
                    //if (e.NavigationItem.Id.Contains("HeThong"))
                    //{
                    //    CriteriaOperator filter = CriteriaOperator.Parse("SuDung and PhanHe = 3 and LaThuMuc");
                    //    XPCollection<AppMenu> thuMucList = new XPCollection<AppMenu>(((XPObjectSpace)_obs).Session, filter, new SortProperty("SoThuTu", DevExpress.Xpo.DB.SortingDirection.Ascending));
                    //    //
                    //    GetNavigationItemfromAppMenu(e, thuMucList, 3);
                    //}

                    ////Nhắc việc
                    //if (e.NavigationItem.Id.Contains("BanLamViec"))
                    //{
                    //    CriteriaOperator filter = CriteriaOperator.Parse("SuDung and PhanHe = 4 and LaThuMuc");
                    //    XPCollection<AppMenu> thuMucList = new XPCollection<AppMenu>(((XPObjectSpace)_obs).Session, filter, new SortProperty("SoThuTu", DevExpress.Xpo.DB.SortingDirection.Ascending));
                    //    //
                    //    GetNavigationItemfromAppMenu(e, thuMucList, 4);
                    //    //
                    //}
                    ////PMS
                    //if (e.NavigationItem.Id.Contains("PMS"))
                    //{
                    //    CriteriaOperator filter = CriteriaOperator.Parse("SuDung and PhanHe = 5 and LaThuMuc");
                    //    XPCollection<AppMenu> thuMucList = new XPCollection<AppMenu>(((XPObjectSpace)_obs).Session, filter, new SortProperty("SoThuTu", DevExpress.Xpo.DB.SortingDirection.Ascending));
                    //    //

                    //    //
                    //    GetNavigationItemfromAppMenu(e, thuMucList, 5);
                    //}
                }

        private void GetNavigationItemfromAppMenu(NavigationItemCreatedEventArgs e, XPCollection<AppMenu> thuMucList, int phanHe)
        {
            foreach (AppMenu itemThuMuc in thuMucList)
            {
                //
                ChoiceActionItem thuMuc = new ChoiceActionItem();
                thuMuc.ImageName = itemThuMuc.HinhAnh;
                thuMuc.Caption = itemThuMuc.TenChucNang;

                //
                CriteriaOperator filter = CriteriaOperator.Parse("SuDung and PhanHe = ? and !LaThuMuc and ThuMucQuanLy = ?", phanHe, itemThuMuc.Oid);
                XPCollection<AppMenu> chucNangList = new XPCollection<AppMenu>(((XPObjectSpace)_obs).Session, filter, new SortProperty("SoThuTu", DevExpress.Xpo.DB.SortingDirection.Ascending));
                foreach (AppMenu itemChucNang in chucNangList)
                {
                    //Tìm object trong source
                    Type objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == itemChucNang.AppObject.KeyObject);
                    if (objecttype != null)
                    {
                        bool isAccess = HamDungChung.IsAccessGranted(objecttype);
                        //
                        if (isAccess || HamDungChung.CurrentUser().UserName == "admin") // Nếu có quyền truy cập  mới tạo menu
                        {
                            ChoiceActionItem chucNang = new ChoiceActionItem();
                            chucNang.ImageName = itemChucNang.HinhAnh;
                            chucNang.Caption = itemChucNang.TenChucNang;
                            string loaiView = string.Empty;
                            if (itemChucNang.LoaiView == LoaiViewEnum.ListView)
                            {
                                loaiView = "ListView";

                            }
                            else if (itemChucNang.LoaiView == LoaiViewEnum.DetailView)
                            {
                                loaiView = "DetailView";
                            }
                            else
                            {
                                string loaiCustom = string.Empty;
                                if (itemChucNang.LoaiCustom == LoaiCustomEnum.ConCongTacTaiTruong)
                                {
                                    loaiCustom = "ConCongTacTaiTruong";
                                }
                                else if (itemChucNang.LoaiCustom == LoaiCustomEnum.KhongConCongTacTaiTruong)
                                {
                                    loaiCustom = "KhongConCongTacTaiTruong";
                                }
                                else if (itemChucNang.LoaiCustom == LoaiCustomEnum.NhanVienCongNhat)
                                {
                                    loaiCustom = "NhanVienCongNhat";
                                }
                                else if (itemChucNang.LoaiCustom == LoaiCustomEnum.GiangVienThinhGiang)
                                {
                                    loaiCustom = "GiangVienThinhGiang";
                                }
                                else if (itemChucNang.LoaiCustom == LoaiCustomEnum.GiangVienThinhGiangNghiViec)
                                {
                                    loaiCustom = "GiangVienThinhGiangNghiViec";
                                }
                                else
                                {
                                    loaiCustom = "ToChucDang";
                                }

                                loaiView = string.Format("{0}_CustomCategorizedListEditor", loaiCustom);
                            }
                            chucNang.Id = string.Format("{0}_{1}", itemChucNang.AppObject.KeyObject, loaiView);
                            //
                            thuMuc.Items.Add(chucNang);
                            //
                            if ((TruongConfig.MaTruong.Equals("BUH") || TruongConfig.MaTruong.Equals("DLU")) && chucNang.Id.Equals("NhacViec_HetHanDiHocNuocNgoai_DetailView"))
                                _nav.ShowNavigationItemAction.DoExecute(chucNang);
                        }
                    }

                    //Riêng biểu đồ thống kê và quy trình không phân quyền
                    if (itemChucNang.LoaiView == LoaiViewEnum.DashboardView)
                    {
                        ChoiceActionItem chucNang = new ChoiceActionItem();
                        chucNang.ImageName = itemChucNang.HinhAnh;
                        chucNang.Caption = itemChucNang.TenChucNang;
                        chucNang.Id = string.Format("{0}_{1}", itemChucNang.AppObject.KeyObject, "DashboardView");
                        //
                        thuMuc.Items.Add(chucNang);
                        //
                        //if (TruongConfig.MaTruong.Equals("BUH") && chucNang.Id.Equals("ThongKeLoaiNhanSu_DashboardView"))
                        //    _nav.ShowNavigationItemAction.DoExecute(chucNang);
                    }
                }
                //
                e.NavigationItem.Items.Add(thuMuc);
            }
        }

        void nav_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            using (DialogUtil.AutoWait("Đang truy cập " + e.ActionArguments.SelectedChoiceActionItem.Caption))
            {
                #region ListView
                if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("_ListView"))
                {
                    string objectName = e.ActionArguments.SelectedChoiceActionItem.Id.ToString().Substring(0, e.ActionArguments.SelectedChoiceActionItem.Id.Length - 9);

                    //Tìm object trong source
                    Type objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == objectName);
                    if (objecttype == null)
                        return;

                    string listViewId = Application.FindListViewId(objecttype);
                    CollectionSourceBase cs = Application.CreateCollectionSource(_obs, objecttype, listViewId);
                    //
                    e.ActionArguments.ShowViewParameters.CreatedView = Application.CreateListView(listViewId, cs, true);
                    e.ActionArguments.ShowViewParameters.Context = TemplateContext.View;
                    e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                }
                #endregion

                #region DetailView
                if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("_DetailView"))
                {
                    DetailView view = null;
                    if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("CauHinhChung"))
                    {
                        //
                        CauHinhChung cauHingChung = _obs.FindObject<CauHinhChung>(CriteriaOperator.Parse("ThongTinTruong.Oid=?", HamDungChung.ThongTinTruong(((XPObjectSpace)_obs).Session)));
                        view = Application.CreateDetailView(_obs, cauHingChung, true);
                    }
                    else
                    {
                        //
                        string objectName = e.ActionArguments.SelectedChoiceActionItem.Id.ToString().Substring(0, e.ActionArguments.SelectedChoiceActionItem.Id.Length - 11);
                        //Tìm object trong source
                        Type objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == objectName);
                        if (objecttype == null)
                            return;
                        //
                        object obj = _obs.CreateObject(objecttype);
                        //
                        view = Application.CreateDetailView(_obs, obj, true);
                    }
                    //
                    view.ViewEditMode = ViewEditMode.Edit;
                    e.ActionArguments.ShowViewParameters.CreatedView = view;
                    e.ActionArguments.ShowViewParameters.Context = TemplateContext.View;
                    e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                }
                #endregion

                #region DashboardView
                if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("_DashboardView"))
                {
                    //
                    if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("QuyTrinh"))
                    {
                        e.ActionArguments.ShowViewParameters.CreatedView = Application.CreateDashboardView(_obs, "QuyTrinh_DashboardView", true);
                    }
                    else
                    {
                        e.ActionArguments.ShowViewParameters.CreatedView = Application.CreateDashboardView(_obs, "ThongKe_DashboardView", true);
                    }
                    //
                    e.ActionArguments.ShowViewParameters.Context = TemplateContext.View;
                    e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                }
                #endregion

                #region Custom
                if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("_ConCongTacTaiTruong_CustomCategorizedListEditor"))
                {
                    //using (DialogUtil.AutoWait("Đang truy cập " + e.ActionArguments.SelectedChoiceActionItem.Caption))
                    {
                        object obj = _obs.CreateObject(typeof(ThongTinNhanVien));
                        IModelListView listViewModel = GetListViewModel(obj, "ThongTinNhanVien_RutGon_ListView", "TinhTrang.KhongConCongTacTaiTruong=0");
                        //
                        e.ActionArguments.ShowViewParameters.CreatedView = Application.CreateListView(listViewModel, Application.CreateCollectionSource(_obs, obj.GetType(), listViewModel.Id), true);
                        e.ActionArguments.ShowViewParameters.Context = TemplateContext.View;
                        e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;

                        // Refesh lại view để không bắt save
                        _obs.Refresh();
                    }
                }
                if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("_KhongConCongTacTaiTruong_CustomCategorizedListEditor"))
                {
                    string listViewId = Application.FindListViewId(typeof(ThongTinNhanVien));
                    CollectionSourceBase cs = Application.CreateCollectionSource(_obs, typeof(ThongTinNhanVien), listViewId);
                    //
                    DevExpress.ExpressApp.ListView listView = Application.CreateListView(listViewId, cs, true);
                    listView.CollectionSource.Criteria["ThongTinNhanVien"] = CriteriaOperator.Parse("TinhTrang.KhongConCongTacTaiTruong");
                    //
                    e.ActionArguments.ShowViewParameters.CreatedView = listView;
                    e.ActionArguments.ShowViewParameters.Context = TemplateContext.View;
                    e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;

                    // Refesh lại view để không bắt save
                    _obs.Refresh();
                }

                //if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("_NhanVienCongNhat_CustomCategorizedListEditor"))
                //{
                //    object obj = _obs.CreateObject<ThongTinNhanVien>();
                //    IModelListView listViewModel = GetListViewModel(obj, "NhanVienCongNhat_ListView", "TinhTrang.KhongConCongTacTaiTruong = 0 And NhanVienThongTinLuong.PhanLoai = 4 ");
                //    //
                //    e.ActionArguments.ShowViewParameters.CreatedView = Application.CreateListView(listViewModel, Application.CreateCollectionSource(_obs, obj.GetType(), listViewModel.Id), true);
                //    e.ActionArguments.ShowViewParameters.Context = TemplateContext.View;
                //    e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;

                //    // Refesh lại view để không bắt save
                //    _obs.Refresh();
                //}

                if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("_GiangVienThinhGiang_CustomCategorizedListEditor"))
                {
                    object obj = _obs.CreateObject<GiangVienThinhGiang>();
                    IModelListView listViewModel = GetListViewModel(obj, "GiangVienThinhGiang_RutGon_ListView", "TinhTrang.KhongConCongTacTaiTruong = 0");
                    //
                    e.ActionArguments.ShowViewParameters.CreatedView = Application.CreateListView(listViewModel, Application.CreateCollectionSource(_obs, obj.GetType(), listViewModel.Id), true);
                    e.ActionArguments.ShowViewParameters.Context = TemplateContext.View;
                    e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;

                    // Refesh lại view để không bắt save
                    _obs.Refresh();
                }
                if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("_GiangVienThinhGiangNghiViec_CustomCategorizedListEditor"))
                {
                    string listViewId = Application.FindListViewId(typeof(GiangVienThinhGiang));
                    CollectionSourceBase cs = Application.CreateCollectionSource(_obs, typeof(GiangVienThinhGiang), listViewId);
                    //
                    DevExpress.ExpressApp.ListView listView = Application.CreateListView(listViewId, cs, true);
                    listView.CollectionSource.Criteria["GiangVienThinhGiang"] = CriteriaOperator.Parse("TinhTrang.KhongConCongTacTaiTruong");
                    //
                    e.ActionArguments.ShowViewParameters.CreatedView = listView;
                    e.ActionArguments.ShowViewParameters.Context = TemplateContext.View;
                    e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;

                    // Refesh lại view để không bắt save
                    _obs.Refresh();
                }
                #endregion
            }
        }

        private IModelListView GetListViewModel(object obj, string viewId, string criteria)
        {
            IModelListView modelListView = Application.FindModelView(viewId) as IModelListView;
            if (modelListView == null)
            {
                modelListView = Application.Model.Views.AddNode<IModelListView>(viewId);
                modelListView.ModelClass = Application.Model.BOModel.GetClass(obj.GetType());
                modelListView.MasterDetailMode = MasterDetailMode.ListViewAndDetailView;
                modelListView.Criteria = criteria;
            }
            return modelListView;
        }
    }
}
