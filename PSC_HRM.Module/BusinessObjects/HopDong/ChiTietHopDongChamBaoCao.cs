using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.HopDong
{
    [DefaultProperty("Lop")]
    [ImageName("BO_Contract")]
    [ModelDefault("Caption", "Chi tiết hợp đồng chấm báo cáo")]
    public class ChiTietHopDongChamBaoCao : BaseObject
    {
        // Fields...
        private decimal _ThueTNCN;
        private decimal _ThanhTien;
        private decimal _SoTien;
        private decimal _BaoCao;
        private int _SiSo;
        private string _Lop;
        private HopDong_ChamBaoCao _HopDongChamBaoCao;

        [Browsable(false)]
        [ModelDefault("Caption", "Hợp đồng chấm báo cáo")]
        [Association("HopDongChamBaoCao-ListChiTietHopDongChamBaoCao")]
        public HopDong_ChamBaoCao HopDongChamBaoCao
        {
            get
            {
                return _HopDongChamBaoCao;
            }
            set
            {
                SetPropertyValue("HopDongChamBaoCao", ref _HopDongChamBaoCao, value);
            }
        }

        [ModelDefault("Caption", "Lớp")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
            }
        }

        [ModelDefault("Caption", "Sĩ số")]
        public int SiSo
        {
            get
            {
                return _SiSo;
            }
            set
            {
                SetPropertyValue("SiSo", ref _SiSo, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Báo cáo")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal BaoCao
        {
            get
            {
                return _BaoCao;
            }
            set
            {
                SetPropertyValue("BaoCao", ref _BaoCao, value);
                if (!IsLoading)
                {
                    ThanhTien = BaoCao * SoTien;
                }
            }
        }

        [ImmediatePostData]
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
                    ThanhTien = BaoCao * SoTien;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thành tiền")]
        public decimal ThanhTien
        {
            get
            {
                return _ThanhTien;
            }
            set
            {
                SetPropertyValue("ThanhTien", ref _ThanhTien, value);
                if (!IsLoading && value > 0)
                {
                    if (HopDongChamBaoCao.TongSoTien + value > HopDongChamBaoCao.ThongTinTruong.MocTinhThueTNCN.MucThuNhapTinhThue)
                    {
                        if (HopDongChamBaoCao.NhanVien.NhanVienThongTinLuong.KhongCuTru)
                            ThueTNCN = value * HopDongChamBaoCao.ThongTinTruong.MocTinhThueTNCN.KhongCuTru / 100;
                        else if (string.IsNullOrEmpty(HopDongChamBaoCao.NhanVien.NhanVienThongTinLuong.MaSoThue))
                            ThueTNCN = value * HopDongChamBaoCao.ThongTinTruong.MocTinhThueTNCN.KhongCoMaSoThue / 100;
                        else
                            ThueTNCN = value * HopDongChamBaoCao.ThongTinTruong.MocTinhThueTNCN.CoMaSoThue / 100;
                    }
                }
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thuế TNCN")]
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

        [NonPersistent]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Số tiền còn lại")]
        public decimal SoTienConLai
        {
            get
            {
                return ThanhTien - ThueTNCN;
            }
        }

        public ChiTietHopDongChamBaoCao(Session session) : base(session) { }
    }

}
