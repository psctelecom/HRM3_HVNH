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
    [ModelDefault("Caption", "Import truy lĩnh")]
    public class ImportTruyLuong : ImportBase
    {
        public ImportTruyLuong(Session session)
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

                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:G]"))
                        {
                                BangTruyLuong bangTruyLuong = obj as BangTruyLuong;
                               
                                uow.BeginTransaction();
                                ThongTinNhanVien nhanVien;
                                var mainLog = new StringBuilder();
                                var errorLog = new StringBuilder();

                                TruyLuongNhanVien truyLuong = null;
                                ChiTietTruyLuong chiTiet = null;
                                XPCollection<TruyLuongNhanVien> listChiTiet = new XPCollection<TruyLuongNhanVien>(uow);

                                const int sttIdx = 0;
                                const int boPhanIdx = 1;
                                const int maQuanLyIdx = 2;
                                const int hoTenIdx = 3;
                                const int soTienIdx = 4;
                                const int soTienChiuThueIdx = 5;
                                const int ghiChuIdx = 6;

                                foreach (DataRow item in dt.Rows)
                                {
                                    String sttText = item[sttIdx].ToString();
                                    String tenBoPhan = item[boPhanIdx].ToString().Trim();
                                    String maQuanLy = item[maQuanLyIdx].ToString().Trim();
                                    String hoTenNhanVien = item[hoTenIdx].ToString().Trim();
                                    String soTien = item[soTienIdx].ToString().Trim();
                                    String soTienChiuThue = item[soTienChiuThueIdx].ToString().Trim();
                                    String ghiChu = item[ghiChuIdx].ToString().Trim();

                                    nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy = ? && HoTen = ?", maQuanLy, hoTenNhanVien));
                                    if (nhanVien == null)
                                    {
                                        errorLog.AppendLine(string.Format("+ Mã quản lý: {0}, {1} chưa tồn tại trong hệ thống", maQuanLy, hoTenNhanVien));
                                    }
                                    else
                                    {
                                        CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien =? && BangTruyLuong =?", nhanVien, bangTruyLuong);
                                        listChiTiet.Filter = filter;

                                        if (listChiTiet.Count > 0)
                                        {
                                            chiTiet = new ChiTietTruyLuong(uow);
                                            chiTiet.CongTru = CongTruEnum.Cong;
                                            chiTiet.KyTinhLuong = uow.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Oid = ?", bangTruyLuong.KyTinhLuong.Oid));
                                            chiTiet.TruyLuongNhanVien = listChiTiet[0];
                                            chiTiet.MaChiTiet = ghiChu;

                                            try
                                            {
                                                chiTiet.SoTien = Convert.ToDecimal(soTien);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine("+ Sai thông tin số tiền");
                                            }

                                            if ((!string.IsNullOrEmpty(soTienChiuThue)))
                                            try
                                            {
                                                chiTiet.SoTienChiuThue = Convert.ToDecimal(soTienChiuThue);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine("+ Sai thông tin số tiền chịu thuế");
                                            }

                                        }
                                        else
                                        {
                                            truyLuong = new TruyLuongNhanVien(uow);
                                            truyLuong.BangTruyLuong = uow.FindObject<BangTruyLuong>(CriteriaOperator.Parse("Oid = ?", bangTruyLuong.Oid));

                                            BoPhan boPhan;
                                            boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan Like ?", tenBoPhan));
                                            if (boPhan != null)
                                                truyLuong.BoPhan = boPhan;

                                            truyLuong.ThongTinNhanVien = nhanVien;
                                            truyLuong.TuNgay = bangTruyLuong.TuNgay;
                                            truyLuong.DenNgay = bangTruyLuong.DenNgay;
                                            try
                                            {
                                                truyLuong.SoTien = Convert.ToDecimal(soTien);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine("+ Sai thông tin số tiền");
                                            }

                                            if(!string.IsNullOrEmpty(soTienChiuThue))
                                            try
                                            {
                                                truyLuong.SoTienChiuThue = Convert.ToDecimal(soTienChiuThue);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine("+ Sai thông tin số tiền chịu thuế");
                                            }

                                            chiTiet = new ChiTietTruyLuong(uow);
                                            chiTiet.CongTru = CongTruEnum.Cong;
                                            chiTiet.KyTinhLuong = uow.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Oid = ?", bangTruyLuong.KyTinhLuong.Oid));
                                            chiTiet.TruyLuongNhanVien = truyLuong;
                                            chiTiet.MaChiTiet = ghiChu;

                                            try
                                            {
                                                chiTiet.SoTien = Convert.ToDecimal(soTien);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine("+ Sai thông tin số tiền");
                                            }

                                            try
                                            {
                                                chiTiet.SoTienChiuThue = Convert.ToDecimal(soTienChiuThue);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine("+ Sai thông tin số tiền chịu thuế");
                                            }
                                        }
                                    }

                                    if (errorLog.Length > 0)
                                    {
                                        mainLog.AppendLine("- STT: " + sttText + " - Họ Tên:" + hoTenNhanVien);
                                        mainLog.AppendLine(errorLog.ToString());
                                    }
                                    else
                                    {
                                        listChiTiet.Add(truyLuong);
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
                                    DialogUtil.ShowInfo("Import Thành công !");
                                }
                        }
                    }
                }
            }
        }

    }
}
