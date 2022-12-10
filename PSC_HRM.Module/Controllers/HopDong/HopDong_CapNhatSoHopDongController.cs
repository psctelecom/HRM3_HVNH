using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using System.Text;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.Controllers
{
    public partial class HopDong_CapNhatSoHopDongController : ViewController
    {
        private IObjectSpace obs;
        private HopDong_TaoHopDong chonHopDong;
        private QuanLyHopDong quanLyHopDong;

        public HopDong_CapNhatSoHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_CapNhatSoHopDongController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            //
            quanLyHopDong = View.CurrentObject as QuanLyHopDong;
            chonHopDong = obs.CreateObject<HopDong_TaoHopDong>();
            e.View = Application.CreateDetailView(obs, chonHopDong);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //Tiến hành cập nhật số hợp đồng từ excel
            CapNhatSoHopDong();
        }
        private void HopDong_CapNhatSoHopDongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active.Clear();
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<HopDong.HopDong>()
                && HamDungChung.IsWriteGranted<HopDong_NhanVien>()
                && HamDungChung.IsWriteGranted<HopDong_LamViec>()
                && HamDungChung.IsWriteGranted<HopDong_Khoan>()
                && HamDungChung.IsWriteGranted<HopDong_LaoDong>();
        }

        private void CapNhatSoHopDong()
        {
            if (quanLyHopDong != null)
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        using (DialogUtil.AutoWait())
                        {
                            //
                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
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
                                            int idx_SoHopDong = 1;

                                            if (string.IsNullOrEmpty(item[idx_MaQuanLy].ToString().Trim()))//Nếu không có mã thì ngừng
                                            {
                                                break;
                                            }
                                            #region Hợp đồng làm việc
                                            if (chonHopDong.LoaiHopDong == TaoHopDongEnum.HopDongLamViec)
                                            {
                                                HopDong_LamViec hd = uow.FindObject<HopDong_LamViec>(CriteriaOperator.Parse("QuanLyHopDong=? and NhanVien.MaQuanLy=?", quanLyHopDong.Oid, item[idx_MaQuanLy].ToString().Trim()));
                                                if (hd != null )
                                                {
                                                    if (!string.IsNullOrEmpty(item[idx_SoHopDong].ToString().RemoveEmpty()) && String.IsNullOrEmpty(hd.SoHopDong))
                                                    {
                                                        hd.SoHopDong = item[idx_SoHopDong].ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    mainLog.AppendLine(" + Không tìm thấy cán bộ nào có mã quản lý là:" + item[idx_MaQuanLy].ToString() + " trong tất cả hợp đồng làm việc.");
                                                }
                                            }
                                            #endregion
                                            #region Hợp đồng hệ số
                                            else if (chonHopDong.LoaiHopDong == TaoHopDongEnum.HopDongHeSo)
                                            {
                                                HopDong_LaoDong hd = uow.FindObject<HopDong_LaoDong>(CriteriaOperator.Parse("QuanLyHopDong=? and NhanVien.MaQuanLy=?", quanLyHopDong.Oid, item[idx_MaQuanLy].ToString().Trim()));
                                                if (hd != null )
                                                {
                                                    if (!string.IsNullOrEmpty(item[idx_SoHopDong].ToString().RemoveEmpty()) && String.IsNullOrEmpty(hd.SoHopDong))
                                                    {
                                                        hd.SoHopDong = item[idx_SoHopDong].ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    mainLog.AppendLine(" + Không tìm thấy cán bộ nào có mã quản lý là:" + item[idx_MaQuanLy].ToString() + " trong tất cả hợp đồng lao động.");
                                                }
                                            }
                                            #endregion
                                                #region Hợp đồng khoán
                                            else
                                            {
                                                HopDong_Khoan hd = uow.FindObject<HopDong_Khoan>(CriteriaOperator.Parse("QuanLyHopDong=? and NhanVien.MaQuanLy=?", quanLyHopDong.Oid, item[idx_MaQuanLy].ToString().Trim()));
                                                if (hd != null)
                                                {
                                                    if (!string.IsNullOrEmpty(item[idx_SoHopDong].ToString().RemoveEmpty()) && String.IsNullOrEmpty(hd.SoHopDong))
                                                    {
                                                        hd.SoHopDong = item[idx_SoHopDong].ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    mainLog.AppendLine(" + Không tìm thấy cán bộ nào có mã quản lý là:" + item[idx_MaQuanLy].ToString() + " trong tất cả hợp đồng khoán.");
                                                }
                                            }
                                                #endregion

                                        }
                                        if (mainLog.Length > 0)
                                        {
                                            uow.RollbackTransaction();

                                            if (DialogUtil.ShowYesNo("Không thể cập nhật số hợp đồng vì sai thông tin. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
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
                                            DialogUtil.ShowInfo("Cập nhật số hợp đồng thành công.");

                                            View.ObjectSpace.Refresh();
                                            obs.Refresh();
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
}
