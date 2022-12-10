using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using PSC_HRM.Module.Win.Forms;
using PSC_HRM.Module.XuLyQuyTrinh;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.ThoiViec;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.ThoiViec
{
    public partial class QuyTrinhThoiViecController : QuyTrinhBaseController
    {
        private QuanLyThoiViec _QuanLyThoiViec;

        public QuyTrinhThoiViecController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình thôi việc");
        }

        private void QuyTrinhNghiHuuController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhThoiViec);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((DevExpress.ExpressApp.Xpo.XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLyThoiViec = ObjectSpace.GetObjectByKey<QuanLyThoiViec>(obj);
                if (_QuanLyThoiViec != null && _QuanLyThoiViec.NamHoc != null)
                {
                    StartQuyTrinh();
                    SetNotification("Đang chạy quy trình thôi việc năm học " + _QuanLyThoiViec.NamHoc.TenNamHoc);
                }
            }
            else
            {
                EndQuyTrinh();
                SetNotification("Nhấn nút 'Bắt đầu' để bắt đầu quy trình mới");
            }
        }

        protected override void btnBatDau_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyThoiViec>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuanLyThoiViec>> popup = new frmPopUp<ChonDuLieuController<QuanLyThoiViec>>(Application, ObjectSpace, new ChonDuLieuController<QuanLyThoiViec>(Application, ObjectSpace, "Quản lý thôi việc", CriteriaOperator.Parse(""), new string[] { "NamHoc.TenNamHoc" }, new string[] { "Năm học" }, new int[] { 150 }), "Chọn quản lý thôi việc", true);
                if (popup.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    _QuanLyThoiViec = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLyThoiViec))
                    {
                        StartQuyTrinh();
                        SetNotification("Đang chạy quy trình thôi việc năm học " + _QuanLyThoiViec.NamHoc.TenNamHoc);
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDThoiViec_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhThoiViec>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, null), "Chọn cán bộ", true);
                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        QuyetDinhThoiViec quyetDinh = ObjectSpace.CreateObject<QuyetDinhThoiViec>();
                        quyetDinh.BoPhan = nhanVien.BoPhan;
                        quyetDinh.ThongTinNhanVien = nhanVien;

                        Application.ShowView<QuyetDinhThoiViec>(ObjectSpace, quyetDinh);

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDThoiViec.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyThoiViec>())
            {
                ThucHienQuyTrinh.KetThuc(Application.CreateObjectSpace());
                EndQuyTrinh();
                SetNotification("Nhấn nút 'Bắt đầu' để bắt đầu quy trình mới");
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnOpen_Click(object sender, EventArgs e)
        {
            ObjectSpace = Application.CreateObjectSpace();
            Guid obj = ThucHienQuyTrinh.DaBatDau(((DevExpress.ExpressApp.Xpo.XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                ObjectSpace = Application.CreateObjectSpace();
                _QuanLyThoiViec = ObjectSpace.GetObjectByKey<QuanLyThoiViec>(obj);
                if (_QuanLyThoiViec != null)
                {
                    Application.ShowView<QuanLyThoiViec>(ObjectSpace, _QuanLyThoiViec);
                }
            }
            else
            {
                SetNotification("Hiện tại quy trình chưa được bắt đầu.");
            }
        }

        protected override void btnHelp_Click(object sender, EventArgs e)
        {
            frmHelper helper = new frmHelper();
            helper.XuLy("QuyTrinhThoiViec", "Quy trình thôi việc");
        }

        private void StartQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
            btnQDThoiViec.State = CustomButton.ButtonState.Normal;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            btnQDThoiViec.State = CustomButton.ButtonState.Disable;
        }
    }
}
