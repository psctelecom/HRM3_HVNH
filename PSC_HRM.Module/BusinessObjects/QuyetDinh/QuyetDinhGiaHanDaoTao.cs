using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]    
    [ModelDefault("Caption", "Quyết định gia hạn đào tạo")]
    [Appearance("Hide.KhacNEU", TargetItems = "LyDo;NgayXinGiaHan;SoCongVan", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong!='NEU'")]
    public class QuyetDinhGiaHanDaoTao : QuyetDinhCaNhan
    {
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private QuyetDinhDaoTao _QuyetDinhDaoTao;
        private int _ThoiGianGiaHan = 12;
        private bool _KhongHuongLuong;
        private TinhTrang _TinhTrangCu;
        //NEU
        private string _LyDo;
        private DateTime _NgayXinGiaHan;
        private string _SoCongVan;
       

        [ImmediatePostData]
        [DataSourceProperty("QuyetDinhList")]
        [ModelDefault("Caption", "Quyết định đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    TuNgay = value.DenNgay;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thơi gian gia hạn (tháng)")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int ThoiGianGiaHan
        {
            get
            {
                return _ThoiGianGiaHan;
            }
            set
            {
                SetPropertyValue("ThoiGianGiaHan", ref _ThoiGianGiaHan, value);
                if (!IsLoading && TuNgay != DateTime.MinValue)
                    DenNgay = TuNgay.AddMonths(value).AddDays(-1);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
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
                if (!IsLoading && TuNgay != DateTime.MinValue)
                    DenNgay = value.AddMonths(ThoiGianGiaHan).AddDays(-1);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày xin gia hạn")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NgayXinGiaHan
        {
            get
            {
                return _NgayXinGiaHan;
            }
            set
            {
                SetPropertyValue("NgayXinGiaHan", ref _NgayXinGiaHan, value);
            }
        }

        [ModelDefault("Caption", "Lý do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }
        [ModelDefault("Caption", "Số công văn")]
        public string SoCongVan
        {
            get
            {
                return _SoCongVan;
            }
            set
            {
                SetPropertyValue("SoCongVan", ref _SoCongVan, value);
            }
        }
        [ModelDefault("Caption", "Không hưởng lương")]
        public bool KhongHuongLuong
        {
            get
            {
                return _KhongHuongLuong;
            }
            set
            {
                SetPropertyValue("KhongHuongLuong", ref _KhongHuongLuong, value);
                if (!IsLoading && KhongHuongLuong && ThongTinNhanVien != null)
                {
                    this.TinhTrangCu = ThongTinNhanVien.TinhTrang;
                }
                else
                {
                    this.TinhTrangCu = null;
                }
            }
        }

        [Browsable(false)]
        public TinhTrang TinhTrangCu
        {
            get
            {
                return _TinhTrangCu;
            }
            set
            {
                SetPropertyValue("TinhTrangCu", ref _TinhTrangCu, value);
            }
        }

        public QuyetDinhGiaHanDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhGiaHanDaoTao;
        }

        protected override void AfterNhanVienChanged()
        {
            QuyetDinhDaoTao = null;
            //cập nhật danh sách quyết định
            UpdateQuyetDinhList();

            //lấy quyết định đào tạo mới nhất
            CriteriaOperator filter = CriteriaOperator.Parse("ListChiTietDaoTao[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
            SortProperty sort = new SortProperty("NgayHieuLuc", DevExpress.Xpo.DB.SortingDirection.Descending);
            using (XPCollection<QuyetDinhDaoTao> qdList = new XPCollection<QuyetDinhDaoTao>(Session, filter, sort))
            {
                qdList.TopReturnedObjects = 1;
                if (qdList.Count == 1)
                    QuyetDinhDaoTao = qdList[0];
            }

            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định gia hạn đào tạo"));
        }
        
        [Browsable(false)]
        public XPCollection<QuyetDinhDaoTao> QuyetDinhList { get; set; }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhDaoTao>(Session);
            QuyetDinhList.Criteria = CriteriaOperator.Parse("ListChiTietDaoTao[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
        }

        protected override void OnLoaded()
        {
            base.OnLoading();

            if (GiayToHoSo == null)
            {
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
                if (GiayToList.Count > 0 && SoQuyetDinh != null)
                {
                    GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định", SoQuyetDinh);
                    if (GiayToList.Count > 0)
                        GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
                }
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            //1. kéo dài thời gian đào tạo
            //2. kéo dài mốc nâng lương lần sau (Trừ QNU vẫn giữ nguyên mốc nâng lương lần sau)
            if (!IsDeleted && ThongTinNhanVien != null)
            {
                if (TruongConfig.MaTruong != "QNU")
                {
                    //kéo dài mốc nâng lương điều chỉnh
                    if (ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh == DateTime.MinValue
                        || ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh < ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong)
                    {
                        if (ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau != DateTime.MinValue)
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau.AddMonths(ThoiGianGiaHan);
                            ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh = "Gia hạn đào tạo theo QĐ số " + SoQuyetDinh;
                        }
                    }
                    else
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh.AddMonths(ThoiGianGiaHan);
                        ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh += "; Gia hạn đào tạo theo QĐ số " + SoQuyetDinh;
                    }

                    //luu tru giay to ho so can bo huong dan
                    GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                    GiayToHoSo.SoGiayTo = SoQuyetDinh;
                    GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                    GiayToHoSo.TrichYeu = NoiDung;

                    //
                    if (KhongHuongLuong)
                    {
                        TinhTrang tinhTrang = Session.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ? or TenTinhTrang like ?", "Đi học ngoài nước không hưởng lương", "Đi học nước ngoài không hưởng lương"));
                        if (tinhTrang != null)
                        {
                            if (TuNgay <= HamDungChung.GetServerTime())
                            {

                            }
                        }
                        else
                        {
                            tinhTrang = new TinhTrang(Session);
                            tinhTrang.TenTinhTrang = "Đi học nước ngoài không hưởng lương";
                            tinhTrang.MaQuanLy = "DHNNKHL";
                        }
                        //
                        ThongTinNhanVien.TinhTrang = tinhTrang;
                    }
                }
                else
                {
                    if (KhongHuongLuong)
                    {
                        TinhTrang tinhTrang = Session.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ? or TenTinhTrang like ?", "Đi học ngoài nước không hưởng lương", "Đi học nước ngoài không hưởng lương"));
                        if (tinhTrang != null)
                        {
                            if (TuNgay <= HamDungChung.GetServerTime())
                            {

                            }
                        }
                        else
                        {
                            tinhTrang = new TinhTrang(Session);
                            tinhTrang.TenTinhTrang = "Đi học nước ngoài không hưởng lương";
                            tinhTrang.MaQuanLy = "DHNNKHL";
                        }
                        //
                        ThongTinNhanVien.TinhTrang = tinhTrang;
                    }
                }
            }
        }

        protected override void OnDeleting()
        {
            if (ThongTinNhanVien != null)
            {
                //mốc nâng lương điều chỉnh
                DateTime temp = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong.AddMonths(ThoiGianGiaHan);
                if (temp == ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh)
                {
                    ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh = DateTime.MinValue;
                    ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh = null;
                }
                else
                {
                    ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh.AddMonths(-ThoiGianGiaHan);
                    if (!string.IsNullOrWhiteSpace(ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh))
                    {
                        int length = ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh.IndexOf(';');
                        if (length <= 0)
                            length = ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh.Length;
                        ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh = ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh.Substring(0, length);
                    }
                }

                //xoa giay to
                if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);

                //Trường công nghiệp thì cập nhật lại tình trạng không hưởng lương
                if (TruongConfig.MaTruong.Equals("IUH") && KhongHuongLuong)
                {
                    TinhTrang tinhTrang = Session.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đi học ngoài nước có hưởng lương"));
                    if (tinhTrang != null)
                        ThongTinNhanVien.TinhTrang = tinhTrang;
                }
            }
            base.OnDeleting();
        }
    }
}
