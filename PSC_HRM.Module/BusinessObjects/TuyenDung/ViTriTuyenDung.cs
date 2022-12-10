using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.TuyenDung
{
    [DefaultProperty("Caption")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Vị trí tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique("ViTriTuyenDung.Unique", DefaultContexts.Save, "QuanLyTuyenDung;MaQuanLy")]
    public class ViTriTuyenDung : BaseObject
    {
        // Fields...
        //private LoaiNhanVienEnum _PhanLoai;
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private string _MaQuanLy;
        private string _TenViTriTuyenDung;
        private LoaiTuyenDung _LoaiTuyenDung;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [Association("QuanLyTuyenDung-ListViTriTuyenDung")]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }
        
        [ModelDefault("Caption", "Tên vị trí tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenViTriTuyenDung
        {
            get
            {
                return _TenViTriTuyenDung;
            }
            set
            {
                SetPropertyValue("TenViTriTuyenDung", ref _TenViTriTuyenDung, value);
            }
        }

        //[ModelDefault("Caption", "Phân loại")]
        //public LoaiNhanVienEnum PhanLoai
        //{
        //    get
        //    {
        //        return _PhanLoai;
        //    }
        //    set
        //    {
        //        SetPropertyValue("PhanLoai", ref _PhanLoai, value);
        //    }
        //}

        [ModelDefault("Caption", "Loại tuyển dụng")]
        public LoaiTuyenDung LoaiTuyenDung
        {
            get
            {
                return _LoaiTuyenDung;
            }
            set
            {
                SetPropertyValue("LoaiTuyenDung", ref _LoaiTuyenDung, value);
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public string Caption
        {
            get
            {
                string tenLoaiTuyenDung = string.Empty;
                if (LoaiTuyenDung != null)
                {
                    tenLoaiTuyenDung = TenViTriTuyenDung + " " + LoaiTuyenDung.TenLoaiTuyenDung;
                }
                else
                {
                    tenLoaiTuyenDung = TenViTriTuyenDung;
                }
                //
                return tenLoaiTuyenDung;
            }
        }

        public ViTriTuyenDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            XPCollection<LoaiTuyenDung> listLoaiTuyenDung = new XPCollection<LoaiTuyenDung>(Session);

            LoaiTuyenDung loaiTuyenDung = listLoaiTuyenDung.Session.FindObject<LoaiTuyenDung>(CriteriaOperator.Parse("TenLoaiTuyenDung = ?", "Cơ hữu"));

            LoaiTuyenDung = loaiTuyenDung;
          
            //PhanLoai = LoaiNhanVienEnum.BienChe;
        }
    }

}
