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

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thành lập hội đồng đánh giá hết tập sự")]  
    public class QuyetDinhThanhLapHoiDongDanhGiaHetTapSu : QuyetDinh
    {
       // private QuanLyTuyenDung _QuanLyTuyenDung;
        //private string _LuuTru;
        private string _TenTapSu;
        private string _DonViTapSu;

        //[ModelDefault("Caption", "Quản lý tuyển dụng")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public QuanLyTuyenDung QuanLyTuyenDung
        //{
        //    get
        //    {
        //        return _QuanLyTuyenDung;
        //    }
        //    set
        //    {
        //        SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
        //    }
        //}

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

        [ModelDefault("Caption", "Tên tập sự")]
       // [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        public string TenTapSu
        {
            get
            {
                return _TenTapSu;
            }
            set
            {
                SetPropertyValue("TenTapSu", ref _TenTapSu, value);
            }
        }
        [ModelDefault("Caption", "Đơn vị của tập sự")]
        // [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        public string DonViTapSu
        {
            get
            {
                return _DonViTapSu;
            }
            set
            {
                SetPropertyValue("DonViTapSu", ref _DonViTapSu, value);
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Danh sách hội đồng")]
        [Association("QuyetDinhThanhLapHoiDongHetTapSu-ListHoiDongHetTapSu")]
        public XPCollection<HoiDongHetTapSu> ListHoiDongHetTapSu
        {
            get
            {
                return GetCollection<HoiDongHetTapSu>("ListHoiDongHetTapSu");
            }
        }

        [Browsable(false)]
        [NonPersistent]
        public bool IsSave { get; set; }

        public QuyetDinhThanhLapHoiDongDanhGiaHetTapSu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThanhLapHoiDongTuyenDung;
        }

        //protected override void OnSaving()
        //{
        //    base.OnSaving();

        //    if (!IsDeleted)
        //    {
        //        IsSave = true;

        //        foreach (HoiDongTuyenDung item in ListHoiDongTuyenDung)
        //        {
        //            item.QuanLyTuyenDung = QuanLyTuyenDung;
        //        }

        //        //luu giay to ho so
        //        if (!String.IsNullOrWhiteSpace(LuuTru) &&
        //            !String.IsNullOrWhiteSpace(SoQuyetDinh))
        //        {
        //            GiayToHoSo giayToHoSo;
        //            foreach (HoiDongTuyenDung item in ListHoiDongTuyenDung)
        //            {
        //                giayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("HoSo=? and SoGiayTo=?", item.ThongTinNhanVien.Oid, SoQuyetDinh));
        //                if (giayToHoSo != null)
        //                {
        //                    giayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định"));
        //                    giayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
        //                    giayToHoSo.SoGiayTo = SoQuyetDinh;
        //                    giayToHoSo.NgayBanHanh = NgayHieuLuc;
        //                    giayToHoSo.LuuTru = LuuTru;
        //                }
        //            }
        //        }
        //    }
        //}

        protected override void OnSaving()
        {
            base.OnSaving();

            if(!IsDeleted)
            {
                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListHoiDongHetTapSu.Count == 1)
                {
                    BoPhanText = ListHoiDongHetTapSu[0].BoPhan.TenBoPhan;
                    NhanVienText = ListHoiDongHetTapSu[0].ThongTinNhanVien.HoTen;
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
