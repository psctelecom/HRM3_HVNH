using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.QuaTrinh;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định tham dự hội thi tay nghề trẻ")]
    public class QuyetDinhThamDuHoiThiTayNgheTre : QuyetDinh
    {
        private string _SoCongVan;
        private string _DiaDiem;
        private string _DonViToChuc;
        private NguonKinhPhi _NguonKinhPhi;
        private string _TruongHoTro;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private string _LyDo;
        //private string _LuuTru;

        [ModelDefault("Caption", "Nguồn kinh phí")]
        public NguonKinhPhi NguonKinhPhi
        {
            get
            {
                return _NguonKinhPhi;
            }
            set
            {
                SetPropertyValue("NguonKinhPhi", ref _NguonKinhPhi, value);
            }
        }

        [ModelDefault("Caption", "Trường hỗ trợ")]
        public string TruongHoTro
        {
            get
            {
                return _TruongHoTro;
            }
            set
            {
                SetPropertyValue("TruongHoTro", ref _TruongHoTro, value);
            }
        }


        [Browsable(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Số công văn")]
        public string SoCongVan
        {
            get
            {
                return _SoCongVan;
            }
            set
            {
                SetPropertyValue("SoCongVan", ref _SoCongVan, value);
            }
        }

        [Browsable(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Đơn vị tổ chức")]
        public string DonViToChuc
        {
            get
            {
                return _DonViToChuc;
            }
            set
            {
                SetPropertyValue("DonViToChuc", ref _DonViToChuc, value);
            }
        }

        [Size(8000)]
        [ModelDefault("Caption", "Địa điểm")]
        public string DiaDiem
        {
            get
            {
                return _DiaDiem;
            }
            set
            {
                SetPropertyValue("DiaDiem", ref _DiaDiem, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }


        [Browsable(false)]
        [NonPersistent]
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
        [Association("QuyetDinhThamDuHoiThiTayNgheTre-ListChiTietQuyetDinhThamDuHoiThiTayNgheTre")]
        public XPCollection<ChiTietQuyetDinhThamDuHoiThiTayNgheTre> ListChiTietQuyetDinhThamDuHoiThiTayNgheTre
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhThamDuHoiThiTayNgheTre>("ListChiTietQuyetDinhThamDuHoiThiTayNgheTre");
            }
        }

        public QuyetDinhThamDuHoiThiTayNgheTre(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThamDuHoiThiTayNgheTre;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietQuyetDinhThamDuHoiThiTayNgheTre item in ListChiTietQuyetDinhThamDuHoiThiTayNgheTre)
                    {
                        item.GiayToHoSo.QuyetDinh = this;
                        item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
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
