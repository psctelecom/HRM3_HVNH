using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định bổ nhiệm ngạch")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhBoNhiemNgach;ThongTinNhanVien")]
    public class ChiTietQuyetDinhBoNhiemNgach : BaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private QuyetDinhBoNhiemNgach _QuyetDinhBoNhiemNgach;
        private GiayToHoSo _GiayToHoSo;
        private DateTime _NgayBoNhiemNgach;
        private DateTime _NgayHuongLuong;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private decimal _HeSoLuong;
        private DateTime _MocNangLuong;
        private bool _Huong85PhanTramLuong;

        [Browsable(false)]
        [Association("QuyetDinhBoNhiemNgach-ListChiTietQuyetDinhBoNhiemNgach")]
        public QuyetDinhBoNhiemNgach QuyetDinhBoNhiemNgach
        {
            get
            {
                return _QuyetDinhBoNhiemNgach;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiemNgach", ref _QuyetDinhBoNhiemNgach, value);
                if (!IsLoading && value != null)
                {
                    //GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                    //GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                    //GiayToHoSo.LuuTru = value.LuuTru;
                    //GiayToHoSo.TrichYeu = value.NoiDung;

                    NgayHuongLuong = value.NgayHieuLuc;
                    MocNangLuong = value.NgayHieuLuc;
                    NgayBoNhiemNgach = value.NgayHieuLuc;
                }
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
                    NgachLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
                    BacLuong = ThongTinNhanVien.NhanVienThongTinLuong.BacLuong;
                    HeSoLuong = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
                    NgayBoNhiemNgach = ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach;
                    MocNangLuong = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong;
                    NgayHuongLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
                    Huong85PhanTramLuong = ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong;
                    if (GiayToHoSo != null)
                        GiayToHoSo.HoSo = value;
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                }                
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
                if (!IsLoading)
                {
                    BacLuong = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
                if (!IsLoading && value != null)
                {
                    HeSoLuong = value.HeSoLuong;
                }
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayBoNhiemNgach
        {
            get
            {
                return _NgayBoNhiemNgach;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgach", ref _NgayBoNhiemNgach, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime MocNangLuong
        {
            get
            {
                return _MocNangLuong;
            }
            set
            {
                SetPropertyValue("MocNangLuong", ref _MocNangLuong, value);
            }
        }


        [ModelDefault("Caption", "Hưởng 85 phần trăm")]
        public bool Huong85PhanTramLuong
        {
            get
            {
                return _Huong85PhanTramLuong;
            }
            set
            {
                SetPropertyValue("Huong85PhanTramLuong", ref _Huong85PhanTramLuong, value);
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

        public ChiTietQuyetDinhBoNhiemNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định bổ nhiệm chức danh nghề nghiệp"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (QuyetDinhBoNhiemNgach != null
                && !IsLoading
                && !QuyetDinhBoNhiemNgach.IsDirty)
                QuyetDinhBoNhiemNgach.IsDirty = true;
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

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted &&
                Oid != Guid.Empty && Session is NestedUnitOfWork)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                    ThongTinNhanVien);
                HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                if (QuyetDinhBoNhiemNgach.QuyetDinhMoi)
                {
                    //1. chỉ phát sinh biến động khi nhân viên tham gia bảo hiểm xã hội
                    //tăng mức đóng
                    if (hoSoBaoHiem != null &&
                        QuyetDinhBoNhiemNgach.NgayPhatSinhBienDong != DateTime.MinValue)
                    {
                        BienDongHelper.CreateBienDongThayDoiLuong(Session, QuyetDinhBoNhiemNgach, BoPhan, ThongTinNhanVien, QuyetDinhBoNhiemNgach.NgayPhatSinhBienDong, HeSoLuong, ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu, 0, ThongTinNhanVien.NhanVienThongTinLuong.ThamNien, ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac, false);
                    }

                    //2. cập nhật thông tin
                    ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong = HeSoLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = NgayBoNhiemNgach;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = MocNangLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = false;
                    ThongTinNhanVien.LoaiNhanVien = HoSoHelper.HopDongCoThoiHan(Session);
                }

                //3. tạo diễn biến lương
                QuaTrinhHelper.CreateDienBienLuong(Session, QuyetDinhBoNhiemNgach, ThongTinNhanVien, NgayHuongLuong,this);
                
                //4. quá trình tham gia Bảo hiểm xã hội
                if (hoSoBaoHiem != null && NgayHuongLuong != DateTime.MinValue)
                {
                    QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(Session, QuyetDinhBoNhiemNgach, hoSoBaoHiem, NgayHuongLuong);
                }
            }
        }

        protected override void OnDeleting()
        {
            //1. xóa biến động
            BienDongHelper.DeleteBienDong<BienDong_ThayDoiLuong>(Session, ThongTinNhanVien, QuyetDinhBoNhiemNgach.NgayPhatSinhBienDong);

            //2. trả lại dữ liệu
            if (QuyetDinhBoNhiemNgach.QuyetDinhMoi)
            {
                ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = true;
                ThongTinNhanVien.LoaiNhanVien = HoSoHelper.TapSu(Session);
            }

            //3. xóa diễn biến lương
            QuaTrinhHelper.DeleteQuaTrinh<DienBienLuong>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhBoNhiemNgach.Oid, ThongTinNhanVien.Oid));

            //4. xóa quá trình bhxh
            CriteriaOperator filter = CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?",
                ThongTinNhanVien, NgayHuongLuong);
            QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhThamGiaBHXH>(Session, filter);

            //xóa giấy tờ
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }
    }

}
