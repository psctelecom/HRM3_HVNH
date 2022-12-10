using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.KhenThuong;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thành lập hội đồng khen thưởng")]
    public class QuyetDinhThanhLapHoiDongKhenThuong : QuyetDinh
    {
        // Fields...
        private QuanLyKhenThuong _QuanLyKhenThuong;
        //private string _LuuTru;

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Quản lý khen thưởng")]
        public QuanLyKhenThuong QuanLyKhenThuong
        {
            get
            {
                return _QuanLyKhenThuong;
            }
            set
            {
                SetPropertyValue("QuanLyKhenThuong", ref _QuanLyKhenThuong, value);
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
        [ModelDefault("Caption", "Hội đồng khen thưởng")]
        [Association("QuyetDinhThanhLapHoiDongKhenThuong-ListHoiDongKhenThuong")]
        public XPCollection<HoiDongKhenThuong> ListHoiDongKhenThuong
        {
            get
            {
                return GetCollection<HoiDongKhenThuong>("ListHoiDongKhenThuong");
            }
        }

        [Browsable(false)]
        [NonPersistent]
        public bool IsSave { get; set; }

        public QuyetDinhThanhLapHoiDongKhenThuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThanhLapHoiDongKhenThuong;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                IsSave = true;

                //quan ly khen thuong
                foreach (HoiDongKhenThuong item in ListHoiDongKhenThuong)
                {
                    item.QuanLyKhenThuong = QuanLyKhenThuong;
                }

                //luu giay to ho so
                if (GiayToHoSo != null)
                {                  
                    foreach (HoiDongKhenThuong item in ListHoiDongKhenThuong)
                    {
                        item.GiayToHoSo.QuyetDinh = this;
                        item.GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định thành lập hội đồng khen thưởng"));
                        item.GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
                        item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
                        item.GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                        item.GiayToHoSo.NgayLap = NgayQuyetDinh;
                        item.GiayToHoSo.LuuTru = GiayToHoSo.LuuTru;
                        item.GiayToHoSo.DuongDanFile = GiayToHoSo.DuongDanFile;
                    }
                }

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListHoiDongKhenThuong.Count == 1)
                {
                    BoPhanText = ListHoiDongKhenThuong[0].BoPhan.TenBoPhan;
                    NhanVienText = ListHoiDongKhenThuong[0].ThongTinNhanVien.HoTen;
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
