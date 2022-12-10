using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using PSC_HRM.Module.XuLyQuyTrinh;
using PSC_HRM.Module.Win.Forms;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;
using PSC_HRM.Module.ThoiViec;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    public partial class QuyTrinhChuyenCongTacController : QuyTrinhBaseController
    {
        private QuanLyThoiViec _QuanLy;

        public QuyTrinhChuyenCongTacController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình chuyển công tác");
        }

        private void QuyTrinhChuyenNgachController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhChuyenCongTac);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLy = ObjectSpace.GetObjectByKey<QuanLyThoiViec>(obj);
                if (_QuanLy != null)
                {
                    StartQuyTrinh();
                    SetNotification("Đang chạy quy trình chuyển công tác năm học " + _QuanLy.NamHoc.TenNamHoc);
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
                frmPopUp<ChonDuLieuController<QuanLyThoiViec>> popup = new frmPopUp<ChonDuLieuController<QuanLyThoiViec>>(Application, ObjectSpace, new ChonDuLieuController<QuanLyThoiViec>(Application, ObjectSpace, "Quản lý chuyển ngạch", CriteriaOperator.Parse(""), new string[] { "NamHoc.TenNamHoc" }, new string[] { "Năm học" }, new int[] { 150 }), "Chọn quản lý chuyển chuyển công tác", true);
                if (popup.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    _QuanLy = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLy))
                    {
                        StartQuyTrinh();
                        SetNotification("Đang chạy quy trình chuyển công tác năm học " + _QuanLy.NamHoc.TenNamHoc);
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnLapQDChuyenCongTac_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhChuyenCongTac>())
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
                        QuyetDinhChuyenCongTac quyetDinh = ObjectSpace.CreateObject<QuyetDinhChuyenCongTac>();
                        quyetDinh.BoPhan = nhanVien.BoPhan;
                        quyetDinh.ThongTinNhanVien = nhanVien;

                        Application.ShowView<QuyetDinhChuyenCongTac>(ObjectSpace, quyetDinh);

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapQDChuyenCongTac.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnGiayThoiTraLuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhChuyenCongTac>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonQuyetDinhChuyenCongTacController> popup = new frmPopUp<ChonQuyetDinhChuyenCongTacController>(Application, ObjectSpace, new ChonQuyetDinhChuyenCongTacController(Application, ObjectSpace), "Chọn cán bộ", true);
                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    QuyetDinhChuyenCongTac quyetDinh = popup.CurrentControl.GetData();
                    if (quyetDinh != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        quyetDinh = ObjectSpace.GetObjectByKey<QuyetDinhChuyenCongTac>(quyetDinh.Oid);
                        if (quyetDinh != null)
                        {
                            var list = new List<QuyetDinhChuyenCongTac>() { quyetDinh };
                            MailMerge_GiayThoiTraLuong mailMerge = new MailMerge_GiayThoiTraLuong();
                            mailMerge.Merge(ObjectSpace, list);

                            ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnGiayThoiTraLuong.Caption);
                        }
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
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                ObjectSpace = Application.CreateObjectSpace();
                _QuanLy = ObjectSpace.GetObjectByKey<QuanLyThoiViec>(obj);
                if (_QuanLy != null)
                {
                    Application.ShowView<QuanLyThoiViec>(ObjectSpace, _QuanLy);
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
            helper.XuLy("QuyTrinhChuyenCongTac", "Quy trình chuyển công tác");
        }

        private void StartQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
            btnLapQDChuyenCongTac.State = CustomButton.ButtonState.Normal;
            btnGiayThoiTraLuong.State = CustomButton.ButtonState.Normal;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            btnLapQDChuyenCongTac.State = CustomButton.ButtonState.Disable;
            btnGiayThoiTraLuong.State = CustomButton.ButtonState.Disable;
        }
    }
}
