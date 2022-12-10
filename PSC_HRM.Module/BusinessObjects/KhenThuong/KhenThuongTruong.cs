using System;
using System.ComponentModel;
using DevExpress.Xpo;
using System.Linq;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.KhenThuong
{
    [DefaultProperty("Caption")]
    [ImageName("BO_QuanLyKhenThuong")]
    [ModelDefault("Caption", "Khen thưởng của Trường")]
    [RuleCombinationOfPropertiesIsUnique("KhenThuongTruong", DefaultContexts.Save, "QuanLyKhenThuong;DanhHieuKhenThuong")]
    public class KhenThuongTruong : BaseObject
    {
        // Fields...
        private DateTime _NgayLap;
        private DanhHieuKhenThuong _DanhHieuKhenThuong;
        private QuanLyKhenThuong _QuanLyKhenThuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý khen thưởng")]
        [Association("QuanLyKhenThuong-ListKhenThuongTruong")]
        public QuanLyKhenThuong QuanLyKhenThuong
        {
            get
            {
                return _QuanLyKhenThuong;
            }
            set
            {
                SetPropertyValue("QuanLyKhenThuong", ref _QuanLyKhenThuong, value);
            }
        }

        [ModelDefault("Caption", "Danh hiệu khen thưởng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DanhHieuKhenThuong DanhHieuKhenThuong
        {
            get
            {
                return _DanhHieuKhenThuong;
            }
            set
            {
                SetPropertyValue("DanhHieuKhenThuong", ref _DanhHieuKhenThuong, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }

        [Browsable(false)]
        public string Caption
        {
            get
            {
                if (QuanLyKhenThuong != null
                    && DanhHieuKhenThuong != null)
                    return string.Format("{0} {1}", QuanLyKhenThuong.NamHoc.TenNamHoc, DanhHieuKhenThuong.TenDanhHieu);
                return "";
            }
        }

        public KhenThuongTruong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLap = HamDungChung.GetServerTime();
        }
    }

}
