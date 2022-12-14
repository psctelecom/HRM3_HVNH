using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.ThuNhap.Thue;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;

namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    [DefaultClassOptions]
    [ImageName("BO_TienMat")]
    [DefaultProperty("SoChungTu")]
    [ModelDefault("Caption", "Chi tiết bảng chốt tổng thu nhập của nhân viên")]
    public class ChiTietBangChotTongThuNhapNhanVien: BaseObject
    {
        private BangChotTongThuNhapNhanVien _BangChotTongThuNhapNhanVien;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private decimal _TongThuNhap;
        private decimal _TongBaoHiem;
        private decimal _ThuNhapChiuThue;
        private decimal _GiamTruBanThan;
        private decimal _GiamTruGiaCanh;
        private decimal _ThuNhapTinhThue;
        private decimal _ThueTNCN;
        private decimal _ThueTNCNTamTru;
        private decimal _ThueTNCNConLai;
        private decimal _ThucNhan;

        [Browsable(false)]
        [Association("BangChotTongThuNhapNhanVien-ListChiTietBangChotTongThuNhapNhanVien")]
        public BangChotTongThuNhapNhanVien BangChotTongThuNhapNhanVien
        {
            get
            {
                return _BangChotTongThuNhapNhanVien;
            }
            set
            {
                SetPropertyValue("BangChotTongThuNhapNhanVien", ref _BangChotTongThuNhapNhanVien, value);
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

        [ModelDefault("Caption", "Tổng thu nhập")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongThuNhap
        {
            get
            {
                return _TongThuNhap;
            }
            set
            {
                SetPropertyValue("TongThuNhap", ref _TongThuNhap, value);
            }
        }

        [ModelDefault("Caption", "Tổng bảo hiểm")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongBaoHiem
        {
            get
            {
                return _TongBaoHiem;
            }
            set
            {
                SetPropertyValue("TongBaoHiem", ref _TongBaoHiem, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập chịu thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
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

        [ModelDefault("Caption", "Giảm trừ bản thân")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal GiamTruBanThan
        {
            get
            {
                return _GiamTruBanThan;
            }
            set
            {
                SetPropertyValue("GiamTruBanThan", ref _GiamTruBanThan, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ gia cảnh")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal GiamTruGiaCanh
        {
            get
            {
                return _GiamTruGiaCanh;
            }
            set
            {
                SetPropertyValue("GiamTruGiaCanh", ref _GiamTruGiaCanh, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập tính thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThuNhapTinhThue
        {
            get
            {
                return _ThuNhapTinhThue;
            }
            set
            {
                SetPropertyValue("ThuNhapTinhThue", ref _ThuNhapTinhThue, value);
            }
        }

        [ModelDefault("Caption", "Thuế TNCN")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThueTNCN
        {
            get
            {
                return _ThueTNCN;
            }
            set
            {
                SetPropertyValue("ThueTNCN", ref _ThueTNCN, value);
            }
        }

        [ModelDefault("Caption", "Thuế TNCN tạm trừ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
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

        [ModelDefault("Caption", "Thuế TNCN còn lại")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThueTNCNConLai
        {
            get
            {
                return _ThueTNCNConLai;
            }
            set
            {
                SetPropertyValue("ThueTNCNConLai", ref _ThueTNCNConLai, value);
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

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        public ChiTietBangChotTongThuNhapNhanVien(Session session) : base(session) { }

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
