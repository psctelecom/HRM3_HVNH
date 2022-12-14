using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.DanhGia
{
    [ImageName("BO_DanhGiaCanBo")]
    [ModelDefault("Caption", "Chi tiết đánh giá cán bộ lần 1")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietDanhGiaCanBoCuoiNamLan1.Unique", DefaultContexts.Save, "DanhGiaCanBoCuoiNam;ThongTinNhanVien")]
    //[Appearance("DisableDetail", TargetItems = "ThongTinNhanVien;BoPhan;KetQuaCongTac;TinhThanKyLuat;TinhThanPhoiHop;TinhTrungThuc;LoiSongDaoDuc;TinhThanHocTap;TinhThanPhucVuNhanDan;TuDanhGia;TongDiem;TinhTrangDuyet;ChapHanhChinhSachPhapLuat;DonViDanhGia", Visibility = ViewItemVisibility.Hide, Criteria = "TinhTrangDuyet=2")]
    [Appearance("ChiTietDanhGiaCanBoCuoiNamLan1.ChotDanhGia", TargetItems = "*", Enabled = false, Criteria = "TinhTrangDuyet=2")]
    public class ChiTietDanhGiaCanBoCuoiNamLan1 : BaseObject, IBoPhan
    {
        // Fields...
        private decimal _ChapHanhChinhSachPhapLuat;
        private decimal _KetQuaCongTac;
        private decimal _TinhThanKyLuat;
        private decimal _TinhThanPhoiHop;
        private decimal _TinhTrungThuc;
        private decimal _LoiSongDaoDuc;
        private decimal _TinhThanHocTap;
        private decimal _TinhThanPhucVuNhanDan;
        private decimal _TongDiem;
        private string _TuDanhGia;
        private string _DonViDanhGia;
        private TinhTrangDuyetEnum _TinhTrangDuyet;
        private DanhGiaCanBoCuoiNam _DanhGiaCanBoCuoiNam;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        [Browsable(false)]
        [ModelDefault("Caption", "Đánh giá cuối năm")]
        [Association("DanhGiaCanBoCuoiNam-ListChiTietDanhGiaCanBoCuoiNamLan1")]
        public DanhGiaCanBoCuoiNam DanhGiaCanBoCuoiNam
        {
            get
            {
                return _DanhGiaCanBoCuoiNam;
            }
            set
            {
                SetPropertyValue("DanhGiaCanBoCuoiNam", ref _DanhGiaCanBoCuoiNam, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chấp hành chính sách, pháp luật của Nhà nước")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal ChapHanhChinhSachPhapLuat
        {
            get
            {
                return _ChapHanhChinhSachPhapLuat;
            }
            set
            {
                SetPropertyValue("ChapHanhChinhSachPhapLuat", ref _ChapHanhChinhSachPhapLuat, value);
                if (!IsLoading)
                    TongDiem = ChapHanhChinhSachPhapLuat + KetQuaCongTac + TinhThanKyLuat + TinhThanPhoiHop + TinhTrungThuc + LoiSongDaoDuc + TinhThanHocTap + TinhThanPhucVuNhanDan;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Kết quả công tác")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal KetQuaCongTac
        {
            get
            {
                return _KetQuaCongTac;
            }
            set
            {
                SetPropertyValue("KetQuaCongTac", ref _KetQuaCongTac, value);
                if (!IsLoading)
                    TongDiem = ChapHanhChinhSachPhapLuat + KetQuaCongTac + TinhThanKyLuat + TinhThanPhoiHop + TinhTrungThuc + LoiSongDaoDuc + TinhThanHocTap + TinhThanPhucVuNhanDan;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tinh thần kỷ luật")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TinhThanKyLuat
        {
            get
            {
                return _TinhThanKyLuat;
            }
            set
            {
                SetPropertyValue("TinhThanKyLuat", ref _TinhThanKyLuat, value);
                if (!IsLoading)
                    TongDiem = ChapHanhChinhSachPhapLuat + KetQuaCongTac + TinhThanKyLuat + TinhThanPhoiHop + TinhTrungThuc + LoiSongDaoDuc + TinhThanHocTap + TinhThanPhucVuNhanDan;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tinh thần phối hợp trong công tác")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TinhThanPhoiHop
        {
            get
            {
                return _TinhThanPhoiHop;
            }
            set
            {
                SetPropertyValue("TinhThanPhoiHop", ref _TinhThanPhoiHop, value);
                if (!IsLoading)
                    TongDiem = ChapHanhChinhSachPhapLuat + KetQuaCongTac + TinhThanKyLuat + TinhThanPhoiHop + TinhTrungThuc + LoiSongDaoDuc + TinhThanHocTap + TinhThanPhucVuNhanDan;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tính trung thực trong công tác")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TinhTrungThuc
        {
            get
            {
                return _TinhTrungThuc;
            }
            set
            {
                SetPropertyValue("TinhTrungThuc", ref _TinhTrungThuc, value);
                if (!IsLoading)
                    TongDiem = ChapHanhChinhSachPhapLuat + KetQuaCongTac + TinhThanKyLuat + TinhThanPhoiHop + TinhTrungThuc + LoiSongDaoDuc + TinhThanHocTap + TinhThanPhucVuNhanDan;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Lối sống, đạo đức")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal LoiSongDaoDuc
        {
            get
            {
                return _LoiSongDaoDuc;
            }
            set
            {
                SetPropertyValue("LoiSongDaoDuc", ref _LoiSongDaoDuc, value);
                if (!IsLoading)
                    TongDiem = ChapHanhChinhSachPhapLuat + KetQuaCongTac + TinhThanKyLuat + TinhThanPhoiHop + TinhTrungThuc + LoiSongDaoDuc + TinhThanHocTap + TinhThanPhucVuNhanDan;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tinh thần học tập, nâng cao trình độ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TinhThanHocTap
        {
            get
            {
                return _TinhThanHocTap;
            }
            set
            {
                SetPropertyValue("TinhThanHocTap", ref _TinhThanHocTap, value);
                if (!IsLoading)
                    TongDiem = ChapHanhChinhSachPhapLuat + KetQuaCongTac + TinhThanKyLuat + TinhThanPhoiHop + TinhTrungThuc + LoiSongDaoDuc + TinhThanHocTap + TinhThanPhucVuNhanDan;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tinh thần, thái độ phục vụ nhân dân")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TinhThanPhucVuNhanDan
        {
            get
            {
                return _TinhThanPhucVuNhanDan;
            }
            set
            {
                SetPropertyValue("TinhThanPhucVuNhanDan", ref _TinhThanPhucVuNhanDan, value);
                if (!IsLoading)
                    TongDiem = ChapHanhChinhSachPhapLuat + KetQuaCongTac + TinhThanKyLuat + TinhThanPhoiHop + TinhTrungThuc + LoiSongDaoDuc + TinhThanHocTap + TinhThanPhucVuNhanDan;
            }
        }

        [ModelDefault("Caption", "Tổng điểm")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TongDiem
        {
            get
            {
                return _TongDiem;
            }
            set
            {
                SetPropertyValue("TongDiem", ref _TongDiem, value);
            }
        }

        //[Size(500)]
        [ModelDefault("Caption", "Bản thân tự đánh giá")]
        public string TuDanhGia
        {
            get
            {
                return _TuDanhGia;
            }
            set
            {
                SetPropertyValue("TuDanhGia", ref _TuDanhGia, value);
            }
        }

        [ModelDefault("Caption", "Ý kiến của tập thể đơn vị")]
        public string DonViDanhGia
        {
            get
            {
                return _DonViDanhGia;
            }
            set
            {
                SetPropertyValue("DonViDanhGia", ref _DonViDanhGia, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("TinhTrangDuyet", Enabled = false)]
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

        [Browsable(false)]
        public XPCollection<BoPhan> BoPhanList { get; set; }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        public ChiTietDanhGiaCanBoCuoiNamLan1(Session session) : base(session) { }

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

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //Lấy bộ phận theo quyền người dùng
            CriteriaBoPhanList();
            UpdateNVList();

            //Tình trạng duyệt
            TinhTrangDuyet = TinhTrangDuyetEnum.ChuaDuyet;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            //Lấy bộ phận theo quyền người dùng
            CriteriaBoPhanList();
            UpdateNVList();
        }
    }

}
