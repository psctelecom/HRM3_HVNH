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
    public class ChamCong_ImportChamCongKhac
    {
        public static bool XuLy(IObjectSpace obs, QuanLyChamCongKhac quanLyChamCong)
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
                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            ChiTietChamCongKhac chamCong;
                            ThongTinNhanVien nhanVien;
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    //Khởi tạo bộ nhớ đệm
                                    detailLog = new StringBuilder();

                                    int idx_MaQuanLy = 0;
                                    int idx_SoNgayCong = 3;
                                    int idx_GhiChu = 4;

                                    //Tìm nhân viên theo mã quản lý
                                    nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", item[idx_MaQuanLy].ToString().Trim()));
                                    if (nhanVien != null)
                                    {
                                        chamCong = uow.FindObject<ChiTietChamCongKhac>(CriteriaOperator.Parse("ThongTinNhanVien = ? and QuanLyChamCongKhac = ?", nhanVien.Oid, quanLyChamCong.Oid));
                                        if (chamCong == null)
                                        {
                                            chamCong = new ChiTietChamCongKhac(uow);
                                            chamCong.QuanLyChamCongKhac = uow.GetObjectByKey<QuanLyChamCongKhac>(quanLyChamCong.Oid);
                                            chamCong.BoPhan = nhanVien.BoPhan;
                                            chamCong.ThongTinNhanVien = nhanVien;
                                        }

                                        //Số ngày công
                                        if (!item.IsNull(idx_SoNgayCong) && !string.IsNullOrEmpty(item[idx_SoNgayCong].ToString()))
                                        {
                                            try
                                            {
                                                chamCong.SoNgayCong = Convert.ToDecimal(item[idx_SoNgayCong].ToString().Trim());
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Số ngày công không hợp lệ:" + item[idx_SoNgayCong].ToString());
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Số ngày công không tìm thấy.");
                                        }

                                        //Ghi chú
                                        if (!item.IsNull(idx_GhiChu) && !string.IsNullOrEmpty(item[idx_GhiChu].ToString()))
                                        {
                                           chamCong.GhiChu =item[idx_GhiChu].ToString();
                                        }


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
                                        mainLog.AppendLine(string.Format("- Không có cán bộ nào có mã quản lý là: {0}", item[0]));
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
