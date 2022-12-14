using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [ImageName("BO_List")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết định mức nộp thuế TNCN")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietDinhMucNopThueTNCN.Unique", DefaultContexts.Save, "BangDinhMucNopThueTNCN;ThongTinNhanVien")]
    public class ChiTietDinhMucNopThueTNCN : BaseObject, IBoPhan
    {
        // Fields...
        private decimal _DinhMucNopThueTNCN;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private BangDinhMucNopThueTNCN _BangDinhMucNopThueTNCN;

        [Browsable(false)]
        [Association("BangDinhMucNopThueTNCN-ListChiTietDinhMucNopThueTNCN")]
        public BangDinhMucNopThueTNCN BangDinhMucNopThueTNCN
        {
            get
            {
                return _BangDinhMucNopThueTNCN;
            }
            set
            {
                SetPropertyValue("BangDinhMucNopThueTNCN", ref _BangDinhMucNopThueTNCN, value);
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
                if (!IsLoading && value != null)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                if (!IsLoading && value != null)
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Định mức nộp thuế TNCN")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal DinhMucNopThueTNCN
        {
            get
            {
                return _DinhMucNopThueTNCN;
            }
            set
            {
                SetPropertyValue("DinhMucNopThueTNCN", ref _DinhMucNopThueTNCN, value);
            }
        }

        public ChiTietDinhMucNopThueTNCN(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);

            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? or TinhTrang.TenTinhTrang not like ?", "%nghỉ việc%", "%nghỉ hưu%"));

            NVList.Criteria = go;
        }
    }

}
