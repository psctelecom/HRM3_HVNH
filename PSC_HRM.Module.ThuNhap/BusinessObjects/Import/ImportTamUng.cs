using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.ThuNhap.TamUng;
using PSC_HRM.Module.HoSo;
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
    [ModelDefault("Caption", "Import tạm ứng")]
    public class ImportTamUng : ImportBase
    {
        public ImportTamUng(Session session) : base(session) { }

        public override void XuLy(IObjectSpace obs, object obj)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.FileName = "";
                    dialog.Multiselect = false;
                    dialog.Filter = "Excel 1997-2003 file (*.xls)|*.xls";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                BangTamUng bangTamUng = obj as BangTamUng;
                                ThongTinNhanVien nhanVien;
                                TamUng.TamUng chiTiet;
                                decimal soTien = 0;
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
                                            chiTiet = uow.FindObject<TamUng.TamUng>(CriteriaOperator.Parse("BangTamUng=? and ThongTinNhanVien=?", 
                                                bangTamUng.Oid, nhanVien.Oid, ngay));
                                            if (chiTiet == null)
                                            {
                                                chiTiet = new TamUng.TamUng(uow);
                                                chiTiet.BangTamUng = uow.GetObjectByKey<BangTamUng>(bangTamUng.Oid);
                                                chiTiet.BoPhan = nhanVien.BoPhan;
                                                chiTiet.ThongTinNhanVien = nhanVien;
                                            }

                                            ChiTietTamUng chiTietTamUng = uow.FindObject<ChiTietTamUng>(CriteriaOperator.Parse("TamUng=? and NgayLap=?", 
                                                chiTiet.Oid, ngay));
                                            if (chiTietTamUng == null)
                                            {
                                                chiTietTamUng = new ChiTietTamUng(uow);
                                                chiTietTamUng.TamUng = chiTiet;
                                                chiTietTamUng.NgayLap = ngay;
                                                chiTietTamUng.SoTien = soTien;
                                                chiTietTamUng.LyDo = item[6].ToString();
                                            }

                                            uow.CommitChanges();
                                        }
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
                        }
                    }
                }
            }
        }
    }

}
