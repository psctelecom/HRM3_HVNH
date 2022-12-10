using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.ChamCong
{
    [ImageName("BO_QuanLyChamCong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết bảng chấm công")]
    [Appearance("ChiTietChamCongNhanVien", TargetItems = "*", Enabled = false,
        Criteria = "QuanLyChamCongNhanVien is not null and QuanLyChamCongNhanVien.KyTinhLuong is not null and QuanLyChamCongNhanVien.KyTinhLuong.KhoaSo")]
    public class ChiTietChamCongNhanVien : TruongBaseObject
    {
        // Fields...
        private decimal _SoNgayCong;
        private decimal _NghiNuaNgay;
        private decimal _NghiCoPhep;
        private decimal _NghiRo;
        private decimal _NghiThaiSan;
        private decimal _NghiHe;
        private string _DanhGia;
        private string _DanhGiaTruocDieuChinh;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuanLyChamCongNhanVien _QuanLyChamCongNhanVien;
        private string _DienGiai;
        
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý chấm công")]
        [Association("QuanLyChamCongNhanVien-ChiTietChamCongNhanVienList")]
        public QuanLyChamCongNhanVien QuanLyChamCongNhanVien
        {
            get
            {
                return _QuanLyChamCongNhanVien;
            }
            set
            {
                SetPropertyValue("QuanLyChamCongNhanVien", ref _QuanLyChamCongNhanVien, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Số ngày công")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayCong
        {
            get
            {
                return _SoNgayCong;
            }
            set
            {
                SetPropertyValue("SoNgayCong", ref _SoNgayCong, value);
            }
        }
        [ModelDefault("Caption", "Nghỉ nửa ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NghiNuaNgay
        {
            get
            {
                return _NghiNuaNgay;
            }
            set
            {
                SetPropertyValue("NghiNuaNgay", ref _NghiNuaNgay, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ có phép")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NghiCoPhep
        {
            get
            {
                return _NghiCoPhep;
            }
            set
            {
                SetPropertyValue("NghiCoPhep", ref _NghiCoPhep, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ trừ lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NghiRo
        {
            get
            {
                return _NghiRo;
            }
            set
            {
                SetPropertyValue("NghiRo", ref _NghiRo, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ thai sản")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NghiThaiSan
        {
            get
            {
                return _NghiThaiSan;
            }
            set
            {
                SetPropertyValue("NghiThaiSan", ref _NghiThaiSan, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ hè")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NghiHe
        {
            get
            {
                return _NghiHe;
            }
            set
            {
                SetPropertyValue("NghiHe", ref _NghiHe, value);
            }
        }

        [ModelDefault("Caption", "Đánh giá")]
        public string DanhGia
        {
            get
            {
                return _DanhGia;
            }
            set
            {
                SetPropertyValue("DanhGia", ref _DanhGia, value);
            }
        }

        [ModelDefault("Caption", "Đánh giá trước điều chỉnh")]
        public string DanhGiaTruocDieuChinh
        {
            get
            {
                return _DanhGiaTruocDieuChinh;
            }
            set
            {
                SetPropertyValue("DanhGiaTruocDieuChinh", ref _DanhGiaTruocDieuChinh, value);
            }
        }

        [Size(8000)]
        [ModelDefault("Caption", "Diễn giải")]
        public string DienGiai
        {
            get
            {
                return _DienGiai;
            }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }

        public ChiTietChamCongNhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateNVList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateNVList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
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
