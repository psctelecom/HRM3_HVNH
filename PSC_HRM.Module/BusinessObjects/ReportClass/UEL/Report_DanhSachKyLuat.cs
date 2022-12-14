using System;

using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Reports;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách kỷ luật")]
    [ImageName("BO_Report")]
    [Appearance("Report_DanhSachKyLua.TatCaDonVi", TargetItems = "DonVi;TatCaCanBo;CanBo", Enabled = false, Criteria = "TatCaDonVi")]
    [Appearance("Report_DanhSachKyLuat.TatCaCanBo", TargetItems = "CanBo", Enabled = false, Criteria = "TatCaCanBo")]
    public class Report_DanhSachKyLuat : ReportParametersObjectBase
    {
        private NamHoc _NamHoc;
        private bool _TatCaDonVi = true;
        private BoPhan _DonVi;
        private bool _TatCaCanBo = true;
        private ThongTinNhanVien _CanBo;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn năm học")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Tất cả đơn vị")]
        [ImmediatePostData]
        public bool TatCaDonVi
        {
            get
            {
                return _TatCaDonVi;
            }
            set
            {
                SetPropertyValue("TatCaDonVi", ref _TatCaDonVi, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn đơn vị", TargetCriteria = "!TatCaDonVi")]
        public BoPhan DonVi
        {
            get
            {
                return _DonVi;
            }
            set
            {
                SetPropertyValue("DonVi", ref _DonVi, value);
            }
        }

        [ModelDefault("Caption", "Tất cả cán bộ")]
        [ImmediatePostData]
        public bool TatCaCanBo
        {
            get
            {
                return _TatCaCanBo;
            }
            set
            {
                SetPropertyValue("TatCaCanBo", ref _TatCaCanBo, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn cán bộ", TargetCriteria = "!TatCaCanBo")]
        public ThongTinNhanVien CanBo
        {
            get
            {
                return _CanBo;
            }
            set
            {
                SetPropertyValue("CanBo", ref _CanBo, value);
            }
        }

        public Report_DanhSachKyLuat(Session session) : base(session) { }

        public override CriteriaOperator GetCriteria()
        {
            List<string> lstBP;
            if (TatCaDonVi)
                lstBP = DungChung.HamDungChung.DanhSachBoPhanDuocPhanQuyen();
            else
                lstBP = DungChung.HamDungChung.GetCriteriaBoPhan(Session, DonVi);

            GroupOperator go = new GroupOperator();
            go.OperatorType = GroupOperatorType.And;


            if (TatCaCanBo)
                go.Operands.Add(new InOperator("ThongTinNhanVienList[BoPhan.Oid=?]", lstBP));
            else
                go.Operands.Add(CriteriaOperator.Parse("ThongTinNhanVienList[Oid=?]", CanBo.Oid));

            go.Operands.Add(CriteriaOperator.Parse("NgayHieuLuc Between(?,?)", NamHoc.NgayBatDau, NamHoc.NgayKetThuc));

            return go;
        }

        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();

            return sorting;
        }
    }

}
