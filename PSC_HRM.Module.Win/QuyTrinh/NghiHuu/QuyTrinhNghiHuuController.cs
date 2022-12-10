using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using PSC_HRM.Module.Win.Forms;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BanLamViec;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;
using PSC_HRM.Module.XuLyQuyTrinh.NghiHuu;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.NghiHuu
{
    public partial class QuyTrinhNghiHuuController : QuyTrinhBaseController
    {
        public QuyTrinhNghiHuuController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình nghỉ hưu");
        }

        private void QuyTrinhNghiHuuController_Load(object sender, EventArgs e)
        {
            StartQuyTrinh();
        }

        protected override void btnBatDau_Click(object sender, EventArgs e)
        {
            StartQuyTrinh();
        }

        private void btnTheoDoiNghiHuu_Click(object sender, EventArgs e)
        {
            ObjectSpace = Application.CreateObjectSpace();
            frmPopUp<TheoDoiNghiHuuController> popup = new frmPopUp<TheoDoiNghiHuuController>(Application, ObjectSpace, new TheoDoiNghiHuuController(Application, ObjectSpace), "Theo dõi nghỉ hưu", false);
            popup.Size = new System.Drawing.Size(601, 471);
            popup.Show();
            ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnTheoDoiNghiHuu.Caption);
        }

        private void btnQDKeoDaiThoiGianCongTac_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhKeoDaiThoiGianCongTac>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetNhanVienList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        QuyetDinhKeoDaiThoiGianCongTac quyetDinh = ObjectSpace.CreateObject<QuyetDinhKeoDaiThoiGianCongTac>();
                        quyetDinh.BoPhan = nhanVien.BoPhan;
                        quyetDinh.ThongTinNhanVien = nhanVien;

                        Application.ShowView<QuyetDinhKeoDaiThoiGianCongTac>(ObjectSpace, quyetDinh);

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDKeoDaiThoiGianCongTac.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnGuiHoSoChoBaoHiem_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<BienDong_GiamLaoDong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<GuiHoSoBaoHiemController> popup = new frmPopUp<GuiHoSoBaoHiemController>(Application, ObjectSpace, new GuiHoSoBaoHiemController(ObjectSpace, GetNhanVienList()), "Chọn quản lý biến động", true);
                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    QuanLyBienDong quanLy = popup.CurrentControl.GetQuanLyBienDong();
                    if (nhanVien != null && quanLy != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        BienDong_GiamLaoDong bienDong = ObjectSpace.CreateObject<BienDong_GiamLaoDong>();
                        bienDong.QuanLyBienDong = ObjectSpace.GetObjectByKey<QuanLyBienDong>(quanLy.Oid);
                        bienDong.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                        bienDong.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        bienDong.LyDo = LyDoNghiEnum.ThoiViec;
                        if (nhanVien.NgayNghiHuu != DateTime.MinValue)
                            bienDong.TuNgay = nhanVien.NgayNghiHuu;
                        else
                        {
                            TuoiNghiHuu tuoiNghiHuu = ObjectSpace.FindObject<TuoiNghiHuu>(CriteriaOperator.Parse("GioiTinh=?", (byte)nhanVien.GioiTinh));
                            if (tuoiNghiHuu != null)
                            {
                                bienDong.TuNgay = nhanVien.NgaySinh.AddYears(tuoiNghiHuu.Tuoi);
                            }
                        }

                        Application.ShowView<BienDong_GiamLaoDong>(ObjectSpace, bienDong);

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnGuiHoSoChoBaoHiem.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnThongBaoNghiHuu_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<ThongTinNghiHuu>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetNhanVienList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);

                        ThongTinNghiHuu thongTinNghiHuu = ObjectSpace.CreateObject<ThongTinNghiHuu>();
                        thongTinNghiHuu.ThongTinNhanVien = nhanVien;

                        var list = new List<ThongTinNghiHuu>() { thongTinNghiHuu };
                        MailMerge_ThongBaoNghiHuu mailMerge = new MailMerge_ThongBaoNghiHuu();
                        mailMerge.Merge(ObjectSpace, list);

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnThongBaoNghiHuu.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDNghiHuu_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhNghiHuu>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetNhanVienList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        QuyetDinhNghiHuu quyetDinh = ObjectSpace.CreateObject<QuyetDinhNghiHuu>();
                        quyetDinh.BoPhan = nhanVien.BoPhan;
                        quyetDinh.ThongTinNhanVien = nhanVien;

                        Application.ShowView<QuyetDinhNghiHuu>(ObjectSpace, quyetDinh);
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDNghiHuu.Caption);
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
            helper.XuLy("QuyTrinhNghiHuu", "Quy trình nghỉ hưu");
        }

        protected override void btnOpen_Click(object sender, EventArgs e)
        {

        }

        private void StartQuyTrinh()
        {
            btnOpen.Visible = false;
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnGuiHoSoChoBaoHiem.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
            btnQDKeoDaiThoiGianCongTac.State = CustomButton.ButtonState.Normal;
            btnQDNghiHuu.State = CustomButton.ButtonState.Normal;
            btnTheoDoiNghiHuu.State = CustomButton.ButtonState.Normal;
            btnThongBaoNghiHuu.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnGuiHoSoChoBaoHiem.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            btnQDKeoDaiThoiGianCongTac.State = CustomButton.ButtonState.Disable;
            btnQDNghiHuu.State = CustomButton.ButtonState.Disable;
            btnTheoDoiNghiHuu.State = CustomButton.ButtonState.Disable;
            btnThongBaoNghiHuu.State = CustomButton.ButtonState.Disable;
        }

        private List<Guid> GetNhanVienList()
        {
            ObjectSpace = Application.CreateObjectSpace();

            DateTime current = HamDungChung.GetServerTime();
            current = current.SetTime(SetTimeEnum.EndMonth).AddMonths(1);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DenNgay", current);
            param[1] = new SqlParameter("@ThongTinTruong", System.Data.SqlDbType.UniqueIdentifier);
            param[1].Value = HamDungChung.ThongTinTruong(((XPObjectSpace)ObjectSpace).Session).Oid;
            var data = SystemContainer.Resolver<INhacViec<ThongTinNghiHuu>>().GetData(ObjectSpace, param);


            var temp = (from a in data
                        select a.ThongTinNhanVien.Oid).ToList<Guid>();

            return temp;
        }
    }
}
