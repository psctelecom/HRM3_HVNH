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

namespace PSC_HRM.Module.DanhGia
{
    [ImageName("BO_DanhGiaCanBo")]
    [ModelDefault("Caption", "Chi tiết đánh giá cán bộ lần 2")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietDanhGiaCanBoCuoiNamLan2.Unique", DefaultContexts.Save, "DanhGiaCanBoCuoiNam;ThongTinNhanVien")]
    //[Appearance("DanhGiaCanBoCuoiNam.ChotDanhGia", TargetItems = "*", Enabled = false, Criteria = "ChotDanhGia")]
    public class ChiTietDanhGiaCanBoCuoiNamLan2 : BaseObject, IBoPhan
    {
        // Fields...
        private decimal _ChapHanhChinhSachPhapLuat;
        private string _GhiChu1;
        private decimal _KetQuaCongTac;
        private string _GhiChu2;
        private decimal _TinhThanKyLuat;
        private string _GhiChu3;
        private decimal _TinhThanPhoiHop;
        private string _GhiChu4;
        private decimal _TinhTrungThuc;
        private string _GhiChu5;
        private decimal _LoiSongDaoDuc;
        private string _GhiChu6;
        private decimal _TinhThanHocTap;
        private string _GhiChu7;
        private decimal _TinhThanPhucVuNhanDan;
        private string _GhiChu8;
        private decimal _TongDiem;
        private string _TuDanhGia;
        private string _DonViDanhGia;
        private TinhTrangDuyetEnum _TinhTrangDuyet;
        private XepLoaiDanhGiaVienChucEnum _XepLoaiDanhGiaVienChuc;
        private DanhGiaCanBoCuoiNam _DanhGiaCanBoCuoiNam;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        [Browsable(false)]
        [ModelDefault("Caption", "Đánh giá cuối năm")]
        [Association("DanhGiaCanBoCuoiNam-ListChiTietDanhGiaCanBoCuoiNamLan2")]
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

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu1
        {
            get
            {
                return _GhiChu1;
            }
            set
            {
                SetPropertyValue("GhiChu1", ref _GhiChu1, value);
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

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu2
        {
            get
            {
                return _GhiChu2;
            }
            set
            {
                SetPropertyValue("GhiChu2", ref _GhiChu2, value);
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

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu3
        {
            get
            {
                return _GhiChu3;
            }
            set
            {
                SetPropertyValue("GhiChu3", ref _GhiChu3, value);
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

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu4
        {
            get
            {
                return _GhiChu4;
            }
            set
            {
                SetPropertyValue("GhiChu4", ref _GhiChu4, value);
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

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu5
        {
            get
            {
                return _GhiChu5;
            }
            set
            {
                SetPropertyValue("GhiChu5", ref _GhiChu5, value);
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

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu6
        {
            get
            {
                return _GhiChu6;
            }
            set
            {
                SetPropertyValue("GhiChu6", ref _GhiChu6, value);
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

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu7
        {
            get
            {
                return _GhiChu7;
            }
            set
            {
                SetPropertyValue("GhiChu7", ref _GhiChu7, value);
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

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu8
        {
            get
            {
                return _GhiChu8;
            }
            set
            {
                SetPropertyValue("GhiChu8", ref _GhiChu8, value);
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

        [ModelDefault("Caption", "Xếp loại")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XepLoaiDanhGiaVienChucEnum XepLoaiDanhGiaVienChuc
        {
            get
            {
                return _XepLoaiDanhGiaVienChuc;
            }
            set
            {
                SetPropertyValue("XepLoaiDanhGiaVienChuc", ref _XepLoaiDanhGiaVienChuc, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        public ChiTietDanhGiaCanBoCuoiNamLan2(Session session) : base(session) { }

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

            //Set tình trạng duyệt
            TinhTrangDuyet = TinhTrangDuyetEnum.ChuaDuyet;

            //Lấy bộ phận theo quyền người dùng
            CriteriaBoPhanList();
            UpdateNVList();
        }

        protected override void OnLoading()
        {
            base.OnLoading();
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }

        protected override void OnDeleting()
        {
            CriteriaOperator filter = CriteriaOperator.Parse("DanhGiaCanBoCuoiNam=? and ThongTinNhanVien=?", this.DanhGiaCanBoCuoiNam, this.ThongTinNhanVien);
            ChiTietDanhGiaCanBoCuoiNamLan1 chiTiet = Session.FindObject<ChiTietDanhGiaCanBoCuoiNamLan1>(filter);
            if (chiTiet != null)
            {
                if (chiTiet.TinhTrangDuyet == TinhTrangDuyetEnum.DaChot)
                {
                    chiTiet.TinhTrangDuyet = TinhTrangDuyetEnum.DaDuyet;
                }
            }

            base.OnDeleting();
        }
    }

}
