using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public interface IMailMerge<T> where T : class
    {
        void Merge(IObjectSpace obs, T obj);
    }
}
