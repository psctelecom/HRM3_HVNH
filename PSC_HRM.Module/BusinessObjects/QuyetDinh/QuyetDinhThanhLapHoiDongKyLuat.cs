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
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.KyLuat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thành lập hội đồng kỷ luật")]
    public class QuyetDinhThanhLapHoiDongKyLuat : QuyetDinh
    {
        // Fields...
        //private string _LuuTru;
        
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
        [ModelDefault("Caption", "Hội đồng kỷ luật")]
        [Association("QuyetDinhThanhLapHoiDongKyLuat-ListHoiDongKyLuat")]
        public XPCollection<HoiDongKyLuat> ListHoiDongKyLuat
        {
            get
            {
                return GetCollection<HoiDongKyLuat>("ListHoiDongKyLuat");
            }
        }

        [Browsable(false)]
        [NonPersistent]
        public bool IsSave { get; set; }

        public QuyetDinhThanhLapHoiDongKyLuat(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThanhLapHoiDongKyLuat;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                IsSave = true;
                
                //luu giay to ho so
                if (GiayToHoSo != null)
                {                   
                    foreach (HoiDongKyLuat item in ListHoiDongKyLuat)
                    {
                        item.GiayToHoSo.QuyetDinh = this;
                        item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
                        item.GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                        item.GiayToHoSo.LuuTru = GiayToHoSo.LuuTru;
                        item.GiayToHoSo.DuongDanFile = GiayToHoSo.DuongDanFile;
                    }
                }
            }
        }
    }

}
