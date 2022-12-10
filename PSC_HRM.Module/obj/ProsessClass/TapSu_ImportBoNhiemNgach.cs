using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.HoSo;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Xpo;
using System.Text;
using PSC_HRM.Module;
using System.Collections.Generic;
using PSC_HRM.Module.TapSu;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Controllers
{
    public class TapSu_ImportBoNhiemNgach
    {
        public static bool XuLy(IObjectSpace obs, DeNghiBoNhiemNgach deNghiBoNhiemNgach)
        {
            bool _oke = false;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    //
                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                    {

                        ChiTietDeNghiBoNhiemNgach chiTietDeNghiBoNhiemNgach;
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
                                int idx_NgachLuong = 4;
                                int idx_BacLuong = 5;
                                int idx_NgayHuongLuong = 7;
                                int idx_NgayBoNhiemNgach = 8;
                                int idx_MocNangLuong = 9;

                                //Tìm nhân viên theo mã quản lý
                                nhanVien = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item[idx_MaQuanLy].ToString().Trim(),item[idx_MaQuanLy].ToString().Trim()));
                                if (nhanVien != null)
                                {
                                    chiTietDeNghiBoNhiemNgach = obs.FindObject<ChiTietDeNghiBoNhiemNgach>(CriteriaOperator.Parse("ThongTinNhanVien = ? and DeNghiBoNhiemNgach = ?", nhanVien.Oid, deNghiBoNhiemNgach.Oid));
                                    if (chiTietDeNghiBoNhiemNgach == null)
                                    {
                                        chiTietDeNghiBoNhiemNgach = new ChiTietDeNghiBoNhiemNgach(((XPObjectSpace)obs).Session);
                                        chiTietDeNghiBoNhiemNgach.DeNghiBoNhiemNgach = obs.GetObjectByKey<DeNghiBoNhiemNgach>(deNghiBoNhiemNgach.Oid);
                                        chiTietDeNghiBoNhiemNgach.BoPhan = nhanVien.BoPhan;
                                        chiTietDeNghiBoNhiemNgach.ThongTinNhanVien = nhanVien;
                                    }

                                    #region Số quyết định
                                    if (!item.IsNull(idx_SoQuyetDinh) && !string.IsNullOrEmpty(item[idx_SoQuyetDinh].ToString()))
                                    {
                                        chiTietDeNghiBoNhiemNgach.SoQuyetDinh = item[idx_SoQuyetDinh].ToString().Trim();
                                    }
                                    #endregion

                                    #region Ngạch lương
                                    if (!item.IsNull(idx_NgachLuong) && !string.IsNullOrEmpty(item[idx_NgachLuong].ToString()))
                                    {
                                        NgachLuong ngachLuong = obs.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_NgachLuong].ToString().Trim()));
                                        if (ngachLuong != null)
                                        {
                                            chiTietDeNghiBoNhiemNgach.NgachLuong = ngachLuong;

                                            //Bậc lương  - Hệ số lương 
                                            if (!item.IsNull(idx_BacLuong) && !string.IsNullOrEmpty(item[idx_BacLuong].ToString()))
                                            {
                                                BacLuong bacLuong = obs.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ?", ngachLuong, item[idx_BacLuong].ToString().Trim()));
                                                if (bacLuong != null)
                                                {
                                                    chiTietDeNghiBoNhiemNgach.BacLuong = bacLuong;
                                                    chiTietDeNghiBoNhiemNgach.HeSoLuong = bacLuong.HeSoLuong;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Bậc lương không hợp lệ:" + item[idx_BacLuong].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Bậc lương không tìm thấy.");
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Ngạch lương không hợp lệ:" + item[idx_NgachLuong].ToString());
                                        }
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(" + Ngạch lương không tìm thấy.");
                                    }
                                    #endregion

                                    #region Ngày hưởng lương
                                    if (!item.IsNull(idx_NgayHuongLuong) && !string.IsNullOrEmpty(item[idx_NgayHuongLuong].ToString()))
                                    {
                                        try
                                        {
                                            DateTime ngayHuongLuong = Convert.ToDateTime(item[idx_NgayHuongLuong].ToString().Trim());
                                            if (ngayHuongLuong != null && ngayHuongLuong != DateTime.MinValue)
                                                chiTietDeNghiBoNhiemNgach.NgayHuongLuong = ngayHuongLuong;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Ngày hưởng lương không hợp lệ:" + item[idx_NgayHuongLuong].ToString());
                                        }
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(" + Ngày hưởng lương không tìm thấy.");
                                    }
                                    #endregion

                                    #region Ngày bổ nhiệm ngạch
                                    if (!item.IsNull(idx_NgayBoNhiemNgach) && !string.IsNullOrEmpty(item[idx_NgayBoNhiemNgach].ToString()))
                                    {
                                        try
                                        {
                                            DateTime ngayBoNhiemNgach = Convert.ToDateTime(item[idx_NgayBoNhiemNgach].ToString().Trim());
                                            if (ngayBoNhiemNgach != null && ngayBoNhiemNgach != DateTime.MinValue)
                                                chiTietDeNghiBoNhiemNgach.NgayBoNhiemNgach = ngayBoNhiemNgach;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Ngày bổ nhiệm ngạch không hợp lệ:" + item[idx_NgayBoNhiemNgach].ToString());
                                        }
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(" + Ngày bổ nhiệm ngạch không tìm thấy.");
                                    }
                                    #endregion

                                    #region Mốc nâng lương
                                    if (!item.IsNull(idx_MocNangLuong) && !string.IsNullOrEmpty(item[idx_MocNangLuong].ToString()))
                                    {
                                        try
                                        {
                                            DateTime mocNangLuong = Convert.ToDateTime(item[idx_MocNangLuong].ToString().Trim());
                                            if (mocNangLuong != null && mocNangLuong != DateTime.MinValue)
                                                chiTietDeNghiBoNhiemNgach.MocNangLuong = mocNangLuong;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Mốc nâng lương không hợp lệ:" + item[idx_MocNangLuong].ToString());
                                        }
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(" + Mốc nâng lương không tìm thấy.");
                                    }
                                    #endregion


                                    //Đưa thông tin bị lỗi vào blog
                                    if (detailLog.Length > 0)
                                    {
                                        mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                        mainLog.AppendLine(detailLog.ToString());
                                    }
                                    else
                                        deNghiBoNhiemNgach.ListChiTietDeNghiBoNhiemNgach.Add(chiTietDeNghiBoNhiemNgach);
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
                            deNghiBoNhiemNgach.ListChiTietDeNghiBoNhiemNgach.Reload();

                            if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin chuyển ngạch. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
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
