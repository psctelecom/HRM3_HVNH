using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PSC_HRM.Module.Win.QuyTrinh.Common
{
    public class NhanVienList : BindingList<NhanVienItem>
    {
        public NhanVienList()
        { }

        public NhanVienList(IList<NhanVienItem> list)
            : base(list)
        { }

        /// <summary>
        /// Get list nhan vien
        /// </summary>
        /// <returns></returns>
        public List<Guid> GetNhanVienList()
        {
                var data = from a in Items
                           where a.Chon == true
                           select a.Oid;

                return data.ToList<Guid>();
        }
    }
}
