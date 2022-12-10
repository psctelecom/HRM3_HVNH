using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BanLamViec
{
    [DefaultProperty("TieuDe")]
    [ModelDefault("Caption", "Lịch làm việc")]
    public class LichLamViec : BaseObject
    {
        // Fields...
        private bool _CongKhai;
        private string _GhiChu;
        private DateTime _KetThuc;
        private DateTime _BatDau;
        private NhomCongViec _NhomCongViec;
        private string _DiaDiem;
        private string _TieuDe;
        private NguoiSuDung _NguoiSuDung;

        [ModelDefault("Caption", "Tên")]
        public string TieuDe
        {
            get
            {
                return _TieuDe;
            }
            set
            {
                SetPropertyValue("TieuDe", ref _TieuDe, value);
            }
        }

        [ModelDefault("Caption", "Địa điểm")]
        public string DiaDiem
        {
            get
            {
                return _DiaDiem;
            }
            set
            {
                SetPropertyValue("DiaDiem", ref _DiaDiem, value);
            }
        }

        [Size(300)]
        [ModelDefault("Caption", "Nội dung")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [ModelDefault("Caption", "Nhóm công việc")]
        public NhomCongViec NhomCongViec
        {
            get
            {
                return _NhomCongViec;
            }
            set
            {
                SetPropertyValue("NhomCongViec", ref _NhomCongViec, value);
            }
        }

        [ModelDefault("Caption", "Bắt đầu")]
        public DateTime BatDau
        {
            get
            {
                return _BatDau;
            }
            set
            {
                SetPropertyValue("BatDau", ref _BatDau, value);
            }
        }

        [ModelDefault("Caption", "Kết thúc")]
        public DateTime KetThuc
        {
            get
            {
                return _KetThuc;
            }
            set
            {
                SetPropertyValue("KetThuc", ref _KetThuc, value);
            }
        }

        [ModelDefault("Caption", "Tài khoản")]
        public NguoiSuDung NguoiSuDung
        {
            get
            {
                return _NguoiSuDung;
            }
            set
            {
                SetPropertyValue("NguoiSuDung", ref _NguoiSuDung, value);
            }
        }

        [ModelDefault("Caption", "Công khai")]
        public bool CongKhai
        {
            get
            {
                return _CongKhai;
            }
            set
            {
                SetPropertyValue("CongKhai", ref _CongKhai, value);
            }
        }

        public LichLamViec(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NguoiSuDung user = HamDungChung.CurrentUser();
            NguoiSuDung = Session.GetObjectByKey<NguoiSuDung>(user.Oid);
        }
    }

}
