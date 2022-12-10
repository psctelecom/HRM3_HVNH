using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Utils;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PSC_HRM.Module.DanhGia
{
    public class DanhGiaHelper
    {
        /// <summary>
        /// Import vi pham PMS
        /// </summary>
        /// <param name="app"></param>
        /// <param name="bang"></param>
        public static void ImportViPhamPMS(XafApplication app, BangTheoDoiViPham bang)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý", "Vui lòng chờ..."))
            {
                IObjectSpace obs = app.CreateObjectSpace();
                string uis = ConfigurationManager.AppSettings.Get("UIS").ToLower();
                if (uis.Equals("true"))
                {
                    using (SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("PSC_PMS.bin")))
                    {
                        if (cnn.State != ConnectionState.Open)
                            cnn.Open();

                        const string query = "Select MaQuanLy, Ngay, MaHinhThucViPham, Tiet, Lop, LyDo From dbo.ThanhTraGiangDay Where MaHinhThucViPham is not null and Ngay>=@TuNgay and Ngay<=@DenNgay";
                        using (SqlCommand cmd = new SqlCommand(query, cnn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@TuNgay", bang.ThoiGian.SetTime(SetTimeEnum.StartMonth));
                            cmd.Parameters.AddWithValue("@DenNgay", bang.ThoiGian.SetTime(SetTimeEnum.EndMonth));

                            using (DataTable dt = new DataTable())
                            {
                                dt.Load(cmd.ExecuteReader());
                                ChiTietViPham chiTietViPham;
                                ThongTinNhanVien nhanVien;
                                CriteriaOperator filter;
                                HinhThucViPham hinhThucViPham;
                                DateTime ngay;

                                foreach (DataRow row in dt.Rows)
                                {
                                    filter = CriteriaOperator.Parse("SoHieuCongChuc like ?", row["MaQuanLy"]);
                                    nhanVien = obs.FindObject<ThongTinNhanVien>(filter);
                                    if (nhanVien != null)
                                    {
                                        hinhThucViPham = obs.GetObjectByKey<HinhThucViPham>(new Guid(row["MaHinhThucViPham"].ToString()));
                                        if (hinhThucViPham != null && DateTime.TryParse(row["Ngay"].ToString(), out ngay))
                                        {
                                            filter = CriteriaOperator.Parse("BangTheoDoiViPham=? and ThongTinNhanVien=? and Ngay=? and HinhThucViPham=?",
                                                bang.Oid, nhanVien.Oid, ngay, hinhThucViPham.Oid);
                                            chiTietViPham = obs.FindObject<ChiTietViPham>(filter);
                                            if (chiTietViPham == null)
                                            {
                                                chiTietViPham = obs.CreateObject<ChiTietViPham>();
                                                chiTietViPham.BangTheoDoiViPham = obs.GetObjectByKey<BangTheoDoiViPham>(bang.Oid);
                                                chiTietViPham.BoPhan = nhanVien.BoPhan;
                                                chiTietViPham.ThongTinNhanVien = nhanVien;
                                                chiTietViPham.Ngay = ngay;
                                                chiTietViPham.HinhThucViPham = hinhThucViPham;
                                                chiTietViPham.Tiet = row["Tiet"].ToString();
                                                chiTietViPham.Lop = row["Lop"].ToString();
                                                if (!row.IsNull("LyDo"))
                                                    chiTietViPham.GhiChu = row["LyDo"].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            obs.CommitChanges();
                            HamDungChung.ShowSuccessMessage("Lấy danh sách giảng viên vi phạm từ phần mềm PMS thành công.");
                        }
                    }
                }
                else
                    HamDungChung.ShowWarningMessage("Chưa triển khai phần mềm PMS");
            }
        }

        public static void XuLyViPham(XafApplication app, DanhGia_DanhSachDiTreVeSom danhSach, DateTime ngay)
        {
            IObjectSpace obs = app.CreateObjectSpace();
            CriteriaOperator filter = CriteriaOperator.Parse("ThoiGian>=? and ThoiGian<=?",
                ngay.SetTime(SetTimeEnum.StartMonth),
                ngay.SetTime(SetTimeEnum.EndMonth));
            BangTheoDoiViPham bang = obs.FindObject<BangTheoDoiViPham>(filter);
            if (bang == null)
            {
                bang = obs.CreateObject<BangTheoDoiViPham>();
                bang.ThoiGian = ngay;
            }
            foreach (DanhGia_DiTreVeSom item in danhSach.ViPhamList)
            {
                if (item.Chon && item.ThongTinNhanVien != null &&
                    item.HinhThucViPham != null &&
                    item.BoPhan != null)
                {
                    filter = CriteriaOperator.Parse("BangTheoDoiViPham=? and ThongTinNhanVien=? and HinhThucViPham=? and Ngay=?",
                        bang, item.ThongTinNhanVien.Oid,
                        item.HinhThucViPham.Oid, item.Ngay);
                    ChiTietViPham viPham = obs.FindObject<ChiTietViPham>(filter);
                    if (viPham == null)
                    {
                        viPham = obs.CreateObject<ChiTietViPham>();
                        viPham.BangTheoDoiViPham = bang;
                        viPham.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        viPham.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    }
                    viPham.HinhThucViPham = obs.GetObjectByKey<HinhThucViPham>(item.HinhThucViPham.Oid);
                    viPham.Ngay = item.Ngay;
                    viPham.GhiChu = item.GhiChu;
                }
            }
            obs.CommitChanges();
        }
    }
}
