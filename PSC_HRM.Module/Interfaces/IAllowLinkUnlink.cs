using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module
{
    public interface IAllowLinkUnlink : IModelNode
    {
        bool AllowLinkUnlink { get; set; }
    }
}
