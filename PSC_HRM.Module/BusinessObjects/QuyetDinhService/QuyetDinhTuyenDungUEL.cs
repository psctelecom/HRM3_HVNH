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
    public class QuyetDinhTuyenDungUEL : IQuyetDinhTuyenDungService
    {
        public void Save(Session session, ChiTietQuyetDinhTuyenDung obj)
        {
            if (!obj.IsDeleted
                && obj.Oid != Guid.Empty)
            {
                if (obj.QuyetDinhTuyenDung.QuyetDinhMoi)
                {
                    //cập nhật thông tin lương
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.PhanLoai = ThongTinLuongEnum.LuongHeSo;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan = 0;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = obj.NgachLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = obj.BacLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong = obj.HeSoLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = obj.NgayHuongLuong;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = DateTime.MinValue;
                    obj.ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = obj.Huong85PhanTramLuong;

                    //bien dong tang lao dong
                    BienDongHelper.CreateBienDongTangLaoDong(session, obj.QuyetDinhTuyenDung, obj.BoPhan, obj.ThongTinNhanVien, obj.QuyetDinhTuyenDung.NgayPhatSinhBienDong);
                }

                //update dien bien luong
                QuaTrinhHelper.CreateDienBienLuong(session,obj.QuyetDinhTuyenDung, obj.ThongTinNhanVien, obj.NgayHuongLuong, obj);

                //Bảo hiểm xã hội
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                    obj.ThongTinNhanVien);
                HoSoBaoHiem hoSoBaoHiem = session.FindObject<HoSoBaoHiem>(filter);
                if (hoSoBaoHiem != null && obj.NgayHuongLuong != DateTime.MinValue)
                {
                    QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(session, obj.QuyetDinhTuyenDung, hoSoBaoHiem, obj.NgayHuongLuong);
                }
            }
        }

        public void Delete(Session session, ChiTietQuyetDinhTuyenDung obj)
        {
            //reset data
            obj.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = null;
            obj.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = null;
            obj.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong = 0;
            obj.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = DateTime.MinValue;

            //xóa diễn biến lương
            QuaTrinhHelper.DeleteQuaTrinh<DienBienLuong>(session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", obj.QuyetDinhTuyenDung.Oid, obj.ThongTinNhanVien.Oid));

            //xóa quá trình tham gia BHXH
            QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhThamGiaBHXH>(session, CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=?", obj.ThongTinNhanVien.Oid));

            //xóa biến động
            if (obj.QuyetDinhTuyenDung.NgayPhatSinhBienDong != DateTime.MinValue)
                BienDongHelper.DeleteBienDong<BienDong_TangLaoDong>(session, obj.ThongTinNhanVien, obj.QuyetDinhTuyenDung.NgayPhatSinhBienDong);
        }
    }
}
