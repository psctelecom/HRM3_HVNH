using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.ThuNhapKhac
{
    [DefaultProperty("NhanVien")]
    [ModelDefault("Caption", "Chi tiết thu nhập khác")]
    [Appearance("ChiTietThuNhapKhac.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "BangThuNhapKhac is not null and ((BangThuNhapKhac.KyTinhLuong is not null and BangThuNhapKhac.KyTinhLuong.KhoaSo) or BangThuNhapKhac.ChungTu is not null)")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "BangThuNhapKhac;ThongTinNhanVien")]
    public class ChiTietThuNhapKhac : ThuNhapBaseObject, IBoPhan
    {
        private BangThuNhapKhac _BangThuNhapKhac;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _NgayLap;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private string _GhiChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng thu nhập khác")]
        [Association("BangThuNhapKhac-ListChiTietThuNhapKhac")]
        public BangThuNhapKhac BangThuNhapKhac
        {
            get
            {
                return _BangThuNhapKhac;
            }
            set
            {
                SetPropertyValue("BangThuNhapKhac", ref _BangThuNhapKhac, value);
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
                if (!IsLoading && value != null)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        //[RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        [ModelDefault("Caption", "Số tiền chịu thuế")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal SoTienChiuThue
        {
            get
            {
                return _SoTienChiuThue;
            }
            set
            {
                SetPropertyValue("SoTienChiuThue", ref _SoTienChiuThue, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public ChiTietThuNhapKhac(Session session) : base(session) { }

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
