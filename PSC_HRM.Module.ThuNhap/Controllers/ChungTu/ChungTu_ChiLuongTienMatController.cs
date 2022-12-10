using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.ThuNhap.ChungTu;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ChungTu_ChiLuongTienMatController : ViewController
    {
        public ChungTu_ChiLuongTienMatController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ChiLuongTienMatController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ChiTMLuongNhanVien>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            ChiTMLuongNhanVien chiTMLuongNhanVien = View.CurrentObject as ChiTMLuongNhanVien;
            if (chiTMLuongNhanVien != null)
            {
                if (KiemTraChuyenKhoanLuongNhanVien(chiTMLuongNhanVien))
                {
                    DialogUtil.ShowError("Loại tính tiền mặt đã tồn tại.");
                    return;
                }
                else
                {
                    View.ObjectSpace.CommitChanges();
                }

                if (chiTMLuongNhanVien.KyTinhLuong.KhoaSo)
                {
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", chiTMLuongNhanVien.KyTinhLuong.TenKy));
                }
                else if (chiTMLuongNhanVien.NgayLap < chiTMLuongNhanVien.KyTinhLuong.TuNgay || chiTMLuongNhanVien.NgayLap > chiTMLuongNhanVien.KyTinhLuong.DenNgay)
                {
                    DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                }
                else
                {
                    using (DialogUtil.AutoWait())
                    {
                        SystemContainer.Resolver<ITaiChinh>("TinhChiTienMat").XuLy(View.ObjectSpace, chiTMLuongNhanVien, null);

                        View.ObjectSpace.ReloadObject(chiTMLuongNhanVien);
                        chiTMLuongNhanVien.SoTienBangChu = HamDungChung.DocTien(Math.Round(chiTMLuongNhanVien.SoTien, 0));
                        View.ObjectSpace.CommitChanges();
                        (View as DetailView).Refresh();
                    }
                    DialogUtil.ShowInfo("Lập chứng từ tiền mặt thành công!");
                }
            }
        }
        private bool KiemTraChuyenKhoanLuongNhanVien(ChiTMLuongNhanVien chungTu)
        {
            bool result = false;
            //string LuongKy1 = string.Empty;
            //string LuongKy2 = string.Empty;
            //string PhuCap = string.Empty;
            //string TruyLinhKy1 = string.Empty;
            //string TruyLinhKy2 = string.Empty;
            //string KhenThuong = string.Empty;
            //string ThuNhapKhac = string.Empty;
            //string TruyThu = string.Empty;
            //string ThuNhapTangThem = string.Empty;
            //string NgoaiGio = string.Empty;
            //string ThuLao  = string.Empty;

            ////Xét loại chuyển khoản
            //if (chungTu.LoaiChi.Contains("LuongKy1"))
            //{
            //    LuongKy1 = "LuongKy1";
            //}
            //if (chungTu.LoaiChi.Contains("LuongKy2"))
            //{
            //    LuongKy2 = "LuongKy2";
            //}
            //if (chungTu.LoaiChi.Contains("PhuCap"))
            //{
            //    PhuCap = "PhuCap";
            //}
            //if (chungTu.LoaiChi.Contains("TruyLuongKy1"))
            //{
            //    TruyLinhKy1 = "TruyLinhKy1";
            //}
            //if (chungTu.LoaiChi.Contains("TruyLuongKy2"))
            //{
            //    TruyLinhKy2 = "TruyLinhKy2";
            //}
            //if (chungTu.LoaiChi.Contains("KhenThuong"))
            //{
            //    KhenThuong = "KhenThuong";
            //}
            //if (chungTu.LoaiChi.Contains("ThuNhapKhac"))
            //{
            //    ThuNhapKhac = "ThuNhapKhac";
            //}
            //if (chungTu.LoaiChi.Contains("TruyThu"))
            //{
            //    TruyThu = "TruyThu";
            //}
            //if (chungTu.LoaiChi.Contains("ThuNhapTangThem"))
            //{
            //    ThuNhapTangThem = "ThuNhapTangThem";
            //}
            //if (chungTu.LoaiChi.Contains("NgoaiGio"))
            //{
            //    NgoaiGio = "NgoaiGio";
            //}
            //if (chungTu.LoaiChi.Contains("ThuLao"))
            //{
            //    ThuLao = "ThuLao";
            //}
            //CriteriaOperator filter = CriteriaOperator.Parse("KyTinhLuong=? And Oid != ?", chungTu.KyTinhLuong.Oid, chungTu.Oid);
            //XPCollection<ChiTMLuongNhanVien> chiTMLuongNhanVienList = new XPCollection<ChiTMLuongNhanVien>(((XPObjectSpace)View.ObjectSpace).Session, filter);

            //if (chiTMLuongNhanVienList != null && chiTMLuongNhanVienList.Count > 0)
            //{
            //    foreach (ChiTMLuongNhanVien item in chiTMLuongNhanVienList)
            //    {
            //        if (!String.IsNullOrEmpty(item.LoaiChi))
            //        {
            //            //
            //            if ((item.LoaiChi.Trim().Contains(string.Format("{0}", LuongKy1)) && !string.IsNullOrEmpty(LuongKy1))
            //                || (item.LoaiChi.Trim().Contains(string.Format("{0}", LuongKy2)) && !string.IsNullOrEmpty(LuongKy2))
            //                || (item.LoaiChi.Trim().Contains(string.Format("{0}", PhuCap)) && !string.IsNullOrEmpty(PhuCap))
            //                || (item.LoaiChi.Trim().Contains(string.Format("{0}", TruyLinhKy1)) && !string.IsNullOrEmpty(TruyLinhKy1))
            //                || (item.LoaiChi.Trim().Contains(string.Format("{0}", TruyLinhKy2)) && !string.IsNullOrEmpty(TruyLinhKy2))
            //                || (item.LoaiChi.Trim().Contains(string.Format("{0}", KhenThuong)) && !string.IsNullOrEmpty(KhenThuong))
            //                || (item.LoaiChi.Trim().Contains(string.Format("{0}", ThuNhapKhac)) && !string.IsNullOrEmpty(ThuNhapKhac))
            //                || (item.LoaiChi.Trim().Contains(string.Format("{0}", TruyThu)) && !string.IsNullOrEmpty(TruyThu))
            //                || (item.LoaiChi.Trim().Contains(string.Format("{0}", ThuNhapTangThem)) && !string.IsNullOrEmpty(ThuNhapTangThem))
            //                || (item.LoaiChi.Trim().Contains(string.Format("{0}", NgoaiGio)) && !string.IsNullOrEmpty(NgoaiGio))
            //                || (item.LoaiChi.Trim().Contains(string.Format("{0}", ThuLao)) && !string.IsNullOrEmpty(ThuLao)))
            //            {
            //                result = true;
            //            }
            //        }
            //    }
            //}

            return result;
        }
    }
}
