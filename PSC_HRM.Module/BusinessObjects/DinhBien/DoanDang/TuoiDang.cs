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
    [ModelDefault("Caption", "Tuổi Đảng")]
    public class TuoiDang : BaseObject, ISupportController
    {
        // Fields...
        //private bool _Chon;
        private ToChucDang _ChiBo;
        private DangVien _DangVien;
        private DateTime _NgayVaoDang;
        private DateTime _NgayVaoDangChinhThuc;
        private DateTime _NgayQuyetDinhChinhThuc;
        private int _TuoiDang;
        
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

        [ModelDefault("Caption", "Chi bộ")]
        public ToChucDang ChiBo
        {
            get
            {
                return _ChiBo;
            }
            set
            {
                SetPropertyValue("ChiBo", ref _ChiBo, value);
            }
        }

        [ModelDefault("Caption", "Đảng viên")]
        public DangVien DangVien
        {
            get
            {
                return _DangVien;
            }
            set
            {
                SetPropertyValue("DangVien", ref _DangVien, value);
            }
        }
        
        [ModelDefault("Caption", "Tuổi Đảng")]
        public int Tuoi
        {
            get
            {
                return _TuoiDang;
            }
            set
            {
                SetPropertyValue("Tuoi", ref _TuoiDang, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào Đảng dự bị")]
        public DateTime NgayVaoDang
        {
            get
            {
                return _NgayVaoDang;
            }
            set
            {
                SetPropertyValue("NgayVaoDang", ref _NgayVaoDang, value);
                if (!IsLoading && value != DateTime.MinValue)
                    Tuoi = (int)(DateTime.Now - value).TotalDays / 365;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày vào Đảng chính thức")]
        public DateTime NgayVaoDangChinhThuc
        {
            get
            {
                return _NgayVaoDangChinhThuc;
            }
            set
            {
                SetPropertyValue("NgayVaoDangChinhThuc", ref _NgayVaoDangChinhThuc, value);
                
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày quyết định chính thức")]
        public DateTime NgayQuyetDinhChinhThuc
        {
            get
            {
                return _NgayQuyetDinhChinhThuc;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinhChinhThuc", ref _NgayQuyetDinhChinhThuc, value);
            }
        }
        
        public TuoiDang(Session session) : base(session) { }
    }

}
