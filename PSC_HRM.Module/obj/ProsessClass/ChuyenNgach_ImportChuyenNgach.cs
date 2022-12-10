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
using System.Collections.Generic;
using PSC_HRM.Module.ChuyenNgach;

namespace PSC_HRM.Module.Controllers
{
    public class ChuyenNgach_ImportChuyenNgach
    {
        public static bool XuLy(IObjectSpace obs, DeNghiChuyenNgach deNghiChuyenNgach)
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

                        ChiTietDeNghiChuyenNgach chiTietDeNghiChuyenNgach;
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
                                int idx_NgachLuongCu = 4;
                                int idx_BacLuongCu = 5;
                                int idx_NgayHuongLuongCu = 7;
                                int idx_MocNangLuongCu = 8;
                                int idx_NgachLuongMoi = 9;
                                int idx_BacLuongMoi = 10;
                                int idx_NgayHuongLuongMoi = 12;
                                int idx_MocNangLuongMoi = 13;

                                //Tìm nhân viên theo mã quản lý
                                nhanVien = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item[idx_MaQuanLy].ToString().Trim(), item[idx_MaQuanLy].ToString().Trim()));
                                if (nhanVien != null)
                                {
                                    chiTietDeNghiChuyenNgach = obs.FindObject<ChiTietDeNghiChuyenNgach>(CriteriaOperator.Parse("ThongTinNhanVien = ? and DeNghiChuyenNgach = ?", nhanVien.Oid, deNghiChuyenNgach.Oid));
                                    if (chiTietDeNghiChuyenNgach == null)
                                    {
                                        chiTietDeNghiChuyenNgach = new ChiTietDeNghiChuyenNgach(((XPObjectSpace)obs).Session);
                                        chiTietDeNghiChuyenNgach.DeNghiChuyenNgach = obs.GetObjectByKey<DeNghiChuyenNgach>(deNghiChuyenNgach.Oid);
                                        chiTietDeNghiChuyenNgach.BoPhan = nhanVien.BoPhan;
                                        chiTietDeNghiChuyenNgach.ThongTinNhanVien = nhanVien;
                                    }

                                    #region Số quyết định
                                    if (!item.IsNull(idx_SoQuyetDinh) && !string.IsNullOrEmpty(item[idx_SoQuyetDinh].ToString()))
                                    {
                                        chiTietDeNghiChuyenNgach.SoQuyetDinh = item[idx_SoQuyetDinh].ToString().Trim();
                                    }
                                    #endregion

                                    #region Ngạch lương cũ
                                    if (!item.IsNull(idx_NgachLuongCu) && !string.IsNullOrEmpty(item[idx_NgachLuongCu].ToString()))
                                    {

                                        NgachLuong ngachLuong = obs.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_NgachLuongCu].ToString().Trim()));
                                        if (ngachLuong != null)
                                        {
                                            chiTietDeNghiChuyenNgach.NgachLuongCu = ngachLuong;

                                            //Phòng trường hợp bậc lương cũ không có trong excel
                                            chiTietDeNghiChuyenNgach.BacLuongCu = nhanVien.NhanVienThongTinLuong.BacLuong;

                                            //Bậc lương cũ - Hệ số lương cũ
                                            if (!item.IsNull(idx_BacLuongCu) && !string.IsNullOrEmpty(item[idx_BacLuongCu].ToString()))
                                            {
                                                BacLuong bacLuong = obs.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ?", ngachLuong, item[idx_BacLuongCu].ToString().Trim()));
                                                if (bacLuong != null)
                                                {
                                                    chiTietDeNghiChuyenNgach.BacLuongCu = bacLuong;
                                                    chiTietDeNghiChuyenNgach.HeSoLuongCu = bacLuong.HeSoLuong;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Bậc lương cũ không hợp lệ:" + item[idx_BacLuongCu].ToString());
                                                }
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Ngạch lương cũ không hợp lệ:" + item[idx_NgachLuongCu].ToString());
                                        }
                                    }
                                    #endregion

                                    #region Ngày hưởng lương cũ
                                    if (!item.IsNull(idx_NgayHuongLuongCu) && !string.IsNullOrEmpty(item[idx_NgayHuongLuongCu].ToString()))
                                    {
                                        try
                                        {
                                            DateTime ngayHuongLuongCu = Convert.ToDateTime(item[idx_NgayHuongLuongCu].ToString().Trim());
                                            if (ngayHuongLuongCu != null && ngayHuongLuongCu != DateTime.MinValue)
                                                chiTietDeNghiChuyenNgach.NgayHuongLuongCu = ngayHuongLuongCu;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Ngày hưởng lương cũ không hợp lệ:" + item[idx_NgayHuongLuongCu].ToString());
                                        }
                                    }
                                    #endregion

                                    #region Mốc nâng lương cũ
                                    if (!item.IsNull(idx_MocNangLuongCu) && !string.IsNullOrEmpty(item[idx_MocNangLuongCu].ToString()))
                                    {
                                        try
                                        {
                                            DateTime mocNangLuongCu = Convert.ToDateTime(item[idx_MocNangLuongCu].ToString().Trim());
                                            if (mocNangLuongCu != null && mocNangLuongCu != DateTime.MinValue)
                                                chiTietDeNghiChuyenNgach.MocNangLuongCu = mocNangLuongCu;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Mốc nâng lương cũ không hợp lệ:" + item[idx_MocNangLuongCu].ToString());
                                        }
                                    }
                                    #endregion

                                    #region Ngạch lương mới
                                    if (!item.IsNull(idx_NgachLuongMoi) && !string.IsNullOrEmpty(item[idx_NgachLuongMoi].ToString()))
                                    {

                                        NgachLuong ngachLuong = obs.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_NgachLuongMoi].ToString().Trim()));
                                        if (ngachLuong != null)
                                        {
                                            chiTietDeNghiChuyenNgach.NgachLuongMoi = ngachLuong;

                                            //Bậc lương mới - Hệ số lương mới
                                            if (!item.IsNull(idx_BacLuongMoi) && !string.IsNullOrEmpty(item[idx_BacLuongMoi].ToString()))
                                            {
                                                BacLuong bacLuong = obs.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong = ? and MaQuanLy like ? and BacLuongCu = false", ngachLuong.Oid, item[idx_BacLuongMoi].ToString().Trim()));
                                                if (bacLuong != null)
                                                {
                                                    chiTietDeNghiChuyenNgach.BacLuongMoi = bacLuong;
                                                    chiTietDeNghiChuyenNgach.HeSoLuongMoi = bacLuong.HeSoLuong;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Bậc lương mới không hợp lệ:" + item[idx_BacLuongMoi].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Bậc lương mới không tìm thấy.");
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Ngạch lương mới không hợp lệ:" + item[idx_NgachLuongMoi].ToString());
                                        }
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(" + Ngạch lương mới không tìm thấy.");
                                    }
                                    #endregion

                                    #region Ngày hưởng lương mới
                                    if (!item.IsNull(idx_NgayHuongLuongMoi) && !string.IsNullOrEmpty(item[idx_NgayHuongLuongMoi].ToString()))
                                    {
                                        try
                                        {
                                            DateTime ngayHuongLuongMoi = Convert.ToDateTime(item[idx_NgayHuongLuongMoi].ToString().Trim());
                                            if (ngayHuongLuongMoi != null && ngayHuongLuongMoi != DateTime.MinValue)
                                                chiTietDeNghiChuyenNgach.NgayHuongLuongMoi = ngayHuongLuongMoi;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Ngày hưởng lương mới không hợp lệ:" + item[idx_NgayHuongLuongMoi].ToString());
                                        }
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(" + Ngày hưởng lương mới không tìm thấy.");
                                    }
                                    #endregion

                                    #region Mốc nâng lương mới
                                    if (!item.IsNull(idx_MocNangLuongMoi) && !string.IsNullOrEmpty(item[idx_MocNangLuongMoi].ToString()))
                                    {
                                        try
                                        {
                                            DateTime mocNangLuongMoi = Convert.ToDateTime(item[idx_MocNangLuongMoi].ToString().Trim());
                                            if (mocNangLuongMoi != null && mocNangLuongMoi != DateTime.MinValue)
                                                chiTietDeNghiChuyenNgach.MocNangLuongMoi = mocNangLuongMoi;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Mốc nâng lương mới không hợp lệ:" + item[idx_MocNangLuongMoi].ToString());
                                        }
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(" + Mốc nâng lương mới không tìm thấy.");
                                    }
                                    #endregion

                                    //Đưa thông tin bị lỗi vào blog
                                    if (detailLog.Length > 0)
                                    {
                                        mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                        mainLog.AppendLine(detailLog.ToString());
                                    }
                                    else
                                        deNghiChuyenNgach.ListChiTietDeNghiChuyenNgach.Add(chiTietDeNghiChuyenNgach);
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
                            deNghiChuyenNgach.ListChiTietDeNghiChuyenNgach.Reload();

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
