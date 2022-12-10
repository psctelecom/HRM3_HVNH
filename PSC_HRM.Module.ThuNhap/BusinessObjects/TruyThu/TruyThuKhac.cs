using System;
using System.ComponentModel;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.BaseImpl;

namespace PSC_HRM.Module.ThuNhap.TruyThu
{
    [ModelDefault("Caption", "Truy thu khác")]
    [RuleCombinationOfPropertiesIsUnique("TruyThuKhac.Unique", DefaultContexts.Save, "BangTruyThuKhac;ThongTinNhanVien")]
    public class TruyThuKhac : BaseObject
    {
        private BangTruyThuKhac _BangTruyThuKhac;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _GhiChu;
        
        [Browsable(false)]
        [ModelDefault("Caption", "Bảng truy thu")]
        [Association("BangTruyThuKhac-ListTruyThuKhac")]
        public BangTruyThuKhac BangTruyThuKhac
        {
            get
            {
                return _BangTruyThuKhac;
            }
            set
            {
                SetPropertyValue("BangTruyThuKhac", ref _BangTruyThuKhac, value);
            }
        }
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
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
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }
 

        [Size(500)]
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

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết truy thu khác")]
        [Association("TruyThuKhac-ListChiTietTruyThuKhac")]
        public XPCollection<ChiTietTruyThuKhac> ListChiTietTruyThuKhac
        {
            get
            {
                return GetCollection<ChiTietTruyThuKhac>("ListChiTietTruyThuKhac");
            }
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        public TruyThuKhac(Session session) : base(session) { }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);

            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.KhongConCongTacTaiTruong = false"));

            NVList.Criteria = go;
        }
    }

}
 