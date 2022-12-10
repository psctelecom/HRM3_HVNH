using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.NghiepVu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn NV Cập Nhật")]
    public class ChiTietThuLao_NhanVien_Update : BaseObject
    {
        private Guid _BangThuLaoNhanVien;
        private ThongTinTruong _ThongTinTruong;
        private BoPhan _BoPhan;
        
        //

        [Browsable(false)]
        public Guid BangThuLaoNhanVien
        {
            get { return _BangThuLaoNhanVien; }
            set { SetPropertyValue("BangThuLaoNhanVien", ref _BangThuLaoNhanVien, value); }
        }

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Browsable(false)]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Danh sách nhân viên")]
        public XPCollection<dsChiTietThuLao_NhanVien_Update> listChiTietThuLao
        {
            get;
            set;
        }
    
        [ModelDefault("Caption", "Bộ phận")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    LoadData();
                }
            }
        }
        //
        public void LoadData()
        {
            using (DialogUtil.AutoWait("Đang lấy danh sách bảng chốt"))
            {
                listChiTietThuLao.Reload();
                if (BoPhan != null)
                {              
                    SqlParameter[] param = new SqlParameter[1]; /*Số parameter trên Store Procedure*/
                    param[0] = new SqlParameter("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty);
                    DataTable dt = DataProvider.GetDataTable("spd_pms_BangThuLaoCanBo_SuaChiTietThuLao", System.Data.CommandType.StoredProcedure, param);
                    if (dt != null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            dsChiTietThuLao_NhanVien_Update BangChotThuLao = new dsChiTietThuLao_NhanVien_Update(Session);
                            if (item["Oid"].ToString() != string.Empty)
                                BangChotThuLao.OidThongTinBangChot = new Guid(item["ThongTinBangChot"].ToString());// Dòng để lấy ra dược thay đổi
                            BangChotThuLao.OidChiTietBangChotThuLaoGiangDay = new Guid(item["oid"].ToString());
                            BangChotThuLao.MaQuanLy = item["MaGiangVien"].ToString();
                            if (item["LopHocPhan"].ToString() != string.Empty)
                                BangChotThuLao.LopHocPhan = item["LopHocPhan"].ToString();
                            if (item["TenHoatDong"].ToString() != string.Empty)
                                BangChotThuLao.TenHoatDong = item["TenHoatDong"].ToString();
                            BangChotThuLao.BacDaoTao = item["TenBacDaoTao"].ToString();
                            BangChotThuLao.NhanVien = item["HoTen"].ToString();
                            BangChotThuLao.TongGio = Convert.ToDecimal(item["TongGio"].ToString());
                            BangChotThuLao.TongGioA1 = Convert.ToDecimal(item["TongGioA1"].ToString());
                            BangChotThuLao.TongGioA1 = Convert.ToDecimal(item["TongGioA2"].ToString());
                            BangChotThuLao.TongNo = Convert.ToDecimal(item["TongNo"].ToString());
                            BangChotThuLao.SoTienThanhToan = Convert.ToDecimal(item["SoTienThanhToan"].ToString());
                            BangChotThuLao.CongTru = 0;
                            listChiTietThuLao.Add(BangChotThuLao);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bảng chốt chưa khóa!", "Thông báo");
                    }
                }
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            listChiTietThuLao = new XPCollection<dsChiTietThuLao_NhanVien_Update>(Session, false);
            
        }

        public ChiTietThuLao_NhanVien_Update(Session session)
            : base(session)
        { }


    }
}
