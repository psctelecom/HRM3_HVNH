using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.PMS.NghiepVu.SauDaiHoc;
using PSC_HRM.Module.PMS.NghiepVu.ThanhToan;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.Import.ImportClass
{
    public class Imp_SiSoChuyenNganh_SauDaiHoc
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs)
        {
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            var mainLog = new StringBuilder();
            //
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        //Loại file
                        //LoaiOfficeEnum loaiOffice = LoaiOfficeEnum.Office2003;
                        //if (open.SafeFileName.Contains(".xlsx"))
                        //{ loaiOffice = LoaiOfficeEnum.Office2010; }

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[sheet1$A1:E]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idSTT = 0;
                            const int idChuyenNganh=1;
                            const int idKhoa = 2;
                            const int idSiSo = 4;

                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();

                                int SiSo = 0;
                                int STT=0;
                                SiSoChuyenNganh _SiSoChuyenNganh = null;

                                //Duyệt qua tất cả các dòng trong file excel
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        STT++;
                                        
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //

                                        #region STT
                                        string txtSTT = dr[idSTT].ToString();
                                        #endregion

                                        #region ChuyenNganh
                                        string txtChuyenNganh = dr[idChuyenNganh].ToString();
                                        #endregion

                                        #region Khoa
                                        string txtKhoa = dr[idKhoa].ToString();
                                        #endregion

                                        #region SiSo
                                        if (dr[idSiSo].ToString() != string.Empty)
                                            SiSo = Convert.ToInt32(dr[idSiSo].ToString());
                                        #endregion
                                        //
                                        #endregion
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        //#region Kiểm tra dữ liệu
                                        if ((!string.IsNullOrEmpty(txtChuyenNganh) && !string.IsNullOrEmpty(txtKhoa)) )
                                        {
                                            CriteriaOperator f = CriteriaOperator.Parse("TenNganh =? and Khoa =?", txtChuyenNganh, txtKhoa);
                                            _SiSoChuyenNganh = uow.FindObject<SiSoChuyenNganh>(f);
                                            if (_SiSoChuyenNganh == null)
                                            {
                                                _SiSoChuyenNganh = new SiSoChuyenNganh(uow);
                                                _SiSoChuyenNganh.TenNganh = txtChuyenNganh;
                                                _SiSoChuyenNganh.Khoa = txtKhoa;
                                                _SiSoChuyenNganh.SoHocVien = SiSo;
                                                uow.CommitChanges();//Lưu
                                                sucessNumber++;
                                            }
                                        }
                                        #region Ghi File log
                                        //Đưa thông tin bị lỗi vào blog
                                        if (errorLog.Length > 0)
                                        {
                                            mainLog.AppendLine("- STT: " + STT);
                                            mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                            mainLog.AppendLine(errorLog.ToString());
                                            sucessImport = false;
                                        }
                                        ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////     
                                        if (sucessImport)
                                        {
                                            //uow.CommitChanges();////Lưu                                        
                                            sucessNumber++;
                                        }
                                        else
                                        {
                                            uow.RollbackTransaction(); ////trả lại dữ liệu ban đầu
                                            erorrNumber++;
                                            sucessImport = true;
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                    }
                    //
                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                    DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Số hoạt động không thành công " + erorrNumber + " " + s + "!");

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
                        Process.Start(tenFile);
                    }
                }
            }
        }
        #endregion
    }
}