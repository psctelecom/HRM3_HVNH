using System;
using System.Collections.Generic;
using PSC_HRM.Module.ThuNhap.Thuong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    [ModelDefault("Caption", "Import thưởng")]
    public class ImportThuongTuQuyetDinh : ImportBase
    {
        public ImportThuongTuQuyetDinh(Session session)
            : base(session)
        { }

        public override void XuLy(IObjectSpace obs, object obj)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                Import_QuyetDinhKhenThuong quyetDinh = Data as Import_QuyetDinhKhenThuong;
                if (quyetDinh != null)
                {
                    BangThuongNhanVien bangThuong = obj as BangThuongNhanVien;
                    ChiTietThuongNhanVien chiTietThuong;

                    foreach (var item in quyetDinh.QuyetDinhKhenThuong.ListChiTietKhenThuongNhanVien)
                    {
                        chiTietThuong = uow.FindObject<ChiTietThuongNhanVien>(CriteriaOperator.Parse("BangKhenThuongPhucLoi=? and ThongTinNhanVien=? and NgayThuong=?",
                            bangThuong.Oid, item.ThongTinNhanVien.Oid, item.QuyetDinhKhenThuong.NgayHieuLuc));
                        if (chiTietThuong == null)
                        {
                            chiTietThuong = new ChiTietThuongNhanVien(uow);
                            chiTietThuong.BangThuongNhanVien = uow.GetObjectByKey<BangThuongNhanVien>(bangThuong.Oid);
                            chiTietThuong.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                            chiTietThuong.NhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        }
                        chiTietThuong.NgayThuong = item.QuyetDinhKhenThuong.NgayHieuLuc;
                        chiTietThuong.SoTien = quyetDinh.SoTienThuong;
                        chiTietThuong.SoTienChiuThue = quyetDinh.SoTienChiuThue;

                        uow.CommitChanges();
                    }

                    HamDungChung.ShowSuccessMessage("Import dữ liệu từ file excel thành công");
                }
            }
        }
    }
}
