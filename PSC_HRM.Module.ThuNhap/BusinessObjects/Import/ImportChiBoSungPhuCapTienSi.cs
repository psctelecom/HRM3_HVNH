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
using PSC_HRM.Module.DanhMuc;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ModelDefault("Caption", "Import chi bổ sung lương phụ cấp tiến sĩ")]
    public static class ImportChiBoSungPhuCapTienSi 
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
                                        int idx_SoTien = 2;
                                        int idx_SoThangTruyLinh = 3;

                                        ThongTinNhanVien thongTinNhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item.ItemArray[idx_MaQuanLy].ToString().Trim(), item.ItemArray[idx_MaQuanLy].ToString().Trim()));
                                        if (thongTinNhanVien != null)
                                        {
                                            ChiBoSungPhuCapTienSi chiBoSungPhuCapTienSi = uow.FindObject<ChiBoSungPhuCapTienSi>(CriteriaOperator.Parse("BoSungLuongNhanVien=? and ThongTinNhanVien=?", boSungLuongNhanVien.Oid, thongTinNhanVien.Oid));
                                            if (chiBoSungPhuCapTienSi == null)
                                            {
                                                chiBoSungPhuCapTienSi = new ChiBoSungPhuCapTienSi(uow);
                                                chiBoSungPhuCapTienSi.BoSungLuongNhanVien = uow.GetObjectByKey<BoSungLuongNhanVien>(boSungLuongNhanVien.Oid);
                                                chiBoSungPhuCapTienSi.BoPhan = thongTinNhanVien.BoPhan;
                                                chiBoSungPhuCapTienSi.ThongTinNhanVien = thongTinNhanVien;
                                                
                                            }

                                            //Số tiền
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_SoTien].ToString()))
                                            {
                                                try
                                                {
                                                    decimal soTien = Convert.ToDecimal(item.ItemArray[idx_SoTien].ToString().Trim());
                                                    //
                                                    chiBoSungPhuCapTienSi.SoTien = soTien;
                                                
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Số tiền không đúng định dạng: " + item.ItemArray[idx_SoTien]);
                                                }
                                            }

                                            //Số tháng truy lĩnh
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_SoThangTruyLinh].ToString()))
                                            {
                                                try
                                                {
                                                    decimal soThangTruyLinh = Convert.ToDecimal(item.ItemArray[idx_SoThangTruyLinh].ToString().Trim());
                                                    //
                                                    chiBoSungPhuCapTienSi.SoThangTruyLinh = soThangTruyLinh;

                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Số tháng truy lĩnh không đúng định dạng: " + item.ItemArray[idx_SoThangTruyLinh]);
                                                }
                                            }
                                                
                                            //Tính thành tiền
                                            chiBoSungPhuCapTienSi.ThanhTien = chiBoSungPhuCapTienSi.SoTien * chiBoSungPhuCapTienSi.SoThangTruyLinh;
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
