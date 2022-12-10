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
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public class ChamCong_ImportChamCongNEU
    {
        public static bool XuLy(IObjectSpace obs, QuanLyChamCongNhanVien quanLyChamCong)
        {
            bool _oke = false;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();
                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet$]"))
                        {
                            ChiTietChamCongNhanVien chamCong;
                            ThongTinNhanVien nhanVien;
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    //Khởi tạo bộ nhớ đệm
                                    detailLog = new StringBuilder();

                                    int idx_MaQuanLy = 1;
                                    int idx_NghiPhep = 4;
                                    int idx_NghiRo = 5;                                    
                                    int idx_NghiThaiSan = 6;
                                    //int idx_DanhGiaCanBo = 8;

                                    //Tìm nhân viên theo mã quản lý
                                    nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc=?", item[idx_MaQuanLy].ToString().Trim()));
                                    if (nhanVien != null)
                                    {
                                        chamCong = uow.FindObject<ChiTietChamCongNhanVien>(CriteriaOperator.Parse("ThongTinNhanVien = ? and QuanLyChamCongNhanVien = ?", nhanVien.Oid, quanLyChamCong.Oid));
                                        if (chamCong == null)
                                        {
                                            chamCong = new ChiTietChamCongNhanVien(uow);
                                            chamCong.QuanLyChamCongNhanVien = uow.GetObjectByKey<QuanLyChamCongNhanVien>(quanLyChamCong.Oid);
                                            chamCong.BoPhan = nhanVien.BoPhan;
                                            chamCong.ThongTinNhanVien = nhanVien;
                                        }

                                        //Nghỉ Phép
                                        if (!item.IsNull(idx_NghiPhep) && !string.IsNullOrEmpty(item[idx_NghiPhep].ToString()))
                                        {
                                            try
                                            {
                                                chamCong.NghiCoPhep = Convert.ToInt32(item[idx_NghiPhep].ToString().Trim());
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Nghỉ phép không hợp lệ:" + item[idx_NghiPhep].ToString());
                                            }
                                        }
                                        //else
                                        //{
                                        //    detailLog.AppendLine(" + Nghỉ phép không tìm thấy.");
                                        //}

                                        //Nghỉ Ro
                                        if (!item.IsNull(idx_NghiRo) && !string.IsNullOrEmpty(item[idx_NghiRo].ToString()))
                                        {
                                            try
                                            {
                                                chamCong.NghiRo = Convert.ToInt32(item[idx_NghiRo].ToString().Trim());
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Nghỉ Ro không hợp lệ:" + item[idx_NghiRo].ToString());
                                            }
                                        }
                                        //else
                                        //{
                                        //    detailLog.AppendLine(" + Nghỉ Ro không tìm thấy.");
                                        //}

                                        //Nghỉ thai sản
                                        if (!item.IsNull(idx_NghiThaiSan) && !string.IsNullOrEmpty(item[idx_NghiThaiSan].ToString()))
                                        {
                                            try
                                            {
                                                chamCong.NghiThaiSan = Convert.ToInt32(item[idx_NghiThaiSan].ToString().Trim());
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Nghỉ thai sản không hợp lệ:" + item[idx_NghiThaiSan].ToString());
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Nghỉ thai sản không tìm thấy.");
                                        }

                                        /*
                                        //Đánh giá cán bộ
                                        if (!item.IsNull(idx_DanhGiaCanBo) && !string.IsNullOrEmpty(item[idx_DanhGiaCanBo].ToString()))
                                        {
                                            chamCong.DanhGia = item[idx_DanhGiaCanBo].ToString();
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(string.Format(" + Cán bộ [{0}] chưa có đánh giá.",nhanVien.HoTen));
                                        }
                                        */

                                        chamCong.SoNgayCong = (22 - chamCong.NghiRo - chamCong.NghiThaiSan ) > 0 ?
                                                                (22 - chamCong.NghiRo - chamCong.NghiThaiSan) : 0;

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không import chấm công của cán bộ [{0}] vào phần mềm được: ", nhanVien.HoTen));
                                            mainLog.AppendLine(detailLog.ToString());

                                            //Thoát 
                                            break;
                                        }

                                    }
                                    else
                                    {
                                        //mainLog.AppendLine(string.Format("- Không có cán bộ nào có mã quản lý là: {0}", item[0]));
                                    }
                                }
                            }

                            if (mainLog.Length > 0)
                            {
                                //Tiến hành trả lại dữ liệu không import vào phần mền
                                uow.RollbackTransaction();

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

                                //Xuất thông báo lỗi
                                _oke = false;
                            }
                            else
                            {
                                //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                uow.CommitChanges();

                                //Xuất thông báo thành công
                                _oke = true;
                            }
                        }
                    }
                }
            }
            return _oke;
        }
    }

}
