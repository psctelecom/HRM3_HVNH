using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.ThuNhap.LuongWeb;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.ThuNhap.LuongWeb
{
    [DefaultProperty("DienGiai")]
    [ImageName("BO_ChiTietLuong")]
    [ModelDefault("Caption", "Chi tiết lương cán bộ")]
    //[RuleCombinationOfPropertiesIsUnique("ChiTietLuongNhanVienWeb.Unique", DefaultContexts.Save, "BangLuongNhanVienWeb;ThongTinNhanVien")]
    public class ChiTietLuongNhanVienWeb : BaseObject
    {
        private BangLuongNhanVienWeb _BangLuongNhanVienWeb;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DateTime _NgayChi;
        private string _NoiDung;
        private decimal _SoTien;
        private string _GhiChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng lương nhân viên")]
        [Association("BangLuongNhanVienWeb-ListChiTietLuongNhanVienWeb")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BangLuongNhanVienWeb BangLuongNhanVienWeb
        {
            get
            {
                return _BangLuongNhanVienWeb;
            }
            set
            {
                SetPropertyValue("BangLuongNhanVienWeb", ref _BangLuongNhanVienWeb, value);
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
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Nội dung")]
        public string NoiDung
        {
            get
            {
                return _NoiDung;
            }
            set
            {
                SetPropertyValue("NoiDung", ref _NoiDung, value);
            }
        }

        [ModelDefault("Caption", "Ngày chi")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NgayChi
        {
            get
            {
                return _NgayChi;
            }
            set
            {
                SetPropertyValue("NgaChi", ref _NgayChi, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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

        public ChiTietLuongNhanVienWeb(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.KhongConCongTacTaiTruong = false"));

            NVList.Criteria = go;
        }

    }

}
