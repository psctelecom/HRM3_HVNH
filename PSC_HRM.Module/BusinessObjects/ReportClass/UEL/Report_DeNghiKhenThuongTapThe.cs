using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.KhenThuong;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Danh sách tập thể đề nghị khen thưởng")]
    public class Report_DeNghiKhenThuongTapThe : StoreProcedureReport
    {
        private DanhHieuKhenThuong _DanhHieu;
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Danh hiệu")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DanhHieuKhenThuong DanhHieu
        {
            get
            {
                return _DanhHieu;
            }
            set
            {
                SetPropertyValue("DanhHieu", ref _DanhHieu, value);
            }
        }

        public Report_DeNghiKhenThuongTapThe(Session session) : base(session) { }

        //public override void FillDataSource()
        //{
        //    DataTable.Columns.Add("DonVi", typeof(string));
        //    DataTable.Columns.Add("Diem", typeof(string));
        //    DataTable.Columns.Add("LDTT", typeof(string));
        //    DataTable.Columns.Add("LDXS", typeof(string));
        //    DataTable.Columns.Add("ThanhTich", typeof(string));
        //    DataTable.Columns.Add("GhiChu", typeof(string));


        //    DanhSachDeNghiThiDuaKhenThuong listDeNghi = Session.FindObject<DanhSachDeNghiThiDuaKhenThuong>(
        //        CriteriaOperator.Parse("NamKhenThuong=? AND DanhHieu.TenDanhHieu=?", nam, "lao động tiên tiến"), false);

        //    List<TapTheForReport> listTapThe = new List<TapTheForReport>();

        //    if (listDeNghi != null && listDeNghi.DanhSachTapThe != null && listDeNghi.DanhSachTapThe.Count > 0)
        //    {
        //        foreach (ChiTietDanhSachTapTheDeNghiKhenThuong item in listDeNghi.DanhSachTapThe)
        //        {
        //            listTapThe.Add(new TapTheForReport() { DonVi = item.BoPhan.TenBoPhan, LDTT = "x", LDXS = "-", GhiChu = item.GhiChu });
        //        }
        //    }

        //    listDeNghi = Session.FindObject<DanhSachDeNghiThiDuaKhenThuong>(
        //        CriteriaOperator.Parse("NamKhenThuong=? AND DanhHieu.TenDanhHieu=?", nam, "lao động xuất sắc"), false);

        //    int index = -1;
        //    if (listDeNghi != null && listDeNghi.DanhSachTapThe != null && listDeNghi.DanhSachTapThe.Count > 0)
        //    {
        //        foreach (ChiTietDanhSachTapTheDeNghiKhenThuong item in listDeNghi.DanhSachTapThe)
        //        {
        //            index = Find(listTapThe, item.BoPhan.TenBoPhan);

        //            if (index < 0)
        //                listTapThe.Add(new TapTheForReport() { DonVi = item.BoPhan.TenBoPhan, LDTT = "-", LDXS = "x", GhiChu = item.GhiChu });
        //            else
        //                listTapThe[index].LDXS = "x";
        //        }
        //    }
        //    XPCollection<ChamDiemThiDua> chamDiem = new XPCollection<ChamDiemThiDua>(Session,
        //        CriteriaOperator.Parse("Nam=?", nam));

        //    if (chamDiem != null && chamDiem.Count > 0)
        //    {
        //        foreach (ChamDiemThiDua item in chamDiem)
        //        {
        //            index = Find(listTapThe, item.BoPhan.TenBoPhan);
        //            if (index >= 0)
        //                listTapThe[index].Diem = item.Diem.ToString();
        //        }
        //    }

        //    foreach (TapTheForReport item in listTapThe)
        //    {
        //        DataTable.Rows.Add(item.DonVi, item.Diem, item.LDTT, item.LDXS, item.ThanhTich, item.GhiChu);
        //    }

        //}

        //private int Find(List<TapTheForReport> list, string donVi)
        //{
        //    return list.FindIndex(delegate(TapTheForReport tt)
        //    {
        //        return tt.DonVi.Equals(donVi);
        //    });
        //}


        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_DanhSachTapTheDeNghiKhenThuong", (SqlConnection)Session.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
            cmd.Parameters.AddWithValue("@DanhHieu", DanhHieu.Oid);

            return cmd;
        }
    }
    //public class TapTheForReport
    //{
    //    public string DonVi { get; set; }
    //    public string Diem { get; set; }
    //    public string LDTT { get; set; }
    //    public string LDXS { get; set; }
    //    public string ThanhTich { get; set; }
    //    public string GhiChu { get; set; }
    //}
}
