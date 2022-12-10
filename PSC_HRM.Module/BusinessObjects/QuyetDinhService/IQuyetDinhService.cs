using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace PSC_HRM.Module.QuyetDinhService
{
    public interface IQuyetDinhService<T> where T : BaseObject
    {
        void Save(Session session, T obj);
        void Delete(Session session, T obj);
    }
}
