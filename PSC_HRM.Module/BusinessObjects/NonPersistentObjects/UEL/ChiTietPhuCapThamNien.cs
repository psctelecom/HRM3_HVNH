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
using PSC_HRM.Module.DoanDang;
using System.Collections.Generic;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn đơn vị")]
    public class ChiTietPhuCapThamNien : BaseObject
    {
        private decimal _HSPCThamNien;
        private int _SoNamCongTac;
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
                if (!IsLoading && value != null && value.NgayVaoCoQuan != DateTime.MinValue)
                {
                    SoNamCongTac = HamDungChung.TinhSoNam(value.NgayVaoCoQuan, HamDungChung.SetTime(HamDungChung.GetServerTime().AddMonths(1), 2));
                }
            }
        }

        [ModelDefault("Caption", "Số năm công tác")]
        public int SoNamCongTac
        {
            get
            {
                return _SoNamCongTac;
            }
            set
            {
                SetPropertyValue("SoNamCongTac", ref _SoNamCongTac, value);
                if (!IsLoading && value > 0)
                {
                    HeSoPhuCapThamNien hspcTrachNhiem = Session.FindObject<HeSoPhuCapThamNien>(CriteriaOperator.Parse("TuNam<=? and DenNam>?", value, value));
                    if (hspcTrachNhiem != null)
                        HSPCThamNien = hspcTrachNhiem.HSPCThamNien;
                }
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC thâm niên")]
        public decimal HSPCThamNien
        {
            get
            {
                return _HSPCThamNien;
            }
            set
            {
                SetPropertyValue("HSPCThamNien", ref _HSPCThamNien, value);
            }
        }

        public ChiTietPhuCapThamNien(Session session) : base(session) { }

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
