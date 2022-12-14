using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Tạo đơn xác nhận bảng lương")]
    public class Luong_DonXacNhanBangLuong : BaseObject
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _TuThang;
        private DateTime _DenThang;
        private string _LyDo;
        private bool _TiengAnh;
        private DateTime _NgayLapDon;
        private ThongTinNhanVien _NguoiKy;

        [ModelDefault("Caption", "Thông tin nhân viên")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Từ tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime TuThang
        {
            get
            {
                return _TuThang;
            }
            set
            {
                SetPropertyValue("TuThang", ref _TuThang, value);
            }
        }

        [ModelDefault("Caption", "Đến tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime DenThang
        {
            get
            {
                return _DenThang;
            }
            set
            {
                SetPropertyValue("DenThang", ref _DenThang, value);
            }
        }

        [ModelDefault("Caption", "Lý do")]  
        //[Size(250)] 
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        [ModelDefault("Caption", "Tiếng Anh")]
        public bool TiengAnh
        {
            get
            {
                return _TiengAnh;
            }
            set
            {
                SetPropertyValue("TiengAnh", ref _TiengAnh, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập đơn")]        
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayLapDon
        {
            get
            {
                return _NgayLapDon;
            }
            set
            {
                SetPropertyValue("NgayLapDon", ref _NgayLapDon, value);
            }
        }

        [ModelDefault("Caption", "Người ký")]        
        public ThongTinNhanVien NguoiKy
        {
            get
            {
                return _NguoiKy;
            }
            set
            {
                SetPropertyValue("NguoiKy", ref _NguoiKy, value);
            }
        }

        public Luong_DonXacNhanBangLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLapDon = HamDungChung.GetServerTime();
            //NguoiKy = HamDungChung.CurrentUser().ThongTinNhanVien;
            TuThang = HamDungChung.GetServerTime();
            DenThang = HamDungChung.GetServerTime();
            LyDo = "Làm thủ tục cho con đi học nước ngoài";
        }
    }

}
