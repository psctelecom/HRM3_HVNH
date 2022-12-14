using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nghỉ thai sản")]
    public class QuyetDinhNghiThaiSan : QuyetDinhCaNhan
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private DateTime _NgayXinNghi;
        private bool _QuyetDinhMoi;
        private string _SoSoBHXH;
        private TinhTrang _TinhTrangCu;

        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
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
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
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
        [ModelDefault("Caption", "Ngày xin nghỉ")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NgayXinNghi
        {
            get
            {
                return _NgayXinNghi;
            }
            set
            {
                SetPropertyValue("NgayXinNghi", ref _NgayXinNghi, value);
            }
        }

        [ModelDefault("Caption", "Số sổ BHXH")]
        public string SoSoBHXH
        {
            get
            {
                return _SoSoBHXH;
            }
            set
            {
                SetPropertyValue("SoSoBHXH", ref _SoSoBHXH, value);
            }
        }
        [Browsable (false)]
        [ModelDefault("Caption", "Tình trang cũ")]
        public TinhTrang TinhTrangCu
        {
            get
            {
                return _TinhTrangCu;
            }
            set
            {
                SetPropertyValue("TinhTrangCu", ref _TinhTrangCu, value);
            }
        }

        [ModelDefault("Caption", "Quyết định mới")]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
            }
        }

        public QuyetDinhNghiThaiSan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định nghỉ BHXH"));
            
        }

        protected override void AfterNhanVienChanged()
        {
            base.AfterNhanVienChanged();
            TinhTrangCu = ThongTinNhanVien.TinhTrang;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                CriteriaOperator filter;
                if (QuyetDinhMoi)
                {
                    //thiết lập tình trạng
                    if (TuNgay <= HamDungChung.GetServerTime())
                    {
                        filter = CriteriaOperator.Parse("TenTinhTrang like ? ", "Nghỉ thai sản");
                        TinhTrang tinhTrang = Session.FindObject<TinhTrang>(filter);
                        if (tinhTrang == null)
                        {
                            tinhTrang = new TinhTrang(Session);
                            tinhTrang.MaQuanLy = "NTS";
                            tinhTrang.TenTinhTrang = "Nghỉ thai sản";
                        }
                        ThongTinNhanVien.TinhTrang = tinhTrang;
                    }
                }
                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                BoPhanText = BoPhan.TenBoPhan;
                NhanVienText = ThongTinNhanVien.HoTen;
            }
        }
        protected override void OnDeleting()
        {
            if (QuyetDinhMoi)
            {
                ThongTinNhanVien.TinhTrang = TinhTrangCu;
            }
            base.OnDeleting();
        }
    }

}
