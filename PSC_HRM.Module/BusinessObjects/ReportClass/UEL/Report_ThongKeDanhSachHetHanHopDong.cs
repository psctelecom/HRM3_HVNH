using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{

    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Thống kê danh sách hết hạn hợp đồng")]
    [Appearance("Report_ThongKeDanhSachHetHanHopDong.Thang", TargetItems = "TuNgay,DenNgay,Quy", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiThongKe1=0")]
    [Appearance("Report_ThongKeDanhSachHetHanHopDong.Quy", TargetItems = "TuNgay,DenNgay,Thang", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiThongKe1=1")]
    [Appearance("Report_ThongKeDanhSachHetHanHopDong.Nam", TargetItems = "TuNgay,DenNgay,Thang,Quy", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiThongKe1=2")]
    [Appearance("Report_ThongKeDanhSachHetHanHopDong.KhoangThoiGian", TargetItems = "Thang,Nam,Quy", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiThongKe1=3")]
    public class Report_ThongKeDanhSachHetHanHopDong : StoreProcedureReport
    {
        public Report_ThongKeDanhSachHetHanHopDong(Session session) : base(session) { }
        
        private LoaiThongKe _LoaiThongKe = LoaiThongKe.Thang;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private int _Thang = 1;
        private int _Nam = DateTime.Today.Year;
        private QuyEnum _Quy = QuyEnum.QuyI;

        [ModelDefault("Caption", "Loại Thống Kê")]
        public LoaiThongKe LoaiThongKe1
        {
            get { return _LoaiThongKe; }
            set { _LoaiThongKe = value; }
        }

        [ModelDefault("Caption", "Tháng")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập tháng", TargetCriteria = "LoaiThongKe1=0")]
        [RuleRange("", DefaultContexts.Save, 1, 12, "Tháng nằm trong khoảng từ 1 -> 12")]
        public int Thang
        {
            get { return _Thang; }
            set
            {
                _Thang = value;
                Calculator();
            }
        }

        [ModelDefault("Caption", "Quý")]
        public QuyEnum Quy
        {
            get { return _Quy; }
            set
            {
                _Quy = value;
                Calculator();
            }
        }

        [ModelDefault("Caption", "Năm")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập năm", TargetCriteria = "LoaiThongKe1=0 OR LoaiThongKe1=1 OR LoaiThongKe1=2")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int Nam
        {
            get { return _Nam; }
            set
            {
                _Nam = value;
                Calculator();
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập ngày bắt đầu", TargetCriteria = "LoaiThongKe1=3")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime TuNgay
        {
            get { return _TuNgay; }
            set { _TuNgay = value; }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập ngày kết thúc", TargetCriteria = "LoaiThongKe1=3")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime DenNgay
        {
            get { return _DenNgay; }
            set { _DenNgay = value; }
        }

        private void Calculator()
        {
            switch (LoaiThongKe1)
            {
                case LoaiThongKe.Thang:
                    TuNgay = new DateTime(Nam, Thang, 1);
                    DenNgay = TuNgay.AddMonths(1).AddDays(-1);
                    break;
                case LoaiThongKe.Quy:
                    switch (Quy)
                    {
                        case QuyEnum.QuyI:
                            TuNgay = new DateTime(Nam, 1, 1);
                            DenNgay = TuNgay.AddMonths(3).AddDays(-1);
                            break;
                        case QuyEnum.QuyII:
                            TuNgay = new DateTime(Nam, 4, 1);
                            DenNgay = TuNgay.AddMonths(3).AddDays(-1);
                            break;
                        case QuyEnum.QuyIII:
                            TuNgay = new DateTime(Nam, 6, 1);
                            DenNgay = TuNgay.AddMonths(3).AddDays(-1);
                            break;
                        case QuyEnum.QuyIV:
                            TuNgay = new DateTime(Nam, 9, 1);
                            DenNgay = TuNgay.AddMonths(3).AddDays(-1);
                            break;
                        default:
                            TuNgay = new DateTime(Nam, 1, 1);
                            DenNgay = TuNgay.AddMonths(3).AddDays(-1);
                            break;
                    }
                    break;
                case LoaiThongKe.Nam:
                    TuNgay = new DateTime(Nam, 1, 1);
                    DenNgay = TuNgay.AddMonths(12).AddDays(-1);
                    break;
                default:
                    TuNgay = new DateTime(Nam, Thang, 1);
                    DenNgay = TuNgay.AddMonths(1).AddDays(-1);
                    break;
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Calculator();
        }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlConnection cnn = (SqlConnection)Session.Connection;
            SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThongKeHetHanHopDongTheoSoLuongHopDong", cnn);

            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@TuNgay", HamDungChung.SetTime(TuNgay, 0));
            da.SelectCommand.Parameters.AddWithValue("@DenNgay", HamDungChung.SetTime(DenNgay, 1));

            da.Fill(DataSource);
        }
    }

}
