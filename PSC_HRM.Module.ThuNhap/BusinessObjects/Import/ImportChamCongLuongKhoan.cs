using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.Utils;
using System.Data;
using PSC_HRM.Module.ChamCong;
using DevExpress.Persistent.Base;
using DevExpress.XtraEditors;
using System.Text;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    [ModelDefault("Caption", "Import bảng chấm công khoán")]
    public class ImportChamCongLuongKhoan : ImportBase
    {
        public ImportChamCongLuongKhoan(Session session) : base(session) { }

        public override void XuLy(IObjectSpace obs, object obj)
        {
            using (UnitOfWork uow = new UnitOfWork(Session.DataLayer))
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
                                using (WaitDialogForm wait = new WaitDialogForm("Đang import bảng chấm công từ file excel", "Vui lòng chờ..."))
                                {
                                    BangChamCongKhoan bangChamCong = obj as BangChamCongKhoan;
                                    ChiTietChamCongKhoan chamCong;
                                    ThongTinNhanVien nhanVien;
                                    int cong;
                                    StringBuilder log = null;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        nhanVien = GetNhanVien(uow, item[0].ToString(),string.Empty);
                                        if (nhanVien != null
                                            && int.TryParse(item[4].ToString(), out cong)
                                            && cong > 0)
                                        {
                                            chamCong = uow.FindObject<ChiTietChamCongKhoan>(CriteriaOperator.Parse("BangChamCongLuongKhoan=? and ThongTinNhanVien=?", bangChamCong.Oid, nhanVien.Oid));
                                            if (chamCong == null)
                                            {
                                                chamCong = new ChiTietChamCongKhoan(uow);
                                                chamCong.BangChamCongLuongKhoan = uow.GetObjectByKey<BangChamCongKhoan>(bangChamCong.Oid);
                                                chamCong.BoPhan = nhanVien.BoPhan;
                                                chamCong.ThongTinNhanVien = nhanVien;
                                            }
                                            chamCong.SoNgayCong = cong;

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

}
