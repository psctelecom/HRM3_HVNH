using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using PSC_HRM.Module.ThuNhap.ThuLao;
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi;
using System.Windows.Forms;
using PSC_HRM.Module.PMS.NghiepVu.NCKH;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class QuanLyHoatDongKhacNhapNon_LayDuLieu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        QuanLyHDK_Nhap_Non _HoatDong;
        CollectionSource collectionSource;
        Nhap_ChiTietKeKhaiHDK_VHU_Non _source;
        public QuanLyHoatDongKhacNhapNon_LayDuLieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyHDK_Nhap_Non_DetailView";
        }

        private void btSearch_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _HoatDong = View.CurrentObject as QuanLyHDK_Nhap_Non;
            if (_HoatDong != null)
            {
                using (DialogUtil.AutoWait())
                {
                    _HoatDong.LoadData();
                }
            }
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string Chuoi = "";
            object kq;
            _HoatDong = View.CurrentObject as QuanLyHDK_Nhap_Non;
            if (_HoatDong != null)
            {
                foreach (var item in _HoatDong.DanhSach)
                {
                    if (item.Chon)
                    {
                        Chuoi += item.OidKey + ";";
                    }
                }
                if (Chuoi == "")
                {
                    MessageBox.Show("Vui lòng chọn dòng cần hủy xác nhận!");
                }
                else
                {
                    if (DialogUtil.ShowYesNo(string.Format("Bạn có muốn xóa hoạt động?")) == DialogResult.Yes)
                    {
                        using (DialogUtil.AutoWait())
                        {
                            SqlParameter[] param = new SqlParameter[1];
                            param[0] = new SqlParameter("@Chuoi", Chuoi);
                            kq = DataProvider.GetValueFromDatabase("spd_QuanLyHoatDongKhac_XoaHoatDong", CommandType.StoredProcedure, param);

                            _HoatDong.LoadData();
                        }
                        MessageBox.Show(kq.ToString(), "Thông báo");
                    }
                }
            }
        }

        private void PopKeKhaiSauGiang_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            session = ((XPObjectSpace)_obs).Session;
            _HoatDong = View.CurrentObject as QuanLyHDK_Nhap_Non;
            if (_HoatDong != null)
            {
                collectionSource = new CollectionSource(_obs, typeof(Nhap_ChiTietKeKhaiHDK_VHU_Non));
                using (DialogUtil.AutoWait("Load danh sách giảng viên"))
                {
                    collectionSource = new CollectionSource(_obs, typeof(Nhap_ChiTietKeKhaiHDK_VHU_Non));
                    _source = new Nhap_ChiTietKeKhaiHDK_VHU_Non(session);
                    _source.GhiChu = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    e.View = Application.CreateDetailView(_obs, _source);
                }
            }
        }

        private void popKeKhaiSauGiang_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_source.ThanhToanTienMat == false)
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@NhanVien", _source.NhanVien.Oid);
                param[1] = new SqlParameter("@DanhMucHoatDongKhac", _source.DanhMucHoatDongKhac.Oid);
                param[2] = new SqlParameter("@NamHoc", _source.NamHoc.Oid);
                param[3] = new SqlParameter("@HocKy", _source.HocKy.Oid);
                param[4] = new SqlParameter("@SoLuong", _source.SoLuong);
                param[5] = new SqlParameter("@SoGioQuyDoi", _source.SoGioQuyDoi);
                param[6] = new SqlParameter("@GhiChu", _source.GhiChu != "" ? _source.GhiChu : "" + " - " + _source.ChiTietKhoiLuongGiangDay_Moi == null ? "" : _source.ChiTietKhoiLuongGiangDay_Moi.LopHocPhan.ToString() + " - " + _source.ChiTietKhoiLuongGiangDay_Moi == null ? "" : _source.SoTC.ToString());
                DataProvider.ExecuteNonQuery("spd_QuanLyHoatDongKhac_ThemMoiHoatDong", CommandType.StoredProcedure, param);
            }
            else
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@NhanVien", _source.NhanVien.Oid);
                param[1] = new SqlParameter("@DanhMucHoatDongKhac", _source.DanhMucHoatDongKhac.Oid);
                param[2] = new SqlParameter("@NamHoc", _source.NamHoc.Oid);
                param[3] = new SqlParameter("@HocKy", _source.HocKy.Oid);
                param[4] = new SqlParameter("@SoTienThanhToan", _source.SoTienThanhToan);
                param[5] = new SqlParameter("@GhiChu", _source.GhiChu != "" ? _source.GhiChu : "" + " - " + _source.ChiTietKhoiLuongGiangDay_Moi == null ? "" : _source.ChiTietKhoiLuongGiangDay_Moi.LopHocPhan.ToString() + " - " + _source.ChiTietKhoiLuongGiangDay_Moi == null ? "" : _source.SoTC.ToString());
                param[6] = new SqlParameter("@SoLuong", _source.SoLuong);
                param[7] = new SqlParameter("@SoGioQuyDoi", _source.SoGioQuyDoi);
                DataProvider.ExecuteNonQuery("spd_QuanLyHoatDongKhac_ThemMoiHoatDong_TienMat", CommandType.StoredProcedure, param);
            }
            _HoatDong.LoadData();
            MessageBox.Show("Thêm thành công!", "Thông báo");
        }
    }
}