using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.KhauTru
{
    [DefaultClassOptions]
    [DefaultProperty("TenLoai")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Loại khấu trừ lương")]
    public class LoaiKhauTruLuong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoai;
        private bool _DuocGiamTruKhiTinhThueTNCN;
        private CongThucTinhKhauTru _CongThucTinhKhauTru;

        [ModelDefault("Caption", "Mã quản lý")]
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

        [ModelDefault("Caption", "Tên loại")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TenLoai
        {
            get
            {
                return _TenLoai;
            }
            set
            {
                SetPropertyValue("TenLoai", ref _TenLoai, value);
            }
        }

        #region 05/11/2016 - Không tính khấu trừ lương từ công thức nữa (chỉ dùng import)
        [Browsable(false)]
        [ModelDefault("Caption", "Được giảm trừ khi tính thuế TNCN")]
        public bool DuocGiamTruKhiTinhThueTNCN
        {
            get
            {
                return _DuocGiamTruKhiTinhThueTNCN;
            }
            set
            {
                SetPropertyValue("DuocGiamTruKhiTinhThueTNCN", ref _DuocGiamTruKhiTinhThueTNCN, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính khấu trừ")]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public CongThucTinhKhauTru CongThucTinhKhauTru
        {
            get
            {
                return _CongThucTinhKhauTru;
            }
            set
            {
                SetPropertyValue("CongThucTinhKhauTru", ref _CongThucTinhKhauTru, value);
            }
        }
        #endregion

        public LoaiKhauTruLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //CongThucTinhKhauTru = new CongThucTinhKhauTru(Session);
        }
    }

}
