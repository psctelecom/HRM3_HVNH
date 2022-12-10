using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_HopDong")]
    [DefaultProperty("HopDongThinhGiangChatLuongCao")]
    [ModelDefault("Caption", "Thanh lý hợp đồng thỉnh giảng chất lượng cao")]
    public class ChiTietThanhLyHopDongThinhGiangChatLuongCao : BaseObject
    {
        // Fields...
        private ThongTinNhanVien _NguoiKy;
        private ChucVu _ChucVuNguoiKy;
        private NguoiKyEnum _PhanLoaiNguoiKy = NguoiKyEnum.DangTaiChuc;
        private string _So;
        private DateTime _NgayLap;
        private HopDong_ThinhGiangChatLuongCao _HopDongThinhGiangChatLuongCao;
        private QuanLyThanhLyHopDongThinhGiangChatLuongCao _QuanLyThanhLyHopDongThinhGiangChatLuongCao;

        //UTE
        private decimal _SoTien1Tiet;
        private int _SoTiet;

        [ModelDefault("Caption", "Số tiền 1 tiết")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTien1Tiet
        {
            get
            {
                return _SoTien1Tiet;
            }
            set
            {
                SetPropertyValue("SoTien1Tiet", ref _SoTien1Tiet, value);
            }
        }
        [ModelDefault("Caption", "Số tiết")]
        public int SoTiet
        {
            get
            {
                return _SoTiet;
            }
            set
            {
                SetPropertyValue("SoTiet",ref _SoTiet, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý thanh lý hợp đồng thỉnh giảng chất lượng cao")]
        [Association("QuanLyThanhLyHopDongThinhGiangChatLuongCao-ListChiTietThanhLyHopDongThinhGiangChatLuongCao")]
        public QuanLyThanhLyHopDongThinhGiangChatLuongCao QuanLyThanhLyHopDongThinhGiangChatLuongCao
        {
            get
            {
                return _QuanLyThanhLyHopDongThinhGiangChatLuongCao;
            }
            set
            {
                SetPropertyValue("QuanLyThanhLyHopDongThinhGiangChatLuongCao", ref _QuanLyThanhLyHopDongThinhGiangChatLuongCao, value);
            }
        }

        [ModelDefault("Caption", "Số")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string So
        {
            get
            {
                return _So;
            }
            set
            {
                SetPropertyValue("So", ref _So, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Hợp đồng thỉnh giảng chất lượng cao")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HopDong_ThinhGiangChatLuongCao HopDongThinhGiangChatLuongCao
        {
            get
            {
                return _HopDongThinhGiangChatLuongCao;
            }
            set
            {
                SetPropertyValue("HopDongThinhGiangChatLuongCao", ref _HopDongThinhGiangChatLuongCao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại người ký")]
        public NguoiKyEnum PhanLoaiNguoiKy
        {
            get
            {
                return _PhanLoaiNguoiKy;
            }
            set
            {
                SetPropertyValue("PhanLoaiNguoiKy", ref _PhanLoaiNguoiKy, value);
                if (!IsLoading && ChucVuNguoiKy != null)
                {
                    UpdateNguoiKyList();
                    NguoiKy = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ người ký")]
        [DataSourceProperty("ChucVuList")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVu ChucVuNguoiKy
        {
            get
            {
                return _ChucVuNguoiKy;
            }
            set
            {
                SetPropertyValue("ChucVuNguoiKy", ref _ChucVuNguoiKy, value);
                if (!IsLoading)
                {
                    UpdateNguoiKyList();
                    NguoiKy = null;
                }
            }
        }

        [ModelDefault("Caption", "Người ký")]
        [DataSourceProperty("NguoiKyList")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        public ChiTietThanhLyHopDongThinhGiangChatLuongCao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLap = HamDungChung.GetServerTime();
            MaTruong = TruongConfig.MaTruong;
            UpdateChucVuList();
            UpdateNguoiKyList();
            ThongTinTruong ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            //Chức vụ người ký mặc định Hiệu trưởng
            ChucVuNguoiKy = Session.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", "Hiệu trưởng%"));
        }

        [NonPersistent]
        [Browsable(false)]
        private string MaTruong { get; set; }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            MaTruong = TruongConfig.MaTruong;
            UpdateChucVuList();
            UpdateNguoiKyList();
        
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //thiet lap trang thai hop dong cu cho hop dong vua duoc thanh ly
                HopDongThinhGiangChatLuongCao.HopDongCu = true;
            }
        }

        protected override void OnDeleting()
        {
            HopDongThinhGiangChatLuongCao.HopDongCu = false;
            base.OnDeleting();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyList { get; set; }

        [Browsable(false)]
        public XPCollection<ChucVu> ChucVuList { get; set; }

        //Cập nhật danh sách người ký
        private void UpdateNguoiKyList()
        {
            if (NguoiKyList == null)
                NguoiKyList = new XPCollection<ThongTinNhanVien>(Session);
            if (PhanLoaiNguoiKy == NguoiKyEnum.DangTaiChuc)
                NguoiKyList.Criteria = CriteriaOperator.Parse("ChucVu=?", ChucVuNguoiKy);
            else if (PhanLoaiNguoiKy == NguoiKyEnum.DangKhongTaiChuc)
                NguoiKyList.Criteria = CriteriaOperator.Parse("(ChucVu is null or ChucVu!=?) and ListQuaTrinhBoNhiem[ChucVu=?].Count>0", ChucVuNguoiKy, ChucVuNguoiKy);
        }

        //Cập nhật danh sách chức vụ
        private void UpdateChucVuList()
        {
            if (ChucVuList == null)
                ChucVuList = new XPCollection<ChucVu>(Session);

            ChucVuList.Criteria = CriteriaOperator.Parse("PhanLoai=2 or PhanLoai=0 or PhanLoai is null");
        }
    }

}
