using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.KhenThuong;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.KyLuat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định trực lễ")]
    public class QuyetDinhTrucLe : QuyetDinh
    {
        // Fields...
        //private string _LuuTru;
        private string _NoiDungTrucLe;
        private DateTime _NgayDeNghi;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                
            }
        }
        [ModelDefault("Caption", "Ngày đề nghị")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        public DateTime NgayDeNghi
        {
            get
            {
                return _NgayDeNghi;
            }
            set
            {
                SetPropertyValue("NgayDeNghi", ref _NgayDeNghi, value);
            }
        }
        [ModelDefault("Caption", "Nội dung trực lễ")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        public string NoiDungTrucLe
        {
            get
            {
                return _NoiDungTrucLe;
            }
            set
            {
                SetPropertyValue("NoiDungTrucLe", ref _NoiDungTrucLe, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ trực lễ")]
        [Association("QuyetDinhTrucLe-ListCanBoTrucLe")]
        public XPCollection<CanBoTrucLe> ListCanBoTrucLe
        {
            get
            {
                return GetCollection<CanBoTrucLe>("ListCanBoTrucLe");
            }
        }

        [Browsable(false)]
        [NonPersistent]
        public bool IsSave { get; set; }

        public QuyetDinhTrucLe(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhTrucLe;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                IsSave = true;
                
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (CanBoTrucLe item in ListCanBoTrucLe)
                    {
                        item.GiayToHoSo.QuyetDinh = Session.GetObjectByKey<QuyetDinh>(this.Oid);                      
                        item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
                        item.GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                        item.GiayToHoSo.LuuTru = GiayToHoSo.LuuTru;
                        item.GiayToHoSo.DuongDanFile = GiayToHoSo.DuongDanFile;
                    }
                }

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListCanBoTrucLe.Count == 1)
                {
                    BoPhanText = ListCanBoTrucLe[0].BoPhan.TenBoPhan;
                    NhanVienText = ListCanBoTrucLe[0].ThongTinNhanVien.HoTen;
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
