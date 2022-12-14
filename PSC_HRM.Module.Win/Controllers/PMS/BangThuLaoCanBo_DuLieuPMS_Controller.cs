using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;
using PSC_HRM.Module.ThuNhap.ThuLao;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.NonPersistent;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class BangThuLaoCanBo_DuLieuPMS_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session ses;
        CollectionSource collectionSource;
        ChonThongTinBangChot _source;
        dsThanhToanThuLao _dsThanhToanThuLao;
        public BangThuLaoCanBo_DuLieuPMS_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "BangThuLaoNhanVien_DetailView";
        }
        private void popDongBo_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(dsThongTinNhanVien));

            BangThuLaoNhanVien obj = View.CurrentObject as BangThuLaoNhanVien;
            if (obj.KyTinhLuong != null)
            {
                View.ObjectSpace.CommitChanges();
                using (DialogUtil.AutoWait("Load danh sách"))
                {
                    collectionSource = new CollectionSource(_obs, typeof(ChonThongTinBangChot));
                    _source = new ChonThongTinBangChot(ses);
                    _source.BangThuLaoNhanVien = new Guid(obj.Oid.ToString());
                    _source.NamHoc = ses.FindObject<NamHoc>(CriteriaOperator.Parse("NgayBatDau <=? and NgayKetThuc >=?", obj.KyTinhLuong.TuNgay, obj.KyTinhLuong.TuNgay));
                    e.View = Application.CreateDetailView(_obs, _source);
                }
            }
            else
            {
                XtraMessageBox.Show("Vui lòng chọn kỳ tính lương!");
            }
        }
        private void popDongBo_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //_obs = View.ObjectSpace;
            //ses = ((XPObjectSpace)_obs).Session;
            BangThuLaoNhanVien obj = View.CurrentObject as BangThuLaoNhanVien;

            if (obj != null)
            {
                if (obj.KyTinhLuong != null)
                {
                   // View.ObjectSpace.CommitChanges();
                    string listThongTinBangChot = "";
                    string listChiTietThongTinBangChot = "";
                    List<dsBangChotThuLao> list = (from d in _source.listBangChot
                                                   where d.Chon == true
                                                   select d).ToList();
                    using (DialogUtil.AutoWait("Đang lấy dữ liệu"))
                    {
                        if (TruongConfig.MaTruong == "HVNH")
                        {
                            foreach (dsBangChotThuLao item in list)
                            {
                                listThongTinBangChot += " UNION ALL " + "SELECT '" + item.OidChiTietBangChotThuLaoGiangDay + "' as ChiTietGioGiang";
                            }
                            listThongTinBangChot = listThongTinBangChot.Substring(11);
                        }
                        else
                        {
                            foreach (dsBangChotThuLao item in list)
                            {
                                listThongTinBangChot += item.OidThongTinBangChot.ToString() + ";";
                                listChiTietThongTinBangChot += item.OidChiTietBangChotThuLaoGiangDay.ToString() + ";";
                            }
                        }
                        if (listThongTinBangChot != string.Empty)
                        {
                            listThongTinBangChot = listThongTinBangChot.Replace("\n", "");

                            SqlParameter[] pDongBo = new SqlParameter[2];
                            pDongBo[0] = new SqlParameter("@listThongTinBangChot", listThongTinBangChot);
                            pDongBo[1] = new SqlParameter("@BangThuLaoNhanVien", obj.Oid);
                            //SqlCommand cmd = DataProvider.GetCommand("spd_pms_BangThuLaoCanBo_DuLieuPMS", CommandType.StoredProcedure, pDongBo);               
                            //DataSet dataset = DataProvider.GetDataSet(cmd);
                            DataProvider.ExecuteNonQuery("spd_pms_BangThuLaoCanBo_DuLieuPMS", CommandType.StoredProcedure, pDongBo);                           
                        }
                    }
                    using (DialogUtil.AutoWait("Đang tính thù lao giảng dạy"))
                    {

                        View.ObjectSpace.CommitChanges();
                        //Lấy công thức tính lương
                        XPCollection<CongThucTinhThuLaoGiangDay> congThucTinhThuLaoList;
                        congThucTinhThuLaoList = new XPCollection<CongThucTinhThuLaoGiangDay>(((XPObjectSpace)View.ObjectSpace).Session);

                        string dieuKienNhanVien;

                        foreach (CongThucTinhThuLaoGiangDay ct in congThucTinhThuLaoList)
                        {
                            if (!ct.NgungSuDung)
                            {
                                dieuKienNhanVien = ct.DieuKienNhanVien.XuLyDieuKienPMS(((XPObjectSpace)View.ObjectSpace), false, new object[] { obj.KyTinhLuong.TuNgay, obj.KyTinhLuong.DenNgay });
                                //
                                foreach (ChiTietCongThucTinhThuLaoGiangDay ctItem in ct.ListChiTietCongThuc)
                                {
                                    if (!ctItem.NgungSuDung)
                                    {
                                        SqlParameter[] param = new SqlParameter[3];
                                        param[0] = new SqlParameter("@BangThuLaoNhanVien", obj.Oid);
                                        param[1] = new SqlParameter("@DieuKienNhanVien", dieuKienNhanVien);
                                        param[2] = new SqlParameter("@CongThucTinhThuLao", ctItem.Oid);

                                        Utils.XuLyDuLieu(((XPObjectSpace)View.ObjectSpace).Session, "spd_PMS_TinhThuLaoGiangDay_HVNH", CommandType.StoredProcedure, param);
                                    }
                                }
                            }
                        }
                        View.ObjectSpace.Refresh();
                    }
                    XtraMessageBox.Show("Lấy dữ liệu thành công!");
                }
                else XtraMessageBox.Show("Vui lòng chọn kỳ tính lương!");

            }
        }

        private void popCheckThanhToan_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            BangThuLaoNhanVien obj = View.CurrentObject as BangThuLaoNhanVien;
            if (obj != null)
            {
                _obs = Application.CreateObjectSpace();
                ses = ((XPObjectSpace)_obs).Session;
                collectionSource = new CollectionSource(_obs, typeof(dsThanhToanThuLao));
                string sql = "SELECT ChiTietThuLaoNhanVien.Oid AS 'ChiTietThuLaoNhanVien',";
                sql += " HoTen, MaQuanLy,";
                sql += " ISNULL(SoTien,0) AS SoTien";
                sql += " FROM dbo.BangThuLaoNhanVien";
                sql += " JOIN dbo.ChiTietThuLaoNhanVien ON ChiTietThuLaoNhanVien.BangThuLaoNhanVien = BangThuLaoNhanVien.Oid";
                sql += " JOIN dbo.NhanVien ON NhanVien.Oid = ChiTietThuLaoNhanVien.NhanVien";
                sql += " JOIN dbo.HoSo ON HoSo.Oid = NhanVien.Oid";
                sql += " WHERE BangThuLaoNhanVien.Oid='" + obj.Oid + "'";
                sql += " AND BangThuLaoNhanVien.GCRecord IS NULL";
                sql += " AND ChiTietThuLaoNhanVien.GCRecord IS NULL";
                sql += " AND ISNULL(DaThanhToan,0)=0";

                DataTable dt = DataProvider.GetDataTable(sql, CommandType.Text);

                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        _dsThanhToanThuLao = new dsThanhToanThuLao(ses);
                        _dsThanhToanThuLao.ChiTietThuLaoNhanVien = new Guid(item["ChiTietThuLaoNhanVien"].ToString());
                        _dsThanhToanThuLao.HoTen = item["HoTen"].ToString();
                        _dsThanhToanThuLao.MaQuanLy = item["MaQuanLy"].ToString();
                        _dsThanhToanThuLao.SoTienThanhToan = Convert.ToDecimal(item["SoTien"]);
                        collectionSource.Add(_dsThanhToanThuLao);
                    }
                }
                e.View = Application.CreateListView("dsThanhToanThuLao_ListView", collectionSource, true);
            }
        }

        private void popCheckThanhToan_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_dsThanhToanThuLao != null)
            {
                BangThuLaoNhanVien obj = View.CurrentObject as BangThuLaoNhanVien;
                string ds = "";
                foreach (dsThanhToanThuLao item in collectionSource.List)
                {
                    ds += " UNION ALL " + "SELECT '" + item.ChiTietThuLaoNhanVien + "' as ChiTietThuLaoNhanVien";
                }
                if (ds != string.Empty)
                {
                    SqlParameter[] pThanhToan = new SqlParameter[3];
                    pThanhToan[0] = new SqlParameter("@ds", ds.Substring(10));
                    pThanhToan[1] = new SqlParameter("@BangThuLaoNhanVien", obj.Oid);
                    pThanhToan[2] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                    DataProvider.ExecuteNonQuery("spd_pms_BangThuLaoCanBo_DaThanhToan", CommandType.StoredProcedure, pThanhToan);
                    XtraMessageBox.Show("Cập nhật dữ liệu thành công!");
                    View.ObjectSpace.Refresh();
                }
            }
        }
    }
}