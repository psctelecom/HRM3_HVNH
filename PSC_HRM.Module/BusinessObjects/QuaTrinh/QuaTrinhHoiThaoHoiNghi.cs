using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuaTrinh
{
    [ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình tham gia hội thảo, hội nghị")]
    public class QuaTrinhHoiThaoHoiNghi : BaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _TenHoiThao;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private string _DiaDiem;
        private QuocGia _QuocGia;
        private bool _BaiThamLuan;
        private NguonKinhPhi _NguonKinhPhi;

        public QuaTrinhHoiThaoHoiNghi(Session session) : base(session) { }
 
        [Browsable(false)]
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

        [ModelDefault("Caption", "Tên hội thảo")]
        public string TenHoiThao
        {
            get
            {
                return _TenHoiThao;
            }
            set
            {
                SetPropertyValue("TenHoiThao", ref _TenHoiThao, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Địa điểm")]
        public string DiaDiem
        {
            get
            {
                return _DiaDiem;
            }
            set
            {
                SetPropertyValue("DiaDiem", ref _DiaDiem, value);
            }
        }

        [ModelDefault("Caption", "Quốc gia")]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
            }
        }

        [ModelDefault("Caption", "Có bài tham luận")]
        public bool BaiThamLuan
        {
            get
            {
                return _BaiThamLuan;
            }
            set
            {
                SetPropertyValue("BaiThamLuan", ref _BaiThamLuan, value);
            }
        }

        [ModelDefault("Caption", "Nguồn kinh phí")]
        public NguonKinhPhi NguonKinhPhi
        {
            get
            {
                return _NguonKinhPhi;
            }
            set
            {
                SetPropertyValue("NguonKinhPhi", ref _NguonKinhPhi, value);
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            QuocGia = HamDungChung.GetCurrentQuocGia(Session);
            if (ThongTinNhanVien.NhanVien != null)
                ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(ThongTinNhanVien.NhanVien.Oid);

        }
    }

}
