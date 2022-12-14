using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using PSC_HRM.Module.ThuNhap.ThuLao;
using System.Windows.Forms;
using PSC_HRM.Module.PMS.NghiepVu.NCKH;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.PMS.BaoCao;
using PSC_HRM.Module.MailMerge.HopDong;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.MailMerge;
using DevExpress.XtraRichEdit.Model;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class BaoCao_HopDong_Megre_VHU_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        PMS_ThanhToanThuLaoGiangDay_HopDong_NonBaoCao _HoatDong;
        public BaoCao_HopDong_Megre_VHU_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "PMS_ThanhToanThuLaoGiangDay_HopDong_NonBaoCao_DetailView";
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string query = "";
            object kq;
            _HoatDong = View.CurrentObject as PMS_ThanhToanThuLaoGiangDay_HopDong_NonBaoCao;
            if (_HoatDong != null)
            {
                foreach (var chiTiet in _HoatDong.ListDanhSach)
                {
                    if (chiTiet.Chon)
                    {
                        query += " UNION ALL " + "SELECT '"
                                               + chiTiet.MaGV.Replace("'", "''") + "' as MaNhanVien,N'"
                                               + chiTiet.HoTen.Replace("'", "''") + "' as HoTen,N'"
                                               + chiTiet.ChucDanh.Replace("'", "''") + "' as ChucDanh,N'"
                                               + chiTiet.HocHam.Replace("'", "''") + "' as HocHam,N'"
                                               + chiTiet.HocVi.Replace("'", "''") + "' as HocVi,N'"
                                               + chiTiet.LoaiGV.Replace("'", "''") + "' as LoaiGV,N'"
                                               + chiTiet.LoaiHocPhan.Replace("'", "''") + "' as LoaiHocPhan,N'"
                                               + chiTiet.MaHocPhan.Replace("'", "''") + "' as MaHocPhan,N'"
                                               + chiTiet.TenHocPhan.Replace("'", "''") + "' as TenHocPhan,CAST("
                                               + chiTiet.SoTietLT.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietLT, CAST("
                                               + chiTiet.SoTietTH.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietTH, CAST("
                                               + chiTiet.SoTietKhac.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietKhac, CAST("
                                               + chiTiet.SiSo.ToString().Replace(",", ".") + " AS INT) as SiSo, CAST("
                                               + chiTiet.SoTietThucDay.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietThucDay, CAST("
                                               + chiTiet.TietQuyDoi.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as TietQuyDoi, CAST("
                                               + chiTiet.DonGia.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as DonGia, CAST("
                                               + chiTiet.ThanhTienChiTiet.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as ThanhTienChiTiet, N'"
                                               + chiTiet.TenBoPhan.Replace("'", "''") + "' as TenBoPhan, N'"
                                               + chiTiet.TenNamhoc.Replace("'", "''") + "' as TenNamHoc, N'"
                                               + chiTiet.TenHocKy.Replace("'", "''") + "' as TenHocKy, N'"
                                               + chiTiet.ThoiGiangDay.Replace("'", "''") + "' as ThoiGiangDay, N'"
                                               + chiTiet.DiaDiemDay.Replace("'", "''") + "' as DiaDiemDay";
                    }
                }

                if (query == "")
                {
                    MessageBox.Show("Vui lòng chọn dòng cần xác nhận!");
                }
                else
                {
                    DataTable dt = null;
                    DataTable dt1 = null;
                    SqlParameter[] parameter = new SqlParameter[2];
                    parameter[0] = new SqlParameter("@Sql", query); ;
                    parameter[1] = new SqlParameter("@User", HamDungChung.CurrentUser().Oid);
                    SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_YTe_SoPhatThuoc", System.Data.CommandType.StoredProcedure, parameter);
                    DataSet dataset = DataProvider.GetDataSet(cmd);
                    if (dataset != null)
                    {
                        dt = dataset.Tables[0];
                        dt1 = dataset.Tables[1];
                    }

                        var list = new List<PMS_MailMegre_HopDongThinhGiang_ThongTinChung>();
                        PMS_MailMegre_HopDongThinhGiang_ThongTinChung qd = null;
                    if (dt != null)
                    {
                        qd.HoTen = dt.Rows[0]["HoTen"].ToString();
                        qd.ChucDanh = dt.Rows[0]["HoTen"].ToString();
                        qd.HocHam = dt.Rows[0]["HoTen"].ToString();
                        qd.HocVi = dt.Rows[0]["HoTen"].ToString();
                        qd.LoaiGV = dt.Rows[0]["HoTen"].ToString();
                        qd.MaSoThue = dt.Rows[0]["HoTen"].ToString();
                        qd.SoTaiKhoan = dt.Rows[0]["HoTen"].ToString();
                        qd.TenNganHang = dt.Rows[0]["HoTen"].ToString();
                        qd.TenBoPhan = dt.Rows[0]["HoTen"].ToString();
                        qd.TenNamHoc = dt.Rows[0]["HoTen"].ToString();
                        qd.DienThoaiDiDong = dt.Rows[0]["HoTen"].ToString();
                        qd.Email = dt.Rows[0]["HoTen"].ToString();
                        qd.TenHocKy = dt.Rows[0]["HoTen"].ToString();
                        qd.TenDaiDien1 = dt.Rows[0]["HoTen"].ToString();
                        qd.NhanDaiDien1 = dt.Rows[0]["HoTen"].ToString();
                        qd.TenDaiDien2 = dt.Rows[0]["HoTen"].ToString();
                        qd.NhanDaiDien2 = dt.Rows[0]["HoTen"].ToString();
                        qd.FullDiaChi = dt.Rows[0]["HoTen"].ToString();
                        qd.MaSoThue_CongTy = dt.Rows[0]["HoTen"].ToString();
                        qd.DienThoai = dt.Rows[0]["HoTen"].ToString();
                        qd.Fax = dt.Rows[0]["HoTen"].ToString();

                    }
                    if(dt1 != null)
                    {
                        //detail
                        PMS_MailMegre_HopDongThinhGiang_ThongTinDetail detail;
                        int stt = 1;
                        foreach (DataRow itemRow in dt.Rows)
                        {
                            detail = new PMS_MailMegre_HopDongThinhGiang_ThongTinDetail();
                            detail.TenHocPhan = itemRow["MaNhanVien"].ToString();
                            detail.STT = stt.ToString();
                            detail.SoTietLT = Convert.ToDecimal(itemRow["SoTietLT"].ToString());
                            detail.SoTietTH = Convert.ToDecimal(itemRow["SoTietTH"].ToString());
                            detail.SoTietKhac = Convert.ToDecimal(itemRow["SoTietKhac"].ToString());
                            detail.SiSo = Convert.ToDecimal(itemRow["SiSo"].ToString());
                            detail.DonGia = Convert.ToDecimal(itemRow["DonGia"].ToString());
                            detail.DiaDiemDay = itemRow["DiaDiemDay"].ToString();
                            qd.Detail.Add(detail);
                            stt++;
                        }
                    }

                    MailMergeTemplate[] merge = new MailMergeTemplate[3];
                    merge[1] = _obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "HopDongThinhGiang_VHUMaster.rtf")); ;
                    merge[2] = _obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "HopDongThinhGiang_VHUDetail.rtf")); ;

                    merge[0] = HamDungChung.GetTemplate(obs, "HopDongThinhGiang_VHU.rtf");
                    if (merge[0] != null && merge[1] != null && merge[2] != null)
                        MailMergeHelper.ShowEditor<PMS_MailMegre_HopDongThinhGiang_ThongTinChung>(list, _obs, merge);
                    else
                        HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng.");

                }
            }
        }
    }
}