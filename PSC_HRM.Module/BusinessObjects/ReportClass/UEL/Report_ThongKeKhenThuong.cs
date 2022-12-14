using System;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Tổng hợp thống kê khen thưởng")]
    [Appearance("Report_ThongKeKhenThuong.ThoiGianE", TargetItems = "DenNam", Enabled = false, Criteria = "ThoiGian=0")]
    [Appearance("Report_ThongKeKhenThuong.LoaiE", TargetItems = "CaNhan,TatCaCaNhan", Enabled = false, Criteria = "Loai=1 OR TatCaDonVi")]
    [Appearance("Report_ThongKeKhenThuong.LoaiEsdfg", TargetItems = "CaNhan", Enabled = false, Criteria = "TatCaCaNhan")]
    [Appearance("Report_ThongKeKhenThuong.LoaiEsadfdfg", TargetItems = "DonVi", Enabled = false, Criteria = "TatCaDonVi")]
    public class Report_ThongKeKhenThuong : StoreProcedureReport
    {
        public Report_ThongKeKhenThuong(Session session) : base(session) { }

        public enum ThoiGianEnum
        {
            [DevExpress.Xpo.DisplayName("Năm")]
            Nam = 0,
            [DevExpress.Xpo.DisplayName("Khoảng thời gian")]
            KhoangThoiGian = 1
        }

        public enum LoaiEnum
        {
            [DevExpress.Xpo.DisplayName("Cá nhân")]
            CaNhan = 0,
            [DevExpress.Xpo.DisplayName("Đơn vị")]
            DonVi = 1
        }

        private int _TuNam;
        private int _DenNam;
        private bool _TatCaCaNhan;
        private bool _TatCaDonVi;

        private BoPhan _DonVi;
        private ThongTinNhanVien _CaNhan;

        private ThoiGianEnum _ThoiGian;
        private LoaiEnum _Loai;

        [ModelDefault("Caption", "Thời gian")]
        public ThoiGianEnum ThoiGian
        {
            get { return _ThoiGian; }
            set { SetPropertyValue<ThoiGianEnum>("ThoiGian", ref _ThoiGian, value); }
        }

        [ModelDefault("Caption", "Hình thức")]
        public LoaiEnum Loai
        {
            get { return _Loai; }
            set { SetPropertyValue<LoaiEnum>("Loai", ref _Loai, value); }
        }

        [ModelDefault("Caption", "Từ năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int TuNam
        {
            get { return _TuNam; }
            set { SetPropertyValue("TuNam", ref _TuNam, value); }
        }

        [ModelDefault("Caption", "Đến năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int DenNam
        {
            get { return _DenNam; }
            set { SetPropertyValue("DenNam", ref _DenNam, value); }
        }

        [ModelDefault("Caption", "Tất cả cá nhân")]
        [ImmediatePostData]
        public bool TatCaCaNhan
        {
            get { return _TatCaCaNhan; }
            set { SetPropertyValue("TatCaCaNhan", ref _TatCaCaNhan, value); }
        }

        [ModelDefault("Caption", "Tất cả đơn vị")]
        [ImmediatePostData]
        public bool TatCaDonVi
        {
            get { return _TatCaDonVi; }
            set { SetPropertyValue("TatCaDonVi", ref _TatCaDonVi, value); }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn đơn vị", TargetCriteria = "!TatCaDonVi")]
        public BoPhan DonVi
        {
            get { return _DonVi; }
            set { SetPropertyValue<BoPhan>("DonVi", ref _DonVi, value); }
        }

        [ModelDefault("Caption", "Cá nhân")]
        [DataSourceProperty("DonVi.ThongTinNhanVienList")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn cá nhân", TargetCriteria = "!TatCaCaNhan")]
        public ThongTinNhanVien CaNhan
        {
            get { return _CaNhan; }
            set { SetPropertyValue<ThongTinNhanVien>("CaNhan", ref _CaNhan, value); }
        }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            GroupOperator group = new GroupOperator();
            GroupOperator group1 = new GroupOperator();

            if (ThoiGian == ThoiGianEnum.Nam)
                group.Operands.Add(CriteriaOperator.Parse("NamKhenThuong=?", TuNam));
            else
                group.Operands.Add(CriteriaOperator.Parse("NamKhenThuong>=? AND NamKhenThuong<=?", TuNam, DenNam));


            XPCollection<DanhSachThiDuaKhenThuong> ds = new XPCollection<DanhSachThiDuaKhenThuong>(Session, group);

            if (ds != null)
            {
                DataTable.Columns.Add("HoTen", typeof(string));
                DataTable.Columns.Add("DonVi", typeof(string));
                DataTable.Columns.Add("DanhHieu", typeof(string));
                DataTable.Columns.Add("Nam", typeof(int));
                DataTable.Columns.Add("GhiChu", typeof(string));

                CriteriaOperator co = null;

                if (Loai == LoaiEnum.CaNhan)
                {
                    XPCollection<ChiTietCaNhanThiDuaKhenThuong> ctcn = null;

                    foreach (DanhSachThiDuaKhenThuong item in ds)
                    {
                        if (!TatCaCaNhan && !TatCaDonVi)
                            co = CriteriaOperator.Parse("DanhSachThiDuaKhenThuong=? AND ThongTinNhanVien=? AND BoPhan=?", item, CaNhan, DonVi);
                        else if (!TatCaDonVi)
                            co = CriteriaOperator.Parse("DanhSachThiDuaKhenThuong=? AND BoPhan=?", item, DonVi);
                        else
                            co = CriteriaOperator.Parse("DanhSachThiDuaKhenThuong=?", item);

                        ctcn = new XPCollection<ChiTietCaNhanThiDuaKhenThuong>(Session, co);

                        if (ctcn != null && ctcn.Count > 0)
                            foreach (ChiTietCaNhanThiDuaKhenThuong i in ctcn)
                            {
                                DataTable.Rows.Add(i.ThongTinNhanVien.HoTen, i.ThongTinNhanVien.BoPhan.TenBoPhan, item.DanhHieu, item.NamKhenThuong, i.GhiChu);
                            }
                    }
                }
                else
                {
                    XPCollection<ChiTietTapTheThiDuaKhenThuong> cttt = null;

                    foreach (DanhSachThiDuaKhenThuong item in ds)
                    {
                        if (!TatCaDonVi)
                            co = CriteriaOperator.Parse("DanhSachThiDuaKhenThuong=? AND BoPhan=?", item, DonVi);
                        else
                            co = CriteriaOperator.Parse("DanhSachThiDuaKhenThuong=?", item);

                        cttt = new XPCollection<ChiTietTapTheThiDuaKhenThuong>(Session, co);

                        foreach (ChiTietTapTheThiDuaKhenThuong i in cttt)
                        {
                            DataTable.Rows.Add("", i.BoPhan.TenBoPhan, item.DanhHieu, item.NamKhenThuong, i.GhiChu);
                        }
                    }
                }
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TuNam = DateTime.Today.Year;
            DenNam = TuNam;

            TatCaCaNhan = true;
            TatCaDonVi = true;
        }
    }

}
