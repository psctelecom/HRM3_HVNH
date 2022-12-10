using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.GiayTo;

namespace PSC_HRM.Module.QuyetDinhService
{
    public class QuyetDinhTiepNhanUEL : IQuyetDinhTiepNhanService
    {
        public void Save(Session session, QuyetDinhTiepNhan obj)
        {
            if (!obj.IsDeleted && obj.ThongTinNhanVien != null)
            {
                CriteriaOperator filter; 
                if (obj.QuyetDinhNghiKhongHuongLuong.TinhTrang != null)
                {
                    obj.ThongTinNhanVien.TinhTrang = obj.QuyetDinhNghiKhongHuongLuong.TinhTrang;
                }
                else
                {
                    filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc");
                    if (obj.QuyetDinhMoi)
                    {
                        //thiết lập tình trạng
                        if (obj.TuNgay <= HamDungChung.GetServerTime())
                        {
                            TinhTrang tinhTrang = session.FindObject<TinhTrang>(filter);
                            if (tinhTrang == null)
                            {
                                tinhTrang = new TinhTrang(session);
                                tinhTrang.MaQuanLy = tinhTrang.TenTinhTrang = "Đang làm việc";
                            }
                            obj.ThongTinNhanVien.TinhTrang = tinhTrang;
                        }
                    }
                }
                

                filter = CriteriaOperator.Parse("ThongTinNhanVien=?", obj.ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayHieuLuc", DevExpress.Xpo.DB.SortingDirection.Descending);
                using (XPCollection<QuyetDinhNghiKhongHuongLuong> qdList = new XPCollection<QuyetDinhNghiKhongHuongLuong>(session, filter, sort))
                {
                    qdList.TopReturnedObjects = 1;
                    if (qdList.Count == 1 && !qdList[0].CoDongBaoHiem)
                    {
                        //tăng lao động
                        HoSoBaoHiem hoSoBaoHiem = session.FindObject<HoSoBaoHiem>(filter);
                        if (hoSoBaoHiem != null &&
                            obj.NgayPhatSinhBienDong != DateTime.MinValue)
                        {
                            BienDongHelper.CreateBienDongTangLaoDong(session, obj, obj.NgayPhatSinhBienDong);
                        }
                    }
                }

                //luu tru giay to ho so can bo huong dan
                obj.GiayToHoSo.NgayBanHanh = obj.NgayHieuLuc;
                obj.GiayToHoSo.SoGiayTo = obj.SoQuyetDinh;
                obj.GiayToHoSo.NgayBanHanh = obj.NgayHieuLuc;
                obj.GiayToHoSo.TrichYeu = obj.NoiDung;
            }
        }

        public void Delete(Session session, QuyetDinhTiepNhan obj)
        {
            if (obj.ThongTinNhanVien != null)
            {
                if (obj.QuyetDinhMoi)
                    obj.ThongTinNhanVien.TinhTrang = obj.TinhTrangCu;

                //xóa biến động
                if (obj.NgayPhatSinhBienDong != DateTime.MinValue)
                    BienDongHelper.DeleteBienDong<BienDong_TangLaoDong>(session, obj.ThongTinNhanVien, obj.NgayPhatSinhBienDong);

                //xoa giay to
                if (!String.IsNullOrWhiteSpace(obj.SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(session, obj.ThongTinNhanVien, obj.SoQuyetDinh);
            }
        }
    }
}
