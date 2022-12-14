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
using System.Windows.Forms;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class HopDong_ThanhLy_ThinhGiang_ChonDuLieu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        ChonKhoiLuongGiangDay _source;
        public HopDong_ThanhLy_ThinhGiang_ChonDuLieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "HopDong_ThanhLy_ThinhGiang_ListView";
        }

        void HopDong_ThanhLy_ThinhGiang_ChonDuLieu_Controller_Activated(object sender, System.EventArgs e)
        {
            popDongBo.Active["TruyCap"] = true;
        }

        private void popDongBo_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;

            using (DialogUtil.AutoWait("Load dữ liệu"))
            {
                _source = new ChonKhoiLuongGiangDay(ses);
                _source.AnHien = true;
                e.View = Application.CreateDetailView(_obs, _source);
            }
        }
        private void popDongBo_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            _obs = View.ObjectSpace;
            Session ses = ((XPObjectSpace)_obs).Session;
            CriteriaOperator fbangchot = CriteriaOperator.Parse("NamHoc = ? and GCRecord IS NULL", _source.NamHoc.Oid);
            BangChotThuLao bangchot = null;
            bangchot = ses.FindObject<BangChotThuLao>(fbangchot);
            if (bangchot != null)
            {
                if(bangchot.Khoa)
                {
                    SqlParameter[] pDongBo = new SqlParameter[1];
                    pDongBo[0] = new SqlParameter("@BangChotThuLao", bangchot.Oid);
                    DataProvider.ExecuteNonQuery("spd_PMS_ThanhLy_HopDong_ThinhGiang", CommandType.StoredProcedure, pDongBo);
                    View.ObjectSpace.Refresh();
                }
                else
                {
                    MessageBox.Show("Bảng chốt chưa khóa thông tin có thể thay đổi, vui lòng khóa bản chốt!");
                }
            }
            else
            {
                MessageBox.Show("Chưa có bảng chốt không thể lấy thông tin!");
            }
        }
    }
}