using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.Controllers
{
    public class HoSo_LuuTruHinhAnh
    {
        public static void XuLy(IObjectSpace obs)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "jpg files (*.jpg)|*.jpg";
                dialog.DefaultExt = "jpg files (*.jpg)|*.jpg";
                dialog.Multiselect = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    int sucessNumber = 0;
                    int erorrNumber = 0;
                    bool sucessImport = true;                 
                    StringBuilder mainLog = new StringBuilder();
                    StringBuilder detailLog;
                    Session ses = ((XPObjectSpace)obs).Session;
                    using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();

                        using (DialogUtil.AutoWait())
                        {
                            foreach (String fullFileName in dialog.FileNames)
                            {
                                //Khởi tạo bộ nhớ đệm
                                detailLog = new StringBuilder();
                                ThongTinNhanVien thongTinNhanVien = null;
                                string fileNamePart = string.Empty;                                                      
                               
                                if (!String.IsNullOrEmpty(fullFileName))
                                {
                                    string name = fullFileName.Substring(fullFileName.LastIndexOf(@"\") + 1);
                                    fileNamePart = name.Replace(".jpg", String.Empty).Replace(".JPG", String.Empty);
                                    thongTinNhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc = ?", fileNamePart));
                                   
                                    if (thongTinNhanVien != null)
                                    {
                                        try
                                        {
                                            Image img = Image.FromFile(fullFileName);
                                            //thongTinNhanVien.HinhAnh = img;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + " + ex.Message);
                                        }
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(" + Cán bộ có mã viên chức không tồn tại trong hệ thống: " + fileNamePart);
                                    }
                                }
                                else
                                {
                                    detailLog.AppendLine(" + Tên hình không hợp lệ!");
                                }

                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0)
                                {
                                    mainLog.AppendLine(string.Format("- Không upload hình [{0}] vào được: ", fullFileName));
                                    mainLog.AppendLine(detailLog.ToString());

                                    sucessImport = false;
                                }

                                ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////
                                if (sucessImport)
                                {
                                    //Lưu
                                    //giayToHoSo.Save();
                                    uow.CommitChanges();
                                    //
                                    sucessNumber++;
                                }
                                else
                                {
                                    erorrNumber++;
                                    //
                                    sucessImport = true;
                                }
                            }
                        }
                    }
                        string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                        DialogUtil.ShowInfo("Import Thành công " + sucessNumber + " hình - Số hình không thành công " + erorrNumber + " " + s + "!");

                        //Mở file log lỗi lên
                        if (erorrNumber > 0)
                        {
                            string tenFile = "Import_Log.txt";
                            StreamWriter writer = new StreamWriter(tenFile);
                            writer.WriteLine(mainLog.ToString());
                            writer.Flush();
                            writer.Close();
                            writer.Dispose();
                            HamDungChung.WriteLog(tenFile, mainLog.ToString());
                            System.Diagnostics.Process.Start(tenFile);
                        }                       

                    //}
                    
                }
            }
        }

        public static void XuLyThinhGiang(IObjectSpace obs, GiangVienThinhGiang giangVienThinhGiang)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Pdf files (*.pdf)|*.pdf";
                dialog.DefaultExt = "Pdf files (*.pdf)|*.pdf";
                dialog.Multiselect = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    int sucessNumber = 0;
                    int erorrNumber = 0;
                    bool sucessImport = true;
                    GiayToHoSo giayToHoSo;
                    StringBuilder mainLog = new StringBuilder();
                    StringBuilder detailLog;
                    Session ses = ((XPObjectSpace)obs).Session;
                    using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();

                        using (DialogUtil.AutoWait())
                        {
                            foreach (String fullFileName in dialog.FileNames)
                            {
                                //Khởi tạo bộ nhớ đệm
                                detailLog = new StringBuilder();
                                string giayToText = string.Empty;
                                string soGiayToText = string.Empty;
                                string dangLuuTruText = string.Empty;
                                LoaiGiayTo loaiGiayTo = null;
                                string tenLoaiGiayTo = string.Empty;
                                string fileName_Next = string.Empty;
                                DanhMuc.GiayTo giayTo = null;
                                DangLuuTru dangLuuTru = null;
                                //quyetdinhnangluong-01/QGNL-ANC

                                if (!String.IsNullOrEmpty(giangVienThinhGiang.MaQuanLy) && fullFileName.Contains(giangVienThinhGiang.MaQuanLy.Trim()))
                                {
                                    string name = fullFileName.Substring(fullFileName.LastIndexOf(@"\") + 1);

                                    string[] fileNamePart = name.Split('_');

                                    if (fileNamePart.Count() > 0)
                                    {
                                        giayToText = fileNamePart[0].Replace(".pdf", String.Empty).Replace(".PDF", String.Empty);

                                        if (fileNamePart.Count() > 1)
                                            soGiayToText = fileNamePart[1].Replace(".pdf", String.Empty).Replace(".PDF", String.Empty);

                                        if (fileNamePart.Count() > 2)
                                        {
                                            dangLuuTruText = fileNamePart[2].Replace(".pdf", String.Empty).Replace(".PDF", String.Empty);
                                            if (!String.IsNullOrEmpty(dangLuuTruText))
                                                dangLuuTru = uow.FindObject<DangLuuTru>(CriteriaOperator.Parse("MaQuanLy Like ?", dangLuuTruText));
                                        }

                                        if (!String.IsNullOrEmpty(giayToText))
                                        {
                                            giayTo = uow.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("MaQuanLy Like ?", giayToText));
                                            if (giayTo != null)
                                            {
                                                loaiGiayTo = uow.GetObjectByKey<LoaiGiayTo>(giayTo.LoaiGiayTo.Oid);
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Tên file ([Giấy tở]-[Số giấy tờ]) không tồn tại trong danh mục Giấy tờ: " + giayToText);
                                            }
                                        }

                                        if (loaiGiayTo != null)
                                        {
                                            try
                                            {
                                                tenLoaiGiayTo = HoSo_LuuTruGiayToHoSo.LayTenLoaiGiayTo(loaiGiayTo);
                                                //Lấy đường dẫn máy chủ theo tên loại giấy tờ
                                                string filePath = string.Format("{0}{1}/", HamDungChung.NoiLuuTruGiayTo, tenLoaiGiayTo);
                                                //Lấy tên file gốc
                                                fileName_Next = name.Replace(".pdf", String.Empty).Replace(".PDF", String.Empty);
                                                //Xử lý dấu tiếng việt
                                                fileName_Next = StringHelper.ReplaceVietnameseChar(StringHelper.ToTitleCase(fileName_Next)).Replace(" ", String.Empty);
                                                //Lấy thêm số thứ tự file và tên loại file để tránh bị trùng
                                                //fileName_Next = FptProvider.GetFileName_Next(tenLoaiGiayTo, loaiGiayTo.Oid, fileName_Next);

                                                if (!string.IsNullOrEmpty(fileName_Next))
                                                {
                                                    fileName_Next = string.Format("{0}-{1}.pdf", giangVienThinhGiang.MaQuanLy.Trim(), fileName_Next);

                                                    {//Tiến hành upload dữ liệu
                                                        FptProvider.UploadFileMultiData(filePath, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password, fullFileName, fileName_Next);
                                                    }

                                                    {//Nếu upload thành công
                                                        if (!String.IsNullOrEmpty(filePath) && !String.IsNullOrEmpty(fileName_Next) && !String.IsNullOrEmpty(tenLoaiGiayTo))
                                                        {
                                                            giayToHoSo = new GiayToHoSo(uow);
                                                            giayToHoSo.HoSo = uow.GetObjectByKey<HoSo.HoSo>(giangVienThinhGiang.Oid);
                                                            giayToHoSo.GiayTo = giayTo;
                                                            giayToHoSo.SoGiayTo = soGiayToText;
                                                            giayToHoSo.DangLuuTru = dangLuuTru;
                                                            giayToHoSo.DuongDanFile = string.Format("{0}{1}", filePath, fileName_Next);
                                                            giayToHoSo.LuuTru = string.Format("{0}/{1}", tenLoaiGiayTo, fileName_Next);
                                                            giayToHoSo.NgayLap = HamDungChung.GetServerTime();
                                                            giayToHoSo.SoBan = 1;
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine(String.Concat(filePath, fileName_Next, tenLoaiGiayTo));
                                                        }
                                                        //giayToHoSo.DangLuuTru =                                                 
                                                        //giayToHoSo.TrichYeu =
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Tên file hợp lệ: " + fileName_Next);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + " + ex.Message);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Loại Giấy tờ không hợp lệ: " + giayToText);
                                        }
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(" + Tên file không hợp lệ " + fileNamePart);
                                    }
                                }
                                else
                                {
                                    detailLog.AppendLine(" + Mã quản lý của thỉnh giảng rỗng hoặc không khớp với thư mục");
                                }

                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0)
                                {
                                    mainLog.AppendLine(string.Format("- Không upload file [{0}] vào được: ", fullFileName));
                                    mainLog.AppendLine(detailLog.ToString());

                                    sucessImport = false;
                                }

                                ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////
                                if (sucessImport)
                                {
                                    //Lưu
                                    //giayToHoSo.Save();
                                    uow.CommitChanges();
                                    //
                                    sucessNumber++;
                                }
                                else
                                {
                                    erorrNumber++;
                                    //
                                    sucessImport = true;
                                }
                            }
                        }
                    }
                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                    DialogUtil.ShowInfo("Import Thành công " + sucessNumber + " file - Số file không thành công " + erorrNumber + " " + s + "!");

                    //Mở file log lỗi lên
                    if (erorrNumber > 0)
                    {
                        string tenFile = "Import_Log.txt";
                        StreamWriter writer = new StreamWriter(tenFile);
                        writer.WriteLine(mainLog.ToString());
                        writer.Flush();
                        writer.Close();
                        writer.Dispose();
                        HamDungChung.WriteLog(tenFile, mainLog.ToString());
                        System.Diagnostics.Process.Start(tenFile);
                    }

                    //}

                }
            }
        }

        public static string LayTenLoaiGiayTo(LoaiGiayTo loaiGiayTo)
        {
            string tenLoaiGiayTo = string.Empty;

            if (loaiGiayTo.TenLoaiGiayTo.Contains("Hồ sơ"))
            {
                tenLoaiGiayTo = "HoSo";
            }
            else if (loaiGiayTo.TenLoaiGiayTo.Contains("Quyết định"))
            {
                tenLoaiGiayTo = "QuyetDinh";
            }
            else if (loaiGiayTo.TenLoaiGiayTo.Contains("Văn bằng, chứng chỉ"))
            {
                tenLoaiGiayTo = "VanBangChungChi";
            }
            else if (loaiGiayTo.TenLoaiGiayTo.Contains("Giấy tờ tùy thân"))
            {
                tenLoaiGiayTo = "GiayToTuyThan";
            }
            else if (loaiGiayTo.TenLoaiGiayTo.Contains("Công nhận chức danh giáo sư, phó giáo sư"))
            {
                tenLoaiGiayTo = "CongNhanChucDanh";
            }
            else if (loaiGiayTo.TenLoaiGiayTo.Contains("Hợp đồng"))
            {
                tenLoaiGiayTo = "HopDong";
            }
            else
            {
                tenLoaiGiayTo = "GiayToKhac";
            }

            return tenLoaiGiayTo;
        }
    }
}
