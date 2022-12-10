using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DoanDang
{
    [NonPersistent]
    [ModelDefault("Caption", "Tuổi Đoàn")]
    public class TuoiDoan : BaseObject, IBoPhan, ISupportController
    {
        // Fields...
        //private bool _Chon;
        private int _Tuoi;
        private DateTime _NgayKetNap;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;

        //[ModelDefault("Caption", "Chọn")]
        //public bool Chon
        //{
        //    get
        //    {
        //        return _Chon;
        //    }
        //    set
        //    {
        //        SetPropertyValue("Chon", ref _Chon, value);
        //    }
        //}

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    BoPhan = value.BoPhan;
                }
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày kết nạp")]
        public DateTime NgayKetNap
        {
            get
            {
                return _NgayKetNap;
            }
            set
            {
                SetPropertyValue("NgayKetNap", ref _NgayKetNap, value);
                if (!IsLoading && NgayKetNap != DateTime.MinValue)
                {
                    Tuoi = DateTime.Today.Year - NgayKetNap.Year;
                }
            }
        }

        [ModelDefault("Caption", "Tuổi đoàn")]
        public int Tuoi
        {
            get
            {
                return _Tuoi;
            }
            set
            {
                SetPropertyValue("Tuoi", ref _Tuoi, value);
            }
        }

        public TuoiDoan(Session session) : base(session) { }
    }
}
