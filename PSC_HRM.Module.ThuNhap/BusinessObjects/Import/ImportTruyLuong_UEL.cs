using System;
using System.Collections.Generic;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using System.Windows.Forms;
using System.Data;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Text;
using PSC_HRM.Module.ThuNhap.TruyLuong;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.ThuNhapTangThem;
using PSC_HRM.Module.BaoMat;
using System.IO;
using PSC_HRM.Module.DanhMuc;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    [ModelDefault("Caption", "Import truy lĩnh")]
    public class ImportTruyLuong_UEL : ImportBase
    {
        public ImportTruyLuong_UEL(Session session)
            : base(session)
        { }

        public override void XuLy(IObjectSpace obs, object obj)
        {
            BangTruyLuong bangTruyLuong = obj as BangTruyLuong;
            if (bangTruyLuong != null
                && !bangTruyLuong.KyTinhLuong.KhoaSo
                && bangTruyLuong.ChungTu == null)
            {
                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    //import
                    using (OpenFileDialog dialog = new OpenFileDialog())
                    {
                        dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";
                        dialog.Multiselect = false;

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    //xoa du lieu cu
                                    //uow.Delete(bangTruyLuong.ListTruyLuongNhanVien);
                                    //uow.CommitChanges();

                                    ThongTinNhanVien nhanVien;
                                    TruyLuongNhanVien truyLuong = null;
                                    ChiTietTruyLuong chiTiet = null;
                                    StringBuilder log = null;
                                    decimal temp;
                                    BangTruyLuong bang = uow.GetObjectByKey<BangTruyLuong>(bangTruyLuong.Oid);

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc=?", item[0]));
                                        if (nhanVien != null)
                                        {
                                            truyLuong = uow.FindObject<TruyLuongNhanVien>(CriteriaOperator.Parse("BangTruyLuong=? and ThongTinNhanVien=?",
                                                bangTruyLuong.Oid, nhanVien.Oid));
                                            if (truyLuong == null)
                                            {
                                                truyLuong = new TruyLuongNhanVien(uow);
                                                truyLuong.BangTruyLuong = bang;
                                                truyLuong.BoPhan = nhanVien.BoPhan;
                                                truyLuong.ThongTinNhanVien = nhanVien;
                                            }

                                            if (item.ItemArray.Length > 3)
                                            {
                                                for (int i = 3; i < item.ItemArray.Length; i++)
                                                {
                                                    chiTiet = uow.FindObject<ChiTietTruyLuong>(CriteriaOperator.Parse("TruyLuongNhanVien=? and MaChiTiet=?",
                                                        truyLuong.Oid, dt.Columns[i].ColumnName));
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietTruyLuong(uow);
                                                        chiTiet.MaChiTiet = dt.Columns[i].ColumnName;
                                                        chiTiet.KyTinhLuong = bang.KyTinhLuong;
                                                        if (chiTiet.MaChiTiet.ToLower().StartsWith("bh"))
                                                            chiTiet.CongTru = CongTruEnum.Tru;
                                                        else
                                                            chiTiet.CongTru = CongTruEnum.Cong;
                                                        truyLuong.ListChiTietTruyLuong.Add(chiTiet);
                                                    }

                                                    //so tien nhan
                                                    if (decimal.TryParse(item[i].ToString(), out temp))
                                                    {
                                                        chiTiet.SoTien = temp;
                                                        if (!chiTiet.MaChiTiet.ToLower().StartsWith("bh"))
                                                            chiTiet.SoTienChiuThue = chiTiet.SoTien;
                                                    }

                                                    if (chiTiet.SoTien <= 0)
                                                        uow.Delete(chiTiet);
                                                }
                                                truyLuong.XuLy();
                                            }
                                            if (truyLuong.SoTien <= 0)
                                                uow.Delete(truyLuong);

                                            uow.CommitChanges();
                                        }
                                        else
                                        {
                                            if (log == null)
                                            {
                                                log = new StringBuilder();
                                                log.AppendLine("Danh sách cán bộ không import được dữ liệu từ file excel vào phần mềm:");
                                            }
                                            log.AppendLine(String.Format(" - {0} - {1} - {2}", item[0], item[1], item[2]));
                                        }
                                    }

                                    if (log != null && log.Length > 0)
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
                                        XtraMessageBox.Show("Import dữ liệu từ file excel thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                else
                                    MessageBox.Show("Không có dữ liệu hoặc không có sheet tên là Sheet1", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            else
                HamDungChung.ShowWarningMessage("Bảng truy lĩnh đã được lập chứng từ chuyển khoản");
        }

    }
}
