using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using PSC_HRM.Module.BanLamViec;
using PSC_HRM.Module.BanLamViec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.XuLyQuyTrinh.NangThamNienCongTac
{
    [NonPersistent]
    [ImageName("BO_Group5")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "Thâm niên công tác")]
    public class ThamNienCongTac : Notification
    {
        // Fields...
        private decimal _ThamNienMoi;
        private decimal _ThamNienCu;
        private DateTime _NgayVaoCoQuan;

        [ModelDefault("Caption", "Ngày vào cơ quan")]
        public DateTime NgayVaoCoQuan
        {
            get
            {
                return _NgayVaoCoQuan;
            }
            set
            {
                SetPropertyValue("NgayVaoCoQuan", ref _NgayVaoCoQuan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% thâm niên cũ")]
        public decimal ThamNienCu
        {
            get
            {
                return _ThamNienCu;
            }
            set
            {
                SetPropertyValue("ThamNienCu", ref _ThamNienCu, value);
                if (!IsLoading)
                {
                    if (value == 0)
                        ThamNienMoi = 5;
                    else
                        ThamNienMoi = value + 1;
                }
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% thâm niên mới")]
        public decimal ThamNienMoi
        {
            get
            {
                return _ThamNienMoi;
            }
            set
            {
                SetPropertyValue("ThamNienMoi", ref _ThamNienMoi, value);
            }
        }

        public ThamNienCongTac(Session session) : base(session) { }

        protected override void AfterThongTinNhanVienChanged()
        {
            NgayVaoCoQuan = ThongTinNhanVien.NgayVaoCoQuan;
            Ngay = new DateTime(DateTime.Today.Year, NgayVaoCoQuan.Month, NgayVaoCoQuan.Day);
            ThamNienCu = ThongTinNhanVien.NhanVienThongTinLuong.ThamNienCongTac;
            ThamNienMoi = ThamNienCu + 1;
            GhiChu = ObjectFormatter.Format("Cán bộ {ThongTinNhanVien.HoTen} đến hạn nâng thâm niên công tác.", this);
        }
    }
}
