using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Security;
using DevExpress.Utils;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using System.Windows.Forms;
using System.Data;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using System.Text;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DoanDang;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using PSC_HRM.Module.Controllers;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class HoSo_ImportQuaTrinhController : ViewController
    {
        private HoSo_ImportQuaTrinhNPO _obj;
        private IObjectSpace _obs;

        public HoSo_ImportQuaTrinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void HoSo_ImportQuaTrinhController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong == "DLU" || TruongConfig.MaTruong == "LUH" || TruongConfig.MaTruong == "BUH")
            {
                popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ThongTinNhanVien>();
            }
            else
                popupWindowShowAction1.Active["TruyCap"] = false;
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            _obj = _obs.CreateObject<HoSo_ImportQuaTrinhNPO>();
            e.View = Application.CreateDetailView(_obs, _obj);
        }

        private void popupWindowShowAction1_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
             using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        if (_obj.LoaiImportQuaTrinhEnum == LoaiImportQuaTrinhEnum.LichSuBanThan)
                        {
                            HoSo_ImportQuaTrinh.XuLy_ImportLichSuBanThan(View.ObjectSpace, dialog.FileName);
                        }
                        else if (_obj.LoaiImportQuaTrinhEnum == LoaiImportQuaTrinhEnum.DienBienLuong)
                        {
                            HoSo_ImportQuaTrinh.XuLy_ImportDienBienLuong(View.ObjectSpace, dialog.FileName);
                        }
                        else if (_obj.LoaiImportQuaTrinhEnum == LoaiImportQuaTrinhEnum.BoNhiem)
                        {
                            HoSo_ImportQuaTrinh.XuLy_ImportQuaTrinhBoNhiem(View.ObjectSpace, dialog.FileName);
                        }
                        else if (_obj.LoaiImportQuaTrinhEnum == LoaiImportQuaTrinhEnum.CongTac)
                        {
                            HoSo_ImportQuaTrinh.XuLy_ImportQuaTrinhCongTac(View.ObjectSpace, dialog.FileName);
                        }
                        else if (_obj.LoaiImportQuaTrinhEnum == LoaiImportQuaTrinhEnum.DaoTao)
                        {
                            HoSo_ImportQuaTrinh.XuLy_ImportQuaTrinhDaoTao(View.ObjectSpace, dialog.FileName);
                        }
                        else if (_obj.LoaiImportQuaTrinhEnum == LoaiImportQuaTrinhEnum.BoiDuong)
                        {
                            HoSo_ImportQuaTrinh.XuLy_ImportQuaTrinhBoiDuong(View.ObjectSpace, dialog.FileName);
                        }
                        else if (_obj.LoaiImportQuaTrinhEnum == LoaiImportQuaTrinhEnum.KhenThuong)
                        {
                            HoSo_ImportQuaTrinh.XuLy_ImportQuaTrinhKhenThuong(View.ObjectSpace, dialog.FileName);
                        }
                        else if (_obj.LoaiImportQuaTrinhEnum == LoaiImportQuaTrinhEnum.KyLuat)
                        {
                            HoSo_ImportQuaTrinh.XuLy_ImportQuaTrinhKyLuat(View.ObjectSpace, dialog.FileName);
                        }

                        View.ObjectSpace.Refresh();
                    }
                }
             }
        }

    }
}
