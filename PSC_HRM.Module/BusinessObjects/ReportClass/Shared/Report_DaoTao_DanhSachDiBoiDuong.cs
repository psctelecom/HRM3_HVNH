using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Danh sách nhân viên đi bồi dưỡng")]
    public class Report_DaoTao_DanhSachDiBoiDuong : StoreProcedureReport
    {
        // Fields...
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private LoaiDaoTaoEnum _LoaiDaoTao;
        private LoaiHinhBoiDuong _LoaiHinhBoiDuong;

        [ModelDefault("Caption", "Loại đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiDaoTaoEnum LoaiDaoTao
        {
            get
            {
                return _LoaiDaoTao;
            }
            set
            {
                SetPropertyValue("LoaiDaoTao", ref _LoaiDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Loại hình bồi dưỡng")]
        public LoaiHinhBoiDuong LoaiHinhBoiDuong
        {
            get
            {
                return _LoaiHinhBoiDuong;
            }
            set
            {
                SetPropertyValue("LoaiHinhBoiDuong", ref _LoaiHinhBoiDuong, value);
            }
        }

        [ModelDefault("Caption", "Từ tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến tháng")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        public Report_DaoTao_DanhSachDiBoiDuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            LoaiDaoTao = LoaiDaoTaoEnum.TatCa;
        }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("[spd_Report_BoiDuong_DanhSachCanBoDiBoiDuong]");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TuNgay", TuNgay);
            cmd.Parameters.AddWithValue("@DenNgay", DenNgay);
            cmd.Parameters.AddWithValue("@LoaiDaoTao", LoaiDaoTao);
            if (LoaiHinhBoiDuong != null)
                cmd.Parameters.AddWithValue("@TenLoaiHinhBoiDuong", LoaiHinhBoiDuong.TenLoaiHinhBoiDuong);
            else
                cmd.Parameters.AddWithValue("@TenLoaiHinhBoiDuong", string.Empty);

            return cmd;
        }
    }

}
