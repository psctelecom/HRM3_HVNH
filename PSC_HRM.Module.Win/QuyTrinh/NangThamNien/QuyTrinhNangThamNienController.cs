using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using PSC_HRM.Module.NangThamNien;
using PSC_HRM.Module.Win.Forms;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.XuLyQuyTrinh;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using System.Data;
using DevExpress.Xpo;
using PSC_HRM.Module.Win.QuyTrinh.Common;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.NangThamNien
{
    public partial class QuyTrinhNangThamNienController : QuyTrinhBaseController
    {
        private QuanLyNangPhuCapThamNien _QuanLyNangPhuCapThamNien;

        public QuyTrinhNangThamNienController(XafApplication application, IObjectSpace objectSpace)
            : base(application, objectSpace)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình nâng phụ cấp thâm niên");
        }

        private void QuyTrinhNangThamNienController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhNangThamNien);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLyNangPhuCapThamNien = ObjectSpace.GetObjectByKey<QuanLyNangPhuCapThamNien>(obj);
                if (_QuanLyNangPhuCapThamNien != null)
                {
                    StartQuyTrinh();
                    SetNotification("Đang chạy quy trình nâng phụ cấp thâm niên " + _QuanLyNangPhuCapThamNien.Nam.ToString("####"));
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
            if (HamDungChung.IsWriteGranted<QuanLyNangPhuCapThamNien>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuanLyNangPhuCapThamNien>> popup = new frmPopUp<ChonDuLieuController<QuanLyNangPhuCapThamNien>>(Application, ObjectSpace, new ChonDuLieuController<QuanLyNangPhuCapThamNien>(Application, ObjectSpace, "Quản lý nâng phụ cấp thâm niên", CriteriaOperator.Parse(""), new string[] { "Nam" }, new string[] { "Năm" }, new int[] { 150 }), "Chọn quản lý nâng phụ cấp thâm niên", true);
                if (popup.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    _QuanLyNangPhuCapThamNien = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLyNangPhuCapThamNien))
                    {
                        StartQuyTrinh();
                        SetNotification("Đang chạy quy trình nâng phụ cấp thâm niên " + _QuanLyNangPhuCapThamNien.Nam.ToString("####"));
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnTheoDoiNangThamNien_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyNangPhuCapThamNien>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<TheoDoiNangThamNienController> popup = new frmPopUp<TheoDoiNangThamNienController>(Application, ObjectSpace, new TheoDoiNangThamNienController(Application, ObjectSpace, _QuanLyNangPhuCapThamNien), "Theo dõi nâng phụ cấp thâm niên", false);
                popup.Size = new System.Drawing.Size(601, 471);
                popup.Show();
                ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnTheoDoiNangThamNien.Caption);
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnLapDeNghiNangThamNien_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<ChiTietDeNghiNangPhuCapThamNien>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDanhSachNhanVienController> popup = new frmPopUp<ChonDanhSachNhanVienController>(Application, ObjectSpace,
                    new ChonDanhSachNhanVienController(GetDenHanNangThamNienList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var oid = popup.CurrentControl.GetNhanVienList();
                    var nvList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)ObjectSpace).Session, new InOperator("Oid", oid));
                    ObjectSpace = Application.CreateObjectSpace();
                    DateTime current = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
                    DeNghiNangPhuCapThamNien deNghi = ObjectSpace.FindObject<DeNghiNangPhuCapThamNien>(CriteriaOperator.Parse("QuanLyNangPhuCapThamNien=? and Thang=?", _QuanLyNangPhuCapThamNien.Oid, current));
                    if (deNghi == null)
                    {
                        deNghi = ObjectSpace.CreateObject<DeNghiNangPhuCapThamNien>();
                        deNghi.QuanLyNangPhuCapThamNien = ObjectSpace.GetObjectByKey<QuanLyNangPhuCapThamNien>(_QuanLyNangPhuCapThamNien.Oid);
                        deNghi.Thang = current;
                    }

                    ChiTietDeNghiNangPhuCapThamNien chiTiet;
                    foreach (var item in nvList)
                    {
                        chiTiet = ObjectSpace.FindObject<ChiTietDeNghiNangPhuCapThamNien>(CriteriaOperator.Parse("DeNghiNangPhuCapThamNien=? and ThongTinNhanVien=?", deNghi.Oid, item.Oid));
                        if (chiTiet == null)
                        {
                            chiTiet = ObjectSpace.CreateObject<ChiTietDeNghiNangPhuCapThamNien>();
                            chiTiet.DeNghiNangPhuCapThamNien = deNghi;
                            chiTiet.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                            chiTiet.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(item.Oid);
                            deNghi.ListChiTietDeNghiNangPhuCapThamNien.Add(chiTiet);
                        }
                    }

                    Application.ShowView<DeNghiNangPhuCapThamNien>(ObjectSpace, deNghi);
                    ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapDeNghiNangThamNien.Caption);
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDNangThamNien_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhNghiKhongHuongLuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<DeNghiNangPhuCapThamNien>> popup = new frmPopUp<ChonDuLieuController<DeNghiNangPhuCapThamNien>>(Application, ObjectSpace,
                    new ChonDuLieuController<DeNghiNangPhuCapThamNien>(Application, ObjectSpace, "Đề nghị nâng thâm niên",
                        CriteriaOperator.Parse("QuanLyNangPhuCapThamNien=?", _QuanLyNangPhuCapThamNien.Oid),
                        new string[] { "Thang" }, new string[] { "Tháng" }, new int[] { 100 }), "Chọn đề nghị nâng thâm niên", true);
                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DeNghiNangPhuCapThamNien deNghi = popup.CurrentControl.GetData();
                    if (deNghi != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        deNghi = ObjectSpace.GetObjectByKey<DeNghiNangPhuCapThamNien>(deNghi.Oid);

                        QuyetDinhNangPhuCapThamNienNhaGiao quyetDinh = ObjectSpace.FindObject<QuyetDinhNangPhuCapThamNienNhaGiao>(CriteriaOperator.Parse("DeNghiNangPhuCapThamNien=?", deNghi.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhNangPhuCapThamNienNhaGiao>();
                            quyetDinh.DeNghiNangPhuCapThamNien = deNghi;
                        }

                        ChiTietQuyetDinhNangPhuCapThamNienNhaGiao chiTiet;
                        foreach (var item in deNghi.ListChiTietDeNghiNangPhuCapThamNien)
                        {
                            chiTiet = ObjectSpace.FindObject<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>(CriteriaOperator.Parse("QuyetDinhNangPhuCapThamNienNhaGiao=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                            if (chiTiet == null)
                            {
                                chiTiet = ObjectSpace.CreateObject<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>();
                                chiTiet.QuyetDinhNangPhuCapThamNienNhaGiao = quyetDinh;
                                chiTiet.BoPhan = item.BoPhan;
                                chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                                chiTiet.ThamNienCu = item.ThamNienCu;
                                chiTiet.NgayHuongThamNienCu = item.NgayHuongThamNienCu;
                                chiTiet.ThamNienMoi = item.ThamNienMoi;
                                chiTiet.NgayHuongThamNienMoi = item.NgayHuongThamNienMoi;
                                quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Add(chiTiet);
                            }
                        }
                        Application.ShowView<QuyetDinhNangPhuCapThamNienNhaGiao>(ObjectSpace, quyetDinh);
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDNangThamNien.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyNangPhuCapThamNien>())
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
                _QuanLyNangPhuCapThamNien = ObjectSpace.GetObjectByKey<QuanLyNangPhuCapThamNien>(obj);
                if (_QuanLyNangPhuCapThamNien != null)
                {
                    Application.ShowView<QuanLyNangPhuCapThamNien>(ObjectSpace, _QuanLyNangPhuCapThamNien);
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
            helper.XuLy("QuyTrinhNangPhuCapThamNien", "Quy trình nâng phụ cấp thâm niên");
        }

        private void StartQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
            btnTheoDoiNangThamNien.State = CustomButton.ButtonState.Normal;
            btnLapDeNghiNangThamNien.State = CustomButton.ButtonState.Normal;
            btnQDNangThamNien.State = CustomButton.ButtonState.Normal;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            btnTheoDoiNangThamNien.State = CustomButton.ButtonState.Disable;
            btnLapDeNghiNangThamNien.State = CustomButton.ButtonState.Disable;
            btnQDNangThamNien.State = CustomButton.ButtonState.Disable;
        }

        private NhanVienList GetDenHanNangThamNienList()
        {
            var result = new NhanVienList();
            using (DataTable dt = DataProvider.GetDataTable("spd_System_DenHanNangThamNien", CommandType.StoredProcedure, new SqlParameter("@DenNgay", HamDungChung.GetServerTime().SetTime(SetTimeEnum.EndMonth))))
            {
                NhanVienItem nvItem;
                Guid oid;
                foreach (DataRow item in dt.Rows)
                {
                    nvItem = new NhanVienItem();
                    if (Guid.TryParse(item[0].ToString(), out oid))
                        nvItem.Oid = oid;
                    nvItem.Ho = item[1].ToString();
                    nvItem.Ten = item[2].ToString();
                    nvItem.BoPhan = item[3].ToString();
                    result.Add(nvItem);
                }
            }
            return result;
        }
    }
}
