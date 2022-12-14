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
    [ModelDefault("Caption", "Quyết định tiếp nhận bồi dưỡng")]
    [Appearance("Hide_QuyetDinhBoiDuong", TargetItems = "QuyetDinhBoiDuong;", Enabled = false, Criteria = "QuyetDinhBoiDuong is not null")]
    public class QuyetDinhTiepNhanBoiDuong : QuyetDinh
    {
        private DateTime _TuNgay;
        //private DateTime _NgayPhatSinhBienDong;
        private QuyetDinhBoiDuong _QuyetDinhBoiDuong;
        private string _LuuTru;
        private bool _QuyetDinhMoi;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định bồi dưỡng")]
        public QuyetDinhBoiDuong QuyetDinhBoiDuong
        {
            get
            {
                return _QuyetDinhBoiDuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoiDuong", ref _QuyetDinhBoiDuong, value);
                if (!IsLoading && value != null)
                {
                    ChiTietTiepNhanBoiDuong chiTiet;
                    foreach (ChiTietBoiDuong item in value.ListChiTietBoiDuong)
                    {
                        chiTiet = Session.FindObject<ChiTietTiepNhanBoiDuong>(CriteriaOperator.Parse("QuyetDinhTiepNhanBoiDuong=? and ThongTinNhanVien=?", Oid, item.ThongTinNhanVien));
                        if (chiTiet == null)
                        {
                            chiTiet = new ChiTietTiepNhanBoiDuong(Session);
                            chiTiet.QuyetDinhTiepNhanBoiDuong = this;
                            chiTiet.BoPhan = item.BoPhan;
                            chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                            chiTiet.TinhTrangMoi = item.TinhTrang;
                            ListChiTietTiepNhanBoiDuong.Add(chiTiet);
                        }
                    }
                }
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

        [ModelDefault("Caption", "Lưu trữ")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        public string LuuTru
        {
            get
            {
                return _LuuTru;
            }
            set
            {
                SetPropertyValue("LuuTru", ref _LuuTru, value);
            }
        }

       

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhTiepNhanBoiDuong-ListChiTietTiepNhanBoiDuong")]
        public XPCollection<ChiTietTiepNhanBoiDuong> ListChiTietTiepNhanBoiDuong
        {
            get
            {
                return GetCollection<ChiTietTiepNhanBoiDuong>("ListChiTietTiepNhanBoiDuong");
            }
        }

        public QuyetDinhTiepNhanBoiDuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhCongNhanBoiDuong;

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
                if (!String.IsNullOrEmpty(LuuTru) ||
                    !String.IsNullOrWhiteSpace(SoQuyetDinh))
                {
                    foreach (ChiTietTiepNhanBoiDuong item in ListChiTietTiepNhanBoiDuong)
                    {
                        item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
                        item.GiayToHoSo.LuuTru = LuuTru;
                    }
                }

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietTiepNhanBoiDuong.Count == 1)
                {
                    BoPhanText = ListChiTietTiepNhanBoiDuong[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietTiepNhanBoiDuong[0].ThongTinNhanVien.HoTen;
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
