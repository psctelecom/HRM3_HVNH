using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.GiayTo
{
    [ImageName("BO_GiayToHoSo")]
    [DefaultProperty("SoGiayTo")]
    [ModelDefault("Caption", "Giấy tờ hồ sơ")]
    public class GiayToHoSo : BaseObject
    {
        // Fields...
        private string _LuuTru;
        private DateTime _NgayBanHanh;
        private DateTime _NgayLap;
        private string _TrichYeu;
        private string _SoGiayTo;
        private int _SoBan = 1;
        private DangLuuTru _DangLuuTru;
        private DanhMuc.GiayTo _GiayTo;
        private HoSo.HoSo _HoSo;
        private string _DuongDanFile;
        private QuyetDinh.QuyetDinh _QuyetDinh;

        //[Browsable(false)]
        [Association("HoSo-ListGiayToHoSo")]
        public HoSo.HoSo HoSo
        {
            get
            {
                return _HoSo;
            }
            set
            {
                SetPropertyValue("HoSo", ref _HoSo, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinh.QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Giấy tờ")]
        public DanhMuc.GiayTo GiayTo
        {
            get
            {
                return _GiayTo;
            }
            set
            {
                SetPropertyValue("GiayTo", ref _GiayTo, value);
            }
        }

        [ModelDefault("Caption", "Số giấy tờ")]
        public string SoGiayTo
        {
            get
            {
                return _SoGiayTo;
            }
            set
            {
                SetPropertyValue("SoGiayTo", ref _SoGiayTo, value);
            }
        }

        [ModelDefault("Caption", "Ngày ban hành")]
        public DateTime NgayBanHanh
        {
            get
            {
                return _NgayBanHanh;
            }
            set
            {
                SetPropertyValue("NgayBanHanh", ref _NgayBanHanh, value);
            }
        }

        [Browsable(false)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }
        [Browsable(false)]
        public string DuongDanFile
        {
            get
            {
                return _DuongDanFile;
            }
            set
            {
                SetPropertyValue("DuongDanFile", ref _DuongDanFile, value);
            }
        }

        [ModelDefault("Caption", "Dạng lưu trữ")]
        public DangLuuTru DangLuuTru
        {
            get
            {
                return _DangLuuTru;
            }
            set
            {
                SetPropertyValue("DangLuuTru", ref _DangLuuTru, value);
            }
        }

        [ModelDefault("Caption", "Số bản")]
        public int SoBan
        {
            get
            {
                return _SoBan;
            }
            set
            {
                SetPropertyValue("SoBan", ref _SoBan, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Trích yếu")]
        public string TrichYeu
        {
            get
            {
                return _TrichYeu;
            }
            set
            {
                SetPropertyValue("TrichYeu", ref _TrichYeu, value);
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
      
        public GiayToHoSo(Session session) : base(session) { }

        protected override void OnDeleting()
        {
            ////Tiến hành xóa file trên máy chủ
            //if (!IsSaving && !string.IsNullOrEmpty(this.DuongDanFile) && !string.IsNullOrEmpty(this.LuuTru))
            //{
            //    using (DialogUtil.AutoWait("Đang xóa file trên máy chủ..."))
            //    {
            //        FptProvider.DeleteFileOnServer(this.DuongDanFile, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password);
            //    }
            //}

            base.OnDeleting();
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Sao y có công chứng%"));

        }
    }

}
