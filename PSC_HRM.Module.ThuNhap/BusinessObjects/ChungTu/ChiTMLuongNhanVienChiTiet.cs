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

namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    [ModelDefault("Caption","Chi tiết chi lương cán bộ")]
    [ImageName("BO_ChuyenKhoan")]
    [RuleCombinationOfPropertiesIsUnique("ChiTMLuongNhanVienChiTiet.Unique", DefaultContexts.Save, "ChiTMLuongNhanVien;NhanVien")]
    public class ChiTMLuongNhanVienChiTiet : BaseObject, IBoPhan
    {
        private ChiTMLuongNhanVien _ChiTMLuongNhanVien;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private decimal _ThuNhap;
        private decimal _KhauTru;
        private decimal _ThucNhan;

        [Browsable(false)]
        [Association("ChiTMLuongNhanVien-ChiTietNhanVien")]
        public ChiTMLuongNhanVien ChiTMLuongNhanVien
        {
            get
            {
                return _ChiTMLuongNhanVien;
            }
            set
            {
                SetPropertyValue("ChiTMLuongNhanVien", ref _ChiTMLuongNhanVien, value);
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
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Thu nhập")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThuNhap
        {
            get
            {
                return _ThuNhap;
            }
            set
            {
                SetPropertyValue("ThuNhap", ref _ThuNhap, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Khấu trừ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal KhauTru
        {
            get
            {
                return _KhauTru;
            }
            set
            {
                SetPropertyValue("KhauTru", ref _KhauTru, value);
            }
        }

        [ModelDefault("Caption", "Thực nhận")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThucNhan
        {
            get
            {
                return _ThucNhan;
            }
            set
            {
                SetPropertyValue("ThucNhan", ref _ThucNhan, value);
            }
        }

        public ChiTMLuongNhanVienChiTiet(Session session) : base(session) { }

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
