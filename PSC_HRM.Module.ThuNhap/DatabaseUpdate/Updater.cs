using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using System.Data;
using PSC_HRM.Module.HoSo;
using System.Text;
using PSC_HRM.Module.ThuNhap.KhauTru;
using PSC_HRM.Module.ThuNhap.TruyLuong;
using PSC_HRM.Module.ThuNhap.Thue;
using PSC_HRM.Module;
//using DevExpress.ExpressApp.Reports;
//using DevExpress.ExpressApp.PivotChart;
//using DevExpress.ExpressApp.Security.Strategy;
//using PSC_HRM.Module.ThuNhap.Module.BusinessObjects;

namespace PSC_HRM.Module.ThuNhap.DatabaseUpdate
{
    // Allows you to handle a database update when the application version changes (http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic help article for more details).
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        // Override to specify the database update code which should be performed after the database schema is updated (http://documentation.devexpress.com/#Xaf/DevExpressExpressAppUpdatingModuleUpdater_UpdateDatabaseAfterUpdateSchematopic).
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}
            //ImportTruyLinh();
            //ImportThue2013();
            //ImportKhamSuKhoe();
            //ImportDinhMucNopThue();
        }

        // Override to perform the required changes with the database structure before the database schema is updated (http://documentation.devexpress.com/#Xaf/DevExpressExpressAppUpdatingModuleUpdater_UpdateDatabaseBeforeUpdateSchematopic).
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }

        private void ImportKhamSuKhoe()
        {
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\LUH_Thang8.xls", "[SheetKhamSucKhoe$]"))
            {
                BangKhauTruLuong bang = ObjectSpace.GetObjectByKey<BangKhauTruLuong>(Guid.Parse("7DABC25B-D2A0-41DB-9E69-EDEF271872C1"));
                if (bang != null)
                {
                    ChiTietKhauTruLuong chiTiet;
                    ThongTinNhanVien nhanVien;
                    StringBuilder sb = new StringBuilder();
                    decimal temp;
                    foreach (DataRow dr in dt.Rows)
                    {
                        nhanVien = ObjectSpace.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("HoTen like ?", dr[0].ToString()));
                        if (nhanVien != null)
                        {
                            chiTiet = ObjectSpace.FindObject<ChiTietKhauTruLuong>(CriteriaOperator.Parse("BangKhauTruLuong=? and ThongTinNhanVien=?", bang.Oid, nhanVien.Oid));
                            if (chiTiet == null)
                            {
                                chiTiet = ObjectSpace.CreateObject<ChiTietKhauTruLuong>();
                                chiTiet.BangKhauTruLuong = bang;
                                chiTiet.BoPhan = nhanVien.BoPhan;
                                chiTiet.ThongTinNhanVien = nhanVien;
                            }
                            if (decimal.TryParse(dr[1].ToString(), out temp))
                                chiTiet.SoTien = temp;
                            chiTiet.Save();
                        }
                        else
                            sb.Append(String.Format("{0} {1}", dr[0], dr[1]));
                    }

                    if (sb.Length > 0)
                        HamDungChung.WriteLog(@"E:\Import\luhsuckhoe.txt", sb.ToString());
                }
            }
        }

        private void ImportTruyLinh()
        {
            //5CE394F5-5F66-4DF5-ADB4-FB4B8DED870C
            string ma = "BHTN";
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\Luong_7_2014_LUH.xls", string.Format("[SheetTL_{0}$]", ma)))
            {
                BangTruyLuong bang = ObjectSpace.GetObjectByKey<BangTruyLuong>(new Guid("5B62B07B-40B5-4329-9120-58A65F35C65D"));

                ThongTinNhanVien nhanVien = null;
                TruyLuongNhanVien truy;
                ChiTietTruyLuong chiTiet;
                decimal temp;
                StringBuilder sb = new StringBuilder();
                foreach (DataRow item in dt.Rows)
                {
                    nhanVien = ObjectSpace.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("HoTen like ?", item[0].ToString().Trim()));
                    if (nhanVien != null
                        && decimal.TryParse(item[2].ToString(), out temp))
                    {
                        truy = ObjectSpace.FindObject<TruyLuongNhanVien>(CriteriaOperator.Parse("BangTruyLuong=? and ThongTinNhanVien=?", bang, nhanVien));
                        if (truy == null)
                        {
                            truy = ObjectSpace.CreateObject<TruyLuongNhanVien>();
                            truy.BangTruyLuong = bang;
                            truy.BoPhan = nhanVien.BoPhan;
                            truy.ThongTinNhanVien = nhanVien;
                            truy.TuNgay = new DateTime(2014, 6, 1);
                            truy.DenNgay = new DateTime(2014, 6, 30);
                        }
                        chiTiet = ObjectSpace.FindObject<ChiTietTruyLuong>(CriteriaOperator.Parse("TruyLuongNhanVien=? and MaChiTiet=?", truy, ma));
                        if (chiTiet == null)
                        {
                            chiTiet = ObjectSpace.CreateObject<ChiTietTruyLuong>();
                            chiTiet.MaChiTiet = ma;
                            chiTiet.DienGiai = "Bảo hiểm thất nghiệp";
                            chiTiet.CongTru = CongTruEnum.Tru;
                            truy.ListChiTietTruyLuong.Add(chiTiet);
                        }
                        chiTiet.SoTien = temp;
                        chiTiet.SoTienChiuThue = 0;

                        truy.Save();
                    }
                    else
                    {
                        sb.AppendLine(String.Format("{0} {1}", item[0], item[2]));
                    }
                }
                bang.Save();
                if (sb.Length > 0)
                    HamDungChung.WriteLog(@"E:\Import\luhtruylinh.txt", sb.ToString());
            }
        }

        private void ImportThamNienCongTac()
        {
            //5CE394F5-5F66-4DF5-ADB4-FB4B8DED870C
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\luhtruylinhtnct.xls", "[Sheet1$]"))
            {
                BangTruyLuong bang = ObjectSpace.GetObjectByKey<BangTruyLuong>(new Guid("5CE394F5-5F66-4DF5-ADB4-FB4B8DED870C"));

                ThongTinNhanVien nhanVien = null;
                TruyLuongNhanVien truy;
                ChiTietTruyLuong chiTiet;
                decimal temp;
                StringBuilder sb = new StringBuilder();
                foreach (DataRow item in dt.Rows)
                {
                    nhanVien = ObjectSpace.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("HoTen like ?", item[0].ToString().Trim()));
                    if (nhanVien != null
                        && decimal.TryParse(item[1].ToString(), out temp))
                    {
                        truy = ObjectSpace.CreateObject<TruyLuongNhanVien>();
                        truy.BangTruyLuong = bang;
                        truy.BoPhan = nhanVien.BoPhan;
                        truy.ThongTinNhanVien = nhanVien;
                        truy.TuNgay = new DateTime(2014, 5, 1);
                        truy.DenNgay = new DateTime(2014, 5, 31);

                        chiTiet = ObjectSpace.CreateObject<ChiTietTruyLuong>();
                        chiTiet.MaChiTiet = "TNCT";
                        chiTiet.DienGiai = "Thâm niên công tác";
                        chiTiet.SoTien = temp;
                        chiTiet.SoTienChiuThue = temp;

                        truy.ListChiTietTruyLuong.Add(chiTiet);
                        truy.Save();
                    }
                    else
                    {
                        sb.AppendLine(String.Format("{0} {1}", item[0], item[1]));
                    }
                }
                bang.Save();
                if (sb.Length > 0)
                    HamDungChung.WriteLog(@"E:\Import\luhtruylinhtnct.txt", sb.ToString());
            }
        }

        private void ImportThue2013()
        {
            //4CC90935-EC95-43C3-A136-E44E4714CA1C
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\LUH_Thang8.xls", "[SheetThueTNCNDot5$]"))
            {
                BangKhauTruLuong bang = ObjectSpace.GetObjectByKey<BangKhauTruLuong>(new Guid("F015292D-3D43-4D77-9423-0EC51A1A9C10"));

                ThongTinNhanVien nhanVien = null;
                ChiTietKhauTruLuong chiTiet;
                decimal temp;
                StringBuilder sb = new StringBuilder();
                foreach (DataRow item in dt.Rows)
                {
                    nhanVien = ObjectSpace.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("HoTen like ?", item[0].ToString().Trim()));
                    if (nhanVien != null
                        && decimal.TryParse(item[1].ToString(), out temp))
                    {
                        chiTiet = ObjectSpace.CreateObject<ChiTietKhauTruLuong>();
                        chiTiet.BangKhauTruLuong = bang;
                        chiTiet.BoPhan = nhanVien.BoPhan;
                        chiTiet.ThongTinNhanVien = nhanVien;
                        chiTiet.SoTien = temp;

                        chiTiet.Save();
                    }
                    else
                    {
                        sb.AppendLine(String.Format("{0} {1}", item[0], item[1]));
                    }
                }
                bang.Save();
                if (sb.Length > 0)
                    HamDungChung.WriteLog(@"E:\Import\luhthuetncn.txt", sb.ToString());
            }
        }

        private void ImportDinhMucNopThue()
        {
            //4CC90935-EC95-43C3-A136-E44E4714CA1C
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\LUH_Thang8.xls", "[SheetDinhMucThueTNCN$]"))
            {
                BangDinhMucNopThueTNCN bang = ObjectSpace.GetObjectByKey<BangDinhMucNopThueTNCN>(new Guid("4012140A-BB57-41FA-A899-B0D151AF909F"));

                ThongTinNhanVien nhanVien = null;
                ChiTietDinhMucNopThueTNCN chiTiet;
                decimal temp;
                StringBuilder sb = new StringBuilder();
                foreach (DataRow item in dt.Rows)
                {
                    nhanVien = ObjectSpace.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("HoTen like ?", item[0].ToString().Trim()));
                    if (nhanVien != null
                        && decimal.TryParse(item[1].ToString(), out temp))
                    {
                        chiTiet = ObjectSpace.CreateObject<ChiTietDinhMucNopThueTNCN>();
                        chiTiet.BangDinhMucNopThueTNCN = bang;
                        chiTiet.BoPhan = nhanVien.BoPhan;
                        chiTiet.ThongTinNhanVien = nhanVien;
                        chiTiet.DinhMucNopThueTNCN = temp;

                        chiTiet.Save();
                    }
                    else
                    {
                        sb.AppendLine(String.Format("{0} {1}", item[0], item[1]));
                    }
                }
                bang.Save();
                if (sb.Length > 0)
                    HamDungChung.WriteLog(@"E:\Import\luhthuetncn.txt", sb.ToString());
            }
        }
    }
}
