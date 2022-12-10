using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PSC_HRM.Module
{
    public sealed class TruongConfig
    {
        public static string MaTruong 
        { 
            get
            {
                return ConfigurationManager.AppSettings["MaTruong"];
            }
        }

        public static string SoHoaTaiLieu
        {
            get
            {
                return ConfigurationManager.AppSettings["SoHoaTaiLieu"];
            }
        }
    }
}
