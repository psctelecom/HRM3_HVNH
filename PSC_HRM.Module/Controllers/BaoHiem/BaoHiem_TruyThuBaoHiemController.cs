using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.BaoHiem;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System.Data;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BaoHiem_TruyThuBaoHiemController : ViewController
    {
        public BaoHiem_TruyThuBaoHiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TruyThuBaoHiemAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyTruyThuBaoHiem>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (WaitDialogForm wait = new WaitDialogForm("Đang tính truy thu bảo hiểm", "Vui lòng chờ..."))
            {
                View.ObjectSpace.CommitChanges();                

                QuanLyTruyThuBaoHiem truyThu = View.CurrentObject as QuanLyTruyThuBaoHiem;
                if (truyThu != null)
                {
                    //xóa dữ liệu cũ
                    DataProvider.ExecuteNonQuery("spd_BaoHiem_XoaTruyThuBaoHiem", CommandType.StoredProcedure, new SqlParameter("QuanLyTruyThuBaoHiem", truyThu.Oid));

                    //tính dữ liệu mới
                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@TuNgay", truyThu.TuNgay);
                    param[1] = new SqlParameter("@DenNgay", truyThu.DenNgay);
                    param[2] = new SqlParameter("@LaiSuatBHXH", SqlDbType.Decimal);
                    param[2].Precision = 10;
                    param[2].Scale = 8;
                    param[2].Value = truyThu.LaiSuatBHXH / 100;
                    param[3] = new SqlParameter("@LaiSuatBHYT", SqlDbType.Decimal);
                    param[3].Precision = 10;
                    param[3].Scale = 8;
                    param[3].Value = truyThu.LaiSuatBHYT / 100;
                    using (DataTable dt = DataProvider.GetDataTable("spd_BaoHiem_TruyThuBaoHiem", CommandType.StoredProcedure, param))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            TruyThuBaoHiem chiTiet;
                            decimal dTemp;
                            DateTime dtTemp;
                            foreach (DataRow row in dt.Rows)
                            {
                                chiTiet = View.ObjectSpace.CreateObject<TruyThuBaoHiem>();
                                chiTiet.QuanLyTruyThuBaoHiem = truyThu;
                                if (!row.IsNull("BoPhan"))
                                    chiTiet.BoPhan = View.ObjectSpace.GetObjectByKey<BoPhan>(new Guid(row["BoPhan"].ToString()));
                                if (!row.IsNull("NhanVien"))
                                    chiTiet.ThongTinNhanVien = View.ObjectSpace.GetObjectByKey<ThongTinNhanVien>(new Guid(row["NhanVien"].ToString()));
                                if (!row.IsNull("QuyetDinh"))
                                    chiTiet.QuyetDinh = View.ObjectSpace.GetObjectByKey<QuyetDinh.QuyetDinh>(new Guid(row["QuyetDinh"].ToString()));
                                if (!row.IsNull("ChenhLechTienLuong") && decimal.TryParse(row["ChenhLechTienLuong"].ToString(), out dTemp))
                                    chiTiet.ChenhLechTienLuong = dTemp;
                                if (!row.IsNull("TuNgay") && DateTime.TryParse(row["TuNgay"].ToString(), out dtTemp))
                                    chiTiet.TuThang = dtTemp;
                                if (!row.IsNull("DenNgay") && DateTime.TryParse(row["DenNgay"].ToString(), out dtTemp))
                                    chiTiet.DenThang = dtTemp;
                                if (!row.IsNull("SoThangTruyThu") && decimal.TryParse(row["SoThangTruyThu"].ToString(), out dTemp))
                                    chiTiet.SoThangTruyThu = (int)dTemp;
                                if (!row.IsNull("LuongCoBan") && decimal.TryParse(row["LuongCoBan"].ToString(), out dTemp))
                                    chiTiet.LuongToiThieu = dTemp;
                                if (!row.IsNull("BHXH") && decimal.TryParse(row["BHXH"].ToString(), out dTemp))
                                    chiTiet.PTBHXH = dTemp;
                                if (!row.IsNull("BHYT") && decimal.TryParse(row["BHYT"].ToString(), out dTemp))
                                    chiTiet.PTBHYT = dTemp;
                                if (!row.IsNull("BHTN") && decimal.TryParse(row["BHTN"].ToString(), out dTemp))
                                    chiTiet.PTBHTN = dTemp;
                                if (!row.IsNull("TongThoiGianChamDong") && decimal.TryParse(row["TongThoiGianChamDong"].ToString(), out dTemp))
                                    chiTiet.TongThoiGianChamDong = (int)dTemp;
                                if (!row.IsNull("LaiBHXH") && decimal.TryParse(row["LaiBHXH"].ToString(), out dTemp))
                                    chiTiet.LaiBHXH = dTemp;
                                if (!row.IsNull("LaiBHYT") && decimal.TryParse(row["LaiBHYT"].ToString(), out dTemp))
                                    chiTiet.LaiBHYT = dTemp;
                                if (!row.IsNull("LaiBHTN") && decimal.TryParse(row["LaiBHTN"].ToString(), out dTemp))
                                    chiTiet.LaiBHTN = dTemp;
                            }
                        }
                    }
                    View.ObjectSpace.CommitChanges();
                    View.ObjectSpace.Refresh();
                    XtraMessageBox.Show("Truy thu bảo hiểm thành công.", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
        }
    }
}
