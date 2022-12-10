using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Đơn giá tiết chuẩn (thỉnh giảng nước ngoài)")]
    public class DonGiaTietChuanThinhgGiangNuocNgoai : BaseObject
    {
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private HocHam _HocHam;
        private decimal _SoTien;

        [ModelDefault("Caption", "Học Vị")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get { return _HocHam; }
            set
            {
                SetPropertyValue("HocHam", ref _HocHam, value);
            }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Số tiền")]
        public decimal SoTien
        {
            get { return _SoTien; }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }
        public DonGiaTietChuanThinhgGiangNuocNgoai(Session session) : base(session) { }

    }
}
