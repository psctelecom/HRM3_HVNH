using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.NangNgach;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nâng ngạch lương")]
    //[Appearance("Hide_IUH", TargetItems = "TrinhDoChuyenMonCaoNhat;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='IUH'")]
    public class QuyetDinhNangNgach : QuyetDinh
    {
        private DeNghiNangNgach _DeNghiNangNgach;
        private DateTime _NgayPhatSinhBienDong;
        private bool _QuyetDinhMoi;
        //private string _LuuTru;

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

        [ModelDefault("Caption", "Đề nghị nâng ngạch")]
        public DeNghiNangNgach DeNghiNangNgach
        {
            get
            {
                return _DeNghiNangNgach;
            }
            set
            {
                SetPropertyValue("DeNghiNangNgach", ref _DeNghiNangNgach, value);
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
        [Association("QuyetDinhNangNgach-ListChiTietQuyetDinhNangNgach")]
        public XPCollection<ChiTietQuyetDinhNangNgach> ListChiTietQuyetDinhNangNgach
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhNangNgach>("ListChiTietQuyetDinhNangNgach");
            }
        }

        public QuyetDinhNangNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhNangNgach;           
        }
        public void CreateListChiTietQuyetDinhNangNgach(HoSo_NhanVienItem item)
        {
            ChiTietQuyetDinhNangNgach chiTiet = new ChiTietQuyetDinhNangNgach(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietQuyetDinhNangNgach.Add(chiTiet);
        }

        private void UpdateGiayToList()
        {
            if (ListChiTietQuyetDinhNangNgach.Count == 1)
                GiayToList = ListChiTietQuyetDinhNangNgach[0].ThongTinNhanVien.ListGiayToHoSo;
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
                    foreach (ChiTietQuyetDinhNangNgach item in ListChiTietQuyetDinhNangNgach)
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
                if (ListChiTietQuyetDinhNangNgach.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhNangNgach[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhNangNgach[0].ThongTinNhanVien.HoTen;
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
