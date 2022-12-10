using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuyTrinh
{
    [DefaultClassOptions]
    [DefaultProperty("QuyTrinh")]
    [ModelDefault("Caption", "Thực hiện quy trình")]
    [Appearance("ThuHienQuyTrinh", TargetItems = "*", Enabled = false, Criteria = "KetThuc")]
    public class ThucHienQuyTrinh : BaseObject
    {
        // Fields...
        private Guid _LuuTruDuLieu;
        private bool _KetThuc;
        private bool _BatDau;
        private ChiTietQuyTrinh _ChiTietQuyTrinh;
        private QuyTrinh _QuyTrinh;

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Quy trình")]
        public QuyTrinh QuyTrinh
        {
            get
            {
                return _QuyTrinh;
            }
            set
            {
                SetPropertyValue("QuyTrinh", ref _QuyTrinh, value);
            }
        }

        [DataSourceProperty("QuyTrinh.ListChiTietQuyTrinh")]
        [ModelDefault("Caption", "Chi tiết quy trình")]
        public ChiTietQuyTrinh ChiTietQuyTrinh
        {
            get
            {
                return _ChiTietQuyTrinh;
            }
            set
            {
                SetPropertyValue("ChiTietQuyTrinh", ref _ChiTietQuyTrinh, value);
            }
        }

        [ModelDefault("Caption", "Bắt đầu")]
        public bool BatDau
        {
            get
            {
                return _BatDau;
            }
            set
            {
                SetPropertyValue("BatDau", ref _BatDau, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Kết thúc")]
        public bool KetThuc
        {
            get
            {
                return _KetThuc;
            }
            set
            {
                SetPropertyValue("KetThuc", ref _KetThuc, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ dữ liệu")]
        public Guid LuuTruDuLieu
        {
            get
            {
                return _LuuTruDuLieu;
            }
            set
            {
                SetPropertyValue("LuuTruDuLieu", ref _LuuTruDuLieu, value);
            }
        }

        public ThucHienQuyTrinh(Session session) : base(session) { }
    }
}
