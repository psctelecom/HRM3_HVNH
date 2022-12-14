using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.GiayTo;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.TapSu;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Chi tiết thông báo gia hạn tập sự")]
    public class ChiTietThongBaoGiaHanTapSu : BaseObject
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _NgayBatDauTapSu;
        private DateTime _NgayKetThucTapSu;
        private int _ThoiGianGiaHan;
        private DateTime _NgayDuocGiaHan;
        private string _LyDo;
        private GiayToHoSo _GiayToHoSo;
        private ThongBaoGiaHanTapSu _ThongBaoGiaHanTapSu;

        [Browsable(false)]
        [Association("ThongBaoGiaHanTapSu-ListChiTietThongBaoGiaHanTapSu")]
        public ThongBaoGiaHanTapSu ThongBaoGiaHanTapSu
        {
            get
            {
                return _ThongBaoGiaHanTapSu;
            }
            set
            {
                SetPropertyValue("ThongBaoGiaHanTapSu", ref _ThongBaoGiaHanTapSu, value);
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
                }
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
        
        [ModelDefault("Caption", "Ngày bắt đầu tập sự")]
        public DateTime NgayBatDauTapSu
        {
            get
            {
                return _NgayBatDauTapSu;
            }
            set
            {
                SetPropertyValue("NgayBatDauTapSu", ref _NgayBatDauTapSu, value);
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc tập sự")]
        public DateTime NgayKetThucTapSu
        {
            get
            {
                return _NgayKetThucTapSu;
            }
            set
            {
                SetPropertyValue("NgayKetThucTapSu", ref _NgayKetThucTapSu, value);
            }
        }

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Thời gian gia hạn (tháng)")]
        public int ThoiGianGiaHan
        {
            get
            {
                return _ThoiGianGiaHan;
            }
            set
            {
                SetPropertyValue("ThoiGianGiaHan", ref _ThoiGianGiaHan, value);
                if (!IsLoading && value > 0)
                {
                    TinhNgayHetHanTapSu();
                }
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Ngày được gia hạn")]
        public DateTime NgayDuocGiaHan
        {
            get
            {
                return _NgayDuocGiaHan;
            }
            set
            {
                SetPropertyValue("NgayDuocGiaHan", ref _NgayDuocGiaHan, value);
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

        [Aggregated]
        [Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
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

        public ChiTietThongBaoGiaHanTapSu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            ThoiGianGiaHan = 12;
        }

        public void AfterNhanVienChanged()
        {
            if (ThongBaoGiaHanTapSu.QuyetDinhHuongDanTapSu == null)
            {
                if (ThongBaoGiaHanTapSu.QuyetDinhList == null)
                    ThongBaoGiaHanTapSu.QuyetDinhList = new XPCollection<QuyetDinhHuongDanTapSu>(Session);

                ThongBaoGiaHanTapSu.QuyetDinhList.Criteria = CriteriaOperator.Parse("ListChiTietQuyetDinhHuongDanTapSu[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
                ThongBaoGiaHanTapSu.QuyetDinhHuongDanTapSu = Session.FindObject<QuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("ListChiTietQuyetDinhHuongDanTapSu[ThongTinNhanVien=?]", ThongTinNhanVien.Oid));
            }

            if (ThongBaoGiaHanTapSu.QuyetDinhHuongDanTapSu != null)
            {
                ChiTietQuyetDinhHuongDanTapSu tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", ThongBaoGiaHanTapSu.QuyetDinhHuongDanTapSu.Oid, ThongTinNhanVien.Oid));
                if (tapSu != null)
                {
                    NgayBatDauTapSu = tapSu.TuNgay;
                    NgayKetThucTapSu = tapSu.DenNgay;
                }
                TinhNgayHetHanTapSu();
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoading();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && ThongBaoGiaHanTapSu.QuyetDinhHuongDanTapSu != null)
            {
                ChiTietQuyetDinhHuongDanTapSu tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", ThongBaoGiaHanTapSu.QuyetDinhHuongDanTapSu.Oid, ThongTinNhanVien.Oid));
                if (tapSu != null && tapSu.CanBoHuongDan is ThongTinNhanVien)
                {
                    //can bo huong dan se khong duoc huong HSPC trach nhiem
                    tapSu.CanBoHuongDan.NhanVienThongTinLuong.HSPCTrachNhiem = 0;
                }
            }
        }

        protected override void OnDeleting()
        {
            ChiTietQuyetDinhHuongDanTapSu tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", ThongBaoGiaHanTapSu.QuyetDinhHuongDanTapSu.Oid, ThongTinNhanVien.Oid));
            if (tapSu != null && tapSu.CanBoHuongDan is ThongTinNhanVien)
            {
                //khoi phuc lai HSPC trach nhiem cua can bo huong dan
                tapSu.CanBoHuongDan.NhanVienThongTinLuong.HSPCTrachNhiem = tapSu.QuyetDinhHuongDanTapSu.HSPCTrachNhiem;
                //xoa giay to
                if (!String.IsNullOrWhiteSpace(ThongBaoGiaHanTapSu.SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, (ThongTinNhanVien)tapSu.CanBoHuongDan, ThongBaoGiaHanTapSu.SoQuyetDinh);
            }
            base.OnDeleting();
        }

        private void TinhNgayHetHanTapSu()
        {
            if (ThongTinNhanVien != null)
            {
                QuyetDinhTamHoanTapSu quyetDinh = Session.FindObject<QuyetDinhTamHoanTapSu>(CriteriaOperator.Parse("ThongTinNhanVien = ?", ThongTinNhanVien.Oid));
                if (quyetDinh != null)
                {
                    NgayDuocGiaHan = quyetDinh.TapSuDenNgay.AddMonths(ThoiGianGiaHan);
                }
                else
                {
                    ChiTietQuyetDinhHuongDanTapSu tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", ThongBaoGiaHanTapSu.QuyetDinhHuongDanTapSu.Oid, ThongTinNhanVien.Oid));
                    if (tapSu != null)
                        NgayDuocGiaHan = tapSu.DenNgay.AddMonths(ThoiGianGiaHan);
                }
            }
        }
    }
}
