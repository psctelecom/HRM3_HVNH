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
    [ModelDefault("Caption", "Quyết định cử cố vấn học tập")]
    public class QuyetDinhKhac : QuyetDinh
    {
        //private string _LuuTru;
        private string _GhiChu;
        private NamHoc _NamHoc;


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
       
        [ModelDefault("Caption", "Năm Học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }


        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhKhac-ListChiTietQuyetDinhKhac")]
        public XPCollection<ChiTietQuyetDinhKhac> ListChiTietQuyetDinhKhac
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhKhac>("ListChiTietQuyetDinhKhac");
            }
        }

        public QuyetDinhKhac(Session session) : base(session) { }

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
                    foreach (ChiTietQuyetDinhKhac item in ListChiTietQuyetDinhKhac)
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
                if (ListChiTietQuyetDinhKhac.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhKhac[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhKhac[0].ThongTinNhanVien.HoTen;
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
