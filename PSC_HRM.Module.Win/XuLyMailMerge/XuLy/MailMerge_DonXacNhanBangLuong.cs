using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.MailMerge;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.MailMerge.ToTrinh;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Xpo;
using System.Globalization;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_DonXacNhanBangLuong : IMailMerge<IList<Luong_DonXacNhanBangLuong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<Luong_DonXacNhanBangLuong> DonXacNhanBangLuongList)
        {
            var list = new List<Non_DonXacNhanBangLuong>();
            Non_DonXacNhanBangLuong qd;

            foreach (Luong_DonXacNhanBangLuong obj in DonXacNhanBangLuongList)
            {
                qd = new Non_DonXacNhanBangLuong();
                qd.Oid = obj.Oid.ToString();

                if (obj.TiengAnh == true)
                {
                    qd.NgayLapDon = obj.NgayLapDon != DateTime.MinValue ? obj.NgayLapDon.ToString("MMMM dd, yyyy", new CultureInfo("en-US")) : "";
                    qd.TuThang = obj.TuThang != DateTime.MinValue ? obj.TuThang.ToString("MM/yyyy") : "";
                    qd.DenThang = obj.DenThang != DateTime.MinValue ? obj.DenThang.ToString("MM/yyyy") : "";

                    if (obj.NguoiKy != null)
                    {
                        qd.NguoiKy = HamDungChung.BoDauTiengViet(obj.NguoiKy.HoTen.ToString());
                        if (obj.NguoiKy.ChucVu != null)
                            qd.ChucVuNguoiKy = obj.NguoiKy.ChucVu.TenChucVu.ToUpper();
                        else
                            qd.ChucVuNguoiKy = "";
                        qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhEnglish(obj.NguoiKy);
                    }
                }
                else
                {
                    qd.NgayLapDon = obj.NgayLapDon != DateTime.MinValue ? obj.NgayLapDon.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.TuThang = obj.TuThang != DateTime.MinValue ? obj.TuThang.ToString("MM/yyyy") : "";
                    qd.DenThang = obj.DenThang != DateTime.MinValue ? obj.DenThang.ToString("MM/yyyy") : "";

                    if (obj.NguoiKy != null)
                    {
                        qd.NguoiKy = obj.NguoiKy.HoTen;
                        if (obj.NguoiKy.ChucVu != null)
                            qd.ChucVuNguoiKy = obj.NguoiKy.ChucVu.TenChucVu.ToUpper();
                        else
                            qd.ChucVuNguoiKy = "";
                        qd.ChucDanhNguoiKy = HamDungChung.GetChucDanh(obj.NguoiKy);
                    } 
                }
                qd.LyDo = obj.LyDo;              

                if (obj.ThongTinNhanVien != null)
                {
                    if (obj.TiengAnh == true)
                    {
                        qd.DanhXungVietThuong = obj.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Mr." : "Ms.";
                        qd.HoTen = HamDungChung.BoDauTiengViet(obj.ThongTinNhanVien.HoTen.ToString());
                        qd.DonVi = obj.ThongTinNhanVien.BoPhan.TenBoPhanENG;
                        qd.NgaySinh = obj.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? obj.ThongTinNhanVien.NgaySinh.ToString("MMMM dd, yyyy", new CultureInfo("en-US")) : "";
                    }
                    else
                    {
                        qd.DanhXungVietThuong = obj.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                        qd.HoTen = obj.ThongTinNhanVien.HoTen.ToString();
                        qd.DonVi = obj.ThongTinNhanVien.BoPhan.TenBoPhan;
                        qd.NgaySinh = obj.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? obj.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy") : "";
                    }
    
                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@ThongTinNhanVien", obj.ThongTinNhanVien.Oid);
                    param[1] = new SqlParameter("@TuThang", obj.TuThang);
                    param[2] = new SqlParameter("@DenThang", obj.DenThang);
                    param[3] = new SqlParameter("@LyDo", obj.LyDo);
                    param[4] = new SqlParameter("@TiengAnh", obj.TiengAnh);
                    //
                    using (DataTable dt = DataProvider.GetDataTable("spd_Report_Luong_DonXinXacNhanBangLuong", CommandType.StoredProcedure, param))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                //qd.HoTen = item["HoTen"].ToString();                                
                                //qd.DonVi = item["TenBoPhan"].ToString();                                
                                //try
                                //{
                                //    DateTime ngaySinh = Convert.ToDateTime(item["NgaySinh"]);
                                //    qd.NgaySinh = ngaySinh.ToString("d");
                                //}
                                //catch { qd.NgaySinh = ""; }
                                qd.MaNgach = item["MaNgach"].ToString();
                                if (!obj.TiengAnh)
                                {
                                    try
                                    {
                                        decimal heSoLuong = Convert.ToDecimal(item["HeSoLuong"]);
                                        qd.HeSoLuong = heSoLuong.ToString("N2");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal hspcChucVu = Convert.ToDecimal(item["HSPCChucVu"]);
                                        qd.HSPCChucVu = hspcChucVu.ToString("N2");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal hspcThamNien = Convert.ToDecimal(item["HSPCThamNien"]);
                                        qd.HSPCThamNien = hspcThamNien.ToString("N2");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal hspcVuotKhung = Convert.ToDecimal(item["HSPCVuotKhung"]);
                                        qd.HSPCVuotKhung = hspcVuotKhung.ToString("N2");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal hspcUuDai = Convert.ToDecimal(item["HSPCUuDai"]);
                                        qd.HSPCUuDai = hspcUuDai.ToString("N2");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal baoHiem = Convert.ToDecimal(item["BaoHiem"]);
                                        qd.BaoHiem = baoHiem.ToString("N0");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal pcAnTrua = Convert.ToDecimal(item["PhuCapTienAn"]);
                                        qd.PhuCapAnTrua = pcAnTrua.ToString("N0");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal pcQuanLy = Convert.ToDecimal(item["PhuCapQuanLy"]);
                                        qd.PhuCapQuanLy = pcQuanLy.ToString("N0");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal tnTangThem = Convert.ToDecimal(item["ThuNhapTangThem"]);
                                        qd.ThuNhapTangThem = tnTangThem.ToString("N0");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal thuclanh = Convert.ToDecimal(item["ThucLanh"].ToString());
                                        qd.TongThucLinh = thuclanh.ToString("N0");                                      
                                        qd.TienBangChu = HamDungChung.DocTien(thuclanh);
                                    }
                                    catch { }
                                }
                                else
                                {
                                    try
                                    {
                                        decimal heSoLuong = Convert.ToDecimal(item["HeSoLuong"]);
                                        qd.HeSoLuong = heSoLuong.ToString("N2").Replace(",",".");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal hspcChucVu = Convert.ToDecimal(item["HSPCChucVu"]);
                                        qd.HSPCChucVu = hspcChucVu.ToString("N2").Replace(",", ".");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal hspcThamNien = Convert.ToDecimal(item["HSPCThamNien"]);
                                        qd.HSPCThamNien = hspcThamNien.ToString("N2").Replace(",", ".");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal hspcVuotKhung = Convert.ToDecimal(item["HSPCVuotKhung"]);
                                        qd.HSPCVuotKhung = hspcVuotKhung.ToString("N2").Replace(",", ".");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal hspcUuDai = Convert.ToDecimal(item["HSPCUuDai"]);
                                        qd.HSPCUuDai = hspcUuDai.ToString("N2").Replace(",", ".");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal baoHiem = Convert.ToDecimal(item["BaoHiem"]);
                                        qd.BaoHiem = baoHiem.ToString("N0").Replace(".", ",");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal pcAnTrua = Convert.ToDecimal(item["PhuCapTienAn"]);
                                        qd.PhuCapAnTrua = pcAnTrua.ToString("N0").Replace(".", ",");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal pcQuanLy = Convert.ToDecimal(item["PhuCapQuanLy"]);
                                        qd.PhuCapQuanLy = pcQuanLy.ToString("N0").Replace(".", ",");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal tnTangThem = Convert.ToDecimal(item["ThuNhapTangThem"]);
                                        qd.ThuNhapTangThem = tnTangThem.ToString("N0").Replace(".", ",");
                                    }
                                    catch { }
                                    try
                                    {
                                        decimal thuclanh = Convert.ToDecimal(item["ThucLanh"].ToString());
                                        qd.TongThucLinh = thuclanh.ToString("N0").Replace(".", ",");                                        
                                        qd.TienBangChu = HamDungChung.DocTienEnglish(thuclanh);                                       
                                    }
                                    catch { }
                                }                                                                                              
                            }
                        }
                    }
                }              
                
                list.Add(qd);

                 MailMergeTemplate merge = null;
                if (obj.TiengAnh == true)
                    merge = HamDungChung.GetTemplate(obs, "DonXacNhanBangLuongENG.rtf");
                else
                    merge = HamDungChung.GetTemplate(obs, "DonXacNhanBangLuong.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_DonXacNhanBangLuong>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu Đơn xác nhận bảng lương trong hệ thống.");
            }          
        }
    }
}
