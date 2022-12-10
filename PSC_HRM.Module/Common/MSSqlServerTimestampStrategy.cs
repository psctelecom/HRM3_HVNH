using System;
using System.Data.SqlClient;
using DevExpress.Xpo;
using DevExpress.Persistent.AuditTrail;

namespace PSC_HRM.Module
{
    public class MSSqlServerTimestampStrategy : IAuditTimestampStrategy
    {
        DateTime cachedTimestamp;
        #region IAuditTimestampStrategy Members
        public DateTime GetTimestamp(AuditDataItem auditDataItem)
        {
            return cachedTimestamp;
        }

        public void OnBeginSaveTransaction(Session session)
        {
            cachedTimestamp = HamDungChung.GetServerTime();
        }
        #endregion
    }
}