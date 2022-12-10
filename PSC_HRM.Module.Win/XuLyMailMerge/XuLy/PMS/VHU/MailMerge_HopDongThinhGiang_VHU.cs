using DevExpress.Data.Filtering;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.MailMerge.HopDong;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using System.Data;
using System.Data.SqlClient;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_HopDongThinhGiang_VHU : IMailMerge<IList<PMS_MailMegre_HopDongThinhGiang_ThongTinChung>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<PMS_MailMegre_HopDongThinhGiang_ThongTinChung> qdList)
        {
            var list = new List<PMS_MailMegre_HopDongThinhGiang_ThongTinChung>();
            PMS_MailMegre_HopDongThinhGiang_ThongTinChung qd =  null;
            PMS_MailMegre_HopDongThinhGiang_ThongTinDetail detail = null;
            PMS_MailMegre_HopDongThinhGiang_ThongTinMaster master = null;
            foreach (PMS_MailMegre_HopDongThinhGiang_ThongTinChung quyetDinh in qdList)
            {
                qd = new PMS_MailMegre_HopDongThinhGiang_ThongTinChung();
                qd.Oid = quyetDinh.Oid;
                qd.MaNhanVien = quyetDinh.MaNhanVien;
                qd.HoTen = quyetDinh.HoTen;
                qd.ChucDanh = quyetDinh.ChucDanh;
                qd.HocHam = quyetDinh.HocHam;
                qd.HocVi = quyetDinh.HocVi;
                qd.LoaiGV = quyetDinh.LoaiGV;
                qd.MaSoThueGV = quyetDinh.MaSoThueGV;
                qd.SoTaiKhoan = quyetDinh.SoTaiKhoan;
                qd.TenNganHang = quyetDinh.TenNganHang;
                qd.TenBoPhan = quyetDinh.TenBoPhan;
                qd.TenNamHoc = quyetDinh.TenNamHoc;
                qd.TenHocKy = quyetDinh.TenHocKy;
                qd.DienThoaiDiDong = quyetDinh.DienThoaiDiDong;
                qd.Email = quyetDinh.Email;
                qd.TenDaiDien1 = quyetDinh.TenDaiDien1;
                qd.NhanDaiDien1 = quyetDinh.NhanDaiDien1;
                qd.TenDaiDien2 = quyetDinh.TenDaiDien2;
                qd.NhanDaiDien2 = quyetDinh.NhanDaiDien2;
                qd.FullDiaChi = quyetDinh.FullDiaChi;
                qd.MaSoThue = quyetDinh.MaSoThue;
                qd.DienThoai = quyetDinh.DienThoai;
                qd.Fax = quyetDinh.Fax;
                qd.NoiLamViec = quyetDinh.NoiLamViec;
                qd.ChuyenNganh = quyetDinh.ChuyenNganh;

                master = new PMS_MailMegre_HopDongThinhGiang_ThongTinMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.MaNhanVien = quyetDinh.MaNhanVien;
                master.HoTen = quyetDinh.MaNhanVien;
                qd.Master.Add(master);

                int stt = 1;
                foreach (PMS_MailMegre_HopDongThinhGiang_ThongTinDetail item in quyetDinh.Detail)
                {
                    detail = new PMS_MailMegre_HopDongThinhGiang_ThongTinDetail();
                    detail.Oid = quyetDinh.Oid;
                    detail.STT = item.STT;
                    detail.TenHocPhan = item.TenHocPhan;
                    detail.SoTietLT = item.SoTietLT;
                    detail.SoTietTH = item.SoTietTH;
                    detail.SoTietKhac = item.SoTietKhac;
                    detail.SiSo = item.SiSo;
                    detail.DonGia = item.DonGia;
                    detail.DiaDiemDay = item.DiaDiemDay;
                    detail.SoTinChi = item.SoTinChi;
                    detail.TietQuyDoi = item.TietQuyDoi;
                    detail.DonGiaKhac = item.DonGiaKhac;
                    qd.Detail.Add(detail);
                    stt++;
                }
              

                list.Add(qd);
            }
            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[0] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "HopDongThinhGiang_VHUMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "HopDongThinhGiang_VHUDetail.rtf")); ;
            merge[1] = HamDungChung.GetTemplate(obs, "HopDongThinhGiang_VHU.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<PMS_MailMegre_HopDongThinhGiang_ThongTinChung>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng.");
        }      
    }
}
