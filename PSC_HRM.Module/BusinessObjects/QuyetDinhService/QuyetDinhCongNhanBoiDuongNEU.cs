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
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinhService
{
    public class QuyetDinhCongNhanBoiDuongNEU : IQuyetDinhCongNhanBoiDuongService
    {
       
        public void Save(Session session, ChiTietCongNhanBoiDuong obj)
        {
            if (!obj.IsDeleted
                && obj.Oid != Guid.Empty)
            {
                //1. tình trạng
                if (obj.QuyetDinhCongNhanBoiDuong.TuNgay <= HamDungChung.GetServerTime())
                    obj.ThongTinNhanVien.TinhTrang = HoSoHelper.DangLamViec(session);
            }
        }

        public void Delete(Session session, ChiTietCongNhanBoiDuong obj)
        {
            //1. Tinh trang
            obj.ThongTinNhanVien.TinhTrang = obj.TinhTrang;
        }
    }
}
