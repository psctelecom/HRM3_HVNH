using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSC_HRM.Module.TaoMaQuanLy
{
    public abstract class MaQuanLyBase
    {
        public abstract string TaoMaQuanLy(params SqlParameter[] args);        
    }
}
