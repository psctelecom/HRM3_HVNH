using System;
using System.ComponentModel;
using DevExpress.Xpo;
using System.Linq;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.KhenThuong
{
    [DefaultProperty("Caption")]
    [ImageName("BO_QuanLyKhenThuong")]
    [ModelDefault("Caption", "Chi tiết đề nghị khen thưởng")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietDeNghiKhenThuong", DefaultContexts.Save, "QuanLyKhenThuong;DanhHieuKhenThuong")]
    [Appearance("ChiTietDeNghiKhenThuong", TargetItems = "DanhHieuKhenThuong", Enabled = false, Criteria = "DanhHieuKhenThuong is not null")]
    public class ChiTietDeNghiKhenThuong : BaseObject
    {
        // Fields...
        private DateTime _NgayLap;
        private DanhHieuKhenThuong _DanhHieuKhenThuong;
        private QuanLyKhenThuong _QuanLyKhenThuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý khen thưởng")]
        [Association("QuanLyKhenThuong-ListChiTietDeNghiKhenThuong")]
        public QuanLyKhenThuong QuanLyKhenThuong
        {
            get
            {
                return _QuanLyKhenThuong;
            }
            set
            {
                SetPropertyValue("QuanLyKhenThuong", ref _QuanLyKhenThuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Danh hiệu khen thưởng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DanhHieuKhenThuong DanhHieuKhenThuong
        {
            get
            {
                return _DanhHieuKhenThuong;
            }
            set
            {
                SetPropertyValue("DanhHieuKhenThuong", ref _DanhHieuKhenThuong, value);
                if (!IsLoading 
                    && QuanLyKhenThuong != null
                    && value != null)
                {
                    if (DanhHieuKhenThuong.MaQuanLy == "KNC" && TruongConfig.MaTruong == "BUH")
                    {
                        //Lấy danh sách nhân viên
                        XPCollection<ThongTinNhanVien> nhanVienList = new XPCollection<ThongTinNhanVien>(Session);
                        //Lấy danh sách nhân viên làm việc >15 năm
                        List<ThongTinNhanVien> nvList = (from x in nhanVienList 
                                                         where 
                                                         ( 
                                                            ((NgayLap.Year - x.NgayVaoCoQuan.Year) >= 15 && x.GioiTinh == GioiTinhEnum.Nu && x.NgayVaoCoQuan != DateTime.MinValue)
                                                            || ((NgayLap.Year - x.NgayVaoCoQuan.Year) >= 20 && x.GioiTinh == GioiTinhEnum.Nam && x.NgayVaoCoQuan != DateTime.MinValue)
                                                         )
                                                         select x).ToList();
                        if (nvList != null)
                        {
                            foreach (ThongTinNhanVien caNhan in nvList)
                            {
                                if (!ExistsNhanVien(caNhan))
                                {
                                    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and ChiTietDeNghiKhenThuong.DanhHieuKhenThuong.MaQuanLy like 'KNC' ", caNhan.Oid);
                                    ChiTietCaNhanDeNghiKhenThuong deNghiCaNhan = Session.FindObject<ChiTietCaNhanDeNghiKhenThuong>(filter);
                                    if (deNghiCaNhan == null)
                                    {
                                        deNghiCaNhan = new ChiTietCaNhanDeNghiKhenThuong(Session);
                                        deNghiCaNhan.ChiTietDeNghiKhenThuong = this;
                                        deNghiCaNhan.BoPhan = caNhan.BoPhan;
                                        deNghiCaNhan.ThongTinNhanVien = caNhan;
                                        deNghiCaNhan.GhiChu = "Kỷ niệm chương";
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (ChiTietDangKyThiDua dangKy in QuanLyKhenThuong.ListChiTietDangKyThiDua)
                        {
                            if (dangKy.DanhHieuKhenThuong != null
                                && dangKy.DanhHieuKhenThuong.Oid == value.Oid)
                            {
                                foreach (ChiTietCaNhanDangKyThiDua caNhan in dangKy.ListChiTietCaNhanDangKyThiDua)
                                {
                                    if (!ExistsNhanVien(caNhan.ThongTinNhanVien))
                                    {
                                        ChiTietCaNhanDeNghiKhenThuong deNghiCaNhan = new ChiTietCaNhanDeNghiKhenThuong(Session);
                                        deNghiCaNhan.ChiTietDeNghiKhenThuong = this;
                                        deNghiCaNhan.BoPhan = caNhan.BoPhan;
                                        deNghiCaNhan.ThongTinNhanVien = caNhan.ThongTinNhanVien;
                                        deNghiCaNhan.GhiChu = caNhan.GhiChu;
                                    }
                                }

                                foreach (ChiTietTapTheDangKyThiDua tapThe in dangKy.ListChiTietTapTheDangKyThiDua)
                                {
                                    if (!ExistsBoPhan(tapThe.BoPhan))
                                    {
                                        ChiTietTapTheDeNghiKhenThuong deNghiTapThe = new ChiTietTapTheDeNghiKhenThuong(Session);
                                        deNghiTapThe.ChiTietDeNghiKhenThuong = this;
                                        deNghiTapThe.BoPhan = tapThe.BoPhan;
                                        deNghiTapThe.GhiChu = tapThe.GhiChu;
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
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

        [Browsable(false)]
        public string Caption
        {
            get
            {
                if (QuanLyKhenThuong != null
                    && DanhHieuKhenThuong != null)
                    return string.Format("{0} {1}", QuanLyKhenThuong.NamHoc.TenNamHoc, DanhHieuKhenThuong.TenDanhHieu);
                return "";
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cá nhân")]
        [Association("ChiTietDeNghiKhenThuong-ListChiTietCaNhanDeNghiKhenThuong")]
        public XPCollection<ChiTietCaNhanDeNghiKhenThuong> ListChiTietCaNhanDeNghiKhenThuong
        {
            get
            {
                return GetCollection<ChiTietCaNhanDeNghiKhenThuong>("ListChiTietCaNhanDeNghiKhenThuong");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách tập thể")]
        [Association("ChiTietDeNghiKhenThuong-ListChiTietTapTheDeNghiKhenThuong")]
        public XPCollection<ChiTietTapTheDeNghiKhenThuong> ListChiTietTapTheDeNghiKhenThuong
        {
            get
            {
                return GetCollection<ChiTietTapTheDeNghiKhenThuong>("ListChiTietTapTheDeNghiKhenThuong");
            }
        }

        public ChiTietDeNghiKhenThuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLap = HamDungChung.GetServerTime();
        }

        private bool ExistsNhanVien(ThongTinNhanVien nhanVien)
        {
            bool exists = (from d in ListChiTietCaNhanDeNghiKhenThuong
                           where d.ThongTinNhanVien == nhanVien
                           select true).FirstOrDefault();

            return exists;
        }

        private bool ExistsBoPhan(BoPhan boPhan)
        {
            var exists = (from d in ListChiTietTapTheDeNghiKhenThuong
                          where d.BoPhan == boPhan
                          select true).Count() > 0;

            return exists;
        }
    }

}
