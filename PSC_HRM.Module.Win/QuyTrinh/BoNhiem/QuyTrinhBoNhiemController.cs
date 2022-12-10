using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp;
using PSC_HRM.Module.BoNhiem;
using PSC_HRM.Module.Win.Forms;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.XuLyQuyTrinh;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using System.Threading.Tasks;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using PSC_HRM.Module.BanLamViec;
using PSC_HRM.Module.XuLyQuyTrinh.BoNhiem;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Utils;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.BoNhiem
{
    public partial class QuyTrinhBoNhiemController : QuyTrinhBaseController
    {
        private QuanLyBoNhiem _QuanLyBoNhiem;

        public QuyTrinhBoNhiemController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình bổ nhiệm");
        }

        private void QuyTrinhBoNhiemController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhBoNhiem);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((DevExpress.ExpressApp.Xpo.XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLyBoNhiem = ObjectSpace.GetObjectByKey<QuanLyBoNhiem>(obj);
                if (_QuanLyBoNhiem != null && _QuanLyBoNhiem.NamHoc != null)
                {
                    StartQuyTrinh();
                    SetNotification("Đang chạy quy trình bổ nhiệm năm học " + _QuanLyBoNhiem.NamHoc.TenNamHoc);
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
            if (HamDungChung.IsWriteGranted<QuanLyBoNhiem>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuanLyBoNhiem>> popup = new frmPopUp<ChonDuLieuController<QuanLyBoNhiem>>(Application, ObjectSpace, new ChonDuLieuController<QuanLyBoNhiem>(Application, ObjectSpace, "Quản lý bổ nhiệm", CriteriaOperator.Parse(""), new string[] { "NamHoc.TenNamHoc" }, new string[] { "Năm học" }, new int[] { 150 }), "Chọn quản lý bổ nhiệm", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    _QuanLyBoNhiem = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLyBoNhiem))
                    {
                        StartQuyTrinh();
                        SetNotification("Đang chạy quy trình bổ nhiệm năm học " + _QuanLyBoNhiem.NamHoc.TenNamHoc);
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnThoiDoiHetNhiemKy_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyBoNhiem>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<TheoDoiHetNhiemKyController> popup = new frmPopUp<TheoDoiHetNhiemKyController>(Application, ObjectSpace, new TheoDoiHetNhiemKyController(Application, ObjectSpace), "Theo dõi bổ nhiệm", false);
                popup.Size = new Size(601, 471);
                popup.Show();
                ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnThoiDoiHetNhiemKy.Caption);
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDMienNhiem_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhMienNhiem>() &&
                HamDungChung.IsWriteGranted<QuyetDinhMienNhiemKiemNhiem>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<ThongTinBoNhiem>> popup = new frmPopUp<ChonDuLieuController<ThongTinBoNhiem>>(Application, ObjectSpace, new ChonDuLieuController<ThongTinBoNhiem>("Cán bộ", GetHetNhiemKyList(), new string[] { "ThongTinNhanVien.HoTen", "ChucVu.TenChucVu" }, new string[] { "Họ tên", "Chức vụ" }, new int[] { 100, 80 }), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ThongTinBoNhiem boNhiem = popup.CurrentControl.GetData();
                    if (boNhiem != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        if (boNhiem.KiemNhiem)
                        {
                            QuyetDinhMienNhiemKiemNhiem quyetDinh = ObjectSpace.FindObject<QuyetDinhMienNhiemKiemNhiem>(CriteriaOperator.Parse("QuyetDinhBoNhiemKiemNhiem=?", boNhiem.QuyetDinh.Oid));
                            if (quyetDinh == null)
                            {
                                quyetDinh = ObjectSpace.CreateObject<QuyetDinhMienNhiemKiemNhiem>();
                                quyetDinh.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(boNhiem.BoPhan.Oid);
                                quyetDinh.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(boNhiem.ThongTinNhanVien.Oid);
                                if (boNhiem.QuyetDinh != null)
                                    quyetDinh.QuyetDinhBoNhiemKiemNhiem = ObjectSpace.GetObjectByKey<QuyetDinhBoNhiemKiemNhiem>(boNhiem.QuyetDinh.Oid);
                            }
                            Application.ShowView<QuyetDinhMienNhiemKiemNhiem>(ObjectSpace, quyetDinh);
                        }
                        else
                        {
                            QuyetDinhMienNhiem quyetDinh = ObjectSpace.FindObject<QuyetDinhMienNhiem>(CriteriaOperator.Parse("QuyetDinhBoNhiem=?", boNhiem.QuyetDinh.Oid));
                            if (quyetDinh == null)
                            {
                                quyetDinh = ObjectSpace.CreateObject<QuyetDinhMienNhiem>();
                                quyetDinh.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(boNhiem.BoPhan.Oid);
                                quyetDinh.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(boNhiem.ThongTinNhanVien.Oid);
                                if (boNhiem.QuyetDinh != null)
                                    quyetDinh.QuyetDinhBoNhiem = ObjectSpace.GetObjectByKey<QuyetDinhBoNhiem>(boNhiem.QuyetDinh.Oid);
                            }
                            Application.ShowView<QuyetDinhMienNhiem>(ObjectSpace, quyetDinh);
                        }
                        SetNotification("Lập QĐ miễn nhiệm thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDMienNhiem.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnLapDeNghiBoNhiemLai_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<DeNghiBoNhiem>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<ThongTinBoNhiem>> popup = new frmPopUp<ChonDuLieuController<ThongTinBoNhiem>>(Application, ObjectSpace, new ChonDuLieuController<ThongTinBoNhiem>("Cán bộ", GetHetNhiemKyList(), new string[] { "ThongTinNhanVien.HoTen", "ChucVu.TenChucVu" }, new string[] { "Họ tên", "Chức vụ" }, new int[] { 100, 80 }), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ThongTinBoNhiem boNhiem = popup.CurrentControl.GetData();
                    if (boNhiem != null
                        && boNhiem.ThongTinNhanVien != null
                        && boNhiem.ChucVu != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        DeNghiBoNhiem deNghi = ObjectSpace.FindObject<DeNghiBoNhiem>(CriteriaOperator.Parse("QuanLyBoNhiem=? and ThongTinNhanVien=? and ChucVu=?", _QuanLyBoNhiem.Oid, boNhiem.ThongTinNhanVien.Oid, boNhiem.ChucVu.Oid));
                        if (deNghi == null)
                        {
                            deNghi = ObjectSpace.CreateObject<DeNghiBoNhiem>();
                            deNghi.QuanLyBoNhiem = ObjectSpace.GetObjectByKey<QuanLyBoNhiem>(_QuanLyBoNhiem.Oid);
                            deNghi.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(boNhiem.BoPhan.Oid);
                            deNghi.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(boNhiem.ThongTinNhanVien.Oid);
                            if (boNhiem.ChucVu != null)
                                deNghi.ChucVu = ObjectSpace.GetObjectByKey<ChucVu>(boNhiem.ChucVu.Oid);
                            deNghi.KiemNhiem = boNhiem.KiemNhiem;
                            if (boNhiem.TaiBoPhan != null)
                                deNghi.TaiBoPhan = ObjectSpace.GetObjectByKey<BoPhan>(boNhiem.TaiBoPhan.Oid);
                        }
                        Application.ShowModelView<DeNghiBoNhiem>(ObjectSpace, deNghi);
                        SetNotification("Lập đề nghị bổ nhiệm lại thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapDeNghiBoNhiemLai.Caption);
                    }
                }
            }
        }

        private void btnLapDeNghiBoNhiemMoi_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<DeNghiBoNhiem>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, null), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        DeNghiBoNhiem deNghi = ObjectSpace.FindObject<DeNghiBoNhiem>(CriteriaOperator.Parse("QuanLyBoNhiem=? and ThongTinNhanVien=?", _QuanLyBoNhiem.Oid, nhanVien.Oid));
                        if (deNghi == null)
                        {
                            deNghi = ObjectSpace.CreateObject<DeNghiBoNhiem>();
                            deNghi.QuanLyBoNhiem = ObjectSpace.GetObjectByKey<QuanLyBoNhiem>(_QuanLyBoNhiem.Oid);
                            deNghi.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                            deNghi.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                            deNghi.BoNhiemMoi = true;
                        }
                        Application.ShowModelView<DeNghiBoNhiem>(ObjectSpace, deNghi);
                        SetNotification("Lập đề nghị bổ nhiệm mới thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapDeNghiBoNhiemLai.Caption);
                    }
                }
            }
        }

        private void btnQDBoNhiem_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhMienNhiem>() &&
                HamDungChung.IsWriteGranted<QuyetDinhMienNhiemKiemNhiem>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<DeNghiBoNhiem>> popup = new frmPopUp<ChonDuLieuController<DeNghiBoNhiem>>(Application, ObjectSpace,
                    new ChonDuLieuController<DeNghiBoNhiem>(Application, ObjectSpace, "Chọn cán bộ",
                        CriteriaOperator.Parse("QuanLyBoNhiem=?", _QuanLyBoNhiem.Oid), new string[] { "ThongTinNhanVien.HoTen", "ChucVu.TenChucVu" }, new string[] { "Cán bộ", "Chức vụ" }, new int[] { 100, 80 }), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    DeNghiBoNhiem deNghi = popup.CurrentControl.GetData();
                    if (deNghi != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        ChiTietBoNhiem boNhiem = ObjectSpace.FindObject<ChiTietBoNhiem>(CriteriaOperator.Parse("QuanLyBoNhiem=? and ThongTinNhanVien=? and ChucVu=?", _QuanLyBoNhiem.Oid, deNghi.ThongTinNhanVien.Oid, deNghi.ChucVu.Oid));
                        if ((boNhiem != null && boNhiem.KiemNhiem) || deNghi.KiemNhiem)
                        {
                            QuyetDinhBoNhiemKiemNhiem quyetDinh;
                            if (boNhiem != null)
                                quyetDinh = boNhiem.QuyetDinh as QuyetDinhBoNhiemKiemNhiem;
                            else
                            {
                                quyetDinh = ObjectSpace.CreateObject<QuyetDinhBoNhiemKiemNhiem>();
                                quyetDinh.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(deNghi.BoPhan.Oid);
                                quyetDinh.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(deNghi.ThongTinNhanVien.Oid);
                                quyetDinh.ChucVuKiemNhiemMoi = ObjectSpace.GetObjectByKey<ChucVu>(deNghi.ChucVu.Oid);
                            }
                            Application.ShowView<QuyetDinhBoNhiemKiemNhiem>(ObjectSpace, quyetDinh);
                        }
                        else
                        {
                            QuyetDinhBoNhiem quyetDinh;
                            if (boNhiem != null)
                                quyetDinh = boNhiem.QuyetDinh as QuyetDinhBoNhiem;
                            else
                            {
                                quyetDinh = ObjectSpace.CreateObject<QuyetDinhBoNhiem>();
                                quyetDinh.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(deNghi.BoPhan.Oid);
                                quyetDinh.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(deNghi.ThongTinNhanVien.Oid);
                                quyetDinh.ChucVuMoi = ObjectSpace.GetObjectByKey<ChucVu>(deNghi.ChucVu.Oid);
                            }
                            Application.ShowView<QuyetDinhBoNhiem>(ObjectSpace, quyetDinh);
                        }
                        SetNotification("Lập QĐ bổ nhiệm thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDMienNhiem.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyBoNhiem>())
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
                _QuanLyBoNhiem = ObjectSpace.GetObjectByKey<QuanLyBoNhiem>(obj);
                if (_QuanLyBoNhiem != null)
                {
                    Application.ShowView<QuanLyBoNhiem>(ObjectSpace, _QuanLyBoNhiem);
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
            helper.XuLy("QuyTrinhBoNhiem", "Quy trình bổ nhiệm");
        }

        private void StartQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
            btnThoiDoiHetNhiemKy.State = CustomButton.ButtonState.Normal;
            btnQDMienNhiem.State = CustomButton.ButtonState.Normal;
            btnQDBoNhiem.State = CustomButton.ButtonState.Normal;
            btnLapDeNghiBoNhiemLai.State = CustomButton.ButtonState.Normal;
            btnLapDeNghiBoNhiemMoi.State = CustomButton.ButtonState.Normal;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            btnThoiDoiHetNhiemKy.State = CustomButton.ButtonState.Disable;
            btnQDMienNhiem.State = CustomButton.ButtonState.Disable;
            btnQDBoNhiem.State = CustomButton.ButtonState.Disable;
            btnLapDeNghiBoNhiemLai.State = CustomButton.ButtonState.Disable;
            btnLapDeNghiBoNhiemMoi.State = CustomButton.ButtonState.Disable;
        }

        private IEnumerable<ThongTinBoNhiem> GetHetNhiemKyList()
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Đang xử lý dữ liệu.", "Vui lòng chờ..."))
            {
                ObjectSpace = Application.CreateObjectSpace();

                DateTime current = HamDungChung.GetServerTime();
                current = current.SetTime(SetTimeEnum.EndMonth).AddMonths(1);
                SqlParameter[] param = new SqlParameter[] 
                {
                    new SqlParameter("@TuNgay", new DateTime(2008, 1, 1).SetTime(SetTimeEnum.StartMonth)),
                    new SqlParameter("@DenNgay", current.SetTime(SetTimeEnum.EndMonth)) 
                };
                IEnumerable<ThongTinBoNhiem> data = SystemContainer.Resolver<INhacViec<ThongTinBoNhiem>>().GetData(ObjectSpace, param);

                return data;
            }
        }
    }
}
