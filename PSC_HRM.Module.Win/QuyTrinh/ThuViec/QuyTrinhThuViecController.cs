using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using PSC_HRM.Module.ThuViec;
using PSC_HRM.Module.XuLyQuyTrinh;
using PSC_HRM.Module.Win.Forms;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.ThuViec
{
    public partial class QuyTrinhThuViecController : QuyTrinhBaseController
    {
        private QuanLyThuViec _QuanLyThuViec;

        public QuyTrinhThuViecController(XafApplication application, IObjectSpace objectSpace)
            : base(application, objectSpace)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình thử việc");
        }

        private void QuyTrinhThuViecController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhThuViec);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLyThuViec = ObjectSpace.GetObjectByKey<QuanLyThuViec>(obj);
                if (_QuanLyThuViec != null)
                {
                    StartQuyTrinh();
                    SetNotification("Đang chạy quy trình thử việc " + _QuanLyThuViec.Caption);
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
            if (HamDungChung.IsWriteGranted<QuanLyThuViec>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuanLyThuViec>> popup = new frmPopUp<ChonDuLieuController<QuanLyThuViec>>(Application, ObjectSpace, new ChonDuLieuController<QuanLyThuViec>(Application, ObjectSpace, "Quản lý tập sự", CriteriaOperator.Parse(""), new string[] { "Caption" }, new string[] { "Quản lý thử việc" }, new int[] { 150 }), "Chọn quản lý thử việc", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    _QuanLyThuViec = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLyThuViec))
                    {
                        StartQuyTrinh();
                        SetNotification("Đang chạy quy trình thử việc " + _QuanLyThuViec.Caption);
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDHopDong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhHopDong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetNhanVienList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        QuyetDinhHopDong quyetDinh = ObjectSpace.FindObject<QuyetDinhHopDong>(CriteriaOperator.Parse("ThongTinNhanVien=?", nhanVien.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhHopDong>();
                            quyetDinh.BoPhan = nhanVien.BoPhan;
                            quyetDinh.ThongTinNhanVien = nhanVien;
                        }

                        Application.ShowView<QuyetDinhHopDong>(ObjectSpace, quyetDinh);
                        SetNotification("Lập QĐ hợp đồng thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDHopDong.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnHDLaoDongThuViec_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<HopDong_LaoDong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetNhanVienList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        HopDong_LaoDong hopdong = ObjectSpace.FindObject<HopDong_LaoDong>(CriteriaOperator.Parse("NhanVien=? and PhanLoai=0", nhanVien.Oid));
                        if (hopdong == null)
                        {
                            hopdong = ObjectSpace.CreateObject<HopDong_LaoDong>();
                            hopdong.BoPhan = nhanVien.BoPhan;
                            hopdong.NhanVien = nhanVien;
                            hopdong.PhanLoai = HopDongLaoDongEnum.TapSuThuViec;
                        }

                        Application.ShowView<HopDong_LaoDong>(ObjectSpace, hopdong);
                        SetNotification("Lập HĐ lao động thử việc thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnHDLaoDongThuViec.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnLapDeNghiXepLuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<ChiTietDeNghiXepLuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetThuViecList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        ChiTietDeNghiXepLuong deNghi = ObjectSpace.FindObject<ChiTietDeNghiXepLuong>(CriteriaOperator.Parse("QuanLyThuViec=? and ThongTinNhanVien=?", _QuanLyThuViec.Oid, nhanVien.Oid));
                        if (deNghi == null)
                        {
                            deNghi = ObjectSpace.CreateObject<ChiTietDeNghiXepLuong>();
                            deNghi.QuanLyThuViec = ObjectSpace.GetObjectByKey<QuanLyThuViec>(_QuanLyThuViec.Oid);
                            deNghi.BoPhan = nhanVien.BoPhan;
                            deNghi.ThongTinNhanVien = nhanVien;
                        }

                        Application.ShowView<ChiTietDeNghiXepLuong>(ObjectSpace, deNghi);
                        SetNotification("Lập đề nghị xếp lương thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapDeNghiXepLuong.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnHDLaoDongCoThoiHan_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<HopDong_LaoDong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetDeNghiList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        HopDong_LaoDong hopdong = ObjectSpace.FindObject<HopDong_LaoDong>(CriteriaOperator.Parse("NhanVien=? and PhanLoai=1", nhanVien.Oid));
                        if (hopdong == null)
                        {
                            hopdong = ObjectSpace.CreateObject<HopDong_LaoDong>();
                            hopdong.BoPhan = nhanVien.BoPhan;
                            hopdong.NhanVien = nhanVien;
                            hopdong.PhanLoai = HopDongLaoDongEnum.CoThoiHan;
                            hopdong.HinhThucHopDong = ObjectSpace.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("SoThang=12"));
                        }

                        Application.ShowView<HopDong_LaoDong>(ObjectSpace, hopdong);
                        SetNotification("Lập HĐ lao động có thời hạn thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnHDLaoDongCoThoiHan.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDXepLuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhXepLuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetDeNghiList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        QuyetDinhXepLuong quyetDinh = ObjectSpace.FindObject<QuyetDinhXepLuong>(CriteriaOperator.Parse("ThongTinNhanVien=?", nhanVien.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhXepLuong>();
                            quyetDinh.BoPhan = nhanVien.BoPhan;
                            quyetDinh.ThongTinNhanVien = nhanVien;
                        }

                        Application.ShowView<QuyetDinhXepLuong>(ObjectSpace, quyetDinh);
                        SetNotification("Lập QĐ xếp lương thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDXepLuong.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDChamDutHopDong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhChamDutHopDong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetNhanVienList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        QuyetDinhChamDutHopDong quyetDinh = ObjectSpace.FindObject<QuyetDinhChamDutHopDong>(CriteriaOperator.Parse("ThongTinNhanVien=?", nhanVien.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhChamDutHopDong>();
                            quyetDinh.BoPhan = nhanVien.BoPhan;
                            quyetDinh.ThongTinNhanVien = nhanVien;
                        }

                        Application.ShowView<QuyetDinhChamDutHopDong>(ObjectSpace, quyetDinh);
                        SetNotification("Lập QĐ chấm dứt hợp đồng thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDChamDutHopDong.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyThuViec>())
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
                _QuanLyThuViec = ObjectSpace.GetObjectByKey<QuanLyThuViec>(obj);
                if (_QuanLyThuViec != null)
                {
                    Application.ShowView<QuanLyThuViec>(ObjectSpace, _QuanLyThuViec);
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
            helper.XuLy("QuyTrinhThuViec", "Quy trình thử việc");
        }

        private void StartQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
            btnHDLaoDongThuViec.State = CustomButton.ButtonState.Normal;
            btnLapDeNghiXepLuong.State = CustomButton.ButtonState.Normal;
            btnQDChamDutHopDong.State = CustomButton.ButtonState.Normal;
            btnQDHopDong.State = CustomButton.ButtonState.Normal;
            btnQDXepLuong.State = CustomButton.ButtonState.Normal;
            btnHDLaoDongCoThoiHan.State = CustomButton.ButtonState.Normal;
            btnQDXepLuong.State = CustomButton.ButtonState.Normal;

        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            btnHDLaoDongThuViec.State = CustomButton.ButtonState.Disable;
            btnLapDeNghiXepLuong.State = CustomButton.ButtonState.Disable;
            btnQDChamDutHopDong.State = CustomButton.ButtonState.Disable;
            btnQDHopDong.State = CustomButton.ButtonState.Disable;
            btnQDXepLuong.State = CustomButton.ButtonState.Disable;
            btnHDLaoDongCoThoiHan.State = CustomButton.ButtonState.Disable;
            btnQDXepLuong.State = CustomButton.ButtonState.Disable;

        }


        private List<Guid> GetNhanVienList()
        {
            List<Guid> data = new List<Guid>();
            ObjectSpace = Application.CreateObjectSpace();
            if (_QuanLyThuViec != null)
            {
                SqlParameter[] param = new SqlParameter[] { 
                        new SqlParameter("@NamHoc", _QuanLyThuViec.NamHoc.Oid), 
                        new SqlParameter("@Dot", _QuanLyThuViec.Dot), 
                        new SqlParameter("@BienChe", false) };
                using (DataTable dt = DataProvider.GetDataTable("spd_System_GetUngVien", CommandType.StoredProcedure, param))
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        if (!item.IsNull(0))
                            data.Add(new Guid(item[0].ToString()));
                    }
                }
            }
            return data;
        }


        private List<Guid> GetThuViecList()
        {
            ObjectSpace = Application.CreateObjectSpace();

            //var data = from d in _QuanLyThuViec.ListChiTietThuViec
            //           select d.ThongTinNhanVien.Oid;

            return new List<Guid>();
        }


        private List<Guid> GetDeNghiList()
        {
            ObjectSpace = Application.CreateObjectSpace();
            var data = from d in _QuanLyThuViec.ListChiTietDeNghiXepLuong
                       select d.ThongTinNhanVien.Oid;

            return data.ToList<Guid>();
        }
    }
}
