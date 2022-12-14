using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.ThuNhap.ChungTu;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.Thue
{
    [DefaultClassOptions]
    [ImageName("BO_HoaDon")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowEdit", "False")]
    [ModelDefault("AllowDelete", "False")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Quản lý thuế thu nhập cá nhân")]
    //[RuleCombinationOfPropertiesIsUnique("QuanLyThueThuNhapCaNhan.Unique", DefaultContexts.Save, "KyTinhLuong;ThongTinTruong")]
    public class QuanLyThueThuNhapCaNhan : BaseObject, IThongTinTruong
    {
        // Fields...
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;
        private ChungTu.ChungTu _ChungTu;
        private ThongTinTruong _ThongTinTruong;

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

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }

        [ModelDefault("Caption", "Chứng từ chi tiền")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ChungTu.ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
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
                if (!IsLoading && value != null)
                {
                    KyTinhLuong = null;
                    UpdateKyTinhLuongList();
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách thuế TNCN")]
        [Association("QuanLyThueThuNhapCaNhan-ChiTietThueThuNhapCaNhan")]
        public XPCollection<ChiTietThueThuNhapCaNhan> DanhSachThueTNCNTamTru
        {
            get
            {
                return GetCollection<ChiTietThueThuNhapCaNhan>("DanhSachThueTNCNTamTru");
            }
        }

        public QuanLyThueThuNhapCaNhan(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        private void UpdateKyTinhLuongList()
        {
            if (KyTinhLuongList == null)
                KyTinhLuongList = new XPCollection<KyTinhLuong>(Session);

            if (ThongTinTruong != null)
                KyTinhLuongList.Criteria = CriteriaOperator.Parse("ThongTinTruong=? and !KhoaSo", ThongTinTruong);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NgayLap = HamDungChung.GetServerTime();
            if (ThongTinTruong != null)
                KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("ThongTinTruong = ? and TuNgay<=? and DenNgay>=? and !KhoaSo", ThongTinTruong, NgayLap, NgayLap));
        }       
    }
}
