using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using System.Data;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [Custom("Caption", "Bảng Chấm Công Theo Đơn Vị")]
    [EditorStateRule("Report_BangChamCongTheoDonVi.TatCaDonVi", "BoPhan", EditorState.Disabled, "TatCaDonVi", DevExpress.ExpressApp.ViewType.DetailView)]
    public class Report_BangChamCongTheoDonVi : StoreProcedureReport
    {
        private KyTinhLuong _KyTinhLuong;
        private BoPhan _DonVi;
        private bool _TatCaDonVi;

        [Custom("Caption", "Kỳ Tính Lương")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn Kỳ Tính Lương")]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [Custom("Caption", "Tất cả đơn vị")]
        [ImmediatePostData]
        public bool TatCaDonVi
        {
            get { return _TatCaDonVi; }
            set { SetPropertyValue("TatCaDonVi", ref _TatCaDonVi, value); }
        }

        [Custom("Caption", "Đơn Vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _DonVi;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _DonVi, value);
            }
        }

        public Report_BangChamCongTheoDonVi(Session session) :
            base(session)
        { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }


        private List<DateTime> NgayLeList = null;
        public override void FillDataSource()
        {
            DataTable.Columns.Add("HoTen", typeof(string));
            DataTable.Columns.Add("DonVi", typeof(string));
            DataTable.Columns.Add("Ngay1", typeof(string));
            DataTable.Columns.Add("Ngay2", typeof(string));
            DataTable.Columns.Add("Ngay3", typeof(string));
            DataTable.Columns.Add("Ngay4", typeof(string));
            DataTable.Columns.Add("Ngay5", typeof(string));
            DataTable.Columns.Add("Ngay6", typeof(string));
            DataTable.Columns.Add("Ngay7", typeof(string));
            DataTable.Columns.Add("Ngay8", typeof(string));
            DataTable.Columns.Add("Ngay9", typeof(string));
            DataTable.Columns.Add("Ngay10", typeof(string));
            DataTable.Columns.Add("Ngay11", typeof(string));
            DataTable.Columns.Add("Ngay12", typeof(string));
            DataTable.Columns.Add("Ngay13", typeof(string));
            DataTable.Columns.Add("Ngay14", typeof(string));
            DataTable.Columns.Add("Ngay15", typeof(string));
            DataTable.Columns.Add("Ngay16", typeof(string));
            DataTable.Columns.Add("Ngay17", typeof(string));
            DataTable.Columns.Add("Ngay18", typeof(string));
            DataTable.Columns.Add("Ngay19", typeof(string));
            DataTable.Columns.Add("Ngay20", typeof(string));
            DataTable.Columns.Add("Ngay21", typeof(string));
            DataTable.Columns.Add("Ngay22", typeof(string));
            DataTable.Columns.Add("Ngay23", typeof(string));
            DataTable.Columns.Add("Ngay24", typeof(string));
            DataTable.Columns.Add("Ngay25", typeof(string));
            DataTable.Columns.Add("Ngay26", typeof(string));
            DataTable.Columns.Add("Ngay27", typeof(string));
            DataTable.Columns.Add("Ngay28", typeof(string));
            DataTable.Columns.Add("Ngay29", typeof(string));
            DataTable.Columns.Add("Ngay30", typeof(string));
            DataTable.Columns.Add("Ngay31", typeof(string));
            DataTable.Columns.Add("SoNgayCong", typeof(string));
            DataTable.Columns.Add("NghiKhongLuong", typeof(string));
            DataTable.Columns.Add("CongHuongBHXH", typeof(string));


            if (KyTinhLuong != null && (TatCaDonVi || BoPhan != null))
            {
                if (NgayLeList == null)
                {
                    NgayLeList = new List<DateTime>();
                    using (XPCollection<DanhMuc.NgayNghiTrongNam> NgayLe = new XPCollection<DanhMuc.NgayNghiTrongNam>(Session))
                    {
                        foreach (var ngayle in NgayLe)
                        {
                            NgayLeList.Add(ngayle.NgayNghi);
                            if (ngayle.NgayNghiBu != DateTime.MinValue)
                                NgayLeList.Add(ngayle.NgayNghiBu);
                        }
                    }
                }

                using (XPCollection<ChiTietChamCongNgayNghi> NghiViec = new XPCollection<ChiTietChamCongNgayNghi>(Session))
                {
                    using (XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(Session))
                    {
                        GroupOperator DK = new GroupOperator();
                        GroupOperator ngay = new GroupOperator();
                        CriteriaOperator nv;
                        ngay.OperatorType = GroupOperatorType.Or;
                        ngay.Operands.Add(CriteriaOperator.Parse("TuNgay>=? AND TuNgay<=?", KyTinhLuong.TuNgay, KyTinhLuong.DenNgay));
                        ngay.Operands.Add(CriteriaOperator.Parse("DenNgay>=? AND DenNgay<=?", KyTinhLuong.TuNgay, KyTinhLuong.DenNgay));
                        ngay.Operands.Add(CriteriaOperator.Parse("TuNgay<=? AND TuNgay>=?", KyTinhLuong.TuNgay, KyTinhLuong.DenNgay));
                        CriteriaOperator filterNV;
                        if (!TatCaDonVi)
                        {
                            filterNV = CriteriaOperator.Parse("BoPhan=?", BoPhan.Oid);
                        }
                        else
                        {
                            filterNV = CriteriaOperator.Parse("");
                        }
                        NghiViec.Criteria = DK;
                        nvList.Criteria = filterNV;
                        DataRow dr;
                        int nghi = 0, bhxh = 0;
                        DateTime tungay, denngay;
                        foreach (ThongTinNhanVien item in nvList)
                        {
                            DK = new GroupOperator();
                            nv = CriteriaOperator.Parse("ThongTinNhanVien=?", item.Oid);
                            DK.Operands.Add(ngay);
                            DK.Operands.Add(nv);
                            //DK.Operands.Add(ktl);
                            NghiViec.Criteria = DK;
                            dr = DataTable.NewRow();
                            dr["HoTen"] = item.HoTen;
                            dr["DonVi"] = item.BoPhan.TenBoPhan;
                            dr[29] = "";
                            dr[30] = "";
                            dr[31] = "";
                            dr[32] = "";
                            for (DateTime i = KyTinhLuong.TuNgay; i <= KyTinhLuong.DenNgay; i = i.AddDays(1))
                            {
                                if (i.DayOfWeek == DayOfWeek.Sunday || i.DayOfWeek == DayOfWeek.Saturday || NgayLeList.Contains(i))
                                {
                                    dr[i.Day + 1] = "";
                                }
                                else
                                {
                                    dr[i.Day + 1] = "+";
                                }
                            }
                            nghi = 0;
                            bhxh = 0;
                            if (NghiViec.Count > 0)
                            {
                                foreach (ChiTietChamCongNgayNghi nn in NghiViec)
                                {
                                    if (nn.TuNgay < KyTinhLuong.TuNgay)
                                        tungay = KyTinhLuong.TuNgay;
                                    else
                                        tungay = nn.TuNgay;
                                    if (nn.DenNgay > KyTinhLuong.DenNgay)
                                        denngay = KyTinhLuong.DenNgay;
                                    else
                                        denngay = nn.DenNgay;
                                    //tính số ngày
                                    for (DateTime i = tungay; i <= denngay; i = i.AddDays(1))
                                    {
                                        if (nn.HinhThucNghi.Loai == HinhThucNghiEnum.NghiTruLuong)
                                        {
                                            if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday && !NgayLeList.Contains(i))
                                            {
                                                nghi++;
                                                dr[i.Day + 1] = nn.HinhThucNghi.MaQuanLy;
                                            }
                                        }
                                        else
                                            if (nn.HinhThucNghi.Loai == HinhThucNghiEnum.NghiHuongBaoHiem)
                                            {
                                                if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday && !NgayLeList.Contains(i))
                                                {
                                                    bhxh++;
                                                    dr[i.Day + 1] = nn.HinhThucNghi.MaQuanLy;
                                                }
                                            }
                                            else
                                                if (nn.HinhThucNghi.Loai == HinhThucNghiEnum.NghiPhep)
                                                {
                                                    if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday && !NgayLeList.Contains(i))
                                                    {
                                                        bhxh++;
                                                        dr[i.Day + 1] = nn.HinhThucNghi.MaQuanLy;
                                                    }
                                                }
                                    }
                                }
                            }
                            dr[34] = nghi.ToString();
                            dr[35] = bhxh.ToString();
                            dr[33] = (KyTinhLuong.ThongTinChung.SoNgayThang - nghi - bhxh).ToString();
                            DataTable.Rows.Add(dr);
                        }
                    }
                }
            }
        }



        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TatCaDonVi = true;
        }
    }

}
