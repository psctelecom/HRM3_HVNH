using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.GioChuan;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.PMS.NonPersistentObjects.ThanhTra;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.UEL;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class CapNhatThanhTraGiangDay_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session _ses;
        CollectionSource collectionSource;
        ThanhTraGiangDay _source;
        Quanlythanhtra _KhoiLuong;
        QuanLyThanhTra_Non_UEL _source_UEL;

        public CapNhatThanhTraGiangDay_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "Quanlythanhtra_DetailView";

        }
        void ChonBoPhan_SoGio_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "VHU")
            {
                popCapNhatThanhTra.Active["TruyCap"] = true;
            }
            else
            {
                popCapNhatThanhTra.Active["TruyCap"] = false;
            }
        }

        private void popCapNhatThanhTra_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _KhoiLuong = View.CurrentObject as Quanlythanhtra;
            if(_KhoiLuong.ThongTinTruong.MaQuanLy == "UEL")
            {
                _obs = Application.CreateObjectSpace();
                _ses = ((XPObjectSpace)_obs).Session;
                _source_UEL = new QuanLyThanhTra_Non_UEL(_ses, _KhoiLuong.Oid);
                e.View = Application.CreateDetailView(_obs, _source_UEL);


            }
            else if (_KhoiLuong != null)
            {
                _obs = Application.CreateObjectSpace();
                _ses = ((XPObjectSpace)_obs).Session;
                collectionSource = new CollectionSource(_obs, typeof(ThanhTraGiangDay));
                _source = new ThanhTraGiangDay(_ses);
                _source.Quanlythanhtra = _ses.FindObject<Quanlythanhtra>(CriteriaOperator.Parse("Oid =?", _KhoiLuong.Oid));
                e.View = Application.CreateDetailView(_obs, _source);
            }  
        }

        private void popCapNhatThanhTra_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if(TruongConfig.MaTruong =="UEL")
            {
                string Sql = "";
                foreach(ChiTietKhoiLuongThanhTra_Non_UEL item in _source_UEL.listChiTiet )
                {
                    if(item.Chon == true)
                    {
                        Sql += "Union all \n" +
                               "Select '" + item.OidChiTiet.ToString() + "' AS OidKhoiLuong ,\n" +
                               " " + item.SoTietGhiNhan.ToString().Replace(",",".") + " AS SoTietGhiNhan, \n" +
                               " N'" + item.GhiChu + "' AS GhiChu\n";
                    }
                } 
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@QuanLyThanhTra", _KhoiLuong.Oid);
                parameter[1] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                if(Sql.Length < 11)
                {
                    XtraMessageBox.Show("Bạn phải chọn ít nhất 1 dòng check chọn để thực hiện chức năng này", "Lỗi");
                    return;
                }
                parameter[2] = new SqlParameter("@StringUpdate", Sql.Substring(11));
                parameter[3] = new SqlParameter("@KQ", SqlDbType.NVarChar, 200);
                parameter[3].Direction = ParameterDirection.Output;
                DataTable dt = DataProvider.GetDataTable("spd_PMS_CapNhat_CapNhatThanhTraGioGiang", System.Data.CommandType.StoredProcedure, parameter);

                string KQ = parameter[3].Value.ToString();
                if(KQ =="SUCCES")
                {
                    XtraMessageBox.Show("Cập nhật thành công", "Thành công");
                    View.Refresh();
                    View.ObjectSpace.Refresh();

                }
                else
                {
                    XtraMessageBox.Show("Lỗi ở : "+KQ, "Lỗi");
                    return;
                }
            }
            else
            {
                List<ChiTietThanhTraGiangDay> listChiTiet = (from d in _source.listChiTiet
                                                             where d.Chon == true
                                                             select d).ToList();
                if (listChiTiet != null)
                {
                    string sql = "";
                    string SoTiet = "";
                    string ThoiDiem = "";
                    string GhiChu = "";
                    foreach (var item in listChiTiet)
                    {
                        SoTiet = item.SoTietGhiNhan.ToString().Replace(",", ".");
                        if (item.ThoiDiemThanhLy != DateTime.MinValue)
                            ThoiDiem = item.ThoiDiemThanhLy.ToShortDateString();
                        else
                            ThoiDiem = "";
                        GhiChu = item.GhiChu;
                        sql += " Union All Select '" + item.OidChiTiet + "' as OidChiTiet,"
                            + " '" + SoTiet + "' as SoTiet,"
                            + " '" + ThoiDiem + "' as ThoiDiemThanhLy,"
                            + " '" + GhiChu + "' as GhiChu";
                    }

                    SqlCommand cmd = new SqlCommand("spd_PMS_CapNhatThanhTraGioGiang", DataProvider.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 1800;
                    cmd.Parameters.AddWithValue("@string", sql.Substring(11));
                    cmd.Parameters.AddWithValue("@user", HamDungChung.CurrentUser().UserName);
                    cmd.ExecuteNonQuery();
                    View.ObjectSpace.Refresh();
                }
            }

            
        }
    }
}
