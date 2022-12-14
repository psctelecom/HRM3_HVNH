using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Windows.Forms;
using DevExpress.Utils;
using System.Data;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.ChamCong;
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
    [ModelDefault("Caption", "Import bảng chấm công ngoài giờ")]
    public class ImportChamCongNgoaiGio : ImportBase
    {
        public ImportChamCongNgoaiGio(Session session) : base(session) { }

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
                                using (WaitDialogForm wait = new WaitDialogForm("Đang import bảng chấm công từ file excel", "Vui lòng chờ..."))
                                {
                                    BangChamCongNgoaiGio bangChamCong = obj as BangChamCongNgoaiGio;
                                    ChiTietChamCongNgoaiGio chamCong;
                                    ThongTinNhanVien nhanVien;
                                    decimal cong = 0, cong1 = 0, cong2 = 0;
                                    decimal congsau23h = 0, cong1sau23h = 0, cong2sau23h = 0;
                                    decimal conglamdem = 0;
                                    StringBuilder log = null;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        nhanVien = GetNhanVien(uow, item[0].ToString(),string.Empty);
                                        if (nhanVien != null
                                            && ((decimal.TryParse(item[3].ToString(), out cong) && cong >= 0) &&
                                                (decimal.TryParse(item[5].ToString(), out cong1) && cong1 >= 0) &&
                                                (decimal.TryParse(item[7].ToString(), out cong2) && cong2 >= 0)) &&
                                                (decimal.TryParse(item[4].ToString(), out congsau23h) && congsau23h >= 0) &&
                                                (decimal.TryParse(item[6].ToString(), out cong1sau23h) && cong1sau23h >= 0) &&
                                                (decimal.TryParse(item[8].ToString(), out cong2sau23h) && cong2sau23h >= 0) &&
                                                (decimal.TryParse(item[9].ToString(), out conglamdem) && conglamdem >= 0))
                                        {
                                            chamCong = uow.FindObject<ChiTietChamCongNgoaiGio>(CriteriaOperator.Parse("BangChamCongNgoaiGio=? and ThongTinNhanVien=?", bangChamCong.Oid, nhanVien.Oid));
                                            if (chamCong == null)
                                            {
                                                chamCong = new ChiTietChamCongNgoaiGio(uow);
                                                chamCong.BangChamCongNgoaiGio = uow.GetObjectByKey<BangChamCongNgoaiGio>(bangChamCong.Oid);
                                                chamCong.BoPhan = nhanVien.BoPhan;
                                                chamCong.ThongTinNhanVien = nhanVien;
                                            }

                                            chamCong.SoCongNgoaiGio = cong;
                                            chamCong.SoCongNgoaiGio1 = cong1;
                                            chamCong.SoCongNgoaiGio2 = cong2;
                                            chamCong.SoCongNgoaiGioSau23Gio = congsau23h;
                                            chamCong.SoCongNgoaiGio1Sau23Gio = cong1sau23h;
                                            chamCong.SoCongNgoaiGio2Sau23Gio = cong2sau23h;
                                            chamCong.SoNgayLamDem = conglamdem;

                                            uow.CommitChanges();
                                        }
                                        else
                                        {
                                            if (log == null)
                                            {
                                                log = new StringBuilder();
                                                log.AppendLine("Danh sách cán bộ không import được dữ liệu từ file excel vào phần mềm:");
                                            }
                                            log.AppendLine(String.Format(" - {0} - {1} {2}", item[0], item[1], item[2]));
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
