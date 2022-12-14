using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.ThuNhap.ThuLao;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.HoSo;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent()]
    [ModelDefault("Caption", "Thêm thù lao từ UIS")]
    [ImageName("Action_Import")]
    public class ImportThuLaoPMS : ImportBase
    {
        public ImportThuLaoPMS(Session session) : base(session) { }

        public override void XuLy(IObjectSpace obs, object obj)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
            {
                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();

                    BangThuLaoNhanVien bangThuLao = uow.GetObjectByKey<BangThuLaoNhanVien>(((BaseObject)obj).Oid);
                    if (bangThuLao != null)
                    {
                        //đọc store từ UIS sau đó chuyễn sang object HRM, store return : SoHieuCongChuc|DienGiai|SoTien...
                        using (SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("PSC_PMS.bin")))
                        {
                            if (cnn.State != ConnectionState.Open)
                                cnn.Open();

                            SqlParameter[] param = new SqlParameter[2];
                            param[0] = new SqlParameter("@TuNgay", bangThuLao.NgayLap.SetTime(SetTimeEnum.StartMonth));
                            param[1] = new SqlParameter("@DenNgay", bangThuLao.NgayLap.SetTime(SetTimeEnum.EndMonth));
                            const string query = "Select ID, MaGiangVienQuanLy, NgayLapBang, TongTien, LaGiangVienThinhGiang From dbo.ChiTienThuLaoGiangDay Where SoChungTuHRM Is Null and NgayLapBang >= @TuNgay and NgayLapBang <= @DenNgay";
                            using (SqlCommand cmd = DataProvider.GetCommand(query, CommandType.Text, param))
                            {
                                cmd.Connection = cnn;
                                using (DataTable dt = new DataTable())
                                {
                                    try
                                    {
                                        dt.Load(cmd.ExecuteReader());
                                        ChiTietThuLaoNhanVien chiTiet;
                                        CriteriaOperator filter;
                                        ThongTinNhanVien nhanVien;
                                        DateTime ngayLap;
                                        decimal soTien;
                                        int id;
                                        bool thinhGiang;

                                        foreach (DataRow row in dt.Rows)
                                        {
                                            if (bool.TryParse(row["LaGiangVienThinhGiang"].ToString(), out thinhGiang))
                                            {
                                                nhanVien = GetNhanVien(uow, row["MaGiangVienQuanLy"].ToString(),string.Empty);
                                                if (nhanVien != null)
                                                {
                                                    filter = CriteriaOperator.Parse("BangThuLaoNhanVien=? and ThongTinNhanVien=? and NgayLap=?",
                                                        bangThuLao, nhanVien, row["NgayLapBang"]);
                                                    chiTiet = uow.FindObject<ChiTietThuLaoNhanVien>(filter);
                                                    if (chiTiet == null)
                                                    {
                                                        chiTiet = new ChiTietThuLaoNhanVien(uow);
                                                        chiTiet.BangThuLaoNhanVien = bangThuLao;
                                                        chiTiet.BoPhan = nhanVien.BoPhan;
                                                        chiTiet.NhanVien = nhanVien;
                                                        if (!row.IsNull("ID") && int.TryParse(row["ID"].ToString(), out id))
                                                            chiTiet.ID = id;
                                                        if (DateTime.TryParse(row["NgayLapBang"].ToString(), out ngayLap))
                                                            chiTiet.NgayLap = ngayLap;
                                                        else
                                                            chiTiet.NgayLap = HamDungChung.GetServerTime();
                                                        if (decimal.TryParse(row["TongTien"].ToString(), out soTien))
                                                        {
                                                            chiTiet.SoTien = soTien;
                                                            chiTiet.SoTienChiuThue = soTien;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        uow.CommitChanges();
                                        XtraMessageBox.Show("Lấy dữ liệu thù lao từ phần mềm PMS thành công.", "Thông báo");
                                    }
                                    catch (Exception ex)
                                    {
                                        XtraMessageBox.Show("Đã xảy ra lỗi khi lấy thù lao từ PMS:\r\n" + ex.Message, "Thông báo",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                        uow.RollbackTransaction();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
