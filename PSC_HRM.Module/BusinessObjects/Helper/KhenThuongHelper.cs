using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.XuLyQuyTrinh.KhenThuong;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.KhenThuong
{
    public static class KhenThuongHelper
    {
        /// <summary>
        /// Create quan ly boi duong
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ngay"></param>
        /// <returns></returns>
        public static QuanLyKhenThuong CreateQuanLyKhenThuong(Session session, DateTime ngay)
        {
            Guid oid = new ThucHienQuyTrinhKhenThuong().DaBatDau(session);
            QuanLyKhenThuong quanLy = null;
            if (oid != Guid.Empty)
                quanLy = session.GetObjectByKey<QuanLyKhenThuong>(oid);

            if (quanLy == null)
            {
                NamHoc namHoc = HamDungChung.SearchNamHoc(session, ngay);
                ThongTinTruong truong = HamDungChung.ThongTinTruong(session);
                quanLy = session.FindObject<QuanLyKhenThuong>(CriteriaOperator.Parse("ThongTinTruong=? and NamHoc=?", truong, namHoc));
                if (quanLy == null)
                {
                    using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                    {
                        quanLy = new QuanLyKhenThuong(uow);
                        quanLy.NamHoc = uow.GetObjectByKey<NamHoc>(namHoc.Oid);
                    }
                }
            }

            return quanLy;
        }
    }
}
