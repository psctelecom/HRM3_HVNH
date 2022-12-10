using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenTinhThanh")]
    [ModelDefault("Caption", "Tỉnh thành")]
    public class TinhThanh : TruongBaseObject
    {
        public TinhThanh(Session session) : base(session) { }

        private string _MaQuanLy;
        private string _TenTinhThanh;
        private QuocGia _QuocGia;
        private decimal _SoNgayDiDuong;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên tỉnh thành")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTinhThanh
        {
            get
            {
                return _TenTinhThanh;
            }
            set
            {
                SetPropertyValue("TenTinhThanh", ref _TenTinhThanh, value);
            }
        }

        [ModelDefault("Caption", "Quốc gia")]
        [ModelDefault("Index", "-1")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("QuocGia-TinhThanhList")]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Số ngày đi đường")]
        public decimal SoNgayDiDuong
        {
            get
            {
                return _SoNgayDiDuong;
            }
            set
            {
                SetPropertyValue("SoNgayDiDuong", ref _SoNgayDiDuong, value);
            }
        }

        [ModelDefault("Caption", "Danh sách quận huyện")]
        [Association("TinhThanh-QuanHuyenList")]
        public XPCollection<QuanHuyen> QuanHuyenList
        {
            get
            {
                return GetCollection<QuanHuyen>("QuanHuyenList");
            }
        }
     
    }

}
