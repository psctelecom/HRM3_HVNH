using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuaTrinh
{
    [ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình nghiên cứu khoa học")]
    public class QuaTrinhNghienCuuKhoaHoc : BaseObject
    {
        private int _STT;
        private int _TuNam;
        private int _DenNam;
        private string _CapQuanLy;
        private string _CoQuanChuTri;
        private string _ChucDanhThamGia;
        private string _TenDeTai;
        private DateTime _NgayNghiemThu;
        private string _NoiQuanLyKetQua;
        private HoSo.HoSo _HoSo;
        
        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        public HoSo.HoSo HoSo
        {
            get
            {
                return _HoSo;
            }
            set
            {
                SetPropertyValue("HoSo", ref _HoSo, value);
                if (!IsLoading && value != null)
                {
                    object obj = Session.Evaluate<QuaTrinhNghienCuuKhoaHoc>(CriteriaOperator.Parse("COUNT()"), CriteriaOperator.Parse("HoSo=?", value));
                    if (obj != null)
                        STT = (int)obj + 1;
                    else
                        STT = 1;
                }
            }
        }

        [ModelDefault("Caption", "Số thứ tự")]
        public int STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        [ModelDefault("Caption", "Từ năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        public int TuNam
        {
            get
            {
                return _TuNam;
            }
            set
            {
                SetPropertyValue("TuNam", ref _TuNam, value);
            }
        }

        [ModelDefault("Caption", "Đến năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        public int DenNam
        {
            get
            {
                return _DenNam;
            }
            set
            {
                SetPropertyValue("DenNam", ref _DenNam, value);
            }
        }

        [ModelDefault("Caption", "Cấp quản lý")]
        public string CapQuanLy
        {
            get
            {
                return _CapQuanLy;
            }
            set
            {
                SetPropertyValue("CapQuanLy", ref _CapQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Cơ quan chủ trì")]
        public string CoQuanChuTri
        {
            get
            {
                return _CoQuanChuTri;
            }
            set
            {
                SetPropertyValue("CoQuanChuTri", ref _CoQuanChuTri, value);
            }
        }

        [ModelDefault("Caption", "Chức danh tham gia")]
        public string ChucDanhThamGia
        {
            get
            {
                return _ChucDanhThamGia;
            }
            set
            {
                SetPropertyValue("ChucDanhThamGia", ref _ChucDanhThamGia, value);
            }
        }

        [Size(8000)]
        [ModelDefault("Caption", "Tên đề tài")]
        public string TenDeTai
        {
            get
            {
                return _TenDeTai;
            }
            set
            {
                SetPropertyValue("TenDeTai", ref _TenDeTai, value);
            }
        }

        [ModelDefault("Caption", "Ngày nghiệm thu")]
        public DateTime NgayNghiemThu
        {
            get
            {
                return _NgayNghiemThu;
            }
            set
            {
                SetPropertyValue("NgayNghiemThu", ref _NgayNghiemThu, value);
            }
        }

        [ModelDefault("Caption", "Nơi quản lý kết quả")]
        public string NoiQuanLyKetQua
        {
            get
            {
                return _NoiQuanLyKetQua;
            }
            set
            {
                SetPropertyValue("NoiQuanLyKetQua", ref _NoiQuanLyKetQua, value);
            }
        }

        public QuaTrinhNghienCuuKhoaHoc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Module.HoSo.HoSo.CurrentHoSo != null)
                HoSo = Session.GetObjectByKey<Module.HoSo.HoSo>(Module.HoSo.HoSo.CurrentHoSo.Oid);
        }
    }

}
