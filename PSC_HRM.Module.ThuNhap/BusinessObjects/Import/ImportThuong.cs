using System;
using System.Collections.Generic;
using PSC_HRM.Module.ThuNhap.Thuong;
using DevExpress.Data.Filtering;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.HoSo;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.XtraEditors;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    [ModelDefault("Caption", "Import thưởng")]
    public class ImportThuong : ImportBase
    {
        public ImportThuong(Session session)
            : base(session)
        { }

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
                                BangThuongNhanVien bangThuong = obj as BangThuongNhanVien;
                                ThongTinNhanVien nhanVien;
                                ChiTietThuongNhanVien chiTietThuong;
                                decimal soTien, soTienChiuThue;
                                DateTime ngay;
                                StringBuilder log = null;

                                foreach (DataRow item in dt.Rows)
                                {
                                    nhanVien = GetNhanVien(uow, item[0].ToString(),string.Empty);
                                    if (nhanVien != null)
                                    {
                                        if (DateTime.TryParse(item[4].ToString(), out ngay) &&
                                                decimal.TryParse(item[5].ToString(), out soTien) &&
                                                soTien > 0)
                                        {
                                            chiTietThuong = uow.FindObject<ChiTietThuongNhanVien>(CriteriaOperator.Parse("BangThuongNhanVien=? and NhanVien=? and NgayThuong=?",
                                                bangThuong.Oid, nhanVien.Oid, ngay));
                                            if (chiTietThuong == null)
                                            {
                                                chiTietThuong = new ChiTietThuongNhanVien(uow);
                                                chiTietThuong.BangThuongNhanVien = uow.GetObjectByKey<BangThuongNhanVien>(bangThuong.Oid);
                                                chiTietThuong.BoPhan = nhanVien.BoPhan;
                                                chiTietThuong.NhanVien = nhanVien;
                                            }

                                            chiTietThuong.NgayThuong = ngay;
                                            chiTietThuong.SoTien = soTien;
                                            if (decimal.TryParse(item[6].ToString(), out soTienChiuThue))
                                                chiTietThuong.SoTienChiuThue = soTienChiuThue;
                                            chiTietThuong.GhiChu = item[7].ToString();

                                            uow.CommitChanges();
                                        }
                                        else
                                        {
                                            if (log == null)
                                            {
                                                log = new StringBuilder();
                                                log.AppendLine("Danh sách cán bộ không import được dữ liệu từ file excel vào phần mềm:");
                                            }
                                            log.AppendLine(String.Format(" - {0} - {1} {2} - {3}", item[0], item[1], item[2], item[3]));
                                        }
                                    }
                                }

                                if (log != null)
                                {
                                    if (XtraMessageBox.Show("Có một số cán bộ không import được dữ liệu vào phần mềm.\r\nBạn có muốn lưu danh sách những cán bộ không import được dữ liệu?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                                    {
                                        using (SaveFileDialog saveFile = new SaveFileDialog())
                                        {
                                            saveFile.Filter = "Text files (*.txt)|*.txt";

                                            if (saveFile.ShowDialog() == DialogResult.OK)
                                            {
                                                HamDungChung.WriteLog(saveFile.FileName, log.ToString());
                                            }
                                        }
                                    }
                                }
                                else
                                    HamDungChung.ShowSuccessMessage("Import dữ liệu từ file excel thành công");
                            }
                            else
                                HamDungChung.ShowWarningMessage("Không có dữ liệu hoặc không có sheet tên là Sheet1");
                        }
                    }
                }
            }
        }
    }
}
