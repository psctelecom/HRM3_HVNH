using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Import hợp đồng")]
    [Appearance("HopDong_ImportHopDong.PhanLoaiHDLD", TargetItems = "PhanLoaiHDK;PhanLoaiHDLD;HinhThucThanhToan", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiHopDong=0")]
    [Appearance("HopDong_ImportHopDong.PhanLoaiHDLV", TargetItems = "PhanLoaiHDK;PhanLoaiHDLV", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiHopDong=1")]
    [Appearance("HopDong_ImportHopDong.PhanLoaiHDK", TargetItems = "PhanLoaiHDLV;PhanLoaiHDLD", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiHopDong=2")]
    public class HopDong_ImportHopDong : BaseObject
    {
        // Fields...
        private TaoHopDongEnum _LoaiHopDong = TaoHopDongEnum.HopDongLamViec;
        private ChucVu _ChucVuNguoiKy;
        private NguoiKyEnum _PhanLoaiNguoiKy = NguoiKyEnum.DangTaiChuc;
        private ThongTinNhanVien _NguoiKy;
        private DateTime _NgayKy;
        private HopDongLamViecEnum _PhanLoaiHDLV;
        private HopDongLaoDongEnum _PhanLoaiHDLD;
        private HopDongKhoanEnum _PhanLoaiHDK;
        private HinhThucThanhToanEnum _HinhThucThanhToan = HinhThucThanhToanEnum.ThanhToanQuaThe;

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại hợp đồng")]
        public TaoHopDongEnum LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại HĐ làm việc")]
        public HopDongLamViecEnum PhanLoaiHDLV
        {
            get
            {
                return _PhanLoaiHDLV;
            }
            set
            {
                SetPropertyValue("PhanLoaiHDLV", ref _PhanLoaiHDLV, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại HĐ lao động ")]
        public HopDongLaoDongEnum PhanLoaiHDLD
        {
            get
            {
                return _PhanLoaiHDLD;
            }
            set
            {
                SetPropertyValue("PhanLoaiHDLD", ref _PhanLoaiHDLD, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại HĐ khoán")]
        public HopDongKhoanEnum PhanLoaiHDK
        {
            get
            {
                return _PhanLoaiHDK;
            }
            set
            {
                SetPropertyValue("PhanLoaiHDK", ref _PhanLoaiHDK, value);
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

        [ModelDefault("Caption", "Ngày ký")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayKy
        {
            get
            {
                return _NgayKy;
            }
            set
            {
                SetPropertyValue("NgayKy", ref _NgayKy, value);
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Hình thức thanh toán")]
        public HinhThucThanhToanEnum HinhThucThanhToan
        {
            get
            {
                return _HinhThucThanhToan;
            }
            set
            {
                SetPropertyValue("HinhThucThanhToan", ref _HinhThucThanhToan, value);
            }
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyList { get; set; }

        [Browsable(false)]
        public XPCollection<ChucVu> ChucVuList { get; set; }

        public HopDong_ImportHopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
            ChucVuNguoiKy = Session.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", "Hiệu trưởng%"));
            NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucVu.TenChucVu like ? and TinhTrang.TenTinhTrang like ?", "Hiệu trưởng", "Đang làm việc"));
            NgayKy = HamDungChung.GetServerTime();

            UpdateChucVuList();
            UpdateNguoiKyList();
        }

        private void UpdateNguoiKyList()
        {
            if (NguoiKyList == null)
                NguoiKyList = new XPCollection<ThongTinNhanVien>(Session);

            NguoiKyList.Criteria = HamDungChung.GetNguoiKyTenCriteria(PhanLoaiNguoiKy, ChucVuNguoiKy);
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
