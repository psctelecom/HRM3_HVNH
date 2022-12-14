using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.KhenThuong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định khen thưởng")]
    public class QuyetDinhKhenThuong : QuyetDinh
    {
        private ChiTietDeNghiKhenThuong _DeNghiKhenThuong;
        private NamHoc _NamHoc;
        private DanhHieuKhenThuong _DanhHieuKhenThuong;
        private string _LyDo;
        //private string _LuuTru;
        private decimal _SoTienKhenThuong;
        private string _SoTienBangChu;

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null)
                    UpdateDeNghiKhenThuong();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Danh hiệu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DanhHieuKhenThuong DanhHieuKhenThuong
        {
            get
            {
                return _DanhHieuKhenThuong;
            }
            set
            {
                SetPropertyValue("DanhHieuKhenThuong", ref _DanhHieuKhenThuong, value);
                if (!IsLoading && value != null)
                    UpdateDeNghiKhenThuong();
            }
        }

        [ImmediatePostData]
        [DataSourceProperty("DeNghiList")]
        [ModelDefault("Caption", "Đề nghị khen thưởng")]
        public ChiTietDeNghiKhenThuong DeNghiKhenThuong
        {
            get
            {
                return _DeNghiKhenThuong;
            }
            set
            {
                SetPropertyValue("DeNghiKhenThuong", ref _DeNghiKhenThuong, value);
                if (!IsLoading && value != null)
                {
                    ChiTietKhenThuongNhanVien caNhan;
                    ChiTietKhenThuongBoPhan boPhan;
                    foreach (var item in value.ListChiTietCaNhanDeNghiKhenThuong)
                    {
                        caNhan = Session.FindObject<ChiTietKhenThuongNhanVien>(CriteriaOperator.Parse("QuyetDinhKhenThuong=? and ThongTinNhanVien=?", this, item.ThongTinNhanVien));
                        if (caNhan == null)
                        {
                            caNhan = new ChiTietKhenThuongNhanVien(Session);
                            caNhan.BoPhan = item.BoPhan;
                            caNhan.ThongTinNhanVien = item.ThongTinNhanVien;
                            ListChiTietKhenThuongNhanVien.Add(caNhan);
                        }
                    }
                    foreach (var item in value.ListChiTietTapTheDeNghiKhenThuong)
                    {
                        boPhan = Session.FindObject<ChiTietKhenThuongBoPhan>(CriteriaOperator.Parse("QuyetDinhKhenThuong=? and BoPhan=?", this, item.BoPhan));
                        if (boPhan == null)
                        {
                            boPhan = new ChiTietKhenThuongBoPhan(Session);
                            boPhan.BoPhan = item.BoPhan;
                            ListChiTietKhenThuongBoPhan.Add(boPhan);
                        }
                    }
                }
            }
        }

        [Size(500)]
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

        [ModelDefault("Caption", "Số tiền khen thưởng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [Appearance("SoTienKhenThuong_UTE", TargetItems = "SoTienKhenThuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'IUH'")]
        public decimal SoTienKhenThuong
        {
            get
            {
                return _SoTienKhenThuong;
            }
            set
            {
                SetPropertyValue("SoTienKhenThuong", ref _SoTienKhenThuong, value);
            }
        }

        [ModelDefault("Caption", "Số tiền bằng chữ")]
        [Appearance("SoTienBangChu_UTE", TargetItems = "SoTienBangChu", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'IUH'")]
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cá nhân")]
        [Association("QuyetDinhKhenThuong-ListChiTietKhenThuongNhanVien")]
        public XPCollection<ChiTietKhenThuongNhanVien> ListChiTietKhenThuongNhanVien
        {
            get
            {
                return GetCollection<ChiTietKhenThuongNhanVien>("ListChiTietKhenThuongNhanVien");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách tập thể")]
        [Association("QuyetDinhKhenThuong-ListChiTietKhenThuongBoPhan")]
        public XPCollection<ChiTietKhenThuongBoPhan> ListChiTietKhenThuongBoPhan
        {
            get
            {
                return GetCollection<ChiTietKhenThuongBoPhan>("ListChiTietKhenThuongBoPhan");
            }
        }

        public QuyetDinhKhenThuong(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ChiTietDeNghiKhenThuong> DeNghiList { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhKhenThuong;
            SoTienKhenThuong = 500000;
        }

        private void UpdateDeNghiKhenThuong()
        {
            if (DeNghiList == null)
                DeNghiList = new XPCollection<ChiTietDeNghiKhenThuong>(Session);

            if (NamHoc != null
                && DanhHieuKhenThuong != null)
                DeNghiList.Criteria = CriteriaOperator.Parse("QuanLyKhenThuong.NamHoc=? and DanhHieuKhenThuong=?", NamHoc, DanhHieuKhenThuong);
            else if (NamHoc != null)
                DeNghiList.Criteria = CriteriaOperator.Parse("QuanLyKhenThuong.NamHoc=?", NamHoc);
            else if (DanhHieuKhenThuong != null)
                DeNghiList.Criteria = CriteriaOperator.Parse("DanhHieuKhenThuong=?", DanhHieuKhenThuong);
        }

        protected override void OnSaving()
        {
            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietKhenThuongNhanVien item in ListChiTietKhenThuongNhanVien)
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
            }

            base.OnSaving();
        }
    }

}
