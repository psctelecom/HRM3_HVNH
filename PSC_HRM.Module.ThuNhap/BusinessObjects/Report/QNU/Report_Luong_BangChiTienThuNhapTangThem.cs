using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng chi tiền thu nhập tăng thêm")]
    [Appearance("BangLuong.TatCaDonVi", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaDonVi")]
    public class Report_Luong_BangChiTienThuNhapTangThem : StoreProcedureReport
    {
        private KyTinhLuong _KyTinhLuong;
        private bool _TatCaDonVi = true;
        private BoPhan _BoPhan;     
        private QuyEnum _Quy = QuyEnum.QuyI;

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
                if (value != null)
                    CapNhatQuy();
            }
        }      

        [ModelDefault("Caption", "Quý")]
        public QuyEnum Quy
        {
            get { return _Quy; }
            set
            {
                SetPropertyValue("Quy", ref _Quy, value);                
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả đơn vị")]
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
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCaDonVi")]
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

        public Report_Luong_BangChiTienThuNhapTangThem(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            List<string> listBP = new List<string>();
            if (TatCaDonVi)
                listBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(Session);
            else
                listBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan);

            StringBuilder sb = new StringBuilder();
            foreach (string item in listBP)
            {
                sb.Append(String.Format("{0};", item));
            }

            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_Luong_BangChiTienThuNhapTangThem", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@KyTinhLuong", KyTinhLuong.Oid);
                da.SelectCommand.Parameters.AddWithValue("@Quy", Quy);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", sb.ToString());
                da.SelectCommand.CommandTimeout = 180;
                da.Fill(DataSource);
            }
        }        

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and !KhoaSo", DateTime.Now.Month, DateTime.Now.Year));
        }

        private void CapNhatQuy()
        {
            if (KyTinhLuong.Thang == 1 || KyTinhLuong.Thang == 2 || KyTinhLuong.Thang == 3)
                Quy = QuyEnum.QuyI;
            else if (KyTinhLuong.Thang == 4 || KyTinhLuong.Thang == 5 || KyTinhLuong.Thang == 6)
                Quy = QuyEnum.QuyII;
            else if (KyTinhLuong.Thang == 7 || KyTinhLuong.Thang == 8 || KyTinhLuong.Thang == 9)
                Quy = QuyEnum.QuyIII;
            else if (KyTinhLuong.Thang == 10 || KyTinhLuong.Thang == 11 || KyTinhLuong.Thang == 12)
                Quy = QuyEnum.QuyIV;          
        }
    }
}
