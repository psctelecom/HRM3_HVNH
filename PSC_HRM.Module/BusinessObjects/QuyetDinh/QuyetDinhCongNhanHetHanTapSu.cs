using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.QuaTrinh;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định công nhận hết hạn tập sự")]
    public class QuyetDinhCongNhanHetHanTapSu : QuyetDinh
    {
        // Fields...        
        private bool _QuyetDinhMoi;
        private QuyetDinhHuongDanTapSu _QuyetDinhHuongDanTapSu;
        //private string _LuuTru;

        [ImmediatePostData]
        [ModelDefault("Caption", "QĐ hướng dẫn tập sự")]       
        public QuyetDinhHuongDanTapSu QuyetDinhHuongDanTapSu
        {
            get
            {
                return _QuyetDinhHuongDanTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuongDanTapSu", ref _QuyetDinhHuongDanTapSu, value);
            }
        }

        [ModelDefault("Caption", "Quyết định mới")]
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
        [ModelDefault("Caption", "Danh sách cá nhân")]
        [Association("QuyetDinhCongNhanHetHanTapSu-ListChiTietCongNhanHetHanTapSu")]
        public XPCollection<ChiTietCongNhanHetHanTapSu> ListChiTietCongNhanHetHanTapSu
        {
            get
            {
                return GetCollection<ChiTietCongNhanHetHanTapSu>("ListChiTietCongNhanHetHanTapSu");
            }
        }

        public QuyetDinhCongNhanHetHanTapSu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (String.IsNullOrEmpty(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhCongNhanHetHanTapSu;
        }

        private void UpdateGiayToList()
        {
            if (ListChiTietCongNhanHetHanTapSu.Count == 1)
                GiayToList = ListChiTietCongNhanHetHanTapSu[0].ThongTinNhanVien.ListGiayToHoSo;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateGiayToList();
        }
        protected override void OnSaving()
        {
            base.OnSaving();
            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietCongNhanHetHanTapSu item in ListChiTietCongNhanHetHanTapSu)
                    {
                        if (item.GiayToHoSo != null)
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

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietCongNhanHetHanTapSu.Count == 1)
                {
                    BoPhanText = ListChiTietCongNhanHetHanTapSu[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietCongNhanHetHanTapSu[0].ThongTinNhanVien.HoTen;
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
