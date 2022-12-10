using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_DieuChinhHoSo")]
    [ModelDefault("Caption", "Điều chỉnh hồ sơ")]
    public class DieuChinhHoSo : BaseObject
    {
        // Fields...
        private string _MaSoMoi;
        private BenhVien _NoiDangKyKCBMoi;
        private DateTime _NgayHetHanTheBHYTMoi;
        private string _NoiOHienNayMoi;
        private TinhThanh _NoiCapMoi;
        private DateTime _NgayCapMoi;
        private string _SoCMNDMoi;
        private string _QueQuanMoi;
        private DateTime _NgaySinhMoi;
        private string _TenMoi;
        private string _HoMoi;
        private string _MaSoCu;
        private BenhVien _NoiDangKyKCBCu;
        private DateTime _NgayHetHanTheBHYTCu;
        private string _NoiOHienNayCu;
        private DateTime _NgayCapCu;
        private TinhThanh _NoiCapCu;
        private string _SoCMNDCu;
        private string _QueQuanCu;
        private DateTime _NgaySinhCu;
        private string _TenCu;
        private string _HoCu;
        private QuanLyDieuChinhHoSo _QuanLyDieuChinhHoSo;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;

        [Browsable(false)]
        [Association("QuanLyDieuChinhHoSo-ListDieuChinhHoSo")]
        public QuanLyDieuChinhHoSo QuanLyDieuChinhHoSo
        {
            get
            {
                return _QuanLyDieuChinhHoSo;
            }
            set
            {
                SetPropertyValue("QuanLyDieuChinhHoSo", ref _QuanLyDieuChinhHoSo, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
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
                {
                    UpdateNhanVienList();
                }
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
                {
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    HoCu = value.Ho;
                    TenCu = value.Ten;
                    NgaySinhCu = value.NgaySinh;
                    QueQuanCu = value.QueQuan != null ? value.QueQuan.FullDiaChi : "";
                    SoCMNDCu = value.CMND;
                    NgayCapCu = value.NgayCap;
                    NoiCapCu = value.NoiCap;
                    NoiOHienNayCu = value.NoiOHienNay != null ? value.NoiOHienNay.FullDiaChi : "";
                    HoMoi = value.Ho;
                    TenMoi = value.Ten;
                    NgaySinhMoi = value.NgaySinh;
                    QueQuanMoi = value.QueQuan != null ? value.QueQuan.FullDiaChi : "";
                    SoCMNDMoi = value.CMND;
                    NgayCapMoi = value.NgayCap;
                    NoiCapMoi = value.NoiCap;
                    NoiOHienNayMoi = value.NoiOHienNay != null ? value.NoiOHienNay.FullDiaChi : "";

                    HoSoBaoHiem bhyt = Session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", value.Oid));
                    if (bhyt != null)
                    {
                        NoiDangKyKCBCu = bhyt.NoiDangKyKhamChuaBenh;
                        NoiDangKyKCBMoi = bhyt.NoiDangKyKhamChuaBenh;
                    }
                }
            }
        }

        //Thong tin cu
        [ModelDefault("Caption", "Họ")]
        public string HoCu
        {
            get
            {
                return _HoCu;
            }
            set
            {
                SetPropertyValue("HoCu", ref _HoCu, value);
            }
        }

        [ModelDefault("Caption", "Tên")]
        public string TenCu
        {
            get
            {
                return _TenCu;
            }
            set
            {
                SetPropertyValue("TenCu", ref _TenCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinhCu
        {
            get
            {
                return _NgaySinhCu;
            }
            set
            {
                SetPropertyValue("NgaySinhCu", ref _NgaySinhCu, value);
            }
        }

        [ModelDefault("Caption", "Quê quán")]
        public string QueQuanCu
        {
            get
            {
                return _QueQuanCu;
            }
            set
            {
                SetPropertyValue("QueQuanCu", ref _QueQuanCu, value);
            }
        }

        [ModelDefault("Caption", "Số CMND")]
        public string SoCMNDCu
        {
            get
            {
                return _SoCMNDCu;
            }
            set
            {
                SetPropertyValue("SoCMNDCu", ref _SoCMNDCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCapCu
        {
            get
            {
                return _NgayCapCu;
            }
            set
            {
                SetPropertyValue("NgayCapCu", ref _NgayCapCu, value);
            }
        }

        [ModelDefault("Caption", "Nơi cấp")]
        public TinhThanh NoiCapCu
        {
            get
            {
                return _NoiCapCu;
            }
            set
            {
                SetPropertyValue("NoiCapCu", ref _NoiCapCu, value);
            }
        }

        [ModelDefault("Caption", "Nơi ở hiện nay")]
        public string NoiOHienNayCu
        {
            get
            {
                return _NoiOHienNayCu;
            }
            set
            {
                SetPropertyValue("NoiOHienNayCu", ref _NoiOHienNayCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn thẻ BHYT")]
        public DateTime NgayHetHanTheBHYTCu
        {
            get
            {
                return _NgayHetHanTheBHYTCu;
            }
            set
            {
                SetPropertyValue("NgayHetHanTheBHYTCu", ref _NgayHetHanTheBHYTCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nơi đăng ký KCB")]
        public BenhVien NoiDangKyKCBCu
        {
            get
            {
                return _NoiDangKyKCBCu;
            }
            set
            {
                SetPropertyValue("NoiDangKyKCBCu", ref _NoiDangKyKCBCu, value);
                if (!IsLoading && value != null)
                    MaSoCu = value.MaQuanLy;
            }
        }

        [ModelDefault("Caption", "Mã số")]
        [ModelDefault("AllowEdit", "False")]
        public string MaSoCu
        {
            get
            {
                return _MaSoCu;
            }
            set
            {
                SetPropertyValue("MaSoCu", ref _MaSoCu, value);
            }
        }




        //Thong tin moi
        [ModelDefault("Caption", "Họ 1")]
        public string HoMoi
        {
            get
            {
                return _HoMoi;
            }
            set
            {
                SetPropertyValue("HoMoi", ref _HoMoi, value);
            }
        }

        [ModelDefault("Caption", "Tên 1")]
        public string TenMoi
        {
            get
            {
                return _TenMoi;
            }
            set
            {
                SetPropertyValue("TenMoi", ref _TenMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh 1")]
        public DateTime NgaySinhMoi
        {
            get
            {
                return _NgaySinhMoi;
            }
            set
            {
                SetPropertyValue("NgaySinhMoi", ref _NgaySinhMoi, value);
            }
        }

        [ModelDefault("Caption", "Quê quán mới 1")]
        public string QueQuanMoi
        {
            get
            {
                return _QueQuanMoi;
            }
            set
            {
                SetPropertyValue("QueQuanMoi", ref _QueQuanMoi, value);
            }
        }

        [ModelDefault("Caption", "Số CMND 1")]
        public string SoCMNDMoi
        {
            get
            {
                return _SoCMNDMoi;
            }
            set
            {
                SetPropertyValue("SoCMNDMoi", ref _SoCMNDMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp 1")]
        public DateTime NgayCapMoi
        {
            get
            {
                return _NgayCapMoi;
            }
            set
            {
                SetPropertyValue("NgayCapMoi", ref _NgayCapMoi, value);
            }
        }

        [ModelDefault("Caption", "Nơi cấp 1")]
        public TinhThanh NoiCapMoi
        {
            get
            {
                return _NoiCapMoi;
            }
            set
            {
                SetPropertyValue("NoiCapMoi", ref _NoiCapMoi, value);
            }
        }

        [ModelDefault("Caption", "Nơi ở hiện nay 1")]
        public string NoiOHienNayMoi
        {
            get
            {
                return _NoiOHienNayMoi;
            }
            set
            {
                SetPropertyValue("NoiOHienNayMoi", ref _NoiOHienNayMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn thẻ BHYT 1")]
        public DateTime NgayHetHanTheBHYTMoi
        {
            get
            {
                return _NgayHetHanTheBHYTMoi;
            }
            set
            {
                SetPropertyValue("NgayHetHanTheBHYTMoi", ref _NgayHetHanTheBHYTMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nơi đăng ký KCB 1")]
        public BenhVien NoiDangKyKCBMoi
        {
            get
            {
                return _NoiDangKyKCBMoi;
            }
            set
            {
                SetPropertyValue("NoiDangKyKCBMoi", ref _NoiDangKyKCBMoi, value);
                if (!IsLoading && value != null)
                    MaSoMoi = value.MaQuanLy;
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Mã số 1")]
        public string MaSoMoi
        {
            get
            {
                return _MaSoMoi;
            }
            set
            {
                SetPropertyValue("MaSoMoi", ref _MaSoMoi, value);
            }
        }


        [Aggregated]
        [ModelDefault("Caption", "Danh sách thông tin điều chỉnh")]
        [Association("DieuChinhHoSo-ListThongTinDieuChinh")]
        public XPCollection<ThongTinDieuChinh> ListThongTinDieuChinh
        {
            get
            {
                return GetCollection<ThongTinDieuChinh>("ListThongTinDieuChinh");
            }
        }

        public DieuChinhHoSo(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NVList = new XPCollection<ThongTinNhanVien>(Session);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<HoSo.ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<HoSo.ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        public void XuLy()
        {
            if (HoCu != HoMoi && !IsExists("Họ và tên đệm"))
            {
                ListThongTinDieuChinh.Add(new ThongTinDieuChinh(Session)
                {
                    NoiDungThayDoi = "Họ và tên đệm",
                    NoiDungCu = HoCu,
                    NoiDungMoi = HoMoi,
                    LyDoDieuChinh = "Điều chỉnh họ và tên đệm"
                });
            }
            if (TenCu != TenMoi && !IsExists("Tên"))
            {
                ListThongTinDieuChinh.Add(new ThongTinDieuChinh(Session)
                {
                    NoiDungThayDoi = "Tên",
                    NoiDungCu = TenCu,
                    NoiDungMoi = TenMoi,
                    LyDoDieuChinh = "Điều chỉnh tên"
                });
            }
            if (NgaySinhCu != NgaySinhMoi && !IsExists("Ngày sinh"))
            {
                ListThongTinDieuChinh.Add(new ThongTinDieuChinh(Session)
                {
                    NoiDungThayDoi = "Ngày sinh",
                    NoiDungCu = NgaySinhCu.ToString("dd/MM/yyyy"),
                    NoiDungMoi = NgaySinhMoi.ToString("dd/MM/yyyy"),
                    LyDoDieuChinh = "Điều chỉnh ngày sinh"
                });
            }
            if (QueQuanCu != QueQuanMoi && !IsExists("Quê quán"))
            {
                ListThongTinDieuChinh.Add(new ThongTinDieuChinh(Session)
                {
                    NoiDungThayDoi = "Quê quán",
                    NoiDungCu = QueQuanCu,
                    NoiDungMoi = QueQuanMoi,
                    LyDoDieuChinh = "Điều chỉnh quê quán"
                });
            }
            if (SoCMNDCu != SoCMNDMoi && !IsExists("Số CMND"))
            {
                ListThongTinDieuChinh.Add(new ThongTinDieuChinh(Session)
                {
                    NoiDungThayDoi = "Số CMND",
                    NoiDungCu = SoCMNDCu,
                    NoiDungMoi = SoCMNDMoi,
                    LyDoDieuChinh = "Điều chỉnh số CMND"
                });
            }
            if (NgayCapCu != NgayCapMoi && !IsExists("Ngày cấp CMND"))
            {
                ListThongTinDieuChinh.Add(new ThongTinDieuChinh(Session)
                {
                    NoiDungThayDoi = "Ngày cấp CMND",
                    NoiDungCu = NgayCapCu.ToString("dd/MM/yyyy"),
                    NoiDungMoi = NgayCapMoi.ToString("dd/MM/yyyy"),
                    LyDoDieuChinh = "Điều chỉnh ngày cấp CMND"
                });
            }
            if (NoiCapCu != null && NoiCapMoi != null &&
                NoiCapCu.TenTinhThanh != NoiCapMoi.TenTinhThanh && !IsExists("Nơi cấp CMND"))
            {
                ListThongTinDieuChinh.Add(new ThongTinDieuChinh(Session)
                {
                    NoiDungThayDoi = "Nơi cấp CMND",
                    NoiDungCu = "CA " + NoiCapCu.TenTinhThanh,
                    NoiDungMoi = "CA " + NoiCapMoi.TenTinhThanh,
                    LyDoDieuChinh = "Điều chỉnh nơi cấp CMND"
                });
            }
            if (NoiOHienNayCu != NoiOHienNayMoi && !IsExists("Nơi ở hiện nay"))
            {
                ListThongTinDieuChinh.Add(new ThongTinDieuChinh(Session)
                {
                    NoiDungThayDoi = "Nơi ở hiện nay",
                    NoiDungCu = NoiOHienNayCu,
                    NoiDungMoi = NoiOHienNayMoi,
                    LyDoDieuChinh = "Điều chỉnh nơi ở hiện nay"
                });
            }
            if (NgayHetHanTheBHYTCu != NgayHetHanTheBHYTMoi && 
                !IsExists("Ngày hết hạn thẻ BHYT"))
            {
                ListThongTinDieuChinh.Add(new ThongTinDieuChinh(Session)
                {
                    NoiDungThayDoi = "Ngày hết hạn thẻ BHYT",
                    NoiDungCu = NgayHetHanTheBHYTCu.ToString("dd/MM/yyyy"),
                    NoiDungMoi = NgayHetHanTheBHYTMoi.ToString("dd/MM/yyyy"),
                    LyDoDieuChinh = "Điều chỉnh ngày hết hạn thẻ BHYT"
                });
            }
            if (NoiDangKyKCBCu != null &&
                NoiDangKyKCBMoi != null &&
                NoiDangKyKCBCu.TenBenhVien != NoiDangKyKCBMoi.TenBenhVien &&
                !IsExists("Nơi đăng ký KCB"))
            {
                ListThongTinDieuChinh.Add(new ThongTinDieuChinh(Session)
                {
                    NoiDungThayDoi = "Nơi đăng ký KCB",
                    NoiDungCu = NoiDangKyKCBCu.TenBenhVien,
                    NoiDungMoi = NoiDangKyKCBMoi.TenBenhVien,
                    LyDoDieuChinh = "Điều chỉnh nơi đăng ký KCB"
                });
            }
            if (MaSoCu != MaSoMoi && !IsExists("Mã số nơi đăng ký KCB"))
            {
                ListThongTinDieuChinh.Add(new ThongTinDieuChinh(Session)
                {
                    NoiDungThayDoi = "Mã số nơi đăng ký KCB",
                    NoiDungCu = MaSoCu,
                    NoiDungMoi = MaSoMoi,
                    LyDoDieuChinh = "Điều chỉnh mã số nơi đăng ký KCB"
                });
            }
        }

        private bool IsExists(string noiDung)
        {
            foreach (ThongTinDieuChinh item in ListThongTinDieuChinh)
            {
                if (item.NoiDungThayDoi == noiDung)
                    return true;
            }
            return false;
        }
    }

}
