using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module.QuyetDinh;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.ThoiViec;

namespace PSC_HRM.Module.ThoiViec
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách cán bộ thôi việc")]
    public class TimKiemCanBoThoiViec : TruongBaseObject
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Từ ngày")]
        [ImmediatePostData]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ImmediatePostData]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<DanhSachCanBoThoiViec> ListCanBo { get; set; }

        public TimKiemCanBoThoiViec(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ListCanBo = new XPCollection<DanhSachCanBoThoiViec>(Session, false);
            //
            DateTime current = HamDungChung.GetServerTime();
            TuNgay = new DateTime(current.Year, current.Month, 1);
            DenNgay = TuNgay.AddMonths(1).AddDays(-1);
        }

        public void LoadData()
        {
            if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue &&
                TuNgay <= DenNgay)
            {
                //
                ListCanBo.Reload();
                //
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@TuNgay", this.TuNgay);
                param[1] = new SqlParameter("@DenNgay", this.DenNgay);
                //
                DataTable dt = DataProvider.GetDataTable("spd_TimKiemDanhSachNghiViec", CommandType.StoredProcedure, param);
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DanhSachCanBoThoiViec DanhSach;
                        foreach (DataRow item in dt.Rows)
                        {
                            DanhSach = new DanhSachCanBoThoiViec(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                                DanhSach.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                            if (DanhSach.ThongTinNhanVien != null)
                            {                            
                                DanhSach.TuNgay = DateTime.Parse(item["TuNgay"].ToString());
                            }
                            //
                            ListCanBo.Add(DanhSach);
                        }
                    }
                }          
            }
        }
    }

}
