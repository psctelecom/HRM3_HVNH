using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.XuLyQuyTrinh.TapSu;
using DevExpress.XtraEditors;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class TapSu_HetHanTamHoanTapSuController : ViewController
    {
        public TapSu_HetHanTamHoanTapSuController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TapSu_HetHanTamHoanTapSuController");
        }

        private void TapSu_QuyetDinhCongNhanHetHanTapSuController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ThongTinHetHanTapSu>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            ThongTinHetHanTamHoanTapSu obj = View.CurrentObject as ThongTinHetHanTamHoanTapSu;
            if (obj != null
                && obj.ThongTinNhanVien != null
                && obj.CanBoHuongDan != null)
            {
                if (XtraMessageBox.Show("Bạn có chắc chắn muốn cấp nhật:\n\r- Tình trạng cán bộ tập sự thành Đang làm việc.\n\r- Cán bộ hướng dẫn được hưởng phụ cấp trách nhiệm.", "Thông báo", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    ThongTinNhanVien nhanVien = obs.GetObjectByKey<ThongTinNhanVien>(obj.ThongTinNhanVien.Oid);
                    nhanVien.TinhTrang = obs.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));

                    ThongTinNhanVien huongDan = obs.GetObjectByKey<ThongTinNhanVien>(obj.CanBoHuongDan.Oid);
                    huongDan.NhanVienThongTinLuong.HSPCTrachNhiem = obj.QuyetDinhHuongDanTapSu.HSPCTrachNhiem;

                    obs.CommitChanges();
                }
            }
        }
    }
}
