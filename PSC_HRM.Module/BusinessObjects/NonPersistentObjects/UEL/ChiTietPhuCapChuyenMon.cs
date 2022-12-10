using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn đơn vị")]
    public class ChiTietPhuCapChuyenMon : BaseObject
    {
        // Fields...
        private decimal _HSPCChuyenMon;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    ThongTinNhanVien = null;
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    TrinhDoChuyenMon = value.NhanVienTrinhDo.TrinhDoChuyenMon;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trình độ chuyên môn")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
                if (!IsLoading && value != null)
                {
                    HeSoChuyenMon hspcChuyenMon = Session.FindObject<HeSoChuyenMon>(CriteriaOperator.Parse("TrinhDoChuyenMon=?", value.Oid));
                    if (hspcChuyenMon != null)
                        HSPCChuyenMon = hspcChuyenMon.HSPCChuyenMon;
                }
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC chuyên môn")]
        public decimal HSPCChuyenMon
        {
            get
            {
                return _HSPCChuyenMon;
            }
            set
            {
                SetPropertyValue("HSPCChuyenMon", ref _HSPCChuyenMon, value);
            }
        }

        public ChiTietPhuCapChuyenMon(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<HoSo.ThongTinNhanVien>(Session);
            NVList.Criteria = PSC_HRM.Module.HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
