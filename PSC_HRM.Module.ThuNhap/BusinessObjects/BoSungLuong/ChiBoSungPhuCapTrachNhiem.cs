using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.ThuNhap.BoSungLuong
{
    [ImageName("BO_ChiTietLuong")]
    [ModelDefault("Caption", "Chi bổ sung phụ cấp trách nhiệm")]
    [Appearance("ChiBoSungPhuCapTrachNhiem.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "BoSungLuongNhanVien is not null and BoSungLuongNhanVien.KyTinhLuong is not null and BoSungLuongNhanVien.KyTinhLuong.KhoaSo")]
    public class ChiBoSungPhuCapTrachNhiem : ThuNhapBaseObject
    {
        private BoSungLuongNhanVien _BoSungLuongNhanVien;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private decimal _HSPCTrachNhiem;
        private decimal _SoThangTruyLinh;
        private decimal _ThanhTien;
        private decimal _SoNgayCong;
        private decimal _SoTienChiuThue;
        private decimal _ThucNhan;
        private string _GhiChu;

        public ChiBoSungPhuCapTrachNhiem(Session session) : base(session) { }

        [Browsable(false)]
        [ModelDefault("Caption", "Chi bổ sung phụ cấp trách nhiệm")]
        [Association("BoSungLuongNhanVien-ListChiBoSungPhuCapTrachNhiem")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoSungLuongNhanVien BoSungLuongNhanVien
        {
            get
            {
                return _BoSungLuongNhanVien;
            }
            set
            {
                SetPropertyValue("BoSungLuongNhanVien", ref _BoSungLuongNhanVien, value);
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
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                  }
            }
        }

        [ModelDefault("Caption", "Hệ số trách nhiệm")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCTrachNhiem
        {
            get
            {
                return _HSPCTrachNhiem;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiem", ref _HSPCTrachNhiem, value);
            }
        }

        [ModelDefault("Caption", "Số ngày công")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
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

        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThanhTien
        {
            get
            {
                return _ThanhTien;
            }
            set
            {
                SetPropertyValue("ThanhTien", ref _ThanhTien, value);
            }
        }
  
        [ModelDefault("Caption", "Số tháng truy lĩnh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoThangTruyLinh
        {
            get
            {
                return _SoThangTruyLinh;
            }
            set
            {
                SetPropertyValue("SoThangTruyLinh", ref _SoThangTruyLinh, value);
            }
        }

        [ModelDefault("Caption", "Thực nhận")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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

        [ModelDefault("Caption", "Số tiền chịu thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? and TinhTrang.TenTinhTrang not like ? and TinhTrang.TenTinhTrang not like ?", "%nghỉ việc%", "%nghỉ hưu%","%chuyển công tác%"));

            NVList.Criteria = go;
        }
    }

}
