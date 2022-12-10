using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Validation;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    public class KhaiBao_KeKhaiSauGiang : BaseObject
    {
        private BoPhanView _BoPhanView;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;

        [ModelDefault("Caption", "Đơn vị")]
        [ImmediatePostData]
        [DataSourceProperty("listbp", DataSourcePropertyIsNullMode.SelectAll)]
        public BoPhanView BoPhanView
        {
            get { return _BoPhanView; }
            set
            {
                SetPropertyValue("BoPhanView", ref _BoPhanView, value);
                if (!IsLoading)
                    LoadDanhSachNhanVien();
            }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BacDaoTao BacDaoTao
        {
            get
            {
                return _BacDaoTao;
            }
            set
            {
                SetPropertyValue("BacDaoTao", ref _BacDaoTao, value);
                if (!IsLoading)
                    LoadDanhSachNhanVien();
            }
        }

        [ModelDefault("Caption", "Loại hình đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HeDaoTao HeDaoTao
        {
            get
            {
                return _HeDaoTao;
            }
            set
            {
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);
                if (!IsLoading)
                    LoadDanhSachNhanVien();
            }
        }

        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietKhaiBao_KeKhaiSauGiang> listKetKhai
        {
            get;
            set;
        }
        public KhaiBao_KeKhaiSauGiang(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
        void LoadDanhSachNhanVien()
        {
            listKetKhai.Reload();
            if (BoPhanView != null && BacDaoTao != null && HeDaoTao != null)
            {
                listKetKhai.Reload();
                XPCollection<NhanVienView> listNV = HamDungChung.getNhanVien(Session, BoPhanView.OidBoPhan);
                if(listNV!=null)
                {
                    foreach(NhanVienView item in listNV)
                    {
                        ChiTietKhaiBao_KeKhaiSauGiang ct = new ChiTietKhaiBao_KeKhaiSauGiang(Session);
                        ct.OidNhanVien = item.OidNhanVien;
                        ct.HoTen = item.HoTen;
                        listKetKhai.Add(ct);
                    }

                }
            }
        }

        [Browsable(false)]
        public XPCollection<BoPhanView> listbp { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            listKetKhai = new XPCollection<ChiTietKhaiBao_KeKhaiSauGiang>(Session, false);
            listbp = HamDungChung.getBoPhan(Session);
            OnChanged("listbp");
        }
    }
}