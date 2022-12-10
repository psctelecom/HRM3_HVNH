using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [DefaultClassOptions]
    [ImageName("BO_HoaDon")]
    [ModelDefault("Caption", "Chi tiết thuế thu nhập cá nhân")]
    public class ChiTietThueThuNhapCaNhan : BaseObject
    {
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private string _MaQuanLy;
        private decimal _TongThuNhap;
        private decimal _TongBaoHiem;
        private decimal _ThuNhapChiuThue;
        private decimal _GiamTruBanThan;
        private decimal _GiamTruGiaCanh;
        private decimal _ThuNhapTinhThue;
        private decimal _ThueTNCN;
        private QuanLyThueThuNhapCaNhan _QuanLyThueThuNhapCaNhan;

        [Browsable(false)]
        [Association("QuanLyThueThuNhapCaNhan-ChiTietThueThuNhapCaNhan")]
        public QuanLyThueThuNhapCaNhan QuanLyThueThuNhapCaNhan
        {
            get
            {
                return _QuanLyThueThuNhapCaNhan;
            }
            set
            {
                SetPropertyValue("QuanLyThueThuNhapCaNhan", ref _QuanLyThueThuNhapCaNhan, value);
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
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Mã nhân viên")]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tổng thu nhập")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongThuNhap
        {
            get
            {
                return _TongThuNhap;
            }
            set
            {
                SetPropertyValue("TongThuNhap", ref _TongThuNhap, value);
            }
        }

        [ModelDefault("Caption", "Tổng tiền bảo hiểm")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongBaoHiem
        {
            get
            {
                return _TongBaoHiem;
            }
            set
            {
                SetPropertyValue("TongBaoHiem", ref _TongBaoHiem, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập chịu thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThuNhapChiuThue
        {
            get
            {
                return _ThuNhapChiuThue;
            }
            set
            {
                SetPropertyValue("ThuNhapChiuThue", ref _ThuNhapChiuThue, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ bản thân")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal GiamTruBanThan
        {
            get
            {
                return _GiamTruBanThan;
            }
            set
            {
                SetPropertyValue("GiamTruBanThan", ref _GiamTruBanThan, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ gia cảnh")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal GiamTruGiaCanh
        {
            get
            {
                return _GiamTruGiaCanh;
            }
            set
            {
                SetPropertyValue("GiamTruGiaCanh", ref _GiamTruGiaCanh, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập tính thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThuNhapTinhThue
        {
            get
            {
                return _ThuNhapTinhThue;
            }
            set
            {
                SetPropertyValue("ThuNhapTinhThue", ref _ThuNhapTinhThue, value);
            }
        }

        [ModelDefault("Caption", "Thuế TNCN")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThueTNCN
        {
            get
            {
                return _ThueTNCN;
            }
            set
            {
                SetPropertyValue("ThueTNCN", ref _ThueTNCN, value);
            }
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (string.IsNullOrEmpty(this.MaQuanLy) && NhanVien!=null)
            {
                this.MaQuanLy = NhanVien.MaQuanLy;
            }
        }
        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? or TinhTrang.TenTinhTrang not like ?", "%nghỉ việc%", "%nghỉ hưu%"));

            NVList.Criteria = go;
        }

        public ChiTietThueThuNhapCaNhan(Session session) : base(session) { }
    
    }

}
