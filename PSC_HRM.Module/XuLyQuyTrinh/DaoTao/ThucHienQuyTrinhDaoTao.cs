using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace PSC_HRM.Module.XuLyQuyTrinh.DaoTao
{
    public class ThucHienQuyTrinhDaoTao : IThucHienQuyTrinh
    {
        public bool BatDau(IObjectSpace obs, BaseObject obj)
        {
            return ThucHienQuyTrinhHelper.BatDau(obs, obj, "Quy trình đào tạo");
        }

        public Guid DaBatDau(Session session)
        {
            return ThucHienQuyTrinhHelper.DaBatDau(session, "Quy trình đào tạo");
        }

        public void ChiTietQuyTrinh(IObjectSpace obs, string chiTietQuyTrinh)
        {
            ThucHienQuyTrinhHelper.ChiTietQuyTrinh(obs, "Quy trình đào tạo", chiTietQuyTrinh);
        }

        public bool KetThuc(IObjectSpace obs)
        {
            return ThucHienQuyTrinhHelper.KetThuc(obs, "Quy trình đào tạo");
        }
    }
}
