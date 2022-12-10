using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [DefaultProperty("PhanLoai")]
    [ModelDefault("Caption", "Chi tiết điều chỉnh 05A/BK-TNCN")]
    [Appearance("ChiTietDieuChinhThueTNCN.SoLuong", TargetItems = "*", Visibility=ViewItemVisibility.Hide,
        Criteria = "PhanLoai=3")]
    [Appearance("ChiTietDieuChinhThueTNCN.SoTien", TargetItems = "*", Visibility = ViewItemVisibility.Hide,
        Criteria = "PhanLoai!=3")]
    public class ChiTietDieuChinhThueTNCNNhanVien : BaseObject
    {
        // Fields...
        private int _SoLuong;
        private string _GhiChu;
        private decimal _SoTien;
        private DieuChinhThueTNCNNhanVienEnum _PhanLoai;
        private DieuChinhThueTNCNNhanVien _DieuChinhThueTNCNNhanVien;

        [Browsable(false)]
        [ModelDefault("Caption", "Điều chỉnh thuế TNCN")]
        [Association("DieuChinhThueTNCNNhanVien-ListChiTietDieuChinhThueTNCNNhanVien")]
        public DieuChinhThueTNCNNhanVien DieuChinhThueTNCNNhanVien
        {
            get
            {
                return _DieuChinhThueTNCNNhanVien;
            }
            set
            {
                SetPropertyValue("DieuChinhThueTNCNNhanVien", ref _DieuChinhThueTNCNNhanVien, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại")]
        public DieuChinhThueTNCNNhanVienEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        [ModelDefault("Caption", "Số lượng")]
        public int SoLuong
        {
            get
            {
                return _SoLuong;
            }
            set
            {
                SetPropertyValue("SoLuong", ref _SoLuong, value);
            }
        }

        [Size(500)]
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

        public ChiTietDieuChinhThueTNCNNhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            PhanLoai = DieuChinhThueTNCNNhanVienEnum.BHXH;
        }
    }

}
