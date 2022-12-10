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
    public class QuyetDinhCongNhanDaoTaoUEL : IQuyetDinhCongNhanDaoTaoService
    {
        //Không tạo văn bằng khi công nhận đào tạo
        public void Save(Session session, ChiTietCongNhanDaoTao obj)
        {
            if (!obj.IsDeleted
                && obj.Oid != Guid.Empty)
            {
                //1. tình trạng
                if (obj.QuyetDinhCongNhanDaoTao.TuNgay <= HamDungChung.GetServerTime())
                    obj.ThongTinNhanVien.TinhTrang = HoSoHelper.DangLamViec(session);
            }
        }

        public void Delete(Session session, ChiTietCongNhanDaoTao obj)
        {
            //1. Tinh trang
            obj.ThongTinNhanVien.TinhTrang = obj.TinhTrang;
        }
    }
}
