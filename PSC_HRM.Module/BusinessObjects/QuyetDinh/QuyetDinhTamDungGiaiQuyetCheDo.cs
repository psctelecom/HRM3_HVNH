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
    [ModelDefault("Caption", "Quyết định tạm dừng giải quyết các chế độ")]
    
    public class QuyetDinhTamDungGiaiQuyetCheDo : QuyetDinhCaNhan
    {
        // Fields...
        private TinhTrang _TinhTrang;
        private DateTime _NgayPhatSinhBienDong;
        private bool _CoDongBaoHiem;
        private bool _QuyetDinhMoi;
        private DateTime _TuNgay;
        private DateTime _NgayHop;
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
                NgayPhatSinhBienDong = value; 

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
        [ModelDefault("Caption", "Ngày họp")]
        public DateTime NgayHop
        {
            get
            {
                return _NgayHop;
            }
            set
            {
                SetPropertyValue("NgayHop", ref _NgayHop, value);
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

        public QuyetDinhTamDungGiaiQuyetCheDo(Session session) : base(session) { }

        protected override void AfterNhanVienChanged()
        {
            base.AfterNhanVienChanged();

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
