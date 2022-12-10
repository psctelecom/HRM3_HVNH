using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.QuyetDinhService
{
    public class QuyetDinhTiepNhanVienChucDiNuocNgoaiUEL : IQuyetDinhTiepNhanVienChucDiNuocNgoaiService
    {
        public void Save(Session session, ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai obj)
        {
            if (!obj.IsDeleted
                && obj.Oid != Guid.Empty
                && obj.QuyetDinhTiepNhanVienChucDiNuocNgoai.TuNgay != DateTime.MinValue)
            {
                if (obj.QuyetDinhTiepNhanVienChucDiNuocNgoai.QuyetDinhDiNuocNgoai != null
                        && obj.QuyetDinhTiepNhanVienChucDiNuocNgoai.QuyetDinhDiNuocNgoai.DiNuocNgoaiTren30Ngay
                        && obj.QuyetDinhTiepNhanVienChucDiNuocNgoai.QuyetDinhMoi
                        && obj.QuyetDinhTiepNhanVienChucDiNuocNgoai.TuNgay <= HamDungChung.GetServerTime())
                {
                    obj.ThongTinNhanVien.TinhTrang = HoSoHelper.DangLamViec(session);

                    //quá trình đi nước ngoài
                    QuaTrinhHelper.UpdateQuaTrinhDiNuocNgoai(session, obj.QuyetDinhTiepNhanVienChucDiNuocNgoai.QuyetDinhDiNuocNgoai,
                        obj.ThongTinNhanVien, obj.QuyetDinhTiepNhanVienChucDiNuocNgoai.TuNgay);
                }
            }
        }

        public void Delete(Session session, ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai obj)
        {
            if (obj.ThongTinNhanVien != null)
            {
                if (obj.QuyetDinhTiepNhanVienChucDiNuocNgoai.QuyetDinhDiNuocNgoai != null
                    && obj.QuyetDinhTiepNhanVienChucDiNuocNgoai.QuyetDinhDiNuocNgoai.DiNuocNgoaiTren30Ngay
                    && obj.QuyetDinhTiepNhanVienChucDiNuocNgoai.QuyetDinhMoi)
                {
                    obj.ThongTinNhanVien.TinhTrang = obj.TinhTrang;

                    //reset quá trình di nuoc ngoai
                    QuaTrinhHelper.ResetQuaTrinhDiNuocNgoai(session, obj.QuyetDinhTiepNhanVienChucDiNuocNgoai.QuyetDinhDiNuocNgoai, obj.ThongTinNhanVien);
                }
            }
            if (obj.GiayToHoSo != null)
            {
                session.Delete(obj.GiayToHoSo);
                session.Save(obj.GiayToHoSo);
            }
        }
    }
}
