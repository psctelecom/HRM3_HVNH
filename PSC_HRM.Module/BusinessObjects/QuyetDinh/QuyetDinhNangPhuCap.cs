using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nâng phụ cấp")]
    public class QuyetDinhNangPhuCap : QuyetDinh
    {
        private string _GhiChu;
        private DateTime _NgayPhatSinhBienDong;
        private bool _QuyetDinhMoi;
        //private string _LuuTru;

        [RuleRequiredField(DefaultContexts.Save)]
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
                if (!IsLoading)
                {
                    foreach (ChiTietQuyetDinhNangPhuCap item in this.ListChiTietQuyetDinhNangPhuCap)
                    {
                        //Đối với quyết định đã lập rồi giờ chỉ cập nhật lại quyết định có hiệu lực thì set tất cả các con
                        item.QuyetDinhMoi = true;
                    }
                }
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

        [Size(200)]
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhNangPhuCap-ListChiTietQuyetDinhNangPhuCap")]
        public XPCollection<ChiTietQuyetDinhNangPhuCap> ListChiTietQuyetDinhNangPhuCap
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhNangPhuCap>("ListChiTietQuyetDinhNangPhuCap");
            }
        }

        public QuyetDinhNangPhuCap(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhNangLuong;
            QuyetDinhMoi = true;

        }
        public void CreateListChiTietQuyetDinhNangPhuCap(HoSo_NhanVienItem item)
        {
            ChiTietQuyetDinhNangPhuCap chiTiet = new ChiTietQuyetDinhNangPhuCap(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);

            this.ListChiTietQuyetDinhNangPhuCap.Add(chiTiet);
        }
        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietQuyetDinhNangPhuCap item in ListChiTietQuyetDinhNangPhuCap)
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
                if (ListChiTietQuyetDinhNangPhuCap.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhNangPhuCap[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhNangPhuCap[0].ThongTinNhanVien.HoTen;
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
