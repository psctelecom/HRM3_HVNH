using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.BanLamViec
{
    [NonPersistent]
    [DefaultClassOptions]
    [ImageName("BO_Money2")]
    [ModelDefault("Caption", "Nội dung chi tiết")]
    public class NhacViec_ChiTietHetHanHopDong : BaseObject
    {
        private string _LoaiHopDong;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private HopDong.HopDong _HopDongHienTai;
        private DateTime _NgayHetHan;
        private string _GhiChu;

        [ImmediatePostData]
        [ModelDefault("Caption", "Số hợp đồng")]
        public HopDong.HopDong HopDongHienTai
        {
            get
            {
                return _HopDongHienTai;
            }
            set
            {
                SetPropertyValue("HopDongHienTai", ref _HopDongHienTai, value);
            }
        }

        [ModelDefault("Caption", "Loại hợp đồng")]
        public string LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
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
            }
        }


        [ModelDefault("Caption", "Ngày hết hạn")]
        public DateTime NgayHetHan
        {
            get
            {
                return _NgayHetHan;
            }
            set
            {
                SetPropertyValue("NgayHetHan", ref _NgayHetHan, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public NhacViec_ChiTietHetHanHopDong(Session session) : base(session) { }

    }

}
