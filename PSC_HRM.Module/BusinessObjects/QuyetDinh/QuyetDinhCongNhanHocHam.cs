using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định công nhận học hàm")]
    [Appearance("UnHideLUH", TargetItems = "HSPCChuyenMonCu;HSPCChuyenMonMoi;NgayHuongHSPCChuyenMonMoi", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'LUH'")]
    [Appearance("HideNEU", TargetItems = "BoPhan;ThongTinNhanVien;HocHam;TuNgay", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong='NEU'")]
    [Appearance("UnHideNEU", TargetItems = "ListChiTietCongNhanHocHam", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong!='NEU'")]
    public class QuyetDinhCongNhanHocHam : QuyetDinhCaNhan
    {
        // Fields...
        private DateTime _TuNgay;
        private HocHam _HocHam;
        private ChucDanh _ChucDanh;
        private bool _QuyetDinhMoi;
        private decimal _HSPCChuyenMonCu;
        private decimal _HSPCChuyenMonMoi;
        private DateTime _NgayHuongHSPCChuyenMonMoi;

        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'NEU'")]
        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get
            {
                return _HocHam;
            }
            set
            {
                SetPropertyValue("HocHam", ref _HocHam, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'NEU'")]
        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
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

        [ModelDefault("Caption", "HSPC chuyên môn cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChuyenMonCu
        {
            get
            {
                return _HSPCChuyenMonCu;
            }
            set
            {
                SetPropertyValue("HSPCChuyenMonCu", ref _HSPCChuyenMonCu, value);
            }
        }

        [ModelDefault("Caption", "HSPC chuyên môn mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChuyenMonMoi
        {
            get
            {
                return _HSPCChuyenMonMoi;
            }
            set
            {
                SetPropertyValue("HSPCChuyenMonMoi", ref _HSPCChuyenMonMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HSPC chuyên môn mới")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NgayHuongHSPCChuyenMonMoi
        {
            get
            {
                return _NgayHuongHSPCChuyenMonMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCChuyenMonMoi", ref _NgayHuongHSPCChuyenMonMoi, value);
            }
        }

        //Dùng để lưu vết
        [Browsable(false)]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhCongNhanHocHam-ListChiTietCongNhanHocHam")]
        public XPCollection<ChiTietCongNhanHocHam> ListChiTietCongNhanHocHam
        {
            get
            {
                return GetCollection<ChiTietCongNhanHocHam>("ListChiTietCongNhanHocHam");
            }
        }

        public QuyetDinhCongNhanHocHam(Session session) : base(session) { }
   
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhCongNhanHocHam;

            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định công nhận học hàm (GS, PGS)"));
        }

        //protected override void AfterNhanVienChanged()
        //{
        //    HSPCChuyenMonCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChuyenMon;
        //}

        protected override void OnLoaded()
        {
            base.OnLoading();

            //if (GiayToHoSo == null)
            //{
            //    GiayToList = ThongTinNhanVien.ListGiayToHoSo;
            //    if (GiayToList.Count > 0 && SoQuyetDinh != null)
            //    {
            //        GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định công nhận học hàm (GS, PGS)", SoQuyetDinh);
            //        if (GiayToList.Count > 0)
            //            GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
            //    }
            //}

            if (ThongTinNhanVien == null && ListChiTietCongNhanHocHam.Count > 0)
            {
                ThongTinNhanVien = ListChiTietCongNhanHocHam[0].ThongTinNhanVien;
                BoPhan = ListChiTietCongNhanHocHam[0].BoPhan;
                TuNgay = ListChiTietCongNhanHocHam[0].NgayCongNhanMoi;
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && ThongTinNhanVien != null && MaTruong != "NEU")
            {
                if (QuyetDinhMoi)
                {
                    //update hoc ham
                    CriteriaOperator filter = CriteriaOperator.Parse("Oid like ?", ThongTinNhanVien.Oid);
                    NhanVien nhanVien = Session.FindObject<NhanVien>(filter);
                    NhanVienTrinhDo trinhDo = Session.FindObject<NhanVienTrinhDo>(CriteriaOperator.Parse("Oid = ?", nhanVien.NhanVienTrinhDo));
                    trinhDo.HocHam = HocHam;
                    trinhDo.NamCongNhanHocHam = TuNgay.Year;
                    trinhDo.NgayCongTac = NgayHieuLuc;
                    trinhDo.NgayCongNhanHocHam = TuNgay;
                    trinhDo.NgayHuongCheDo = TuNgay;
                    trinhDo.NgayPhongDanhHieu = TuNgay;

                    //update chuc danh
                        ChucDanh = nhanVien.ChucDanh;
                        if (MaTruong != "QNU")
                        {
                        TrinhDoChuyenMon trinhDoChuyenMon = Session.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("MaQuanLy like ?", "TS"));
                        if (trinhDo.TrinhDoChuyenMon.Oid == trinhDoChuyenMon.Oid && HocHam.TenHocHam == "Giáo sư")
                            nhanVien.ChucDanh = Session.FindObject<ChucDanh>(CriteriaOperator.Parse("MaQuanLy like ?", "GS.TS"));
                        else
                            nhanVien.ChucDanh = Session.FindObject<ChucDanh>(CriteriaOperator.Parse("MaQuanLy like ?", "PGS.TS"));
                    }
                }            

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietCongNhanHocHam.Count == 1)
                {
                    BoPhanText = ListChiTietCongNhanHocHam[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietCongNhanHocHam[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
        }

        protected override void OnDeleting()
        {
            if (ThongTinNhanVien != null && MaTruong != "NEU")
            {
                if (QuyetDinhMoi)
                {
                    //xoa hoc ham
                    CriteriaOperator filter = CriteriaOperator.Parse("Oid like ?", ThongTinNhanVien.Oid);
                    NhanVien nhanVien = Session.FindObject<NhanVien>(filter);
                    NhanVienTrinhDo trinhDo = Session.FindObject<NhanVienTrinhDo>(CriteriaOperator.Parse("Oid = ?", nhanVien.NhanVienTrinhDo));
                    trinhDo.HocHam = null;
                    trinhDo.NamCongNhanHocHam = 0;
                    trinhDo.NgayCongTac = DateTime.MinValue;
                    trinhDo.NgayCongNhanHocHam = DateTime.MinValue;
                    trinhDo.NgayHuongCheDo = DateTime.MinValue;
                    trinhDo.NgayPhongDanhHieu = DateTime.MinValue;

                    //update chuc danh                    
                    nhanVien.ChucDanh = ChucDanh;
                                      
                }              
            }

            base.OnDeleting();
        }
    }

}
