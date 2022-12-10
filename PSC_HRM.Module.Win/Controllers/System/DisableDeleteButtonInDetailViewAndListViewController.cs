using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.SystemModule;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.ThuNhap.NgoaiGio;
using PSC_HRM.Module.ThuNhap.KhauTru;
using PSC_HRM.Module.ThuNhap.TamUng;
using PSC_HRM.Module.ThuNhap.ThuLao;
using PSC_HRM.Module.ThuNhap.ThuNhapKhac;
using PSC_HRM.Module.ThuNhap.Thuong;
using PSC_HRM.Module.ThuNhap.TruyLuong;
using PSC_HRM.Module.ThuNhap.Thue;
using PSC_HRM.Module.ChamCong;
using DevExpress.ExpressApp;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using PSC_HRM.Module.ThuNhap.ChungTu;
using PSC_HRM.Module.ThuNhap.TruyThu;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.DanhGia;

namespace PSC_HRM.Module.Win.Controllers
{
    public class DisableDeleteButtonInDetailViewAndListViewController : DeleteObjectsViewController
    {
        protected override void UpdateActionState()
        {
            DeleteAction.BeginUpdate();

            try
            {
                base.UpdateActionState();

                if (View == null)
                    return;

                //Ẩn trong detailview
                if (View is DetailView)
                { 
                    DeleteAction.Active["ViewAllowDelete"] = false; 
                }
                //Ẩn trong listview
                else
                {
                    bool enable = true;

                    
                    if (View.ObjectTypeInfo.FullName.Contains("PSC_HRM.Module.ThuNhap"))
                    {
                        #region Module Tài Chính
                        if (View.ObjectTypeInfo.Name == "KyTinhLuong")
                        {
                            KyTinhLuong obj = View.CurrentObject as KyTinhLuong;
                            if (obj != null && obj.KhoaSo)
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangChamCongKhoan")
                        {
                            BangChamCongKhoan obj = View.CurrentObject as BangChamCongKhoan;
                            if (obj != null && obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo)
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangChamCongNgoaiGio")
                        {
                            BangChamCongNgoaiGio obj = View.CurrentObject as BangChamCongNgoaiGio;
                            if (obj != null && obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo)
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangLuongNhanVien")
                        {
                            BangLuongNhanVien obj = View.CurrentObject as BangLuongNhanVien;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "LuongNhanVien")
                        {
                            LuongNhanVien obj = View.CurrentObject as LuongNhanVien;
                            if (obj != null && ((obj.BangLuongNhanVien.KyTinhLuong != null && obj.BangLuongNhanVien.KyTinhLuong.KhoaSo) || obj.BangLuongNhanVien.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietLuongNhanVien")
                        {
                            ChiTietLuongNhanVien obj = View.CurrentObject as ChiTietLuongNhanVien;
                            if (obj != null && ((obj.LuongNhanVien.BangLuongNhanVien.KyTinhLuong != null && obj.LuongNhanVien.BangLuongNhanVien.KyTinhLuong.KhoaSo) || obj.LuongNhanVien.BangLuongNhanVien.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangLuongNgoaiGio")
                        {
                            BangLuongNgoaiGio obj = View.CurrentObject as BangLuongNgoaiGio;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietLuongNgoaiGio")
                        {
                            ChiTietLuongNgoaiGio obj = View.CurrentObject as ChiTietLuongNgoaiGio;
                            if (obj != null && ((obj.BangLuongNgoaiGio.KyTinhLuong != null && obj.BangLuongNgoaiGio.KyTinhLuong.KhoaSo) || obj.BangLuongNgoaiGio.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangKhauTruLuong")
                        {
                            BangKhauTruLuong obj = View.CurrentObject as BangKhauTruLuong;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietKhauTruLuong")
                        {
                            ChiTietKhauTruLuong obj = View.CurrentObject as ChiTietKhauTruLuong;
                            if (obj != null && ((obj.BangKhauTruLuong.KyTinhLuong != null && obj.BangKhauTruLuong.KyTinhLuong.KhoaSo) || obj.BangKhauTruLuong.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangTamUng")
                        {
                            BangTamUng obj = View.CurrentObject as BangTamUng;
                            if (obj != null && obj.KhoaSo)
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "TamUng")
                        {
                            PSC_HRM.Module.ThuNhap.TamUng.TamUng obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.TamUng.TamUng;
                            if (obj != null && obj.BangTamUng != null && obj.BangTamUng.KhoaSo)
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietTamUng")
                        {
                            ChiTietTamUng obj = View.CurrentObject as ChiTietTamUng;
                            if (obj != null && obj.TamUng != null && obj.TamUng.BangTamUng != null && obj.TamUng.BangTamUng.KhoaSo)
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangKhauTruTamUng")
                        {
                            BangKhauTruTamUng obj = View.CurrentObject as BangKhauTruTamUng;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietKhauTruTamUng")
                        {
                            ChiTietKhauTruTamUng obj = View.CurrentObject as ChiTietKhauTruTamUng;
                            if (obj != null && ((obj.BangKhauTruTamUng.KyTinhLuong != null && obj.BangKhauTruTamUng.KyTinhLuong.KhoaSo) || obj.BangKhauTruTamUng.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangThuLaoNhanVien")
                        {
                            BangThuLaoNhanVien obj = View.CurrentObject as BangThuLaoNhanVien;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietThuLaoNhanVien")
                        {
                            ChiTietThuLaoNhanVien obj = View.CurrentObject as ChiTietThuLaoNhanVien;
                            if (obj != null && ((obj.BangThuLaoNhanVien.KyTinhLuong != null && obj.BangThuLaoNhanVien.KyTinhLuong.KhoaSo) || obj.BangThuLaoNhanVien.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangThuNhapKhac")
                        {
                            BangThuNhapKhac obj = View.CurrentObject as BangThuNhapKhac;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietThuNhapKhac")
                        {
                            ChiTietThuNhapKhac obj = View.CurrentObject as ChiTietThuNhapKhac;
                            if (obj != null && ((obj.BangThuNhapKhac.KyTinhLuong != null && obj.BangThuNhapKhac.KyTinhLuong.KhoaSo) || obj.BangThuNhapKhac.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangThuNhapTangThem")
                        {
                            PSC_HRM.Module.ThuNhap.ThuNhapTangThem.BangThuNhapTangThem obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.ThuNhapTangThem.BangThuNhapTangThem;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietThuNhapTangThem")
                        {
                            PSC_HRM.Module.ThuNhap.ThuNhapTangThem.ChiTietThuNhapTangThem obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.ThuNhapTangThem.ChiTietThuNhapTangThem;
                            if (obj != null && ((obj.BangThuNhapTangThem.KyTinhLuong != null && obj.BangThuNhapTangThem.KyTinhLuong.KhoaSo) || obj.BangThuNhapTangThem.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangThuongNhanVien")
                        {
                            BangThuongNhanVien obj = View.CurrentObject as BangThuongNhanVien;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietThuongNhanVien")
                        {
                            ChiTietThuongNhanVien obj = View.CurrentObject as ChiTietThuongNhanVien;
                            if (obj != null && ((obj.BangThuongNhanVien.KyTinhLuong != null && obj.BangThuongNhanVien.KyTinhLuong.KhoaSo) || obj.BangThuongNhanVien.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangTruyLuong")
                        {
                            BangTruyLuong obj = View.CurrentObject as BangTruyLuong;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "TruyLuongNhanVien")
                        {
                            TruyLuongNhanVien obj = View.CurrentObject as TruyLuongNhanVien;
                            if (obj != null && ((obj.BangTruyLuong.KyTinhLuong != null && obj.BangTruyLuong.KyTinhLuong.KhoaSo) || obj.BangTruyLuong.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietTruyLuong")
                        {
                            ChiTietTruyLuong obj = View.CurrentObject as ChiTietTruyLuong;
                            if (obj != null && ((obj.TruyLuongNhanVien.BangTruyLuong.KyTinhLuong != null && obj.TruyLuongNhanVien.BangTruyLuong.KyTinhLuong.KhoaSo) || obj.TruyLuongNhanVien.BangTruyLuong.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangTruyThu")
                        {
                            BangTruyThu obj = View.CurrentObject as BangTruyThu;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "TruyThuNhanVien")
                        {
                            TruyThuNhanVien obj = View.CurrentObject as TruyThuNhanVien;
                            if (obj != null && ((obj.BangTruyThu.KyTinhLuong != null && obj.BangTruyThu.KyTinhLuong.KhoaSo) || obj.BangTruyThu.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietTruyThu")
                        {
                            ChiTietTruyThu obj = View.CurrentObject as ChiTietTruyThu;
                            if (obj != null && ((obj.TruyThuNhanVien.BangTruyThu.KyTinhLuong != null && obj.TruyThuNhanVien.BangTruyThu.KyTinhLuong.KhoaSo) || obj.TruyThuNhanVien.BangTruyThu.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangThueTNCNTamTru")
                        {
                            BangThueTNCNTamTru obj = View.CurrentObject as BangThueTNCNTamTru;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ThueTNCNTamTruNhanVien")
                        {
                            ThueTNCNTamTruNhanVien obj = View.CurrentObject as ThueTNCNTamTruNhanVien;
                            if (obj != null && ((obj.BangThueTNCNTamTru.KyTinhLuong != null && obj.BangThueTNCNTamTru.KyTinhLuong.KhoaSo) || obj.BangThueTNCNTamTru.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ThueTNCNTamTruNgoai")
                        {
                            ThueTNCNTamTruNgoai obj = View.CurrentObject as ThueTNCNTamTruNgoai;
                            if (obj != null && ((obj.BangThueTNCNTamTru.KyTinhLuong != null && obj.BangThueTNCNTamTru.KyTinhLuong.KhoaSo) || obj.BangThueTNCNTamTru.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChuyenKhoanLuongNhanVien")
                        {
                            ChuyenKhoanLuongNhanVien obj = View.CurrentObject as ChuyenKhoanLuongNhanVien;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.UyNhiemChi != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChuyenKhoanLuongNhanVienChiTiet")
                        {
                            ChuyenKhoanLuongNhanVienChiTiet obj = View.CurrentObject as ChuyenKhoanLuongNhanVienChiTiet;
                            if (obj != null && ((obj.ChuyenKhoanLuongNhanVien.KyTinhLuong != null && obj.ChuyenKhoanLuongNhanVien.KyTinhLuong.KhoaSo) || obj.ChuyenKhoanLuongNhanVien.UyNhiemChi != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTMLuongNhanVien")
                        {
                            ChiTMLuongNhanVien obj = View.CurrentObject as ChiTMLuongNhanVien;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.UyNhiemChi != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTMLuongNhanVienChiTiet")
                        {
                            ChiTMLuongNhanVienChiTiet obj = View.CurrentObject as ChiTMLuongNhanVienChiTiet;
                            if (obj != null && ((obj.ChiTMLuongNhanVien.KyTinhLuong != null && obj.ChiTMLuongNhanVien.KyTinhLuong.KhoaSo) || obj.ChiTMLuongNhanVien.UyNhiemChi != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChungTu")
                        {
                            PSC_HRM.Module.ThuNhap.ChungTu.ChungTu obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.ChungTu.ChungTu;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.UyNhiemChi != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangChotTongThuNhapNhanVien")
                        {
                            PSC_HRM.Module.ThuNhap.ChungTu.BangChotTongThuNhapNhanVien obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.ChungTu.BangChotTongThuNhapNhanVien;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo)))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BoSungLuongNhanVien")
                        {
                            PSC_HRM.Module.ThuNhap.BoSungLuong.BoSungLuongNhanVien obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.BoSungLuong.BoSungLuongNhanVien;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo)))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiBoSungLuongKy1")
                        {
                            PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungLuongKy1 obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungLuongKy1;
                            if (obj != null && ((obj.BoSungLuongNhanVien.KyTinhLuong != null && obj.BoSungLuongNhanVien.KyTinhLuong.KhoaSo) || obj.BoSungLuongNhanVien.ChungTuLuongKy1 != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiBoSungPhuCapUuDai")
                        {
                            PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungPhuCapUuDai obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungPhuCapUuDai;
                            if (obj != null && ((obj.BoSungLuongNhanVien.KyTinhLuong != null && obj.BoSungLuongNhanVien.KyTinhLuong.KhoaSo) || obj.BoSungLuongNhanVien.ChungTuLuongKy1 != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiBoSungPhuCapTrachNhiem")
                        {
                            PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungPhuCapTrachNhiem obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungPhuCapTrachNhiem;
                            if (obj != null && ((obj.BoSungLuongNhanVien.KyTinhLuong != null && obj.BoSungLuongNhanVien.KyTinhLuong.KhoaSo) || obj.BoSungLuongNhanVien.ChungTuLuongKy1 != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiBoSungPhuCapThamNien")
                        {
                            PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungPhuCapThamNien obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungPhuCapThamNien;
                            if (obj != null && ((obj.BoSungLuongNhanVien.KyTinhLuong != null && obj.BoSungLuongNhanVien.KyTinhLuong.KhoaSo) || obj.BoSungLuongNhanVien.ChungTuLuongKy1 != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiBoSungNangLuongKy1")
                        {
                            PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungNangLuongKy1 obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungNangLuongKy1;
                            if (obj != null && ((obj.BoSungLuongNhanVien.KyTinhLuong != null && obj.BoSungLuongNhanVien.KyTinhLuong.KhoaSo) || obj.BoSungLuongNhanVien.ChungTuLuongKy1 != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiBoSungLuongKy2")
                        {
                            PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungLuongKy2 obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungLuongKy2;
                            if (obj != null && ((obj.BoSungLuongNhanVien.KyTinhLuong != null && obj.BoSungLuongNhanVien.KyTinhLuong.KhoaSo) || obj.BoSungLuongNhanVien.ChungTuLuongKy2 != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiBoSungNangLuongKy2")
                        {
                            PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungNangLuongKy2 obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungNangLuongKy2;
                            if (obj != null && ((obj.BoSungLuongNhanVien.KyTinhLuong != null && obj.BoSungLuongNhanVien.KyTinhLuong.KhoaSo) || obj.BoSungLuongNhanVien.ChungTuLuongKy2 != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiBoSungPhuCapTienSi")
                        {
                            PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungPhuCapTienSi obj = View.CurrentObject as PSC_HRM.Module.ThuNhap.BoSungLuong.ChiBoSungPhuCapTienSi;
                            if (obj != null && ((obj.BoSungLuongNhanVien.KyTinhLuong != null && obj.BoSungLuongNhanVien.KyTinhLuong.KhoaSo) || obj.BoSungLuongNhanVien.ChungTuTienSi != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangTruyThuKhac")
                        {
                            BangTruyThuKhac obj = View.CurrentObject as BangTruyThuKhac;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "TruyThuKhac")
                        {
                            TruyThuKhac obj = View.CurrentObject as TruyThuKhac;
                            if (obj != null && ((obj.BangTruyThuKhac.KyTinhLuong != null && obj.BangTruyThuKhac.KyTinhLuong.KhoaSo) || obj.BangTruyThuKhac.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietTruyThuKhac")
                        {
                            ChiTietTruyThuKhac obj = View.CurrentObject as ChiTietTruyThuKhac;
                            if (obj != null && ((obj.TruyThuKhac.BangTruyThuKhac.KyTinhLuong != null && obj.TruyThuKhac.BangTruyThuKhac.KyTinhLuong.KhoaSo) || obj.TruyThuKhac.BangTruyThuKhac.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "BangTruyLinhKhac")
                        {
                            BangTruyLinhKhac obj = View.CurrentObject as BangTruyLinhKhac;
                            if (obj != null && ((obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo) || obj.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "TruyLinhKhac")
                        {
                            TruyLinhKhac obj = View.CurrentObject as TruyLinhKhac;
                            if (obj != null && ((obj.BangTruyLinhKhac.KyTinhLuong != null && obj.BangTruyLinhKhac.KyTinhLuong.KhoaSo) || obj.BangTruyLinhKhac.ChungTu != null))
                                enable = false;
                        }
                        else if (View.ObjectTypeInfo.Name == "ChiTietTruyLinhKhac")
                        {
                            ChiTietTruyLinhKhac obj = View.CurrentObject as ChiTietTruyLinhKhac;
                            if (obj != null && ((obj.TruyLinhKhac.BangTruyLinhKhac.KyTinhLuong != null && obj.TruyLinhKhac.BangTruyLinhKhac.KyTinhLuong.KhoaSo) || obj.TruyLinhKhac.BangTruyLinhKhac.ChungTu != null))
                                enable = false;
                        }
                       #endregion
                    }
                    else
                    {
                        #region Module Nhân Sự
                        if (View.ObjectTypeInfo.Name == "BangChotThongTinTinhLuong")
                        {
                            BangChotThongTinTinhLuong obj = View.CurrentObject as BangChotThongTinTinhLuong;
                            if (obj != null && obj.KhoaSo)
                                enable = false;
                        }

                        if (View.ObjectTypeInfo.Name == "QuanLyChamCongNhanVien")
                        {
                            QuanLyChamCongNhanVien obj = View.CurrentObject as QuanLyChamCongNhanVien;
                            if (obj != null && obj.KyTinhLuong.KhoaSo)
                                enable = false;
                        }

                        if (View.ObjectTypeInfo.Name == "ChiTietDanhGiaCanBoCuoiNamLan1")
                        {
                            ChiTietDanhGiaCanBoCuoiNamLan1 obj = View.CurrentObject as ChiTietDanhGiaCanBoCuoiNamLan1;
                            if (obj != null && obj.TinhTrangDuyet == TinhTrangDuyetEnum.DaChot)
                                enable = false;
                        }
                        #endregion
                    }
                    //
                    DeleteAction.Active["ViewAllowDelete"] = enable;
                }
            }
            finally
            {
                DeleteAction.EndUpdate();
            }
        }
    }
}
