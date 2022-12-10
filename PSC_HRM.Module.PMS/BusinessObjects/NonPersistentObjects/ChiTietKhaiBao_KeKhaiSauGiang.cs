using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    public class ChiTietKhaiBao_KeKhaiSauGiang : BaseObject
    {
        private bool _Chon;
        private Guid _OidNhanVien;
        private string _HoTen;

        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [Browsable(false)]
        public Guid OidNhanVien
        {
            get { return _OidNhanVien; }
            set { SetPropertyValue("OidNhanVien", ref _OidNhanVien, value); }
        }
        [ModelDefault("Caption", "Họ tên")]
        [ModelDefault("AllowEdit", "False")]
        public string HoTen
        {
            get { return _HoTen; }
            set { SetPropertyValue("HoTen", ref _HoTen, value); }
        }
        private decimal _ChamThiTN;
        private decimal _HDSVThamQuanThucTe;
        private decimal _HDVietCDTN;
        private decimal _HDDeTaiLuanVan;
        private decimal _GiaiDapThacMac;
        private decimal _HeThongHoa_OnThi;
        private decimal _SoanDeThi;
        private decimal _BoSungNganHangCauHoi;
        private decimal _RaDeTotNghiep;
        private decimal _RaDeThiHetHocPhan;

        [ModelDefault("Caption", "Chấm thi tốt nghiệp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ChamThiTN
        {
            get
            {
                return _ChamThiTN;
            }
            set
            {
                SetPropertyValue("ChamThiTN", ref _ChamThiTN, value);
            }
        }

        [ModelDefault("Caption", "Hướng dẫn sinh viên hệ CLC tham quan thực tế")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HDSVThamQuanThucTe
        {
            get
            {
                return _HDSVThamQuanThucTe;
            }
            set
            {
                SetPropertyValue("HDSVThamQuanThucTe", ref _HDSVThamQuanThucTe, value);
            }
        }
        [ModelDefault("Caption", "Hướng dẫn viết CĐTN cho sinh viên cuối khóa (buổi)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HDVietCDTN
        {
            get
            {
                return _HDVietCDTN;
            }
            set
            {
                SetPropertyValue("HDVietCDTN", ref _HDVietCDTN, value);
            }
        }
        [ModelDefault("Caption", "Hướng dẫn đề tài luận văn cho học viện cao học (buổi)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HDDeTaiLuanVan
        {
            get
            {
                return _HDDeTaiLuanVan;
            }
            set
            {
                SetPropertyValue("HDDeTaiLuanVan", ref _HDDeTaiLuanVan, value);
            }
        }
        [ModelDefault("Caption", "Giải đáp thắc mắc cho sinh viên hệ VLVH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GiaiDapThacMac
        {
            get
            {
                return _GiaiDapThacMac;
            }
            set
            {
                SetPropertyValue("GiaiDapThacMac", ref _GiaiDapThacMac, value);
            }
        }
        [ModelDefault("Caption", "Hệ thống hóa và ôn thi cuối khóa")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeThongHoa_OnThi
        {
            get
            {
                return _HeThongHoa_OnThi;
            }
            set
            {
                SetPropertyValue("HeThongHoa_OnThi", ref _HeThongHoa_OnThi, value);
            }
        }
        [ModelDefault("Caption", "Soạn bộ đề thi cho 01 học phần")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoanDeThi
        {
            get
            {
                return _SoanDeThi;
            }
            set
            {
                SetPropertyValue("SoanDeThi", ref _SoanDeThi, value);
            }
        }
        [ModelDefault("Caption", "Bổ sung ngân hàng câu hỏi, đề thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal BoSungNganHangCauHoi
        {
            get
            {
                return _BoSungNganHangCauHoi;
            }
            set
            {
                SetPropertyValue("BoSungNganHangCauHoi", ref _BoSungNganHangCauHoi, value);
            }
        }
        [ModelDefault("Caption", "Ra đề thi tốt nghiệp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal RaDeTotNghiep
        {
            get
            {
                return _RaDeTotNghiep;
            }
            set
            {
                SetPropertyValue("RaDeTotNghiep", ref _RaDeTotNghiep, value);
            }
        }
        [ModelDefault("Caption", "Ra đề thi hết học phần đào tạo SĐH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal RaDeThiHetHocPhan
        {
            get
            {
                return _RaDeThiHetHocPhan;
            }
            set
            {
                SetPropertyValue("RaDeThiHetHocPhan", ref _RaDeThiHetHocPhan, value);
            }
        }
        public ChiTietKhaiBao_KeKhaiSauGiang(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}