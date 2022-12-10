using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Khai báo khối lượng giảng dạy")]
    public class KhaiBao_KhoiLuongGiangDay : BaseObject
    {
        private BoPhanView _BoPhanView;
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
        [ModelDefault("Caption", "Chi tiết ")]
        public XPCollection<ChiTietKhaiBao_KhoiLuongGiangDay> listKetKhai
        {
            get;
            set;
        }
        public KhaiBao_KhoiLuongGiangDay(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
        void LoadDanhSachNhanVien()
        {
            listKetKhai.Reload();
            if (BoPhanView != null)
            {
                listKetKhai.Reload();
                XPCollection<NhanVienView> listNV = HamDungChung.getNhanVien(Session, BoPhanView.OidBoPhan);
                if(listNV!=null)
                {
                    foreach(NhanVienView item in listNV)
                    {
                        ChiTietKhaiBao_KhoiLuongGiangDay ct = new ChiTietKhaiBao_KhoiLuongGiangDay(Session);
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
            listKetKhai = new XPCollection<ChiTietKhaiBao_KhoiLuongGiangDay>(Session, false);
            listbp = HamDungChung.getBoPhan(Session);
            OnChanged("listbp");
        }
    }
}