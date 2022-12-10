using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.ChuyenNgach;
using PSC_HRM.Module;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định chuyển ngạch")]  
    public class QuyetDinhChuyenNgach : QuyetDinh
    {
        private DeNghiChuyenNgach _DeNghiChuyenNgach;
        private bool _QuyetDinhMoi;
        //private string _LuuTru;

        [ImmediatePostData]
        [ModelDefault("Caption", "Đề nghị chuyển ngạch")]
        public DeNghiChuyenNgach DeNghiChuyenNgach
        {
            get
            {
                return _DeNghiChuyenNgach;
            }
            set
            {
                SetPropertyValue("DeNghiChuyenNgach", ref _DeNghiChuyenNgach, value);
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
        [Association("QuyetDinhChuyenNgach-ListChiTietQuyetDinhChuyenNgach")]
        public XPCollection<ChiTietQuyetDinhChuyenNgach> ListChiTietQuyetDinhChuyenNgach
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhChuyenNgach>("ListChiTietQuyetDinhChuyenNgach");
            }
        }

        public QuyetDinhChuyenNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhChuyenNgach;

            QuyetDinhMoi = true;
        }
        public void CreateListChiTietQuyetDinhChuyenNgach(HoSo_NhanVienItem item)
        {
            ChiTietQuyetDinhChuyenNgach chiTiet = new ChiTietQuyetDinhChuyenNgach(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietQuyetDinhChuyenNgach.Add(chiTiet);
        }

        private void UpdateGiayToList()
        {
            if (ListChiTietQuyetDinhChuyenNgach.Count == 1)
                GiayToList = ListChiTietQuyetDinhChuyenNgach[0].ThongTinNhanVien.ListGiayToHoSo;
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
                    foreach (ChiTietQuyetDinhChuyenNgach item in ListChiTietQuyetDinhChuyenNgach)
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
                if (ListChiTietQuyetDinhChuyenNgach.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhChuyenNgach[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhChuyenNgach[0].ThongTinNhanVien.HoTen;
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
