using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{  
    [ModelDefault("Caption", "Danh Sách Đến Hạn Nâng Lương")]
    [VisibleInReports(true)]
    [NonPersistent()]
    [ImageName("BO_Report")]
    [Appearance("DanhSachDenHanNangLuong.TatCaBoPhan", TargetItems = "BoPhan", Enabled = false,Criteria =  "TatCaBoPhan")]
    [Appearance("DanhSachDenHanNangLuong.Quy", TargetItems = "Quy", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiThongKe=0")]
    [Appearance("DanhSachDenHanNangLuong.Thang", TargetItems = "Thang", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiThongKe=1")]
    public class DanhSachDenHanNangLuong : StoreProcedureReport
    {
        private DateTime TuNgay;
        private DateTime DenNgay;
        private LoaiThongKeEnum loaiThongKe;
        private int thang = DateTime.Today.Month;
        private QuyEnum quy = QuyEnum.QuyI;
        private int nam = DateTime.Today.Year;

        public enum LoaiThongKeEnum
        {
            [DevExpress.Xpo.DisplayName("Theo tháng")]
            TheoThang = 0,
            [DevExpress.Xpo.DisplayName("Theo quý")]
            TheoQuy = 1
        }

        public DanhSachDenHanNangLuong(Session session) : base(session) { }

        [ModelDefault("Caption", "Loại")]
        public LoaiThongKeEnum LoaiThongKe
        {
            get { return loaiThongKe; }
            set { SetPropertyValue("LoaiThongKe", ref loaiThongKe, value); }
        }

        [ModelDefault("Caption", "Tháng")]
        [RuleRange("", DefaultContexts.Save, 1, 12, "Tháng không hợp lệ")]
        public int Thang
        {
            get { return thang; }
            set
            {
                SetPropertyValue("Thang", ref thang, value);

                if (!IsLoading)
                    TinhNgay();
            }
        }

        [ModelDefault("Caption", "Quý")]
        public QuyEnum Quy
        {
            get { return quy; }
            set
            {
                SetPropertyValue("Quy", ref quy, value);

                if (!IsLoading)
                    TinhNgay();
            }
        }

        [ModelDefault("Caption", "Năm")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int Nam
        {
            get { return nam; }
            set
            {
                SetPropertyValue("Nam", ref nam, value);

                if (!IsLoading)
                    TinhNgay();
            }
        }

        private bool _TatCaBoPhan = true;
        [ModelDefault("Caption", "Tất cả đơn vị")]
        [ImmediatePostData()] //Load dữ liệu ngay lập tức
        public bool TatCaBoPhan
        {
            get
            {
                return _TatCaBoPhan;
            }
            set
            {
                SetPropertyValue("TatCaBoPhan", ref _TatCaBoPhan, value);
            }
        }

        private BoPhan _BoPhan;
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
            }
        }
        public override SqlCommand CreateCommand()
        {
            
            List<string> lstBP;
            if (TatCaBoPhan)
                lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen();
            else
                lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan);

            StringBuilder sb = new StringBuilder();
            foreach (string item in lstBP)
            {
                sb.Append(String.Format("{0},", item));
            }

            SqlCommand cmd = new SqlCommand("spd_Report_DanhSachDenHanNangLuong", (SqlConnection)Session.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BoPhan", sb.ToString());
            cmd.Parameters.AddWithValue("@TuNgay", HamDungChung.SetTime(TuNgay, 0));
            cmd.Parameters.AddWithValue("@DenNgay", HamDungChung.SetTime(DenNgay, 1));

            return cmd;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TinhNgay();
        }

        private void TinhNgay()
        {
            if (LoaiThongKe == LoaiThongKeEnum.TheoThang)
            {
                TuNgay = new DateTime(Nam, Thang, 1);
                DenNgay = TuNgay.AddMonths(1).AddDays(-1);
            }
            else
            {
                switch (Quy)
                {
                    case QuyEnum.QuyI:
                        TuNgay = new DateTime(Nam, 1, 1);
                        break;
                    case QuyEnum.QuyII:
                        TuNgay = new DateTime(Nam, 4, 1);
                        break;
                    case QuyEnum.QuyIII:
                        TuNgay = new DateTime(Nam, 7, 1);
                        break;
                    case QuyEnum.QuyIV:
                        TuNgay = new DateTime(Nam, 10, 1);
                        break;
                    default:
                        TuNgay = new DateTime(Nam, 1, 1);
                        break;
                }
                DenNgay = TuNgay.AddMonths(4).AddDays(-1);
            }
        }
    }

}
