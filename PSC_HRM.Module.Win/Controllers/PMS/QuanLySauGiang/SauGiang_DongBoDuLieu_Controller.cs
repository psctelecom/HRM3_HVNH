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
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;
using PSC_HRM.Module.NonPersistent;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class SauGiang_DongBoDuLieu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        ChonNhanVien _source;
        public SauGiang_DongBoDuLieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyKeKhaiSauGiang_DetailView";
        }

        void SauGiang_DongBoDuLieu_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "HVNH")
                popDongBo.Active["TruyCap"] = false;
        }

        private void popDongBo_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(dsThongTinNhanVien));

            QuanLyKeKhaiSauGiang obj = View.CurrentObject as QuanLyKeKhaiSauGiang;
            if (obj != null)
                if (obj.BangChotThuLao != null)
                {
                    XtraMessageBox.Show("Đã chốt khối lượng - không thể đồng bộ!");
                    return;
                }
                else
                {
                    using (DialogUtil.AutoWait("Load danh sách giảng viên"))
                    {
                        collectionSource = new CollectionSource(_obs, typeof(ChonNhanVien));
                        _source = new ChonNhanVien(ses);
                        e.View = Application.CreateDetailView(_obs, _source);
                    }
                }
        }
        private void popDongBo_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            _obs = View.ObjectSpace;
            Session ses = ((XPObjectSpace)_obs).Session;
            QuanLyKeKhaiSauGiang obj = View.CurrentObject as QuanLyKeKhaiSauGiang;
            if (obj != null)
            {
                View.ObjectSpace.CommitChanges();
                string listNhanVien = "";
                using (DialogUtil.AutoWait("Đang đồng bộ dữ liệu"))
                {
                    if (_source.TatCa)
                        listNhanVien = "";
                    else
                        foreach (dsThongTinNhanVien item in _source.listNhanVien)
                        {
                            listNhanVien += item.OidThongTinNhanVien.ToString() + ";";
                        }


                    SqlParameter[] pDongBo = new SqlParameter[3];
                    pDongBo[0] = new SqlParameter("@QuanLyKeKhaiSauGiang", obj.Oid);
                    pDongBo[1] = new SqlParameter("@listNhanVien", listNhanVien);
                    pDongBo[2] = new SqlParameter("@TatCa", _source.TatCa);
                    object kq = DataProvider.GetValueFromDatabase("spd_PMS_DongBoDuLieu_KeKhaiSauGiang", CommandType.StoredProcedure, pDongBo);
                    if (kq != null)
                        XtraMessageBox.Show(kq.ToString(), "Thông báo!");
                    View.ObjectSpace.Refresh();

                }
            }
        }
    }
}