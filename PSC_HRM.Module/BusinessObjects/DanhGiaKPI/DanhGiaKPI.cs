using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.DanhGiaKPI
{
    //xét thi đua
    [DefaultClassOptions]
    [ImageName("BO_DanhGiaCanBo")]
    [ModelDefault("Caption", "Đánh giá KPI")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinNhanVien;QuanLyDanhGiaKPI;VongDanhGia")]
    [Appearance("DanhGiaKPI.ChotDanhGia", TargetItems = "*", Enabled = false, Criteria = "TinhTrangDuyet = 1 or TinhTrangDuyet = 2")]
    public class DanhGiaKPI : BaseObject, IBoPhan
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private CongViec _CongViec;
        private XepLoaiCanBo _XepLoaiCanBo;
        private decimal _TongPhanTramHoanThanh;
        private TinhTrangDuyetEnum _TinhTrangDuyet;
        private QuanLyDanhGiaKPI _QuanLyDanhGiaKPI;
        private VongDanhGia _VongDanhGia;

        [ImmediatePostData]
        [ModelDefault("Caption", "Nhân viên")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                    {
                        BoPhan = value.BoPhan;
                        CongViec = value.CongViecHienNay;
                    }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ phận")]
        [DataSourceProperty("BoPhanList")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    UpdateNVList();
            }
        }

        [ModelDefault("Caption", "Công việc")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongViec CongViec
        {
            get
            {
                return _CongViec;
            }
            set
            {
                SetPropertyValue("CongViec", ref _CongViec, value);
            }
        }

        [ModelDefault("Caption", "Tổng % hoàn thành")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongPhanTramHoanThanh
        {
            get
            {
                return _TongPhanTramHoanThanh;
            }
            set
            {
                SetPropertyValue("TongPhanTramDatDuoc", ref _TongPhanTramHoanThanh, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại cán bộ")]
        public XepLoaiCanBo XepLoaiCanBo
        {
            get
            {
                return _XepLoaiCanBo;
            }
            set
            {
                SetPropertyValue("XepLoaiCanBo", ref _XepLoaiCanBo, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng duyệt")]
        public TinhTrangDuyetEnum TinhTrangDuyet
        {
            get
            {
                return _TinhTrangDuyet;
            }
            set
            {
                SetPropertyValue("TinhTrangDuyet", ref _TinhTrangDuyet, value);
            }
        }

        [ModelDefault("Caption", "Vòng đánh giá")]
        public VongDanhGia VongDanhGia
        {
            get
            {
                return _VongDanhGia;
            }
            set
            {
                SetPropertyValue("VongDanhGia", ref _VongDanhGia, value);
            }
        }

        [Browsable(false)]
        [Association("QuanLyDanhGiaKPI-ListDanhGiaKPI")]
        public QuanLyDanhGiaKPI QuanLyDanhGiaKPI
        {
            get
            {
                return _QuanLyDanhGiaKPI;
            }
            set
            {
                SetPropertyValue("QuanLyDanhGiaKPI", ref _QuanLyDanhGiaKPI, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết đánh giá KPI")]
        [Association("DanhGiaKPI-ListChiTietDanhGiaKPI")]
        public XPCollection<ChiTietDanhGiaKPI> ListChiTietDanhGiaKPI
        {
            get
            {
                return GetCollection<ChiTietDanhGiaKPI>("ListChiTietDanhGiaKPI");
            }
        }

        [Browsable(false)]
        public XPCollection<BoPhan> BoPhanList { get; set; }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        public DanhGiaKPI(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //Lấy bộ phận theo quyền người dùng
            CriteriaBoPhanList();
            UpdateNVList();

            //Tình trạng duyệt
            TinhTrangDuyet = TinhTrangDuyetEnum.ChuaDuyet;
        }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //Lấy nhân viên thuộc bộ phận theo quyền người dùng
            //Nếu quyền người dùng có nhiều bộ phận, sau khi chọn bộ phận thì chỉ hiện nhân viên trong bộ phận đó
            GroupOperator go = new GroupOperator();
            if (BoPhan == null)
                go.Operands.Add(new InOperator("BoPhan", BoPhanList));
            else
                go.Operands.Add(new InOperator("BoPhan", BoPhan.Oid));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.KhongConCongTacTaiTruong=?", "False"));
            NVList.Criteria = go;
        }

        private void CriteriaBoPhanList()
        {
            if (BoPhanList == null)
                BoPhanList = new XPCollection<BoPhan>(Session);
            GroupOperator go = new GroupOperator();
            go.Operands.Add(new InOperator("Oid", HamDungChung.GetCriteriaBoPhan()));

            BoPhanList.Criteria = go;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            //Lấy bộ phận theo quyền người dùng
            CriteriaBoPhanList();
            UpdateNVList();
        }
    }

}
