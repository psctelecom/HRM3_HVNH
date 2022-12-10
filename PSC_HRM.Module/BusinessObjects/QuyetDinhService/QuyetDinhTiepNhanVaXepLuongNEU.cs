using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.GiayTo;

namespace PSC_HRM.Module.QuyetDinhService
{
    public class QuyetDinhTiepNhanVaXepLuongNEU : IQuyetDinhTiepNhanVaXepLuongService
    {
        public void Save(Session session, QuyetDinhTiepNhanVaXepLuong obj)
        {
            if (!obj.IsDeleted
                && obj.ThongTinNhanVien != null)
            {
                CriteriaOperator filter;
                if (obj.QuyetDinhMoi)
                {
                    //thiết lập đơn vị
                    obj.ThongTinNhanVien.BoPhan = obj.BoPhanMoi;
                    obj.ThongTinNhanVien.NgayVaoCoQuan = obj.TuNgay;

                    //cập nhật thông tin vào hồ sơ
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = obj.NgachLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = obj.BacLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong = obj.HeSoLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung = obj.VuotKhung;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = obj.MocNangLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = obj.NgayHuongLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = obj.Huong85PhanTramLuong;
                 
                    //tăng lao động
                    filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                        obj.ThongTinNhanVien);
                    HoSoBaoHiem hoSoBaoHiem = session.FindObject<HoSoBaoHiem>(filter);
                    if (hoSoBaoHiem != null
                        && obj.NgayPhatSinhBienDong != DateTime.MinValue)
                    {
                        BienDongHelper.CreateBienDongTangLaoDong(session, obj, obj.NgayPhatSinhBienDong);
                    }
                }

                //luu tru giay to ho so can bo huong dan
                obj.GiayToHoSo.QuyetDinh = obj;
                obj.GiayToHoSo.NgayBanHanh = obj.NgayHieuLuc;
                obj.GiayToHoSo.SoGiayTo = obj.SoQuyetDinh;
                obj.GiayToHoSo.NgayLap = obj.NgayQuyetDinh;
                obj.GiayToHoSo.TrichYeu = obj.NoiDung;
            }
        }

        public void Delete(Session session, QuyetDinhTiepNhanVaXepLuong obj)
        {
            if (obj.ThongTinNhanVien != null)
            {
                if (obj.QuyetDinhMoi && obj.NgayPhatSinhBienDong != DateTime.MinValue)
                    BienDongHelper.DeleteBienDong<BienDong_TangLaoDong>(session, obj.ThongTinNhanVien, obj.NgayPhatSinhBienDong);

                //xoa giay to
                if (!String.IsNullOrWhiteSpace(obj.SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(session, obj.ThongTinNhanVien, obj.SoQuyetDinh);
            }
        }
    }
}
