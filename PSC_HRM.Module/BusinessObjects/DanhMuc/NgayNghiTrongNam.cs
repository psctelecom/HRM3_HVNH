using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [ImageName("BO_List")]
    [DefaultProperty("TenNgayNghi")]
    [ModelDefault("Caption", "Ngày nghỉ trong năm")]    
    public class NgayNghiTrongNam : TruongBaseObject
    {
        private QuanLyNgayNghiTrongNam _QuanLyNgayNghiTrongNam;
        private string _TenNgayNghi;
        private DateTime _NgayNghi;//TuNgay
        private DateTime _DenNgay;

        [Browsable(false)]
        [Association("QuanLyNgayNghiTrongNam-ListNgayNghiTrongNam")]
        public QuanLyNgayNghiTrongNam QuanLyNgayNghiTrongNam
        {
            get
            {
                return _QuanLyNgayNghiTrongNam;
            }
            set
            {
                SetPropertyValue("QuanLyNgayNghiTrongNam", ref _QuanLyNgayNghiTrongNam, value);
            }
        }

        [ModelDefault("Caption", "Tên ngày nghỉ")]
        public string TenNgayNghi
        {
            get
            {
                return _TenNgayNghi;
            }
            set
            {
                SetPropertyValue("TenNgayNghi", ref _TenNgayNghi, value);
            }
        }

        //[ModelDefault("Caption", "Ngày nghỉ")]
        [ModelDefault("Caption", "Nghỉ từ ngày")]
        public DateTime NgayNghi
        {
            get
            {
                return _NgayNghi;
            }
            set
            {
                SetPropertyValue("NgayNghi", ref _NgayNghi, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ đến ngày")]
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

        public NgayNghiTrongNam(Session session) : base(session) { }
    }

}
