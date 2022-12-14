using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BoiDuong;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định hưởng phụ cấp trách nhiệm")]
    public class QuyetDinhHuongPhuCapTrachNhiem : QuyetDinh
    {
        //private string _LuuTru;
        private bool _QuyetDinhMoi;
        
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
        
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhHuongPhuCapTrachNhiem-ListChiTietQuyetDinhHuongPhuCapTrachNhiem")]
        public XPCollection<ChiTietQuyetDinhHuongPhuCapTrachNhiem> ListChiTietQuyetDinhHuongPhuCapTrachNhiem
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhHuongPhuCapTrachNhiem>("ListChiTietQuyetDinhHuongPhuCapTrachNhiem");
            }
        }

        public QuyetDinhHuongPhuCapTrachNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhHuongPhuCapTrachNhiem;
        }
        
        protected override void OnSaving()
        {
            base.OnSaving();
            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietQuyetDinhHuongPhuCapTrachNhiem item in ListChiTietQuyetDinhHuongPhuCapTrachNhiem)
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
        }
    }

}
