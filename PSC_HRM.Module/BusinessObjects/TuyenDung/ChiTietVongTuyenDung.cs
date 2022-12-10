using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TuyenDung
{
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Chi tiết vòng tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietVongTuyenDung", DefaultContexts.Save, "VongTuyenDung;UngVien")]
    [Appearance("ChiTietVongTuyenDung", TargetItems = "Diem", Enabled = false, Criteria = "DuocMien")]
    public class ChiTietVongTuyenDung : BaseObject
    {
        // Fields...
        private NhuCauTuyenDung _NhuCauTuyenDung;
        private bool _DuocMien;
        private bool _DuocChuyenQuaVongSau;
        private string _GhiChu;
        private decimal _Diem;
        private UngVien _UngVien;
        private VongTuyenDung _VongTuyenDung;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Vòng tuyển dụng")]
        [Association("VongTuyenDung-ListChiTietVongTuyenDung")]
        public VongTuyenDung VongTuyenDung
        {
            get
            {
                return _VongTuyenDung;
            }
            set
            {
                SetPropertyValue("VongTuyenDung", ref _VongTuyenDung, value);
                if (!IsLoading && value != null)
                {
                    UngVien = null;
                    UpdateUngVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ứng viên")]
        [DataSourceProperty("UngVienList")]
        [RuleRequiredField(DefaultContexts.Save)]
        public UngVien UngVien
        {
            get
            {
                return _UngVien;
            }
            set
            {
                SetPropertyValue("UngVien", ref _UngVien, value);
                if (!IsLoading && value != null)
                    NhuCauTuyenDung = value.NhuCauTuyenDung;
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Vị trí ứng tuyển")]
        public NhuCauTuyenDung NhuCauTuyenDung
        {
            get
            {
                return _NhuCauTuyenDung;
            }
            set
            {
                SetPropertyValue("NhuCauTuyenDung", ref _NhuCauTuyenDung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Được miễn")]
        public bool DuocMien
        {
            get
            {
                return _DuocMien;
            }
            set
            {
                SetPropertyValue("DuocMien", ref _DuocMien, value);
                if (!IsLoading && value)
                    DuocChuyenQuaVongSau = true;
            }
        }
        [ModelDefault("Caption", "Điểm")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal Diem
        {
            get
            {
                return _Diem;
            }
            set
            {
                SetPropertyValue("Diem", ref _Diem, value);
            }
        }

        [ModelDefault("Caption", "Được chuyển qua vòng sau")]
        public bool DuocChuyenQuaVongSau
        {
            get
            {
                return _DuocChuyenQuaVongSau;
            }
            set
            {
                SetPropertyValue("DuocChuyenQuaVongSau", ref _DuocChuyenQuaVongSau, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public ChiTietVongTuyenDung(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<UngVien> UngVienList { get; set; }

        private void UpdateUngVienList()
        {
            if (UngVienList == null)
                UngVienList = new XPCollection<UngVien>(Session);
            else
                UngVienList.Reload();


            if (VongTuyenDung.BuocTuyenDung != null)
            {
                CriteriaOperator filter;
                //vòng đầu tiên lấy hết những ứng viên theo nhu cầu tuyển dụng
                if (VongTuyenDung.BuocTuyenDung.ThuTu == 1)
                {
                    filter = CriteriaOperator.Parse("NhuCauTuyenDung.ViTriTuyenDung=?",
                        VongTuyenDung.ChiTietTuyenDung.ViTriTuyenDung.Oid);

                    UngVienList.Criteria = filter;
                }
                //các vòng sau chỉ lấy những ứng viên ở vòng trước thôi
                else
                {
                    int thuTu = VongTuyenDung.BuocTuyenDung.ThuTu - 1;
                    filter = CriteriaOperator.Parse("VongTuyenDung.BuocTuyenDung.ThuTu=? and VongTuyenDung.ChiTietTuyenDung=?",
                        thuTu, VongTuyenDung.ChiTietTuyenDung.Oid);

                    List<Guid> oid = new List<Guid>();
                    using (XPCollection<ChiTietVongTuyenDung> ctList = new XPCollection<ChiTietVongTuyenDung>(Session, filter))
                    {
                        foreach (ChiTietVongTuyenDung item in ctList)
                        {
                            oid.Add(item.UngVien.Oid);
                        }

                        UngVienList.Criteria = new InOperator("Oid", oid);
                    }
                }
            }
        }
    }

}
