using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định hướng dẫn tập sự")]
    public class QuyetDinhHuongDanTapSu : QuyetDinh
    {
        // Fields...
        private QuyetDinhTuyenDung _QuyetDinhTuyenDung;
        private bool _QuyetDinhMoi;
        //private string _LuuTru;
        private decimal _HSPCTrachNhiem;
        private DateTime _NgayXacNhan;

        [ImmediatePostData]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Quyết định tuyển dụng")]
        public QuyetDinhTuyenDung QuyetDinhTuyenDung
        {
            get
            {
                return _QuyetDinhTuyenDung;
            }
            set
            {
                SetPropertyValue("QuyetDinhTuyenDung", ref _QuyetDinhTuyenDung, value);
                if (!IsLoading && value != null)
                {
                    ChiTietQuyetDinhHuongDanTapSu chiTiet;
                    foreach (ChiTietQuyetDinhTuyenDung item in value.ListChiTietQuyetDinhTuyenDung)
                    {
                        chiTiet = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", Oid, item.ThongTinNhanVien));
                        if (chiTiet == null)
                        {
                            chiTiet = new ChiTietQuyetDinhHuongDanTapSu(Session);
                            chiTiet.QuyetDinhHuongDanTapSu = this;
                            chiTiet.BoPhan = item.BoPhan;
                            chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                        }
                        chiTiet.TuNgay = item.NgayHuongLuong;
                        chiTiet.DenNgay = item.NgayHuongLuong.AddMonths(item.ThoiGianTapSu);
                    }
                }
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC trách nhiệm")]
        [Appearance("HSPCTrachNhiem_QNU", TargetItems = "HSPCTrachNhiem", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]     
        public decimal HSPCTrachNhiem
        {
            get
            {
                return _HSPCTrachNhiem;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiem", ref _HSPCTrachNhiem, value);
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
        [ModelDefault("Caption", "Ngày xác nhận")]
        public DateTime  NgayXacNhan
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
        [Association("QuyetDinhHuongDanTapSu-ListChiTietQuyetDinhHuongDanTapSu")]
        public XPCollection<ChiTietQuyetDinhHuongDanTapSu> ListChiTietQuyetDinhHuongDanTapSu
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhHuongDanTapSu>("ListChiTietQuyetDinhHuongDanTapSu");
            }
        }

        public QuyetDinhHuongDanTapSu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhHuongDanTapSu;
            HSPCTrachNhiem = HamDungChung.CauHinhChung.CauHinhHoSo.HSPCTrachNhiem;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateGiayToList();
            //Lấy mã trường hiện tại dùng để phân quyền
            MaTruong = TruongConfig.MaTruong;
        }

        private void UpdateGiayToList()
        {
            if (ListChiTietQuyetDinhHuongDanTapSu.Count == 1)
                GiayToList = ListChiTietQuyetDinhHuongDanTapSu[0].ThongTinNhanVien.ListGiayToHoSo;
        }
        public void CreateListChiTietQuyetDinhHuongDanTapSu(HoSo_NhanVienItem item)
        {
            ChiTietQuyetDinhHuongDanTapSu chiTiet = new ChiTietQuyetDinhHuongDanTapSu(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietQuyetDinhHuongDanTapSu.Add(chiTiet);
        }
        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    GiayToHoSo giayTo;
                    foreach (ChiTietQuyetDinhHuongDanTapSu item in ListChiTietQuyetDinhHuongDanTapSu)
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
                        //nguoi huong dan
                        giayTo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("HoSo=? and QuyetDinh=?", item.CanBoHuongDan.Oid, this.Oid));
                        if (giayTo != null)
                        {
                            giayTo.QuyetDinh = this;
                            giayTo.SoGiayTo = SoQuyetDinh;
                            giayTo.TrichYeu = NoiDung;
                            giayTo.NgayBanHanh = NgayHieuLuc;
                            giayTo.NgayLap = NgayQuyetDinh;
                            giayTo.LuuTru = GiayToHoSo.LuuTru;
                            giayTo.DuongDanFile = GiayToHoSo.DuongDanFile;                            
                        }
                        else
                        {
                            giayTo = new GiayToHoSo(Session);
                            giayTo.QuyetDinh = this;
                            giayTo.SoGiayTo = SoQuyetDinh;
                            giayTo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định hướng dẫn tập sự"));
                            giayTo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
                            giayTo.TrichYeu = NoiDung;
                            giayTo.NgayBanHanh = NgayHieuLuc;
                            giayTo.NgayLap = NgayQuyetDinh;
                            giayTo.LuuTru = GiayToHoSo.LuuTru;
                            giayTo.DuongDanFile = GiayToHoSo.DuongDanFile;
                        }
                    }
                }

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietQuyetDinhHuongDanTapSu.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhHuongDanTapSu[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhHuongDanTapSu[0].ThongTinNhanVien.HoTen;
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
