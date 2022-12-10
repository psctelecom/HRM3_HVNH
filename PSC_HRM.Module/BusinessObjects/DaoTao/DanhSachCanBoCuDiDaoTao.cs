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

namespace PSC_HRM.Module.DaoTao
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách cán bộ cử đi đào tạo")]
    public class DanhSachCanBoCuDiDaoTao : TruongBaseObject
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
        public XPCollection<CanBoCuDiDaoTao> ListCanBoCuDiDaoTao { get; set; }

        public DanhSachCanBoCuDiDaoTao(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ListCanBoCuDiDaoTao = new XPCollection<CanBoCuDiDaoTao>(Session, false);
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
                ListCanBoCuDiDaoTao.Reload();
                //
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@TuNgay", this.TuNgay);
                param[1] = new SqlParameter("@DenNgay", this.DenNgay);
                //
                DataTable dt = DataProvider.GetDataTable("spd_Notification_CanBoCuDiDaoTao", CommandType.StoredProcedure, param);
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CanBoCuDiDaoTao canBo;
                        foreach (DataRow item in dt.Rows)
                        {
                            canBo = new CanBoCuDiDaoTao(Session);
                            QuyetDinhDaoTao qd = Session.GetObjectByKey<QuyetDinhDaoTao>(new Guid(item["Oid"].ToString()));

                            canBo.SoQuyetDinh = qd.SoQuyetDinh != null ? qd.SoQuyetDinh.ToString() : null;

                            if (!item.IsNull("ThongTinNhanVien"))
                                canBo.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));

                            if (!item.IsNull("TinhTrangQuyetDinh"))
                                canBo.TinhTrangQuyetDinh = Session.GetObjectByKey<TinhTrang>(new Guid(item["TinhTrangQuyetDinh"].ToString()));

                            if (!item.IsNull("TinhTrangHienTai"))
                                canBo.TinhTrangHienTai = Session.GetObjectByKey<TinhTrang>(new Guid(item["TinhTrangHienTai"].ToString()));

                            if (!item.IsNull("TrinhDoChuyenMon"))
                                canBo.TrinhDoChuyenMon = Session.GetObjectByKey<TrinhDoChuyenMon>(new Guid(item["TrinhDoChuyenMon"].ToString()));
                            
                            if (!item.IsNull("TruongDaoTao"))
                                canBo.TruongDaoTao = Session.GetObjectByKey<TruongDaoTao>(new Guid(item["TruongDaoTao"].ToString()));

                            try
                            {
                                canBo.TuNgay = item["TuNgay"] != null ? Convert.ToDateTime(item["TuNgay"]) : DateTime.MinValue;
                            }
                            catch (Exception ex)
                            {
                                canBo.TuNgay = DateTime.MinValue;
                            }

                            try
                            {
                                canBo.DenNgay = item["DenNgay"] != null ? Convert.ToDateTime(item["DenNgay"]) : DateTime.MinValue;
                            }
                            catch (Exception ex)
                            {
                                canBo.DenNgay = DateTime.MinValue;
                            }

                            if (!item.IsNull("ChuyenMonDaoTao"))
                                canBo.ChuyenMonDaoTao = Session.GetObjectByKey<ChuyenMonDaoTao>(new Guid(item["ChuyenMonDaoTao"].ToString()));

                            if (!item.IsNull("QuocGia"))
                                canBo.QuocGia = Session.GetObjectByKey<QuocGia>(new Guid(item["QuocGia"].ToString()));

                            if (!item.IsNull("BoPhan"))
                                canBo.BoPhan = Session.GetObjectByKey<BoPhan>(new Guid(item["BoPhan"].ToString()));
                            //
                            ListCanBoCuDiDaoTao.Add(canBo);
                        }
                    }
                }          
            }
        }
    }

}
