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
using PSC_HRM.Module.ThuNhap.NonPersistentThuNhap;
using PSC_HRM.Module.ReportClass;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using System.Windows.Forms;
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class ChotKhoiLuongGiangDay_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        public ChotKhoiLuongGiangDay_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "BangChotThuLao_DetailView";
        }

        void ChotKhoiLuongGiangDay_Controller_Activated(object sender, System.EventArgs e)
        {
            btQuyDoi.Active["TruyCap"] = false;
        }
        private void btChotKhoiLuongGiangDay_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            object kq = "";
            _obs = Application.CreateObjectSpace();
            session = ((XPObjectSpace)_obs).Session;
            BangChotThuLao obj = View.CurrentObject as BangChotThuLao;
            if (obj != null)
            {             
                {
                    if (!obj.Khoa)
                    {
                        #region Chốt khối lượng giảng dạy
                        using (DialogUtil.AutoWait("Đang đồng bộ dữ liệu"))
                        {

                            if (TruongConfig.MaTruong == "HUFLIT")
                            {
                                if (obj.HocKy == null)
                                {
                                    XtraMessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo");
                                    return;
                                }
                            }
                            if (TruongConfig.MaTruong == "UEL")
                            {
                                if (obj.KyTinhPMS == null)
                                {
                                    XtraMessageBox.Show("Vui lòng chọn đợt chi trả thù lao!", "Thông báo");
                                    return;
                                }
                            }
                            if (TruongConfig.MaTruong != "DNU")
                                obj.Khoa = true;
                            View.ObjectSpace.CommitChanges();
                            SqlParameter[] pDongBo = new SqlParameter[2];
                            pDongBo[0] = new SqlParameter("@BangChot", obj.Oid);
                            pDongBo[1] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                            kq = DataProvider.GetValueFromDatabase("spd_pms_ChotThuLaoGiangDay", CommandType.StoredProcedure, pDongBo);

                            if (TruongConfig.MaTruong == "UFM")
                            {
                                SqlParameter[] pTinhTien = new SqlParameter[1];
                                pTinhTien[0] = new SqlParameter("@BangChot", obj.Oid);
                                DataProvider.ExecuteNonQuery("spd_PMS_QuyDoi_ThongTinBangChot", CommandType.StoredProcedure, pTinhTien);
                            }
                            if (TruongConfig.MaTruong == "HVNH")
                            {
                                QuanLyGioGiang gioGiang = session.FindObject<QuanLyGioGiang>(CriteriaOperator.Parse("NamHoc =?", obj.NamHoc.Oid));
                                if (gioGiang == null)
                                {
                                    gioGiang = new QuanLyGioGiang(session);
                                    gioGiang.NamHoc = session.FindObject<NamHoc>(CriteriaOperator.Parse("Oid =?", obj.NamHoc.Oid));
                                    gioGiang.ThongTinTruong = HamDungChung.ThongTinTruong(session);
                                }
                                _obs.CommitChanges();
                                SqlParameter[] pQuyDoi = new SqlParameter[3];
                                pQuyDoi[0] = new SqlParameter("@NamHoc", gioGiang.NamHoc.Oid);
                                pQuyDoi[1] = new SqlParameter("@QuanLyGioGiang", gioGiang.Oid);
                                pQuyDoi[2] = new SqlParameter("@BangChot", obj.Oid);
                                DataProvider.GetValueFromDatabase("spd_PMS_DongBoDuLieu_QuanLyGioGiang", CommandType.StoredProcedure, pQuyDoi);
                            }
                        }
                        //View.ObjectSpace.Refresh();
                        #endregion

                        XtraMessageBox.Show(kq.ToString(), "Thông báo");

                        #region Tính thù lao
                        if (TruongConfig.MaTruong != "UEL" && TruongConfig.MaTruong != "DNU" && TruongConfig.MaTruong != "UFM")
                        {
                            using (DialogUtil.AutoWait("Đang tính thù lao giảng dạy"))
                            {
                                if (obj.Khoa)
                                {
                                    obj.DaTinhThuLao = true;
                                    View.ObjectSpace.CommitChanges();
                                    //Lấy công thức tính lương
                                    XPCollection<CongThucTinhThuLaoGiangDay> congThucTinhThuLaoList;
                                    congThucTinhThuLaoList = new XPCollection<CongThucTinhThuLaoGiangDay>(((XPObjectSpace)View.ObjectSpace).Session);

                                    //xóa dữ liệu cũ
                                    //Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_Luong_XoaLuongNhanVienTheoBangLuong", CommandType.StoredProcedure, new SqlParameter("@BangLuongNhanVien", bangLuong.Oid));

                                    //phân quyền bộ phận
                                    //Utils.PhanQuyenDonVi(((XPObjectSpace)obs).Session, bangLuong.ThongTinTruong);

                                    string dieuKienNhanVien;

                                    foreach (CongThucTinhThuLaoGiangDay ct in congThucTinhThuLaoList)
                                    {
                                        if (!ct.NgungSuDung)
                                        {
                                            dieuKienNhanVien = ct.DieuKienNhanVien.XuLyDieuKienPMS(((XPObjectSpace)View.ObjectSpace), false, new object[] { obj.NamHoc.NgayBatDau, obj.NamHoc.NgayKetThuc });
                                            //
                                            foreach (ChiTietCongThucTinhThuLaoGiangDay ctItem in ct.ListChiTietCongThuc)
                                            {
                                                if (!ctItem.NgungSuDung)
                                                {
                                                    SqlParameter[] param = new SqlParameter[3];
                                                    param[0] = new SqlParameter("@BangChot", obj.Oid);
                                                    param[1] = new SqlParameter("@DieuKienNhanVien", dieuKienNhanVien);
                                                    param[2] = new SqlParameter("@CongThucTinhThuLao", ctItem.Oid);

                                                    Utils.XuLyDuLieu(session, "spd_PMS_TinhThuLaoGiangDay", CommandType.StoredProcedure, param);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                    XtraMessageBox.Show("Khóa bảng chốt trước khi tính thù lao!", "Thông báo");
                            }
                        }
                        #endregion

                        View.ObjectSpace.Refresh();
                    }
                    else
                        XtraMessageBox.Show("Đã khóa bảng chốt, không thể chốt khối lượng", "Thông báo");
                }
            }
        }

        private void btMoKhoa_BangChot_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            session = ((XPObjectSpace)_obs).Session;
            BangChotThuLao obj = View.CurrentObject as BangChotThuLao;
            if (obj != null)
            {
                object kq = 0;

                if (obj.Khoa)
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa dữ liệu cũ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SqlParameter[] pXoaChot = new SqlParameter[1];
                        pXoaChot[0] = new SqlParameter("@BangChot", obj.Oid);
                        kq = DataProvider.GetValueFromDatabase("spd_PMS_MoKhoa_XoaDuLieuChotThuLao", CommandType.StoredProcedure, pXoaChot);
                        if (Convert.ToInt32(kq) > 0)
                            XtraMessageBox.Show("Dữ liệu đã được chuyển qua tài chính - Không thể mở khóa!", "Thông báo");
                        else
                        {
                            View.ObjectSpace.Refresh();
                            XtraMessageBox.Show("Đã mở khóa - xóa dữ liệu bảng chốt thành công!", "Thông báo");
                        }
                    }
                }
                else
                    XtraMessageBox.Show("Bảng chốt không khóa!", "Thông báo");
            }
        }

        private void btQuyDoi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BangChotThuLao obj = View.CurrentObject as BangChotThuLao;
            if (obj != null)
            {
                if (!obj.Khoa)
                {
                    using (DialogUtil.AutoWait("Đang quy đổi dữ liệu"))
                    {
                        obj.Khoa = true;
                        View.ObjectSpace.CommitChanges();
                        SqlParameter[] pQuyDoi = new SqlParameter[1];
                        pQuyDoi[0] = new SqlParameter("@BangChot", obj.Oid);
                        DataProvider.ExecuteNonQuery("spd_PMS_QuyDoi_ThongTinBangChot", CommandType.StoredProcedure, pQuyDoi);
                    }
                }
                else
                    XtraMessageBox.Show("Đã khóa dữ liệu - không thể quy đổi", "Thông báo"); 
            }                      
        }
    }
}