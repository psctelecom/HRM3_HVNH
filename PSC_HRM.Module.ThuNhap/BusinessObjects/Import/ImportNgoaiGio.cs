using System;
using System.Collections.Generic;
using PSC_HRM.Module.ThuNhap.NgoaiGio;
using DevExpress.Data.Filtering;
using System.Data;
using System.Windows.Forms;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Base;
using System.Text;
using DevExpress.XtraEditors;
using PSC_HRM.Module.ChamCong;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    [ModelDefault("Caption", "Import lương ngoài giờ")]
    public class ImportNgoaiGio : ImportBase
    {
        public ImportNgoaiGio(Session session)
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
                                BangLuongNgoaiGio bangLuong = obj as BangLuongNgoaiGio;
                                BangChamCongNgoaiGio bangChamCong = uow.FindObject<BangChamCongNgoaiGio>(CriteriaOperator.Parse("KyTinhLuong=?", bangLuong.KyTinhLuong.Oid));
                                if (bangChamCong == null)
                                {
                                    bangChamCong = new BangChamCongNgoaiGio(uow);
                                    bangChamCong.KyTinhLuong = bangLuong.KyTinhLuong;
                                    bangChamCong.NgayLap = bangLuong.NgayLap;
                                }

                                ChiTietLuongNgoaiGio luong;
                                ChiTietChamCongNgoaiGio chamCong;
                                ThongTinNhanVien nhanVien;
                                decimal temp1 = 0, temp2 = 0, temp3 = 0, temp4 = 0, temp5 = 0;
                                StringBuilder log = null;

                                foreach (DataRow item in dt.Rows)
                                {
                                    nhanVien = GetNhanVien(uow, item[0].ToString(),string.Empty);
                                    if (nhanVien != null)
                                    {
                                        if ((decimal.TryParse(item[4].ToString(), out temp1) && temp1 > 0) ||
                                            (decimal.TryParse(item[5].ToString(), out temp2) && temp2 > 0) ||
                                            (decimal.TryParse(item[6].ToString(), out temp3) && temp3 > 0) ||
                                            (decimal.TryParse(item[7].ToString(), out temp4) && temp4 > 0) ||
                                            (decimal.TryParse(item[8].ToString(), out temp5) && temp5 > 0))
                                        {
                                            luong = uow.FindObject<ChiTietLuongNgoaiGio>(CriteriaOperator.Parse("BangLuongNgoaiGio=? and ThongTinNhanVien=?", bangLuong.Oid, nhanVien.Oid));
                                            if (luong == null)
                                            {
                                                luong = new ChiTietLuongNgoaiGio(uow);

                                                luong.BangLuongNgoaiGio = uow.GetObjectByKey<BangLuongNgoaiGio>(bangLuong.Oid);
                                                luong.BoPhan = nhanVien.BoPhan;
                                                luong.ThongTinNhanVien = nhanVien;
                                            }

                                            chamCong = uow.FindObject<ChiTietChamCongNgoaiGio>(CriteriaOperator.Parse("BangChamCongNgoaiGio=? and ThongTinNhanVien=?", bangChamCong.Oid, nhanVien.Oid));
                                            if (chamCong == null)
                                            {
                                                chamCong = new ChiTietChamCongNgoaiGio(uow);
                                                chamCong.BangChamCongNgoaiGio = bangChamCong;
                                                chamCong.BoPhan = nhanVien.BoPhan;
                                                chamCong.ThongTinNhanVien = nhanVien;
                                            }

                                            luong.SoCongNgoaiGio = temp1;
                                            chamCong.SoCongNgoaiGio = temp1;
                                            luong.SoCongNgoaiGio1 = temp2;
                                            chamCong.SoCongNgoaiGio1 = temp2;
                                            luong.SoCongNgoaiGio2 = temp3;
                                            chamCong.SoCongNgoaiGio2 = temp3;
                                            luong.SoTien = temp4;
                                            luong.SoTienChiuThue = temp5;
                                            luong.GhiChu = item[9].ToString();

                                            if (luong.SoTien <= 0)
                                                uow.Delete(luong);

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
                            else
                                HamDungChung.ShowWarningMessage("Không có dữ liệu hoặc không có sheet tên là Sheet1");
                        }
                    }
                }
            }
        }
    }
}
