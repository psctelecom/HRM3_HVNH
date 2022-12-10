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
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.ChotThongTinTinhLuong;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ChungTu_LapChungTuTamGiuLuongNhanVienController : ViewController
    {
        public ChungTu_LapChungTuTamGiuLuongNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ChungTu_LapChungTuTamGiuLuongNhanVienController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<TamGiuLuongNhanVien>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (DialogUtil.ShowYesNo("Bạn thật sự muốn lập chứng từ chi lương cho nhân viên đã chọn?") == DialogResult.Yes)
            {
                    //Tiến hành cập nhật chi lại lương nhân viên
                    TamGiuLuongNhanVien tamGiuLuongNhanVien = View.CurrentObject as TamGiuLuongNhanVien;
                    if (tamGiuLuongNhanVien != null)
                    {
                        decimal soTienChungTu = 0;
                        //
                        IObjectSpace obs = Application.CreateObjectSpace();
                        ChungTu_TamGiuLuong chungTu = new ChungTu_TamGiuLuong(((XPObjectSpace)obs).Session);
                        
                        //
                        foreach (ChiTietTamGiuLuongNhanVien item in tamGiuLuongNhanVien.ListChiTietTamGiuLuongNhanVien)
                        {
                            if (item.Chon && item.TrangThai == TrangThaiChiLuongEnum.TamGiuLuong)
                            {
                                ChuyenKhoanLuongNhanVienChiTiet_TamGiuLuong chiTiet = new ChuyenKhoanLuongNhanVienChiTiet_TamGiuLuong(((XPObjectSpace)obs).Session);
                                chiTiet.ChungTu = obs.GetObjectByKey<PSC_HRM.Module.ThuNhap.ChungTu.ChungTu>(item.ChungTu.Oid);
                                chiTiet.KyTinhLuong = obs.GetObjectByKey<KyTinhLuong>(item.KyTinhLuong.Oid);
                                chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                chiTiet.NhanVien = obs.GetObjectByKey<NhanVien>(item.ThongTinNhanVien.Oid);
                                //Lấy ngân hàng và số tài khoản hiện tại của nhân viên 
                                CriteriaOperator criteria = CriteriaOperator.Parse("BangChotThongTinTinhLuong=? and ThongTinNhanVien=?",chiTiet.KyTinhLuong.BangChotThongTinTinhLuong,chiTiet.NhanVien);
                                ThongTinTinhLuong thongTinTinhLuong = obs.FindObject<ThongTinTinhLuong>(criteria);
                                if(thongTinTinhLuong!=null)
                                {
                                    chiTiet.NganHang = obs.GetObjectByKey<NganHang>(thongTinTinhLuong.NganHang.Oid);
                                    chiTiet.SoTaiKhoan = thongTinTinhLuong.SoTaiKhoan;
                                }
                                chiTiet.ThucNhan = item.ThucNhan;
                                chiTiet.ThueTNCN = item.ThueTNCN;
                                //
                                soTienChungTu += item.ThucNhan;
                                //Đưa vào chứng từ
                                chungTu.ChiTietList.Add(chiTiet);
                                
                            }
                        }
                        chungTu.SoTien = soTienChungTu;
                        chungTu.SoTienBangChu = HamDungChung.DocTien(chungTu.SoTien);

                        //Gọi cửa sổ lập chứng từ tạm giữ lương
                        Application.ShowView<ChungTu_TamGiuLuong>(obs, chungTu);
                }
                   
            }
            
        }  
    }
}
