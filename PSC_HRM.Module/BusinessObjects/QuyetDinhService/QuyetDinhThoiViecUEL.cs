using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.ThoiViec;

namespace PSC_HRM.Module.QuyetDinhService
{
    public class QuyetDinhThoiViecUEL : IQuyetDinhThoiViecService
    {
        public void Save(Session session, QuyetDinhThoiViec obj)
        {
            if (!obj.IsDeleted && obj.ThongTinNhanVien != null)
            {
                CriteriaOperator filter;
                TinhTrang tinhtrang;
                obj.ThongTinNhanVien.NgayNghiViec = obj.NghiViecTuNgay;
                //
                if (obj.NghiViecTuNgay <= HamDungChung.GetServerTime())
                {
                    if (obj.HinhThucThoiViec.TenLoaiQuyetDinh.ToLower().Contains("nghỉ hưu"))
                    {
                        filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Nghỉ hưu");
                        tinhtrang = session.FindObject<TinhTrang>(filter);

                        if (tinhtrang == null)
                        {
                            tinhtrang = new TinhTrang(session);
                            tinhtrang.TenTinhTrang = "Nghỉ hưu";
                            tinhtrang.MaQuanLy = "NH";
                        }
                    }
                    else
                    {
                        filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Nghỉ việc");
                        tinhtrang = session.FindObject<TinhTrang>(filter);

                        if (tinhtrang == null)
                        {
                            tinhtrang = new TinhTrang(session);
                            tinhtrang.TenTinhTrang = "Nghỉ việc";
                            tinhtrang.MaQuanLy = "NV";
                        }
                    }
                    obj.ThongTinNhanVien.TinhTrang = tinhtrang;
                }

                //quản lý biến động
                //giảm lao động
                filter = CriteriaOperator.Parse("ThongTinNhanVien=?", obj.ThongTinNhanVien);
                HoSoBaoHiem hoSoBaoHiem = session.FindObject<HoSoBaoHiem>(filter);
                if (hoSoBaoHiem != null
                    && obj.NgayPhatSinhBienDong != DateTime.MinValue)
                {
                    BienDongHelper.SetTrangThaiThamGiaBHXH(session, obj, LyDoNghiEnum.ThoiViec);
                    BienDongHelper.CreateBienDongGiamLaoDong(session, obj, obj.NgayPhatSinhBienDong, LyDoNghiEnum.ThoiViec);
                }

                //quản lý thôi việc
                ThoiViecHelper.CreateThoiViec(session, obj, obj.LyDo, obj.NghiViecTuNgay);

                //luu tru giay to ho so can bo huong dan
                obj.GiayToHoSo.NgayBanHanh = obj.NgayHieuLuc;
                obj.GiayToHoSo.SoGiayTo = obj.SoQuyetDinh;
                obj.GiayToHoSo.NgayBanHanh = obj.NgayHieuLuc;
                obj.GiayToHoSo.TrichYeu = obj.NoiDung;
            }
        }

        public void Delete(Session session, QuyetDinhThoiViec obj)
        {
            if (obj.ThongTinNhanVien != null)
            {
                //thiết lập tình trạng
                if (obj.TinhTrang != null)
                    obj.ThongTinNhanVien.TinhTrang = obj.TinhTrang;
                else
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("TenTinhTrang like ?", "%đang làm việc%");
                    TinhTrang tinhtrang = session.FindObject<TinhTrang>(filter);
                    if (tinhtrang != null)
                        obj.ThongTinNhanVien.TinhTrang = tinhtrang;
                }
                obj.ThongTinNhanVien.NgayNghiViec = DateTime.MinValue;

                //xóa biến động
                if (obj.NgayPhatSinhBienDong != DateTime.MinValue)
                {
                    BienDongHelper.ResetTrangThaiThamGiaBHXH(session, obj);
                    BienDongHelper.DeleteBienDong<BienDong_GiamLaoDong>(session, obj.ThongTinNhanVien, obj.NgayPhatSinhBienDong);
                }

                //xóa quản lý bổ nhiệm
                ThoiViecHelper.DeleteThoiViec<ChiTietThoiViec>(session, obj);

                //xoa giay to
                if (!String.IsNullOrWhiteSpace(obj.SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(session, obj.ThongTinNhanVien, obj.SoQuyetDinh);
            }
        }
    }
}
