using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.ThuNhap.ThuLao;
using PSC_HRM.Module.CauHinh;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.ThuNhap.Report.QNU_PMS
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng thanh toán thù lao nhân viên")]
    public class Report_ThanhToanThuLaoNhanVien : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private BangThuLaoNhanVien _BangChotThuLao;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField("", DefaultContexts.Save, "Trường")]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }
        [ModelDefault("Caption", "Bảng thù lao")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn Bảng thù lao")]
        public BangThuLaoNhanVien BangThuLaoNhanVien
        {
            get
            {
                return _BangChotThuLao;
            }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        public Report_ThanhToanThuLaoNhanVien(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_Report_ThanhToanThuLaoGiangDay", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@BangThuLaoNhanVien", BangThuLaoNhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);

                da.Fill(DataSource);
            }
        }
    }

}
