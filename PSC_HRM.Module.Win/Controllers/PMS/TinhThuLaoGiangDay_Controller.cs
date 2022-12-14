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

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class TinhThuLaoGiangDay_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        public TinhThuLaoGiangDay_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "BangChotThuLao_DetailView";
        }
        private void TinhThuLaoGiangDay_Controller_Activated(object sender, EventArgs e)
        {
            btTinhThuLao.Active["TruyCap"] = false;
        }

        private void btTinhThuLao_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            session = ((XPObjectSpace)_obs).Session;
            BangChotThuLao obj = View.CurrentObject as BangChotThuLao;
            if (obj != null)
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

                                        Utils.XuLyDuLieu(((XPObjectSpace)View.ObjectSpace).Session, "spd_PMS_TinhThuLaoGiangDay", CommandType.StoredProcedure, param);
                                    }
                                }
                            }
                        }                        
                      
                        View.ObjectSpace.Refresh();
                    }
                    else
                        XtraMessageBox.Show("Khóa bảng chốt trước khi tính thù lao!", "Thông báo");
                }
            }
        }
    }
}