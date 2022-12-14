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
using PSC_HRM.Module.GiayTo;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thành lập khác")]
    public class QuyetDinhThanhLapKhac : QuyetDinh
    {
        //private string _LuuTru;
        private string _GhiChu;
        private bool _DinhKem;
        
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

        [Size(-1)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [Persistent]
        [ModelDefault("Caption", "Đính kèm")]
        public bool DinhKem
        {
            get
            {
                return _DinhKem;
            }
            set
            {
                SetPropertyValue("DinhKem", ref _DinhKem, value);
            }
        }
        
        [Aggregated]
        [ModelDefault("Caption", "Danh sách tổ chức")]
        [Association("QuyetDinhThanhLapKhac-ListChiTietThanhLapKhac_ToChuc")]
        public XPCollection<ChiTietThanhLapKhac_ToChuc> ListChiTietThanhLapKhac_ToChuc
        {
            get
            {
                return GetCollection<ChiTietThanhLapKhac_ToChuc>("ListChiTietThanhLapKhac_ToChuc");
            }
        }

        public QuyetDinhThanhLapKhac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietThanhLapKhac_ToChuc item_tochuc in ListChiTietThanhLapKhac_ToChuc)
                    {
                        foreach (ChiTietThanhLapKhac_ThanhVien item in item_tochuc.ListChiTietThanhLapKhac_ThanhVien)
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

}
