using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.GiayTo;

namespace PSC_HRM.Module.QuyetDinhService
{
    public class QuyetDinhKyLuatUEL : IQuyetDinhKyLuatService
    {
        public void Save(Session session, QuyetDinhKyLuat obj)
        {
            if (!obj.IsDeleted &&
                obj.ThongTinNhanVien != null)
            {
                //xử lý kỷ luật kéo dài mốc nâng lương
                if (obj.QuyetDinhMoi
                    && obj.HinhThucKyLuat != null
                    && obj.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong != DateTime.MinValue)
                {
                    //Kéo dài thời hạn nâng lương 6 tháng 
                    if (obj.HinhThucKyLuat.TenHinhThucKyLuat.ToLower().Contains("Kéo dài thời hạn nâng lương 6 tháng"))
                    {
                        obj.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh = obj.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong.AddMonths(6);
                        obj.ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh = "Bị kỷ luật Kéo dài thời hạn nâng lương 6 tháng theo QĐ " + obj.SoQuyetDinh;
                    }
                }

                CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=?", obj.Oid);

                //quá trình kỷ luật
                QuaTrinhKyLuat quaTrinhKyLuat = session.FindObject<QuaTrinhKyLuat>(filter);
                if (quaTrinhKyLuat == null)
                {
                    quaTrinhKyLuat = new QuaTrinhKyLuat(session);
                    quaTrinhKyLuat.ThongTinNhanVien = obj.ThongTinNhanVien;
                }
                quaTrinhKyLuat.HinhThucKyLuat = obj.HinhThucKyLuat;
                quaTrinhKyLuat.TuNgay = obj.TuNgay;
                quaTrinhKyLuat.DenNgay = obj.DenNgay;
                quaTrinhKyLuat.LyDo = obj.LyDo;

                //luu tru giay to ho so can bo huong dan
                obj.GiayToHoSo.NgayBanHanh = obj.NgayHieuLuc;
                obj.GiayToHoSo.SoGiayTo = obj.SoQuyetDinh;
                obj.GiayToHoSo.NgayBanHanh = obj.NgayHieuLuc;
                obj.GiayToHoSo.TrichYeu = obj.NoiDung;
            }
        }

        public void Delete(Session session, QuyetDinhKyLuat obj)
        {
            if (obj.ThongTinNhanVien != null)
            {
                //xóa kéo dài mốc nâng lương
                if (obj.QuyetDinhMoi
                    && obj.HinhThucKyLuat != null
                    && obj.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong != DateTime.MinValue)
                {
                    //Kéo dài thời hạn nâng lương 6 tháng 
                    if (obj.HinhThucKyLuat.TenHinhThucKyLuat.ToLower().Contains("Kéo dài thời hạn nâng lương 6 tháng"))
                    {
                        obj.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh = DateTime.MinValue;
                        obj.ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh = "";
                    }
                }

                //xóa quá trình kỷ luật
                CriteriaOperator criteria = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?",
                    obj.Oid, obj.ThongTinNhanVien);
                QuaTrinhKyLuat quaTrinh = session.FindObject<QuaTrinhKyLuat>(criteria);
                if (quaTrinh != null)
                {
                    session.Delete(quaTrinh);
                    session.Save(quaTrinh);
                }

                //xoa giay to
                if (!String.IsNullOrWhiteSpace(obj.SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(session, obj.ThongTinNhanVien, obj.SoQuyetDinh);
            }
        }
    }
}
