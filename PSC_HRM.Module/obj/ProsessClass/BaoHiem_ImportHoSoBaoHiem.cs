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
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.CauHinh;

namespace PSC_HRM.Module.Controllers
{
    public class BaoHiem_ImportHoSoBaoHiem
    {
        public static bool XuLy(IObjectSpace obs)
        {
            bool oke = false;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();

                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    //Khởi tạo bộ nhớ đệm
                                    detailLog = new StringBuilder();

                                    //
                                    int idx_MaQuanLy = 0;
                                    int idx_SoSoBHXH = 1;
                                    int idx_HoTen = 2;
                                    int idx_TuThang = 3;
                                    int idx_DenThang = 4;
                                    int idx_LuongKhoan = 5;
                                    int idx_HeSoLuong = 6;
                                    int idx_HSPCChucVu = 7;
                                    int idx_HSPCVuotKhung = 8;
                                    int idx_HSPCThamNien = 9;

                                    //Tìm nhân viên theo mã quản lý
                                    ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item[idx_MaQuanLy].ToString().Trim(), item[idx_MaQuanLy].ToString().Trim()));
                                    if (nhanVien != null)
                                    {
                                        HoSoBaoHiem hoSoBaoHiem = uow.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien.MaQuanLy=?", item[idx_MaQuanLy].ToString().Trim()));
                                        if (hoSoBaoHiem == null)
                                        {
                                            hoSoBaoHiem = new HoSoBaoHiem(uow);
                                            hoSoBaoHiem.ThongTinNhanVien = nhanVien;
                                            hoSoBaoHiem.BoPhan = nhanVien.BoPhan;
                                        }

                                        #region Số sổ bảo hiểm
                                        if (!item.IsNull(idx_SoSoBHXH) && !string.IsNullOrEmpty(item[idx_SoSoBHXH].ToString()))
                                        {
                                            hoSoBaoHiem.SoSoBHXH = item[idx_SoSoBHXH].ToString().Trim();
                                        }
                                        #endregion

                                        if (!item.IsNull(idx_TuThang) && !string.IsNullOrEmpty(item[idx_TuThang].ToString()) && !item.IsNull(idx_DenThang) && !string.IsNullOrEmpty(item[idx_DenThang].ToString()))
                                        {
                                            QuaTrinhThamGiaBHXH quaTrinhThamGiaBHXH = uow.FindObject<QuaTrinhThamGiaBHXH>(CriteriaOperator.Parse("TuNam=? and HoSoBaoHiem=?", Convert.ToDateTime(item[idx_TuThang].ToString().Trim()), hoSoBaoHiem != null ? hoSoBaoHiem.Oid : new Guid()));
                                            if (quaTrinhThamGiaBHXH == null)
                                            {
                                                quaTrinhThamGiaBHXH = new QuaTrinhThamGiaBHXH(uow);
                                                quaTrinhThamGiaBHXH.HoSoBaoHiem = uow.GetObjectByKey<HoSoBaoHiem>(hoSoBaoHiem.Oid);
                                            }

                                            #region Từ tháng
                                            if (!item.IsNull(idx_TuThang) && !string.IsNullOrEmpty(item[idx_TuThang].ToString()))
                                            {
                                                try
                                                {
                                                    DateTime tuThang = Convert.ToDateTime(item[idx_TuThang].ToString().Trim());
                                                    if (tuThang != null && tuThang != DateTime.MinValue)
                                                        quaTrinhThamGiaBHXH.TuNam = tuThang;
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Từ tháng không hợp lệ:" + item[idx_TuThang].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Từ tháng không tìm thấy.");
                                            }
                                            #endregion

                                            #region Đến tháng
                                            if (!item.IsNull(idx_DenThang) && !string.IsNullOrEmpty(item[idx_DenThang].ToString()))
                                            {
                                                try
                                                {
                                                    DateTime denThang = Convert.ToDateTime(item[idx_DenThang].ToString().Trim());
                                                    if (denThang != null && denThang != DateTime.MinValue)
                                                        quaTrinhThamGiaBHXH.DenNam = denThang;
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Đến tháng không hợp lệ:" + item[idx_DenThang].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Đến tháng không tìm thấy.");
                                            }
                                            #endregion

                                            #region Lương khoán
                                            if (!item.IsNull(idx_LuongKhoan) && !string.IsNullOrEmpty(item[idx_LuongKhoan].ToString()))
                                            {
                                                try
                                                {
                                                    decimal luongKhoan = Convert.ToDecimal(item[idx_LuongKhoan].ToString().Trim());
                                                    quaTrinhThamGiaBHXH.LuongKhoan = luongKhoan;
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Lương ML không hợp lệ:" + item[idx_LuongKhoan].ToString());
                                                }
                                            }
                                            #endregion

                                            #region Hệ số lương
                                            if (!item.IsNull(idx_HeSoLuong) && !string.IsNullOrEmpty(item[idx_HeSoLuong].ToString()))
                                            {
                                                try
                                                {
                                                    decimal heSoLuong = Convert.ToDecimal(item[idx_HeSoLuong].ToString().Trim());
                                                    quaTrinhThamGiaBHXH.HeSoLuong = heSoLuong;
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Hệ số lương không hợp lệ:" + item[idx_HeSoLuong].ToString());
                                                }
                                            }
                                            else
                                            {
                                                quaTrinhThamGiaBHXH.HeSoLuong = 0;
                                            }
                                            #endregion

                                            #region Hệ số vượt khung
                                            if (!item.IsNull(idx_HSPCVuotKhung) && !string.IsNullOrEmpty(item[idx_HSPCVuotKhung].ToString()))
                                            {
                                                try
                                                {
                                                    decimal hsPCVuotKhung = Convert.ToDecimal(item[idx_HSPCVuotKhung].ToString().Trim());
                                                    quaTrinhThamGiaBHXH.VuotKhung = hsPCVuotKhung;
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Hệ số vượt khung không hợp lệ:" + item[idx_HSPCVuotKhung].ToString());
                                                }
                                            }
                                            else
                                            {
                                                quaTrinhThamGiaBHXH.VuotKhung = 0;
                                            }
                                            #endregion

                                            #region Hệ số chức vụ
                                            if (!item.IsNull(idx_HSPCChucVu) && !string.IsNullOrEmpty(item[idx_HSPCChucVu].ToString()))
                                            {
                                                try
                                                {
                                                    decimal hSPCChucVu = Convert.ToDecimal(item[idx_HSPCChucVu].ToString().Trim());
                                                    quaTrinhThamGiaBHXH.PhuCapChucVu = hSPCChucVu;
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Hệ số chức vụ không hợp lệ:" + item[idx_HSPCChucVu].ToString());
                                                }
                                            }
                                            else
                                            {
                                                quaTrinhThamGiaBHXH.PhuCapChucVu = 0;
                                            }
                                            #endregion

                                            #region Hệ số thâm niên
                                            if (!item.IsNull(idx_HSPCThamNien) && !string.IsNullOrEmpty(item[idx_HSPCThamNien].ToString()))
                                            {
                                                try
                                                {
                                                    decimal hSPCThamNien = Convert.ToDecimal(item[idx_HSPCThamNien].ToString().Trim());
                                                    quaTrinhThamGiaBHXH.ThamNienGiangDay = hSPCThamNien;
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Hệ số thâm niên không hợp lệ:" + item[idx_HSPCThamNien].ToString());
                                                }
                                            }
                                            else
                                            {
                                                quaTrinhThamGiaBHXH.ThamNienGiangDay = 0;
                                            }
                                            #endregion

                                            #region Lấy tỉ lệ đóng
                                            CriteriaOperator criteria = CriteriaOperator.Parse("TuNgay <= ? AND DenNgay>= ? ", Convert.ToDateTime(item[idx_TuThang].ToString()), Convert.ToDateTime(item[idx_TuThang].ToString()));
                                            //
                                            LuongToiThieu luongToiThieu = uow.FindObject<LuongToiThieu>(criteria);
                                            if (luongToiThieu != null)
                                            {
                                                quaTrinhThamGiaBHXH.TyLeDong = luongToiThieu;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Lương tối thiểu không tìm thấy.");
                                            }
                                            #endregion

                                            //Đưa quá trình tham gia BHXH vào hồ sơ bảo hiểm
                                            hoSoBaoHiem.ListQuaTrinhThamGiaBHXH.Add(quaTrinhThamGiaBHXH);
                                        }

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", item[idx_HoTen]));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                        else  //Trường hợp này do một người lập lại nhiều dòng nên phải lưu lại liền để kiểm tra tiếp
                                        {
                                            //Tiến hành lưu liệu khi thành công
                                            uow.CommitChanges();
                                        }
                                    }
                                    else
                                    {
                                        mainLog.AppendLine(string.Format("- Không có cán bộ nào có mã quản lý là: {0}", item[idx_MaQuanLy].ToString().Trim()));
                                    }
                                }
                            }

                            if (mainLog.Length > 0)
                            {
                                //Trả dữ liệu về ban đầu nếu có lỗi
                                uow.RollbackTransaction();
                                //
                                if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
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
                                oke = false;
                            }
                            else
                            {
                                //Xuất thông báo thành công
                                oke = true;
                            }

                        }
                    }
                }
            }
            return oke;
        }
    }
}

