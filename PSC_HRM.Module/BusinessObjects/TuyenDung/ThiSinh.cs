using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TuyenDung
{
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Thí sinh")]
    [Appearance("ThiSinh.VangThi", TargetItems = "DiemSo", Enabled = false, Criteria = "VangThi")]
    [Appearance("ThiSinh.MienThi", TargetItems = "DiemSo;VangThi", Enabled = false, Criteria = "MienThi")]
    public class ThiSinh : BaseObject
    {
        // Fields...
        private bool _VangThi;
        private bool _MienThi;
        private string _GhiChu;
        private int _DiemSo;
        private UngVien _UngVien;
        private DanhSachThi _DanhSachThi;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách thi")]
        [Association("DanhSachThi-ListThiSinh")]
        public DanhSachThi DanhSachThi
        {
            get
            {
                return _DanhSachThi;
            }
            set
            {
                SetPropertyValue("DanhSachThi", ref _DanhSachThi, value);
                if (!IsLoading && value != null)
                {
                    UngVien = null;
                    UpdateUngVienList();
                }
            }
        }

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
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Miễn thi")]
        public bool MienThi
        {
            get
            {
                return _MienThi;
            }
            set
            {
                SetPropertyValue("MienThi", ref _MienThi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Vắng thi")]
        public bool VangThi
        {
            get
            {
                return _VangThi;
            }
            set
            {
                SetPropertyValue("VangThi", ref _VangThi, value);
            }
        }

        [ModelDefault("Caption", "Điểm số")]
        public int DiemSo
        {
            get
            {
                return _DiemSo;
            }
            set
            {
                SetPropertyValue("DiemSo", ref _DiemSo, value);
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

        public ThiSinh(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<UngVien> UngVienList { get; set; }

        private void UpdateUngVienList()
        {
            if (UngVienList == null)
                UngVienList = new XPCollection<UngVien>(Session);
            else
                UngVienList.Reload();

            //chỉ lấy các ứng viên ở vòng tuyển dụng cuối cùng
            if (DanhSachThi.BuocTuyenDung != null)
            {
                CriteriaOperator filter;
                filter = CriteriaOperator.Parse("VongTuyenDung.BuocTuyenDung=? and VongTuyenDung.ChiTietTuyenDung=?",
                    DanhSachThi.BuocTuyenDung.Oid, DanhSachThi.ChiTietTuyenDung.Oid);

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
