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
using PSC_HRM.Module.NangThamNienTangThem;

namespace PSC_HRM.Module.Controllers
{
    public class NangThamNienTangThem_ImportNangThamNienTangThem
    {
        public static bool XuLy(IObjectSpace obs, DeNghiNangThamNienTangThem deNghiNangThamNienTangThem)
        {
            bool _oke = false;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                    {
                        ChiTietDeNghiNangThamNienTangThem chiTietDeNghi;
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
                                int idx_MocHuongThamNienTangThemCu = 4;
                                int idx_HSLTangThemTheoThamNienCu = 5;
                                int idx_MocHuongThamNienTangThemMoi = 6;
                                int idx_HSLTangThemTheoThamNienMoi = 7;

                                //Tìm nhân viên theo mã quản lý
                                nhanVien = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item[idx_MaQuanLy].ToString().Trim(),item[idx_MaQuanLy].ToString().Trim()));
                                if (nhanVien != null)
                                {
                                    chiTietDeNghi = obs.FindObject<ChiTietDeNghiNangThamNienTangThem>(CriteriaOperator.Parse("ThongTinNhanVien = ? and DeNghiNangThamNienTangThem = ?", nhanVien.Oid, deNghiNangThamNienTangThem.Oid));
                                    if (chiTietDeNghi == null)
                                    {
                                        chiTietDeNghi = new ChiTietDeNghiNangThamNienTangThem(((XPObjectSpace)obs).Session);
                                        chiTietDeNghi.DeNghiNangThamNienTangThem = obs.GetObjectByKey<DeNghiNangThamNienTangThem>(deNghiNangThamNienTangThem.Oid);
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
                                    if (!item.IsNull(idx_MocHuongThamNienTangThemCu) && !string.IsNullOrEmpty(item[idx_MocHuongThamNienTangThemCu].ToString()))
                                    {
                                        try
                                        {
                                            DateTime mocHuongThamNienTangThemCu = Convert.ToDateTime(item[idx_MocHuongThamNienTangThemCu].ToString().Trim());
                                            if (mocHuongThamNienTangThemCu != null && mocHuongThamNienTangThemCu != DateTime.MinValue)
                                                chiTietDeNghi.MocHuongThamNienTangThemCu = mocHuongThamNienTangThemCu;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + mốc hưởng thâm niên tăng thêm cũ không hợp lệ:" + item[idx_MocHuongThamNienTangThemCu].ToString());
                                        }
                                    }
                                    #endregion

                                    #region thâm niên cũ
                                    if (!item.IsNull(idx_HSLTangThemTheoThamNienCu) && !string.IsNullOrEmpty(item[idx_HSLTangThemTheoThamNienCu].ToString()))
                                    {
                                        try
                                        {
                                            HSLTangThemTheoThamNien hslTangThemTheoThamNien = obs.FindObject<HSLTangThemTheoThamNien>(CriteriaOperator.Parse("HeSoPhuCap=?", Convert.ToDecimal(item[idx_HSLTangThemTheoThamNienCu].ToString().Trim())));
                                            if (hslTangThemTheoThamNien != null)
                                            {
                                                chiTietDeNghi.HSLTangThemTheoThamNienCu = hslTangThemTheoThamNien;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + thâm niên tăng thêm cũ không hợp lệ:" + item[idx_HSLTangThemTheoThamNienCu].ToString());
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + thâm niên tăng thêm cũ không hợp lệ:" + item[idx_HSLTangThemTheoThamNienCu].ToString());
                                        }
                                    }
                                    #endregion

                                    #region Ngày hưởng thâm niên mới
                                    if (!item.IsNull(idx_MocHuongThamNienTangThemMoi) && !string.IsNullOrEmpty(item[idx_MocHuongThamNienTangThemMoi].ToString()))
                                    {
                                        try
                                        {
                                            DateTime mocHuongThamNienTangThemMoi = Convert.ToDateTime(item[idx_MocHuongThamNienTangThemMoi].ToString().Trim());
                                            if (mocHuongThamNienTangThemMoi != null && mocHuongThamNienTangThemMoi != DateTime.MinValue)
                                                chiTietDeNghi.MocHuongThamNienTangThemMoi = mocHuongThamNienTangThemMoi;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Mốc hưởng thâm niên tăng thêm mới không hợp lệ:" + item[idx_MocHuongThamNienTangThemMoi].ToString());
                                        }
                                    }
                                    #endregion

                                    #region thâm niên mới
                                    if (!item.IsNull(idx_HSLTangThemTheoThamNienMoi) && !string.IsNullOrEmpty(item[idx_HSLTangThemTheoThamNienMoi].ToString()))
                                    {
                                        try
                                        {
                                            HSLTangThemTheoThamNien hslTangThemTheoThamNien = obs.FindObject<HSLTangThemTheoThamNien>(CriteriaOperator.Parse("HeSoPhuCap=?", Convert.ToDecimal(item[idx_HSLTangThemTheoThamNienMoi].ToString().Trim())));
                                            if (hslTangThemTheoThamNien != null)
                                            {
                                                chiTietDeNghi.HSLTangThemTheoThamNienMoi = hslTangThemTheoThamNien;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + thâm niên tăng thêm mới không hợp lệ:" + item[idx_HSLTangThemTheoThamNienMoi].ToString());
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + thâm niên tăng thêm mới không hợp lệ:" + item[idx_HSLTangThemTheoThamNienMoi].ToString());
                                        }
                                    }
                                    #endregion

                                    //Đưa thông tin bị lỗi vào blog
                                    if (detailLog.Length > 0)
                                    {
                                        mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                        mainLog.AppendLine(detailLog.ToString());
                                    }
                                    else
                                        deNghiNangThamNienTangThem.ListChiTietDeNghiNangThamNienTangThem.Add(chiTietDeNghi);

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
                            deNghiNangThamNienTangThem.ListChiTietDeNghiNangThamNienTangThem.Reload();

                            if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin nâng thâm niên tăng thêm. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
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
