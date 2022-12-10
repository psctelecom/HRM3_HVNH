using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects;
using System.Data.SqlClient;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class HoSo_QuyetDinh_NhanVien_Controller : ViewController
    {
        private IObjectSpace _obs = null;
        private ThongTinNhanVien _ThongTinNhanVien;
        private Session _ses = null;
        private HoSo_QuyetDinh_NhanVien_ChucVu _ChucVu;
        private HoSo_QuyetDinh_NhanVien_CongTac _CongTac;
        private HoSo_QuyetDinh_NhanVien_ChucDanh _ChucDanh;
        private HoSo_QuyetDinh_NhanVien_HocHam _HocHam;
        public HoSo_QuyetDinh_NhanVien_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        void HoSo_QuyetDinh_NhanVien_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "VHU" ||TruongConfig.MaTruong=="Demo")
                SingleChoiceAction1.Active["TruyCap"] = true;
        }

        private void HoSo_QuyetDinh_NhanVien_Controller_ViewControlsCreated(object sender, EventArgs e)
        {
            _ThongTinNhanVien = View.CurrentObject as ThongTinNhanVien;
            if (_ThongTinNhanVien != null)
            {
                SingleChoiceAction1.Items.Clear();
                ChoiceActionItem subItem;
                //
                if (TruongConfig.MaTruong == "VHU" || TruongConfig.MaTruong == "Demo")
                {
                    {
                        subItem = new ChoiceActionItem();
                        subItem.Id = "HoSo_QuyetDinh_NhanVien_ChucVu_Controller";
                        subItem.Caption = "Chức vụ";
                        SingleChoiceAction1.Items.Add(subItem);
                    }

                    {
                        subItem = new ChoiceActionItem();
                        subItem.Id = "HoSo_QuyetDinh_NhanVien_CongTac_Controller";
                        subItem.Caption = "Loại giảng viên";
                        SingleChoiceAction1.Items.Add(subItem);
                    }

                    {
                        subItem = new ChoiceActionItem();
                        subItem.Id = "HoSo_QuyetDinh_NhanVien_ChucDanh_Controller";
                        subItem.Caption = "Chức danh";
                        SingleChoiceAction1.Items.Add(subItem);
                    }

                    {
                        subItem = new ChoiceActionItem();
                        subItem.Id = "HoSo_QuyetDinh_NhanVien_HocHam_Controller";
                        subItem.Caption = "Học hàm";
                        SingleChoiceAction1.Items.Add(subItem);
                    }
                }
            }
        }
        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {

            if (e.SelectedChoiceActionItem.Id == "HoSo_QuyetDinh_NhanVien_ChucVu_Controller")
            {
                _obs = Application.CreateObjectSpace();
                _ThongTinNhanVien = View.CurrentObject as ThongTinNhanVien;
                if (_ThongTinNhanVien != null)
                {
                    _obs = Application.CreateObjectSpace();
                    _ses = ((XPObjectSpace)_obs).Session;
                    _ChucVu = _obs.CreateObject<HoSo_QuyetDinh_NhanVien_ChucVu>();
                    _ChucVu.NhanVien = _ses.GetObjectByKey<NhanVien>(_ThongTinNhanVien.Oid);
                    e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, _ChucVu);
                    e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                    var ctrl = new DevExpress.ExpressApp.SystemModule.DialogController();
                    e.ShowViewParameters.Controllers.Add(ctrl);
                    ctrl.AcceptAction.Execute += HoSo_QuyetDinh_NhanVien_ChucVu;
                    ctrl.AcceptAction.Caption = "Đồng ý";
                }
            }

            if (e.SelectedChoiceActionItem.Id == "HoSo_QuyetDinh_NhanVien_CongTac_Controller")
            {
                _obs = Application.CreateObjectSpace();
                _ThongTinNhanVien = View.CurrentObject as ThongTinNhanVien;
                if (_ThongTinNhanVien != null)
                {
                    _obs = Application.CreateObjectSpace();
                    _ses = ((XPObjectSpace)_obs).Session;
                    _CongTac = _obs.CreateObject<HoSo_QuyetDinh_NhanVien_CongTac>();
                    _CongTac.NhanVien = _ses.GetObjectByKey<NhanVien>(_ThongTinNhanVien.Oid);
                    e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, _CongTac);
                    e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                    var ctrl = new DevExpress.ExpressApp.SystemModule.DialogController();
                    e.ShowViewParameters.Controllers.Add(ctrl);
                    ctrl.AcceptAction.Execute += HoSo_QuyetDinh_NhanVien_CongTac;
                    ctrl.AcceptAction.Caption = "Đồng ý";
                }
            }

            if (e.SelectedChoiceActionItem.Id == "HoSo_QuyetDinh_NhanVien_ChucDanh_Controller")
            {
                _obs = Application.CreateObjectSpace();
                _ThongTinNhanVien = View.CurrentObject as ThongTinNhanVien;
                if (_ThongTinNhanVien != null)
                {
                    _obs = Application.CreateObjectSpace();
                    _ses = ((XPObjectSpace)_obs).Session;
                    _ChucDanh = _obs.CreateObject<HoSo_QuyetDinh_NhanVien_ChucDanh>();
                    _ChucDanh.NhanVien = _ses.GetObjectByKey<NhanVien>(_ThongTinNhanVien.Oid);
                    e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, _ChucDanh);
                    e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                    var ctrl = new DevExpress.ExpressApp.SystemModule.DialogController();
                    e.ShowViewParameters.Controllers.Add(ctrl);
                    ctrl.AcceptAction.Execute += HoSo_QuyetDinh_NhanVien_ChucDanh;
                    ctrl.AcceptAction.Caption = "Đồng ý";
                }
            }

            if (e.SelectedChoiceActionItem.Id == "HoSo_QuyetDinh_NhanVien_HocHam_Controller")
            {
                _obs = Application.CreateObjectSpace();
                _ThongTinNhanVien = View.CurrentObject as ThongTinNhanVien;
                if (_ThongTinNhanVien != null)
                {
                    _obs = Application.CreateObjectSpace();
                    _ses = ((XPObjectSpace)_obs).Session;
                    _HocHam = _obs.CreateObject<HoSo_QuyetDinh_NhanVien_HocHam>();
                    _HocHam.NhanVien = _ses.GetObjectByKey<NhanVien>(_ThongTinNhanVien.Oid);
                    e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, _HocHam);
                    e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                    var ctrl = new DevExpress.ExpressApp.SystemModule.DialogController();
                    e.ShowViewParameters.Controllers.Add(ctrl);
                    ctrl.AcceptAction.Execute += HoSo_QuyetDinh_NhanVien_HocHam;
                    ctrl.AcceptAction.Caption = "Đồng ý";
                }
            }
        }

        private void HoSo_QuyetDinh_NhanVien_ChucVu(object sender, SimpleActionExecuteEventArgs e)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@NhanVien", _ChucVu.NhanVien.Oid);
            param[1] = new SqlParameter("@Oid_Moi", _ChucVu.ChucVu.Oid);
            param[2] = new SqlParameter("@NgayQuyetDinh", _ChucVu.NgayQuyetDinh.Date);
            param[3] = new SqlParameter("@NgayHetHieuLuc", _ChucVu.NgayHetHieuLuc.Date == DateTime.MinValue.Date? DateTime.MaxValue.Date: _ChucVu.NgayHetHieuLuc.Date);
            param[4] = new SqlParameter("@Loai", 1);
            param[5] = new SqlParameter("@SoQuyetDinh", _ChucVu.SoQuyetDinh); 
            DataProvider.ExecuteNonQuery("spd_PMS_QuyetDinhThayDoiChucDanh", System.Data.CommandType.StoredProcedure, param);
        }

        private void HoSo_QuyetDinh_NhanVien_CongTac(object sender, SimpleActionExecuteEventArgs e)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@NhanVien", _CongTac.NhanVien.Oid);
            param[1] = new SqlParameter("@Oid_Moi", _CongTac.LoaiNhanVien.Oid);
            param[2] = new SqlParameter("@NgayQuyetDinh", _CongTac.NgayQuyetDinh.Date);
            param[3] = new SqlParameter("@NgayHetHieuLuc", DateTime.MaxValue.Date);
            param[4] = new SqlParameter("@Loai", 2);
            param[5] = new SqlParameter("@SoQuyetDinh", _CongTac.SoQuyetDinh);
            DataProvider.ExecuteNonQuery("spd_PMS_QuyetDinhThayDoiChucDanh", System.Data.CommandType.StoredProcedure, param);
        }

        private void HoSo_QuyetDinh_NhanVien_ChucDanh(object sender, SimpleActionExecuteEventArgs e)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@NhanVien", _ChucDanh.NhanVien.Oid);
            param[1] = new SqlParameter("@Oid_Moi", _ChucDanh.ChucDanh.Oid);
            param[2] = new SqlParameter("@NgayQuyetDinh", _ChucDanh.NgayQuyetDinh.Date);
            param[3] = new SqlParameter("@NgayHetHieuLuc", DateTime.MaxValue.Date);
            param[4] = new SqlParameter("@Loai", 3);
            param[5] = new SqlParameter("@SoQuyetDinh", _ChucDanh.SoQuyetDinh);
            DataProvider.ExecuteNonQuery("spd_PMS_QuyetDinhThayDoiChucDanh", System.Data.CommandType.StoredProcedure, param);
        }

        private void HoSo_QuyetDinh_NhanVien_HocHam(object sender, SimpleActionExecuteEventArgs e)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@NhanVien", _HocHam.NhanVien.Oid);
            param[1] = new SqlParameter("@Oid_Moi", _HocHam.HocHam.Oid);
            param[2] = new SqlParameter("@NgayQuyetDinh", _HocHam.NgayCongNhan.Date);
            param[3] = new SqlParameter("@Loai", 4);
            DataProvider.ExecuteNonQuery("spd_PMS_QuyetDinhThayDoiHocHam", System.Data.CommandType.StoredProcedure, param);
            View.ObjectSpace.Refresh();
        }
    }
}
