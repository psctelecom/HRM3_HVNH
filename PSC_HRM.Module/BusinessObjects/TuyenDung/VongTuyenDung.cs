using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.TuyenDung
{
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [DefaultProperty("BuocTuyenDung")]
    [ModelDefault("Caption", "Chi tiết vòng tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique("VongTuyenDung", DefaultContexts.Save, "ChiTietTuyenDung;BuocTuyenDung")]
    [Appearance("VongTuyenDung", TargetItems = "BuocTuyenDung", Enabled = false, Criteria = "BuocTuyenDung is not null")]
    public class VongTuyenDung : BaseObject
    {
        // Fields...
        private BuocTuyenDung _BuocTuyenDung;
        private ChiTietTuyenDung _ChiTietTuyenDung;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Chi tiết tuyển dụng")]
        [Association("ChiTietTuyenDung-ListVongTuyenDung")]
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
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Tên vòng tuyển dụng")]
        [DataSourceProperty("ChiTietTuyenDung.ListBuocTuyenDung", DataSourcePropertyIsNullMode.SelectNothing)]
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
                    //load danh sách ứng viên đạt ở vòng trước vô đây
                    CriteriaOperator filter;
                    ChiTietVongTuyenDung chiTietVongTuyenDung;
                    if (value.ThuTu == 1)
                    {
                        filter = CriteriaOperator.Parse("NhuCauTuyenDung.ViTriTuyenDung=?",
                            ChiTietTuyenDung.ViTriTuyenDung.Oid);
                        using (XPCollection<UngVien> ungVienList = new XPCollection<UngVien>(Session, filter))
                        {
                            foreach (UngVien item in ungVienList)
                            {
                                filter = CriteriaOperator.Parse("VongTuyenDung=? and UngVien=?", Oid, item.Oid);
                                chiTietVongTuyenDung = Session.FindObject<ChiTietVongTuyenDung>(filter);
                                if (chiTietVongTuyenDung == null)
                                {
                                    chiTietVongTuyenDung = new ChiTietVongTuyenDung(Session);
									ListChiTietVongTuyenDung.Add(chiTietVongTuyenDung);
                                    chiTietVongTuyenDung.UngVien = item;                                    
                                }
                            }
                        }
                    }
                    else
                    {
                        int thuTu = value.ThuTu - 1;
                        filter = CriteriaOperator.Parse("VongTuyenDung.ChiTietTuyenDung.ViTriTuyenDung=? and VongTuyenDung.BuocTuyenDung.ThuTu=? and DuocChuyenQuaVongSau",
                            ChiTietTuyenDung.ViTriTuyenDung.Oid, thuTu);
                        using (XPCollection<ChiTietVongTuyenDung> chiTietList = new XPCollection<ChiTietVongTuyenDung>(Session, filter))
                        {
                            foreach (ChiTietVongTuyenDung item in chiTietList)
                            {
                                filter = CriteriaOperator.Parse("VongTuyenDung=? and UngVien=?", Oid, item.UngVien.Oid);
                                chiTietVongTuyenDung = Session.FindObject<ChiTietVongTuyenDung>(filter);
                                if (chiTietVongTuyenDung == null)
                                {
                                    chiTietVongTuyenDung = new ChiTietVongTuyenDung(Session);
									ListChiTietVongTuyenDung.Add(chiTietVongTuyenDung);
                                    chiTietVongTuyenDung.UngVien = item.UngVien;               
                                }
                            }
                        }
                    }
                }
            }
        }

        //[Aggregated]
        [ModelDefault("Caption", "Danh sách ứng viên")]
        [Association("VongTuyenDung-ListChiTietVongTuyenDung")]
        public XPCollection<ChiTietVongTuyenDung> ListChiTietVongTuyenDung
        {
            get
            {
                return GetCollection<ChiTietVongTuyenDung>("ListChiTietVongTuyenDung");
            }
        }

        public VongTuyenDung(Session session) : base(session) { }

        //public bool IsExist(UngVien ungVien)
        //{
        //    foreach (ChiTietVongTuyenDung item in ListChiTietVongTuyenDung)
        //    {
        //        if (item.UngVien.Oid == ungVien.Oid)
        //            return true;
        //    }
        //    return false;
        //}

        protected override void OnDeleting()
        {
            Session.Delete(ListChiTietVongTuyenDung);
            Session.Save(ListChiTietVongTuyenDung);

            base.OnDeleting();
        }
    }
}
