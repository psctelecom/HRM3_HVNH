using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using PSC_HRM.Module.Win.Forms;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BanLamViec;
using System.Data.SqlClient;
using PSC_HRM.Module.XuLyQuyTrinh.NghiKhongHuongLuong;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.NghiKhongHuongLuong
{
    public partial class QuyTrinhNghiKhongHuongLuongController : QuyTrinhBaseController
    {
        public QuyTrinhNghiKhongHuongLuongController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình nghỉ không hưởng lương");
        }

        private void QuyTrinhNghiHuuController_Load(object sender, EventArgs e)
        {
            StartQuyTrinh();
        }

        protected override void btnBatDau_Click(object sender, EventArgs e)
        {
            StartQuyTrinh();
        }

        private void btnLapQDNghiKhongHuongLuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhNghiKhongHuongLuong>())
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
                        QuyetDinhNghiKhongHuongLuong quyetDinh = ObjectSpace.CreateObject<QuyetDinhNghiKhongHuongLuong>();
                        quyetDinh.BoPhan = nhanVien.BoPhan;
                        quyetDinh.ThongTinNhanVien = nhanVien;

                        Application.ShowView<QuyetDinhNghiKhongHuongLuong>(ObjectSpace, quyetDinh);

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapQDNghiKhongHuongLuong.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnTheoDoiNghiKhongHuongLuong_Click(object sender, EventArgs e)
        {
            ObjectSpace = Application.CreateObjectSpace();
            frmPopUp<TheoDoiNghiKhongHuongLuongController> popup = new frmPopUp<TheoDoiNghiKhongHuongLuongController>(Application, ObjectSpace, new TheoDoiNghiKhongHuongLuongController(Application, ObjectSpace), "Theo dõi nghỉ không hưởng lương", false);
            popup.Size = new System.Drawing.Size(601, 471);
            popup.Show();
            ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnTheoDoiNghiKhongHuongLuong.Caption);
        }

        private void btnLapQDTiepNhan_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhTiepNhan>())
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
                        QuyetDinhTiepNhan quyetDinh = ObjectSpace.CreateObject<QuyetDinhTiepNhan>();
                        quyetDinh.BoPhan = nhanVien.BoPhan;
                        quyetDinh.ThongTinNhanVien = nhanVien;

                        Application.ShowView<QuyetDinhTiepNhan>(ObjectSpace, quyetDinh);

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapQDTiepNhan.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            StartQuyTrinh();
        }

        protected override void btnHelp_Click(object sender, EventArgs e)
        {
            frmHelper helper = new frmHelper();
            helper.XuLy("QuyTrinhNghiKhongHuongLuong", "Quy trình nghỉ không hưởng lương");
        }

        protected override void btnOpen_Click(object sender, EventArgs e)
        { }

        private void StartQuyTrinh()
        {
            btnOpen.Visible = false;
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnLapQDTiepNhan.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
            btnTheoDoiNghiKhongHuongLuong.State = CustomButton.ButtonState.Normal;
            btnLapQDNghiKhongHuongLuong.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnLapQDTiepNhan.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            btnTheoDoiNghiKhongHuongLuong.State = CustomButton.ButtonState.Disable;
            btnLapQDNghiKhongHuongLuong.State = CustomButton.ButtonState.Disable;
        }

        private IEnumerable<ThongTinNghiKhongHuongLuong> GetNhanVienList()
        {
            ObjectSpace = Application.CreateObjectSpace();

            DateTime current = HamDungChung.GetServerTime();
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@TuNgay", current.SetTime(SetTimeEnum.StartYear)),
                new SqlParameter("@DenNgay", current.SetTime(SetTimeEnum.EndMonth)) 
            };
            var data = SystemContainer.Resolver<INhacViec<ThongTinNghiKhongHuongLuong>>().GetData(ObjectSpace, param);

            return (IEnumerable<ThongTinNghiKhongHuongLuong>)data;
        }
    }
}
