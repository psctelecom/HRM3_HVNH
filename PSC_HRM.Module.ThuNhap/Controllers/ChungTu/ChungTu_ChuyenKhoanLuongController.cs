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

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ChungTu_ChuyenKhoanLuongController : ViewController
    {
        public ChungTu_ChuyenKhoanLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ChuyenKhoanLuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ChuyenKhoanLuongNhanVien>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ChuyenKhoanLuongNhanVien chuyenKhoan = View.CurrentObject as ChuyenKhoanLuongNhanVien;

            if (chuyenKhoan != null)
            {
                if (KiemTraChuyenKhoanLuongNhanVien(chuyenKhoan) && !TruongConfig.MaTruong.Contains("UEL"))
                {
                    DialogUtil.ShowError("Loại chuyển khoản đã tồn tại.");
                    return;
                }
                else
                {
                    View.ObjectSpace.CommitChanges();
                }

                if (chuyenKhoan.KyTinhLuong.KhoaSo)
                {
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", chuyenKhoan.KyTinhLuong.TenKy));
                }
                else if (chuyenKhoan.ThanhToan == true)
                {
                    DialogUtil.ShowWarning("Không thể lập chứng từ chuyển khoản. Chứng từ đã được chi tiền.");
                }
                else if (chuyenKhoan.NgayLap < chuyenKhoan.KyTinhLuong.TuNgay || chuyenKhoan.NgayLap > chuyenKhoan.KyTinhLuong.DenNgay.AddHours(1))
                {
                    DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                }
                else
                {
                    using (DialogUtil.AutoWait())
                    {
                        //Xử lý dữ liệu mới
                        SystemContainer.Resolver<ITaiChinh>("TinhChuyenKhoan").XuLy(View.ObjectSpace, chuyenKhoan, null);

                        View.ObjectSpace.ReloadObject(chuyenKhoan);
                        chuyenKhoan.SoTienBangChu = HamDungChung.DocTien(Math.Round(chuyenKhoan.SoTien, 0));
                        View.ObjectSpace.CommitChanges();
                        (View as DetailView).Refresh();
                    }
                    DialogUtil.ShowInfo("Tính chuyển khoản thành công.");
                }
            }
        }

        private bool KiemTraChuyenKhoanLuongNhanVien(ChuyenKhoanLuongNhanVien chungTu) 
        {
            bool result = false;

            if (chungTu.LoaiChi != null)
            {
                string LuongVaPhuCap = string.Empty;
                string LuongKy1 = string.Empty;
                string LuongKy2 = string.Empty;
                string PhuCap = string.Empty;
                string TruyLinh = string.Empty;
                string KhenThuong = string.Empty;
                string ThuNhapKhac = string.Empty;
                string TruyThu = string.Empty;
                string ThuNhapTangThem = string.Empty;
                string NgoaiGio = string.Empty;
                string KhauTruLuong = string.Empty;
                string ThuLao = string.Empty;
                string LuongThuViec = string.Empty;
                string BoSungLuongKy1 = string.Empty;
                string BoSungLuongKy2 = string.Empty;
                string BoSungPhuCapThamNien = string.Empty;
                string BoSungPhuCapTrachNhiem = string.Empty;
                string BoSungNangLuongKy1 = string.Empty;
                string BoSungNangLuongKy2 = string.Empty;
                string PhucVuDaoTao = string.Empty;
                string TrachNhiemQuanLy = string.Empty;
                string DienThoai = string.Empty;

                //Xét loại chuyển khoản
                if (chungTu.LoaiChi.Contains("LuongVaPhuCap"))
                {
                    LuongVaPhuCap = "LuongVaPhuCap";
                }
                if (chungTu.LoaiChi.Equals("LuongKy1"))
                {
                    LuongKy1 = "LuongKy1";
                }
                if (chungTu.LoaiChi.Equals("LuongKy2"))
                {
                    LuongKy2 = "LuongKy2";                }

                if (chungTu.LoaiChi.Contains("PhuCap"))
                {
                    PhuCap = "PhuCap";
                }
                if (chungTu.LoaiChi.Contains("TruyLinh"))
                {
                    TruyLinh = "TruyLinh";
                }
                if (chungTu.LoaiChi.Contains("LuongThuViec"))
                {
                    LuongThuViec = "LuongThuViec";
                }
                if (chungTu.LoaiChi.Contains("KhenThuong"))
                {
                    KhenThuong = "KhenThuong";
                }
                if (chungTu.LoaiChi.Contains("ThuNhapKhac"))
                {
                    ThuNhapKhac = "ThuNhapKhac";
                }
                if (chungTu.LoaiChi.Contains("TruyThu"))
                {
                    TruyThu = "TruyThu";
                }
                if (chungTu.LoaiChi.Contains("ThuNhapTangThem"))
                {
                    ThuNhapTangThem = "ThuNhapTangThem";
                }
                if (chungTu.LoaiChi.Contains("NgoaiGio"))
                {
                    NgoaiGio = "NgoaiGio";
                }
                if (chungTu.LoaiChi.Contains("ThuLao"))
                {
                    ThuLao = "ThuLao";
                }
                if (chungTu.LoaiChi.Contains("BoSungLuongKy1"))
                {
                    BoSungLuongKy1 = "BoSungLuongKy1";
                }
                if (chungTu.LoaiChi.Contains("BoSungLuongKy2"))
                {
                    BoSungLuongKy2 = "BoSungLuongKy2";
                }
                if (chungTu.LoaiChi.Contains("BoSungPhuCapThamNien"))
                {
                    BoSungPhuCapThamNien = "BoSungPhuCapThamNien";
                }
                if (chungTu.LoaiChi.Contains("BoSungPhuCapTrachNhiem"))
                {
                    BoSungPhuCapTrachNhiem = "BoSungPhuCapTrachNhiem";
                }
                if (chungTu.LoaiChi.Contains("BoSungNangLuongKy1"))
                {
                    BoSungNangLuongKy1 = "BoSungNangLuongKy1";
                }
                if (chungTu.LoaiChi.Contains("BoSungNangLuongKy2"))
                {
                    BoSungNangLuongKy2 = "BoSungNangLuongKy2";
                }
                if (chungTu.LoaiChi.Contains("PhucVuDaoTao"))
                {
                    PhucVuDaoTao = "PhucVuDaoTao";
                }
                if (chungTu.LoaiChi.Contains("TrachNhiemQuanLy"))
                {
                    TrachNhiemQuanLy = "TrachNhiemQuanLy";
                }
                if (chungTu.LoaiChi.Contains("DienThoai"))
                {
                    DienThoai = "DienThoai";
                }

                CriteriaOperator filter = CriteriaOperator.Parse("KyTinhLuong=? And Oid != ?", chungTu.KyTinhLuong.Oid, chungTu.Oid);
                XPCollection<ChuyenKhoanLuongNhanVien> chuyenKhoanLuongNhanVienList = new XPCollection<ChuyenKhoanLuongNhanVien>(((XPObjectSpace)View.ObjectSpace).Session, filter);

                if (chuyenKhoanLuongNhanVienList != null && chuyenKhoanLuongNhanVienList.Count > 0)
                {
                    foreach (ChuyenKhoanLuongNhanVien item in chuyenKhoanLuongNhanVienList)
                    {
                        if (!String.IsNullOrEmpty(item.LoaiChi))
                        {
                            //
                            if ((item.LoaiChi.Trim().Equals(string.Format("{0}", LuongKy1)) && !string.IsNullOrEmpty(LuongKy1))
                                || (item.LoaiChi.Trim().Equals(string.Format("{0}", LuongKy2)) && !string.IsNullOrEmpty(LuongKy2))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", PhuCap)) && !string.IsNullOrEmpty(PhuCap))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", LuongThuViec)) && !string.IsNullOrEmpty(LuongThuViec))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", KhenThuong)) && !string.IsNullOrEmpty(KhenThuong))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", ThuNhapKhac)) && !string.IsNullOrEmpty(ThuNhapKhac))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", TruyThu)) && !string.IsNullOrEmpty(TruyThu))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", ThuNhapTangThem)) && !string.IsNullOrEmpty(ThuNhapTangThem))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", LuongVaPhuCap)) && !string.IsNullOrEmpty(LuongVaPhuCap))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", KhauTruLuong)) && !string.IsNullOrEmpty(KhauTruLuong))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", NgoaiGio)) && !string.IsNullOrEmpty(NgoaiGio))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", ThuLao)) && !string.IsNullOrEmpty(ThuLao))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", BoSungLuongKy1)) && !string.IsNullOrEmpty(BoSungLuongKy1))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", BoSungLuongKy2)) && !string.IsNullOrEmpty(BoSungLuongKy2))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", BoSungPhuCapThamNien)) && !string.IsNullOrEmpty(BoSungPhuCapThamNien))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", BoSungPhuCapTrachNhiem)) && !string.IsNullOrEmpty(BoSungPhuCapTrachNhiem))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", BoSungNangLuongKy1)) && !string.IsNullOrEmpty(BoSungNangLuongKy1))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", BoSungNangLuongKy2)) && !string.IsNullOrEmpty(BoSungNangLuongKy2))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", PhucVuDaoTao)) && !string.IsNullOrEmpty(PhucVuDaoTao))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", TrachNhiemQuanLy)) && !string.IsNullOrEmpty(TrachNhiemQuanLy))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", DienThoai)) && !string.IsNullOrEmpty(DienThoai))
                                )
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            else
                DialogUtil.ShowError("Chưa chọn loại chi tiền.");

            return result;
        }

    }
}

