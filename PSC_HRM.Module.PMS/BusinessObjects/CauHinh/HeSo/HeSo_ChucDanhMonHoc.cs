using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{
    [ModelDefault("Caption", "Hệ số chức danh (Đặc biệt)")]
    [DefaultProperty("Caption")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "QuanLyHeSo;NhanVien", "Hệ số cho nhân viên đã tồn tại.")]
    public class HeSo_ChucDanhMonHoc : BaseObject
    {
        #region  key
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSo_ChucDanhMonHoc")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }
        #endregion

        private NhanVien _NhanVien;
        private decimal _HeSoChucDanhMonHoc;

        [ModelDefault("Caption", "Nhân viên")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSo_ChucDanhMonHoc", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSoChucDanhMonHoc
        {
            get { return _HeSoChucDanhMonHoc; }
            set { SetPropertyValue("HeSoChucDanhMonHoc", ref _HeSoChucDanhMonHoc, value); }
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format("{0} - Hệ số : {1}", NhanVien != null ? NhanVien.HoTen : "", HeSoChucDanhMonHoc > 0 ? Math.Round(HeSoChucDanhMonHoc,2).ToString() : "");
            }
        }
        
        public HeSo_ChucDanhMonHoc(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
