using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.QuyTrinh;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.XuLyQuyTrinh
{
    public class ThucHienQuyTrinhHelper
    {
        /// <summary>
        /// bắt đầu quy trình
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="obj"></param>
        /// <param name="tenQuyTrinh"></param>
        /// <returns></returns>
        public static bool BatDau(IObjectSpace obs, BaseObject obj, string tenQuyTrinh)
        {
            QuyTrinh.QuyTrinh quyTrinh = obs.FindObject<QuyTrinh.QuyTrinh>(CriteriaOperator.Parse("TenQuyTrinh like ?", tenQuyTrinh));
            if (quyTrinh != null)
            {
                ThucHienQuyTrinh thucHienQuyTrinh = obs.CreateObject<ThucHienQuyTrinh>();
                thucHienQuyTrinh.QuyTrinh = quyTrinh;
                thucHienQuyTrinh.BatDau = true;
                thucHienQuyTrinh.LuuTruDuLieu = obj.Oid;
                obs.CommitChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// kiểm tra quy trình đã bắt đầu chưa
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="tenQuyTrinh"></param>
        /// <returns></returns>
        public static Guid DaBatDau(Session session, string tenQuyTrinh)
        {
            ThucHienQuyTrinh thucHienQuyTrinh = session.FindObject<ThucHienQuyTrinh>(CriteriaOperator.Parse("QuyTrinh.TenQuyTrinh like ? and BatDau and !KetThuc", tenQuyTrinh));
            if (thucHienQuyTrinh != null)
                return thucHienQuyTrinh.LuuTruDuLieu;
            return Guid.Empty;
        }

        /// <summary>
        /// các bước trong quy trình
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="chiTietQuyTrinh"></param>
        /// <param name="tenQuyTrinh"></param>
        public static void ChiTietQuyTrinh(IObjectSpace obs, string tenQuyTrinh, string chiTietQuyTrinh)
        {
            ChiTietQuyTrinh chiTiet = obs.FindObject<ChiTietQuyTrinh>(CriteriaOperator.Parse("QuyTrinh.TenQuyTrinh like ? and TenChiTietQuyTrinh like ?", tenQuyTrinh, chiTietQuyTrinh));
            if (chiTiet != null)
            {
                ThucHienQuyTrinh thucHienQuyTrinh = obs.FindObject<ThucHienQuyTrinh>(CriteriaOperator.Parse("QuyTrinh=? and BatDau and !KetThuc", chiTiet.QuyTrinh.Oid));
                if (thucHienQuyTrinh != null)
                {
                    thucHienQuyTrinh.ChiTietQuyTrinh = chiTiet;
                    obs.CommitChanges();
                }
            }
        }

        /// <summary>
        /// ket thuc quy trình
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="tenQuyTrinh"></param>
        /// <returns></returns>
        public static bool KetThuc(IObjectSpace obs, string tenQuyTrinh)
        {
            ThucHienQuyTrinh thucHienQuyTrinh = obs.FindObject<ThucHienQuyTrinh>(CriteriaOperator.Parse("QuyTrinh.TenQuyTrinh like ? and BatDau and !KetThuc", tenQuyTrinh));
            if (thucHienQuyTrinh != null)
            {
                thucHienQuyTrinh.KetThuc = true;
                obs.CommitChanges();
                return true;
            }
            return false;
        }
    }
}
