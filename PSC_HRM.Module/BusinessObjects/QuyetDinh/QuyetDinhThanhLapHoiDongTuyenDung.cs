using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using PSC_HRM.Module.TuyenDung;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.GiayTo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thành lập hội đồng tuyển dụng")]
    [Appearance("Hide_QNU", TargetItems = "ThongTinNhanVien;BoPhan;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]
    [Appearance("Hide_UFM", TargetItems = "ThongTinNhanVien;BoPhan;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UFM'")]
    public class QuyetDinhThanhLapHoiDongTuyenDung : QuyetDinh,IBoPhan
    {
        private QuanLyTuyenDung _QuanLyTuyenDung;
        //private string _LuuTru;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;

        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
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

        [ModelDefault("Caption", "Cán bộ")]
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
                 BoPhan = value.BoPhan;
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Danh sách hội đồng")]
        [Association("QuyetDinhThanhLapHoiDongTuyenDung-ListHoiDongTuyenDung")]
        public XPCollection<HoiDongTuyenDung> ListHoiDongTuyenDung
        {
            get
            {
                return GetCollection<HoiDongTuyenDung>("ListHoiDongTuyenDung");
            }
        }

        [Browsable(false)]
        [NonPersistent]
        public bool IsSave { get; set; }

        public QuyetDinhThanhLapHoiDongTuyenDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThanhLapHoiDongTuyenDung;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                IsSave = true;

                foreach (HoiDongTuyenDung item in ListHoiDongTuyenDung)
                {
                    item.QuanLyTuyenDung = QuanLyTuyenDung;
                }

                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (HoiDongTuyenDung item in ListHoiDongTuyenDung)
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

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListHoiDongTuyenDung.Count == 1)
                {
                    BoPhanText = ListHoiDongTuyenDung[0].BoPhan.TenBoPhan;
                    NhanVienText = ListHoiDongTuyenDung[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
        }
    }
}
