using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.HoSo
{
    [ModelDefault("Caption", "Đánh giá giảng viên thỉnh giảng")]
    public class DanhGiaGiangVienThinhGiang : BaseObject
    {
        private HocKy _HocKy;
        private NamHoc _NamHoc;
        private ChuyenDeGiangDay _ChuyenDeGiangDay;
        private XepLoaiDaoDuc _XepLoaiDaoDuc;
        private XepLoaiChuyenMon _XepLoaiChuyenMon;
        private string _NhanXet;
        private GiangVienThinhGiang _GiangVienThinhGiang;

        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("GiangVienThinhGiang-ListDanhGiaGiangVienThinhGiang")]
        public GiangVienThinhGiang GiangVienThinhGiang
        {
            get
            {
                return _GiangVienThinhGiang;
            }
            set
            {
                SetPropertyValue("GiangVienThinhGiang", ref _GiangVienThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "Học kỳ")]
        public HocKy HocKy
        {
            get
            {
                return _HocKy;
            }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Chuyên đề giảng dạy")]
        public ChuyenDeGiangDay ChuyenDeGiangDay
        {
            get
            {
                return _ChuyenDeGiangDay;
            }
            set
            {
                SetPropertyValue("ChuyenDeGiangDay", ref _ChuyenDeGiangDay, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại đạo đức")]
        public XepLoaiDaoDuc XepLoaiDaoDuc
        {
            get
            {
                return _XepLoaiDaoDuc;
            }
            set
            {
                SetPropertyValue("XepLoaiDaoDuc", ref _XepLoaiDaoDuc, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại chuyên môn")]
        public XepLoaiChuyenMon XepLoaiChuyenMon
        {
            get
            {
                return _XepLoaiChuyenMon;
            }
            set
            {
                SetPropertyValue("XepLoaiChuyenMon", ref _XepLoaiChuyenMon, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Nhận xét")]
        public string NhanXet
        {
            get
            {
                return _NhanXet;
            }
            set
            {
                SetPropertyValue("NhanXet", ref _NhanXet, value);
            }
        }

        public DanhGiaGiangVienThinhGiang(Session session) : base(session) { }
    }

}
