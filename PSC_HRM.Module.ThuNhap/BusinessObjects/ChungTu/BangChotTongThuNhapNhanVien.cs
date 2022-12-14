using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    [DefaultClassOptions]
    [ImageName("BO_ChungTu")]
    [DefaultProperty("SoChungTu")]
    [ModelDefault("Caption", "Bảng tổng hợp thu nhập nhân viên")]
    [RuleCombinationOfPropertiesIsUnique("BangChotTongThuNhapNhanVien.Unique", DefaultContexts.Save, "KyTinhLuong;ThongTinTruong")]
    public class BangChotTongThuNhapNhanVien : BaseObject, IThongTinTruong
    {
        private KyTinhLuong _KyTinhLuong;
        private ThongTinTruong _ThongTinTruong;
        private DateTime _NgayChot = HamDungChung.GetServerTime();

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [DataSourceProperty("KyTinhLuongList")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [ModelDefault("Caption", "Ngày chốt")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayChot
        {
            get
            {
                return _NgayChot;
            }
            set
            {
                SetPropertyValue("NgayChot", ref _NgayChot, value.SetTime(SetTimeEnum.EndDay));
                if (!IsLoading)
                {
                    KyTinhLuong = null;
                    KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and !KhoaSo",NgayChot.Month, NgayChot.Year));
                }
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Thông tin trường")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }

        [Aggregated]
        [Association("BangChotTongThuNhapNhanVien-ListChiTietBangChotTongThuNhapNhanVien")]
        [ModelDefault("Caption", "Danh sách chi tiết tổng thu nhập")]
        public XPCollection<ChiTietBangChotTongThuNhapNhanVien> ChiTietBangChotTongThuNhapNhanVienList
        {
            get
            {
                return GetCollection<ChiTietBangChotTongThuNhapNhanVien>("ChiTietBangChotTongThuNhapNhanVienList");
            }
        }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        public BangChotTongThuNhapNhanVien(Session session) : base(session) { }

        private void UpdateKyTinhLuongList()
        {
            if (KyTinhLuongList == null)
                KyTinhLuongList = new XPCollection<KyTinhLuong>(Session);
            //
            if (ThongTinTruong != null)
                KyTinhLuongList.Criteria = CriteriaOperator.Parse("ThongTinTruong=? and !KhoaSo and Nam=?", ThongTinTruong,NgayChot.Year);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            //
            UpdateKyTinhLuongList();
            //
            KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and !KhoaSo", NgayChot.Month, NgayChot.Year));
        }
    }

}
