using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.NghiepVu.KhaoThi
{
    [ModelDefault("Caption", "Chi tiết coi thi / chấm thi")]
    public class ChiTietCoiThi_ChamThi : BaseObject
    {
        #region 1. Key
        private QuanLyKhaoThi _QuanLyKhaoThi;
        [Association("QuanLyKhaoThi-ListChiTietCoiThi_ChamThi")]
        [ModelDefault("Caption", "Quản lý coi thi / chấm thi")]
        [Browsable(false)]
        public QuanLyKhaoThi QuanLyKhaoThi
        {
            get
            {
                return _QuanLyKhaoThi;
            }
            set
            {
                SetPropertyValue("QuanLyKhaoThi", ref _QuanLyKhaoThi, value);
            }
        }
        #endregion
        #region 2. Khai báo 
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private decimal _TongTGQuyDoi;
        private decimal _TGConLai; 
        #endregion
        #region 3.HUFLIT
        private decimal _TongSoGioBiTru;
        #endregion

        [ModelDefault("Caption","Bộ phận")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }
        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }
        [ModelDefault("Caption", "Tổng TG quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongTGQuyDoi
        {
            get { return _TongTGQuyDoi; }
            set
            {
                SetPropertyValue("TongTGQuyDoi", ref _TongTGQuyDoi, value);
            }
        }

        [ModelDefault("Caption", "Tổng số giờ bị trừ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongSoGioBiTru
        {
            get { return _TongSoGioBiTru; }
            set
            {
                SetPropertyValue("TongSoGioBiTru", ref _TongSoGioBiTru, value);
            }
        }

        [ModelDefault("Caption", "TG còn lại")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TGConLai
        {
            get { return _TGConLai; }
            set
            {
                SetPropertyValue("TGConLai", ref _TGConLai, value);
            }
        }

        //

        [Aggregated]
        [Association("ChiTietCoiThi_ChamThi-ListCoiThi_ChamThi")]
        [ModelDefault("Caption", "Coi thi/Chấm thi")]
        public XPCollection<CoiThi_ChamThi> ListChiTietCoiThi_ChamThi
        {
            get
            {
                return GetCollection<CoiThi_ChamThi>("ListChiTietCoiThi_ChamThi");
            }
        }
        public ChiTietCoiThi_ChamThi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
