using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.TaoMaQuanLy;
using System.Data.SqlClient;
using PSC_HRM.Module;


namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_Contract")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng chấm báo cáo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyHopDongThinhGiang;SoHopDong")]
    public class HopDong_ChamBaoCao : HopDong
    {
        // Fields...
        private string _MonChamBaoCao;
        private decimal _ThucLanh;
        private decimal _ThueTNCN;
        private decimal _TongSoTien;

        [ModelDefault("Caption", "Môn chấm báo cáo")]
        public string MonChamBaoCao
        {
            get
            {
                return _MonChamBaoCao;
            }
            set
            {
                SetPropertyValue("MonChamBaoCao", ref _MonChamBaoCao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tổng số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongSoTien
        {
            get
            {
                return _TongSoTien;
            }
            set
            {
                SetPropertyValue("TongSoTien", ref _TongSoTien, value);
                if (!IsLoading)
                {
                    if (ThongTinTruong.MocTinhThueTNCN != null && value >= ThongTinTruong.MocTinhThueTNCN.MucThuNhapTinhThue)
                    {
                        if (NhanVien.NhanVienThongTinLuong.KhongCuTru)
                            ThueTNCN = TongSoTien * ThongTinTruong.MocTinhThueTNCN.KhongCuTru / 100;
                        else if (string.IsNullOrEmpty(NhanVien.NhanVienThongTinLuong.MaSoThue))
                            ThueTNCN = TongSoTien * ThongTinTruong.MocTinhThueTNCN.KhongCoMaSoThue / 100;
                        else
                            ThueTNCN = TongSoTien * ThongTinTruong.MocTinhThueTNCN.CoMaSoThue / 100;
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thuế TNCN")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCN
        {
            get
            {
                return _ThueTNCN;
            }
            set
            {
                SetPropertyValue("ThueTNCN", ref _ThueTNCN, value);
                if (!IsLoading)
                {
                    ThucLanh = TongSoTien - value;
                }
            }
        }

        [ModelDefault("Caption", "Thực lãnh")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThucLanh
        {
            get
            {
                return _ThucLanh;
            }
            set
            {
                SetPropertyValue("ThucLanh", ref _ThucLanh, value);
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Danh sách lớp")]
        [Association("HopDongChamBaoCao-ListChiTietHopDongChamBaoCao")]
        public XPCollection<ChiTietHopDongChamBaoCao> ListChiTietHopDongChamBaoCao
        {
            get
            {
                return GetCollection<ChiTietHopDongChamBaoCao>("ListChiTietHopDongChamBaoCao");
            }
        }

        public HopDong_ChamBaoCao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LoaiHopDong = "Hợp đồng chấm báo cáo";
            UpdateNhanVienList();
        }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
            {
                NVList = new XPCollection<NhanVien>(Session, HamDungChung.GetCriteriaGiangVienThinhGiang(Session));
            }
        }
		
        protected override void TaoSoHopDong()
        {
            if (QuanLyHopDongThinhGiang != null)
            {
                SqlParameter param = new SqlParameter("@QuanLyHopDongThinhGiang", QuanLyHopDongThinhGiang.Oid);
                SoHopDong = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoHopDongChamBaoCao, param);
            }
        }
        
        protected override void TaoTrichYeu()
        {
            if (GiayToHoSo != null)
                GiayToHoSo.TrichYeu = LoaiHopDong;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsLoading)
            {
                decimal temp = 0;
                foreach (ChiTietHopDongChamBaoCao item in ListChiTietHopDongChamBaoCao)
                {
                    if (!item.IsDeleted)
                    {
                        temp += item.ThanhTien;
                    }
                }
                TongSoTien = temp;
            }
        }
    }

}
