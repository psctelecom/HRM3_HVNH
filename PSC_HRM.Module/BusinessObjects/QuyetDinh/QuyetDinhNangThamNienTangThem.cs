using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.NangThamNienTangThem;
using PSC_HRM.Module;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nâng thâm niên tăng thêm")]
    public class QuyetDinhNangThamNienTangThem : QuyetDinh
    {
        // Fields...
        private DeNghiNangThamNienTangThem _DeNghiNangThamNienTangThem;
        private bool _QuyetDinhMoi;
        //private string _LuuTru;
        
        [ModelDefault("Caption", "Đề nghị nâng thâm niên tăng thêm")]
        public DeNghiNangThamNienTangThem DeNghiNangThamNienTangThem
        {
            get
            {
                return _DeNghiNangThamNienTangThem;
            }
            set
            {
                SetPropertyValue("DeNghiNangThamNienTangThem", ref _DeNghiNangThamNienTangThem, value);
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
        [Association("QuyetDinhNangThamNienTangThem-ListChiTietQuyetDinhNangThamNienTangThem")]
        public XPCollection<ChiTietQuyetDinhNangThamNienTangThem> ListChiTietQuyetDinhNangThamNienTangThem
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhNangThamNienTangThem>("ListChiTietQuyetDinhNangThamNienTangThem");
            }
        }

        public QuyetDinhNangThamNienTangThem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhNangPhuCapThamNienNhaGiao;

            //UTE không còn quyết định cũ
            if (TruongConfig.MaTruong == "UTE")
            {
                QuyetDinhMoi = true;
            }
        }
        public void CreateListChiTietQuyetDinhNangPhuCapThamNienNhaGiao(HoSo_NhanVienItem item)
        {
            ChiTietQuyetDinhNangThamNienTangThem chiTiet = new ChiTietQuyetDinhNangThamNienTangThem(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietQuyetDinhNangThamNienTangThem.Add(chiTiet);

        }
        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietQuyetDinhNangThamNienTangThem item in ListChiTietQuyetDinhNangThamNienTangThem)
                    {
                        //nguoi tap su
                        item.GiayToHoSo.QuyetDinh = this;
                        item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
                        item.GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                        item.GiayToHoSo.NgayLap = NgayQuyetDinh;
                        item.GiayToHoSo.LuuTru = GiayToHoSo.LuuTru;
                        item.GiayToHoSo.DuongDanFile = GiayToHoSo.DuongDanFile;
                    }
                }

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietQuyetDinhNangThamNienTangThem.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhNangThamNienTangThem[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhNangThamNienTangThem[0].ThongTinNhanVien.HoTen;
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
