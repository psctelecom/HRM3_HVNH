using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.CauHinh;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [Appearance("Hide_ChiTietCheDoXaHoi_VHU", TargetItems = "SoThang,GiamTru"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyCheDoXaHoi.ThongTinTruong.TenVietTat = 'VHU'")]
    [Appearance("Hide_ChiTietCheDoXaHoi_DNU", TargetItems = "TuNgay,DenNgay"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyCheDoXaHoi.ThongTinTruong.TenVietTat = 'DNU'")]
    [ModelDefault("Caption","Chi tiết chế độ xã hội")]
    public class ChiTietCheDoXaHoi : BaseObject
    {
        private QuanLyCheDoXaHoi _QuanLyCheDoXaHoi;
        //
        private NhanVien _NhanVien;
        private CheDoXaHoi _CheDoXaHoi;
        private decimal _SoThang;
        private decimal _GiamTru;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        //

        [ModelDefault("Caption", "Quản lý chế độ xã hội")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyCheDoXaHoi-ListChiTietCheDoXH")]
        public QuanLyCheDoXaHoi QuanLyCheDoXaHoi
        {
            get
            {
                return _QuanLyCheDoXaHoi;
            }
            set
            {
                SetPropertyValue("QuanLyCheDoXaHoi", ref _QuanLyCheDoXaHoi, value);
            }
        }


        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Giảng viên")]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ImmediatePostData]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Chế độ xã hội")]
        public CheDoXaHoi CheDoXaHoi
        {
            get
            {
                return _CheDoXaHoi;
            }
            set
            {
                SetPropertyValue("CheDoXaHoi", ref _CheDoXaHoi, value);
                if (!IsLoading && value != null)
                {
                    GiamTru = TinhGiamTru(value.TinhTheoThang);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số tháng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoThang
        {
            get
            {
                return _SoThang;
            }
            set
            {
                SetPropertyValue("SoThang", ref _SoThang, value);
                if(!IsLoading && value != 0)
                {
                    GiamTru = TinhGiamTru(CheDoXaHoi.TinhTheoThang);
                }
            }
        }

        [ModelDefault("Caption", "Giảm trừ(Giờ)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GiamTru
        {
            get
            {
                return _GiamTru;
            }
            set
            {
                SetPropertyValue("GiamTru", ref _GiamTru, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }
        public ChiTietCheDoXaHoi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            SoThang = 0;
        }

        public decimal TinhGiamTru(bool DK)
        {
            decimal KQ = 0;
            CauHinhChung chChung = Session.FindObject<CauHinhChung>(CriteriaOperator.Parse("ThongTinTruong =?", HamDungChung.ThongTinTruong(Session).Oid));
            if (chChung != null)
            {
                if (DK)
                {
                    KQ = SoThang * ((chChung.SoGioChuan * CheDoXaHoi.PhanTramGiamTru) / 100);
                }
                else
                {
                    KQ = ((chChung.SoGioChuan * CheDoXaHoi.PhanTramGiamTru) / 100);
                }
            }
            return KQ;
        }
    }
}
