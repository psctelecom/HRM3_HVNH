using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.NghiepVu.KhaoThi
{
    [ModelDefault("Caption", "Danh sách sinh viên tham gia thi")]
    public class DanhSachSVThamGiaThi : BaseObject
    {
        #region 1. Key
        private QuanLyKhaoThi _QuanLyKhaoThi;
        [Association("QuanLyKhaoThi-ListDanhSachSVThamGiaThi")]
        [ModelDefault("Caption", "Quản lý khảo thí")]
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
        private HocKy _HocKy;
        private BoPhan _BoPhan;
        private int _SoLuongSV;
        #endregion

        [ModelDefault("Caption", "Học kỳ")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Số lượt sinh viên")]
        public int SoLuongSV
        {
            get { return _SoLuongSV; }
            set
            {
                SetPropertyValue("SoLuongSV", ref _SoLuongSV, value);
            }
        }

        public DanhSachSVThamGiaThi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
