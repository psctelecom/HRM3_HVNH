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
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [ImageName("BO_HoaDon")]
    [DefaultProperty("NhanVien")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowEdit", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "Thuế TNCN tạm trừ cán bộ không HĐLĐ")]
    [RuleCombinationOfPropertiesIsUnique("ThueTNCNTamTruNgoai.Unique", DefaultContexts.Save, "BangThueTNCNTamTru;NhanVien")]
    [Appearance("ThueTNCNTamTruNgoai.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "BangThueTNCNTamTru is not null and ((BangThueTNCNTamTru.KyTinhLuong is not null and BangThueTNCNTamTru.KyTinhLuong.KhoaSo) or BangThueTNCNTamTru.ChungTu is not null)")]
    public class ThueTNCNTamTruNgoai : BaseObject, IBoPhan
    {
        private BangThueTNCNTamTru _BangThueTNCNTamTru;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private decimal _TongThuNhapChiuThue;
        private decimal _TongTNCTLamCanCuGiamTru;
        private decimal _TongThueTNCNTamTru;
        private decimal _ThuNhapChiuThueTrongThang;
        private decimal _ThueTNCNTamTruTrongThang;
        private decimal _ThuNhap;
        private decimal _ThueTNCNTamTru;

        [Browsable(false)]
        [Association("BangThueTNCNTamTru-DanhSachThueTNCNTamTruNgoai")]
        public BangThueTNCNTamTru BangThueTNCNTamTru
        {
            get
            {
                return _BangThueTNCNTamTru;
            }
            set
            {
                SetPropertyValue("BangThueTNCNTamTru", ref _BangThueTNCNTamTru, value);
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






        //********************************************************************
        //Tổng phát sinh từ đầu năm
        //********************************************************************
        //Tổng thu nhập chịu thuế từ đầu năm tới thời điểm hiện tại
        [ModelDefault("Caption", "Tổng thu nhập chịu thuế")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongThuNhapChiuThue
        {
            get
            {
                return _TongThuNhapChiuThue;
            }
            set
            {
                SetPropertyValue("TongThuNhapChiuThue", ref _TongThuNhapChiuThue, value);
            }
        }

        [ModelDefault("Caption", "Tổng TNCT làm căn cứ giảm trừ")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongTNCTLamCanCuGiamTru
        {
            get
            {
                return _TongTNCTLamCanCuGiamTru;
            }
            set
            {
                SetPropertyValue("TongTNCTLamCanCuGiamTru", ref _TongTNCTLamCanCuGiamTru, value);
            }
        }

        //Tổng thuế TNCN đã tạm thu từ đầu năm tới giờ
        [ModelDefault("Caption", "Tổng thuế TNCN đã tạm trừ")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongThueTNCNTamTru
        {
            get
            {
                return _TongThueTNCNTamTru;
            }
            set
            {
                SetPropertyValue("TongThueTNCNTamTru", ref _TongThueTNCNTamTru, value);
            }
        }






        //********************************************************************
        //Phát sinh trong tháng
        //********************************************************************
        [ModelDefault("Caption", "Thu nhập chịu thuế trong tháng")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal ThuNhapChiuThueTrongThang
        {
            get
            {
                return _ThuNhapChiuThueTrongThang;
            }
            set
            {
                SetPropertyValue("ThuNhapChiuThueTrongThang", ref _ThuNhapChiuThueTrongThang, value);
            }
        }

        [ModelDefault("Caption", "Thuế TNCN tạm trừ trong tháng")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal ThueTNCNTamTruTrongThang
        {
            get
            {
                return _ThueTNCNTamTruTrongThang;
            }
            set
            {
                SetPropertyValue("ThueTNCNTamTruTrongThang", ref _ThueTNCNTamTruTrongThang, value);
            }
        }







        //********************************************************************
        //Phát sinh trong tháng
        //********************************************************************
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

        //Lưu vết Thuế TNCN tạm thu
        [ModelDefault("Caption", "Thuế TNCN tạm trừ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal ThueTNCNTamTru
        {
            get
            {
                return _ThueTNCNTamTru;
            }
            set
            {
                SetPropertyValue("ThueTNCNTamTru", ref _ThueTNCNTamTru, value);
            }
        }

        public ThueTNCNTamTruNgoai(Session session) : base(session) { }

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
