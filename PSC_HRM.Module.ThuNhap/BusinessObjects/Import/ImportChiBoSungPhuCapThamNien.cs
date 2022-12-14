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
    [ModelDefault("Caption", "Import chi bổ sung lương phụ cấp thâm niên")]
    public static class ImportChiBoSungPhuCapThamNien 
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
                                        int idx_TuThang = 2;
                                        int idx_ThangNamTu = 3;
                                        int idx_ThoiGianBatDauTinh = 4;
                                        int idx_DenThang = 5;
                                        

                                        ThongTinNhanVien thongTinNhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item.ItemArray[idx_MaQuanLy].ToString().Trim(), item.ItemArray[idx_MaQuanLy].ToString().Trim()));
                                        if (thongTinNhanVien != null)
                                        {
                                            ChiBoSungPhuCapThamNien chiBoSungLuongPhuCapThamNien = uow.FindObject<ChiBoSungPhuCapThamNien>(CriteriaOperator.Parse("BoSungLuongNhanVien=? and ThongTinNhanVien=?", boSungLuongNhanVien.Oid, thongTinNhanVien.Oid));
                                            if (chiBoSungLuongPhuCapThamNien == null)
                                            {
                                                chiBoSungLuongPhuCapThamNien = new ChiBoSungPhuCapThamNien(uow);
                                                chiBoSungLuongPhuCapThamNien.BoSungLuongNhanVien = uow.GetObjectByKey<BoSungLuongNhanVien>(boSungLuongNhanVien.Oid);
                                                chiBoSungLuongPhuCapThamNien.BoPhan = thongTinNhanVien.BoPhan;
                                                chiBoSungLuongPhuCapThamNien.ThongTinNhanVien = thongTinNhanVien;
                                            }
                                             //Từ tháng
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_TuThang].ToString()))
                                            {
                                                try
                                                {
                                                    DateTime tuThang = Convert.ToDateTime(item.ItemArray[idx_TuThang].ToString().Trim());
                                                    //
                                                    chiBoSungLuongPhuCapThamNien.TuThang = tuThang;
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Tính PCTN từ tháng không đúng định dạng: " + item.ItemArray[idx_TuThang]);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.Append("+ Tính PCTN từ tháng không được trống.");
                                            }

                                            //Tháng năm từ
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_ThangNamTu].ToString()))
                                            {
                                                chiBoSungLuongPhuCapThamNien.ThangNamTu = item.ItemArray[idx_ThangNamTu].ToString().Trim();
                                            }
                                            else
                                            {
                                                detailLog.Append("+ Tổng số tháng năm từ không được trống.");
                                            }

                                            //Thời gian bắt đầu tính
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_ThoiGianBatDauTinh].ToString()))
                                            {
                                                try
                                                {
                                                    DateTime thoiGianBatDauTinh = Convert.ToDateTime(item.ItemArray[idx_ThoiGianBatDauTinh].ToString().Trim());
                                                    //
                                                    chiBoSungLuongPhuCapThamNien.ThoiGianBatDauTinh = thoiGianBatDauTinh;
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Thời gian bắt đầu tính PCTN không đúng định dạng: " + item.ItemArray[idx_ThoiGianBatDauTinh]);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.Append("+ Thời gian bắt đầu tính PCTN không được trống.");
                                            }

                                            //Đến tháng
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_DenThang].ToString()))
                                            {
                                                try
                                                {
                                                    DateTime denThang = Convert.ToDateTime(item.ItemArray[idx_DenThang].ToString().Trim());
                                                    //
                                                    chiBoSungLuongPhuCapThamNien.DenThang = denThang;

                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Tính PCTN đến tháng không đúng định dạng: " + item.ItemArray[idx_DenThang]);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            detailLog.Append(string.Format("+ Không có cán bộ nào có mã nhân sự (Số hiệu công chức) là: {0}", item.ItemArray[idx_MaQuanLy]));
                                        }

                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không import chi bổ sung lương phụ cấp thâm niên của cán bộ [{0}] vào được: ", item.ItemArray[idx_HoTen]));
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
