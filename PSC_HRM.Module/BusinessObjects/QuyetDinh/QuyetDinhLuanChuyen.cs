using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.NghiPhep;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định điều động")]
    [Appearance("Hide_KhacNEU", TargetItems = "BoPhanMoiText", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'NEU'")]
    public class QuyetDinhLuanChuyen : QuyetDinhCaNhan
    {
        private BoPhan _BoPhanMoi;
        private CongViec _CongViecCu;
        private bool _QuyetDinhMoi;
        private ChucVu _ChucVuCu;
        private ChucVu _ChucVuMoi;
        //
        private string _BoPhanMoiText;

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị mới")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhanMoi
        {
            get
            {
                return _BoPhanMoi;
            }
            set
            {
                SetPropertyValue("BoPhanMoi", ref _BoPhanMoi, value);
            }
        }
        
        [ModelDefault("Caption", "Đơn vị mới(text)")]
        public string BoPhanMoiText
        {
            get
            {
                return _BoPhanMoiText;
            }
            set
            {
                SetPropertyValue("BoPhanMoiText", ref _BoPhanMoiText, value);
            }
        }
        [Browsable(false)]
        public CongViec CongViecCu
        {
            get
            {
                return _CongViecCu;
            }
            set
            {
                SetPropertyValue("CongViecCu", ref _CongViecCu, value);
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

        [ModelDefault("Caption", "Chức vụ mới")]
        public ChucVu ChucVuMoi
        {
            get
            {
                return _ChucVuMoi;
            }
            set
            {
                SetPropertyValue("ChucVuMoi", ref _ChucVuMoi, value);
            }
        }

        [Browsable(false)]
        public ChucVu ChucVuCu
        {
            get
            {
                return _ChucVuCu;
            }
            set
            {
                SetPropertyValue("ChucVuCu", ref _ChucVuCu, value);
            }
        }

        public QuyetDinhLuanChuyen(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhLuanChuyen;

            MaTruong = TruongConfig.MaTruong;
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định luân chuyển"));
        }

        protected override void AfterNhanVienChanged()
        {
            base.AfterNhanVienChanged();
            BoPhan = ThongTinNhanVien.BoPhan;
            CongViecCu = ThongTinNhanVien.CongViecDuocGiao;
            //HBU
            ChucVuCu = ThongTinNhanVien.ChucVu;
            ChucVuMoi = ChucVuMoi;
     
        }

        protected override void OnLoaded()
        {
            base.OnLoading();
            MaTruong = TruongConfig.MaTruong;

            if (GiayToHoSo == null && ThongTinNhanVien != null)
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

            if (TruongConfig.MaTruong.Equals("QNU"))
            {
                if (!IsDeleted && ThongTinNhanVien != null)
                {

                    if (QuyetDinhMoi)
                    {
                        if (NgayHieuLuc <= HamDungChung.GetServerTime())
                        {
                            UpdateHoSoBaoHiem(this);
                            UpdateQuanLyPhep(this);
                            ThongTinNhanVien.BoPhan = BoPhanMoi;
                            //HBU
                            ThongTinNhanVien.ChucVu = ChucVuMoi;
                        }

                        //quan ly dieu dong
                        QuaTrinhHelper.CreateQuaTrinhLuanChuyen(Session, this, BoPhan, BoPhanMoi, ChucVuMoi, NgayHieuLuc);

                        //qua trinh cong tac
                        QuaTrinhHelper.UpdateQuaTrinhCongTac(Session, this, NgayQuyetDinh);
                        QuaTrinhHelper.CreateQuaTrinhCongTac(Session, this,
                           string.Format("{0} {1}, {2}", ChucVuMoi == null ? "" : ChucVuMoi.TenChucVu, ThongTinNhanVien.BoPhan.TenBoPhan, ThongTinNhanVien.ThongTinTruong));

                        //luu tru giay to ho so can bo huong dan
                        GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                        GiayToHoSo.SoGiayTo = SoQuyetDinh;
                        GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                        GiayToHoSo.TrichYeu = NoiDung;

                    }
                }
            }
            else
            {
                if (!IsDeleted && ThongTinNhanVien != null)
                {

                    if (QuyetDinhMoi)
                    {
                        UpdateHoSoBaoHiem(this);
                        ThongTinNhanVien.BoPhan = BoPhanMoi;
                        //HBU
                        ThongTinNhanVien.ChucVu = ChucVuMoi;

                    }

                    //quan ly dieu dong
                    QuaTrinhHelper.CreateQuaTrinhLuanChuyen(Session, this, BoPhan, BoPhanMoi, ChucVuMoi, NgayHieuLuc);

                    //qua trinh cong tac
                    QuaTrinhHelper.UpdateQuaTrinhCongTac(Session, this, NgayQuyetDinh);
                    QuaTrinhHelper.CreateQuaTrinhCongTac(Session, this,
                       string.Format("{0} {1}, {2}", ChucVuMoi == null ? "" : ChucVuMoi.TenChucVu, ThongTinNhanVien.BoPhan.TenBoPhan, ThongTinNhanVien.ThongTinTruong));

                    //luu tru giay to ho so can bo huong dan
                    GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                    GiayToHoSo.SoGiayTo = SoQuyetDinh;
                    GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                    GiayToHoSo.TrichYeu = NoiDung;
                }
            }
           
        }

        protected override void OnDeleting()
        {
            RecoverData();
            base.OnDeleting();
        }

        private void RecoverData()
        {
            if (ThongTinNhanVien != null)
            {
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                CriteriaOperator filter = CriteriaOperator.Parse("Oid=?", Oid);
                using (XPCollection<QuyetDinhLuanChuyen> quyetdinh = new XPCollection<QuyetDinhLuanChuyen>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    if (quyetdinh.Count > 0)
                    {
                        if (quyetdinh[0] == This && QuyetDinhMoi)
                        {
                            DeleteHoSoBaoHiem(quyetdinh[0]);
                            DeleteQuanLyPhep(quyetdinh[0]);
                            if (BoPhan != null)
                                ThongTinNhanVien.BoPhan = BoPhan;
                            ThongTinNhanVien.CongViecDuocGiao = CongViecCu;
                            //HBU
                            ThongTinNhanVien.ChucVu = ChucVuCu;
                        }
                    }
                }

                //xoa giay to
                if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);
            }
        }

        //QNU
        private void UpdateHoSoBaoHiem(QuyetDinhLuanChuyen qd)
        {
            if (qd.ThongTinNhanVien != null)
            {
                //SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", qd.ThongTinNhanVien.Oid);
                using (XPCollection<HoSoBaoHiem> hs = new XPCollection<HoSoBaoHiem>(Session, filter))
                {
                    hs.TopReturnedObjects = 1;
                    if (hs.Count > 0)
                    {
                            if (qd.BoPhan != null)
                                hs[0].BoPhan = qd.BoPhanMoi;
                    }
                    
                }
            }
      }

        private void DeleteHoSoBaoHiem(QuyetDinhLuanChuyen qd)
        {
            if (qd.ThongTinNhanVien != null)
            {
                //SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", qd.ThongTinNhanVien.Oid);
                using (XPCollection<HoSoBaoHiem> hs = new XPCollection<HoSoBaoHiem>(Session, filter))
                {
                    hs.TopReturnedObjects = 1;
                    if (hs.Count > 0)
                    {
                        if (qd.BoPhan != null)
                            hs[0].BoPhan = qd.BoPhan;
                    }

                }
            }
        }

        private void UpdateQuanLyPhep(QuyetDinhLuanChuyen qd)
        {
            if (qd.ThongTinNhanVien != null)
            {
                //SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", qd.ThongTinNhanVien.Oid);
                using (XPCollection<ThongTinNghiPhep> p = new XPCollection<ThongTinNghiPhep>(Session, filter))
                {
                    //p.TopReturnedObjects = 1;
                    int temp = 0;
                    while (temp < p.Count)
                    {
                        if (qd.BoPhan != null)
                            p[temp].BoPhan = qd.BoPhanMoi;
                        temp = temp + 1;
                    }

                }
            }
        }
        private void DeleteQuanLyPhep(QuyetDinhLuanChuyen qd)
        {
            if (qd.ThongTinNhanVien != null)
            {
                //SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", qd.ThongTinNhanVien.Oid);
                using (XPCollection<ThongTinNghiPhep> p = new XPCollection<ThongTinNghiPhep>(Session, filter))
                {
                    p.TopReturnedObjects = 1;
                    if (p.Count > 0)
                    {
                        if (qd.BoPhan != null)
                            p[0].BoPhan = qd.BoPhan;
                    }

                }
            }
        }
}
}
