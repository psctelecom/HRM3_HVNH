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

namespace PSC_HRM.Module.ChamCong
{
    [ImageName("BO_ChamCong")]
    [ModelDefault("Caption", "Chi tiết chấm công khoán")]
    [Appearance("ChiTietChamCongLuongKhoan", TargetItems = "*", Enabled = false, 
        Criteria = "BangChamCongLuongKhoan is not null and BangChamCongLuongKhoan.KyTinhLuong is not null and BangChamCongLuongKhoan.KyTinhLuong.KhoaSo")]
    public class ChiTietChamCongKhoan : BaseObject, IBoPhan
    {
        private BangChamCongKhoan _BangChamCongLuongKhoan;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private int _SoNgayCong;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng chẩm công lương khoán")]
        [Association("BangChamCongLuongKhoan-ListChiTietChamCongLuongKhoan")]
        public BangChamCongKhoan BangChamCongLuongKhoan
        {
            get
            {
                return _BangChamCongLuongKhoan;
            }
            set
            {
                SetPropertyValue("BangChamCongLuongKhoan", ref _BangChamCongLuongKhoan, value);
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
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                if (!IsLoading && value != null)
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Số ngày công")]
        [RuleValueComparison("", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public int SoNgayCong
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

        public ChiTietChamCongKhoan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateNhanVienList();
        }

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
