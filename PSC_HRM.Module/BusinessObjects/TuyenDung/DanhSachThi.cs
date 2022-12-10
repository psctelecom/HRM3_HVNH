using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.TuyenDung
{
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [DefaultProperty("MonThi")]
    [ModelDefault("Caption", "Danh sách thi")]
    [RuleCombinationOfPropertiesIsUnique("DanhSachThi.Unique", DefaultContexts.Save, "BuocTuyenDung;PhongThi;MonThi;NgayThi;GioThi")]
    [Appearance("DanhSachThi", TargetItems = "BuocTuyenDung", Enabled = false, Criteria = "BuocTuyenDung is not null")]
    public class DanhSachThi : BaseObject
    {
        // Fields...
        private MonThi _MonThi;
        private BuocTuyenDung _BuocTuyenDung;
        private ChiTietTuyenDung _ChiTietTuyenDung;
        private DateTime _NgayThi;
        private int _ThoiGianThi;
        private string _GioThi;
        private PhongThi _PhongThi;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Chi tiết tuyển dụng")]
        [Association("ChiTietTuyenDung-ListDanhSachThi")]
        public ChiTietTuyenDung ChiTietTuyenDung
        {
            get
            {
                return _ChiTietTuyenDung;
            }
            set
            {
                SetPropertyValue("ChiTietTuyenDung", ref _ChiTietTuyenDung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Vòng tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("CoToChucThiTuyen")]
        [DataSourceProperty("ChiTietTuyenDung.ListBuocTuyenDung")]
        public BuocTuyenDung BuocTuyenDung
        {
            get
            {
                return _BuocTuyenDung;
            }
            set
            {
                SetPropertyValue("BuocTuyenDung", ref _BuocTuyenDung, value);
                if (!IsLoading && value != null)
                {
                    VongTuyenDung vongTuyenDung = Session.FindObject<VongTuyenDung>(CriteriaOperator.Parse("BuocTuyenDung=?", value.Oid));
                    if (vongTuyenDung != null)
                    {
                        ThiSinh thiSinh;
                        foreach (ChiTietVongTuyenDung item in vongTuyenDung.ListChiTietVongTuyenDung)
                        {
                            thiSinh = new ThiSinh(Session);
                            thiSinh.DanhSachThi = this;
                            thiSinh.UngVien = item.UngVien;
                        }
                    }
                }
            }
        }

        [ModelDefault("Caption", "Phòng thi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public PhongThi PhongThi
        {
            get
            {
                return _PhongThi;
            }
            set
            {
                SetPropertyValue("PhongThi", ref _PhongThi, value);
            }
        }

        [ModelDefault("Caption", "Môn thi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public MonThi MonThi
        {
            get
            {
                return _MonThi;
            }
            set
            {
                SetPropertyValue("MonThi", ref _MonThi, value);
            }
        }

        [ModelDefault("Caption", "Ngày thi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayThi
        {
            get
            {
                return _NgayThi;
            }
            set
            {
                SetPropertyValue("NgayThi", ref _NgayThi, value);
            }
        }

        [ModelDefault("Caption", "Giờ thi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string GioThi
        {
            get
            {
                return _GioThi;
            }
            set
            {
                SetPropertyValue("GioThi", ref _GioThi, value);
            }
        }

        [ModelDefault("Caption", "Thời gian thi (phút)")]
        public int ThoiGianThi
        {
            get
            {
                return _ThoiGianThi;
            }
            set
            {
                SetPropertyValue("ThoiGianThi", ref _ThoiGianThi, value);
            }
        }

        [ModelDefault("Caption", "Danh sách thí sinh")]
        [Association("DanhSachThi-ListThiSinh")]
        public XPCollection<ThiSinh> ListThiSinh
        {
            get
            {
                return GetCollection<ThiSinh>("ListThiSinh");
            }
        }

        public DanhSachThi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayThi = HamDungChung.GetServerTime();
        }

        public bool IsExist(UngVien ungVien)
        {
            foreach (ThiSinh item in ListThiSinh)
            {
                if (item.UngVien.Oid == ungVien.Oid)
                    return true;
            }
            return false;
        }

        protected override void OnDeleting()
        {
            Session.Delete(ListThiSinh);
            Session.Save(ListThiSinh);

            base.OnDeleting();
        }
    }

}
