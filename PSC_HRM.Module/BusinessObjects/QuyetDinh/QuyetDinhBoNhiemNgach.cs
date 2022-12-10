using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.TapSu;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định bổ nhiệm ngạch")]
    [Appearance("Hide_NEU", TargetItems = "NgayXacNhan", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]

    public class QuyetDinhBoNhiemNgach : QuyetDinh
    {
        private DeNghiBoNhiemNgach _DeNghiBoNhiemNgach;
        private DateTime _NgayPhatSinhBienDong;
        private bool _QuyetDinhMoi;
        //private string _LuuTru;
        private DateTime _NgayXacNhan;

        [Browsable(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Ngày phát sinh biến động")]
        public DateTime NgayPhatSinhBienDong
        {
            get
            {
                return _NgayPhatSinhBienDong;
            }
            set
            {
                SetPropertyValue("NgayPhatSinhBienDong", ref _NgayPhatSinhBienDong, value);
            }
        }

        [ModelDefault("Caption", "Ngày xác nhận")]
        public DateTime NgayXacNhan
        {
            get
            {
                return _NgayXacNhan;
            }
            set
            {
                SetPropertyValue("NgayXacNhan", ref _NgayXacNhan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đề nghị bổ nhiệm ngạch")]
        public DeNghiBoNhiemNgach DeNghiBoNhiemNgach
        {
            get
            {
                return _DeNghiBoNhiemNgach;
            }
            set
            {
                SetPropertyValue("DeNghiBoNhiemNgach", ref _DeNghiBoNhiemNgach, value);
                if (!IsLoading && value != null)
                {
                    //ChiTietQuyetDinhBoNhiemNgach chiTiet;
                    //foreach (ChiTietDeNghiBoNhiemNgach item in value.ListChiTietDeNghiBoNhiemNgach)
                    //{
                    //    chiTiet = Session.FindObject<ChiTietQuyetDinhBoNhiemNgach>(CriteriaOperator.Parse("QuyetDinhBoNhiemNgach=? and ThongTinNhanVien=?", Oid, item.ThongTinNhanVien));
                    //    if (chiTiet == null)
                    //    {
                    //        chiTiet = new ChiTietQuyetDinhBoNhiemNgach(Session);
                    //        chiTiet.QuyetDinhBoNhiemNgach = this;
                    //        chiTiet.BoPhan = item.BoPhan;
                    //        chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                    //    }
                    //    chiTiet.NgachLuong = item.NgachLuong;
                    //    chiTiet.BacLuong = item.BacLuong;
                    //    chiTiet.HeSoLuong = item.HeSoLuong;
                    //    chiTiet.NgayBoNhiemNgach = item.NgayBoNhiemNgach;
                    //    chiTiet.MocNangLuong = item.NgayBoNhiemNgach;
                    //    chiTiet.NgayHuongLuong = item.NgayHuongLuong;
                    //}
                }
            }
        }

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
        [Association("QuyetDinhBoNhiemNgach-ListChiTietQuyetDinhBoNhiemNgach")]
        public XPCollection<ChiTietQuyetDinhBoNhiemNgach> ListChiTietQuyetDinhBoNhiemNgach
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhBoNhiemNgach>("ListChiTietQuyetDinhBoNhiemNgach");
            }
        }

        public QuyetDinhBoNhiemNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhBoNhiemNgach;           
           
        }
        public void CreateListChiTietQuyetDinhBoNhiemNgach(HoSo_NhanVienItem item)
        {
            ChiTietQuyetDinhBoNhiemNgach chiTiet = new ChiTietQuyetDinhBoNhiemNgach(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietQuyetDinhBoNhiemNgach.Add(chiTiet);
        }

        private void UpdateGiayToList()
        {
            if (ListChiTietQuyetDinhBoNhiemNgach.Count == 1)
                GiayToList = ListChiTietQuyetDinhBoNhiemNgach[0].ThongTinNhanVien.ListGiayToHoSo;
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
                    foreach (ChiTietQuyetDinhBoNhiemNgach item in ListChiTietQuyetDinhBoNhiemNgach)
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
                if (ListChiTietQuyetDinhBoNhiemNgach.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhBoNhiemNgach[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhBoNhiemNgach[0].ThongTinNhanVien.HoTen;
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
