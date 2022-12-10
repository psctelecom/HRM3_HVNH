using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nghỉ không hưởng lương")]
    [Appearance("Hide_QNU", TargetItems = "MocNangLuongDieuChinhMoi", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]
    public class QuyetDinhNghiKhongHuongLuong : QuyetDinhCaNhan
    {
        // Fields...
        private TinhTrang _TinhTrang;
        private DateTime _MocNangLuongDieuChinhMoi;// hiện tại làm bên tiếp nhận nghỉ không lương
        private DateTime _MocNangLuongDieuChinhCu;
        private DateTime _NgayPhatSinhBienDong;
        private bool _CoDongBaoHiem;
        private bool _QuyetDinhMoi;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private DateTime _NgayXinNghi;
        private string _LyDo;

        [Browsable(false)]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!CoDongBaoHiem")]
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
        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                //if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue && !TruongConfig.MaTruong.Equals("QNU"))
                //{
                //    NgayPhatSinhBienDong = value; 
                //    int thang = TuNgay.TinhSoThang(DenNgay);
                //    if (thang > 0)
                //    {
                //        if (MocNangLuongDieuChinhCu != DateTime.MinValue
                //            && ThongTinNhanVien != null
                //            && MocNangLuongDieuChinhCu > ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau)
                //        {
                //            MocNangLuongDieuChinhMoi = MocNangLuongDieuChinhCu.AddMonths(thang); 
                //        }
                //        else
                //        {                            
                //            //MocNangLuongDieuChinhMoi = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong.AddMonths(thang); 
                //            MocNangLuongDieuChinhMoi = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau.AddMonths(thang); 
                //        }
                //    }
                //}
                //else
                //{
                NgayPhatSinhBienDong = value; 
                //}


            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                //if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue && !TruongConfig.MaTruong.Equals("QNU"))
                //{
                //    int thang = TuNgay.TinhSoThang(DenNgay);
                //    if (thang > 0)
                //    {
                //        if (MocNangLuongDieuChinhCu != DateTime.MinValue
                //            && ThongTinNhanVien != null
                //            && MocNangLuongDieuChinhCu > ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau)
                //        {                            
                //           MocNangLuongDieuChinhMoi = MocNangLuongDieuChinhCu.AddMonths(thang); 
                //        }
                //        else
                //        {                           
                //           MocNangLuongDieuChinhMoi = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong.AddMonths(thang); 
                //        }
                //    }
                //}
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương điều chỉnh")]
        public DateTime MocNangLuongDieuChinhMoi
        {
            get
            {
                return _MocNangLuongDieuChinhMoi;
            }
            set
            {
                SetPropertyValue("MocNangLuongDieuChinhMoi", ref _MocNangLuongDieuChinhMoi, value);
            }
        }

        [ModelDefault("Caption", "Có đóng bảo hiểm")]
        public bool CoDongBaoHiem
        {
            get
            {
                return _CoDongBaoHiem;
            }
            set
            {
                SetPropertyValue("CoDongBaoHiem", ref _CoDongBaoHiem, value);
            }
        }

        [ModelDefault("Caption", "Quyết định còn hiệu lực")]
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

        [Browsable(false)]
        [ModelDefault("Caption", "Mốc nâng lương điều chỉnh cũ")]
        public DateTime MocNangLuongDieuChinhCu
        {
            get
            {
                return _MocNangLuongDieuChinhCu;
            }
            set
            {
                SetPropertyValue("MocNangLuongDieuChinhCu", ref _MocNangLuongDieuChinhCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày xin nghỉ")]
        public DateTime NgayXinNghi
        {
            get
            {
                return _NgayXinNghi;
            }
            set
            {
                SetPropertyValue("NgayXinNghi", ref _NgayXinNghi, value);
            }
        }
        
        //lưu vết tình trạng
        [Browsable(false)]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
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

        public QuyetDinhNghiKhongHuongLuong(Session session) : base(session) { }

        protected override void AfterNhanVienChanged()
        {
            base.AfterNhanVienChanged();

            MocNangLuongDieuChinhCu = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh;
            TinhTrang = ThongTinNhanVien.TinhTrang;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhNghiKhongHuongLuong;
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định nghỉ không hưởng lương"));
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

            if (!IsDeleted)
            {
                CriteriaOperator filter;
                if (QuyetDinhMoi)
                {
                    //thiết lập tình trạng
                    if (TuNgay <= HamDungChung.GetServerTime())
                    {
                        filter = CriteriaOperator.Parse("TenTinhTrang like ? or TenTinhTrang like ?", "Nghỉ không hưởng lương", "Nghỉ không lương");
                        TinhTrang tinhTrang = Session.FindObject<TinhTrang>(filter);
                        if (tinhTrang == null)
                        {
                            tinhTrang = new TinhTrang(Session);
                            tinhTrang.MaQuanLy = "NKHL";
                            tinhTrang.TenTinhTrang = "Nghỉ không hưởng lương";
                        }
                        ThongTinNhanVien.TinhTrang = tinhTrang;
                    }
                    //mốc nâng lương điều chỉnh
                    //if (MocNangLuongDieuChinhMoi != DateTime.MinValue && !TruongConfig.MaTruong.Equals("QNU"))
                    //{
                    //    int thang = TuNgay.TinhSoThang(DenNgay);
                    //    ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh = MocNangLuongDieuChinhMoi;
                    //    ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh = string.Concat(ThongTinNhanVien.NhanVienThongTinLuong.LyDoDieuChinh, " Nghỉ không hưởng lương ", thang.ToString(), " tháng.");
                    //} 


                    //quản lý biến động
                    if (!CoDongBaoHiem)
                    {
                        filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                            ThongTinNhanVien);
                        HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);

                        //giảm lao động
                        if (hoSoBaoHiem != null && NgayPhatSinhBienDong != DateTime.MinValue)
                        {
                            BienDongHelper.CreateBienDongGiamLaoDong(Session, this, NgayPhatSinhBienDong, LyDoNghiEnum.NghiKhongLuong);
                        }
                    }
                }

                //luu tru giay to ho so
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.SoGiayTo = SoQuyetDinh;
                GiayToHoSo.NgayLap = NgayQuyetDinh;
                GiayToHoSo.TrichYeu = NoiDung;
                
                filter = CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định");
                DanhMuc.GiayTo giayTo = Session.FindObject<DanhMuc.GiayTo>(filter);
                GiayToHoSo.GiayTo = giayTo;
            }
        }

        protected override void OnDeleting()
        {
            CriteriaOperator filter;
            if (QuyetDinhMoi)
            {
                if (TinhTrang != null)
                    ThongTinNhanVien.TinhTrang = TinhTrang;
                else
                {
                    filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc");
                    ThongTinNhanVien.TinhTrang = Session.FindObject<TinhTrang>(filter);
                }

                if (MocNangLuongDieuChinhCu != DateTime.MinValue)               
                    ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh = MocNangLuongDieuChinhCu;

                //xóa biến động
                if (NgayPhatSinhBienDong != DateTime.MinValue)
                    BienDongHelper.DeleteBienDong<BienDong_GiamLaoDong>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);
            }

            //xoa giay to
            if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);

            base.OnDeleting();
        }
    }

}
