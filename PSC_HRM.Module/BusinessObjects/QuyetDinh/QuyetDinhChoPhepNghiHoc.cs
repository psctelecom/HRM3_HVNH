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
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định cho phép nghỉ học")]
    [Appearance("Hide_QuyetDinhDaoTao", TargetItems = "QuyetDinhDaoTao;", Enabled = false, Criteria = "QuyetDinhDaoTao is not null")]
    public class QuyetDinhChoPhepNghiHoc : QuyetDinh
    {
        private DateTime _TuNgay;
        private QuyetDinhDaoTao _QuyetDinhDaoTao;
        //private string _LuuTru;
        private bool _QuyetDinhMoi;
        private string _GhiChu;
        private string _LyDoNghiHoc;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    ChiTietChoPhepNghiHoc chiTiet;
                    foreach (ChiTietDaoTao item in QuyetDinhDaoTao.ListChiTietDaoTao)
                    {
                        chiTiet = Session.FindObject<ChiTietChoPhepNghiHoc>(CriteriaOperator.Parse("QuyetDinhChoPhepNghiHoc=? and ThongTinNhanVien=?", Oid, item.ThongTinNhanVien));
                        if (chiTiet == null)
                        {
                            chiTiet = new ChiTietChoPhepNghiHoc(Session);
                            chiTiet.BoPhan = item.BoPhan;
                            chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                            chiTiet.ChuyenMonDaoTao = item.ChuyenMonDaoTao;
                            ListChiTietChoPhepNghiHoc.Add(chiTiet);
                        }
                    }
                }
            }
        }

        [ModelDefault("Caption", "Ngày nghỉ học")]
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

        [ModelDefault("Caption", "Lý do nghỉ học")]
        public string LyDoNghiHoc
        {
            get
            {
                return _LyDoNghiHoc;
            }
            set
            {
                SetPropertyValue("LyDoNghiHoc", ref _LyDoNghiHoc, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhChoPhepNghiHoc-ListChiTietChoPhepNghiHoc")]
        public XPCollection<ChiTietChoPhepNghiHoc> ListChiTietChoPhepNghiHoc
        {
            get
            {
                return GetCollection<ChiTietChoPhepNghiHoc>("ListChiTietChoPhepNghiHoc");
            }
        }

        public QuyetDinhChoPhepNghiHoc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
            { NoiDung = "nghỉ học"; }
            TuNgay = HamDungChung.GetServerTime();
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietChoPhepNghiHoc item in ListChiTietChoPhepNghiHoc)
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
                if (ListChiTietChoPhepNghiHoc.Count == 1)
                {
                    BoPhanText = ListChiTietChoPhepNghiHoc[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietChoPhepNghiHoc[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
        }

        public void CreateListChiTietQuyetDinhChoPhepNghiHoc(HoSo_NhanVienItem item)
        {
            ChiTietChoPhepNghiHoc chiTiet = new ChiTietChoPhepNghiHoc(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietChoPhepNghiHoc.Add(chiTiet);
        }
    }

}
