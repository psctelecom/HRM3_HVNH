using System;
using System.ComponentModel;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ThuNhap.TruyLuong
{
    [ModelDefault("Caption", "Chi tiết truy lĩnh khác")]
    [Appearance("Hide_DLU", TargetItems = "SoTienCu", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='DLU'")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietTruyLinhKhac.Unique", DefaultContexts.Save, "TruyLinhKhac;MaChiTiet")]
    public class ChiTietTruyLinhKhac : ThuNhapBaseObject
    {
        private TruyLinhKhac _TruyLinhKhac;
        private string _MaChiTiet;
        private string _DienGiai;
        private CongTruEnum _CongTru;
        private decimal _SoTienTruyLinh;
        private decimal _SoTienCu;
        
        [Browsable(false)]
        [ModelDefault("Caption", "Bảng truy lĩnh")]
        [Association("TruyLinhKhac-ListChiTietTruyLinhKhac")]
        public TruyLinhKhac TruyLinhKhac
        {
            get
            {
                return _TruyLinhKhac;
            }
            set
            {
                SetPropertyValue("TruyLinhKhac", ref _TruyLinhKhac, value);
            }
        }
             
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Mã chi tiết")]
        public string MaChiTiet
        {
            get
            {
                return _MaChiTiet;
            }
            set
            {
                SetPropertyValue("MaChiTiet", ref _MaChiTiet, value);
            }
        }

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

        [ModelDefault("Caption", "Cộng/Trừ")]
        public CongTruEnum CongTru
        {
            get
            {
                return _CongTru;
            }
            set
            {
                SetPropertyValue("CongTru", ref _CongTru, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTienTruyLinh
        {
            get
            {
                return _SoTienTruyLinh;
            }
            set
            {
                SetPropertyValue("SoTienTruyLinh", ref _SoTienTruyLinh, value);
            }
        }

        [ModelDefault("Caption", "Số tiền cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTienCu
        {
            get
            {
                return _SoTienCu;
            }
            set
            {
                SetPropertyValue("SoTienCu", ref _SoTienCu, value);
            }
        }

        [Browsable(false)]
        [NonPersistent]
        public string MaTruong { get; set; }

        public ChiTietTruyLinhKhac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            MaTruong = TruongConfig.MaTruong;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            //
            MaTruong = TruongConfig.MaTruong;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (MaTruong.Equals("LUH"))
            {
                if (!IsDeleted && Oid != Guid.Empty)
                {
                    Save_XuLy();
                }
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                if (MaTruong.Equals("LUH"))
                {
                    Delete_XuLy();
                }
            }
            base.OnDeleting();
        }

        public void Save_XuLy()
        {
            GroupOperator go = new GroupOperator(GroupOperatorType.And);

            go.Operands.Add(CriteriaOperator.Parse("ThongTinNhanVien.Oid = ?", TruyLinhKhac.ThongTinNhanVien.Oid));
            go.Operands.Add(CriteriaOperator.Parse("BangLuongNhanVien.KyTinhLuong.Oid = ?", TruyLinhKhac.BangTruyLinhKhac.KyTinhLuong.Oid));
            
            LuongNhanVien luongNhanVien = Session.FindObject<LuongNhanVien>(go);
            
            if (luongNhanVien != null && CongTru == CongTruEnum.Cong)
            {
                ChiTietLuongNhanVien ctLuongNhanVien = Session.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien.Oid = ? and MaChiTiet like ?", luongNhanVien.Oid, MaChiTiet));
                if (ctLuongNhanVien != null)
                {
                    if (ctLuongNhanVien.SoTien != SoTienCu + SoTienTruyLinh)
                    {
                        SoTienCu = ctLuongNhanVien.SoTien;
                        ctLuongNhanVien.SoTien = SoTienCu + SoTienTruyLinh;
                        ctLuongNhanVien.TienLuong = SoTienCu + SoTienTruyLinh;
                        ctLuongNhanVien.GhiChu = TruyLinhKhac.GhiChu;
                        luongNhanVien.ThucLanh = luongNhanVien.ThucLanh + SoTienTruyLinh;
                        luongNhanVien.TongTienLuong = luongNhanVien.TongTienLuong + SoTienTruyLinh;
                    }
                }
                else
                {
                    ctLuongNhanVien = new ChiTietLuongNhanVien(Session);
                    ctLuongNhanVien.LuongNhanVien = luongNhanVien;
                    ctLuongNhanVien.MaChiTiet = MaChiTiet;
                    ctLuongNhanVien.DienGiai = DienGiai;
                    ctLuongNhanVien.SoTien = SoTienCu + SoTienTruyLinh;
                    ctLuongNhanVien.TienLuong = SoTienCu + SoTienTruyLinh;
                    ctLuongNhanVien.SoTienChiuThue = SoTienCu + SoTienTruyLinh;
                    ctLuongNhanVien.CongTru = CongTruEnum.Cong;
                    ctLuongNhanVien.GhiChu = TruyLinhKhac.GhiChu;
                    luongNhanVien.ThucLanh = luongNhanVien.ThucLanh + SoTienTruyLinh;
                    luongNhanVien.TongTienLuong = luongNhanVien.TongTienLuong + SoTienTruyLinh;
                }
            }
        }

        public void Delete_XuLy()
        {
            GroupOperator go = new GroupOperator(GroupOperatorType.And);

            go.Operands.Add(CriteriaOperator.Parse("ThongTinNhanVien.Oid = ?", TruyLinhKhac.ThongTinNhanVien.Oid));
            go.Operands.Add(CriteriaOperator.Parse("BangLuongNhanVien.KyTinhLuong.Oid = ?", TruyLinhKhac.BangTruyLinhKhac.KyTinhLuong.Oid));
            
            LuongNhanVien luongNhanVien = Session.FindObject<LuongNhanVien>(go);

            if (luongNhanVien != null && CongTru == CongTruEnum.Cong)
            {
                ChiTietLuongNhanVien ctLuongNhanVien = Session.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien.Oid = ? and MaChiTiet like ?", luongNhanVien.Oid, MaChiTiet));
                if (ctLuongNhanVien != null)
                {
                    if (ctLuongNhanVien.SoTien == SoTienCu + SoTienTruyLinh)
                    {
                        if (SoTienCu != 0)
                        {
                            ctLuongNhanVien.SoTien = SoTienCu;
                            ctLuongNhanVien.TienLuong = SoTienCu;
                            ctLuongNhanVien.GhiChu = "";
                            luongNhanVien.ThucLanh = luongNhanVien.ThucLanh - SoTienTruyLinh;
                            luongNhanVien.TongTienLuong = luongNhanVien.TongTienLuong - SoTienTruyLinh;
                        }
                        else
                        {
                            luongNhanVien.ThucLanh = luongNhanVien.ThucLanh - SoTienTruyLinh;
                            luongNhanVien.TongTienLuong = luongNhanVien.TongTienLuong - SoTienTruyLinh;
                            
                            Session.Delete(ctLuongNhanVien);
                            Session.Save(ctLuongNhanVien);
                        }
                    }
                }
            }
        }
    }

}
 