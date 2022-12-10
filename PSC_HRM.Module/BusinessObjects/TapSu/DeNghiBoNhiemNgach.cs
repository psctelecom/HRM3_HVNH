using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.TapSu
{
     [DefaultProperty("ThangText")]
    [ImageName("BO_BoNhiemNgach")]
    [ModelDefault("Caption", "Đề nghị bổ nhiệm ngạch")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyTapSu;Thang")]
    public class DeNghiBoNhiemNgach : BaseObject
    {
        // Fields...
        private DateTime _Thang;
        private QuanLyTapSu _QuanLyTapSu;
        private QuyetDinhHuongDanTapSu _QuyetDinhHuongDanTapSu;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý tập sự")]
        [Association("QuanLyTapSu-ListDeNghiBoNhiemNgach")]
        public QuanLyTapSu QuanLyTapSu
        {
            get
            {
                return _QuanLyTapSu;
            }
            set
            {
                SetPropertyValue("QuanLyTapSu", ref _QuanLyTapSu, value);
            }
        }

        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.StartMonth);
                ThangText = value.ToString("MM/yyyy");
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Tháng")]
        public string ThangText { get; set; }

        [ImmediatePostData]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Quyết định hướng dẫn tập sự")]
        public QuyetDinhHuongDanTapSu QuyetDinhHuongDanTapSu
        {
            get
            {
                return _QuyetDinhHuongDanTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuongDanTapSu", ref _QuyetDinhHuongDanTapSu, value);
                if (!IsLoading && value != null)
                {
                    ChiTietDeNghiBoNhiemNgach chiTiet;
                    foreach (ChiTietQuyetDinhTuyenDung item in value.QuyetDinhTuyenDung.ListChiTietQuyetDinhTuyenDung)
                    {
                        chiTiet = Session.FindObject<ChiTietDeNghiBoNhiemNgach>(CriteriaOperator.Parse("DeNghiBoNhiemNgach=? and ThongTinNhanVien=?", Oid, item.ThongTinNhanVien));
                        if (chiTiet == null)
                        {
                            chiTiet = new ChiTietDeNghiBoNhiemNgach(Session);
                            chiTiet.DeNghiBoNhiemNgach = this;
                            chiTiet.BoPhan = item.BoPhan;
                            chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                        }
                        chiTiet.NgachLuong = item.NgachLuong;
                        chiTiet.BacLuong = item.BacLuong;
                        chiTiet.HeSoLuong = item.HeSoLuong;
                        chiTiet.NgayBoNhiemNgach = item.NgayBoNhiemNgach;
                        chiTiet.NgayHuongLuong = item.NgayHuongLuong;
                        //
                        this.ListChiTietDeNghiBoNhiemNgach.Add(chiTiet);
                    }
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("DeNghiBoNhiemNgach-ListChiTietDeNghiBoNhiemNgach")]
        public XPCollection<ChiTietDeNghiBoNhiemNgach> ListChiTietDeNghiBoNhiemNgach
        {
            get
            {
                return GetCollection<ChiTietDeNghiBoNhiemNgach>("ListChiTietDeNghiBoNhiemNgach");
            }
        }

        public DeNghiBoNhiemNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            Thang = HamDungChung.GetServerTime();
        }
    }

}
