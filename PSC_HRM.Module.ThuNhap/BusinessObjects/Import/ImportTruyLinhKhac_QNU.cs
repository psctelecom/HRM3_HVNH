using System;
using System.Collections.Generic;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using System.Windows.Forms;
using System.Data;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Text;
using PSC_HRM.Module.ThuNhap.TruyLuong;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.ThuNhapTangThem;
using PSC_HRM.Module.BaoMat;
using System.IO;
using PSC_HRM.Module.DanhMuc;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    [ModelDefault("Caption", "Import truy lĩnh khác")]
    public class ImportTruyLinhKhac_QNU : ImportBase
    {
        public ImportTruyLinhKhac_QNU(Session session)
            : base(session)
        { }

        public override void XuLy(IObjectSpace obs, object obj)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                //import
                using (OpenFileDialog dialog = new OpenFileDialog())
                {

                    dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";
                    dialog.Multiselect = false;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {

                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                                //
                                uow.BeginTransaction();
                                //
                                var mainLog = new StringBuilder();
                                //
                                const int maQuanLyIdx = 0;
                                const int hoTenIdx = 1;                               
                                const int luongTangThemIdx = 2;
                                const int tienTrachNhiemQuanLyIdx = 3;
                                const int tienPhucVuDaoTaoIdx = 4;
                                const int tienDienThoaiCongVuIdx = 5;                              
                                const int ghiChuIdx = 6;
                                //
                                using(DialogUtil.AutoWait())
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        var errorLog = new StringBuilder();
                                        //
                                        String maQuanLy = item[maQuanLyIdx].ToString().Trim();
                                        String hoTenNhanVien = item[hoTenIdx].ToString().Trim();                                       
                                        String luongTangThem = item[luongTangThemIdx].ToString().Trim();
                                        String tienTrachNhiemQuanLy = item[tienTrachNhiemQuanLyIdx].ToString().Trim();
                                        String tienPhucVuDaoTao = item[tienPhucVuDaoTaoIdx].ToString().Trim();
                                        String tienDienThoaiCongVu = item[tienDienThoaiCongVuIdx].ToString().Trim();
                                        String ghiChu = item[ghiChuIdx].ToString().Trim();
                                        //
                                        ThongTinNhanVien nhanVien = GetNhanVien(uow, maQuanLy, maQuanLy);
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine(string.Format("+ Không có nhân viên nào có Mã quản lý (Số hiệu công chức, CMND hoặc Số tài khoản): [{0}] trong hệ thống", maQuanLy));
                                        }
                                        else
                                        {
                                            BangTruyLinhKhac bangTruyLinhKhac = uow.GetObjectByKey<BangTruyLinhKhac>((obj as BangTruyLinhKhac).Oid);
                                            if (bangTruyLinhKhac == null)
                                                return;

                                            TruyLinhKhac truyLinhKhac = uow.FindObject<TruyLinhKhac>(CriteriaOperator.Parse("BangTruyLinhKhac = ? and ThongTinNhanVien = ? ", bangTruyLinhKhac, nhanVien));
                                            if (truyLinhKhac == null)
                                            {
                                                truyLinhKhac = new TruyLinhKhac(uow);
                                                truyLinhKhac.BangTruyLinhKhac = bangTruyLinhKhac;
                                                truyLinhKhac.ThongTinNhanVien = nhanVien;
                                                truyLinhKhac.BoPhan = nhanVien.BoPhan;
                                            }                                          

                                            #region 1. Lương tăng thêm
                                            if (!string.IsNullOrEmpty(luongTangThem))
                                            {
                                                try
                                                {
                                                    ChiTietTruyLinhKhac chiTiet = uow.FindObject<ChiTietTruyLinhKhac>(CriteriaOperator.Parse("TruyLinhKhac = ? and MaChiTiet like 'TNTT' ", truyLinhKhac));
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietTruyLinhKhac(uow);
                                                        chiTiet.TruyLinhKhac = truyLinhKhac;
                                                        chiTiet.CongTru = CongTruEnum.Cong;
                                                        chiTiet.MaChiTiet = "TNTT";
                                                        chiTiet.DienGiai = "Thu nhập tăng thêm";
                                                    }
                                                    //
                                                    chiTiet.SoTienTruyLinh = Convert.ToDecimal(luongTangThem);
                                                }
                                                catch
                                                {
                                                    errorLog.Append("+ Lương tăng thêm không hợp lệ: " + luongTangThem);
                                                }
                                            }
                                            #endregion

                                            #region 2. Tiền trách nhiệm quản lý
                                            if (!string.IsNullOrEmpty(tienTrachNhiemQuanLy))
                                            {
                                                try
                                                {
                                                    ChiTietTruyLinhKhac chiTiet = uow.FindObject<ChiTietTruyLinhKhac>(CriteriaOperator.Parse("TruyLinhKhac = ? and MaChiTiet like 'TNQL' ", bangTruyLinhKhac, nhanVien));
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietTruyLinhKhac(uow);
                                                        chiTiet.TruyLinhKhac = truyLinhKhac;
                                                        chiTiet.CongTru = CongTruEnum.Cong;
                                                        chiTiet.MaChiTiet = "TNQL";
                                                        chiTiet.DienGiai = "Tiền trách nhiệm quản lý";
                                                    }
                                                    //
                                                    chiTiet.SoTienTruyLinh = Convert.ToDecimal(tienTrachNhiemQuanLy);
                                                }
                                                catch
                                                {
                                                    errorLog.Append("+ Tiền trách nhiệm quản lý không hợp lệ: " + tienTrachNhiemQuanLy);
                                                }
                                            }
                                            #endregion

                                            #region 4. Tiền phục vụ đào tạo
                                            if (!string.IsNullOrEmpty(tienPhucVuDaoTao))
                                            {
                                                try
                                                {
                                                    ChiTietTruyLinhKhac chiTiet = uow.FindObject<ChiTietTruyLinhKhac>(CriteriaOperator.Parse("TruyLinhKhac = ?  and MaChiTiet like 'BHYT' ", truyLinhKhac));
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietTruyLinhKhac(uow);
                                                        chiTiet.TruyLinhKhac = truyLinhKhac;
                                                        chiTiet.CongTru = CongTruEnum.Cong;
                                                        chiTiet.MaChiTiet = "PVDT";
                                                        chiTiet.DienGiai = "Tiền phục vụ đào tạo";
                                                    }
                                                    //
                                                    chiTiet.SoTienTruyLinh = Convert.ToDecimal(tienPhucVuDaoTao);
                                                }
                                                catch
                                                {
                                                    errorLog.Append("+ Tiền phục vụ đào tạo không hợp lệ: " + tienPhucVuDaoTao);
                                                }
                                            }
                                            #endregion

                                            #region 5. Tiền điện thoại công vụ
                                            if (!string.IsNullOrEmpty(tienDienThoaiCongVu))
                                            {
                                                try
                                                {
                                                    ChiTietTruyLinhKhac chiTiet = uow.FindObject<ChiTietTruyLinhKhac>(CriteriaOperator.Parse("TruyLinhKhac = ? and  MaChiTiet like 'BHTN' ", truyLinhKhac));
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietTruyLinhKhac(uow);
                                                        chiTiet.TruyLinhKhac = truyLinhKhac;
                                                        chiTiet.CongTru = CongTruEnum.Cong;
                                                        chiTiet.MaChiTiet = "PCDT";
                                                        chiTiet.DienGiai = "Tiền điện thoại công vụ";
                                                    }
                                                    //
                                                    chiTiet.SoTienTruyLinh = Convert.ToDecimal(tienDienThoaiCongVu);
                                                }
                                                catch
                                                {
                                                    errorLog.Append("+ Tiền điện thoại công vụ không hợp lệ: " + tienDienThoaiCongVu);
                                                }
                                            }
                                            #endregion                                          

                                            #region 6. Ghi chú
                                            if (!string.IsNullOrEmpty(ghiChu))
                                            {
                                                truyLinhKhac.GhiChu = ghiChu;
                                            }
                                            #endregion

                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import Truy lĩnh lương cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                                mainLog.AppendLine(errorLog.ToString());
                                            }
                                        }
                                    }
                                }

                                if (mainLog.Length > 0)
                                {
                                    uow.RollbackTransaction();
                                    if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi") == DialogResult.Yes)
                                    {
                                        string tenFile = "Import_Log.txt";
                                        //FileStream fileStream = File.Open(tenFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                                        StreamWriter writer = new StreamWriter(tenFile);
                                        writer.WriteLine(mainLog.ToString());
                                        writer.Flush();
                                        writer.Close();
                                        writer.Dispose();
                                        HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                        System.Diagnostics.Process.Start(tenFile);
                                    }
                                }
                                else
                                {
                                    uow.CommitChanges();
                                    //
                                    DialogUtil.ShowInfo("Import Thành công !");
                                }
                        }
                    }
                }
            }
        }
    }
}
