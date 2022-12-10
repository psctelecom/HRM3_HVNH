using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.NangThamNien;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định nâng phụ cấp thâm niên")]
    //[RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhNangPhuCapThamNienNhaGiao;ThongTinNhanVien")]
    public class ChiTietQuyetDinhNangPhuCapThamNienNhaGiao : BaseObject
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private QuyetDinhNangPhuCapThamNienNhaGiao _QuyetDinhNangPhuCapThamNienNhaGiao;
        private GiayToHoSo _GiayToHoSo;
        private DateTime _NgayHuongThamNienCu;
        private DateTime _NgayHuongThamNienMoi;
        private decimal _ThamNienMoi;
        private decimal _ThamNienCu;
        private NgachLuong _NgachLuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định nâng phụ cấp thâm niên nhà giáo")]
        [Association("QuyetDinhNangPhuCapThamNienNhaGiao-ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao")]
        public QuyetDinhNangPhuCapThamNienNhaGiao QuyetDinhNangPhuCapThamNienNhaGiao
        {
            get
            {
                return _QuyetDinhNangPhuCapThamNienNhaGiao;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangPhuCapThamNienNhaGiao", ref _QuyetDinhNangPhuCapThamNienNhaGiao, value);
                //if (!IsLoading && value != null)
                //{
                //    GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                //    GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                //    GiayToHoSo.LuuTru = value.LuuTru;
                //    GiayToHoSo.TrichYeu = value.NoiDung;
                //}
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
                    NgachLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
                    ThamNienCu = ThongTinNhanVien.NhanVienThongTinLuong.ThamNien;
                    NgayHuongThamNienCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongThamNien;
                    ThamNienMoi = ThamNienCu == 0 ? 5 : ThamNienCu + 1;
                    NgayHuongThamNienMoi = ThamNienCu == 0 ? NgayHuongThamNienCu.AddYears(5) : NgayHuongThamNienCu.AddYears(1);
                }                
            }
        }

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
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "% Thâm niên cũ")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal ThamNienCu
        {
            get
            {
                return _ThamNienCu;
            }
            set
            {
                SetPropertyValue("ThamNienCu", ref _ThamNienCu, value);
                if (!IsLoading && value != null)
                {
                    ThamNienMoi = ThamNienCu == 0 ? 5 : ThamNienCu + 1;
                    NgayHuongThamNienMoi = ThamNienCu == 0 ? NgayHuongThamNienCu.AddYears(5) : NgayHuongThamNienCu.AddYears(1);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày hưởng thâm niên cũ")]
        public DateTime NgayHuongThamNienCu
        {
            get
            {
                return _NgayHuongThamNienCu;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNienCu", ref _NgayHuongThamNienCu, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    NgayHuongThamNienMoi = ThamNienCu == 0 ? NgayHuongThamNienCu.AddYears(5) : NgayHuongThamNienCu.AddYears(1);
                }
            }
        }

        [ModelDefault("Caption", "% Thâm niên mới")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal ThamNienMoi
        {
            get
            {
                return _ThamNienMoi;
            }
            set
            {
                SetPropertyValue("ThamNienMoi", ref _ThamNienMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng thâm niên mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongThamNienMoi
        {
            get
            {
                return _NgayHuongThamNienMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNienMoi", ref _NgayHuongThamNienMoi, value);
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

        public ChiTietQuyetDinhNangPhuCapThamNienNhaGiao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định nâng thâm niên"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (QuyetDinhNangPhuCapThamNienNhaGiao != null
                && !IsLoading
                && !QuyetDinhNangPhuCapThamNienNhaGiao.IsDirty)
                QuyetDinhNangPhuCapThamNienNhaGiao.IsDirty = true;
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

            if (!IsDeleted
                && Oid != Guid.Empty && Session is NestedUnitOfWork)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                    ThongTinNhanVien);
                HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                if (QuyetDinhNangPhuCapThamNienNhaGiao.QuyetDinhMoi)
                {
                    //phát sinh tăng mức đóng bảo hiểm
                    if (hoSoBaoHiem != null &&
                        QuyetDinhNangPhuCapThamNienNhaGiao.NgayPhatSinhBienDong != DateTime.MinValue)
                        BienDongHelper.CreateBienDongThayDoiLuong(Session, 
                            QuyetDinhNangPhuCapThamNienNhaGiao,
                            BoPhan, 
                            ThongTinNhanVien,
                            QuyetDinhNangPhuCapThamNienNhaGiao.NgayPhatSinhBienDong,
                            ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong,
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu,
                            ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung,
                            ThamNienMoi, 
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac,
                            ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong                           
                            );

                    //cập nhật thâm niên
                    if (NgayHuongThamNienMoi <= HamDungChung.GetServerTime())
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.ThamNien = ThamNienMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongThamNien = NgayHuongThamNienMoi;
                    }
                }


                //update dien bien luong
                filter = CriteriaOperator.Parse("ThongTinNhanVien=? and ThamNienCu=?",
                    ThongTinNhanVien, ThamNienCu);
                ChiTietQuyetDinhNangPhuCapThamNienNhaGiao chiTiet = Session.FindObject<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>(filter);
                if (chiTiet != null)
                {
                    filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?",
                        chiTiet.QuyetDinhNangPhuCapThamNienNhaGiao.Oid, ThongTinNhanVien.Oid);
                    DienBienLuong updateDienBienLuong = Session.FindObject<DienBienLuong>(filter);
                    if (updateDienBienLuong != null)
                    {
                        updateDienBienLuong.DenNgay = NgayHuongThamNienMoi.AddDays(-1);
                    }
                }

                if (NgayHuongThamNienMoi != DateTime.MinValue)
                {
                    //tạo mới diễn biến lương
                    QuaTrinhHelper.CreateDienBienLuong(Session, QuyetDinhNangPhuCapThamNienNhaGiao, ThongTinNhanVien, NgayHuongThamNienMoi,this);

                    //Bảo hiểm xã hội
                    if (hoSoBaoHiem != null)
                        QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(Session, QuyetDinhNangPhuCapThamNienNhaGiao, hoSoBaoHiem, NgayHuongThamNienMoi);
                }
            }
        }

        protected override void OnDeleting()
        {
            //lấy lại dữ liệu cũ
            if (QuyetDinhNangPhuCapThamNienNhaGiao.QuyetDinhMoi)
            {
                ThongTinNhanVien.NhanVienThongTinLuong.ThamNien = ThamNienCu;
                ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongThamNien = NgayHuongThamNienCu;
            }

            ////xóa biến động
            //BienDongHelper.DeleteBienDong<BienDong_ThayDoiLuong>(Session, ThongTinNhanVien, QuyetDinhNangPhuCapThamNienNhaGiao.NgayPhatSinhBienDong);

            if (NgayHuongThamNienMoi != DateTime.MinValue)
            {
                //xóa diễn biến lương
                QuaTrinhHelper.DeleteQuaTrinh<DienBienLuong>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhNangPhuCapThamNienNhaGiao, ThongTinNhanVien));

                //xóa quá trình bhxh
                QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhThamGiaBHXH>(Session, CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?", ThongTinNhanVien.Oid, Convert.ToDateTime(NgayHuongThamNienMoi.ToString("01/MM/yyyy"))));
            }

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
