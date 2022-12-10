using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhChiaTachDonVi : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Đơn vị cũ")]
        public string DonViCu { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị mới 1")]
        public string DonViMoi1 { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị mới 2")]
        public string DonViMoi2 { get; set; }

        public Non_QuyetDinhChiaTachDonVi()
        {
            Master = new List<Non_ChiTietQuyetDinhChiaTachDonViMaster>();
            Detail = new List<Non_ChiTietQuyetDinhChiaTachDonViDetail>();
        }
    }
}
