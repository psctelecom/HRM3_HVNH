using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DiNuocNgoai;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class DiNuocNgoai_LapQuyetDinhDiNuocNgoaiController : ViewController
    {
        private IObjectSpace obs;
        private DangKyDiNuocNgoai dangKy;
        private QuyetDinhDiNuocNgoai quyetDinh;

        public DiNuocNgoai_LapQuyetDinhDiNuocNgoaiController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DiNuocNgoai_LapQuyetDinhDiNuocNgoaiController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //lưu Đăng ký đi công tác
            View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            dangKy = View.CurrentObject as DangKyDiNuocNgoai;

            if (dangKy != null)
            {
                quyetDinh = obs.FindObject<QuyetDinhDiNuocNgoai>(CriteriaOperator.Parse("DangKyDiNuocNgoai=?", dangKy.Oid));
                if (quyetDinh == null)
                {
                    quyetDinh = obs.CreateObject<QuyetDinhDiNuocNgoai>();
                    quyetDinh.DangKyDiNuocNgoai = obs.GetObjectByKey<DangKyDiNuocNgoai>(dangKy.Oid);
                }

                //ChiTietQuyetDinhDiNuocNgoai chiTiet;
                //foreach (ChiTietDangKyDiNuocNgoai item in dangKy.ListChiTietDangKyDiNuocNgoai)
                //{
                //    chiTiet = obs.FindObject<ChiTietQuyetDinhDiNuocNgoai>(CriteriaOperator.Parse("QuyetDinhDiNuocNgoai=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                //    if (chiTiet == null)
                //    {
                //        chiTiet = obs.CreateObject<ChiTietQuyetDinhDiNuocNgoai>();
                //        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                //        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                //        quyetDinh.ListChiTietQuyetDinhDiNuocNgoai.Add(chiTiet);
                //    }
                //}

                e.Context = TemplateContext.View;
                e.View = Application.CreateDetailView(obs, quyetDinh);
                obs.Committing += obs_Committing;
            }
        }

        void obs_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void DanhGia_CopyBangQuyDoiThangController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] =
                HamDungChung.IsWriteGranted<QuanLyDiNuocNgoai>() &&
                HamDungChung.IsWriteGranted<DangKyDiNuocNgoai>() &&
                HamDungChung.IsWriteGranted<QuyetDinhDiCongTac>();
        }
    }
}
