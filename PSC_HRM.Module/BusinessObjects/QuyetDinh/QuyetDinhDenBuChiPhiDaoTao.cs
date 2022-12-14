using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định đền bù chi phí đào tạo")]
    public class QuyetDinhDenBuChiPhiDaoTao : QuyetDinh
    {
        private DateTime _TuNgay;
        private DateTime _NgayPhatSinhBienDong;
        private QuyetDinhDaoTao _QuyetDinhDaoTao;
        private QuocGia _QuocGia;
        private TruongDaoTao _TruongDaoTao;
        //private string _LuuTru;
        private bool _QuyetDinhMoi;
        private string _GhiChu;
        private string _SoTien;
        private string _SoTienBangChu;
        private string _TenDenBu;
        private string _DonViDenBu;

        [Browsable(false)]
        [ModelDefault("Caption", "Ngày phát sinh biến động")]
        public DateTime NgayPhatSinhBienDong
        {
            get
            {
                return _NgayPhatSinhBienDong;
            }
            set
            {
                SetPropertyValue("NgayPhatSinhBienDong", ref _NgayPhatSinhBienDong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định đào tạo")]
        public QuyetDinhDaoTao QuyetDinhDaoTao
        {
            get
            {
                return _QuyetDinhDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhDaoTao", ref _QuyetDinhDaoTao, value);
                if (!IsLoading && value != null)
                {
                    ChiTietDenBuChiPhiDaoTao chiTiet;
                    
                    foreach (ChiTietDaoTao item in value.ListChiTietDaoTao)
                    {
                        chiTiet = Session.FindObject<ChiTietDenBuChiPhiDaoTao>(CriteriaOperator.Parse("QuyetDinhDenBuChiPhiDaoTao=? and ThongTinNhanVien=?", Oid, item.ThongTinNhanVien));
                        if (chiTiet == null)
                        {
                            chiTiet = new ChiTietDenBuChiPhiDaoTao(Session);
                            chiTiet.QuyetDinhDenBuChiPhiDaoTao = this;
                            chiTiet.BoPhan = item.BoPhan;
                            chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                            //chiTiet.TinhTrangMoi = item.TinhTrang;
                            ListChiTietDenBuChiPhiDaoTao.Add(chiTiet);
                        }
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày trở lại trường")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                    NgayPhatSinhBienDong = value;
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Quốc gia")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
                if (!IsLoading)
                {
                    UpdateTruongList();
                    TruongDaoTao = null;
                }
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Trường đào tạo")]
        [DataSourceProperty("TruongList")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public TruongDaoTao TruongDaoTao
        {
            get
            {
                return _TruongDaoTao;
            }
            set
            {
                SetPropertyValue("TruongDaoTao", ref _TruongDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Quyết định mới")]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
            }
        }

        //[ModelDefault("Caption", "Lưu trữ")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        //public string LuuTru
        //{
        //    get
        //    {
        //        return _LuuTru;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LuuTru", ref _LuuTru, value);
        //    }
        //}

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }
        [ModelDefault("Caption", "Số tiền")]
        public string SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }
        [ModelDefault("Caption", "Số tiền bằng chữ")]
        public string SoTienBangChu
        {
            get
            {
                return _SoTienBangChu;
            }
            set
            {
                SetPropertyValue("SoTienBangChu", ref _SoTienBangChu, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị đền bù ")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        public string DonViDenBu
        {
            get
            {
                return _DonViDenBu;
            }
            set
            {
                SetPropertyValue("DonViDenBu", ref _DonViDenBu, value);
            }
        }

        [ModelDefault("Caption", "Tên đền bù đào tạo")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        public string TenDenBu
        {
            get
            {
                return _TenDenBu;
            }
            set
            {
                SetPropertyValue("TenDenBu", ref _TenDenBu, value);
            }
        }
        

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhDenBuChiPhiDaoTao-ListChiTietDenBuChiPhiDaoTao")]
        public XPCollection<ChiTietDenBuChiPhiDaoTao> ListChiTietDenBuChiPhiDaoTao
        {
            get
            {
                return GetCollection<ChiTietDenBuChiPhiDaoTao>("ListChiTietDenBuChiPhiDaoTao");
            }
        }

        public QuyetDinhDenBuChiPhiDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhDenBuChiPhiDaoTao;
            //
            QuyetDinhMoi = true;
        }

        protected override void QuyetDinhChanged()
        {
            if (NgayHieuLuc != DateTime.MinValue)
                NgayPhatSinhBienDong = NgayHieuLuc;
        }
        [Browsable(false)]
        public XPCollection<TruongDaoTao> TruongList { get; set; }
        private void UpdateTruongList()
        {
            if (TruongList == null)
                TruongList = new XPCollection<TruongDaoTao>(Session);
            TruongList.Criteria = CriteriaOperator.Parse("QuocGia=?", QuocGia);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietDenBuChiPhiDaoTao item in ListChiTietDenBuChiPhiDaoTao)
                    {
                        item.GiayToHoSo.QuyetDinh = this;
                        item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
                        item.GiayToHoSo.TrichYeu = NoiDung;
                        item.GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                        item.GiayToHoSo.NgayLap = NgayQuyetDinh;
                        item.GiayToHoSo.LuuTru = GiayToHoSo.LuuTru;
                        item.GiayToHoSo.DuongDanFile = GiayToHoSo.DuongDanFile;
                    }
                }

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietDenBuChiPhiDaoTao.Count == 1)
                {
                    BoPhanText = ListChiTietDenBuChiPhiDaoTao[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietDenBuChiPhiDaoTao[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
        }
    }

}
