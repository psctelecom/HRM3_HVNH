using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DoanDang
{
    [ModelDefault("Caption", "Ủy viên ban chấp hành Đảng")]
    public class UyVienBanChapHanhDang : BaseObject
    {
        public UyVienBanChapHanhDang(Session session) : base(session) { }

        // Fields...
        private BanChapHanhDang _BanChapHanhDang;
        private ChucVuDang _ChucVuDang;
        //DangVien
        private ToChucDang _ChiBoDang;
        private DangVien _DangVien;


        [ModelDefault("Caption", "Chi bộ Đảng")]
        public ToChucDang ChiBoDang
        {
            get
            {
                return _ChiBoDang;
            }
            set
            {
                SetPropertyValue("ChiBoDang", ref _ChiBoDang, value);
            }
        }

        [ModelDefault("Caption", "Đảng viên")]
        public DangVien DangVien
        {
            get
            {
                return _DangVien;
            }
            set
            {
                SetPropertyValue("DangVien", ref _DangVien, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ Đảng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVuDang ChucVuDang
        {
            get
            {
                return _ChucVuDang;
            }
            set { SetPropertyValue("ChucVuDang", ref _ChucVuDang, value); }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Ban chấp hành Đảng")]
        [Association("BanChapHanhDang-UyVienBanChapHanh")]
        public BanChapHanhDang BanChapHanhDang
        {
            get
            {
                return _BanChapHanhDang;
            }
            set
            {
                SetPropertyValue("BanChapHanhDang", ref _BanChapHanhDang, value);
            }
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && this.Oid != null)
            {
                if (BanChapHanhDang.NhiemKy.TuNam <= HamDungChung.GetServerTime().Year
                    && BanChapHanhDang.NhiemKy.DenNam >= HamDungChung.GetServerTime().Year)
                { 
                    ChucVuDangKiemNhiem chucvu = Session.FindObject<ChucVuDangKiemNhiem>
                        (CriteriaOperator.Parse("ChucVuDang.Oid = ? and NgayBoNhiem = ? and DangVien.oid = ?",
                        ChucVuDang.Oid, new DateTime(BanChapHanhDang.NhiemKy.TuNam, 1, 1), DangVien.Oid));
                    if (chucvu == null)
                    {
                        DangVien.ChucVuDang = ChucVuDang;
                        DangVien.NgayBoNhiem = new DateTime(BanChapHanhDang.NhiemKy.TuNam, 1, 1);
                        //Thêm danh sách chức vụ kiêm nhiệm
                        chucvu = new ChucVuDangKiemNhiem(Session);
                        chucvu.ChucVuDang = ChucVuDang;
                        chucvu.NgayBoNhiem = new DateTime(BanChapHanhDang.NhiemKy.TuNam, 1, 1);
                        chucvu.DangVien = DangVien;
                    }
                }
            }
        }
    }

}
