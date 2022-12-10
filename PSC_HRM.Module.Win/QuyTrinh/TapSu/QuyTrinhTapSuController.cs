using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using PSC_HRM.Module.XuLyQuyTrinh;
using PSC_HRM.Module.TapSu;
using PSC_HRM.Module.Win.Forms;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using System.Data;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.TapSu
{
    public partial class QuyTrinhTapSuController : QuyTrinhBaseController
    {
        private DeNghiBoNhiemNgach _DeNghiBoNhiemNgach;

        public QuyTrinhTapSuController(XafApplication application, IObjectSpace objectSpace)
            : base(application, objectSpace)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình tập sự");
        }

        private void QuyTrinhTapSuController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhTapSu);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _DeNghiBoNhiemNgach = ObjectSpace.GetObjectByKey<DeNghiBoNhiemNgach>(obj);
                if (_DeNghiBoNhiemNgach != null)
                {
                    StartQuyTrinh();
                    SetNotification("Đang chạy quy trình tập sự " + _DeNghiBoNhiemNgach.QuanLyTapSu.NamHoc.TenNamHoc);
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
            if (HamDungChung.IsWriteGranted<QuanLyTapSu>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<DeNghiBoNhiemNgach>> popup = new frmPopUp<ChonDuLieuController<DeNghiBoNhiemNgach>>(Application, ObjectSpace, new ChonDuLieuController<DeNghiBoNhiemNgach>(Application, ObjectSpace, "Quản lý tập sự", CriteriaOperator.Parse(""), new string[] { "QuanLyTapSu.NamHoc.TenNamHoc", "Dot" }, new string[] { "Năm học", "Đợt" }, new int[] { 150, 50 }), "Chọn quản lý tập sự", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    _DeNghiBoNhiemNgach = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _DeNghiBoNhiemNgach))
                    {
                        StartQuyTrinh();
                        SetNotification("Đang chạy quy trình tập sự " + _DeNghiBoNhiemNgach.QuanLyTapSu.NamHoc.TenNamHoc);
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        //private void btnQDTuyenDung_Click(object sender, EventArgs e)
        //{
        //    if (HamDungChung.IsWriteGranted<QuyetDinhTuyenDung>())
        //    {
        //        //ObjectSpace = Application.CreateObjectSpace();
        //        //frmPopUp<ChonDanhSachNhanVienController> popup = new frmPopUp<ChonDanhSachNhanVienController>(Application, ObjectSpace, new ChonDanhSachNhanVienController(((XPObjectSpace)ObjectSpace).Session), "Chọn cán bộ", "Chọn", true);
        //        //if (popup.ShowDialog() == DialogResult.OK)
        //        //{
        //        //    List<Guid> oid = popup.CurrentControl.GetNhanVienList();
        //        //    XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)ObjectSpace).Session, new InOperator("Oid", oid));
        //        //    if (nvList.Count > 0)
        //        //    {
        //        //        ObjectSpace = Application.CreateObjectSpace();

        //        //        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
        //        //        QuyetDinhTuyenDung quyetDinh = ObjectSpace.FindObject<QuyetDinhTuyenDung>(CriteriaOperator.Parse("ThongTinNhanVien=?", nhanVien.Oid));
        //        //        if (quyetDinh == null)
        //        //        {
        //        //            quyetDinh = ObjectSpace.CreateObject<QuyetDinhTuyenDung>();
        //        //            quyetDinh.QuanLyTuyenDung = quanly
        //        //            //quyetDinh.BoPhan = nhanVien.BoPhan;
        //        //            //quyetDinh.ThongTinNhanVien = nhanVien;
        //        //        }

        //        //        Application.ShowView<QuyetDinhTuyenDung>(ObjectSpace, quyetDinh);
        //        //        SetNotification("Lập QĐ tuyển dụng thành công.");
        //        //        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDTuyenDung.Caption);
        //        //    }
        //        //}
        //    }
        //    else
        //        SetNotification("Bạn không được cấp quyền.");
        //}

        private void btnLapHDLamViecLanDau_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<HopDong_LamViec>())
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
                        HopDong_LamViec hopdong = ObjectSpace.FindObject<HopDong_LamViec>(CriteriaOperator.Parse("NhanVien=? and PhanLoai=0", nhanVien.Oid));
                        if (hopdong == null)
                        {
                            hopdong = ObjectSpace.CreateObject<HopDong_LamViec>();
                            hopdong.BoPhan = nhanVien.BoPhan;
                            hopdong.NhanVien = nhanVien;
                            hopdong.PhanLoai = HopDongLamViecEnum.HopDongLanDau;
                            hopdong.HinhThucHopDong = ObjectSpace.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("SoThang=?", 12));
                        }

                        Application.ShowView<HopDong_LamViec>(ObjectSpace, hopdong);
                        SetNotification("Lập HĐ làm việc lần đầu thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapHDLamViecLanDau.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");

        }

        private void btnQDHuongDanTapSu_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhHuongDanTapSu>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuyetDinhTuyenDung>> popup = new frmPopUp<ChonDuLieuController<QuyetDinhTuyenDung>>(Application, ObjectSpace, new ChonDuLieuController<QuyetDinhTuyenDung>(Application, ObjectSpace, "QĐ hướng dẫn tập sự", CriteriaOperator.Parse(""), new string[] { "SoQuyetDinh", "NgayHieuLuc" }, new string[] { "Số quyết định", "Ngày hiệu lực" }, new int[] { 80, 100 }), "Chọn QĐ hướng dẫn tập sự", "Chọn", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    QuyetDinhTuyenDung qd = popup.CurrentControl.GetData();
                    if (qd != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        QuyetDinhHuongDanTapSu quyetDinh = ObjectSpace.FindObject<QuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhTuyenDung=?", qd.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhHuongDanTapSu>();
                            quyetDinh.QuyetDinhTuyenDung = ObjectSpace.GetObjectByKey<QuyetDinhTuyenDung>(qd.Oid);
                        }

                        Application.ShowView<QuyetDinhHuongDanTapSu>(ObjectSpace, quyetDinh);
                        SetNotification("Lập QĐ hướng dẫn tập sự thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDHuongDanTapSu.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");

        }

        private void btnQDTamHoanTapSu_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhTamHoanTapSu>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetTapSuList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        QuyetDinhTamHoanTapSu quyetDinh = ObjectSpace.FindObject<QuyetDinhTamHoanTapSu>(CriteriaOperator.Parse("ThongTinNhanVien=?", nhanVien.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhTamHoanTapSu>();
                            quyetDinh.BoPhan = nhanVien.BoPhan;
                            quyetDinh.ThongTinNhanVien = nhanVien;
                        }
                        Application.ShowView<QuyetDinhTamHoanTapSu>(ObjectSpace, quyetDinh);
                        SetNotification("Lập QĐ tạm hoản tập sự thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDTamHoanTapSu.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDThayCanBoHuongDan_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhThayCanBoHuongDanTapSu>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetTapSuList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        QuyetDinhThayCanBoHuongDanTapSu quyetDinh = ObjectSpace.CreateObject<QuyetDinhThayCanBoHuongDanTapSu>();
                        quyetDinh.BoPhan = nhanVien.BoPhan;
                        quyetDinh.ThongTinNhanVien = nhanVien;

                        Application.ShowView<QuyetDinhThayCanBoHuongDanTapSu>(ObjectSpace, quyetDinh);
                        SetNotification("Lập QĐ thay cán bộ hướng dẫn tập sự thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDThayCanBoHuongDan.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnLapDeNghiBoNhiemNgach_Click(object sender, EventArgs e)
        {
            //if (HamDungChung.IsWriteGranted<ChiTietDeNghiBoNhiemNgach>())
            //{
            //    ObjectSpace = Application.CreateObjectSpace();
            //    frmPopUp<ChonDuLieuController<QuyetDinhHuongDanTapSu>> popup = new frmPopUp<ChonDuLieuController<QuyetDinhHuongDanTapSu>>(Application, ObjectSpace, new ChonDuLieuController<QuyetDinhHuongDanTapSu>(Application, ObjectSpace, "QĐ hướng dẫn tập sự", CriteriaOperator.Parse(""), new string[] { "SoQuyetDinh", "NgayHieuLuc" }, new string[] { "Số quyết định", "Ngày hiệu lực" }, new int[] { 80, 100 }), "Chọn QĐ hướng dẫn tập sự", "Chọn", true);
            //    if (popup.ShowDialog() == DialogResult.OK)
            //    {
            //        QuyetDinhHuongDanTapSu quyetDinh = popup.CurrentControl.GetData();
            //        if (quyetDinh != null)
            //        {
            //            ObjectSpace = Application.CreateObjectSpace();
            //            ChiTietDeNghiBoNhiemNgach deNghi = ObjectSpace.FindObject<ChiTietDeNghiBoNhiemNgach>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=?", quyetDinh.Oid));
            //            if (deNghi == null)
            //            {
            //                deNghi = ObjectSpace.CreateObject<DeNghiBoNhiemNgach>();
            //                deNghi.QuanLyTapSu = ObjectSpace.GetObjectByKey<QuanLyTapSu>(_QuanLyTapSu.Oid);
            //                deNghi.QuyetDinhHuongDanTapSu = ObjectSpace.GetObjectByKey<QuyetDinhHuongDanTapSu>(quyetDinh.Oid);
            //            }

            //            Application.ShowView<DeNghiBoNhiemNgach>(ObjectSpace, deNghi);
            //            SetNotification("Lập đề nghị bổ nhiệm ngạch thành công.");
            //            ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapDeNghiBoNhiemNgach.Caption);
            //        }
            //    }
            //}
            //else
            //    SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDGiaHanTapSu_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhGiaHanTapSu>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetTapSuList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        nhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                        QuyetDinhGiaHanTapSu quyetDinh = ObjectSpace.FindObject<QuyetDinhGiaHanTapSu>(CriteriaOperator.Parse("ThongTinNhanVien=?", nhanVien.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhGiaHanTapSu>();
                            quyetDinh.BoPhan = nhanVien.BoPhan;
                            quyetDinh.ThongTinNhanVien = nhanVien;
                        }

                        Application.ShowView<QuyetDinhGiaHanTapSu>(ObjectSpace, quyetDinh);
                        SetNotification("Lập QĐ gia hạn tập sự thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDGiaHanTapSu.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDBoNhiemNgach_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhBoNhiemNgach>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                ObjectSpace = Application.CreateObjectSpace();
                QuyetDinhBoNhiemNgach quyetDinh = ObjectSpace.FindObject<QuyetDinhBoNhiemNgach>(CriteriaOperator.Parse("QuanLyTapSu=?", _DeNghiBoNhiemNgach.Oid));
                if (quyetDinh == null)
                {
                    quyetDinh = ObjectSpace.CreateObject<QuyetDinhBoNhiemNgach>();
                    quyetDinh.DeNghiBoNhiemNgach = ObjectSpace.GetObjectByKey<DeNghiBoNhiemNgach>(_DeNghiBoNhiemNgach.Oid);
                }

                Application.ShowView<QuyetDinhBoNhiemNgach>(ObjectSpace, quyetDinh);
                SetNotification("Lập QĐ bổ nhiệm ngạch thành công.");
                ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDBoNhiemNgach.Caption);

            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDChamDutHopDong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhChamDutHopDong>())
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

        private void btnHDLamViecCoThoiHan_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<HopDong_LamViec>())
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
                        HopDong_LamViec hopDong = ObjectSpace.FindObject<HopDong_LamViec>(CriteriaOperator.Parse("NhanVien=? and PhanLoai=1", nhanVien.Oid));
                        if (hopDong == null)
                        {
                            hopDong = ObjectSpace.CreateObject<HopDong_LamViec>();
                            hopDong.BoPhan = nhanVien.BoPhan;
                            hopDong.NhanVien = nhanVien;
                            hopDong.PhanLoai = HopDongLamViecEnum.CoThoiHan;
                            hopDong.HinhThucHopDong = ObjectSpace.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("SoThang=?", 12));
                        }

                        Application.ShowView<HopDong_LamViec>(ObjectSpace, hopDong);
                        SetNotification("Lập HĐ làm việc có thời hạn thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnHDLamViecCoThoiHan.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyTapSu>())
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
                _DeNghiBoNhiemNgach = ObjectSpace.GetObjectByKey<DeNghiBoNhiemNgach>(obj);
                if (_DeNghiBoNhiemNgach != null)
                {
                    Application.ShowView<DeNghiBoNhiemNgach>(ObjectSpace, _DeNghiBoNhiemNgach);
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
            helper.XuLy("QuyTrinhTapSu", "Quy trình tập sự");
        }

        private void StartQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            //btnQDTuyenDung.State = CustomButton.ButtonState.Normal;
            btnLapHDLamViecLanDau.State = CustomButton.ButtonState.Disable;
            btnQDHuongDanTapSu.State = CustomButton.ButtonState.Disable;
            btnQDTamHoanTapSu.State = CustomButton.ButtonState.Disable;
            btnQDThayCanBoHuongDan.State = CustomButton.ButtonState.Disable;
            btnHDLamViecCoThoiHan.State = CustomButton.ButtonState.Disable;
            btnQDBoNhiemNgach.State = CustomButton.ButtonState.Disable;
            btnLapDeNghiBoNhiemNgach.State = CustomButton.ButtonState.Disable;
            btnQDChamDutHopDong.State = CustomButton.ButtonState.Disable;
            btnQDGiaHanTapSu.State = CustomButton.ButtonState.Disable;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            //btnQDTuyenDung.State = CustomButton.ButtonState.Disable;
            btnLapHDLamViecLanDau.State = CustomButton.ButtonState.Disable;
            btnQDHuongDanTapSu.State = CustomButton.ButtonState.Disable;
            btnQDTamHoanTapSu.State = CustomButton.ButtonState.Disable;
            btnQDThayCanBoHuongDan.State = CustomButton.ButtonState.Disable;
            btnHDLamViecCoThoiHan.State = CustomButton.ButtonState.Disable;
            btnQDBoNhiemNgach.State = CustomButton.ButtonState.Disable;
            btnLapDeNghiBoNhiemNgach.State = CustomButton.ButtonState.Disable;
            btnQDChamDutHopDong.State = CustomButton.ButtonState.Disable;
            btnQDGiaHanTapSu.State = CustomButton.ButtonState.Disable;
        }


        private List<Guid> GetNhanVienList()
        {
            List<Guid> data = new List<Guid>();
            ObjectSpace = Application.CreateObjectSpace();
            if (_DeNghiBoNhiemNgach != null)
            {
                SqlParameter[] param = new SqlParameter[] { 
                        new SqlParameter("@NamHoc", _DeNghiBoNhiemNgach.QuanLyTapSu.NamHoc.Oid), 
                        //new SqlParameter("@Dot", _QuanLyTapSu.Dot), 
                        new SqlParameter("@BienChe", true) };
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


        private List<Guid> GetTapSuList()
        {
            ObjectSpace = Application.CreateObjectSpace();

            XPCollection<ChiTietQuyetDinhBoNhiemNgach> list = new XPCollection<ChiTietQuyetDinhBoNhiemNgach>(((XPObjectSpace)ObjectSpace).Session);
            var temp = from d in list
                       select d.ThongTinNhanVien.Oid;

            XPCollection<ChiTietQuyetDinhHuongDanTapSu> list1 = new XPCollection<ChiTietQuyetDinhHuongDanTapSu>(((XPObjectSpace)ObjectSpace).Session, new InOperator("ThongTinNhanVien.Oid", temp).Not());

            var data = from d in list1
                       select d.ThongTinNhanVien.Oid;

            return data.ToList<Guid>();
        }


        private List<Guid> GetDeNghiList()
        {
            ObjectSpace = Application.CreateObjectSpace();
            var data = from d in _DeNghiBoNhiemNgach.ListChiTietDeNghiBoNhiemNgach
                       select d.ThongTinNhanVien.Oid;

            return data.ToList<Guid>();
        }
    }
}
