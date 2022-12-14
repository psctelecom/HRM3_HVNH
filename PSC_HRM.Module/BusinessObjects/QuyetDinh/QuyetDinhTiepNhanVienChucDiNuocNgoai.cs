using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định tiếp nhận viên chức đi nước ngoài")]
    public class QuyetDinhTiepNhanVienChucDiNuocNgoai : QuyetDinh
    {
        // Fields...
        //private string _LuuTru;
        private DateTime _TuNgay;
        private QuyetDinhDiNuocNgoai _QuyetDinhDiNuocNgoai;
        private bool _QuyetDinhMoi;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định đi nước ngoài")]
        public QuyetDinhDiNuocNgoai QuyetDinhDiNuocNgoai
        {
            get
            {
                return _QuyetDinhDiNuocNgoai;
            }
            set
            {
                SetPropertyValue("QuyetDinhDiNuocNgoai", ref _QuyetDinhDiNuocNgoai, value);
                if(!IsLoading && value != null)
                {
                    ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai chiTiet;
                    foreach (ChiTietQuyetDinhDiNuocNgoai item in value.ListChiTietQuyetDinhDiNuocNgoai)
                    {
                        chiTiet = Session.FindObject<ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai>(CriteriaOperator.Parse("QuyetDinhTiepNhanVienChucDiNuocNgoai=? and ThongTinNhanVien=?", Oid, item.ThongTinNhanVien));
                        if (chiTiet == null)
                        {
                            chiTiet = new ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai(Session);
                            chiTiet.QuyetDinhTiepNhanVienChucDiNuocNgoai = this;
                            chiTiet.BoPhan = item.BoPhan;
                            chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                            //chiTiet.TinhTrangMoi = item.TinhTrang;
                            ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai.Add(chiTiet);
                        }
                    }
                }
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Quyết định mới")]
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
        [Association("QuyetDinhTiepNhanVienChucDiNuocNgoai-ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai")]
        public XPCollection<ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai> ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai>("ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai");
            }
        }

        public QuyetDinhTiepNhanVienChucDiNuocNgoai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TuNgay = HamDungChung.GetServerTime();
            if (string.IsNullOrEmpty(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhTiepNhanVienChucDiNuocNgoai;
            //
            QuyetDinhMoi = true;
        }

        protected override void OnSaving()
        {
            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai item in ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai)
                    {
                        if (item.GiayToHoSo != null)
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

                IsDirty = false;

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
            base.OnSaving();
        }

        public bool IsExists(ThongTinNhanVien nhanVien)
        {
            foreach (ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai item in ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai)
            {
                if (item.ThongTinNhanVien.Oid == nhanVien.Oid)
                    return true;
            }
            return false;
        }
    }

}
