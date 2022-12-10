using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.DoanDang;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.Controllers
{
    [NonPersistent]
    public class KhenThuong_ImportQuyetDinhKhenThuong : BaseObject
    {
        private bool _TapThe;

        [ModelDefault("Caption", "Import quyết định tập thể")]
        public bool TapThe
        {
            get
            {
                return _TapThe;
            }
            set
            {
                SetPropertyValue("TapThe", ref _TapThe, value);
            }
        }

        public KhenThuong_ImportQuyetDinhKhenThuong(Session session) : base(session) { }

        public void XuLy_BUH(IObjectSpace obs)
        {
            if (TapThe == false)
            {
                #region Import quyết định cá nhân
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {

                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:J]"))
                        {
                            ChiTietKhenThuongNhanVien chiTietKhenThuongNhanVien;
                            QuyetDinhKhenThuong quyetDinhKhenThuong;
                            XPCollection<QuyetDinhKhenThuong> listQuyetDinh;
                            XPCollection<ChiTietKhenThuongNhanVien> listChiTietKhenThuongNhanVien;
                            ThongTinNhanVien nhanVien;
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;
                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                uow.BeginTransaction();
                                listQuyetDinh = new XPCollection<QuyetDinhKhenThuong>(uow);
                                listChiTietKhenThuongNhanVien = new XPCollection<ChiTietKhenThuongNhanVien>(uow);

                                using (DialogUtil.AutoWait())
                                {
                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        int soQuyetDinh = 0;
                                        int ngayQuyetDinh = 1;
                                        int ngayHieuLuc = 2;
                                        int coQuanRaQuyetDinh = 3;
                                        int nguoiKy = 4;
                                        int namHoc = 5;
                                        int danhHieu = 6;
                                        int soTien = 7;
                                        int lyDo = 8;
                                        int maQuanLy = 9;

                                        foreach (DataRow item in dt.Rows)
                                        {
                                            //Khởi tạo bộ nhớ đệm
                                            detailLog = new StringBuilder();

                                            String soQuyetDinhText = item[soQuyetDinh].ToString().FullTrim();
                                            String ngayQuyetDinhText = item[ngayQuyetDinh].ToString().FullTrim();
                                            String ngayHieuLucText = item[ngayHieuLuc].ToString().FullTrim();
                                            String coQuanRaQuyetDinhText = item[coQuanRaQuyetDinh].ToString().FullTrim();
                                            String nguoiKyText = item[nguoiKy].ToString().FullTrim();
                                            String namHocText = item[namHoc].ToString().FullTrim();
                                            String danhHieuText = item[danhHieu].ToString().FullTrim();
                                            String soTienText = item[soTien].ToString().FullTrim();
                                            String lyDoText = item[lyDo].ToString().FullTrim();
                                            String maQuanLyText = item[maQuanLy].ToString().FullTrim();
                                            //Tìm nhân viên theo mã quản lý
                                            nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", maQuanLyText));
                                            if (nhanVien != null)
                                            {
                                                if (!string.IsNullOrEmpty(soQuyetDinhText) && !string.IsNullOrEmpty(namHocText))
                                                {
                                                    listQuyetDinh.Filter = CriteriaOperator.Parse("SoQuyetDinh = ? and NamHoc.TenNamHoc =?", soQuyetDinhText, namHocText);
                                                }
                                                if (listQuyetDinh.Count == 1)
                                                {
                                                    quyetDinhKhenThuong = listQuyetDinh[0];
                                                    listChiTietKhenThuongNhanVien.Filter = CriteriaOperator.Parse("QuyetDinhKhenThuong.Oid = ? and ThongTinNhanVien.Oid =?", quyetDinhKhenThuong.Oid, nhanVien.Oid);
                                                    if (listChiTietKhenThuongNhanVien.Count == 0)
                                                    {
                                                        chiTietKhenThuongNhanVien = new ChiTietKhenThuongNhanVien(uow);

                                                        chiTietKhenThuongNhanVien.ThongTinNhanVien = nhanVien;
                                                        chiTietKhenThuongNhanVien.BoPhan = nhanVien.BoPhan;

                                                        listChiTietKhenThuongNhanVien.Add(chiTietKhenThuongNhanVien);
                                                        listQuyetDinh[0].ListChiTietKhenThuongNhanVien.Add(chiTietKhenThuongNhanVien);
                                                    }
                                                }
                                                else if (listQuyetDinh.Count == 0)
                                                {
                                                    quyetDinhKhenThuong = new QuyetDinhKhenThuong(uow);

                                                    #region Số quyết định
                                                    if (!string.IsNullOrEmpty(soQuyetDinhText))
                                                    {
                                                        quyetDinhKhenThuong.SoQuyetDinh = soQuyetDinhText;
                                                    }
                                                    //else
                                                    //{
                                                    //    detailLog.AppendLine("Số quyết định chưa có dữ liệu");
                                                    //}
                                                    #endregion

                                                    #region Ngày quyết định
                                                    if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                                    {
                                                        try
                                                        {
                                                            quyetDinhKhenThuong.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                                        }
                                                        catch
                                                        {
                                                            detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("Ngày quyết định chưa có dữ liệu");
                                                    }
                                                    #endregion

                                                    #region Ngày hiệu lực
                                                    if (!string.IsNullOrEmpty(ngayHieuLucText))
                                                    {
                                                        try
                                                        {
                                                            quyetDinhKhenThuong.NgayHieuLuc = Convert.ToDateTime(ngayHieuLucText);
                                                        }
                                                        catch
                                                        {
                                                            detailLog.AppendLine(" + Ngày hiệu lực không hợp lệ: " + ngayHieuLucText);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("Ngày hiệu lực chưa có dữ liệu");
                                                    }
                                                    #endregion

                                                    #region Cơ quan ra quyết định - Nguời ký
                                                    if (!string.IsNullOrEmpty(coQuanRaQuyetDinhText))
                                                    {
                                                        quyetDinhKhenThuong.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.CoQuanKhacRaQuyetDinh;
                                                        quyetDinhKhenThuong.TenCoQuan = coQuanRaQuyetDinhText;

                                                        if (!string.IsNullOrEmpty(nguoiKyText))
                                                        {
                                                            quyetDinhKhenThuong.NguoiKy1 = nguoiKyText;
                                                        }

                                                    }
                                                    //else
                                                    //{
                                                    //    quyetDinhKhenThuong.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh;
                                                    //}
                                                    #endregion

                                                    #region Năm học
                                                    if (!string.IsNullOrEmpty(namHocText))
                                                    {
                                                        NamHoc NamHoc = uow.FindObject<NamHoc>(CriteriaOperator.Parse("TenNamHoc =?", namHocText));
                                                        if (NamHoc != null)
                                                            quyetDinhKhenThuong.NamHoc = NamHoc;
                                                        else
                                                            detailLog.AppendLine("Năm học không tìm thấy.");
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("Năm học chưa có dữ liệu.");
                                                    }
                                                    #endregion

                                                    #region Danh hiệu
                                                    if (!string.IsNullOrEmpty(danhHieuText))
                                                    {
                                                        DanhHieuKhenThuong DanhHieuKhenThuong = uow.FindObject<DanhHieuKhenThuong>(CriteriaOperator.Parse("MaQuanLy =?", danhHieuText));
                                                        if (DanhHieuKhenThuong == null)
                                                        {
                                                            DanhHieuKhenThuong = new DanhHieuKhenThuong(uow);
                                                            DanhHieuKhenThuong.MaQuanLy = HamDungChung.TaoChuVietTat(danhHieuText);
                                                            DanhHieuKhenThuong.TenDanhHieu = danhHieuText;
                                                        }
                                                        quyetDinhKhenThuong.DanhHieuKhenThuong = DanhHieuKhenThuong;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("Danh hiệu khen thưởng chưa có dữ liệu.");
                                                    }
                                                    #endregion

                                                    #region Số tiền
                                                    if (!string.IsNullOrEmpty(soTienText))
                                                    {
                                                        try
                                                        {
                                                            quyetDinhKhenThuong.SoTienKhenThuong = Convert.ToDecimal(soTienText);
                                                        }
                                                        catch
                                                        {
                                                            detailLog.AppendLine(" + Số tiền không hợp lệ: " + soTienText);
                                                        }
                                                    }
                                                    else
                                                        quyetDinhKhenThuong.SoTienKhenThuong = 0;
                                                    #endregion

                                                    #region Lý do
                                                    if (!string.IsNullOrEmpty(lyDoText))
                                                    {
                                                        quyetDinhKhenThuong.LyDo = lyDoText;
                                                    }
                                                    #endregion

                                                    //Thêm chi tiết
                                                    chiTietKhenThuongNhanVien = new ChiTietKhenThuongNhanVien(uow);

                                                    chiTietKhenThuongNhanVien.QuyetDinhKhenThuong = quyetDinhKhenThuong;
                                                    chiTietKhenThuongNhanVien.ThongTinNhanVien = nhanVien;
                                                    chiTietKhenThuongNhanVien.BoPhan = nhanVien.BoPhan;

                                                    listQuyetDinh.Add(quyetDinhKhenThuong);
                                                    listChiTietKhenThuongNhanVien.Add(chiTietKhenThuongNhanVien);
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(string.Format("Có nhiều quyết định trùng số {0} trong năm học {1}.", soQuyetDinhText, namHocText));
                                                }
                                                

                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                                    mainLog.AppendLine(detailLog.ToString());
                                                }
                                            }
                                            else
                                            {
                                                mainLog.AppendLine(string.Format("- Không có cán bộ nào có Mã nhân sự (Số hiệu công chức) là: {0}", maQuanLy));
                                            }
                                        }
                                    }
                                }

                                //
                                if (mainLog.Length > 0)
                                {
                                    uow.RollbackTransaction();
                                    if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                                    {
                                        string tenFile = "Import_Log.txt";
                                        StreamWriter writer = new StreamWriter(tenFile);
                                        writer.WriteLine(mainLog.ToString());
                                        writer.Flush();
                                        writer.Close();
                                        writer.Dispose();
                                        HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                        System.Diagnostics.Process.Start(tenFile);
                                    }
                                }
                                else
                                {
                                    //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                    uow.CommitChanges();
                                    //hoàn tất giao tác
                                    DialogUtil.ShowSaveSuccessful("Import Thành Công!");
                                }

                            }
                        }
                    }
                }

                #endregion
            }
            else
            {
                #region Import quyết định tập thể

                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {

                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:J]"))
                        {
                            ChiTietKhenThuongBoPhan chiTietKhenThuongBoPhan;
                            QuyetDinhKhenThuong quyetDinhKhenThuong;
                            XPCollection<QuyetDinhKhenThuong> listQuyetDinh;
                            BoPhan boPhan;
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;
                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                uow.BeginTransaction();
                                listQuyetDinh = new XPCollection<QuyetDinhKhenThuong>(uow);

                                using (DialogUtil.AutoWait())
                                {
                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        //
                                        //
                                        int soQuyetDinh = 0;
                                        int ngayQuyetDinh = 1;
                                        int ngayHieuLuc = 2;
                                        int coQuanRaQuyetDinh = 3;
                                        int nguoiKy = 4;
                                        int namHoc = 5;
                                        int danhHieu = 6;
                                        int soTien = 7;
                                        int lyDo = 8;
                                        int tenBoPhan = 9;

                                        foreach (DataRow item in dt.Rows)
                                        {
                                            //Khởi tạo bộ nhớ đệm
                                            detailLog = new StringBuilder();

                                            String soQuyetDinhText = item[soQuyetDinh].ToString().FullTrim();
                                            String ngayQuyetDinhText = item[ngayQuyetDinh].ToString().FullTrim();
                                            String ngayHieuLucText = item[ngayHieuLuc].ToString().FullTrim();
                                            String coQuanRaQuyetDinhText = item[coQuanRaQuyetDinh].ToString().FullTrim();
                                            String nguoiKyText = item[nguoiKy].ToString().FullTrim();
                                            String namHocText = item[namHoc].ToString().FullTrim();
                                            String danhHieuText = item[danhHieu].ToString().FullTrim();
                                            String soTienText = item[soTien].ToString().FullTrim();
                                            String lyDoText = item[lyDo].ToString().FullTrim();
                                            String tenBoPhanText = item[tenBoPhan].ToString().FullTrim();

                                            //Tìm bộ phận
                                            boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan=?", tenBoPhanText));
                                            if (boPhan != null)
                                            {
                                                #region Số quyết định
                                                if (!string.IsNullOrEmpty(soQuyetDinhText))
                                                {
                                                    QuyetDinhKhenThuong QuyetDinh = uow.FindObject<QuyetDinhKhenThuong>(CriteriaOperator.Parse("SoQuyetDinh =?  and NamHoc.TenNamHoc =?", soQuyetDinhText, namHocText));
                                                    if (QuyetDinh == null)
                                                    {
                                                        listQuyetDinh.Filter = CriteriaOperator.Parse("SoQuyetDinh = ? and NamHoc.TenNamHoc =?", soQuyetDinhText, namHocText);

                                                        if (listQuyetDinh.Count > 0) //Đã tạo quyết định chỉ thêm chi tiết
                                                        {
                                                            chiTietKhenThuongBoPhan = new ChiTietKhenThuongBoPhan(uow);

                                                            chiTietKhenThuongBoPhan.BoPhan = boPhan;

                                                            listQuyetDinh[0].ListChiTietKhenThuongBoPhan.Add(chiTietKhenThuongBoPhan);
                                                        }
                                                        else //Tạo quyết định mới
                                                        {
                                                            quyetDinhKhenThuong = new QuyetDinhKhenThuong(uow);

                                                            #region Số quyết định
                                                            if (!string.IsNullOrEmpty(soQuyetDinhText))
                                                            {
                                                                QuyetDinh.QuyetDinh quyetDinh = uow.FindObject<QuyetDinh.QuyetDinh>(CriteriaOperator.Parse("SoQuyetDinh =?", soQuyetDinhText));
                                                                if (quyetDinh == null)
                                                                    quyetDinhKhenThuong.SoQuyetDinh = soQuyetDinhText;
                                                                else
                                                                    detailLog.AppendLine("Số quyết định đã tồn tại: " + soQuyetDinhText);
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine("Số quyết định chưa có dữ liệu");
                                                            }
                                                            #endregion

                                                            #region Ngày quyết định
                                                            if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                                            {
                                                                try
                                                                {
                                                                    quyetDinhKhenThuong.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                                                }
                                                                catch
                                                                {
                                                                    detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine("Ngày quyết định chưa có dữ liệu");
                                                            }
                                                            #endregion

                                                            #region Ngày hiệu lực
                                                            if (!string.IsNullOrEmpty(ngayHieuLucText))
                                                            {
                                                                try
                                                                {
                                                                    quyetDinhKhenThuong.NgayHieuLuc = Convert.ToDateTime(ngayHieuLucText);
                                                                }
                                                                catch
                                                                {
                                                                    detailLog.AppendLine(" + Ngày hiệu lực không hợp lệ: " + ngayHieuLucText);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine("Ngày hiệu lực chưa có dữ liệu");
                                                            }
                                                            #endregion

                                                            #region Cơ quan ra quyết định - Nguời ký
                                                            if (!string.IsNullOrEmpty(coQuanRaQuyetDinhText))
                                                            {
                                                                quyetDinhKhenThuong.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.CoQuanKhacRaQuyetDinh;
                                                                quyetDinhKhenThuong.TenCoQuan = coQuanRaQuyetDinhText;

                                                                if (!string.IsNullOrEmpty(nguoiKyText))
                                                                {
                                                                    quyetDinhKhenThuong.NguoiKy1 = nguoiKyText;
                                                                }

                                                            }
                                                            //else
                                                            //{
                                                            //    quyetDinhKhenThuong.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh;
                                                            //}
                                                            #endregion

                                                            #region Năm học
                                                            if (!string.IsNullOrEmpty(namHocText))
                                                            {
                                                                NamHoc NamHoc = uow.FindObject<NamHoc>(CriteriaOperator.Parse("TenNamHoc =?", namHocText));
                                                                if (NamHoc != null)
                                                                    quyetDinhKhenThuong.NamHoc = NamHoc;
                                                                else
                                                                    detailLog.AppendLine("Năm học không tìm thấy.");
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine("Năm học không tìm thấy.");
                                                            }
                                                            #endregion

                                                            #region Danh hiệu
                                                            if (!string.IsNullOrEmpty(danhHieuText))
                                                            {
                                                                DanhHieuKhenThuong DanhHieuKhenThuong = uow.FindObject<DanhHieuKhenThuong>(CriteriaOperator.Parse("MaQuanLy =?", danhHieuText));
                                                                if (DanhHieuKhenThuong == null)
                                                                {
                                                                    DanhHieuKhenThuong = new DanhHieuKhenThuong(uow);
                                                                    DanhHieuKhenThuong.MaQuanLy = HamDungChung.TaoChuVietTat(danhHieuText);
                                                                    DanhHieuKhenThuong.TenDanhHieu = danhHieuText;
                                                                }
                                                                quyetDinhKhenThuong.DanhHieuKhenThuong = DanhHieuKhenThuong;
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine("Danh hiệu khen thưởng không tìm thấy.");
                                                            }
                                                            #endregion

                                                            #region Số tiền
                                                            if (!string.IsNullOrEmpty(soTienText))
                                                            {
                                                                try
                                                                {
                                                                    quyetDinhKhenThuong.SoTienKhenThuong = Convert.ToDecimal(soTienText);
                                                                }
                                                                catch
                                                                {
                                                                    detailLog.AppendLine(" + Số tiền không hợp lệ: " + soTienText);
                                                                }
                                                            }
                                                            else
                                                                quyetDinhKhenThuong.SoTienKhenThuong = 0;
                                                            #endregion

                                                            #region Lý do
                                                            if (!string.IsNullOrEmpty(lyDoText))
                                                            {
                                                                quyetDinhKhenThuong.LyDo = lyDoText;
                                                            }
                                                            #endregion

                                                            //Thêm chi tiết khen thưởng
                                                            chiTietKhenThuongBoPhan = new ChiTietKhenThuongBoPhan(uow);

                                                            chiTietKhenThuongBoPhan.QuyetDinhKhenThuong = quyetDinhKhenThuong;
                                                            chiTietKhenThuongBoPhan.BoPhan = boPhan;

                                                            listQuyetDinh.Add(quyetDinhKhenThuong);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        //detailLog.AppendLine("Số quyết định " + soQuyetDinhText + " đã tồn tại");

                                                        chiTietKhenThuongBoPhan = new ChiTietKhenThuongBoPhan(uow);

                                                        chiTietKhenThuongBoPhan.BoPhan = boPhan;

                                                        QuyetDinh.ListChiTietKhenThuongBoPhan.Add(chiTietKhenThuongBoPhan);
                                                    }

                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Số quyết định chưa có dữ liệu");
                                                }
                                                #endregion


                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine(string.Format("- Không import bộ phận [{0}] vào được: ", boPhan));
                                                    mainLog.AppendLine(detailLog.ToString());
                                                }
                                            }
                                            else
                                            {
                                                mainLog.AppendLine(string.Format("- Không có bộ phận nào có tên bộ phận là: {0}", tenBoPhanText));
                                            }
                                        }
                                    }
                                }

                                //
                                if (mainLog.Length > 0)
                                {
                                    uow.RollbackTransaction();
                                    if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                                    {
                                        string tenFile = "Import_Log.txt";
                                        StreamWriter writer = new StreamWriter(tenFile);
                                        writer.WriteLine(mainLog.ToString());
                                        writer.Flush();
                                        writer.Close();
                                        writer.Dispose();
                                        HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                        System.Diagnostics.Process.Start(tenFile);
                                    }
                                }
                                else
                                {
                                    //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                    uow.CommitChanges();
                                    //hoàn tất giao tác
                                    DialogUtil.ShowSaveSuccessful("Import Thành Công!");
                                }

                            }
                        }
                    }
                }
                #endregion
            }
        }
    }
}
