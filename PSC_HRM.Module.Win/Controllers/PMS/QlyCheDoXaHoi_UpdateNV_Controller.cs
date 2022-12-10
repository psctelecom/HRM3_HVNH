using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class QlyCheDoXaHoi_UpdateNV_Controller : ViewController
    {

        IObjectSpace _obs = null;
        Session _ses;
        CollectionSource collectionSource;
        QlyCheDoXaHoi_UpdateNV_DM _source;
        public QlyCheDoXaHoi_UpdateNV_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QlyCheDoXaHoi_UpdateNV_DM_DetailView";
        }
        void QlyCheDoXaHoi_UpdateNV_Controller_ViewControlsCreated(object sender, System.EventArgs e)
        {
            _source = View.CurrentObject as QlyCheDoXaHoi_UpdateNV_DM;
            DetailView view = View as DetailView;
            if (_source != null)
            {
                #region Load
                ControlDetailItem itemLoad = ((DetailView)View).FindItem("btLoadDuLieu") as ControlDetailItem;
                //
                if (itemLoad != null)
                {
                    SimpleButton btnLoad = itemLoad.Control as SimpleButton;
                    if (btnLoad != null)
                    {
                        btnLoad.Text = "Load dữ liệu";
                        //btnSearch.Width = 80;
                        btnLoad.Click += (obj, ea) =>
                        {
                            if (_source.CheDoXaHoi != null)
                            {
                                if (!_source.CheDoXaHoi.TinhTheoThang)
                                {
                                    DialogResult result = XtraMessageBox.Show("Bạn có muốn lọc theo độ tuổi?", "Thông báo", MessageBoxButtons.YesNo);
                                    if (result == DialogResult.Yes)
                                    {
                                        return;
                                    }
                                }
                                _source.Load();
                            }
                            else
                            {
                                XtraMessageBox.Show("Vui lòng chọn chế độ!");
                            }
                        };

                    }
                }
                #endregion
                #region Push
                ControlDetailItem itemThucThi = ((DetailView)View).FindItem("btThucThi") as ControlDetailItem;
                //
                if (itemThucThi != null)
                {
                    SimpleButton btnThucThi = itemThucThi.Control as SimpleButton;
                    if (btnThucThi != null)
                    {
                        btnThucThi.Text = "Thực thi";
                        //btnSearch.Width = 80;
                        btnThucThi.Click += (obj, ea) =>
                        {//spd_pms_KeKhaiCheDoXaHoi
                            string sql = "";
                            List<dsDMucCheDoXaHoi> list = (from d in _source.listNV
                                                           where d.Chon == true
                                                           select d).ToList();

                            foreach (var item in list)
                            {
                                sql += " Union All select '" + item.OidNV.ToString() + "' as NhanVien";
                            }

                            //try
                            //{
                            if (sql != "")
                            {
                                SqlCommand cmd = new SqlCommand("spd_pms_KeKhaiCheDoXaHoi", DataProvider.GetConnection());
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@NhanVien", sql.Substring(11));
                                cmd.Parameters.AddWithValue("@TuNgay", _source.TuNgay != DateTime.MinValue ? _source.TuNgay.ToShortDateString() : "");
                                cmd.Parameters.AddWithValue("@DenNgay", _source.DenNgay != DateTime.MinValue ? _source.DenNgay.ToShortDateString() : "");
                                cmd.Parameters.AddWithValue("@CheDo", _source.CheDoXaHoi.Oid.ToString());
                                cmd.Parameters.AddWithValue("@NamHoc", _source.NamHoc.Oid.ToString());
                                cmd.Parameters.AddWithValue("@HocKy", _source.HocKy.Oid.ToString());
                                cmd.ExecuteNonQuery();
                                XtraMessageBox.Show("Tạo dữ liệu chế độ xã hội thành công!", "Thông báo");
                            }
                            else
                                XtraMessageBox.Show("Không tìm thấy nhân viên/cán bộ", "Thông báo");
                            //}
                            //catch (Exception)
                            //{
                            //    XtraMessageBox.Show("Có lỗi xảy ra, vui lòng kiểm tra lại dữ liệu", "Thông báo");
                            //}
                        };
                    }
                }
                #endregion
            }
        }
    }
}
