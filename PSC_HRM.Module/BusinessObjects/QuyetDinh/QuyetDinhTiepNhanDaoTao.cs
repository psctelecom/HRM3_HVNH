using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định tiếp nhận đào tạo")]
    [Appearance("Hide_QuyetDinhDaoTao", TargetItems = "QuyetDinhDaoTao;", Enabled = false, Criteria = "QuyetDinhDaoTao is not null")]
    public class QuyetDinhTiepNhanDaoTao : QuyetDinh
    {
        private string _SoCongVan;
        private DateTime _TuNgay;
        //private DateTime _NgayPhatSinhBienDong;
        private QuyetDinhDaoTao _QuyetDinhDaoTao;
        private QuyetDinhGiaHanDaoTao _QuyetDinhGiaHanDaoTao;
        //private string _LuuTru;
        private bool _QuyetDinhMoi;

        [ModelDefault("Caption", "Số công văn")]
        public string SoCongVan
        {
            get
            {
                return _SoCongVan;
            }
            set
            {
                SetPropertyValue("SoCongVan", ref _SoCongVan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định đào tạo")]
        public QuyetDinhDaoTao QuyetDinhDaoTao
        {
            get
            {
                return _QuyetDinhDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhDaoTao", ref _QuyetDinhDaoTao, value);
                if (!IsLoading && value != null)
                {
                    ChiTietTiepNhanDaoTao chiTiet;
                    foreach (ChiTietDaoTao item in value.ListChiTietDaoTao)
                    {
                        chiTiet = Session.FindObject<ChiTietTiepNhanDaoTao>(CriteriaOperator.Parse("QuyetDinhTiepNhanDaoTao=? and ThongTinNhanVien=?", Oid, item.ThongTinNhanVien));
                        if (chiTiet == null)
                        {
                            chiTiet = new ChiTietTiepNhanDaoTao(Session);
                            chiTiet.QuyetDinhTiepNhanDaoTao = this;
                            chiTiet.BoPhan = item.BoPhan;
                            chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                            chiTiet.TinhTrangMoi = item.TinhTrang;
                            ListChiTietTiepNhanDaoTao.Add(chiTiet);
                        }
                    }
                    QuyetDinhGiaHanDaoTao GH;
                    GH = Session.FindObject<QuyetDinhGiaHanDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao=?", value.Oid));
                    if(GH!=null)
                    {
                        QuyetDinhGiaHanDaoTao = GH;
                    }
                }
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định gia hạn đào tạo")]
        public QuyetDinhGiaHanDaoTao QuyetDinhGiaHanDaoTao
        {
            get
            {
                return _QuyetDinhGiaHanDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhGiaHanDaoTao", ref _QuyetDinhGiaHanDaoTao, value);
                
            }
        }
        [ModelDefault("Caption", "Ngày trở lại trường")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
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
        [Association("QuyetDinhTiepNhanDaoTao-ListChiTietTiepNhanDaoTao")]
        public XPCollection<ChiTietTiepNhanDaoTao> ListChiTietTiepNhanDaoTao
        {
            get
            {
                return GetCollection<ChiTietTiepNhanDaoTao>("ListChiTietTiepNhanDaoTao");
            }
        }

        public QuyetDinhTiepNhanDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhCongNhanDaoTao;

            //
            QuyetDinhMoi = true;
        }

        protected override void QuyetDinhChanged()
        {
            
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietTiepNhanDaoTao item in ListChiTietTiepNhanDaoTao)
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
                if (ListChiTietTiepNhanDaoTao.Count == 1)
                {
                    BoPhanText = ListChiTietTiepNhanDaoTao[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietTiepNhanDaoTao[0].ThongTinNhanVien.HoTen;
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
