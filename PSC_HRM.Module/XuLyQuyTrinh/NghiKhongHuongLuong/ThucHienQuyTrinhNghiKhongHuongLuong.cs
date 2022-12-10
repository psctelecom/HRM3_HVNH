using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace PSC_HRM.Module.XuLyQuyTrinh.NghiKhongHuongLuong
{
    public class ThucHienQuyTrinhNghiKhongHuongLuong : IThucHienQuyTrinh
    {
        public bool BatDau(IObjectSpace obs, BaseObject obj)
        {
            return ThucHienQuyTrinhHelper.BatDau(obs, obj, "Quy trình nghỉ không hưởng lương");
        }

        public Guid DaBatDau(Session session)
        {
            return ThucHienQuyTrinhHelper.DaBatDau(session, "Quy trình nghỉ không hưởng lương");
        }

        public void ChiTietQuyTrinh(IObjectSpace obs, string chiTietQuyTrinh)
        {
            ThucHienQuyTrinhHelper.ChiTietQuyTrinh(obs, "Quy trình nghỉ không hưởng lương", chiTietQuyTrinh);
        }

        public bool KetThuc(IObjectSpace obs)
        {
            return ThucHienQuyTrinhHelper.KetThuc(obs, "Quy trình nghỉ không hưởng lương");
        }
    }
}
