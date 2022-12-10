using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PhuCapTruong
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông tin chi tiết")]
    public class PhuCapTruong : TruongBaseObject, ISupportController, IBoPhan
    {
        private DateTime _NgayHuongHSPCChuyenMon;
        private DateTime _NgayHuongHSPCThamNien;
        private decimal _HSPCKhac;
        private DateTime _NgayHuong;
        private decimal _HSPCThamNienCu;
        private decimal _HSPCTrachNhiemCu;
        private decimal _HSPCThamNien;
        private decimal _HSPCChuyenMon;
        private decimal _HSPCKiemNhiem;
        private decimal _HSPCLanhDao;
        private bool _Chon;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _NgayVaoCoQuan;
        private string _ThoiGianCongTac;
        private decimal _HSPCTrachNhiem;

        [ModelDefault("Caption", "Chọn")]
        [ModelDefault("AllowEdit", "True")]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    ThongTinNhanVien = null;
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList")]
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
                if (!IsLoading && value != null)
                {
                    NgayVaoCoQuan = value.NgayVaoCoQuan;
                }
            }
        }

        [ModelDefault("Caption", "Ngày về trường")]
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

        [ModelDefault("Caption", "Thời gian công tác")]
        public string ThoiGianCongTac
        {
            get
            {
                return _ThoiGianCongTac;
            }
            set
            {
                SetPropertyValue("ThoiGianCongTac", ref _ThoiGianCongTac, value);
            }
        }

        [ModelDefault("Caption", "HSPC lãnh đạo")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("AllowEdit", "False")]
        public decimal HSPCLanhDao
        {
            get
            {
                return _HSPCLanhDao;
            }
            set
            {
                SetPropertyValue("HSPCLanhDao", ref _HSPCLanhDao, value);
            }
        }

        [ModelDefault("Caption", "HSPC kiêm nhiệm")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("AllowEdit", "False")]
        public decimal HSPCKiemNhiem
        {
            get
            {
                return _HSPCKiemNhiem;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiem", ref _HSPCKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "HSPC chuyên môn")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("AllowEdit", "False")]
        public decimal HSPCChuyenMon
        {
            get
            {
                return _HSPCChuyenMon;
            }
            set
            {
                SetPropertyValue("HSPCChuyenMon", ref _HSPCChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HSPC chuyên môn")]
        public DateTime NgayHuongHSPCChuyenMon
        {
            get
            {
                return _NgayHuongHSPCChuyenMon;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCChuyenMon", ref _NgayHuongHSPCChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "HSPC trách nhiệm cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCTrachNhiemCu
        {
            get
            {
                return _HSPCTrachNhiemCu;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiemCu", ref _HSPCTrachNhiemCu, value);
            }
        }

        [ModelDefault("Caption", "HSPC trách nhiệm mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCTrachNhiem
        {
            get
            {
                return _HSPCTrachNhiem;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiem", ref _HSPCTrachNhiem, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HSPC trách nhiệm")]
        public DateTime NgayHuong
        {
            get
            {
                return _NgayHuong;
            }
            set
            {
                SetPropertyValue("NgayHuong", ref _NgayHuong, value);
            }
        }

        [ModelDefault("Caption", "HSPC thâm niên cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCThamNienCu
        {
            get
            {
                return _HSPCThamNienCu;
            }
            set
            {
                SetPropertyValue("HSPCThamNienCu", ref _HSPCThamNienCu, value);
            }
        }

        [ModelDefault("Caption", "HSPC thâm niên mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCThamNien
        {
            get
            {
                return _HSPCThamNien;
            }
            set
            {
                SetPropertyValue("HSPCThamNien", ref _HSPCThamNien, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HSPC thâm niên")]
        public DateTime NgayHuongHSPCThamNien
        {
            get
            {
                return _NgayHuongHSPCThamNien;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCThamNien", ref _NgayHuongHSPCThamNien, value);
            }
        }

        [ModelDefault("Caption", "HSPC khác")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCKhac
        {
            get
            {
                return _HSPCKhac;
            }
            set
            {
                SetPropertyValue("HSPCKhac", ref _HSPCKhac, value);
            }
        }

        public PhuCapTruong(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<HoSo.ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
