using DevExpress.ExpressApp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PSC_HRM.Module.BanLamViec
{
    public interface INhacViec
    {
        IList GetData(IObjectSpace obs, params SqlParameter[] param);
    }
    public interface INhacViec<T> where T : class
    {
        List<T> GetData(IObjectSpace obs, params SqlParameter[] param);
    }
}
