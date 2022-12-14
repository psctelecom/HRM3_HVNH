using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.ThuNhap.KhauTru;
using DevExpress.Utils;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.BoSungLuong;

namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ModelDefault("Caption", "Import chi bổ sung lương phụ cấp ưu đãi")]
    public static class ImportChiBoSungPhuCapUuDai 
    {

        public static void XuLy(IObjectSpace obs, object obj)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.FileName = "";
                    dialog.Multiselect = false;
                    dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                BoSungLuongNhanVien boSungLuongNhanVien = obj as BoSungLuongNhanVien;
                                StringBuilder mainLog = new StringBuilder();
                                StringBuilder detailLog = null;

                                using (DialogUtil.AutoWait())
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        int idx_MaQuanLy = 0;
                                        int idx_HoTen = 1;
                                        int idx_HeSoLuongCu = 2;
                                        int idx_HeSoLuongMoi = 3;
                                        int idx_TuThang = 4;
                                        int idx_DenThang = 5;
                                        int idx_PhuCapUuDai = 6;
                                        int idx_SoThangTruyLinh = 7;
                                        int idx_SoNgayNghi = 8;
                                        int idx_GhiChu = 9;

                                        ThongTinNhanVien thongTinNhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item.ItemArray[idx_MaQuanLy].ToString().Trim(), item.ItemArray[idx_MaQuanLy].ToString().Trim()));
                                        if (thongTinNhanVien != null)
                                        {
                                            ChiBoSungPhuCapUuDai chiBoSungLuongPhuCapUuDai = uow.FindObject<ChiBoSungPhuCapUuDai>(CriteriaOperator.Parse("BoSungLuongNhanVien=? and ThongTinNhanVien=?", boSungLuongNhanVien.Oid, thongTinNhanVien.Oid));
                                            if (chiBoSungLuongPhuCapUuDai == null)
                                            {
                                                chiBoSungLuongPhuCapUuDai = new ChiBoSungPhuCapUuDai(uow);
                                                chiBoSungLuongPhuCapUuDai.BoSungLuongNhanVien = uow.GetObjectByKey<BoSungLuongNhanVien>(boSungLuongNhanVien.Oid);
                                                chiBoSungLuongPhuCapUuDai.BoPhan = thongTinNhanVien.BoPhan;
                                                chiBoSungLuongPhuCapUuDai.ThongTinNhanVien = thongTinNhanVien;
                                            }
                                            //Hệ số lương cũ
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_HeSoLuongCu].ToString()))
                                            {
                                                try
                                                {
                                                    decimal heSoLuongCu = Convert.ToDecimal(item.ItemArray[idx_HeSoLuongCu].ToString().Trim());
                                                    //
                                                    chiBoSungLuongPhuCapUuDai.HeSoLuongCu = heSoLuongCu;
                                                
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Hệ số lương cũ không đúng định dạng: " + item.ItemArray[idx_HeSoLuongCu]);
                                                }
                                            }
                                            //Hệ số lương mới
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_HeSoLuongMoi].ToString()))
                                            {
                                                try
                                                {
                                                    decimal heSoLuongMoi = Convert.ToDecimal(item.ItemArray[idx_HeSoLuongMoi].ToString().Trim());
                                                    //
                                                    chiBoSungLuongPhuCapUuDai.HeSoLuongMoi = heSoLuongMoi;

                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Hệ số lương mới không đúng định dạng: " + item.ItemArray[idx_HeSoLuongMoi]);
                                                }
                                            }

                                             //Từ tháng
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_TuThang].ToString()))
                                            {
                                                try
                                                {
                                                    DateTime tuThang = Convert.ToDateTime(item.ItemArray[idx_TuThang].ToString().Trim());
                                                    //
                                                    chiBoSungLuongPhuCapUuDai.TuThang = tuThang;
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Từ tháng không đúng định dạng: " + item.ItemArray[idx_TuThang]);
                                                }

                                            }

                                            //Đến tháng
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_DenThang].ToString()))
                                            {
                                                try
                                                {
                                                    DateTime denThang = Convert.ToDateTime(item.ItemArray[idx_DenThang].ToString().Trim());
                                                    //
                                                    chiBoSungLuongPhuCapUuDai.DenThang = denThang;

                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Đến tháng không đúng định dạng: " + item.ItemArray[idx_DenThang]);
                                                }

                                            }

                                            //Phụ cấp ưu đãi
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_PhuCapUuDai].ToString()))
                                            {
                                                try
                                                {
                                                    int phuCapUuDai = Convert.ToInt32(item.ItemArray[idx_PhuCapUuDai].ToString().Trim());
                                                    //
                                                    chiBoSungLuongPhuCapUuDai.PhuCapUuDai = phuCapUuDai;

                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Phụ cấp ưu đãi không đúng định dạng: " + item.ItemArray[idx_PhuCapUuDai]);
                                                }

                                            }

                                            //Số tháng truy lĩnh
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_SoThangTruyLinh].ToString()))
                                            {
                                                try
                                                {
                                                    decimal soThangTruyLinh = Convert.ToDecimal(item.ItemArray[idx_SoThangTruyLinh].ToString().Trim());
                                                    //
                                                    chiBoSungLuongPhuCapUuDai.SoThangTruyLinh = soThangTruyLinh;

                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Số tháng truy lĩnh không đúng định dạng: " + item.ItemArray[idx_SoThangTruyLinh]);
                                                }
                                            }

                                            //Số ngày nghĩ
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_SoNgayNghi].ToString()))
                                            {
                                                try
                                                {
                                                    int soNgayNghi = Convert.ToInt32(item.ItemArray[idx_SoNgayNghi].ToString().Trim());
                                                    //
                                                    chiBoSungLuongPhuCapUuDai.SoNgayNghi = soNgayNghi;

                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Số ngày nghĩ không đúng định dạng: " + item.ItemArray[idx_SoNgayNghi]);
                                                }

                                            }

                                            //Ghi chú
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_GhiChu].ToString()))
                                            {
                                                chiBoSungLuongPhuCapUuDai.GhiChu = item.ItemArray[idx_GhiChu].ToString().Trim();
                                            }
                                                
                                           
                                        }
                                        else
                                        {
                                            detailLog.Append(string.Format("+ Không có cán bộ nào có mã nhân sự (Số hiệu công chức) là: {0}", item.ItemArray[idx_MaQuanLy]));
                                        }

                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không import chi bổ sung lương kỳ 1 của cán bộ [{0}] vào được: ", item.ItemArray[idx_HoTen]));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                    }

                                    if (mainLog.Length > 0)
                                    {
                                        uow.RollbackTransaction();
                                        if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin một số thông tin. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
                                        {
                                            using (SaveFileDialog saveFile = new SaveFileDialog())
                                            {
                                                saveFile.Filter = "Text files (*.txt)|*.txt";

                                                if (saveFile.ShowDialog() == DialogResult.OK)
                                                {
                                                    HamDungChung.WriteLog(saveFile.FileName, mainLog.ToString());
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        uow.CommitChanges();
                                        //
                                        DialogUtil.ShowInfo("Import dữ liệu từ file excel thành công.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
