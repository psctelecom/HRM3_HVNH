using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.TuyenDung;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "SoQuyetDinh;QuanLyTuyenDung")]

    //[Appearance("Hide_BUH", TargetItems = "NgayPhatSinhBienDong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]  

    public class QuyetDinhTuyenDung : QuyetDinh
    {
        // Fields...
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private DateTime _NgayPhatSinhBienDong = DateTime.Now;
        private bool _QuyetDinhMoi;
        //private string _LuuTru;

        [Browsable(false)]
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
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
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
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhTuyenDung-ListChiTietQuyetDinhTuyenDung")]
        public XPCollection<ChiTietQuyetDinhTuyenDung> ListChiTietQuyetDinhTuyenDung
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhTuyenDung>("ListChiTietQuyetDinhTuyenDung");
            }
        }

        public QuyetDinhTuyenDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhTuyenDung;
        }
        public void CreateListChiTietQuyetDinhTuyenDung(ThongTinNhanVien nhanVien)
        {
            ChiTietQuyetDinhTuyenDung chiTiet = new ChiTietQuyetDinhTuyenDung(Session);
            chiTiet.BoPhan = nhanVien.BoPhan;
            chiTiet.ThongTinNhanVien = nhanVien;
            this.ListChiTietQuyetDinhTuyenDung.Add(chiTiet);
        }
        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietQuyetDinhTuyenDung item in ListChiTietQuyetDinhTuyenDung)
                    {
                        item.GiayToHoSo.QuyetDinh = this;
                        item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
                        item.GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                        item.GiayToHoSo.NgayLap = NgayQuyetDinh;
                        item.GiayToHoSo.LuuTru = GiayToHoSo.LuuTru;
                        item.GiayToHoSo.DuongDanFile = GiayToHoSo.DuongDanFile;
                    }
                }

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietQuyetDinhTuyenDung.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhTuyenDung[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhTuyenDung[0].ThongTinNhanVien.HoTen;
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
