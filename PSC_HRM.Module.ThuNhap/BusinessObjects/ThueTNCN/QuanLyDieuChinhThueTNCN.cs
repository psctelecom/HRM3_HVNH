using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [ImageName("BO_HoaDon")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Điều chỉnh 05A/BK-TNCN")]
    public class QuanLyDieuChinhThueTNCN : BaseObject
    {
        // Fields...
        private ToKhaiQuyetToanThueTNCN _ToKhaiQuyetToanThueTNCN;
        private KyTinhLuong _KyTinhLuong;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Tờ khai quyết toán thuế TNCN")]
        [Association("ToKhaiQuyetToanThueTNCN-ListQuanLyDieuChinhThueTNCN")]
        public ToKhaiQuyetToanThueTNCN ToKhaiQuyetToanThueTNCN
        {
            get
            {
                return _ToKhaiQuyetToanThueTNCN;
            }
            set
            {
                SetPropertyValue("ToKhaiQuyetToanThueTNCN", ref _ToKhaiQuyetToanThueTNCN, value);
                if (!IsLoading && value != null)
                {
                    KyTinhLuong = null;
                    UpdateKyTinhLuong();
                }
            }
        }

        [ModelDefault("Caption", "Kỳ tính lương")]
        [DataSourceProperty("KyTinhLuongList")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách điều chỉnh")]
        [Association("QuanLyDieuChinhThueTNCN-ListDieuChinhThueTNCNNhanVien")]
        public XPCollection<DieuChinhThueTNCNNhanVien> ListDieuChinhThueTNCNNhanVien
        {
            get
            {
                return GetCollection<DieuChinhThueTNCNNhanVien>("ListDieuChinhThueTNCNNhanVien");
            }
        }

        public QuanLyDieuChinhThueTNCN(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        private void UpdateKyTinhLuong()
        {
            if (KyTinhLuongList == null)
                KyTinhLuongList = new XPCollection<KyTinhLuong>(Session);

            KyTinhLuongList.Criteria = CriteriaOperator.Parse("Nam=?", 
                ToKhaiQuyetToanThueTNCN.KyTinhThue);
        }
    }

}
