using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.BaoMat;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.NghiepVu;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn danh sách in hợp đồng")]
    [Appearance("Hide_ChonKhoiLuongGiangDay_1", TargetItems = "HeDaoTao;BacDaoTao;listKhoiLuong"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "AnHien")]
    [Appearance("Hide_ChonKhoiLuongGiangDay_2", TargetItems = "NamHoc"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "!AnHien")]
    public class ChonKhoiLuongGiangDay : BaseObject
    {
        private NamHoc _NamHoc;
        private HeDaoTao _HeDaoTao;
        private BacDaoTao _BacDaoTao;
        private bool _AnHien;

        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        [ImmediatePostData]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set
            {
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);      
                if(!IsLoading && value != null && BacDaoTao != null)
                {
                    UpdatelistKhoiLuong();
                }     
            }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        [ImmediatePostData]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set
            {
                SetPropertyValue("BacDaoTao", ref _BacDaoTao, value);
                if (!IsLoading && value != null && HeDaoTao != null)
                {
                    UpdatelistKhoiLuong();
                }
            }
        }

        [ModelDefault("Caption", "Ẩn hiện")]
        [Browsable(false)]
        public bool AnHien
        {
            get { return _AnHien; }
            set
            {
                SetPropertyValue("AnHien", ref _AnHien, value);           
            }
        }

        [ModelDefault("Caption", "Danh sách khối lượng")]
        public XPCollection<dsChonKhoiLuongGiangDay> listKhoiLuong
        {
            get;
            set;
        }

        public ChonKhoiLuongGiangDay(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            listKhoiLuong = new XPCollection<dsChonKhoiLuongGiangDay>(Session, false);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }

        public void UpdatelistKhoiLuong()
        {
            if(NamHoc != null && BacDaoTao != null && HeDaoTao != null)
            {
                if(listKhoiLuong != null)
                {
                    listKhoiLuong.Reload();
                }
                else
                {
                    listKhoiLuong = new XPCollection<dsChonKhoiLuongGiangDay>(Session, false);
                }

                CriteriaOperator fchitiet = CriteriaOperator.Parse("KhoiLuongGiangDay.NamHoc = ? and HeDaoTao = ? and BacDaoTao = ? and GCRecord IS NULL", NamHoc.Oid, HeDaoTao.Oid, BacDaoTao.Oid);
                XPCollection<ChiTietKhoiLuongGiangDay> dsChiTiet = new XPCollection<ChiTietKhoiLuongGiangDay>(Session, fchitiet);
                foreach(ChiTietKhoiLuongGiangDay item in dsChiTiet)
                {
                    dsChonKhoiLuongGiangDay dschon = new dsChonKhoiLuongGiangDay(Session);
                    dschon.ChiTietKhoiLuongGiangDay = item;
                    listKhoiLuong.Add(dschon);
                }
            }
        }
    }

}
