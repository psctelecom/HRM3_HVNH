using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.HoSo;
using System.Collections.Generic;
using DevExpress.Utils;
using PSC_HRM.Module.ThuNhap.ThuLao;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    [ModelDefault("Caption", "Import thù lao từ excel")]
    public class ImportThuLaoExcel : ImportBase
    {
        public ImportThuLaoExcel(Session session)
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
                    dialog.Filter = "Excel 2000-2003 files (*.xls)|*.xls";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        using (WaitDialogForm wait = new WaitDialogForm("Đang import thù lao từ file excel", "Vui lòng chờ..."))
                        {
                            using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    BangThuLaoNhanVien bangThuLao = obj as BangThuLaoNhanVien;
                                    ChiTietThuLaoNhanVien thuLao;
                                    ThongTinNhanVien nhanVien;
                                    decimal soTien, soTienChiuThue;
                                    DateTime ngayLap;
                                    StringBuilder log = null;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        nhanVien = GetNhanVien(uow, item[0].ToString(),string.Empty);
                                        if (nhanVien != null)
                                        {
                                            if (DateTime.TryParse(item[4].ToString(), out ngayLap) &&
                                                decimal.TryParse(item[5].ToString(), out soTien) &&
                                                soTien > 0)
                                            {
                                                thuLao = uow.FindObject<ChiTietThuLaoNhanVien>(CriteriaOperator.Parse("BangThuLaoNhanVien=? AND ThongTinNhanVien=? AND NgayLap=?", 
                                                    bangThuLao.Oid, nhanVien.Oid, ngayLap));
                                                if (thuLao == null)
                                                {
                                                    thuLao = new ChiTietThuLaoNhanVien(uow);
                                                    thuLao.BangThuLaoNhanVien = uow.GetObjectByKey<BangThuLaoNhanVien>(bangThuLao.Oid);
                                                    thuLao.BoPhan = nhanVien.BoPhan;
                                                    thuLao.NhanVien = nhanVien;
                                                }
                                                thuLao.NgayLap = ngayLap;
                                                thuLao.SoTien = soTien;
                                                if (decimal.TryParse(item[6].ToString(), out soTienChiuThue))
                                                    thuLao.SoTienChiuThue = soTienChiuThue;
                                                thuLao.DienGiai = item[7].ToString();

                                                uow.CommitChanges();
                                            }
                                        } 
                                        else
                                        {
                                            if (log == null)
                                            {
                                                log = new StringBuilder();
                                                log.AppendLine("Danh sách cán bộ không import được thù lao từ file excel vào phần mềm:");
                                            }
                                            log.AppendLine(String.Format(" - {0} - {1} {2} - {3}", item[0], item[1], item[2], item[3]));
                                        }
                                    }
                                    
                                    if (log != null)
                                    {
                                        if (XtraMessageBox.Show("Có một số cán bộ không import được thù lao vào phần mềm.\r\nBạn có muốn lưu danh sách những cán bộ không import thù lao được?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
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
                                        XtraMessageBox.Show("Import thù lao cán bộ từ file excel thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
