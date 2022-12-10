using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.ThuNhap.ChungTu;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.ThuNhap.Thue;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;
using System.IO;
using System.Data.OleDb;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ChungTu_ExportExcelBangLuongUTEController : ViewController
    {
        SaveFileDialog _dlg = new SaveFileDialog();

        public ChungTu_ExportExcelBangLuongUTEController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ChungTu_ExportExcelBangLuongUTEController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("UTE"))
            {
                simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ChuyenKhoanLuongNhanVien>();
            }
            else
            {
                simpleAction1.Active["TruyCap"] = false;
            }
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ChuyenKhoanLuongNhanVien chuyenKhoan = View.CurrentObject as ChuyenKhoanLuongNhanVien;
            //
            if (chuyenKhoan != null)
            {
                if (DialogUtil.ShowYesNo("Bạn thật sự muốn xuất bảng lương [" + chuyenKhoan .KyTinhLuong.TenKy+ "]?") == DialogResult.Yes)
                {
                    try
                    {
                        using (DialogUtil.AutoWait())
                        {
                            ExportData(chuyenKhoan);
                        }
                        DialogUtil.ShowInfo("Đã xuất bảng lương [" + chuyenKhoan.KyTinhLuong.TenKy + "] thành công.");
                        System.Diagnostics.Process.Start(_dlg.FileName);
                    }
                    catch (Exception ex)
                    {
                        DialogUtil.ShowError("Không xuất dữ liệu được: " + ex.Message);
                    }
                }
            }
        }

        private void ExportData(ChuyenKhoanLuongNhanVien chungTu)
        {
                _dlg.AddExtension = true;
                _dlg.Filter = "Excel|*.xls|All file|*.*";
                if (_dlg.ShowDialog() == DialogResult.OK)
                {

                    //tạo file template
                    FileStream fs = new FileStream(_dlg.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Write(Properties.Resources.ExportBangLuongFull, 0, Properties.Resources.ExportBangLuongFull.Length);
                    fs.Close();

                    //tạo dữ liệu tạm vào table để xử lý
                    System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
                    cm.Connection = DataProvider.GetConnection();
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.CommandText = "spd_TaiChinh_ExportBangTongHopLuongToExcel";
                    cm.Parameters.AddWithValue("@KyTinhLuong", chungTu.KyTinhLuong.Oid);

                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
                    da.SelectCommand = cm;
                    DataTable tbl = new DataTable("Export");
                    da.Fill(tbl);

                    //ghi dữ liệu vào file

                    string cnnExcel = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=No;\"", _dlg.FileName);
                    OleDbDataAdapter daExcel = new OleDbDataAdapter("Select * From [BangLuongPSC$A1:AX]", cnnExcel);
                    DataTable tblExcel = new DataTable("Export");
                    daExcel.Fill(tblExcel);
                    tblExcel.Clear();
                    string sql = "Insert Into [BangLuongPSC$A1:AX] (F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23,F24,F25,F26,F27,F28,F29,F30,F31,F32,F33,F34,F35,F36,F37,F38,F39,F40,F41,F42,F43,F44,F45,F46,F47,F48,F49,F50) Values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                    daExcel.InsertCommand = new OleDbCommand(sql, daExcel.SelectCommand.Connection);
                    daExcel.InsertCommand.Parameters.Add("p1", OleDbType.WChar, 250, "F1"); // bomon
                    daExcel.InsertCommand.Parameters.Add("p2", OleDbType.WChar, 250, "F2"); // donvi
                    daExcel.InsertCommand.Parameters.Add("p3", OleDbType.WChar, 50, "F3");// ho
                    daExcel.InsertCommand.Parameters.Add("p4", OleDbType.WChar, 50, "F4");// ten
                    daExcel.InsertCommand.Parameters.Add("p5", OleDbType.WChar, 250, "F5"); // hoten
                    daExcel.InsertCommand.Parameters.Add("p6", OleDbType.WChar, 50, "F6");// mangach
                    daExcel.InsertCommand.Parameters.Add("p7", OleDbType.Decimal, 50, "F7");// hsl
                    daExcel.InsertCommand.Parameters.Add("p8", OleDbType.Decimal, 250, "F8"); // vuotkhung
                    daExcel.InsertCommand.Parameters.Add("p9", OleDbType.Decimal, 50, "F9");// tnien_hc
                    daExcel.InsertCommand.Parameters.Add("p10", OleDbType.Decimal, 50, "F10");// hstnien_hc
                    daExcel.InsertCommand.Parameters.Add("p11", OleDbType.WChar, 250, "F11"); // pctn_hc
                    daExcel.InsertCommand.Parameters.Add("p12", OleDbType.Decimal, 50, "F12");// tyle_tnien
                    daExcel.InsertCommand.Parameters.Add("p13", OleDbType.Decimal, 50, "F13");// hs_tnien
                    daExcel.InsertCommand.Parameters.Add("p14", OleDbType.Decimal, 250, "F14"); // pcdh
                    daExcel.InsertCommand.Parameters.Add("p15", OleDbType.Decimal, 50, "F15");// pccv
                    daExcel.InsertCommand.Parameters.Add("p16", OleDbType.Decimal, 50, "F16");// pcgd
                    daExcel.InsertCommand.Parameters.Add("p17", OleDbType.Decimal, 250, "F17"); // pckhac
                    daExcel.InsertCommand.Parameters.Add("p18", OleDbType.Decimal, 50, "F18");// hstangthem
                    daExcel.InsertCommand.Parameters.Add("p19", OleDbType.WChar, 50, "F19");// pcld
                    daExcel.InsertCommand.Parameters.Add("p20", OleDbType.Decimal, 250, "F20"); // hstn1
                    daExcel.InsertCommand.Parameters.Add("p21", OleDbType.Decimal, 50, "F21");// hstn2
                    daExcel.InsertCommand.Parameters.Add("p22", OleDbType.Decimal, 50, "F22");// hstn3
                    daExcel.InsertCommand.Parameters.Add("p23", OleDbType.Decimal, 250, "F23"); // hstn4
                    daExcel.InsertCommand.Parameters.Add("p24", OleDbType.Decimal, 50, "F24");// hstn5
                    daExcel.InsertCommand.Parameters.Add("p26", OleDbType.Decimal, 50, "F25");// tonghstn
                    daExcel.InsertCommand.Parameters.Add("p27", OleDbType.WChar, 250, "F26"); // pctn
                    daExcel.InsertCommand.Parameters.Add("p28", OleDbType.WChar, 50, "F27");// bhxh
                    daExcel.InsertCommand.Parameters.Add("p29", OleDbType.WChar, 50, "F28");// bhyt
                    daExcel.InsertCommand.Parameters.Add("p30", OleDbType.WChar, 250, "F29"); // bhtn
                    daExcel.InsertCommand.Parameters.Add("p31", OleDbType.WChar, 50, "F30");// trukhac
                    daExcel.InsertCommand.Parameters.Add("p32", OleDbType.WChar, 50, "F31");// khautru
                    daExcel.InsertCommand.Parameters.Add("p33", OleDbType.WChar, 250, "F32"); // ghichu
                    daExcel.InsertCommand.Parameters.Add("p34", OleDbType.WChar, 50, "F33");// luongnn
                    daExcel.InsertCommand.Parameters.Add("p35", OleDbType.WChar, 50, "F34");// tylett
                    daExcel.InsertCommand.Parameters.Add("p36", OleDbType.WChar, 250, "F35"); // luongtt
                    daExcel.InsertCommand.Parameters.Add("p37", OleDbType.WChar, 50, "F36");// thuclanh
                    daExcel.InsertCommand.Parameters.Add("p38", OleDbType.WChar, 50, "F37");// sotk
                    daExcel.InsertCommand.Parameters.Add("p39", OleDbType.WChar, 250, "F38"); // madv
                    daExcel.InsertCommand.Parameters.Add("p40", OleDbType.WChar, 50, "F39");// thue_tn
                    daExcel.InsertCommand.Parameters.Add("p41", OleDbType.WChar, 50, "F40");// tyle_vk
                    daExcel.InsertCommand.Parameters.Add("p42", OleDbType.WChar, 250, "F41"); // tru_hoc_nn
                    daExcel.InsertCommand.Parameters.Add("p43", OleDbType.WChar, 50, "F42");// so_sobhxh
                    daExcel.InsertCommand.Parameters.Add("p44", OleDbType.WChar, 50, "F43");// giacanh
                    daExcel.InsertCommand.Parameters.Add("p45", OleDbType.WChar, 250, "F44"); // tien_gc
                    daExcel.InsertCommand.Parameters.Add("p46", OleDbType.WChar, 250, "F45"); // maquanly
                    daExcel.InsertCommand.Parameters.Add("p47", OleDbType.WChar, 250, "F46"); // tinhtrang
                    daExcel.InsertCommand.Parameters.Add("p48", OleDbType.Decimal, 250, "F47"); // tongthunhap
                    daExcel.InsertCommand.Parameters.Add("p49", OleDbType.WChar, 250, "F48"); // sohieucongchuc
                    daExcel.InsertCommand.Parameters.Add("p50", OleDbType.Decimal, 250, "F49"); // sohieucongchuc
                    daExcel.InsertCommand.Parameters.Add("p25", OleDbType.Decimal, 50, "F50");// hstn6

                    foreach (DataRow r in tbl.Rows)
                    {
                      tblExcel.Rows.Add(r["TenBoMon"],r["TenBoPhan"], r["Ho"], r["Ten"], r["HoTen"], r["MaNgach"], r["HeSoLuong"], r["HSPCVuotKhung"], r["PhanTramThamNienHC"], r["HSPCThamNienHC"], r["PhuCapThamNienHanhChinh"],
                                        r["ThamNien"], r["HSPCThamNien"], r["HSPCDocHai"], r["HSPCChucVu"], r["HSPCUuDai"], r["HSPCKhac"], r["HSLTangThem"], r["PhuCapKhoiHanhChinh"], r["HSPCTrachNhiem1"], r["HSPCTrachNhiem2"],
                                        r["HSPCTrachNhiem3"], r["HSPCTrachNhiem4"], r["HSPCTrachNhiem5"], r["TongHSTrachNhiem"], r["PhuCapTrachNhiem"], r["BHXH"], r["BHYT"], r["BHTN"], r["TruKhac"], r["KhauTru"],
                                        r["GhiChu"], r["LuongNhaNuoc"], r["TiLeTangThem"], r["LuongTangThem"], r["ThucLanh"], r["SoTaiKhoan"], r["MaDonVi"], r["ThueTNCN"], r["VuotKhung"], r["HocNuocNgoai"],
                                        r["SoSoBHXH"], r["SoNguoiPhuThuoc"], r["GiamTruGiaCanh"], r["STT_KHTC"], r["TenTinhTrang"], r["TongThuNhap"], r["SoHieuCongChuc"], r["TongThuNhapTruBaoHiem"], r["HSPCTrachNhiem6"]);
                    }

                    daExcel.Update(tblExcel);
                }
             
        }
    }
}

