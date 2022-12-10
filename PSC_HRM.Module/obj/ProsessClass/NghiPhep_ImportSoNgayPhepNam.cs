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
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using System.Collections.Generic;
using PSC_HRM.Module.NghiPhep;

namespace PSC_HRM.Module.Controllers
{
    public class NghiPhep_ImportSoNgayPhepNam
    {
        public static bool XuLy(IObjectSpace obs, QuanLyNghiPhep quanLyNghiPhep)
        {
            bool _oke = false;
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        //
                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            ThongTinNghiPhep thongTinNghiPhep;
                            ChiTietThongTinNghiPhep chiTietThongTinNghiPhep;
                            ThongTinNhanVien nhanVien;
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;

                            QuanLyNghiPhep quanlynghiphep = uow.GetObjectByKey<QuanLyNghiPhep>(quanLyNghiPhep.Oid);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    //Khởi tạo bộ nhớ đệm
                                    detailLog = new StringBuilder();
                                    //                                
                                    int idx_MaQuanLy = 0;
                                    int idx_SoNgayPhepCoBan = 1;
                                    int idx_SoNgayPhepCongThem = 2;
                                    int idx_SoNgayPhepDaNghi = 3;
                                    int idx_SoNgayPhepConLai = 4;
                                    int idx_TruNgayDiDuong = 5;
                                    int idx_SoNgayPhepThanhToan = 6;
                                    int idx_GhiChu = 7;

                                    int idx_TuNgay = 8;
                                    int idx_DenNgay = 9;
                                    int idx_SoNgay = 10;
                                    int idx_TruNgayDiDuongChiTiet = 11;
                                    int idx_GhiChuChiTiet = 12;

                                    //Tìm nhân viên theo mã quản lý
                                    nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item[idx_MaQuanLy].ToString().Trim(), item[idx_MaQuanLy].ToString().Trim()));
                                    if (nhanVien != null)
                                    {
                                        thongTinNghiPhep = uow.FindObject<ThongTinNghiPhep>(CriteriaOperator.Parse("ThongTinNhanVien = ? and QuanLyNghiPhep = ?", nhanVien.Oid, quanLyNghiPhep.Oid));
                                        if (thongTinNghiPhep == null)
                                        {
                                            thongTinNghiPhep = new ThongTinNghiPhep(uow);
                                            thongTinNghiPhep.QuanLyNghiPhep = uow.GetObjectByKey<QuanLyNghiPhep>(quanLyNghiPhep.Oid);
                                            thongTinNghiPhep.BoPhan = nhanVien.BoPhan;
                                            thongTinNghiPhep.ThongTinNhanVien = nhanVien;
                                        }

                                        #region Chi tiết thông tin nghỉ phép
                                        //if (!item.IsNull(idx_TuNgay) && !string.IsNullOrEmpty(item[idx_TuNgay].ToString()) && !item.IsNull(idx_DenNgay) && !string.IsNullOrEmpty(item[idx_DenNgay].ToString()))
                                        //{
                                        //    chiTietThongTinNghiPhep = uow.FindObject<ChiTietThongTinNghiPhep>(CriteriaOperator.Parse("ThongTinNghiPhep = ? and TuNgay = ? and DenNgay = ?", thongTinNghiPhep.Oid, item[idx_TuNgay].ToString().Trim(), item[idx_DenNgay].ToString().Trim()));
                                        //    if (chiTietThongTinNghiPhep == null)
                                        //    {
                                        //        chiTietThongTinNghiPhep = new ChiTietThongTinNghiPhep(uow);
                                        //        chiTietThongTinNghiPhep.ThongTinNghiPhep = thongTinNghiPhep;

                                        //        #region Từ ngày
                                        //        try
                                        //        {
                                        //            DateTime tuNgay = Convert.ToDateTime(item[idx_TuNgay].ToString().Trim());
                                        //            chiTietThongTinNghiPhep.TuNgay = tuNgay;
                                        //        }
                                        //        catch (Exception ex)
                                        //        {
                                        //            detailLog.AppendLine(" + Từ ngày không hợp lệ:" + item[idx_TuNgay].ToString());
                                        //        }
                                        //        #endregion

                                        //        #region Đến ngày
                                        //        try
                                        //        {
                                        //            DateTime denNgay = Convert.ToDateTime(item[idx_DenNgay].ToString().Trim());
                                        //            chiTietThongTinNghiPhep.DenNgay = denNgay;
                                        //        }
                                        //        catch (Exception ex)
                                        //        {
                                        //            detailLog.AppendLine(" + Đến ngày không hợp lệ:" + item[idx_DenNgay].ToString());
                                        //        }
                                        //        #endregion

                                        //        #region Số ngày
                                        //        if (!item.IsNull(idx_SoNgay) && !string.IsNullOrEmpty(item[idx_SoNgay].ToString()))
                                        //        {
                                        //            try
                                        //            {
                                        //                decimal soNgayPhep = Convert.ToDecimal(item[idx_SoNgay].ToString().Trim());
                                        //                chiTietThongTinNghiPhep.SoNgay = soNgayPhep;
                                        //            }
                                        //            catch (Exception ex)
                                        //            {
                                        //                detailLog.AppendLine(" + Số ngày phép không hợp lệ:" + item[idx_SoNgay].ToString());
                                        //            }
                                        //        }
                                        //        #endregion

                                        //        #region Trừ ngày đi đường
                                        //        if (!item.IsNull(idx_TruNgayDiDuongChiTiet) && !string.IsNullOrEmpty(item[idx_TruNgayDiDuongChiTiet].ToString()))
                                        //        {
                                        //            try
                                        //            {
                                        //                chiTietThongTinNghiPhep.TruNgayDiDuong = true;
                                        //            }
                                        //            catch (Exception ex)
                                        //            {
                                        //                detailLog.AppendLine(" + Trừ ngày đi đường chi tiết không hợp lệ:" + item[idx_TruNgayDiDuongChiTiet].ToString());
                                        //            }
                                        //        }
                                        //        #endregion

                                        //        #region Ghi chú chi tiết
                                        //        if (!item.IsNull(idx_GhiChuChiTiet) && !string.IsNullOrEmpty(item[idx_GhiChuChiTiet].ToString()))
                                        //        {
                                        //            chiTietThongTinNghiPhep.GhiChu = item[idx_GhiChuChiTiet].ToString().Trim();
                                        //        }
                                        //        #endregion
                                        //    }
                                        //    else
                                        //    {
                                        //        detailLog.AppendLine(" + Từ ngày - Đến ngày đã tồn tại:" + item[idx_TuNgay].ToString());
                                        //    }
                                        //}
                                        #endregion

                                        #region Ghi chú
                                        if (!item.IsNull(idx_GhiChu) && !string.IsNullOrEmpty(item[idx_GhiChu].ToString()))
                                        {
                                            thongTinNghiPhep.GhiChu = item[idx_GhiChu].ToString().Trim();
                                        }
                                        #endregion

                                        #region Ngày phép cơ bản
                                        if (!item.IsNull(idx_SoNgayPhepCoBan) && !string.IsNullOrEmpty(item[idx_SoNgayPhepCoBan].ToString()))
                                        {
                                            try
                                            {
                                                decimal soNgayPhepCoBan = Convert.ToDecimal(item[idx_SoNgayPhepCoBan].ToString().Trim());
                                                thongTinNghiPhep.SoNgayPhepCoBan = soNgayPhepCoBan;
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Số ngày phép cơ bản không hợp lệ:" + item[idx_SoNgayPhepCoBan].ToString());
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Số ngày phép cơ bản không được trống.");
                                        }
                                        #endregion

                                        #region Số ngày phép cộng thêm
                                        if (!item.IsNull(idx_SoNgayPhepCongThem) && !string.IsNullOrEmpty(item[idx_SoNgayPhepCongThem].ToString()))
                                        {
                                            try
                                            {
                                                decimal soNgayPhepCongThem = Convert.ToDecimal(item[idx_SoNgayPhepCongThem].ToString().Trim());
                                                thongTinNghiPhep.SoNgayPhepCongThem = soNgayPhepCongThem;
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Số ngày phép cộng thêm không hợp lệ:" + item[idx_SoNgayPhepCongThem].ToString());
                                            }
                                        }
                                        #endregion

                                        #region Số ngày phép đã nghỉ
                                        if (!item.IsNull(idx_SoNgayPhepDaNghi) && !string.IsNullOrEmpty(item[idx_SoNgayPhepDaNghi].ToString()))
                                        {
                                            try
                                            {
                                                decimal soNgayPhepDaNghi = Convert.ToDecimal(item[idx_SoNgayPhepDaNghi].ToString().Trim());
                                                thongTinNghiPhep.SoNgayPhepDaNghi = soNgayPhepDaNghi;
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Số ngày phép đã nghỉ không hợp lệ:" + item[idx_SoNgayPhepDaNghi].ToString());
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Số ngày phép đã nghỉ không được trống.");
                                        }
                                        #endregion

                                        #region Số ngày phép còn lại
                                        if (!item.IsNull(idx_SoNgayPhepConLai) && !string.IsNullOrEmpty(item[idx_SoNgayPhepConLai].ToString()))
                                        {
                                            try
                                            {
                                                decimal soNgayPhepConLai = Convert.ToDecimal(item[idx_SoNgayPhepConLai].ToString().Trim());
                                                thongTinNghiPhep.SoNgayPhepConLai = soNgayPhepConLai;
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Số ngày phép còn lại không hợp lệ:" + item[idx_SoNgayPhepConLai].ToString());
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Số ngày phép còn lại không được trống.");
                                        }
                                        #endregion

                                        #region Trừ ngày đi đường
                                        if (!item.IsNull(idx_TruNgayDiDuong) && !string.IsNullOrEmpty(item[idx_TruNgayDiDuong].ToString()))
                                        {
                                            try
                                            {
                                                //decimal truNgayDiDuong = Convert.ToDecimal(item[idx_TruNgayDiDuong].ToString().Trim());
                                                //if (truNgayDiDuong == 1)
                                                thongTinNghiPhep.TruNgayDiDuong = true;
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Trừ ngày đi đường không hợp lệ:" + item[idx_SoNgayPhepConLai].ToString());
                                            }
                                        }
                                        #endregion

                                        #region Số ngày phép thanh toán
                                        //if (!item.IsNull(idx_SoNgayPhepThanhToan) && !string.IsNullOrEmpty(item[idx_SoNgayPhepThanhToan].ToString()))
                                        //{
                                        //    try
                                        //    {
                                        //        decimal soNgayPhepThanhToan = Convert.ToDecimal(item[idx_SoNgayPhepThanhToan].ToString().Trim());
                                        //        thongTinNghiPhep.SoNgayThanhToan = soNgayPhepThanhToan;
                                        //    }
                                        //    catch (Exception ex)
                                        //    {
                                        //        detailLog.AppendLine(" + Số ngày phép thanh toán không hợp lệ:" + item[idx_SoNgayPhepThanhToan].ToString());
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine(" + Số ngày phép thanh toán không được trống.");
                                        //}
                                        #endregion

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không import số ngày phép của cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                        else
                                        {
                                            quanlynghiphep.ListThongTinNghiPhep.Add(thongTinNghiPhep);
                                        }
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
                                quanLyNghiPhep.ListThongTinNghiPhep.Reload();

                                if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin số ngày phép. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
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

                                uow.CommitChanges();
                            }

                        }
                    }
                }
                return _oke;
            }
        }
    }

}
