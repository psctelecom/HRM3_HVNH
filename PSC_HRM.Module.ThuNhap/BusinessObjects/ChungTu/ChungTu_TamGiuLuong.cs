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
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    [DefaultClassOptions]
    [ImageName("BO_ChungTu")]
    [DefaultProperty("SoChungTu")]
    [ModelDefault("Caption", "Chứng từ tạm giữ lương")]
    public class ChungTu_TamGiuLuong : TruongBaseObject, IThongTinTruong
    {
        private int _SoThuTu;
        private string _SoChungTu;
        private DateTime _NgayLap= HamDungChung.GetServerTime();
        private KyTinhLuong _KyTinhLuong;
        private string _DienGiai ;
        private decimal _SoTien;
        private string _SoTienBangChu;
        private ThongTinTruong _ThongTinTruong;

        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Số thứ tự")]
        public int SoThuTu
        {
            get
            {
                return _SoThuTu;
            }
            set
            {
                SetPropertyValue("SoThuTu", ref _SoThuTu, value);
                if (!IsLoading && value > 0 && NgayLap != DateTime.MinValue)
                {
                    SoChungTu = string.Format("CK/{0:0#}/{1:####}", value, NgayLap.Year);
                }
            }
        }

        [ModelDefault("Caption", "Số chứng từ")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string SoChungTu
        {
            get
            {
                return _SoChungTu;
            }
            set
            {
                SetPropertyValue("SoChungTu", ref _SoChungTu, value);
            }
        }

        [ImmediatePostData]
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
                SetPropertyValue("NgayLap", ref _NgayLap, value.SetTime(SetTimeEnum.EndDay));
                if (!IsLoading && value != DateTime.MinValue)
                {
                    //update ky tinh luong
                    KyTinhLuong = null;
                    KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=?", 
                        value.Month, value.Year));

                    //Tạo số thứ tự tự tăng
                    CriteriaOperator filter = CriteriaOperator.Parse("NgayLap>=? and NgayLap<=?",
                        value.SetTime(SetTimeEnum.StartYear), value.SetTime(SetTimeEnum.EndYear));
                    //
                    object obj = Session.Evaluate<ChungTu_TamGiuLuong>(CriteriaOperator.Parse("Max(SoThuTu)"), filter);
                        if (obj != null)
                            SoThuTu = (int)obj + 1;
                    
                    if (SoThuTu == 0)
                        SoThuTu = 1;
                }
            }
        }

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
                if (!IsLoading && value != null)
                {
                    LayNganHangChuyenVaSoTaiKhoanChuyenTheoKyTinhLuong();
                    //
                    DienGiai = string.Format("Chuyển khoản lương nhân viên tháng {0:0#}/{1:####}", value.Thang, value.Nam);
                }
            }
        }

        [ModelDefault("Caption", "Diễn giải")]
        [RuleRequiredField("", DefaultContexts.Save)]
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
            }
        }

        [Size(300)]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Số tiền bằng chữ")]
        public string SoTienBangChu
        {
            get
            {
                return _SoTienBangChu;
            }
            set
            {
                SetPropertyValue("SoTienBangChu", ref _SoTienBangChu, value);
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ tạm giữ lương")]
        [Association("ChungTu_TamGiuLuong-ChiTietNhanVien")]
        public XPCollection<ChuyenKhoanLuongNhanVienChiTiet_TamGiuLuong> ChiTietList
        {
            get
            {
                return GetCollection<ChuyenKhoanLuongNhanVienChiTiet_TamGiuLuong>("ChiTietList");
            }
        }
        public ChungTu_TamGiuLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            //
            UpdateKyTinhLuongList();
            //
            KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and !KhoaSo", NgayLap.Month, NgayLap.Year));
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
                    AfterThongTinTruongChanged();
            }
        }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        private void UpdateKyTinhLuongList()
        {
            if (KyTinhLuongList == null)
                KyTinhLuongList = new XPCollection<KyTinhLuong>(Session);

            KyTinhLuongList.Criteria = CriteriaOperator.Parse("Nam=?", 
                NgayLap.Year);
        }

        protected virtual void AfterThongTinTruongChanged()
        { }

        private void LayNganHangChuyenVaSoTaiKhoanChuyenTheoKyTinhLuong()
        {
            if (this.KyTinhLuong != null)
            { 
                
                //
                foreach (ChuyenKhoanLuongNhanVienChiTiet_TamGiuLuong item in this.ChiTietList)
                {
                    if (item.NganHang != null)
                    {
                        //
                        CriteriaOperator criteria = CriteriaOperator.Parse("ThongTinTruong=? and NganHang=? and TaiKhoanChinh=1", KyTinhLuong.ThongTinTruong.Oid,item.NganHang.Oid);
                        //
                        TaiKhoanNganHang taiKhoanNganHang = Session.FindObject<TaiKhoanNganHang>(criteria);
                        if (taiKhoanNganHang != null)
                        {
                            item.NganHangChuyen = taiKhoanNganHang.NganHang;
                            item.SoTaiKhoanChuyen = taiKhoanNganHang.SoTaiKhoan;
                        }
                    }
                }
            }
        }
    }

}
