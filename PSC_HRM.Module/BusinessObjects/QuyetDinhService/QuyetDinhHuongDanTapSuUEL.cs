using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.GiayTo;
using DevExpress.Xpo.DB;

namespace PSC_HRM.Module.QuyetDinhService
{
    public class QuyetDinhHuongDanTapSuUEL : IQuyetDinhHuongDanTapSuService
    {
        public void Save(Session session, ChiTietQuyetDinhHuongDanTapSu obj)
        {
            if (obj.CanBoHuongDan is ThongTinNhanVien)
            {
                if (!obj.IsDeleted &&
                    obj.Oid != Guid.Empty)
                {
                    if (obj.QuyetDinhHuongDanTapSu.QuyetDinhMoi)
                    {
                        //1. cập nhật thông tin trạng thái cho cán bộ tập sự
                        obj.ThongTinNhanVien.LoaiNhanVien = HoSoHelper.TapSu(session);
                    }

                    //2. create dien bien luong can bo huong dan
                    QuaTrinhHelper.CreateDienBienLuong(session, obj.QuyetDinhHuongDanTapSu, (ThongTinNhanVien)obj.CanBoHuongDan, obj.QuyetDinhHuongDanTapSu.NgayHieuLuc, null);

                    //3. luu tro giay to ho so can bo huong dan
                    GiayToHoSoHelper.CreateGiayToQuyetDinh(session, obj.QuyetDinhHuongDanTapSu.SoQuyetDinh, (ThongTinNhanVien)obj.CanBoHuongDan, obj.QuyetDinhHuongDanTapSu.NgayHieuLuc, obj.GiayToHoSo.LuuTru, obj.QuyetDinhHuongDanTapSu.NoiDung);
                }
            }
        }

        public void Delete(Session session, ChiTietQuyetDinhHuongDanTapSu obj)
        {
            if (obj.CanBoHuongDan is ThongTinNhanVien)
            {
                if (obj.CanBoHuongDan != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("ListChiTietQuyetDinhHuongDanTapSu[CanBoHuongDan=?]",
                        obj.CanBoHuongDan.Oid);
                    if (obj.QuyetDinhHuongDanTapSu.QuyetDinhMoi)
                    {
                        SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                        using (XPCollection<QuyetDinhHuongDanTapSu> qdList = new XPCollection<QuyetDinhHuongDanTapSu>(session, filter, sort))
                        {
                            qdList.TopReturnedObjects = 1;
                            if (qdList.Count == 1 && qdList[0] == obj.QuyetDinhHuongDanTapSu)
                            {
                                obj.CanBoHuongDan.NhanVienThongTinLuong.HSPCTrachNhiem = 0;
                            }
                        }
                    }
                    //xoa dien bien luong
                    QuaTrinhHelper.DeleteQuaTrinh<DienBienLuong>(session, CriteriaOperator.Parse("ThongTinNhanVien=?", obj.CanBoHuongDan.Oid));

                    //xoa giay to ho so can bo huong dan
                    GiayToHoSoHelper.DeleteGiayToHoSo(session, (ThongTinNhanVien)obj.CanBoHuongDan, obj.QuyetDinhHuongDanTapSu.SoQuyetDinh);
                }

                if (obj.GiayToHoSo != null)
                {
                    session.Delete(obj.GiayToHoSo);
                    session.Save(obj.GiayToHoSo);
                }
            }
        }
    }
}
