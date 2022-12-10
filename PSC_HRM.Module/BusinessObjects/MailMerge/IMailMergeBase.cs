using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.MailMerge
{
    public interface IMailMergeBase
    {
        string Oid { get; set; }
        IList Master { get; set; }
        IList Master1 { get; set; }
        IList Detail { get; set; }
        IList Detail1 { get; set; }
    }
}
