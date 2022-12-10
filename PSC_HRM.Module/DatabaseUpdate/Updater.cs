using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using PSC_HRM.Module.BaoMat;
using System.Data;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.CauHinh;
using System.Text;
using System.Threading;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using DevExpress.ExpressApp.Editors;
using System.Diagnostics;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.Report;
using DevExpress.Xpo;
using PSC_HRM.Module;


namespace PSC_HRM.Module.DatabaseUpdate
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
            
            //DataProvider.ExecuteNonQuery("spd_HeThong_TaoMenu_Updater", CommandType.StoredProcedure);

            //SecuritySystemRole adminRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
            //if (adminRole == null)
            //{
            //    adminRole = ObjectSpace.CreateObject<SecuritySystemRole>();
            //    adminRole.Name = SecurityStrategy.AdministratorRoleName;
            //    adminRole.IsAdministrative = true;
            //    adminRole.Save();
            //}

            //NguoiSuDung admin = ObjectSpace.FindObject<NguoiSuDung>(new BinaryOperator("UserName", "admin"));
            //if (admin == null)
            //{
            //    admin = ObjectSpace.CreateObject<NguoiSuDung>();
            //    admin.UserName = "admin";
            //    admin.SetPassword("a");
            //    admin.Roles.Add(adminRole);                
            //    admin.Save();
            //}

            #region Tạo chức năng
            //AppMenu appHeThong = ObjectSpace.FindObject<AppMenu>(CriteriaOperator.Parse("TenChucNang like? and LaThuMuc =? and PhanHe =? and SuDung =?", "%Quản lý menu%", true, PhanHeEnum.HeThong, true));
            //if(appHeThong==null)
            //{
            //    appHeThong = ObjectSpace.CreateObject<AppMenu>();
            //    appHeThong.LaThuMuc = true;
            //    appHeThong.TenChucNang = "Quản lý menu";
            //    appHeThong.PhanHe = PhanHeEnum.HeThong;
            //    appHeThong.SuDung = true;
            //    appHeThong.HinhAnh = "BO_Folder";
            //    appHeThong.Save();
            //}

            //AppObject appObject_AppMenu = ObjectSpace.FindObject<AppObject>(CriteriaOperator.Parse("KeyObject =?", "AppMenu"));
            //if (appObject_AppMenu == null)
            //{
            //    appObject_AppMenu = ObjectSpace.CreateObject<AppObject>();
            //    appObject_AppMenu.KeyObject = "AppMenu";
            //    appObject_AppMenu.Caption = "Chức năng hệ thống";
            //    appObject_AppMenu.Save();
            //}
            //AppMenu appChucNangHT = ObjectSpace.FindObject<AppMenu>(CriteriaOperator.Parse("AppObject =?", appObject_AppMenu.Oid));
            //if (appChucNangHT == null)
            //{
            //    appChucNangHT = ObjectSpace.CreateObject<AppMenu>();
            //    appChucNangHT.LaThuMuc = false;
            //    appChucNangHT.TenChucNang = "Chức năng hệ thống";
            //    appChucNangHT.PhanHe = PhanHeEnum.HeThong;
            //    appChucNangHT.SuDung = true;
            //    appChucNangHT.HinhAnh = "BO_Contact";
            //    appChucNangHT.LoaiView = 0;
            //    appChucNangHT.LoaiCustom = 0;
            //    appChucNangHT.AppObject = appObject_AppMenu;
            //    appChucNangHT.ThuMucQuanLy = appHeThong;
            //    appChucNangHT.Save();
            //}

            //AppObject appObject_SecuritySystemRole = ObjectSpace.FindObject<AppObject>(CriteriaOperator.Parse("KeyObject =?", "SecuritySystemRole"));
            //if (appObject_SecuritySystemRole == null)
            //{
            //    appObject_SecuritySystemRole = ObjectSpace.CreateObject<AppObject>();
            //    appObject_SecuritySystemRole.KeyObject = "SecuritySystemRole";
            //    appObject_SecuritySystemRole.Caption = "Phân quyền chức năng";
            //    appObject_SecuritySystemRole.Save();
            //}
            //AppMenu appPhanQuyen = ObjectSpace.FindObject<AppMenu>(CriteriaOperator.Parse("AppObject =?", appObject_SecuritySystemRole.Oid));
            //if (appPhanQuyen == null)
            //{
            //    appPhanQuyen = ObjectSpace.CreateObject<AppMenu>();
            //    appPhanQuyen.LaThuMuc = false;
            //    appPhanQuyen.TenChucNang = "Phân quyền chức năng";
            //    appPhanQuyen.PhanHe = PhanHeEnum.HeThong;
            //    appPhanQuyen.SuDung = true;
            //    appPhanQuyen.HinhAnh = "BO_Contact";
            //    appPhanQuyen.LoaiView = 0;
            //    appPhanQuyen.LoaiCustom = 0;
            //    appPhanQuyen.ThuMucQuanLy = appHeThong;
            //    appPhanQuyen.AppObject = appObject_SecuritySystemRole;
            //    appPhanQuyen.Save();
            //}
            #endregion

            #region Cấu hình chung
            //CauHinhChung cauHinh = ObjectSpace.FindObject<CauHinhChung>(CriteriaOperator.Parse("ThongTinTruong.TenBoPhan like ?", "%trường đại học%"));
            //if (cauHinh != null)
            //{
            //    cauHinh.CauHinhQuyetDinh = ObjectSpace.CreateObject<CauHinhQuyetDinh>();
            //    cauHinh.Save();
            //}


            //CauHinhChung cauHinh = ObjectSpace.FindObject<CauHinhChung>(CriteriaOperator.Parse("ThongTinTruong.TenBoPhan like ?", "%trường đại học%"));
            //if (cauHinh != null)
            //{
            //    cauHinh.CauHinhHopDong = ObjectSpace.CreateObject<CauHinhHopDong>();
            //    cauHinh.CauHinhHoSo = ObjectSpace.CreateObject<CauHinhHoSo>();
            //    cauHinh.CauHinhTuyenDung = ObjectSpace.CreateObject<CauHinhTuyenDung>();
            //    cauHinh.Save();
            //}
            #endregion

            #region Import
            //ImportHoSo();
            //ImportHeSo();
            //ImportHoSoMoi();
            //ImportThamNien();
            //ImportTaiKhoanNganHang();
            //ImportXang();
            //ImportTNCT();
            //Criteria();
            //CreateXml();
            //InsertThinhGiang();
            #endregion

            //CloneAllReports("LUH");
            
        }

        private void CloneAllReports(string maTruongGoc)
        {
            XPCollection<HRMReport> list = new XPCollection<HRMReport>(((XPObjectSpace)ObjectSpace).Session, CriteriaOperator.Parse("MaTruong like ?", maTruongGoc));
            foreach (HRMReport item in list)
            {
                HRMReport newItem = HamDungChung.Copy<HRMReport>(((XPObjectSpace)ObjectSpace).Session, item);
                newItem.MaTruong = TruongConfig.MaTruong;
                newItem.Save();
            }
        }

        private void InsertThinhGiang()
        {
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\luhgvtg.xls", "[Sheet1$]"))
            {
                Guid oid;
                foreach (DataRow dr in dt.Rows)
                {
                    GiangVienThinhGiang gv = ObjectSpace.CreateObject<GiangVienThinhGiang>();
                    gv.MaQuanLy = dr[0].ToString();
                    gv.Ho = dr[1].ToString().Trim() + ((!dr.IsNull(2) && dr[2].ToString().Trim().Length > 0) ? " " + dr[2].ToString().Trim() : "");
                    gv.Ten = dr[3].ToString().Trim();
                    if (!dr.IsNull(4)
                        && Guid.TryParse(dr[4].ToString().Trim(), out oid))
                        gv.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(oid);
                    if (!dr.IsNull(5)
                        && Guid.TryParse(dr[5].ToString().Trim(), out oid))
                        gv.NhanVienTrinhDo.HocHam = ObjectSpace.GetObjectByKey<HocHam>(oid);
                    if (!dr.IsNull(6)
                        && Guid.TryParse(dr[6].ToString().Trim(), out oid))
                        gv.NhanVienTrinhDo.TrinhDoChuyenMon = ObjectSpace.GetObjectByKey<TrinhDoChuyenMon>(oid);
                    if (!dr.IsNull(7)
                        && Guid.TryParse(dr[7].ToString().Trim(), out oid))
                        gv.TinhTrang = ObjectSpace.GetObjectByKey<TinhTrang>(oid);

                    gv.Save();
                }
            }
        }

        private void CreateXml()
        {
            const string query = @"select top 20 Ho, Ten, GioiTinh, NgaySinh, DanToc, TonGiao, ChucVu, BoPhan, HocHam, TrinhDoChuyenMon as HocVi,
                                    ChuyenMonDaoTao, NamTotNghiep,
                                    case when exists(select * from dbo.DoanVien where ThongTinNhanVien = a.Oid) then 1 else 0 end as DoanVien,
                                    case when exists(select * from dbo.DangVien where ThongTinNhanVien = a.Oid) then 1 else 0 end as DangVien
                                    from dbo.ThongTinNhanVien a
                                    inner join dbo.NhanVien b on a.Oid = b.Oid
                                    inner join dbo.HoSo c on b.Oid = c.Oid
                                    inner join dbo.NhanVienTrinhDo d on b.NhanVienTrinhDo = d.Oid
                                    inner join dbo.ChuyenMonDaoTao e on d.ChuyenMonDaoTao = e.Oid
                                    inner join dbo.LoaiNhanSu f on a.LoaiNhanSu = f.Oid
                                    where c.GCRecord is null
                                    and f.TenLoaiNhanSu like N'%giảng viên%'
                                    and NamTotNghiep > 0";
            const string query1 = @"select Oid, MaQuanLy, TenDanToc
                                    from dbo.DanToc
                                    where GCRecord is null
                                    order by TenDanToc";
            const string query2 = @"select Oid, MaQuanLy, TenTonGiao
                                    from dbo.TonGiao
                                    where GCRecord is null
                                    order by TenTonGiao";
            const string query3 = @"select Oid, MaQuanLy, TenChucVu
                                    from dbo.ChucVu
                                    where GCRecord is null
                                    and PhanLoai = 0
                                    order by TenChucVu";
            const string query4 = @"select Oid, MaQuanLy, TenBoPhan as TenDonVi, BoPhanCha as TrucThuoc
                                    from dbo.BoPhan
                                    where GCRecord is null
                                    order by MaQuanLy";
            const string query5 = @"select Oid, MaQuanLy, TenHocHam
                                    from dbo.HocHam
                                    where GCRecord is null
                                    order by TenHocHam";
            const string query6 = @"select Oid, MaQuanLy, TenTrinhDoChuyenMon as TenHocVi
                                    from dbo.TrinhDoChuyenMon
                                    where GCRecord is null
                                    order by TenTrinhDoChuyenMon";
            const string query7 = @"select top 20 Oid, MaQuanLy, TenChuyenMonDaoTao as TenNganhDaoTao
                                    from dbo.ChuyenMonDaoTao
                                    where GCRecord is null
                                    order by TenChuyenMonDaoTao";
            
            using (DataSet ds = new DataSet("PSC_HRM"))
            {
                DataTable dt = DataProvider.GetDataTable(query, CommandType.Text);
                dt.TableName = "HoSo";

                DataTable dt1 = DataProvider.GetDataTable(query1, CommandType.Text);
                dt1.TableName = "DanToc";

                DataTable dt2 = DataProvider.GetDataTable(query2, CommandType.Text);
                dt2.TableName = "TonGiao";

                DataTable dt3 = DataProvider.GetDataTable(query3, CommandType.Text);
                dt3.TableName = "ChucVu";

                DataTable dt4 = DataProvider.GetDataTable(query4, CommandType.Text);
                dt4.TableName = "DonVi";

                DataTable dt5 = DataProvider.GetDataTable(query5, CommandType.Text);
                dt5.TableName = "HocHam";

                DataTable dt6 = DataProvider.GetDataTable(query6, CommandType.Text);
                dt6.TableName = "HocVi";

                DataTable dt7 = DataProvider.GetDataTable(query7, CommandType.Text);
                dt7.TableName = "NganhDaoTao";


                ds.Tables.Add(dt);
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                ds.Tables.Add(dt3);
                ds.Tables.Add(dt4);
                ds.Tables.Add(dt5);
                ds.Tables.Add(dt6);
                ds.Tables.Add(dt7);

                ds.WriteXml(@"E:\PSC_HRM_Giang_Day.xml", XmlWriteMode.WriteSchema);
            }
        }

        private void Criteria()
        {
            string criteria = "[NhanVien.TinhTrang] = ##XpoObject#PSC_HRM.Module.DanhMuc.TinhTrang({91525498-318c-4bcb-8255-5d063054aa70})# And [HoSo.HoTen] Not Like '%Vũ Duy Cương%' And [HoSo.HoTen] Not Like '%Cao Văn Hào%' And ([NhanVienThongTinLuong.NgachLuong.MaQuanLy] Not Like '15%' Or [NhanVien.BoPhan.LoaiBoPhan] = ##Enum#PSC_HRM.Module.LoaiBoPhanEnum,PhongBan#) Or [NhanVien.TinhTrang] = ##XpoObject#PSC_HRM.Module.DanhMuc.TinhTrang({04e80322-5fce-4546-a916-d7ebd4770c06})# And [HoSo.HoTen] Not Like '%Vũ Duy Cương%' And [HoSo.HoTen] Not Like '%Cao Văn Hào%' And ([NhanVienThongTinLuong.NgachLuong.MaQuanLy] Not Like '15%' Or [NhanVien.BoPhan.LoaiBoPhan] = ##Enum#PSC_HRM.Module.LoaiBoPhanEnum,PhongBan#) Or [NhanVien.TinhTrang] = ##XpoObject#PSC_HRM.Module.DanhMuc.TinhTrang({d74dc744-a30c-448c-b82d-7adc547718a2})# And [HoSo.HoTen] Not Like '%Vũ Duy Cương%' And [HoSo.HoTen] Not Like '%Cao Văn Hào%' And ([NhanVienThongTinLuong.NgachLuong.MaQuanLy] Not Like '15%' Or [NhanVien.BoPhan.LoaiBoPhan] = ##Enum#PSC_HRM.Module.LoaiBoPhanEnum,PhongBan#)";

            CriteriaOperator co = CriteriaEditorHelper.GetCriteriaOperator(criteria, typeof(TapDieuKien.DieuKienTongHop), ObjectSpace);
            GroupOperator go = co as GroupOperator;

            foreach (var item in go.Operands)
            {
                Debug.WriteLine(item.LegacyToString());
            }
        }


        private void ImportKhauTruThueTNCNDot4()
        {
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\Luong_7_2014_LUH.xls", "[SheetThueTNCNDot4$]"))
            {
                ThongTinTinhLuong nhanVien = null;
                StringBuilder sb = new StringBuilder();
                int temp;

                foreach (DataRow item in dt.Rows)
                {
                    nhanVien = ObjectSpace.FindObject<ThongTinTinhLuong>(CriteriaOperator.Parse("ThongTinNhanVien.HoTen like ?", item[0].ToString().Trim()));
                    if (nhanVien != null
                        && int.TryParse(item[2].ToString(), out temp))
                    {
                        nhanVien.PhuCapTienXang = temp;

                        nhanVien.Save();
                    }
                    else
                    {
                        sb.AppendLine(String.Format("{0} {1}", item[0], item[2]));
                    }
                }
                if (sb.Length > 0)
                    HamDungChung.WriteLog(@"E:\Import\luhThueTNCNDot4.txt", sb.ToString());
            }
        }

        private void ImportXang()
        {
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\Luong_7_2014_LUH.xls", "[SheetThueTNCNDot4$]"))
            {
                ThongTinTinhLuong nhanVien = null;
                StringBuilder sb = new StringBuilder();
                int temp;
                foreach (DataRow item in dt.Rows)
                {
                    nhanVien = ObjectSpace.FindObject<ThongTinTinhLuong>(CriteriaOperator.Parse("ThongTinNhanVien.HoTen like ?", item[0].ToString().Trim()));
                    if (nhanVien != null
                        && int.TryParse(item[2].ToString(), out temp))
                    {
                        nhanVien.PhuCapTienXang = temp;

                        nhanVien.Save();
                    }
                    else
                    {
                        sb.AppendLine(String.Format("{0} {1}", item[0], item[2]));
                    }
                }
                if (sb.Length > 0)
                    HamDungChung.WriteLog(@"E:\Import\luhxang.txt", sb.ToString());
            }
        }

        private void ImportTNCT()
        {
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\luhtnct.xls", "[Sheet1$]"))
            {
                ThongTinTinhLuong nhanVien = null;
                StringBuilder sb = new StringBuilder();
                decimal temp;
                foreach (DataRow item in dt.Rows)
                {
                    nhanVien = ObjectSpace.FindObject<ThongTinTinhLuong>(CriteriaOperator.Parse("ThongTinNhanVien.HoTen like ?", item[0].ToString().Trim()));
                    if (nhanVien != null
                        && decimal.TryParse(item[2].ToString(), out temp))
                    {
                        nhanVien.ThamNienCongTac = temp;

                        nhanVien.Save();
                    }
                    else
                    {
                        sb.AppendLine(String.Format("{0} {1}", item[0], item[2]));
                    }
                }
                if (sb.Length > 0)
                    HamDungChung.WriteLog(@"E:\Import\luhtnct.txt", sb.ToString());
            }
        }

        private void ImportTaiKhoanNganHang()
        {
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\taikhoanluh.xls", "[Sheet1$]"))
            {
                NganHang nganHang = ObjectSpace.FindObject<NganHang>(CriteriaOperator.Parse("MaQuanLy like ?", "%Mạc Thị Bưởi%"));

                ThongTinNhanVien nhanVien = null;
                TaiKhoanNganHang taiKhoan;
                StringBuilder sb = new StringBuilder();
                foreach (DataRow item in dt.Rows)
                {
                    nhanVien = ObjectSpace.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("HoTen like ?", item[0].ToString().Trim()));
                    if (nhanVien != null)
                    {
                        taiKhoan = ObjectSpace.CreateObject<TaiKhoanNganHang>();
                        taiKhoan.NganHang = nganHang;
                        taiKhoan.SoTaiKhoan = item[1].ToString().Trim();
                        taiKhoan.TaiKhoanChinh = true;

                        nhanVien.ListTaiKhoanNganHang.Add(taiKhoan);

                        nhanVien.Save();
                    }
                    else
                    {
                        sb.AppendLine(String.Format("{0} {1}", item[0], item[1]));
                    }
                }
                if (sb.Length > 0)
                    HamDungChung.WriteLog(@"E:\Import\taikhoanluh.txt", sb.ToString());
            }
        }

        private void ImportThamNien()
        {
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\luhthamnien.xls", "[Sheet1$]"))
            {
                int iTemp;
                DateTime dtTemp;
                ThongTinNhanVien nhanVien = null;
                StringBuilder sb = new StringBuilder();
                foreach (DataRow item in dt.Rows)
                {
                    nhanVien = ObjectSpace.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Ho like ? and Ten like ?", item[1].ToString().Trim(), item[2].ToString().Trim()));
                    if (nhanVien != null)
                    {
                        if (DateTime.TryParse(item[11].ToString().Trim(), out dtTemp)
                            && dtTemp < new DateTime(2014,4,30))
                        {
                            if (int.TryParse(item[10].ToString().Trim(), out iTemp))
                            {
                                if (iTemp > 0)
                                {
                                    nhanVien.NhanVienThongTinLuong.ThamNien = iTemp;
                                    nhanVien.NhanVienThongTinLuong.NgayHuongThamNien = dtTemp;
                                }
                                else
                                {
                                    if (DateTime.TryParse(item[7].ToString().Trim(), out dtTemp))
                                        nhanVien.NgayTinhThamNienNhaGiao = dtTemp;
                                }
                                nhanVien.NhanVienThongTinLuong.Save();
                                nhanVien.Save();
                            }
                        }
                        else
                        {
                            if (int.TryParse(item[8].ToString().Trim(), out iTemp))
                            {
                                if (iTemp > 0)
                                {
                                    nhanVien.NhanVienThongTinLuong.ThamNien = iTemp;
                                    if (DateTime.TryParse(item[9].ToString().Trim(), out dtTemp))
                                        nhanVien.NhanVienThongTinLuong.NgayHuongThamNien = dtTemp;
                                }
                                else
                                {
                                    if (DateTime.TryParse(item[7].ToString().Trim(), out dtTemp))
                                        nhanVien.NgayTinhThamNienNhaGiao = dtTemp;
                                }
                                nhanVien.NhanVienThongTinLuong.Save();
                                nhanVien.Save();
                            }
                        }
                    }
                    else
                    {
                        sb.AppendLine(String.Format("{0} {1} {2} {3} {4} {5}", item[1], item[2], item[8], item[9], item[10], item[11]));
                    }
                }
                if (sb.Length > 0)
                    HamDungChung.WriteLog(@"E:\Import\luhthamnien.txt", sb.ToString());
            }
        }

        private void ImportHoSoMoi()
        {
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\luh.xls", "[Sheet1$]"))
            {
                string hoTen = "";
                string sTemp, sTemp1;
                int iTemp;
                decimal deTemp;
                ThongTinNhanVien nhanVien = null;
                TinhTrang dlv = ObjectSpace.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
                BoPhan bp = ObjectSpace.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan like ?", "%Tổ chức%"));

                foreach (DataRow item in dt.Rows)
                {
                    sTemp = item[0].ToString().Trim();
                    sTemp1 = item[1].ToString().Trim();
                    hoTen = String.Format("{0} {1}", sTemp, sTemp1);
                    nhanVien = ObjectSpace.CreateObject<ThongTinNhanVien>();
                    nhanVien.Ho = sTemp;
                    nhanVien.Ten = sTemp1;
                    nhanVien.BoPhan = bp;

                    //3. tinh trang
                    nhanVien.TinhTrang = dlv;

                    //4. hsl
                    sTemp = item[4].ToString().Trim();
                    if (decimal.TryParse(sTemp, out deTemp))
                        nhanVien.NhanVienThongTinLuong.HeSoLuong = deTemp;
                    else
                        nhanVien.NhanVienThongTinLuong.HeSoLuong = 0;

                    //5. pccv
                    sTemp = item[5].ToString().Trim();
                    if (decimal.TryParse(sTemp, out deTemp))
                        nhanVien.NhanVienThongTinLuong.HSPCChucVu = deTemp;
                    else
                        nhanVien.NhanVienThongTinLuong.HSPCChucVu = 0;

                    //9. pcvk
                    sTemp = item[9].ToString().Trim();
                    if (int.TryParse(sTemp, out iTemp))
                        nhanVien.NhanVienThongTinLuong.VuotKhung = iTemp;
                    else
                        nhanVien.NhanVienThongTinLuong.VuotKhung = 0;

                    //6. pcud
                    sTemp = item[6].ToString().Trim();
                    if (int.TryParse(sTemp, out iTemp))
                        nhanVien.NhanVienThongTinLuong.PhuCapUuDai = iTemp;
                    else
                        nhanVien.NhanVienThongTinLuong.PhuCapUuDai = 0;

                    //7. pc doc hai
                    sTemp = item[7].ToString().Trim();
                    if (decimal.TryParse(sTemp, out deTemp))
                        nhanVien.NhanVienThongTinLuong.HSPCDocHai = deTemp;
                    else
                        nhanVien.NhanVienThongTinLuong.HSPCDocHai = 0;

                    //8. pc trach nhiem
                    sTemp = item[8].ToString().Trim();
                    if (decimal.TryParse(sTemp, out deTemp))
                        nhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = deTemp;
                    else
                        nhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = 0;

                    //10. pctn
                    sTemp = item[10].ToString().Trim();
                    if (int.TryParse(sTemp, out iTemp))
                        nhanVien.NhanVienThongTinLuong.ThamNien = iTemp;
                    else
                        nhanVien.NhanVienThongTinLuong.ThamNien = 0;

                    //11. huong 85% luong
                    sTemp = item[11].ToString().Trim();
                    nhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = sTemp == "1" ? false : true;

                    //12. luong khoan
                    sTemp = item[12].ToString().Trim();
                    if (decimal.TryParse(sTemp, out deTemp))
                    {
                        nhanVien.NhanVienThongTinLuong.LuongKhoan = deTemp;
                        nhanVien.NhanVienThongTinLuong.PhanLoai = ThongTinLuongEnum.LuongKhoanCoBHXH;
                    }
                    else
                        nhanVien.NhanVienThongTinLuong.LuongKhoan = 0;

                    //13. pccm
                    sTemp = item[13].ToString().Trim();
                    if (decimal.TryParse(sTemp, out deTemp))
                        nhanVien.NhanVienThongTinLuong.HSPCChuyenMon = deTemp;
                    else
                        nhanVien.NhanVienThongTinLuong.HSPCChuyenMon = 0;

                    //17. pc tang them
                    sTemp = item[17].ToString().Trim();
                    if (decimal.TryParse(sTemp, out deTemp))
                        nhanVien.NhanVienThongTinLuong.PhuCapTangThem = deTemp;
                    else
                        nhanVien.NhanVienThongTinLuong.PhuCapTangThem = 0;

                    //18. gtgc
                    sTemp = item[18].ToString().Trim();
                    if (int.TryParse(sTemp, out iTemp))
                    {
                        nhanVien.NhanVienThongTinLuong.SoNguoiPhuThuoc = iTemp;
                        nhanVien.NhanVienThongTinLuong.SoThangGiamTru = iTemp * 12;
                    }
                    else
                        nhanVien.NhanVienThongTinLuong.SoNguoiPhuThuoc = 0;

                    nhanVien.NhanVienThongTinLuong.Save();
                    nhanVien.Save();
                    //save
                    ObjectSpace.CommitChanges();
                }
            }
        }

        private void ImportHeSo()
        {
            using (DataTable dt = DataProvider.GetDataTable(@"E:\Import\He_So_Luat.xls", "[Sheet1$]"))
            {
                string hoTen = "";
                string sTemp, sTemp1;
                int iTemp;

                ThongTinNhanVien nhanVien = null;
                StringBuilder sb = new StringBuilder();
                TinhTrang dlv = ObjectSpace.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
                TinhTrang nts = ObjectSpace.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Nghỉ BHXH"));
                TinhTrang dh = ObjectSpace.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đi học ngoài nước có hưởng lương"));
                foreach (DataRow item in dt.Rows)
                {
                    sTemp = item[0].ToString().Trim();
                    sTemp1 = item[1].ToString().Trim();
                    hoTen = String.Format("{0} {1}", sTemp, sTemp1);
                    nhanVien = ObjectSpace.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("HoTen like ?", hoTen));

                    if (nhanVien != null)
                    {
                        ////3. tinh trang
                        //sTemp = item[3].ToString().Trim();
                        //if (sTemp == "1")
                        //    nhanVien.TinhTrang = dlv;
                        //else if (sTemp == "4")
                        //    nhanVien.TinhTrang = nts;
                        //else
                        //    nhanVien.TinhTrang = dh;

                        ////4. hsl
                        //sTemp = item[4].ToString().Trim();
                        //if (decimal.TryParse(sTemp, out deTemp))
                        //    nhanVien.NhanVienThongTinLuong.HeSoLuong = deTemp;
                        //else
                        //    nhanVien.NhanVienThongTinLuong.HeSoLuong = 0;

                        ////5. pccv
                        //sTemp = item[5].ToString().Trim();
                        //if (decimal.TryParse(sTemp, out deTemp))
                        //    nhanVien.NhanVienThongTinLuong.HSPCChucVu = deTemp;
                        //else
                        //    nhanVien.NhanVienThongTinLuong.HSPCChucVu = 0;

                        ////9. pcvk
                        //sTemp = item[9].ToString().Trim();
                        //if (int.TryParse(sTemp, out iTemp))
                        //    nhanVien.NhanVienThongTinLuong.VuotKhung = iTemp;
                        //else
                        //    nhanVien.NhanVienThongTinLuong.VuotKhung = 0;

                        //6. pcud
                        sTemp = item[6].ToString().Trim();
                        if (int.TryParse(sTemp, out iTemp))
                            nhanVien.NhanVienThongTinLuong.PhuCapUuDai = iTemp;
                        else
                            nhanVien.NhanVienThongTinLuong.PhuCapUuDai = 0;

                        ////7. pc doc hai
                        //sTemp = item[7].ToString().Trim();
                        //if (decimal.TryParse(sTemp, out deTemp))
                        //    nhanVien.NhanVienThongTinLuong.HSPCDocHai = deTemp;
                        //else
                        //    nhanVien.NhanVienThongTinLuong.HSPCDocHai = 0;

                        ////8. pc trach nhiem
                        //sTemp = item[8].ToString().Trim();
                        //if (decimal.TryParse(sTemp, out deTemp))
                        //    nhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = deTemp;
                        //else
                        //    nhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = 0;

                        ////9. pcvk
                        //sTemp = item[9].ToString().Trim();
                        //if (int.TryParse(sTemp, out iTemp))
                        //    nhanVien.NhanVienThongTinLuong.VuotKhung = iTemp;
                        //else
                        //    nhanVien.NhanVienThongTinLuong.VuotKhung = 0;

                        ////10. pctn
                        //sTemp = item[10].ToString().Trim();
                        //if (int.TryParse(sTemp, out iTemp))
                        //    nhanVien.NhanVienThongTinLuong.ThamNien = iTemp;
                        //else
                        //    nhanVien.NhanVienThongTinLuong.ThamNien = 0;

                        ////11. huong 85% luong
                        //sTemp = item[11].ToString().Trim();
                        //nhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = sTemp == "1" ? false : true;

                        ////12. luong khoan
                        //sTemp = item[12].ToString().Trim();
                        //if (decimal.TryParse(sTemp, out deTemp))
                        //{
                        //    nhanVien.NhanVienThongTinLuong.LuongKhoan = deTemp;
                        //    nhanVien.NhanVienThongTinLuong.PhanLoai = ThongTinLuongEnum.LuongKhoanCoBHXH;
                        //}
                        //else
                        //    nhanVien.NhanVienThongTinLuong.LuongKhoan = 0;

                        ////13. pccm
                        //sTemp = item[13].ToString().Trim();
                        //if (decimal.TryParse(sTemp, out deTemp))
                        //    nhanVien.NhanVienThongTinLuong.HSPCChuyenMon = deTemp;
                        //else
                        //    nhanVien.NhanVienThongTinLuong.HSPCChuyenMon = 0;

                        ////14. pcql
                        //sTemp = item[14].ToString().Trim();
                        //if (decimal.TryParse(sTemp, out deTemp))
                        //    nhanVien.NhanVienThongTinLuong.HSPCChucVu1 = deTemp;
                        //else
                        //    nhanVien.NhanVienThongTinLuong.HSPCChucVu1 = 0;

                        ////15. pckn1
                        //sTemp = item[15].ToString().Trim();
                        //if (decimal.TryParse(sTemp, out deTemp))
                        //    nhanVien.NhanVienThongTinLuong.HSPCChucVu2 = deTemp;
                        //else
                        //    nhanVien.NhanVienThongTinLuong.HSPCChucVu2 = 0;

                        ////16. pckn2
                        //sTemp = item[16].ToString().Trim();
                        //if (decimal.TryParse(sTemp, out deTemp))
                        //    nhanVien.NhanVienThongTinLuong.HSPCChucVu3 = deTemp;
                        //else
                        //    nhanVien.NhanVienThongTinLuong.HSPCChucVu3 = 0;

                        ////17. pc tang them
                        //sTemp = item[17].ToString().Trim();
                        //if (decimal.TryParse(sTemp, out deTemp))
                        //    nhanVien.NhanVienThongTinLuong.PhuCapTangThem = deTemp;
                        //else
                        //    nhanVien.NhanVienThongTinLuong.PhuCapTangThem = 0;

                        ////18. gtgc
                        //sTemp = item[18].ToString().Trim();
                        //if (int.TryParse(sTemp, out iTemp))
                        //{
                        //    nhanVien.NhanVienThongTinLuong.SoNguoiPhuThuoc = iTemp;
                        //    nhanVien.NhanVienThongTinLuong.SoThangGiamTru = iTemp * 12;
                        //}
                        //else
                        //    nhanVien.NhanVienThongTinLuong.SoNguoiPhuThuoc = 0;

                        //save

                        nhanVien.NhanVienThongTinLuong.Save();
                        nhanVien.Save();
                        ObjectSpace.CommitChanges();
                    }
                    else
                        sb.AppendLine(hoTen);
                }
                HamDungChung.WriteLog(@"D:\luhlog.txt", sb.ToString());
            }
        }

        private void ImportHoSo()
        {
            using (DataTable dt = DataProvider.GetDataTable(@"D:\import.xls", "[Sheet1$]"))
            {
                int maQuanLy;
                TinhTrang dangLamViec = ObjectSpace.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
                TinhTrang nghiViec = ObjectSpace.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Nghỉ việc"));
                TinhTrang nghiHuu = ObjectSpace.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Nghỉ hưu"));

                foreach (DataRow item in dt.Rows)
                {
                    if (int.TryParse(item[0].ToString().Trim(), out maQuanLy))
                    {
                        ThongTinNhanVien obj = ObjectSpace.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy like ?", maQuanLy.ToString("000#")));
                        if (obj == null)
                        {
                            obj = ObjectSpace.CreateObject<ThongTinNhanVien>();
                            obj.MaQuanLy = maQuanLy.ToString("000#");
                            obj.Ho = item[1].ToString().Trim();
                            obj.Ten = item[2].ToString().Trim();
                            obj.TinhTrang = dangLamViec;

                            if (item[3].ToString().Trim().ToLower().Contains("biên"))
                                obj.BienChe = true;
                            else if (item[3].ToString().Trim().ToLower().Contains("việc"))
                                obj.TinhTrang = nghiViec;
                            else if (item[3].ToString().Trim().ToLower().Contains("hưu"))
                                obj.TinhTrang = nghiHuu;

                            if (item[4].ToString().Trim().ToLower().Contains("nam"))
                                obj.GioiTinh = GioiTinhEnum.Nam;
                            else
                                obj.GioiTinh = GioiTinhEnum.Nu;

                            if (!string.IsNullOrWhiteSpace(item[9].ToString().Trim()))
                                obj.BoPhan = ObjectSpace.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan like ?", item[9].ToString().Trim()));
                            else
                                obj.BoPhan = ObjectSpace.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan like ?", item[5].ToString().Trim()));
                            if (obj.BoPhan == null)
                                obj.BoPhan = ObjectSpace.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy like ?", "IUH"));

                            obj.ChucVu = ObjectSpace.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", item[6].ToString().Trim()));
                            DateTime dtTemp;
                            if (DateTime.TryParse(item[7].ToString().Trim(), out dtTemp))
                                obj.NgayBoNhiem = dtTemp;
                            obj.ChucVuKiemNhiem = ObjectSpace.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", item[8].ToString().Trim()));

                            //10
                            obj.NhanVienTrinhDo.HocHam = ObjectSpace.FindObject<HocHam>(CriteriaOperator.Parse("TenHocHam like ?", item[10].ToString().Trim()));

                            //11
                            int iTemp;
                            if (int.TryParse(item[11].ToString().Trim(), out iTemp))
                                obj.NhanVienTrinhDo.NamCongNhanHocHam = iTemp;

                            //12, 13: trình đồ
                            string sTemp1 = item[12].ToString().Trim().Replace("-", ";");
                            string sTemp2 = item[13].ToString().Trim().Replace("-", ";");
                            if (!string.IsNullOrWhiteSpace(sTemp1)
                                && !string.IsNullOrWhiteSpace(sTemp2))
                            {
                                VanBang vanBang;
                                TrinhDoChuyenMon trinhDo;
                                ChuyenMonDaoTao chuyenMon;
                                if (sTemp1.Contains(';')
                                    && sTemp2.Contains(';'))
                                {
                                    string[] split1 = sTemp1.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                    string[] split2 = sTemp2.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (split1.Length <= split2.Length)
                                    {
                                        int length = split1.Length;
                                        for (int i = 0; i < length; i++)
                                        {
                                            trinhDo = ObjectSpace.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon like ?", split1[i].Trim()));
                                            chuyenMon = ObjectSpace.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao like ?", String.Format("%{0}%", split2[i].Trim())));
                                            if (trinhDo != null)
                                            {
                                                vanBang = ObjectSpace.CreateObject<VanBang>();
                                                vanBang.TrinhDoChuyenMon = trinhDo;
                                                vanBang.ChuyenMonDaoTao = chuyenMon;
                                                obj.ListVanBang.Add(vanBang);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int length = split2.Length;
                                        for (int i = 0; i < length; i++)
                                        {
                                            trinhDo = ObjectSpace.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon like ?", split1[i].Trim()));
                                            chuyenMon = ObjectSpace.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao like ?", String.Format("%{0}%", split2[i].Trim())));
                                            if (trinhDo != null)
                                            {
                                                vanBang = ObjectSpace.CreateObject<VanBang>();
                                                vanBang.TrinhDoChuyenMon = trinhDo;
                                                vanBang.ChuyenMonDaoTao = chuyenMon;
                                                obj.ListVanBang.Add(vanBang);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    trinhDo = ObjectSpace.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon like ?", sTemp1));
                                    chuyenMon = ObjectSpace.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao like ?", String.Format("%{0}%", sTemp2)));
                                    if (trinhDo != null)
                                    {
                                        vanBang = ObjectSpace.CreateObject<VanBang>();
                                        vanBang.TrinhDoChuyenMon = trinhDo;
                                        vanBang.ChuyenMonDaoTao = chuyenMon;
                                        obj.ListVanBang.Add(vanBang);
                                    }
                                }
                            }

                            //14
                            sTemp1 = item[14].ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                ChungChi chungChi = ObjectSpace.CreateObject<ChungChi>();
                                chungChi.TenChungChi = sTemp1;
                                chungChi.LoaiChungChi = ObjectSpace.FindObject<LoaiChungChi>(CriteriaOperator.Parse("TenChungChi like ?", "Chứng chỉ bồi dưỡng"));
                                obj.ListChungChi.Add(chungChi);
                            }

                            sTemp1 = item[15].ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                                obj.NhanVienTrinhDo.ChuongTrinhHoc = ObjectSpace.FindObject<ChuongTrinhHoc>(CriteriaOperator.Parse("TenChuongTrinhHoc like ?", sTemp1));

                            if (DateTime.TryParse(item[16].ToString().Trim(), out dtTemp))
                                obj.NgaySinh = dtTemp;

                            sTemp1 = item[17].ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                DiaChi dc = ObjectSpace.CreateObject<DiaChi>();
                                TinhThanh tinh = ObjectSpace.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ?", sTemp1));
                                if (tinh != null)
                                    dc.TinhThanh = tinh;
                                else
                                    dc.SoNha = sTemp1;

                                obj.NoiSinh = dc;
                            }

                            if (DateTime.TryParse(item[18].ToString().Trim(), out dtTemp))
                                obj.NgayVaoCoQuan = dtTemp;

                            decimal deTemp;
                            if (decimal.TryParse(item[19].ToString().Trim(), out deTemp))
                                obj.NhanVienThongTinLuong.HSPCChucVu = deTemp;

                            obj.NhanVienThongTinLuong.NgachLuong = ObjectSpace.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[20].ToString().Trim()));
                            if (obj.NhanVienThongTinLuong.NgachLuong != null)
                                obj.NhanVienThongTinLuong.BacLuong = ObjectSpace.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong=? and MaQuanLy like ? and !BacLuongCu", obj.NhanVienThongTinLuong.NgachLuong.Oid, item[21].ToString().Trim()));

                            sTemp1 = item[22].ToString().Replace("%", "").Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1)
                                && int.TryParse(sTemp1, out iTemp))
                                obj.NhanVienThongTinLuong.VuotKhung = iTemp;

                            sTemp1 = item[23].ToString().Replace("%", "").Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1)
                                && int.TryParse(sTemp1, out iTemp))
                                obj.NhanVienThongTinLuong.PhuCapUuDai = iTemp;

                            if (DateTime.TryParse(item[24].ToString().Trim(), out dtTemp))
                                obj.NhanVienThongTinLuong.NgayHuongLuong = dtTemp;

                            if (DateTime.TryParse(item[25].ToString().Trim(), out dtTemp))
                                obj.NhanVienThongTinLuong.MocNangLuong = dtTemp;

                            if (DateTime.TryParse(item[26].ToString().Trim(), out dtTemp))
                                obj.NhanVienThongTinLuong.MocNangLuongDieuChinh = dtTemp;
                            obj.NhanVienThongTinLuong.LyDoDieuChinh = item[27].ToString().Trim();

                            obj.CMND = item[28].ToString().Trim();
                            if (DateTime.TryParse(item[29].ToString().Trim(), out dtTemp))
                                obj.NgayCap = dtTemp;

                            sTemp1 = item[30].ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                obj.NoiCap = ObjectSpace.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ?", sTemp1));
                            }

                            sTemp1 = item[31].ToString().Trim();
                            QuanHeGiaDinh nguoiThan;
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                string[] split = sTemp1.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                nguoiThan = ObjectSpace.CreateObject<QuanHeGiaDinh>();
                                nguoiThan.ThongTinNhanVien = obj;
                                nguoiThan.HoTenNguoiThan = split[0];
                                if (split.Length == 2)
                                    nguoiThan.TinhTrang = TinhTrangEnum.DaMat;
                                nguoiThan.QuanHe = ObjectSpace.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Mẹ đẻ"));
                                if (int.TryParse(item[32].ToString().Trim(), out iTemp))
                                    nguoiThan.NgaySinh = iTemp;
                            }

                            sTemp1 = item[33].ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                string[] split = sTemp1.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                nguoiThan = ObjectSpace.CreateObject<QuanHeGiaDinh>();
                                nguoiThan.ThongTinNhanVien = obj;
                                nguoiThan.HoTenNguoiThan = split[0];
                                if (split.Length == 2)
                                    nguoiThan.TinhTrang = TinhTrangEnum.DaMat;
                                nguoiThan.QuanHe = ObjectSpace.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Cha đẻ"));
                                if (int.TryParse(item[34].ToString().Trim(), out iTemp))
                                    nguoiThan.NgaySinh = iTemp;
                            }

                            sTemp1 = item[35].ToString().Replace("-", ",");
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                string[] split = sTemp1.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] split1;
                                foreach (string sitem in split)
                                {
                                    split1 = sitem.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                    nguoiThan = ObjectSpace.CreateObject<QuanHeGiaDinh>();
                                    nguoiThan.ThongTinNhanVien = obj;
                                    nguoiThan.QuanHe = ObjectSpace.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Em ruột"));
                                    nguoiThan.HoTenNguoiThan = split1[0].Trim();
                                    if (split1.Length >= 2)
                                    {
                                        if (split1[1].Contains("chết"))
                                            nguoiThan.TinhTrang = TinhTrangEnum.DaMat;
                                        else if (int.TryParse(split1[1].Trim(), out iTemp))
                                            nguoiThan.NgaySinh = iTemp;
                                    }
                                }
                            }

                            sTemp1 = item[36].ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                nguoiThan = ObjectSpace.CreateObject<QuanHeGiaDinh>();
                                nguoiThan.ThongTinNhanVien = obj;
                                nguoiThan.HoTenNguoiThan = sTemp1;
                                if (obj.GioiTinh == GioiTinhEnum.Nam)
                                    nguoiThan.QuanHe = ObjectSpace.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Vợ"));
                                else
                                    nguoiThan.QuanHe = ObjectSpace.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Chồng"));
                                if (int.TryParse(item[34].ToString().Trim(), out iTemp))
                                    nguoiThan.NgaySinh = iTemp;
                            }

                            sTemp1 = item[37].ToString().Replace("-", ",");
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                string[] split = sTemp1.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] split1;
                                foreach (string sitem in split)
                                {
                                    split1 = sitem.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                    nguoiThan = ObjectSpace.CreateObject<QuanHeGiaDinh>();
                                    nguoiThan.ThongTinNhanVien = obj;
                                    nguoiThan.QuanHe = ObjectSpace.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Con"));
                                    nguoiThan.HoTenNguoiThan = split1[0].Trim();
                                    if (split1.Length >= 2)
                                    {
                                        if (split1[1].Contains("chết"))
                                            nguoiThan.TinhTrang = TinhTrangEnum.DaMat;
                                        else if (int.TryParse(split1[1].Trim(), out iTemp))
                                            nguoiThan.NgaySinh = iTemp;
                                    }
                                }
                            }

                            sTemp1 = item[38].ToString().Replace("-", ",").Replace(";", ",");
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                string[] split = sTemp1.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                TrinhDoNgoaiNguKhac ngoaiNgu;
                                foreach (string sItem in split)
                                {
                                    string[] split1 = sItem.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                                    ngoaiNgu = ObjectSpace.CreateObject<TrinhDoNgoaiNguKhac>();
                                    ngoaiNgu.NgoaiNgu = ObjectSpace.FindObject<NgoaiNgu>(CriteriaOperator.Parse("TenNgoaiNgu like ?", split1[0].Trim()));
                                    if (split1.Length >= 2)
                                        ngoaiNgu.TrinhDoNgoaiNgu = ObjectSpace.FindObject<TrinhDoNgoaiNgu>(CriteriaOperator.Parse("TenTrinhDoNgoaiNgu like ?", split1[1].Trim()));
                                    if (split1.Length >= 3
                                        && decimal.TryParse(split1[2].Trim(), out deTemp))
                                        ngoaiNgu.Diem = deTemp;
                                    obj.ListNgoaiNgu.Add(ngoaiNgu);
                                }
                            }

                            obj.NhanVienTrinhDo.TrinhDoTinHoc = ObjectSpace.FindObject<TrinhDoTinHoc>(CriteriaOperator.Parse("TenTrinhDoTinHoc like ?", item[39].ToString().Trim()));

                            sTemp1 = item[40].ToString();
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                string[] split = sTemp1.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                QuaTrinhCongTac quaTrinh;
                                foreach (string sItem in split)
                                {
                                    string[] split1 = sItem.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                                    quaTrinh = ObjectSpace.CreateObject<QuaTrinhCongTac>();
                                    quaTrinh.HoSo = obj;
                                    if (split1[0].Contains("nay"))
                                    {
                                        quaTrinh.TuNam = split1[0].Trim().Substring(0, split1[0].IndexOf(' '));
                                        quaTrinh.DenNam = "đến nay";
                                    }
                                    else
                                    {
                                        string[] split2 = split1[0].Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                                        if (split2.Length > 0)
                                            quaTrinh.TuNam = split2[0].Trim();
                                        if (split2.Length > 1)
                                            quaTrinh.DenNam = split2[1].Trim();
                                    }
                                    if (split1.Length > 1)
                                        quaTrinh.NoiDung = split1[1];
                                }
                            }

                            sTemp1 = item[42].ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                DiaChi dc = ObjectSpace.CreateObject<DiaChi>();
                                dc.SoNha = sTemp1;

                                obj.DiaChiThuongTru = dc;
                            }

                            sTemp1 = item[43].ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                DiaChi dc = ObjectSpace.CreateObject<DiaChi>();
                                dc.SoNha = sTemp1;

                                obj.NoiOHienNay = dc;
                            }

                            obj.DienThoaiDiDong = item[44].ToString().Trim();

                            sTemp1 = item[45].ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1)
                                && decimal.TryParse(sTemp1, out deTemp))
                                obj.NhanVienThongTinLuong.HSPCChuyenMon = deTemp;
                            
                            sTemp1 = item[47].ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1))
                            {
                                HoSoBaoHiem hoSo = ObjectSpace.CreateObject<HoSoBaoHiem>();
                                hoSo.ThongTinNhanVien = obj;
                                hoSo.SoSoBHXH = sTemp1;
                            }

                            obj.NhanVienThongTinLuong.MaSoThue = item[48].ToString().Trim();

                            sTemp1 = item[49].ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(sTemp1)
                                && DateTime.TryParse(sTemp1, out dtTemp))
                                obj.NgayNghiHuu = dtTemp;
                        }
                    }
                }

                ObjectSpace.CommitChanges();
            }
        }

        // Override to perform the required changes with the database structure before the database schema is updated (http://documentation.devexpress.com/#Xaf/DevExpressExpressAppUpdatingModuleUpdater_UpdateDatabaseBeforeUpdateSchematopic).
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
    }
    
    public static class StringHelper
    {
        public static string Ho(this string source)
        {
            int index = source.LastIndexOf(' ');
            return source.Substring(0, index).VietHoaThuong();
        }

        public static string Ten(this string source)
        {
            int index = source.LastIndexOf(' ') + 1;
            string temp = source.Substring(index, source.Length - index);

            return temp.VietHoaThuong();
        }

        public static string VietHoaThuong(this string source)
        {
            string[] split = source.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (string item in split)
            {
                sb.Append(item.Substring(0, 1).ToUpper());
                sb.Append(item.Substring(1).ToLower());
                sb.Append(" ");
            }
            return sb.ToString().Trim();
        }

        public static string GetString(this object source)
        {
            return source.ToString().Trim();
        }

        public static string GetLowerString(this object source)
        {
            return source.GetString().ToLower();
        }

        public static DateTime GetDateTime(this object source)
        {
            DateTime temp;
            if (DateTime.TryParse(source.GetString(), out temp))
                return temp;
            return DateTime.MinValue;
        }

        public static decimal GetDecimal(this object source)
        {
            decimal temp;
            if (decimal.TryParse(source.GetString(), out temp))
                return temp;
            return 0;
        }

        public static int GetInt(this object source)
        {
            int temp;
            if (int.TryParse(source.GetString(), out temp))
                return temp;
            return 0;
        }

        public static bool GetBool(this object source)
        {
            return source.GetLowerString().Equals("có");
        }
    }
}
