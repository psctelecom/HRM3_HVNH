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
using PSC_HRM.Module.ThuNhap.ThuLao;
using System.Windows.Forms;
using PSC_HRM.Module.PMS.NghiepVu.NCKH;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class NghienCuuKHQuanLyNon_LayDuLieu_Controllery : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        QuanLyNCKH_Non _HoatDong;
        public NghienCuuKHQuanLyNon_LayDuLieu_Controllery()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyNCKH_Non_DetailView";
        }

        private void btSearch_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _HoatDong = View.CurrentObject as QuanLyNCKH_Non;
            if(_HoatDong != null)
            {
                using (DialogUtil.AutoWait())
                {
                    _HoatDong.LoadData();
                }
            }
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string Chuoi = "";
            object kq;
            _HoatDong = View.CurrentObject as QuanLyNCKH_Non;
            if (_HoatDong != null)
            {
                foreach (var item in _HoatDong.DanhSach)
                {
                    if (item.Chon)
                    {
                        Chuoi += item.OidKey + ";";
                    }
                }

                if (Chuoi == "")
                {
                    MessageBox.Show("Vui lòng chọn dòng cần xác nhận!");
                }
                else
                {
                    if (DialogUtil.ShowYesNo(string.Format("Bạn có muốn xác nhận hoạt động?")) == DialogResult.Yes)
                    {
                        using (DialogUtil.AutoWait())
                        {
                            SqlParameter[] param = new SqlParameter[2];
                            param[0] = new SqlParameter("@Chuoi", Chuoi);
                            param[1] = new SqlParameter("@Duyet", true);
                            kq = DataProvider.GetValueFromDatabase("spd_NghienCuuKhoaHoc_XacNhan_HuyXacNhan", CommandType.StoredProcedure, param);

                            _HoatDong.LoadData();
                        }
                        MessageBox.Show(kq.ToString(), "Thông báo");
                    }
                }
            }
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string Chuoi = "";
            object kq;

            _HoatDong = View.CurrentObject as QuanLyNCKH_Non;
            if (_HoatDong != null)
            {
                foreach (var item in _HoatDong.DanhSach)
                {
                    if (item.Chon)
                    {
                        Chuoi += item.OidKey + ";";
                    }
                }
                if (Chuoi == "")
                {
                    MessageBox.Show("Vui lòng chọn dòng cần hủy xác nhận!");
                }
                else
                {
                    if (DialogUtil.ShowYesNo(string.Format("Bạn có muốn hủy xác nhận hoạt động?")) == DialogResult.Yes)
                    {
                        using (DialogUtil.AutoWait())
                        {
                            SqlParameter[] param = new SqlParameter[2];
                            param[0] = new SqlParameter("@Chuoi", Chuoi);
                            param[1] = new SqlParameter("@Duyet", false);
                            kq = DataProvider.GetValueFromDatabase("spd_NghienCuuKhoaHoc_XacNhan_HuyXacNhan", CommandType.StoredProcedure, param);

                            _HoatDong.LoadData();
                        }
                        MessageBox.Show(kq.ToString(), "Thông báo");
                    }
                }
            }
        }

        private void simpleAction3_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string Chuoi = "";
            object kq;
            _HoatDong = View.CurrentObject as QuanLyNCKH_Non;
            if (_HoatDong != null)
            {
                foreach (var item in _HoatDong.DanhSach)
                {
                    if (item.Chon)
                    {
                        Chuoi += item.OidKey + ";";
                    }
                }

                if (Chuoi == "")
                {
                    MessageBox.Show("Vui lòng chọn dòng cần từ chối!");
                }
                else
                {
                    if (DialogUtil.ShowYesNo(string.Format("Bạn có muốn từ chối hoạt động?")) == DialogResult.Yes)
                    {
                        using (DialogUtil.AutoWait())
                        {
                            SqlParameter[] param = new SqlParameter[2];
                            param[0] = new SqlParameter("@Chuoi", Chuoi);
                            param[1] = new SqlParameter("@Duyet", true);
                            kq = DataProvider.GetValueFromDatabase("spd_NghienCuuKhoaHoc_TuChoi_HuyTuChoi", CommandType.StoredProcedure, param);

                            _HoatDong.LoadData();
                        }
                        MessageBox.Show(kq.ToString(), "Thông báo");
                    }
                }
            }
        }

        private void simpleAction4_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string Chuoi = "";
            object kq;

            _HoatDong = View.CurrentObject as QuanLyNCKH_Non;
            if (_HoatDong != null)
            {
                foreach (var item in _HoatDong.DanhSach)
                {
                    if (item.Chon)
                    {
                        Chuoi += item.OidKey + ";";
                    }
                }
                if (Chuoi == "")
                {
                    MessageBox.Show("Vui lòng chọn dòng cần hủy từ chối!");
                }
                else
                {
                    if (DialogUtil.ShowYesNo(string.Format("Bạn có muốn hủy từ chối hoạt động?")) == DialogResult.Yes)
                    {
                        using (DialogUtil.AutoWait())
                        {
                            SqlParameter[] param = new SqlParameter[2];
                            param[0] = new SqlParameter("@Chuoi", Chuoi);
                            param[1] = new SqlParameter("@Duyet", false);
                            kq = DataProvider.GetValueFromDatabase("spd_NghienCuuKhoaHoc_TuChoi_HuyTuChoi", CommandType.StoredProcedure, param);

                            _HoatDong.LoadData();
                        }
                        MessageBox.Show(kq.ToString(), "Thông báo");
                    }
                }
            }
        }

        private void simpleAction5_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string Chuoi = "";
            object kq;
            _HoatDong = View.CurrentObject as QuanLyNCKH_Non;
            if (_HoatDong != null)
            {
                foreach (var item in _HoatDong.DanhSach)
                {
                    if (item.Chon)
                    {
                        Chuoi += item.OidKey + ";";
                    }
                }

                if (Chuoi == "")
                {
                    MessageBox.Show("Vui lòng chọn dòng cần check!");
                }
                else
                {
                    if (DialogUtil.ShowYesNo(string.Format("Bạn có muốn check hoạt động?")) == DialogResult.Yes)
                    {
                        using (DialogUtil.AutoWait())
                        {
                            SqlParameter[] param = new SqlParameter[2];
                            param[0] = new SqlParameter("@Chuoi", Chuoi);
                            param[1] = new SqlParameter("@Duyet", true);
                            kq = DataProvider.GetValueFromDatabase("spd_NghienCuuKhoaHoc_DaThanhToan", CommandType.StoredProcedure, param);

                            _HoatDong.LoadData();
                        }
                        MessageBox.Show(kq.ToString(), "Thông báo");
                    }
                }
            }
        }

        private void simpleAction6_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string Chuoi = "";
            object kq;
            _HoatDong = View.CurrentObject as QuanLyNCKH_Non;
            if (_HoatDong != null)
            {
                foreach (var item in _HoatDong.DanhSach)
                {
                    if (item.Chon)
                    {
                        Chuoi += item.OidKey + ";";
                    }
                }

                if (Chuoi == "")
                {
                    MessageBox.Show("Vui lòng chọn dòng cần hủy check!");
                }
                else
                {
                    if (DialogUtil.ShowYesNo(string.Format("Bạn có muốn hủy check hoạt động?")) == DialogResult.Yes)
                    {
                        using (DialogUtil.AutoWait())
                        {
                            SqlParameter[] param = new SqlParameter[2];
                            param[0] = new SqlParameter("@Chuoi", Chuoi);
                            param[1] = new SqlParameter("@Duyet", false);
                            kq = DataProvider.GetValueFromDatabase("spd_NghienCuuKhoaHoc_DaThanhToan", CommandType.StoredProcedure, param);

                            _HoatDong.LoadData();
                        }
                        MessageBox.Show(kq.ToString(), "Thông báo");
                    }
                }
            }
        }
    }
}