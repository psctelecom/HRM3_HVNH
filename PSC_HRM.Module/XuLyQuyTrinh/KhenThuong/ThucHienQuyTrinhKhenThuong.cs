using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using PSC_HRM.Module.QuyTrinh;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace PSC_HRM.Module.XuLyQuyTrinh.KhenThuong
{
    public class ThucHienQuyTrinhKhenThuong : IThucHienQuyTrinh
    {
        public bool BatDau(IObjectSpace obs, BaseObject obj)
        {
            return ThucHienQuyTrinhHelper.BatDau(obs, obj, "Quy trình khen thưởng");
        }

        public Guid DaBatDau(Session session)
        {
            return ThucHienQuyTrinhHelper.DaBatDau(session, "Quy trình khen thưởng");
        }

        public void ChiTietQuyTrinh(IObjectSpace obs, string chiTietQuyTrinh)
        {
            ThucHienQuyTrinhHelper.ChiTietQuyTrinh(obs, "Quy trình khen thưởng", chiTietQuyTrinh);
        }

        public bool KetThuc(IObjectSpace obs)
        {
            return ThucHienQuyTrinhHelper.KetThuc(obs, "Quy trình khen thưởng");
        }
    }
}
