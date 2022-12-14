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
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class DeNghiThanhToan_ChonDuLieu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        ChonKhoiLuongGiangDay _source;
        public DeNghiThanhToan_ChonDuLieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyDeNghi_DetailView";
        }

        void DeNghiThanhToan_ChonDuLieu_Controller_Activated(object sender, System.EventArgs e)
        {
            popDongBo.Active["TruyCap"] = true;
        }

        private void popDongBo_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(dsChonKhoiLuongGiangDay));

            QuanLyDeNghi obj = View.CurrentObject as QuanLyDeNghi;

            if (obj != null)
            {
                CriteriaOperator fchitiet = CriteriaOperator.Parse("NamHoc = ? and GCRecord IS NULL", obj.NamHoc.Oid);
                BangChotThuLao bangchot = null;
                bangchot = ses.FindObject<BangChotThuLao>(fchitiet);
                if (bangchot != null)
                {
                    if (bangchot.Khoa)
                    {
                        using (DialogUtil.AutoWait("Load danh sách giảng viên"))
                        {
                            collectionSource = new CollectionSource(_obs, typeof(dsChonKhoiLuongGiangDay));
                            _source = new ChonKhoiLuongGiangDay(ses);
                            _source.NamHoc = ses.GetObjectByKey<NamHoc>(obj.NamHoc.Oid);
                            e.View = Application.CreateDetailView(_obs, _source);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Bảng chốt chưa khóa thông tin có thể thay đổi, vui lòng khóa bản chốt!");
                        return;
                    }
                }
                else
                {
                    XtraMessageBox.Show("Chưa có bảng chốt không thể lấy thông tin!");
                    return;
                }
            }
        }
        private void popDongBo_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            _obs = View.ObjectSpace;
            Session ses = ((XPObjectSpace)_obs).Session;
            QuanLyDeNghi obj = View.CurrentObject as QuanLyDeNghi;
            if (obj != null)
            {
                View.ObjectSpace.CommitChanges();
                string listNhanVien = "";
                using (DialogUtil.AutoWait("Đang đồng bộ dữ liệu"))
                {
                    foreach (dsChonKhoiLuongGiangDay item in _source.listKhoiLuong)
                    {
                        if(item.Chon)
                        {
                            listNhanVien += item.ChiTietKhoiLuongGiangDay.Oid.ToString() + ";";
                        }                      
                    }

                    //if (obj.ListChiTietKhoiLuongGiangDay != null)
                    //{

                    //    SqlParameter[] param = new SqlParameter[3];
                    //    param[0] = new SqlParameter("@KhoiLuongGiangDay", obj.Oid);
                    //    param[1] = new SqlParameter("@listNhanVien", listNhanVien);
                    //    param[2] = new SqlParameter("@TatCa", _source.TatCa);
                    //    DataProvider.ExecuteNonQuery("spd_PMS_XoaDuLieu_KhoiLuongGiangDay", CommandType.StoredProcedure, param);
                    //}

                    SqlParameter[] pDongBo = new SqlParameter[2];
                    pDongBo[0] = new SqlParameter("@ChiTietKhoiLuongGiangDay", listNhanVien);
                    pDongBo[1] = new SqlParameter("@QuanLyDeNghi", obj.Oid);
                    DataProvider.ExecuteNonQuery("spd_PMS_SoDeNghi", CommandType.StoredProcedure, pDongBo);                 
                    View.ObjectSpace.Refresh();

                }
            }
        }
    }
}