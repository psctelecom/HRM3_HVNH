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
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.ThuNhap.BoSungLuong
{
    [ImageName("BO_ChiTietLuong")]
    [ModelDefault("Caption", "Chi bổ sung lương phụ cấp tiến sĩ")]
    [Appearance("ChiBoSungPhuCapTienSi.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "BoSungLuongNhanVien is not null and BoSungLuongNhanVien.KyTinhLuong is not null and BoSungLuongNhanVien.KyTinhLuong.KhoaSo")]
    public class ChiBoSungPhuCapTienSi : ThuNhapBaseObject
    {
        private BoSungLuongNhanVien _BoSungLuongNhanVien;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private decimal _SoTien;
        private decimal _SoThangTruyLinh;
        private decimal _ThanhTien;

        public ChiBoSungPhuCapTienSi(Session session) : base(session) { }

        [Browsable(false)]
        [ModelDefault("Caption", "Chi bổ sung lương tiến sĩ")]
        [Association("BoSungLuongNhanVien-ListChiBoSungLuongPhuCapTienSi")]
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
                if (!IsLoading)
                {
                    ThanhTien = SoTien * SoThangTruyLinh;
                }
            }
        }

        [ModelDefault("Caption", "Số tháng truy lĩnh")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoThangTruyLinh
        {
            get
            {
                return _SoThangTruyLinh;
            }
            set
            {
                SetPropertyValue("SoThangTruyLinh", ref _SoThangTruyLinh, value);
                if (!IsLoading)
                {
                    ThanhTien = SoTien * SoThangTruyLinh;
                }
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
