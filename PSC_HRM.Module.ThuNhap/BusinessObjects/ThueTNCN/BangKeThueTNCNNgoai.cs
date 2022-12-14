using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [ImageName("BO_HoaDon")]
    [DefaultProperty("NhanVien")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowEdit", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "05B/BK-TNCN")]
    [RuleCombinationOfPropertiesIsUnique("BangKeThueTNCNNgoai.Unique", DefaultContexts.Save, "ToKhaiThueTNCN;NhanVien")]
    public class BangKeThueTNCNNgoai : BaseObject, IBoPhan
    {
        private ToKhaiQuyetToanThueTNCN _ToKhaiThueTNCN;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private decimal _ThuNhapChiuThue;
        private decimal _TNCTTinhGiamThue;
        private decimal _ThueTNCNDaKhauTru;

        [Browsable(false)]
        [ModelDefault("Caption", "Tờ khai thuế")]
        [Association("ToKhaiThueTNCN-BangKeThueTNCNNgoai")]
        public ToKhaiQuyetToanThueTNCN ToKhaiThueTNCN
        {
            get
            {
                return _ToKhaiThueTNCN;
            }
            set
            {
                SetPropertyValue("ToKhaiThueTNCN", ref _ToKhaiThueTNCN, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Thu nhập chịu thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThuNhapChiuThue
        {
            get
            {
                return _ThuNhapChiuThue;
            }
            set
            {
                SetPropertyValue("ThuNhapChiuThue", ref _ThuNhapChiuThue, value);
            }
        }

        //thu nhập là tiền lương, tiền công trong khu kinh tế
        [ModelDefault("Caption", "TNCT tính giảm thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TNCTTinhGiamThue
        {
            get
            {
                return _TNCTTinhGiamThue;
            }
            set
            {
                SetPropertyValue("TNCTTinhGiamThue", ref _TNCTTinhGiamThue, value);
            }
        }

        [ModelDefault("Caption", "Thuế TNCN đã khấu trừ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNDaKhauTru
        {
            get
            {
                return _ThueTNCNDaKhauTru;
            }
            set
            {
                SetPropertyValue("ThueTNCNDaKhauTru", ref _ThueTNCNDaKhauTru, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết 05B/BK-TNCN")]
        [Association("BangKeThueTNCNNgoai-ListChiTietBangKeThueTNCNNgoai")]
        public XPCollection<ChiTietBangKeThueTNCNNgoai> ListChiTietBangKeThueTNCNNgoai
        {
            get
            {
                return GetCollection<ChiTietBangKeThueTNCNNgoai>("ListChiTietBangKeThueTNCNNgoai");
            }
        }

        public BangKeThueTNCNNgoai(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? or TinhTrang.TenTinhTrang not like ?", "%nghỉ việc%", "%nghỉ hưu%"));

            NVList.Criteria = go;
        }
    }

}
