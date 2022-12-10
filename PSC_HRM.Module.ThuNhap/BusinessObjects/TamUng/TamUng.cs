using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.TamUng
{
    [ImageName("BO_BangLuong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Tạm ứng")]
    [RuleCombinationOfPropertiesIsUnique("TamUng", DefaultContexts.Save, "BangTamUng;ThongTinNhanVien")]
    public class TamUng : BaseObject, IBoPhan
    {
        private decimal _KetChuyenTuNamTruoc;
        private decimal _SoTienDaKhauTru;
        private decimal _TongTamUng;
        private decimal _SoTienConLai;
        private decimal _MucKhauTruHangThang;
        private BangTamUng _BangTamUng;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng tạm ứng")]
        [Association("BangTamUng-ListTamUng")]
        public BangTamUng BangTamUng
        {
            get
            {
                return _BangTamUng;
            }
            set
            {
                SetPropertyValue("BangTamUng", ref _BangTamUng, value);
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
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Kết chuyển từ năm trước")]
        public decimal KetChuyenTuNamTruoc
        {
            get
            {
                return _KetChuyenTuNamTruoc;
            }
            set
            {
                SetPropertyValue("KetChuyenTuNamTruoc", ref _KetChuyenTuNamTruoc, value);
                if (!IsLoading)
                    XuLy();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng tạm ứng")]
        public decimal TongTamUng
        {
            get
            {
                return _TongTamUng;
            }
            set
            {
                SetPropertyValue("TongTamUng", ref _TongTamUng, value);
                if (!IsLoading)
                    SoTienConLai = TongTamUng - SoTienDaKhauTru;
            }
        }        

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Mức khấu trừ hàng tháng")]
        public decimal MucKhauTruHangThang
        {
            get
            {
                return _MucKhauTruHangThang;
            }
            set
            {
                SetPropertyValue("MucKhauTruHangThang", ref _MucKhauTruHangThang, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Số tiền đã khấu trừ")]
        public decimal SoTienDaKhauTru
        {
            get
            {
                return _SoTienDaKhauTru;
            }
            set
            {
                SetPropertyValue("SoTienDaKhauTru", ref _SoTienDaKhauTru, value);
                if (!IsLoading)
                    SoTienConLai = TongTamUng - SoTienDaKhauTru;
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Số tiền còn lại")]
        public decimal SoTienConLai
        {
            get
            {
                return _SoTienConLai;
            }
            set
            {
                value = TongTamUng - SoTienDaKhauTru;
                SetPropertyValue("SoTienConLai", ref _SoTienConLai, value);
            }
        }

        [Aggregated]
        [Association("TamUng-ListChiTietTamUng")]
        [ModelDefault("Caption", "Danh sách tạm ứng")]
        public XPCollection<ChiTietTamUng> ListChiTietTamUng
        {
            get
            {
                return GetCollection<ChiTietTamUng>("ListChiTietTamUng");
            }
        }

        public TamUng(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        public void XuLy()
        {
            if (!IsDeleted)
            {
                decimal temp = 0;
                foreach (ChiTietTamUng item in ListChiTietTamUng)
                {
                    temp += item.SoTien;
                }
                TongTamUng = temp + KetChuyenTuNamTruoc;
            }
        }
    }

}
