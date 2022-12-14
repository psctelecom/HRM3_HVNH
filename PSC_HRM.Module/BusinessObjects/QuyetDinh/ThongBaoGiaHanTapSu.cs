using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Thông báo gia hạn hết hạn tập sự")]
    public class ThongBaoGiaHanTapSu : QuyetDinh
    {
        private QuyetDinhHuongDanTapSu _QuyetDinhHuongDanTapSu;
        //private string _LuuTru;

        [DataSourceProperty("QuyetDinhList")]
        [ModelDefault("Caption", "Quyết định hướng dẫn tập sự")]
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
        [Association("ThongBaoGiaHanTapSu-ListChiTietThongBaoGiaHanTapSu")]
        public XPCollection<ChiTietThongBaoGiaHanTapSu> ListChiTietThongBaoGiaHanTapSu
        {
            get
            {
                return GetCollection<ChiTietThongBaoGiaHanTapSu>("ListChiTietThongBaoGiaHanTapSu");
            }
        }

        [Browsable(false)]
        public XPCollection<QuyetDinhHuongDanTapSu> QuyetDinhList { get; set; }

        public ThongBaoGiaHanTapSu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhGiaHanTapSu;
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhHuongDanTapSu>(Session);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietThongBaoGiaHanTapSu item in ListChiTietThongBaoGiaHanTapSu)
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
                if (ListChiTietThongBaoGiaHanTapSu.Count == 1)
                {
                    BoPhanText = ListChiTietThongBaoGiaHanTapSu[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietThongBaoGiaHanTapSu[0].ThongTinNhanVien.HoTen;
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
