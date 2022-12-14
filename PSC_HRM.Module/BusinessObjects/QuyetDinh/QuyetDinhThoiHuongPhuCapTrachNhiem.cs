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
    [Appearance("QD_Huong", TargetItems = "QuyetDinhHuongPhuCapTrachNhiem", Enabled = false, Criteria = "QuyetDinhHuongPhuCapTrachNhiem is not null")]
    [ModelDefault("Caption", "Quyết định thôi hưởng phụ cấp trách nhiệm")]
    public class QuyetDinhThoiHuongPhuCapTrachNhiem : QuyetDinh
    {
        //private string _LuuTru;
        private bool _QuyetDinhMoi;
        private QuyetDinhHuongPhuCapTrachNhiem _QuyetDinhHuongPhuCapTrachNhiem;

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
        [Association("QuyetDinhThoiHuongPhuCapTrachNhiem-ListChiTietQuyetDinhThoiHuongPhuCapTrachNhiem")]
        public XPCollection<ChiTietQuyetDinhThoiHuongPhuCapTrachNhiem> ListChiTietQuyetDinhThoiHuongPhuCapTrachNhiem
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhThoiHuongPhuCapTrachNhiem>("ListChiTietQuyetDinhThoiHuongPhuCapTrachNhiem");
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định hưởng phụ cấp trách nhiệm")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public QuyetDinhHuongPhuCapTrachNhiem QuyetDinhHuongPhuCapTrachNhiem
        {
            get
            {
                return _QuyetDinhHuongPhuCapTrachNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuongPhuCapTrachNhiem", ref _QuyetDinhHuongPhuCapTrachNhiem, value);
                if (!IsLoading && value != null)
                {
                    foreach (ChiTietQuyetDinhHuongPhuCapTrachNhiem ctHuong in value.ListChiTietQuyetDinhHuongPhuCapTrachNhiem)
                    {
                        ChiTietQuyetDinhThoiHuongPhuCapTrachNhiem ctThoiHuong = new ChiTietQuyetDinhThoiHuongPhuCapTrachNhiem(Session);
                        ctThoiHuong.QuyetDinhThoiHuongPhuCapTrachNhiem = this;
                        ctThoiHuong.ThongTinNhanVien = ctHuong.ThongTinNhanVien;
                        ctThoiHuong.BoPhan = ctHuong.BoPhan;
                        ctThoiHuong.HSPCTrachNhiemCu = ctHuong.HSPCTrachNhiemMoi;
                        ctThoiHuong.NgayThoiHuongHSPCTrachNhiemCu = NgayHieuLuc;
                    }
                }
            }
        }


        public QuyetDinhThoiHuongPhuCapTrachNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThoiHuongPhuCapTrachNhiem;
        }
        
        protected override void OnSaving()
        {
            base.OnSaving();
            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietQuyetDinhThoiHuongPhuCapTrachNhiem item in ListChiTietQuyetDinhThoiHuongPhuCapTrachNhiem)
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
