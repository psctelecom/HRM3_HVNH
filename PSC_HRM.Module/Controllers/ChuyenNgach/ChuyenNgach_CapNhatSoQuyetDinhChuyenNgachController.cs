using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.ChuyenNgach;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.NonPersistentObjects;
using System.Windows.Forms;
using System.Text;
using System.Data;

namespace PSC_HRM.Module.Controllers
{
    public partial class ChuyenNgach_CapNhatSoQuyetDinhController : ViewController
    {
       
        public ChuyenNgach_CapNhatSoQuyetDinhController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ChuyenNgach_CapNhatSoQuyetDinhController");
        }
        private void ChuyenNgach_CapNhatSoQuyetDinhController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhChuyenNgach>();
        }
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Tiến hành cập nhật số quyết định nếu chưa có.
            CapNhatSoQuyetDinh();
        }
        private void CapNhatSoQuyetDinh()
        {
            int count = 0;
            //
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        //
                        using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)View.ObjectSpace).Session.DataLayer))
                        {
                            uow.BeginTransaction();
                            //
                            using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A1:B]"))
                            {
                                StringBuilder mainLog = new StringBuilder();

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //
                                        int idx_MaQuanLy = 0;
                                        int idx_SoQuyetDinh = 1;

                                        if (string.IsNullOrEmpty(item[idx_MaQuanLy].ToString().Trim()))//Nếu không có mã thì ngừng
                                        { break; }
                                        //
                                        QuyetDinhChuyenNgach qd = uow.FindObject<QuyetDinhChuyenNgach>(CriteriaOperator.Parse("ListChiTietQuyetDinhChuyenNgach[ThongTinNhanVien.MaQuanLy=?]",item[idx_MaQuanLy].ToString().Trim()));
                                        if (qd != null)
                                        {
                                            if (!string.IsNullOrEmpty(item[idx_SoQuyetDinh].ToString().RemoveEmpty()) && String.IsNullOrEmpty(qd.SoQuyetDinh) && qd.ListChiTietQuyetDinhChuyenNgach.Count == 1)
                                            {
                                                qd.SoQuyetDinh = item[idx_SoQuyetDinh].ToString();
                                                //
                                                count +=1;
                                            }
                                        }
                                        else
                                        {
                                            mainLog.AppendLine(" + Không tìm thấy cán bộ nào có mã quản lý là:" + item[idx_MaQuanLy].ToString() + " trong tất cả quyết định chuyển ngạch.");
                                        }
                                    }
                                    //
                                    if (mainLog.Length > 0)
                                    {
                                        uow.RollbackTransaction();

                                        if (DialogUtil.ShowYesNo("Không thể cập nhật số quyết địnhk vì sai thông tin. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
                                        {
                                            using (SaveFileDialog saveFile = new SaveFileDialog())
                                            {
                                                saveFile.Filter = "Text files (*.txt)|*.txt";

                                                if (saveFile.ShowDialog() == DialogResult.OK)
                                                {
                                                    HamDungChung.WriteLog(saveFile.FileName, mainLog.ToString());
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                        uow.CommitChanges();
                                        //
                                        View.ObjectSpace.Refresh();
                                        //
                                        DialogUtil.ShowInfo(String.Format("Đã cập nhật {0} số quyết định.",count));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
