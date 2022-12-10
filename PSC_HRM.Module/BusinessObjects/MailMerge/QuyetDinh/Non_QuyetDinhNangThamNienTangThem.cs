using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNangThamNienTangThem : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Năm")]
        public string Nam { get; set; }

        public Non_QuyetDinhNangThamNienTangThem()
        {
            Master = new List<Non_ChiTietQuyetDinhNangThamNienTangThemMaster>();
            Detail = new List<Non_ChiTietQuyetDinhNangThamNienTangThemDetail>();
        }
    }
}
