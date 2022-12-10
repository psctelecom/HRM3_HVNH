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

namespace PSC_HRM.Module.Controllers
{
    public class NangThamNien_ImportNangPhuCapThamNien
    {
        public static bool XuLy(IObjectSpace obs, DeNghiNangPhuCapThamNien deNghiNangPhuCapThamNien)
        {
            bool _oke = false;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                    {
                        ChiTietDeNghiNangPhuCapThamNien chiTietDeNghi;
                        ThongTinNhanVien nhanVien;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                //Khởi tạo bộ nhớ đệm
                                detailLog = new StringBuilder();

                                //
                                int idx_SoQuyetDinh = 0;
                                int idx_MaQuanLy = 1;
                                int idx_NgayHuongThamNienCu = 4;
                                int idx_ThamNienCu = 5;
                                int idx_NgayHuongThamNienMoi = 6;
                                int idx_ThamNienMoi = 7;

                                //Tìm nhân viên theo mã quản lý
                                nhanVien = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item[idx_MaQuanLy].ToString().Trim(),item[idx_MaQuanLy].ToString().Trim()));
                                if (nhanVien != null)
                                {
                                    chiTietDeNghi = obs.FindObject<ChiTietDeNghiNangPhuCapThamNien>(CriteriaOperator.Parse("ThongTinNhanVien = ? and DeNghiNangPhuCapThamNien = ?", nhanVien.Oid, deNghiNangPhuCapThamNien.Oid));
                                    if (chiTietDeNghi == null)
                                    {
                                        chiTietDeNghi = new ChiTietDeNghiNangPhuCapThamNien(((XPObjectSpace)obs).Session);
                                        chiTietDeNghi.DeNghiNangPhuCapThamNien = obs.GetObjectByKey<DeNghiNangPhuCapThamNien>(deNghiNangPhuCapThamNien.Oid);
                                        chiTietDeNghi.BoPhan = nhanVien.BoPhan;
                                        chiTietDeNghi.ThongTinNhanVien = nhanVien;
                                    }

                                    #region Số quyết định
                                    if (!item.IsNull(idx_SoQuyetDinh) && !string.IsNullOrEmpty(item[idx_SoQuyetDinh].ToString()))
                                    {
                                        chiTietDeNghi.SoQuyetDinh = item[idx_SoQuyetDinh].ToString().Trim();
                                    }
                                    #endregion

                                    #region Ngày hưởng thâm niên cũ
                                    if (!item.IsNull(idx_NgayHuongThamNienCu) && !string.IsNullOrEmpty(item[idx_NgayHuongThamNienCu].ToString()))
                                    {
                                        try
                                        {
                                            DateTime ngayHuongThamNienCu = Convert.ToDateTime(item[idx_NgayHuongThamNienCu].ToString().Trim());
                                            if (ngayHuongThamNienCu != null && ngayHuongThamNienCu != DateTime.MinValue)
                                                chiTietDeNghi.NgayHuongThamNienCu = ngayHuongThamNienCu;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Ngày hưởng thâm niên cũ không hợp lệ:" + item[idx_NgayHuongThamNienCu].ToString());
                                        }
                                    }
                                    #endregion

                                    #region % thâm niên cũ
                                    if (!item.IsNull(idx_ThamNienCu) && !string.IsNullOrEmpty(item[idx_ThamNienCu].ToString()))
                                    {
                                        try
                                        {
                                            chiTietDeNghi.ThamNienCu = Convert.ToDecimal(item[idx_ThamNienCu].ToString().Trim());
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + % thâm niên cũ không hợp lệ:" + item[idx_ThamNienCu].ToString());
                                        }
                                    }
                                    #endregion


                                    #region Ngày hưởng thâm niên mới
                                    if (!item.IsNull(idx_NgayHuongThamNienMoi) && !string.IsNullOrEmpty(item[idx_NgayHuongThamNienMoi].ToString()))
                                    {
                                        try
                                        {
                                            DateTime ngayHuongThamNienMoi = Convert.ToDateTime(item[idx_NgayHuongThamNienMoi].ToString().Trim());
                                            if (ngayHuongThamNienMoi != null && ngayHuongThamNienMoi != DateTime.MinValue)
                                                chiTietDeNghi.NgayHuongThamNienMoi = ngayHuongThamNienMoi;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Ngày hưởng thâm niên mới không hợp lệ:" + item[idx_NgayHuongThamNienMoi].ToString());
                                        }
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(" + Ngày hưởng thâm niên mới không tìm thấy.");
                                    }
                                    #endregion

                                    #region % thâm niên mới
                                    if (!item.IsNull(idx_ThamNienMoi) && !string.IsNullOrEmpty(item[idx_ThamNienMoi].ToString()))
                                    {
                                        try
                                        {
                                            chiTietDeNghi.ThamNienMoi = Convert.ToDecimal(item[idx_ThamNienMoi].ToString().Trim());
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + % thâm niên mới không hợp lệ:" + item[idx_ThamNienMoi].ToString());
                                        }
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(" + % thâm niên mới không tìm thấy.");
                                    }
                                    #endregion

                                    //Đưa thông tin bị lỗi vào blog
                                    if (detailLog.Length > 0)
                                    {
                                        mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                        mainLog.AppendLine(detailLog.ToString());
                                    }
                                    else
                                        deNghiNangPhuCapThamNien.ListChiTietDeNghiNangPhuCapThamNien.Add(chiTietDeNghi);

                                }
                                else
                                {
                                    mainLog.AppendLine(string.Format("- Không có cán bộ nào có mã nhân sự (Số hiệu công chức) là: {0}", item[idx_MaQuanLy].ToString().Trim()));
                                }
                            }
                        }

                        if (mainLog.Length > 0)
                        {
                            //Tiến hành trả lại dữ liệu không import vào phần mền
                            deNghiNangPhuCapThamNien.ListChiTietDeNghiNangPhuCapThamNien.Reload();

                            if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin nâng phụ cấp thâm niên. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
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
                            //Xuất thông báo lỗi
                            _oke = false;
                        }
                        else
                        {
                            //Xuất thông báo thành công
                            _oke = true;
                        }
                    }
                }
            }
            return _oke;
        }
    }

}
