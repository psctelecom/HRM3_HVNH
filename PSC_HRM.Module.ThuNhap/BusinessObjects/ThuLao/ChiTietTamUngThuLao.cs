using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.BusinessObjects.ThuLao;

namespace PSC_HRM.Module.ThuNhap.ThuLao
{
    [ImageName("BO_ThuLao")]
    [DefaultProperty("LoaiTamUng")]
    [ModelDefault("Caption", "Chi tiết tạm ứng")]
    public class ChiTietTamUngThuLao : BaseObject
    {
        #region key
        private ChiTietThuLaoNhanVien _ChiTietThuLaoNhanVien;
        [Browsable(false)]
        [ModelDefault("Caption", "ChiTietThuLaoNhanVien")]
        [Association("ChiTietThuLaoNhanVien-ListChiTietTamUng")]
        public ChiTietThuLaoNhanVien ChiTietThuLaoNhanVien
        {
            get
            {
                return _ChiTietThuLaoNhanVien;
            }
            set
            {
                SetPropertyValue("ChiTietThuLaoNhanVien", ref _ChiTietThuLaoNhanVien, value);
            }
        }
        #endregion
        private string _DienGiai;
        private decimal _SoTienTamUng;
        private LoaiTamUng _LoaiTamUng;

        [ModelDefault("Caption", "Diễn giải")]
        [Size(-1)]
        public string DienGiai
        {
            get { return _DienGiai; }
            set { SetPropertyValue("DienGiai", ref _DienGiai, value); }
        }
        [ModelDefault("Caption", "Số tiền ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTienTamUng
        {
            get
            {
                return _SoTienTamUng;
            }
            set
            {
                SetPropertyValue("SoTienTamUng", ref _SoTienTamUng, value);
            }
        }

        [ModelDefault("Caption", "Loại tạm ứng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LoaiTamUng LoaiTamUng
        {
            get { return _LoaiTamUng; }
            set { SetPropertyValue("LoaiTamUng", ref _LoaiTamUng, value); }
        }
        public ChiTietTamUngThuLao(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

    }
}