using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.HoSo;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.ChamCong;
using System.Text;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module;
using System.Collections.Generic;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.CauHinh;
using System.IO;

namespace PSC_HRM.Module.Controllers
{
    public class BaoHiem_ImportTKD01_TNN
    {
        public static bool XuLy(IObjectSpace obs, object obj)
        {
            bool oke = false;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                    {
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;

                        using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();

                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    TKD01_TNN TKD01_TNN = obj as TKD01_TNN;

                                    //
                                    int IDLaoDong = 0;
                                    int hoTen = 1;
                                    int maSoBHXH = 2;
                                    int tenLoaiVanBan = 3;
                                    int soVanBan = 4;
                                    int ngayBanHanh = 5;
                                    int ngayHieuLuc = 6;
                                    int coQuanBanHanh = 7;
                                    int trichYeu = 8;
                                    int noiDungThamDinh = 9;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        String IDLaoDongText = item[IDLaoDong].ToString().FullTrim();
                                        String hoTenText = item[hoTen].ToString().FullTrim();
                                        String maSoBHXHText = item[maSoBHXH].ToString().FullTrim();
                                        String tenLoaiVanBanText = item[tenLoaiVanBan].ToString().FullTrim();
                                        String soVanBanText = item[soVanBan].ToString().FullTrim();
                                        String ngayBanHanhText = item[ngayBanHanh].ToString().FullTrim();
                                        String ngayHieuLucText = item[ngayHieuLuc].ToString().FullTrim();
                                        String coQuanBanHanhText = item[coQuanBanHanh].ToString().FullTrim();
                                        String trichYeuText = item[trichYeu].ToString().FullTrim();
                                        String noiDungThamDinhText = item[noiDungThamDinh].ToString().FullTrim();


                                        ChiTietTKD01_TNN ChiTietTKD01_TNN = uow.FindObject<ChiTietTKD01_TNN>(CriteriaOperator.Parse("TKD01_TNN=?", TKD01_TNN.Oid));
                                        if (ChiTietTKD01_TNN == null)
                                        {
                                            ChiTietTKD01_TNN = new ChiTietTKD01_TNN(uow);
                                            ChiTietTKD01_TNN.TKD01_TNN = uow.GetObjectByKey<TKD01_TNN>(TKD01_TNN.Oid);
                                        }

                                        #region ID lao động

                                        ChiTietTKD01_TNN.IDLaoDong = IDLaoDongText;
                                        #endregion

                                        #region Họ tên
                                        if (!string.IsNullOrEmpty(hoTenText))
                                        {
                                            try
                                            {
                                                ChiTietTKD01_TNN.HoTen = hoTenText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Họ tên không hợp lệ: " + hoTenText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Họ tên chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Số BHXH
                                        if (!string.IsNullOrEmpty(maSoBHXHText))
                                        {
                                            try
                                            {
                                                ChiTietTKD01_TNN.MaSoBHXH = maSoBHXHText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Số BHXH không hợp lệ: " + maSoBHXHText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Số BHXH chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Tên văn bản
                                        if (!string.IsNullOrEmpty(tenLoaiVanBanText))
                                        {
                                            try
                                            {
                                                ChiTietTKD01_TNN.TenLoaiVanBan = tenLoaiVanBanText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Tên văn bản không hợp lệ: " + tenLoaiVanBanText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Tên văn bản chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Số văn bản
                                        if (!string.IsNullOrEmpty(soVanBanText))
                                        {
                                            try
                                            {
                                                ChiTietTKD01_TNN.SoVanBan = soVanBanText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Số văn bản không hợp lệ: " + soVanBanText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Số văn bản chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Ngày ban hành
                                        if (!string.IsNullOrEmpty(ngayBanHanhText))
                                        {
                                            try
                                            {
                                                ChiTietTKD01_TNN.NgayBanHanh = ngayBanHanhText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày ban hành không hợp lệ: " + ngayBanHanhText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Ngày ban hành chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Ngày hiệu lực
                                        if (!string.IsNullOrEmpty(ngayHieuLucText))
                                        {
                                            try
                                            {
                                                ChiTietTKD01_TNN.NgayHieuLuc = ngayHieuLucText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày hiệu lực không hợp lệ: " + ngayHieuLucText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Ngày hiệu lực chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Cơ quan ban hành
                                        if (!string.IsNullOrEmpty(coQuanBanHanhText))
                                        {
                                            try
                                            {
                                                ChiTietTKD01_TNN.CoQuanBanHanh = coQuanBanHanhText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Cơ quan ban hành không hợp lệ: " + coQuanBanHanhText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Cơ quan ban hành chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Trích yếu văn bản
                                        if (!string.IsNullOrEmpty(trichYeuText))
                                        {
                                            try
                                            {
                                                ChiTietTKD01_TNN.TrichYeu = trichYeuText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Trích yếu văn bản không hợp lệ: " + trichYeuText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Trích yếu văn bản chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Trích lược nội dung thẩm định
                                        if (!string.IsNullOrEmpty(noiDungThamDinhText))
                                        {
                                            try
                                            {
                                                ChiTietTKD01_TNN.NoiDungThamDinh = noiDungThamDinhText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Trích lược nội dung thẩm định không hợp lệ: " + noiDungThamDinhText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Trích lược nội dung thẩm định chưa có dữ liệu");
                                        }
                                        #endregion

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", hoTen));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }

                                    }
                                }
                            }

                            //
                            if (mainLog.Length > 0)
                            {
                                uow.RollbackTransaction();
                                if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
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

                                //Xuất thông báo lỗi
                                oke = false;
                            }
                            else
                            {
                                //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                uow.CommitChanges();
                                //Xuất thông báo thành công
                                oke = true;
                            }

                        }
                    }
                }
            }
            return oke;
        }
    }
}

