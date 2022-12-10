using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.XuLyQuyTrinh
{
    public interface IThucHienQuyTrinh
    {
        bool BatDau(IObjectSpace obs, BaseObject obj);
        Guid DaBatDau(Session session);
        void ChiTietQuyTrinh(IObjectSpace obs, string chiTietQuyTrinh);
        bool KetThuc(IObjectSpace obs);
    }
}
