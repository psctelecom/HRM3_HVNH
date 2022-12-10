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
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using System.IO; 

namespace PSC_HRM.Module.Controllers
{
    public class BoNhiemNgach_ImportBoNhiemNgach
    {
        public static void XuLyCu(IObjectSpace obs, QuyetDinhBoNhiemNgach quyetDinhBoNhiemNgach)
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
                                ChiTietQuyetDinhBoNhiemNgach chiTietQuyetDinhBoNhiemNgach;
                                StringBuilder mainLog = new StringBuilder();
                                StringBuilder detailLog;

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    int idx_STT = 0;
                                    int idx_MaQuanLy = 1;
                                    int idx_HoTen = 2;
                                    int idx_NgachLuong = 3;
                                    int idx_BacLuong = 4;
                                    int idx_NgayBoNhiemNgach = 5;
                                    int idx_NgayHuongLuong = 6;
                                    int idx_MocNangLuong = 7;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        String soSTTText = item[idx_STT].ToString().FullTrim();
                                        String maQuanLyText = item[idx_MaQuanLy].ToString().FullTrim();
                                        String hoTenText = item[idx_HoTen].ToString().FullTrim();
                                        String ngachLuongText = item[idx_NgachLuong].ToString().FullTrim();
                                        String bacluongText = item[idx_BacLuong].ToString().FullTrim();
                                        String ngayBoNhiemText = item[idx_NgayBoNhiemNgach].ToString().FullTrim();
                                        String ngayHuongLuongText = item[idx_NgayHuongLuong].ToString().FullTrim();
                                        String mocNangLuongText = item[idx_MocNangLuong].ToString().FullTrim();

                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", maQuanLyText));
                                        if (nhanVien != null)
                                        {
                                            chiTietQuyetDinhBoNhiemNgach = obs.FindObject<ChiTietQuyetDinhBoNhiemNgach>(CriteriaOperator.Parse("ThongTinNhanVien = ? and QuyetDinhBoNhiemNgach = ?", nhanVien.Oid, quyetDinhBoNhiemNgach.Oid));
                                            if (chiTietQuyetDinhBoNhiemNgach == null)
                                            {
                                                chiTietQuyetDinhBoNhiemNgach = new ChiTietQuyetDinhBoNhiemNgach(uow);
                                                chiTietQuyetDinhBoNhiemNgach.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                chiTietQuyetDinhBoNhiemNgach.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                chiTietQuyetDinhBoNhiemNgach.QuyetDinhBoNhiemNgach = uow.GetObjectByKey<QuyetDinhBoNhiemNgach>(quyetDinhBoNhiemNgach.Oid);

                                                #region Ngạch lương
                                                if (!string.IsNullOrEmpty(ngachLuongText))
                                                {
                                                    NgachLuong ngachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy = ?", ngachLuongText));
                                                    if (ngachLuong != null)
                                                    {
                                                        chiTietQuyetDinhBoNhiemNgach.NgachLuong = ngachLuong;

                                                        #region Bậc lương
                                                        if (!string.IsNullOrEmpty(bacluongText))
                                                        {
                                                            BacLuong bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("MaQuanLy = ? AND NgachLuong = ?", bacluongText, ngachLuong.Oid));
                                                            if (bacLuong != null)
                                                            {
                                                                chiTietQuyetDinhBoNhiemNgach.BacLuong = bacLuong;
                                                                chiTietQuyetDinhBoNhiemNgach.HeSoLuong = bacLuong.HeSoLuong;
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine(" + Bậc lương không hợp lệ.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine(" + Bậc lương chưa có dữ liệu.");
                                                        }
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Ngạch lương không hợp lệ.");
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Ngạch lương chưa có dữ liệu.");
                                                }
                                                #endregion

                                                #region Ngày bổ nhiệm ngạch lương
                                                if (!string.IsNullOrEmpty(ngayBoNhiemText))
                                                {
                                                    try
                                                    {
                                                        chiTietQuyetDinhBoNhiemNgach.NgayBoNhiemNgach = Convert.ToDateTime(ngayBoNhiemText);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine(" + Ngày bổ nhiệm ngạch không hợp lệ: " + ngayBoNhiemText);
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Ngày bổ nhiệm ngạch chưa có dữ liệu.");
                                                }
                                                #endregion

                                                #region Ngày hưởng lương
                                                if (!string.IsNullOrEmpty(ngayHuongLuongText))
                                                {
                                                    try
                                                    {
                                                        chiTietQuyetDinhBoNhiemNgach.NgayHuongLuong = Convert.ToDateTime(ngayHuongLuongText);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine(" + Ngày hưởng lương không hợp lệ:" + ngayHuongLuongText);
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Ngày hưởng lương chưa có dữ liệu.");
                                                }
                                                #endregion

                                                #region Mốc nâng lương
                                                if (!string.IsNullOrEmpty(mocNangLuongText))
                                                {
                                                    try
                                                    {
                                                        chiTietQuyetDinhBoNhiemNgach.MocNangLuong = Convert.ToDateTime(mocNangLuongText);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine(" + Mốc nâng lương không hợp lệ:" + mocNangLuongText);
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Mốc nâng lương chưa có dữ liệu.");
                                                }
                                                #endregion
                                            }

                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("STT: {0} - Không import cán bộ [{1}] vào phần mềm được: ", soSTTText, hoTenText));
                                                mainLog.AppendLine(detailLog.ToString());

                                                //Thoát 
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            mainLog.AppendLine(string.Format("STT: {0} - Không có cán bộ nào có số hiệu công chức là: {1} - {2}", soSTTText, maQuanLyText, hoTenText));
                                        }
                                    }
                                }

                                if (mainLog.Length > 0)
                                {
                                    //Tiến hành trả lại dữ liệu không import vào phần mền
                                    uow.RollbackTransaction();
                                    //
                                    oke = false;

                                    if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai dữ liệu. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
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
                DialogUtil.ShowInfo("Import bổ nhiệm ngạch thành công!");
            }
        }

        public static void XuLy(IObjectSpace obs)
        {
            #region Import quyết định cá nhân
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                    {
                        QuyetDinhBoNhiemNgach quyetDinhBoNhiemNgach;
                        ChiTietQuyetDinhBoNhiemNgach chiTietQuyetDinhBoNhiemNgach;
                        XPCollection<QuyetDinhBoNhiemNgach> listQuyetDinh;
                        XPCollection<ChiTietQuyetDinhBoNhiemNgach> listChiTiet;
                        ThongTinNhanVien nhanVien;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;
                        using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();
                            listQuyetDinh = new XPCollection<QuyetDinhBoNhiemNgach>(uow);
                            listChiTiet = new XPCollection<ChiTietQuyetDinhBoNhiemNgach>(uow);

                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    int idx_soQuyetDinh = 0;
                                    int idx_ngayQuyetDinh = 1;
                                    int idx_ngayHieuLuc = 2;
                                    int idx_coQuanRaQuyetDinh = 3;
                                    int idx_nguoiKy = 4;
                                    int idx_maQuanLy = 5;
                                    int idx_ngachLuong = 6;
                                    int idx_bacLuong = 7;
                                    int idx_boNhiem = 8;
                                    int idx_ngayHuongLuong = 9;
                                    int idx_mocNangLuong = 10;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        String soQuyetDinhText = item[idx_soQuyetDinh].ToString().FullTrim();
                                        String ngayQuyetDinhText = item[idx_ngayQuyetDinh].ToString().FullTrim();
                                        String ngayHieuLucText = item[idx_ngayHieuLuc].ToString().FullTrim();
                                        String coQuanRaQuyetDinhText = item[idx_coQuanRaQuyetDinh].ToString().FullTrim();
                                        String nguoiKyText = item[idx_nguoiKy].ToString().FullTrim();
                                        String maQuanLyText = item[idx_maQuanLy].ToString().FullTrim();
                                        String ngachLuongText = item[idx_ngachLuong].ToString().FullTrim();
                                        String bacLuongText = item[idx_bacLuong].ToString().FullTrim();
                                        String ngayBoNhiemText = item[idx_boNhiem].ToString().FullTrim();
                                        String ngayHuongLuongText = item[idx_ngayHuongLuong].ToString().FullTrim();
                                        String mocNangLuongText = item[idx_mocNangLuong].ToString().FullTrim();

                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", maQuanLyText, maQuanLyText));
                                        if (nhanVien != null)
                                        {
                                            DateTime ngayQuyetDinh = DateTime.MinValue;

                                            #region Ngày quyết định
                                            if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                            {
                                                try
                                                {
                                                    ngayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Ngày quyết định chưa có dữ liệu");
                                            }
                                            #endregion

                                            if (!string.IsNullOrEmpty(soQuyetDinhText) && ngayQuyetDinh != DateTime.MinValue)
                                            {
                                                listQuyetDinh.Filter = CriteriaOperator.Parse("SoQuyetDinh = ? and NgayQuyetDinh = ?", soQuyetDinhText, ngayQuyetDinh);
                                            }
                                            if (listQuyetDinh.Count == 1)
                                            {
                                                quyetDinhBoNhiemNgach = listQuyetDinh[0];
                                                listChiTiet.Filter = CriteriaOperator.Parse("QuyetDinhChuyenNgach.Oid = ? and ThongTinNhanVien.Oid =?", quyetDinhBoNhiemNgach.Oid, nhanVien.Oid);
                                                if (listChiTiet.Count == 0)
                                                {
                                                    #region Thêm chi tiết

                                                    chiTietQuyetDinhBoNhiemNgach = new ChiTietQuyetDinhBoNhiemNgach(uow);
                                                    chiTietQuyetDinhBoNhiemNgach.QuyetDinhBoNhiemNgach = quyetDinhBoNhiemNgach;

                                                    chiTietQuyetDinhBoNhiemNgach.ThongTinNhanVien = nhanVien;
                                                    chiTietQuyetDinhBoNhiemNgach.BoPhan = nhanVien.BoPhan;

                                                    #region Ngạch lương mới
                                                    if (!item.IsNull(idx_ngachLuong) && !string.IsNullOrEmpty(item[idx_ngachLuong].ToString()))
                                                    {

                                                        NgachLuong ngachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_ngachLuong].ToString().Trim()));
                                                        if (ngachLuong != null)
                                                        {
                                                            chiTietQuyetDinhBoNhiemNgach.NgachLuong = ngachLuong;

                                                            //Bậc lương - Hệ số lương 
                                                            if (!item.IsNull(idx_bacLuong) && !string.IsNullOrEmpty(item[idx_bacLuong].ToString()))
                                                            {
                                                                BacLuong bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong = ? and TenBacLuong = ? and BacLuongCu = false", ngachLuong.Oid, item[idx_bacLuong].ToString().Trim()));
                                                                if (bacLuong == null)
                                                                {
                                                                    bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ? and BacLuongCu = false", ngachLuong, item[idx_bacLuong].ToString().Trim()));
                                                                }
                                                                if (bacLuong != null)
                                                                {
                                                                    chiTietQuyetDinhBoNhiemNgach.BacLuong = bacLuong;
                                                                    chiTietQuyetDinhBoNhiemNgach.HeSoLuong = bacLuong.HeSoLuong;
                                                                }
                                                                else
                                                                {
                                                                    detailLog.AppendLine(" + Bậc lương không hợp lệ:" + item[idx_bacLuong].ToString());
                                                                }
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine(" + Bậc lương không tìm thấy.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine(" + Ngạch lương không hợp lệ:" + item[idx_ngachLuong].ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Ngạch lương không tìm thấy.");
                                                    }
                                                    #endregion

                                                    #region Ngày bổ nhiệm
                                                    if (!item.IsNull(idx_boNhiem) && !string.IsNullOrEmpty(item[idx_boNhiem].ToString()))
                                                    {
                                                        try
                                                        {
                                                            DateTime ngayBoNhiem = Convert.ToDateTime(item[idx_boNhiem].ToString().Trim());
                                                            if (ngayBoNhiem != null && ngayBoNhiem != DateTime.MinValue)
                                                                chiTietQuyetDinhBoNhiemNgach.NgayBoNhiemNgach = ngayBoNhiem;
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Ngày bổ nhiệm ngạch không hợp lệ:" + item[idx_boNhiem].ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Ngày bổ nhiệm ngạch không tìm thấy.");
                                                    }
                                                    #endregion

                                                    #region Ngày hưởng lương mới
                                                    if (!item.IsNull(idx_ngayHuongLuong) && !string.IsNullOrEmpty(item[idx_ngayHuongLuong].ToString()))
                                                    {
                                                        try
                                                        {
                                                            DateTime ngayHuongLuong = Convert.ToDateTime(item[idx_ngayHuongLuong].ToString().Trim());
                                                            if (ngayHuongLuong != null && ngayHuongLuong != DateTime.MinValue)
                                                                chiTietQuyetDinhBoNhiemNgach.NgayHuongLuong = ngayHuongLuong;
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Ngày hưởng lương không hợp lệ:" + item[idx_ngayHuongLuong].ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Ngày hưởng lương không tìm thấy.");
                                                    }
                                                    #endregion

                                                    #region Mốc nâng lương mới
                                                    if (!item.IsNull(idx_mocNangLuong) && !string.IsNullOrEmpty(item[idx_mocNangLuong].ToString()))
                                                    {
                                                        try
                                                        {
                                                            DateTime mocNangLuong = Convert.ToDateTime(item[idx_mocNangLuong].ToString().Trim());
                                                            if (mocNangLuong != null && mocNangLuong != DateTime.MinValue)
                                                                chiTietQuyetDinhBoNhiemNgach.MocNangLuong = mocNangLuong;
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Mốc nâng lương không hợp lệ:" + item[idx_mocNangLuong].ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Mốc nâng lương không tìm thấy.");
                                                    }
                                                    #endregion

                                                    listChiTiet.Add(chiTietQuyetDinhBoNhiemNgach);
                                                    listQuyetDinh[0].ListChiTietQuyetDinhBoNhiemNgach.Add(chiTietQuyetDinhBoNhiemNgach);
                                                    #endregion
                                                }
                                            }
                                            else if (listQuyetDinh.Count == 0)
                                            {
                                                quyetDinhBoNhiemNgach = new QuyetDinhBoNhiemNgach(uow);
                                                quyetDinhBoNhiemNgach.QuyetDinhMoi = false;

                                                #region Số quyết định
                                                if (!string.IsNullOrEmpty(soQuyetDinhText))
                                                {
                                                    quyetDinhBoNhiemNgach.SoQuyetDinh = soQuyetDinhText;
                                                }
                                                //else
                                                //{
                                                //    detailLog.AppendLine("Số quyết định chưa có dữ liệu");
                                                //}
                                                #endregion

                                                #region Ngày quyết định
                                                if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                                {
                                                    try
                                                    {
                                                        quyetDinhBoNhiemNgach.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Ngày quyết định chưa có dữ liệu");
                                                }
                                                #endregion

                                                #region Ngày hiệu lực
                                                if (!string.IsNullOrEmpty(ngayHieuLucText))
                                                {
                                                    try
                                                    {
                                                        quyetDinhBoNhiemNgach.NgayHieuLuc = Convert.ToDateTime(ngayHieuLucText);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + Ngày hiệu lực không hợp lệ: " + ngayHieuLucText);
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Ngày hiệu lực chưa có dữ liệu");
                                                }
                                                #endregion

                                                #region Cơ quan ra quyết định - Nguời ký
                                                if (!string.IsNullOrEmpty(coQuanRaQuyetDinhText))
                                                {
                                                    quyetDinhBoNhiemNgach.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.CoQuanKhacRaQuyetDinh;
                                                    quyetDinhBoNhiemNgach.TenCoQuan = coQuanRaQuyetDinhText;

                                                    if (!string.IsNullOrEmpty(nguoiKyText))
                                                    {
                                                        quyetDinhBoNhiemNgach.NguoiKy1 = nguoiKyText;
                                                    }

                                                }
                                                //else
                                                //{
                                                //    quyetDinhChuyenNgach.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh;
                                                //}
                                                #endregion

                                                #region Thêm chi tiết
                                                //Thêm chi tiết
                                                chiTietQuyetDinhBoNhiemNgach = new ChiTietQuyetDinhBoNhiemNgach(uow);

                                                chiTietQuyetDinhBoNhiemNgach.QuyetDinhBoNhiemNgach = quyetDinhBoNhiemNgach;
                                                chiTietQuyetDinhBoNhiemNgach.ThongTinNhanVien = nhanVien;
                                                chiTietQuyetDinhBoNhiemNgach.BoPhan = nhanVien.BoPhan;

                                                #region Ngạch lương mới
                                                if (!item.IsNull(idx_ngachLuong) && !string.IsNullOrEmpty(item[idx_ngachLuong].ToString()))
                                                {

                                                    NgachLuong ngachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_ngachLuong].ToString().Trim()));
                                                    if (ngachLuong != null)
                                                    {
                                                        chiTietQuyetDinhBoNhiemNgach.NgachLuong = ngachLuong;

                                                        //Bậc lương - Hệ số lương
                                                        if (!item.IsNull(idx_bacLuong) && !string.IsNullOrEmpty(item[idx_bacLuong].ToString()))
                                                        {
                                                            BacLuong bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong = ? and TenBacLuong like ? and !BacLuongCu", ngachLuong.Oid, item[idx_bacLuong].ToString().Trim()));
                                                            if (bacLuong == null)
                                                            {
                                                                bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong = ? and MaQuanLy like ?", ngachLuong.Oid, item[idx_bacLuong].ToString().Trim()));
                                                            }
                                                            if (bacLuong != null)
                                                            {
                                                                chiTietQuyetDinhBoNhiemNgach.BacLuong = bacLuong;
                                                                chiTietQuyetDinhBoNhiemNgach.HeSoLuong = bacLuong.HeSoLuong;
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine(" + Bậc lương không hợp lệ:" + item[idx_bacLuong].ToString());
                                                            }
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine(" + Bậc lương không tìm thấy.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Ngạch lương không hợp lệ:" + item[idx_ngachLuong].ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Ngạch lương không tìm thấy.");
                                                }
                                                #endregion

                                                #region Ngày bổ nhiệm
                                                if (!item.IsNull(idx_boNhiem) && !string.IsNullOrEmpty(item[idx_boNhiem].ToString()))
                                                {
                                                    try
                                                    {
                                                        DateTime ngayBoNhiem = Convert.ToDateTime(item[idx_boNhiem].ToString().Trim());
                                                        if (ngayBoNhiem != null && ngayBoNhiem != DateTime.MinValue)
                                                            chiTietQuyetDinhBoNhiemNgach.NgayBoNhiemNgach = ngayBoNhiem;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine(" + Ngày bổ nhiệm ngạch không hợp lệ:" + item[idx_boNhiem].ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Ngày bổ nhiệm ngạch không tìm thấy.");
                                                }
                                                #endregion

                                                #region Ngày hưởng lương
                                                if (!item.IsNull(idx_ngayHuongLuong) && !string.IsNullOrEmpty(item[idx_ngayHuongLuong].ToString()))
                                                {
                                                    try
                                                    {
                                                        DateTime ngayHuongLuong = Convert.ToDateTime(item[idx_ngayHuongLuong].ToString().Trim());
                                                        if (ngayHuongLuong != null && ngayHuongLuong != DateTime.MinValue)
                                                            chiTietQuyetDinhBoNhiemNgach.NgayHuongLuong = ngayHuongLuong;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine(" + Ngày hưởng lương không hợp lệ:" + item[idx_ngayHuongLuong].ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Ngày hưởng lương không tìm thấy.");
                                                }
                                                #endregion

                                                #region Mốc nâng lương
                                                if (!item.IsNull(idx_mocNangLuong) && !string.IsNullOrEmpty(item[idx_mocNangLuong].ToString()))
                                                {
                                                    try
                                                    {
                                                        DateTime mocNangLuongMoi = Convert.ToDateTime(item[idx_mocNangLuong].ToString().Trim());
                                                        if (mocNangLuongMoi != null && mocNangLuongMoi != DateTime.MinValue)
                                                            chiTietQuyetDinhBoNhiemNgach.MocNangLuong = mocNangLuongMoi;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine(" + Mốc nâng lương không hợp lệ:" + item[idx_mocNangLuong].ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Mốc nâng lương không tìm thấy.");
                                                }
                                                #endregion

                                                listQuyetDinh.Add(quyetDinhBoNhiemNgach);
                                                listChiTiet.Add(chiTietQuyetDinhBoNhiemNgach);
                                                #endregion
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(string.Format("Có nhiều quyết định trùng số {0} - ngày quyết định {1}.", soQuyetDinhText, ngayQuyetDinhText));
                                            }


                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                                mainLog.AppendLine(detailLog.ToString());
                                            }
                                        }
                                        else
                                        {
                                            mainLog.AppendLine(string.Format("- Không có cán bộ nào có Mã nhân sự (Số hiệu công chức) là: {0}", maQuanLyText));
                                        }
                                    }
                                }
                            }

                            //
                            if (mainLog.Length > 0)
                            {
                                uow.RollbackTransaction();
                                if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                                {
                                    string tenFile = "Import_Log.txt";
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
                                //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                uow.CommitChanges();
                                //hoàn tất giao tác
                                DialogUtil.ShowSaveSuccessful("Import Thành Công!");
                            }

                        }
                    }
                }
            }

            #endregion

        }
    }
}
