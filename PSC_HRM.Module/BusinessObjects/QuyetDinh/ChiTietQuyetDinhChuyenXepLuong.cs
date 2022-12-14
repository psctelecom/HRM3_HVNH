using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoHiem;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định chuyển xếp lương")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhChuyenXepLuong;ThongTinNhanVien")]

    public class ChiTietQuyetDinhChuyenXepLuong : TruongBaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private QuyetDinhChuyenXepLuong _QuyetDinhChuyenXepLuong;
        private GiayToHoSo _GiayToHoSo;
        private decimal _MucLuongCu;
        private decimal _MucLuongMoi;
        private DateTime _NgayHuongLuongCu;
        private DateTime _NgayHuongLuongMoi;
        private bool _QuyetDinhMoi;
        private string _LyDo;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định nâng lương")]
        [Association("QuyetDinhChuyenXepLuong-ListChiTietQuyetDinhchuyenXepLuong")]
        public QuyetDinhChuyenXepLuong QuyetDinhChuyenXepLuong
        {
            get
            {
                return _QuyetDinhChuyenXepLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhChuyenXepLuong", ref _QuyetDinhChuyenXepLuong, value);
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
                    BoPhanText = value.TenBoPhan;
                }
            }
        }
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhanText
        {
            get
            {
                return _BoPhanText;
            }
            set
            {
                SetPropertyValue("BoPhanText", ref _BoPhanText, value);
            }
        }


        [ImmediatePostData]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Cán bộ")]
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
                    if (GiayToHoSo != null)
                        GiayToHoSo.HoSo = value;
                    AfterNhanVienChanged();
                }                
            }
        }

        [ModelDefault("Caption", "Mức lương cũ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal MucLuongCu
        {
            get
            {
                return _MucLuongCu;
            }
            set
            {

                SetPropertyValue("MucLuongCu", ref _MucLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
        public DateTime NgayHuongLuongCu
        {
            get
            {
                return _NgayHuongLuongCu;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongCu", ref _NgayHuongLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Mức lương mới")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal MucLuongMoi
        {
            get
            {
                return _MucLuongMoi;
            }
            set
            {

                SetPropertyValue("MucLuongMoi", ref _MucLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương mới")]
        public DateTime NgayHuongLuongMoi
        {
            get
            {
                return _NgayHuongLuongMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongMoi", ref _NgayHuongLuongMoi, value);
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


        //[Aggregated]
        //[Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        //[ExpandObjectMembers(ExpandObjectMembers.Never)]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        [DataSourceProperty("GiayToList", DataSourcePropertyIsNullMode.SelectAll)]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
            }
        }

        [Browsable(false)]
        [NonPersistent]
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

        public ChiTietQuyetDinhChuyenXepLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //MaTruong = TruongConfig.MaTruong;
            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định chuyển xếp lương"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
            
        }
        
        
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            //if (TruongConfig.MaTruong.Equals("NEU"))
            //{
            //    if (QuyetDinhChuyenXepLuong != null
            //        && !IsLoading
            //        && !QuyetDinhChuyenXepLuong.IsDirty)
            //        QuyetDinhChuyenXepLuong.IsDirty = true;
            //}
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
            UpdateGiayToList();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            } 
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }        

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        [Browsable(false)]
        public XPCollection<GiayToHoSo> GiayToList { get; set; }
        private void UpdateGiayToList()
        {
            //if (GiayToList == null)
            //    GiayToList = new XPCollection<GiayToHoSo>(Session);

            if (ThongTinNhanVien != null)
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
            //    GiayToList.Criteria = CriteriaOperator.Parse("HoSo=? and GiayTo.TenGiayTo like ?", ThongTinNhanVien.Oid, "%Quyết định%");
        }

        private void AfterNhanVienChanged()
        {
         
            MucLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan;
            NgayHuongLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;


            
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted
                && Oid != Guid.Empty && Session is NestedUnitOfWork)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                    ThongTinNhanVien);
                HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                if (QuyetDinhChuyenXepLuong.QuyetDinhMoi)
                {
                   
                    //cập nhật thông tin vào hồ sơ
                    if (NgayHuongLuongMoi <= HamDungChung.GetServerTime())
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan = MucLuongMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongMoi;
                    }
                }


                //update dien bien luong
                if (NgayHuongLuongMoi != DateTime.MinValue && ((MucLuongMoi != null)))
                {
                    filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien.Oid);

                    ChiTietQuyetDinhChuyenXepLuong chiTiet = Session.FindObject<ChiTietQuyetDinhChuyenXepLuong>(filter);
                    if (chiTiet != null)
                    {
                        filter = CriteriaOperator.Parse("QuyetDinh = ?",
                            chiTiet.QuyetDinhChuyenXepLuong.Oid);
                        DienBienLuong updateDienBienLuong = Session.FindObject<DienBienLuong>(filter);
                        if (updateDienBienLuong != null)
                            updateDienBienLuong.DenNgay = NgayHuongLuongMoi.AddDays(-1);
                    }
                }

                //tạo mới diễn biến lương
                if (NgayHuongLuongMoi != DateTime.MinValue)
                {
                    QuaTrinhHelper.CreateDienBienLuong(Session, QuyetDinhChuyenXepLuong, ThongTinNhanVien, NgayHuongLuongMoi,this);

                    //Bảo hiểm xã hội
                    if (hoSoBaoHiem != null)
                        QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(Session, QuyetDinhChuyenXepLuong, hoSoBaoHiem, NgayHuongLuongMoi);
                }
            }
        }

        protected override void OnDeleting()
        {
            if (NgayHuongLuongMoi != DateTime.MinValue)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and NgayHuongLuongMoi>?",
                    ThongTinNhanVien, NgayHuongLuongMoi);
                if (QuyetDinhChuyenXepLuong.QuyetDinhMoi)
                {
                    SortProperty sort = new SortProperty("QuyetDinhChuyenXepLuong.NgayHieuLuc", SortingDirection.Descending);
                    using (XPCollection<ChiTietQuyetDinhChuyenXepLuong> qdList = new XPCollection<ChiTietQuyetDinhChuyenXepLuong>(Session, filter, sort))
                    {
                        qdList.TopReturnedObjects = 1;
                        if (qdList.Count == 0 ||
                            (qdList.Count == 1 && qdList[0].QuyetDinhChuyenXepLuong == QuyetDinhChuyenXepLuong))
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan = MucLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongCu;
                           
                        }
                    }
                }
                //Xóa diễn biến lương
                QuaTrinhHelper.DeleteQuaTrinh<DienBienLuong>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhChuyenXepLuong, ThongTinNhanVien));

                //xóa quá trình bhxh
                QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhThamGiaBHXH>(Session, CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?", ThongTinNhanVien, NgayHuongLuongMoi));
            }
            //xóa biến động
            if (QuyetDinhChuyenXepLuong.NgayPhatSinhBienDong != DateTime.MinValue)
                BienDongHelper.DeleteBienDong<BienDong_ThayDoiLuong>(Session, ThongTinNhanVien, QuyetDinhChuyenXepLuong.NgayPhatSinhBienDong);

            //xóa giấy tờ hồ sơ
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }
    }

}
