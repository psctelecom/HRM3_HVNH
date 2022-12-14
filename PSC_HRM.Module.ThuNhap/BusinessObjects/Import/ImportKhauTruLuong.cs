using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.ThuNhap.KhauTru;
using DevExpress.Utils;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ModelDefault("Caption", "Import khấu trừ lương")]
    public class ImportKhauTruLuong : ImportBase
    {
        public ImportKhauTruLuong(Session session) : base(session) { }

        public override void XuLy(IObjectSpace obs, object obj)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.FileName = "";
                    dialog.Multiselect = false;
                    dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                BangKhauTruLuong bangKhauTruLuong = obj as BangKhauTruLuong;
                                StringBuilder mainLog = new StringBuilder();
                                StringBuilder detailLog = null;

                                using (DialogUtil.AutoWait())
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        int idx_MaQuanLy = 0;
                                        int idx_HoTen = 1;
                                        int idx_NgayKhauTru = 2;
                                        int idx_SoTien = 3;
                                        int idx_GhiChu = 4;

                                        ThongTinNhanVien nhanVien = GetNhanVien(uow, item[idx_MaQuanLy].ToString(), item.ItemArray[idx_HoTen].ToString().Trim());
                                        if (nhanVien != null)
                                        {
                                            if (!string.IsNullOrEmpty(item.ItemArray[idx_SoTien].ToString()))
                                            {
                                                try
                                                {
                                                    decimal soTienKhauTru = Convert.ToDecimal(item.ItemArray[idx_SoTien].ToString().Trim());
                                                    //
                                                    if (soTienKhauTru > 0)
                                                    {
                                                        ChiTietKhauTruLuong chiTietKhauTru = uow.FindObject<ChiTietKhauTruLuong>(CriteriaOperator.Parse("BangKhauTruLuong=? and ThongTinNhanVien=?", bangKhauTruLuong.Oid, nhanVien.Oid));
                                                        if (chiTietKhauTru == null)
                                                        {
                                                            chiTietKhauTru = new ChiTietKhauTruLuong(uow);
                                                            chiTietKhauTru.BangKhauTruLuong = uow.GetObjectByKey<BangKhauTruLuong>(bangKhauTruLuong.Oid);
                                                            chiTietKhauTru.BoPhan = nhanVien.BoPhan;
                                                            chiTietKhauTru.ThongTinNhanVien = nhanVien;
                                                        }
                                                        //Số tiền khấu trừ
                                                        chiTietKhauTru.SoTien = soTienKhauTru;

                                                        //Ngày khấu trừ
                                                        if (!string.IsNullOrEmpty(item.ItemArray[idx_NgayKhauTru].ToString()))
                                                        {
                                                            try
                                                            {
                                                                DateTime ngayKhauTru = Convert.ToDateTime(item.ItemArray[idx_NgayKhauTru].ToString().Trim());
                                                                chiTietKhauTru.NgayLap = ngayKhauTru;
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.Append("+ Ngày khấu trừ không đúng định dạng: " + item.ItemArray[idx_NgayKhauTru]);
                                                            }

                                                        }
                                                        //Ghi chú
                                                        if (!string.IsNullOrEmpty(item.ItemArray[idx_GhiChu].ToString()))
                                                        {
                                                            chiTietKhauTru.GhiChu = item.ItemArray[idx_GhiChu].ToString().Trim();
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.Append("+ Số tiền khấu trừ không đúng định dạng: " + item.ItemArray[idx_SoTien]);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.Append("+ Số tiền khấu trừ phải lớn hơn 0.");
                                            }   
                                        }
                                        else
                                        {
                                            detailLog.Append(string.Format("+ Không có cán bộ nào có Mã quản lý (Số hiệu công chức, CMND hoặc Số tài khoản) là: {0}", item.ItemArray[idx_MaQuanLy]));
                                        }

                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không import khấu trừ lương của cán bộ [{0}] vào được: ", item.ItemArray[idx_HoTen]));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                    }

                                    if (mainLog.Length > 0)
                                    {
                                        uow.RollbackTransaction();
                                        if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin khấu trừ lương. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
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
                                        uow.CommitChanges();
                                        //
                                        DialogUtil.ShowInfo("Import dữ liệu từ file excel thành công.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
