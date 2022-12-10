using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.HoSo;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.ChamCong;
using System.Text;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module;
using PSC_HRM.Module.NangThamNien;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Controllers
{
    public class NangThamNien_ImportNangPhuCapThamNienHC
    {
        public static void XuLy(IObjectSpace obs ,QuyetDinhNangPhuCapThamNienHanhChinh quyetDinh)
        {
            bool oke = false;
            //
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();
                            using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                            {
                                ThongTinNhanVien nhanVien;
                                StringBuilder mainLog = new StringBuilder();
                                StringBuilder detailLog;

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        int idx_SoHieuCongChuc = 0;
                                        int idx_PhanTramThamNienHC = 2;
                                        int idx_NgayHuongThamNienHCMoi = 3;

                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc=?", item[idx_SoHieuCongChuc].ToString().Trim(), item[idx_SoHieuCongChuc].ToString().Trim()));
                                        if (nhanVien != null)
                                        {
                                            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinhNangPhuCapThamNienHanhChinh = ? and ThongTinNhanVien = ?",quyetDinh.Oid,nhanVien.Oid);
                                            ChiTietQuyetDinhNangPhuCapThamNienHanhChinh chiTiet = uow.FindObject<ChiTietQuyetDinhNangPhuCapThamNienHanhChinh>(filter);
                                            if (chiTiet == null)
                                            {
                                                chiTiet = new ChiTietQuyetDinhNangPhuCapThamNienHanhChinh(uow);
                                                chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                chiTiet.QuyetDinhNangPhuCapThamNienHanhChinh = uow.GetObjectByKey<QuyetDinhNangPhuCapThamNienHanhChinh>(quyetDinh.Oid);
                                            }
                                            //Phần trăm thâm niên hành chính mới
                                            if (!item.IsNull(idx_PhanTramThamNienHC) && !string.IsNullOrEmpty(item[idx_PhanTramThamNienHC].ToString()))
                                            {
                                                try
                                                {
                                                    chiTiet.PhanTramThamNienHCMoi = Convert.ToDecimal(item[idx_PhanTramThamNienHC].ToString().Trim());
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Phần trăm thâm niên hành chính mới không hợp lệ:" + item[idx_PhanTramThamNienHC].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Phần trăm thâm niên hành chính mới không tìm thấy.");
                                            }
                                            //Ngày hưởng thâm niên hành chính mới
                                            if (!item.IsNull(idx_NgayHuongThamNienHCMoi) && !string.IsNullOrEmpty(item[idx_NgayHuongThamNienHCMoi].ToString()))
                                            {
                                                try
                                                {
                                                    chiTiet.NgayHuongThamNienHCMoi = Convert.ToDateTime(item[idx_NgayHuongThamNienHCMoi].ToString().Trim());
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Ngày hưởng phần trăm thâm niên hành chính mới không hợp lệ:" + item[idx_NgayHuongThamNienHCMoi].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Ngày hưởng phần trăm thâm niên hành mới không tìm thấy.");
                                            }

                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import phụ cấp khối hành chính của cán bộ [{0}] vào phần mềm được: ", nhanVien.HoTen));
                                                mainLog.AppendLine(detailLog.ToString());

                                                //Thoát 
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            mainLog.AppendLine(string.Format("- Không có cán bộ nào có mã quản lý hoặc số hiệu công chức là: {0}", item[0]));
                                        }
                                    }
                                }

                                if (mainLog.Length > 0)
                                {
                                    //Tiến hành trả lại dữ liệu không import vào phần mền
                                    uow.RollbackTransaction();
                                    //
                                    oke = false;

                                    if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin chấm công. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
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
                                    //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                    uow.CommitChanges();
                                    //
                                    oke = true;
                                }
                            }
                        }
                    }
                }
            }
            if (oke)
            {
                DialogUtil.ShowInfo("Import phần trăm thâm niên hành chính mới thành công!");
            }
        }
    }

}
