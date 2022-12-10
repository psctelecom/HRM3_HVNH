using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.QuyetDinhService
{
    public class QuyetDinhXepLuongUEL : IQuyetDinhXepLuongService
    {
        public void Save(Session session, QuyetDinhXepLuong obj)
        {
            if (!obj.IsDeleted)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                        obj.ThongTinNhanVien);
                HoSoBaoHiem hoSoBaoHiem = session.FindObject<HoSoBaoHiem>(filter);
                if (obj.QuyetDinhMoi)
                {
                    //chỉ phát sinh biến động khi nhân viên tham gia bảo hiểm xã hội
                    //tăng mức đóng
                    if (hoSoBaoHiem != null &&
                        obj.NgayPhatSinhBienDong != DateTime.MinValue)
                    {
                        BienDongHelper.CreateBienDongThayDoiLuong(session, obj, obj.NgayPhatSinhBienDong, obj.HeSoLuong, 0, obj.VuotKhung, 0, 0, obj.Huong85PhanTramLuong);
                    }

                    //cập nhật thông tin vào hồ sơ
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = obj.NgachLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = obj.BacLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong = obj.HeSoLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung = obj.VuotKhung;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = obj.MocNangLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = obj.Huong85PhanTramLuong;
                }

                //tạo mới diễn biến lương
                QuaTrinhHelper.CreateDienBienLuong(session, obj, obj.ThongTinNhanVien, obj.NgayHieuLuc,null);

                //Bảo hiểm xã hội
                if (hoSoBaoHiem != null &&
                    obj.NgayHuongLuong != DateTime.MinValue)
                {
                    QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(session, hoSoBaoHiem, obj, obj.NgayHuongLuong);
                }
            }
        }

        public void Delete(Session session, QuyetDinhXepLuong obj)
        {
            QuaTrinhHelper.DeleteQuaTrinhNhanVien<DienBienLuong>(session, obj);

            //xóa quá trình bhxh
            QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhThamGiaBHXH>(session, CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?",
                obj.ThongTinNhanVien, obj.NgayHuongLuong));
        }
    }
}
