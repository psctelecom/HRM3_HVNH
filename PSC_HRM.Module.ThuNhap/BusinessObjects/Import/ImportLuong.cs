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
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    [ModelDefault("Caption", "Import lương")]
    public class ImportLuong : ImportBase
    {
        public ImportLuong(Session session)
            : base(session)
        { }

        public override void XuLy(IObjectSpace obs, object obj)
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
                                BangLuongNhanVien bangLuong = obj as BangLuongNhanVien;
                                //xoa du lieu cu
                                uow.Delete(bangLuong.ListLuongNhanVien);
                                uow.CommitChanges();

                                ThongTinNhanVien nhanVien;
                                LuongNhanVien luong = null;
                                ChiTietLuongNhanVien chiTiet = null;
                                StringBuilder log = null;
                                decimal temp;

                                foreach (DataRow item in dt.Rows)
                                {
                                    nhanVien = GetNhanVien(uow, item[0].ToString(),string.Empty);
                                    if (nhanVien != null)
                                    {
                                        luong = uow.FindObject<LuongNhanVien>(CriteriaOperator.Parse("BangLuongNhanVien=? and ThongTinNhanVien=?", 
                                            bangLuong.Oid, nhanVien.Oid));
                                        if (luong == null)
                                        {
                                            luong = new LuongNhanVien(uow);
                                            luong.BangLuongNhanVien = uow.GetObjectByKey<BangLuongNhanVien>(bangLuong.Oid);
                                            luong.BoPhan = nhanVien.BoPhan;
                                            luong.ThongTinNhanVien = nhanVien;
                                            //luong.ThongTinLuong = HamDungChung.Copy<NhanVienThongTinLuong>(uow, nhanVien.NhanVienThongTinLuong);
                                        }

                                        if (item.ItemArray.Length > 4)
                                        {
                                            ChiTietCongThucTinhLuong congThuc;
                                            for (int i = 4; i < item.ItemArray.Length; i++)
                                            {
                                                congThuc = uow.FindObject<ChiTietCongThucTinhLuong>(CriteriaOperator.Parse("MaChiTiet=?", 
                                                    dt.Columns[i].ColumnName));
                                                if (congThuc != null)
                                                {
                                                    chiTiet = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", 
                                                        luong.Oid, dt.Columns[i].ColumnName));
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietLuongNhanVien(uow);
                                                        chiTiet.MaChiTiet = dt.Columns[i].ColumnName;
                                                        chiTiet.DienGiai = congThuc.DienGiai;
                                                        chiTiet.CongTru = congThuc.CongTru;
                                                        chiTiet.LuongNhanVien = luong;
                                                        chiTiet.CongThucTinhSoTien = congThuc.CongThucTinhSoTien;
                                                    }

                                                    //so tien nhan
                                                    if (decimal.TryParse(item[i].ToString(), out temp))
                                                        chiTiet.SoTien = temp;

                                                    //so tien chiu thue TNCN
                                                    if (congThuc.TinhTNCT)
                                                    {
                                                        chiTiet.SoTienChiuThue = chiTiet.SoTien;
                                                        chiTiet.CongThucTinhTNCT = congThuc.CongThucTinhSoTien;
                                                    }
                                                    else
                                                    {
                                                        //do chuyển tính tiền xuống database nên chỗ này không tính được thu nhập chịu thuế,
                                                        //tạm thời set = 0
                                                        chiTiet.SoTienChiuThue = 0;
                                                        chiTiet.CongThucTinhTNCT = congThuc.CongThucTinhTNCT;
                                                    }

                                                    if (chiTiet.SoTien <= 0)
                                                        uow.Delete(chiTiet);
                                                }
                                            }
                                            luong.XuLy();                                            
                                        }
                                        if (luong.ThucLanh <= 0)
                                            uow.Delete(luong);

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
                            else
                                HamDungChung.ShowWarningMessage("Không có dữ liệu hoặc không có sheet tên là Sheet1");
                        }
                    }
                }
            }
        }
    }
}
