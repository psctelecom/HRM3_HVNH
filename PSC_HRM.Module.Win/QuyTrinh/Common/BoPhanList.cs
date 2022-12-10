using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PSC_HRM.Module.Win.QuyTrinh.Common
{
    public class BoPhanList : BindingList<BoPhanItem>
    {
        public BoPhanList()
        { }

        public BoPhanList(IList<BoPhanItem> list)
            : base(list)
        { }

        /// <summary>
        /// Get list bo phan
        /// </summary>
        /// <returns></returns>
        public List<Guid> GetBoPhanList()
        {
                var data = from a in Items
                           where a.Chon == true
                           select a.Oid;

                return data.ToList<Guid>();
        }
    }
}
