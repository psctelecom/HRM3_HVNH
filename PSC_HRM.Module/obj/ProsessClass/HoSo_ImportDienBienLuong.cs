using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PSC_HRM.Module.QuaTrinh;
using System.IO;

namespace PSC_HRM.Module.Controllers
{
    class HoSo_ImportDienBienLuong
    {
        public static void XuLy(IObjectSpace obs, string fileName)
        {
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A2:P]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder errorLog;

                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();
                    DienBienLuong dienBienLuong = null;
                    ThongTinNhanVien thongtinnhanvien = null;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int sTTIndex = 0;
                        int maQuanLyIndex = 1;
                        int boPhanIndex = 2;
                        int hoTenIndex = 3;
                        int soQuyetDinhIndex = 4;
                        int tuNgayIndex = 5;
                        int denNgayIndex = 6;
                        int huong85LuongIndex = 7;
                        int maNgachIndex = 8;
                        int bacLuongIndex = 9;
                        int heSoLuongIndex = 10;
                        int phanTramVuotKhungIndex = 11;
                        int hSPCChucVuIndex = 12;
                        int phanTramThamNienNhaGiaoIndex = 13;
                        int phanTramPhuCapUuDaiIndex = 14;
                        int lyDoIndex = 15;

                        using (DialogUtil.AutoWait())
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                String sTT = dr[sTTIndex].ToString().Trim();
                                String maQuanLy = dr[maQuanLyIndex].ToString().Trim();
                                String boPhan = dr[boPhanIndex].ToString().Trim();
                                String hoTen = dr[hoTenIndex].ToString().Trim();
                                String soQuyetDinh = dr[soQuyetDinhIndex].ToString().Trim();
                                String tuNgay = dr[tuNgayIndex].ToString().Trim();
                                String denNgay = dr[denNgayIndex].ToString().Trim();
                                String huong85Luong = dr[huong85LuongIndex].ToString().Trim();
                                String maNgach = dr[maNgachIndex].ToString().Trim();
                                String bacLuong = dr[bacLuongIndex].ToString().Trim();
                                String heSoLuong = dr[heSoLuongIndex].ToString().Trim();
                                String phanTramVuotKhung = dr[phanTramVuotKhungIndex].ToString().Trim();
                                String hSPCChucVu = dr[hSPCChucVuIndex].ToString().Trim();
                                String phanTramThamNienNhaGiao = dr[phanTramThamNienNhaGiaoIndex].ToString().Trim();
                                String phanTramPhuCapUuDai = dr[phanTramPhuCapUuDaiIndex].ToString().Trim();
                                String lyDo = dr[lyDoIndex].ToString().Trim();

                                //Khởi tạo bộ nhớ đệm
                                errorLog = new StringBuilder();

                                #region Thông tin diễn biến lương
                                {
                                    //Tìm nhân viên theo mã quản lý      
                                    thongtinnhanvien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? and HoTen=?",
                                                                                        maQuanLy,
                                                                                        hoTen));
                                    
                                    if (thongtinnhanvien == null)
                                    {
                                        errorLog.AppendLine(string.Format(" + Sai thông tin nhân viên cần import!"));
                                    }
                                    else
                                    {
                                        #region Kiểm tra dữ liệu Import
                                        dienBienLuong = new DienBienLuong(uow);

                                        //Thông tin nhân viên.
                                        dienBienLuong.ThongTinNhanVien = thongtinnhanvien;

                                        //Số quyết định
                                        if (!string.IsNullOrEmpty(soQuyetDinh))
                                        {
                                            dienBienLuong.LyDo = "(Kèm theo quyết định số " + soQuyetDinh + ")";
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin số quyết định");
                                        }

                                        //Từ ngày
                                        if (!string.IsNullOrEmpty(tuNgay))
                                        {
                                            try
                                            {
                                                DateTime TuNgay = Convert.ToDateTime(tuNgay);
                                                if (TuNgay != null && TuNgay != DateTime.MinValue)
                                                    dienBienLuong.TuNgay = Convert.ToDateTime(tuNgay);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Từ ngày không hợp lệ: " + tuNgay);
                                            }
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin từ ngày");
                                        }


                                        //Đến ngày
                                        if (!string.IsNullOrEmpty(tuNgay))
                                        {
                                            try
                                            {
                                                DateTime DenNgay = Convert.ToDateTime(denNgay);
                                                if (DenNgay != null && DenNgay != DateTime.MinValue)
                                                    dienBienLuong.DenNgay = Convert.ToDateTime(denNgay);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Đến ngày không hợp lệ: " + denNgay);
                                            }
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin đến ngày");
                                        }

                                        //Hưởng 85% lương
                                        if (!string.IsNullOrEmpty(huong85Luong))
                                        {
                                            if (huong85Luong.ToLower() == "true")
                                                dienBienLuong.Huong85PhanTramLuong = true;
                                            else
                                                dienBienLuong.Huong85PhanTramLuong = false;
                                        }

                                        //Mã ngạch
                                        if (!string.IsNullOrEmpty(maNgach))
                                        {
                                            NgachLuong NgachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy=?", maNgach));
                                            if (NgachLuong != null)
                                                dienBienLuong.NgachLuong = NgachLuong;
                                            else
                                                errorLog.AppendLine(" + Ngạch lương không hợp lệ: " + maNgach);
                                        }

                                        //Bậc lương
                                        if (!string.IsNullOrEmpty(bacLuong))
                                        {
                                            BacLuong BacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("MaQuanLy=?", bacLuong));
                                            if (BacLuong != null)
                                                dienBienLuong.BacLuong = BacLuong;
                                            else
                                                errorLog.AppendLine(" + Bậc lương không hợp lệ: " + bacLuong);
                                        }

                                        //Hệ số lương
                                        if (!string.IsNullOrEmpty(heSoLuong))
                                        {
                                            try
                                            {
                                                dienBienLuong.HeSoLuong = Convert.ToDecimal(heSoLuong);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Hệ số lương không hợp lệ: " + heSoLuong);
                                            }

                                        }

                                        //Vượt khung
                                        if (!string.IsNullOrEmpty(phanTramVuotKhung))
                                        {
                                            try
                                            {
                                                dienBienLuong.VuotKhung = Convert.ToInt32(phanTramVuotKhung);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Phần trăm vượt khung không hợp lệ: " + phanTramVuotKhung);
                                            }
                                        }

                                        //HSPC Chức vự
                                        if (!string.IsNullOrEmpty(hSPCChucVu))
                                        {
                                            try
                                            {
                                                dienBienLuong.HSPCChucVu = Convert.ToDecimal(hSPCChucVu);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Hệ số PCCV không hợp lệ: " + hSPCChucVu);
                                            }

                                        }

                                        //Phần trăm thâm niên nhà giáo
                                        if (!string.IsNullOrEmpty(phanTramThamNienNhaGiao))
                                        {
                                            try
                                            {
                                                dienBienLuong.ThamNien = Convert.ToDecimal(phanTramThamNienNhaGiao); ;
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Phần trăm thâm niên nhà giáo không hợp lệ: " + phanTramThamNienNhaGiao);
                                            }
                                        }

                                        //Phần trăm phụ cấp ưu đãi
                                        if (!string.IsNullOrEmpty(phanTramPhuCapUuDai))
                                        {
                                            try
                                            {
                                                dienBienLuong.PhuCapUuDai = Convert.ToInt32(phanTramPhuCapUuDai);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Phần trăm phụ cấp ưu đãi không hợp lệ: " + phanTramPhuCapUuDai);
                                            }
                                        }

                                        //Lý do
                                        if (!string.IsNullOrEmpty(lyDo))
                                        {
                                            dienBienLuong.LyDo = lyDo + " " + dienBienLuong.LyDo;
                                        }
                                        #endregion
                                    }

                                    #region Ghi File log
                                    {
                                        //Đưa thông tin bị lỗi vào blog
                                        if (errorLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- STT: {0} - {1} không import vào phần mềm được: ", sTT, hoTen));
                                            mainLog.AppendLine(errorLog.ToString());
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }
                        if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                            {
                                string tenFile = "Import_Log.txt";
                                //FileStream fileStream = File.Open(tenFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                                StreamWriter writer = new StreamWriter(tenFile);
                                writer.WriteLine(mainLog.ToString());
                                writer.Flush();
                                writer.Close();
                                writer.Dispose();
                                HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                System.Diagnostics.Process.Start(tenFile);
                            }
                        }
                        else
                        {
                            //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                            uow.CommitChanges();

                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                            obs.Refresh();
                        }
                    }
                }
            }
        }

        public static void XuLy_BUH(IObjectSpace obs, string fileName)
        {
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A2:R]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder errorLog;

                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();
                    DienBienLuong dienBienLuong = null;
                    ThongTinNhanVien thongtinnhanvien = null;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int sTTIndex = 0;
                        int maQuanLyIndex = 1;
                        int boPhanIndex = 2;
                        int hoTenIndex = 3;
                        int soQuyetDinhIndex = 4;
                        int ngayQuyetDinhIndex = 5;
                        int tuNgayIndex = 6;
                        int denNgayIndex = 7;
                        int lyDoIndex = 8;
                        int chucVuIndex = 9;
                        int huong85LuongIndex = 10;
                        int maNgachIndex = 11;
                        int bacLuongIndex = 12;
                        int heSoLuongIndex = 13;
                        int phanTramVuotKhungIndex = 14;
                        int hSPCChucVuIndex = 15;
                        int phanTramThamNienNhaGiaoIndex = 16;
                        int phanTramPhuCapUuDaiIndex = 17;
                        

                        using (DialogUtil.AutoWait())
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                String sTT = dr[sTTIndex].ToString().Trim();
                                String maQuanLy = dr[maQuanLyIndex].ToString().Trim();
                                String boPhan = dr[boPhanIndex].ToString().Trim();
                                String hoTen = dr[hoTenIndex].ToString().Trim();
                                String soQuyetDinh = dr[soQuyetDinhIndex].ToString().Trim();
                                String ngayQuyetDinh = dr[ngayQuyetDinhIndex].ToString().Trim();
                                String tuNgay = dr[tuNgayIndex].ToString().Trim();
                                String denNgay = dr[denNgayIndex].ToString().Trim();
                                String lyDo = dr[lyDoIndex].ToString().Trim();
                                String chucVu = dr[chucVuIndex].ToString().Trim();
                                String huong85Luong = dr[huong85LuongIndex].ToString().Trim();
                                String maNgach = dr[maNgachIndex].ToString().Trim();
                                String bacLuong = dr[bacLuongIndex].ToString().Trim();
                                String heSoLuong = dr[heSoLuongIndex].ToString().Trim();
                                String phanTramVuotKhung = dr[phanTramVuotKhungIndex].ToString().Trim();
                                String hSPCChucVu = dr[hSPCChucVuIndex].ToString().Trim();
                                String phanTramThamNienNhaGiao = dr[phanTramThamNienNhaGiaoIndex].ToString().Trim();
                                String phanTramPhuCapUuDai = dr[phanTramPhuCapUuDaiIndex].ToString().Trim();
                                
                                //Khởi tạo bộ nhớ đệm
                                errorLog = new StringBuilder();

                                #region Thông tin diễn biến lương
                                {
                                    //Tìm nhân viên theo mã quản lý      

                                    thongtinnhanvien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", maQuanLy));
                                    
                                    if (thongtinnhanvien == null)
                                    {
                                        errorLog.AppendLine(string.Format(" + Sai thông tin nhân viên cần import!"));
                                    }
                                    else
                                    {
                                        #region Kiểm tra dữ liệu Import
                                        dienBienLuong = new DienBienLuong(uow);

                                        //Thông tin nhân viên.
                                        dienBienLuong.ThongTinNhanVien = thongtinnhanvien;

                                        //Số quyết định
                                        if (!string.IsNullOrEmpty(soQuyetDinh))
                                        {
                                            dienBienLuong.SoQuyetDinh = soQuyetDinh;
                                        }
                                        //else
                                        //{
                                        //    errorLog.AppendLine(" + Thiếu thông tin số quyết định");
                                        //}

                                        //Ngày quyết định
                                        if (!string.IsNullOrEmpty(ngayQuyetDinh))
                                        {
                                            try
                                            {
                                                DateTime NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinh);
                                                if (NgayQuyetDinh != null && NgayQuyetDinh != DateTime.MinValue)
                                                    dienBienLuong.NgayQuyetDinh = NgayQuyetDinh;
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinh);
                                            }
                                        }
                                        //else
                                        //{
                                        //    errorLog.AppendLine(" + Thiếu thông tin từ ngày");
                                        //}

                                        //Từ ngày
                                        if (!string.IsNullOrEmpty(tuNgay))
                                        {
                                            try
                                            {
                                                DateTime TuNgay = Convert.ToDateTime(tuNgay);
                                                if (TuNgay != null && TuNgay != DateTime.MinValue)
                                                    dienBienLuong.TuNgay = Convert.ToDateTime(tuNgay);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Từ ngày không hợp lệ: " + tuNgay);
                                            }
                                        }
                                        //else
                                        //{
                                        //    errorLog.AppendLine(" + Thiếu thông tin từ ngày");
                                        //}


                                        //Đến ngày
                                        if (!string.IsNullOrEmpty(denNgay))
                                        {
                                            try
                                            {
                                                DateTime DenNgay = Convert.ToDateTime(denNgay);
                                                if (DenNgay != null && DenNgay != DateTime.MinValue)
                                                    dienBienLuong.DenNgay = Convert.ToDateTime(denNgay);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Đến ngày không hợp lệ: " + denNgay);
                                            }
                                        }
                                        //else
                                        //{
                                        //    errorLog.AppendLine(" + Thiếu thông tin đến ngày");
                                        //}

                                        //Lý do
                                        if (!string.IsNullOrEmpty(lyDo))
                                        {
                                            dienBienLuong.LyDo = lyDo + " " + dienBienLuong.LyDo;
                                        }
                                        #endregion

                                        //Chức vụ
                                        if (!string.IsNullOrEmpty(chucVu))
                                        {
                                            ChucVu ChucVu = uow.FindObject<ChucVu>(CriteriaOperator.Parse("MaQuanLy=?", chucVu));
                                            if (ChucVu != null)
                                                dienBienLuong.ChucVu = ChucVu;
                                            else
                                                errorLog.AppendLine(" + Ngạch lương không hợp lệ: " + maNgach);
                                        }
                                        
                                        //Hưởng 85% lương
                                        if (!string.IsNullOrEmpty(huong85Luong))
                                        {
                                            if (huong85Luong == "85")
                                                dienBienLuong.Huong85PhanTramLuong = true;
                                            else
                                                dienBienLuong.Huong85PhanTramLuong = false;
                                        }

                                        //Mã ngạch
                                        if (!string.IsNullOrEmpty(maNgach))
                                        {
                                            NgachLuong NgachLuong =  uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy=?", maNgach));
                                            if (NgachLuong != null)
                                                dienBienLuong.NgachLuong = NgachLuong;
                                            else
                                                errorLog.AppendLine(" + Ngạch lương không hợp lệ: " + maNgach);
                                        }

                                        //Bậc lương
                                        if (!string.IsNullOrEmpty(bacLuong))
                                        {
                                            BacLuong BacLuong =  uow.FindObject<BacLuong>(CriteriaOperator.Parse("TenBacLuong=?", bacLuong));
                                            if(BacLuong != null)
                                                dienBienLuong.BacLuong = BacLuong;
                                            else
                                                errorLog.AppendLine(" + Bậc lương không hợp lệ: " + bacLuong); 
                                        }

                                        //Hệ số lương
                                        if (!string.IsNullOrEmpty(heSoLuong))
                                        {
                                            try
                                            {
                                                dienBienLuong.HeSoLuong = Convert.ToDecimal(heSoLuong);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Hệ số lương không hợp lệ: " + heSoLuong);
                                            }

                                        }

                                        //Vượt khung
                                        if (!string.IsNullOrEmpty(phanTramVuotKhung))
                                        {
                                            try
                                            {
                                                dienBienLuong.VuotKhung = Convert.ToInt32(phanTramVuotKhung);
                                            }
                                            catch (Exception ex)
                                            {
                                                 errorLog.AppendLine(" + Phần trăm vượt khung không hợp lệ: " + phanTramVuotKhung); 
                                            }
                                        }

                                        //HSPC Chức vự
                                        if (!string.IsNullOrEmpty(hSPCChucVu))
                                        {
                                            try
                                            {
                                                dienBienLuong.HSPCChucVu = Convert.ToDecimal(hSPCChucVu);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Hệ số PCCV không hợp lệ: " + hSPCChucVu);
                                            }

                                        }

                                        //Phần trăm thâm niên nhà giáo
                                        if (!string.IsNullOrEmpty(phanTramThamNienNhaGiao))
                                        {
                                            try
                                            {
                                                dienBienLuong.ThamNien = Convert.ToDecimal(phanTramThamNienNhaGiao); ;
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Phần trăm thâm niên nhà giáo không hợp lệ: " + phanTramThamNienNhaGiao);
                                            }
                                        }

                                        //Phần trăm phụ cấp ưu đãi
                                        if (!string.IsNullOrEmpty(phanTramPhuCapUuDai))
                                        {
                                            try
                                            {
                                                dienBienLuong.PhuCapUuDai = Convert.ToInt32(phanTramPhuCapUuDai);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Phần trăm phụ cấp ưu đãi không hợp lệ: " + phanTramPhuCapUuDai);
                                            }
                                        }

                                        
                                    }

                                    #region Ghi File log
                                    {
                                        //Đưa thông tin bị lỗi vào blog
                                        if (errorLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- STT: {0} - {1} không import vào phần mềm được: ", sTT, hoTen));
                                            mainLog.AppendLine(errorLog.ToString());
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }
                        if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                            {
                                string tenFile = "Import_Log.txt";
                                //FileStream fileStream = File.Open(tenFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                                StreamWriter writer = new StreamWriter(tenFile);
                                writer.WriteLine(mainLog.ToString());
                                writer.Flush();
                                writer.Close();
                                writer.Dispose();
                                HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                System.Diagnostics.Process.Start(tenFile);
                            }
                        }
                        else
                        {
                            //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                            uow.CommitChanges();

                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                            obs.Refresh();
                        }
                    }
                }
            }
        }
    }
}
