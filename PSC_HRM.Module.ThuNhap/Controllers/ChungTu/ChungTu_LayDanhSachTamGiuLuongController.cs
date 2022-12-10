using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using System.Data.SqlClient;
using PSC_HRM.Module.ThuNhap.ChungTu;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ChungTu_LayDanhSachTamGiuLuongController : ViewController
    {
        public ChungTu_LayDanhSachTamGiuLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ChungTu_LayDanhSachTamGiuLuongController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;

            if (view != null)
            {
                foreach (ControlDetailItem item in view.GetItems<ControlDetailItem>())
                {
                    if (item.Id == "btnSearch")
                    {
                        SimpleButton btnSearch = item.Control as SimpleButton;
                        if (btnSearch != null)
                        {
                            btnSearch.Text = "Tìm kiếm";
                            btnSearch.Width = 80;
                            btnSearch.Click += (obj, ea) =>
                            {
                                using (DialogUtil.AutoWait())
                                {
                                    TamGiuLuongNhanVien search = view.CurrentObject as TamGiuLuongNhanVien;
                                    if (search != null)
                                    {
                                        IObjectSpace obs = Application.CreateObjectSpace();
                                        //
                                        search.ListChiTietTamGiuLuongNhanVien = new XPCollection<ChiTietTamGiuLuongNhanVien>(((XPObjectSpace)obs).Session, false);
                                        //
                                        DataTable dt = new DataTable();

                                        SqlParameter[] param = new SqlParameter[2];
                                        param[0] = new SqlParameter("@TuThang", search.TuThang);
                                        param[1] = new SqlParameter("@DenThang", search.DenThang);

                                        SqlCommand cmd = DataProvider.GetCommand("spd_ChungTu_LayDanhSachTamGiuLuong", CommandType.StoredProcedure, param);
                                        cmd.Connection = DataProvider.GetConnection();
                                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                                        {
                                            da.Fill(dt);

                                            foreach (DataRow itemData in dt.Rows)
                                            {
                                                ChiTietTamGiuLuongNhanVien chiTiet = new ChiTietTamGiuLuongNhanVien(((XPObjectSpace)obs).Session);
                                                chiTiet.ChungTu = obs.GetObjectByKey<PSC_HRM.Module.ThuNhap.ChungTu.ChungTu>(new Guid(itemData["ChungTu"].ToString()));
                                                chiTiet.KyTinhLuong = chiTiet.ChungTu.KyTinhLuong;
                                                chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(new Guid(itemData["BoPhan"].ToString()));
                                                chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(itemData["NhanVien"].ToString()));
                                                chiTiet.ThueTNCN = Convert.ToDecimal(itemData["ThueTNCN"]);
                                                chiTiet.ThucNhan = Convert.ToDecimal(itemData["ThucNhan"]);
                                                chiTiet.TrangThai = Convert.ToBoolean(itemData["ChiLaiLuong"]) == true ? TrangThaiChiLuongEnum.ChiLaiLuong : TrangThaiChiLuongEnum.TamGiuLuong;
                                                search.ListChiTietTamGiuLuongNhanVien.Add(chiTiet);
                                            }
                                        }
                                    }
                                    //Refesh lại dữ liệu
                                    View.Refresh();
                                }         
                            };
                        }
                    }
                }
            }
        }
    }
}
