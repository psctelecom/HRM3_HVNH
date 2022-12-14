using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.PMS.NonPersistentObjects;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_HopDongThanhToanKLGDThinhGiangController : ViewController
    {
        public MailMerge_HopDongThanhToanKLGDThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<PMS_MailMegre_HopDongThinhGiang_ThongTinChung>();
            PMS_MailMegre_HopDongThinhGiang_ThongTinChung qd = null;

            string query = "";
            object kq;
            PMS_ThanhToanThuLaoGiangDay_HopDong_NonBaoCao _HoatDong = View.CurrentObject as PMS_ThanhToanThuLaoGiangDay_HopDong_NonBaoCao;
            if (_HoatDong != null)
            {
                foreach (var chiTiet in _HoatDong.ListDanhSach)
                {
                    if (chiTiet.Chon)
                    {
                        query += " UNION ALL " + "SELECT '"
                                               + chiTiet.MaGV.Replace("'", "''") + "' as MaNhanVien,N'"
                                               + chiTiet.HoTen.Replace("'", "''") + "' as HoTen,N'"
                                               + chiTiet.ChucDanh.Replace("'", "''") + "' as ChucDanh,N'"
                                               + chiTiet.HocHam.Replace("'", "''") + "' as HocHam,N'"
                                               + chiTiet.HocVi.Replace("'", "''") + "' as HocVi,N'"
                                               + chiTiet.LoaiGV.Replace("'", "''") + "' as LoaiGV,N'"
                                               + chiTiet.LoaiHocPhan.Replace("'", "''") + "' as LoaiHocPhan,N'"
                                               + chiTiet.MaHocPhan.Replace("'", "''") + "' as MaHocPhan,N'"
                                               + chiTiet.TenHocPhan.Replace("'", "''") + "' as TenHocPhan,CAST("
                                               + chiTiet.SoTietLT.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietLT, CAST("
                                               + chiTiet.SoTietTH.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietTH, CAST("
                                               + chiTiet.SoTietKhac.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietKhac, CAST("
                                               + chiTiet.SiSo.ToString().Replace(",", ".") + " AS INT) as SiSo, CAST("
                                               + chiTiet.SoTinhChi.ToString().Replace(",", ".") + " AS INT) as SoTC, CAST("
                                               + chiTiet.SoTietThucDay.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietThucDay, CAST("
                                               + chiTiet.TietQuyDoi.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as TietQuyDoi, CAST("
                                               + chiTiet.DonGia.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as DonGia, CAST("
                                               + chiTiet.ThanhTienChiTiet.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as ThanhTienChiTiet, N'"
                                               + chiTiet.TenBoPhan.Replace("'", "''") + "' as TenBoPhan, N'"
                                               + chiTiet.TenNamhoc.Replace("'", "''") + "' as TenNamHoc, N'"
                                               + chiTiet.TenHocKy.Replace("'", "''") + "' as TenHocKy, N'"
                                               + chiTiet.ThoiGiangDay.Replace("'", "''") + "' as ThoiGiangDay, N'"
                                               + chiTiet.DiaDiemDay.Replace("'", "''") + "' as DiaDiemDay";
                    }
                }

                if (query == "")
                {
                    MessageBox.Show("Vui lòng chọn dòng cần xác nhận!");
                }
                else
                {
                    //SqlCommand cmd = new SqlCommand("spd_Report_ThanhToanThuLaoGiangDay_ChayStore_HopDong_Megre");
                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Sql", query.Substring(11).ToString());
                    //cmd.Parameters.AddWithValue("@User", HamDungChung.CurrentUser().Oid);
                    //DataSet ds = DataProvider.GetDataSet(cmd);
                    //if (ds != null)
                    //{
                    //    DataTable dt = ds.Tables[0];
                    //    if(dt != null)
                    //    {
                    //        foreach (DataRow item in dt.Rows)
                    //        {
                    //            qd = new PMS_MailMegre_HopDongThinhGiang_ThongTinChung();
                    //            qd.MaNhanVien = item["MaNhanVien"].ToString();
                    //            qd.HoTen = item["HoTen"].ToString();
                    //            qd.ChucDanh = item["ChucDanh"].ToString();
                    //            qd.HocHam = item["HocHam"].ToString();
                    //            qd.HocVi = item["HocVi"].ToString();
                    //            qd.LoaiGV = item["LoaiGV"].ToString();
                    //            qd.MaSoThueGV = item["MaSoThueGV"].ToString();
                    //            qd.SoTaiKhoan = item["SoTaiKhoan"].ToString();
                    //            qd.TenNganHang = item["TenNganHang"].ToString();
                    //            qd.TenBoPhan = item["TenBoPhan"].ToString();
                    //            qd.TenNamHoc = item["TenNamHoc"].ToString();
                    //            qd.TenHocKy = item["TenHocKy"].ToString();
                    //            qd.DienThoaiDiDong = item["DienThoaiDiDong"].ToString();
                    //            qd.Email = item["Email"].ToString();
                    //            qd.TenDaiDien1 = item["TenDaiDien1"].ToString();
                    //            qd.NhanDaiDien1 = item["NhanDaiDien1"].ToString();
                    //            qd.TenDaiDien2 = item["TenDaiDien2"].ToString();
                    //            qd.NhanDaiDien2 = item["NhanDaiDien2"].ToString();
                    //            qd.FullDiaChi = item["FullDiaChi"].ToString();
                    //            qd.MaSoThue = item["MaSoThue"].ToString();
                    //            qd.DienThoai = item["DienThoai"].ToString();
                    //            qd.Fax = item["Fax"].ToString();
                    //            list.Add(qd);
                    //        }
                    //    }

                    Guid Oid = Guid.NewGuid();
                    SqlCommand cmd = new SqlCommand("spd_Report_ThanhToanThuLaoGiangDay_ChayStore_HopDong_Megre");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Sql", query.Substring(11).ToString());
                    cmd.Parameters.AddWithValue("@User", HamDungChung.CurrentUser().Oid);
                    DataSet ds = DataProvider.GetDataSet(cmd);
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables[0];
                        DataTable dt1 = ds.Tables[1];
                        if (dt != null)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                qd = new PMS_MailMegre_HopDongThinhGiang_ThongTinChung();
                                qd.Oid = Oid.ToString();
                                qd.MaNhanVien = item["MaNhanVien"].ToString();
                                qd.HoTen = item["HoTen"].ToString();
                                qd.ChucDanh = item["ChucDanh"].ToString();
                                qd.HocHam = item["HocHam"].ToString();
                                qd.HocVi = item["HocVi"].ToString();
                                qd.LoaiGV = item["LoaiGV"].ToString();
                                qd.MaSoThueGV = item["MaSoThueGV"].ToString();
                                qd.SoTaiKhoan = item["SoTaiKhoan"].ToString();
                                qd.TenNganHang = item["TenNganHang"].ToString();
                                qd.TenBoPhan = item["TenBoPhan"].ToString();
                                qd.TenNamHoc = item["TenNamHoc"].ToString();
                                qd.TenHocKy = item["TenHocKy"].ToString();
                                qd.DienThoaiDiDong = item["DienThoaiDiDong"].ToString();
                                qd.Email = item["Email"].ToString();
                                qd.TenDaiDien1 = item["TenDaiDien1"].ToString();
                                qd.NhanDaiDien1 = item["NhanDaiDien1"].ToString();
                                qd.TenDaiDien2 = item["TenDaiDien2"].ToString();
                                qd.NhanDaiDien2 = item["NhanDaiDien2"].ToString();
                                qd.FullDiaChi = item["FullDiaChi"].ToString();
                                qd.MaSoThue = item["MaSoThue"].ToString();
                                qd.DienThoai = item["DienThoai"].ToString();
                                qd.Fax = item["Fax"].ToString();
                                qd.NoiLamViec = item["NoiLamViec"].ToString();
                                qd.ChuyenNganh = item["ChuyenNganh"].ToString();

                                PMS_MailMegre_HopDongThinhGiang_ThongTinMaster master = new PMS_MailMegre_HopDongThinhGiang_ThongTinMaster();
                                master.Oid = Oid.ToString();
                                master.MaNhanVien = item["MaNhanVien"].ToString();
                                master.HoTen = item["HoTen"].ToString();
                                qd.Master.Add(master);
                            }
                        }
                        if (dt1 != null)
                        {
                            int stt = 1;
                            foreach (DataRow item in dt1.Rows)
                            {
                                //detail 
                                PMS_MailMegre_HopDongThinhGiang_ThongTinDetail detail = new PMS_MailMegre_HopDongThinhGiang_ThongTinDetail();
                                detail.Oid = Oid.ToString();
                                detail.STT = stt.ToString();
                                detail.TenHocPhan = item["TenHocPhan"].ToString();
                                detail.SoTietLT = item["SoTietLT"].ToString();
                                detail.SoTietTH = item["SoTietTH"].ToString();
                                detail.SoTietKhac = item["SoTietKhac"].ToString();
                                detail.SiSo = item["SiSo"].ToString();
                                detail.SoTinChi = item["SoTC"].ToString();
                                detail.TietQuyDoi = item["TietQuyDoi"].ToString();
                                detail.DonGia = item["DonGia"].ToString();
                                detail.DiaDiemDay = item["DiaDiemDay"].ToString();
                                detail.DonGiaKhac = item["DonGiaKhac"].ToString();
                                qd.Detail.Add(detail);
                                stt++;
                            }
                        }
                        list.Add(qd);
                    }
                }

                SystemContainer.Resolver<MailMerge_HopDongThinhGiang_VHU>().Merge(Application.CreateObjectSpace(), list);
            }
        }
    }
}
