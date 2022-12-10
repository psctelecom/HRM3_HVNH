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
    public class ImportTruyLinhKhac : ImportBase
    {
        public ImportTruyLinhKhac(Session session)
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
                                const int luongHeSoIdx = 2;
                                const int luongTangThemIdx = 3;
                                const int baoHiemXaHoiIdx = 4;
                                const int baoHiemYTeIdx = 5;
                                const int baoHiemThatNghiepIdx = 6;
                                const int congDoanIdx = 7;
                                const int ghiChuIdx = 8;
                                //
                                using(DialogUtil.AutoWait())
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        var errorLog = new StringBuilder();
                                        //
                                        String maQuanLy = item[maQuanLyIdx].ToString().Trim();
                                        String hoTenNhanVien = item[hoTenIdx].ToString().Trim();
                                        String luongHeSo = item[luongHeSoIdx].ToString().Trim();
                                        String luongTangThem = item[luongTangThemIdx].ToString().Trim();
                                        String baoHiemXaHoi = item[baoHiemXaHoiIdx].ToString().Trim();
                                        String baoHiemYTe = item[baoHiemYTeIdx].ToString().Trim();
                                        String baoHiemThatNghiep = item[baoHiemThatNghiepIdx].ToString().Trim();
                                        String congDoan = item[congDoanIdx].ToString().Trim();
                                        String ghiChu = item[ghiChuIdx].ToString().Trim();
                                        //
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy like ?", maQuanLy));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine(string.Format("+ Không có nhân viên nào có Mã quản lý: [{0}] trong hệ thống", maQuanLy));
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
                                            //
                                            #region 1. Lương hệ số
                                            if (!string.IsNullOrEmpty(luongHeSo))
                                            {
                                                try
                                                {
                                                    ChiTietTruyLinhKhac chiTiet = uow.FindObject<ChiTietTruyLinhKhac>(CriteriaOperator.Parse("TruyLinhKhac = ? and MaChiTiet like 'LgNhaNuoc' ", truyLinhKhac));
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietTruyLinhKhac(uow);
                                                        chiTiet.TruyLinhKhac = truyLinhKhac;
                                                        chiTiet.CongTru = CongTruEnum.Cong;
                                                        chiTiet.MaChiTiet = "LgHeSo";
                                                        chiTiet.DienGiai = "Lương nhà nước";
                                                    }
                                                    //
                                                    chiTiet.SoTienTruyLinh = Convert.ToDecimal(luongHeSo);
                                                }
                                                catch
                                                {
                                                    errorLog.Append("+ Lương hệ số không đúng định dạng: " + luongHeSo);
                                                }
                                            }
                                            #endregion

                                            #region 2. Lương tăng thêm
                                            if (!string.IsNullOrEmpty(luongTangThem))
                                            {
                                                try
                                                {
                                                    ChiTietTruyLinhKhac chiTiet = uow.FindObject<ChiTietTruyLinhKhac>(CriteriaOperator.Parse("TruyLinhKhac = ? and MaChiTiet like 'LgTangThem' ", truyLinhKhac));
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietTruyLinhKhac(uow);
                                                        chiTiet.TruyLinhKhac = truyLinhKhac;
                                                        chiTiet.CongTru = CongTruEnum.Cong;
                                                        chiTiet.MaChiTiet = "LgTangThem";
                                                        chiTiet.DienGiai = "Lương tăng thêm";
                                                    }
                                                    //
                                                    chiTiet.SoTienTruyLinh = Convert.ToDecimal(luongTangThem);
                                                }
                                                catch
                                                {
                                                    errorLog.Append("+ Lương tăng thêm không đúng định dạng: " + luongTangThem);
                                                }
                                            }
                                            #endregion

                                            #region 3. Bảo hiểm xã hội
                                            if (!string.IsNullOrEmpty(baoHiemXaHoi))
                                            {
                                                try
                                                {
                                                    ChiTietTruyLinhKhac chiTiet = uow.FindObject<ChiTietTruyLinhKhac>(CriteriaOperator.Parse("TruyLinhKhac = ? and MaChiTiet like 'BHXH' ", bangTruyLinhKhac, nhanVien));
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietTruyLinhKhac(uow);
                                                        chiTiet.TruyLinhKhac = truyLinhKhac;
                                                        chiTiet.CongTru = CongTruEnum.Tru;
                                                        chiTiet.MaChiTiet = "BHXH";
                                                        chiTiet.DienGiai = "Bảo hiểm xã hội";
                                                    }
                                                    //
                                                    chiTiet.SoTienTruyLinh = Convert.ToDecimal(baoHiemXaHoi);
                                                }
                                                catch
                                                {
                                                    errorLog.Append("+ Bảo hiểm xã hội không đúng định dạng: " + baoHiemXaHoi);
                                                }
                                            }
                                            #endregion

                                            #region 4. Bảo hiểm y tế
                                            if (!string.IsNullOrEmpty(baoHiemYTe))
                                            {
                                                try
                                                {
                                                    ChiTietTruyLinhKhac chiTiet = uow.FindObject<ChiTietTruyLinhKhac>(CriteriaOperator.Parse("TruyLinhKhac = ?  and MaChiTiet like 'BHYT' ", truyLinhKhac));
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietTruyLinhKhac(uow);
                                                        chiTiet.TruyLinhKhac = truyLinhKhac;
                                                        chiTiet.CongTru = CongTruEnum.Tru;
                                                        chiTiet.MaChiTiet = "BHYT";
                                                        chiTiet.DienGiai = "Bảo hiểm y tế";
                                                    }
                                                    //
                                                    chiTiet.SoTienTruyLinh = Convert.ToDecimal(baoHiemYTe);
                                                }
                                                catch
                                                {
                                                    errorLog.Append("+ Bảo hiểm y tế không đúng định dạng: " + baoHiemYTe);
                                                }
                                            }
                                            #endregion

                                            #region 5. Bảo hiểm thất nghiệp
                                            if (!string.IsNullOrEmpty(baoHiemThatNghiep))
                                            {
                                                try
                                                {
                                                    ChiTietTruyLinhKhac chiTiet = uow.FindObject<ChiTietTruyLinhKhac>(CriteriaOperator.Parse("TruyLinhKhac = ? and  MaChiTiet like 'BHTN' ", truyLinhKhac));
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietTruyLinhKhac(uow);
                                                        chiTiet.TruyLinhKhac = truyLinhKhac;
                                                        chiTiet.CongTru = CongTruEnum.Tru;
                                                        chiTiet.MaChiTiet = "BHTN";
                                                        chiTiet.DienGiai = "Bảo hiểm thất nghiệp";
                                                    }
                                                    //
                                                    chiTiet.SoTienTruyLinh = Convert.ToDecimal(baoHiemThatNghiep);
                                                }
                                                catch
                                                {
                                                    errorLog.Append("+ Bảo hiểm thất nghiệp không đúng định dạng: " + baoHiemThatNghiep);
                                                }
                                            }
                                            #endregion

                                            #region 6. Công đoàn
                                            if (!string.IsNullOrEmpty(congDoan))
                                            {
                                                try
                                                {
                                                    ChiTietTruyLinhKhac chiTiet = uow.FindObject<ChiTietTruyLinhKhac>(CriteriaOperator.Parse("TruyLinhKhac = ?  and MaChiTiet like 'CongDoan' ", truyLinhKhac));
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietTruyLinhKhac(uow);
                                                        chiTiet.TruyLinhKhac = truyLinhKhac;
                                                        chiTiet.CongTru = CongTruEnum.Tru;
                                                        chiTiet.MaChiTiet = "CongDoan";
                                                        chiTiet.DienGiai = "Công đoàn";
                                                    }
                                                    //
                                                    chiTiet.SoTienTruyLinh = Convert.ToDecimal(congDoan);
                                                }
                                                catch
                                                {
                                                    errorLog.Append("+ Công đoàn không không đúng định dạng: " + congDoan);
                                                }
                                            }
                                            #endregion

                                            #region 7. Ghi chú
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
