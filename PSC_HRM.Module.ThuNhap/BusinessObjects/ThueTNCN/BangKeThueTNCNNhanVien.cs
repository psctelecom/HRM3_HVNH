using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [ImageName("BO_HoaDon")]
    [DefaultProperty("NhanVien")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowEdit", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "05A/BK-TNCN")]
    [RuleCombinationOfPropertiesIsUnique("BangKeThueTNCNNhanVien.Unique", DefaultContexts.Save, "ToKhaiThueTNCN;NhanVien")]
    public class BangKeThueTNCNNhanVien : BaseObject, IBoPhan
    {
        private int _SoThangPhatSinhThuNhap;
        private decimal _SoThueDuocMien;
        private ToKhaiQuyetToanThueTNCN _ToKhaiThueTNCN;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private bool _CaNhanUyQuyenQuyetToanThay;
        private decimal _TongThuNhapChiuThue;
        private decimal _TongTNCTLamCanCuGiamTru;
        private decimal _ThuNhapTinhThue;
        private decimal _GiamTruGiaCanh;
        private decimal _TuThienNhanDaoKhuyenHoc;
        private decimal _BaoHiemBatBuoc;
        private decimal _ThueTNCNDaKhauTru;
        private decimal _SoThuePhaiNop;
        private decimal _SoThueNopThua;
        private decimal _SoThueKhauTruThem;

        [Browsable(false)]
        [ModelDefault("Caption", "Tờ khai quyết toán thuế")]
        [Association("ToKhaiThueTNCN-BangKeThueTNCNNhanVien")]
        public ToKhaiQuyetToanThueTNCN ToKhaiThueTNCN
        {
            get
            {
                return _ToKhaiThueTNCN;
            }
            set
            {
                SetPropertyValue("ToKhaiThueTNCN", ref _ToKhaiThueTNCN, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số tháng phát sinh thu nhập")]
        [RuleRange("", DefaultContexts.Save, 1, 12)]
        public int SoThangPhatSinhThuNhap
        {
            get
            {
                return _SoThangPhatSinhThuNhap;
            }
            set
            {
                SetPropertyValue("SoThangPhatSinhThuNhap", ref _SoThangPhatSinhThuNhap, value);
                if (!IsLoading)
                {
                    CaNhanUyQuyenQuyetToanThay = value == 12;
                }
            }
        }

        [ModelDefault("Caption", "Cá nhân ủy quyền quyết toán thay")]
        public bool CaNhanUyQuyenQuyetToanThay
        {
            get
            {
                return _CaNhanUyQuyenQuyetToanThay;
            }
            set
            {
                SetPropertyValue("CaNhanUyQuyenQuyetToanThay", ref _CaNhanUyQuyenQuyetToanThay, value);
            }
        }

        [ModelDefault("Caption", "Tổng thu nhập chịu thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongThuNhapChiuThue
        {
            get
            {
                return _TongThuNhapChiuThue;
            }
            set
            {
                SetPropertyValue("TongThuNhapChiuThue", ref _TongThuNhapChiuThue, value);
            }
        }

        [ModelDefault("Caption", "Tổng TNCT làm căn cứ giảm trừ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongTNCTLamCanCuGiamTru
        {
            get
            {
                return _TongTNCTLamCanCuGiamTru;
            }
            set
            {
                SetPropertyValue("TongTNCTLamCanCuGiamTru", ref _TongTNCTLamCanCuGiamTru, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ gia cảnh")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal GiamTruGiaCanh
        {
            get
            {
                return _GiamTruGiaCanh;
            }
            set
            {
                SetPropertyValue("GiamTruGiaCanh", ref _GiamTruGiaCanh, value);
            }
        }

        [ModelDefault("Caption", "Từ thiện nhân đạo, khuyến học")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TuThienNhanDaoKhuyenHoc
        {
            get
            {
                return _TuThienNhanDaoKhuyenHoc;
            }
            set
            {
                SetPropertyValue("TuThienNhanDaoKhuyenHoc", ref _TuThienNhanDaoKhuyenHoc, value);
            }
        }

        [ModelDefault("Caption", "Bảo hiểm bắt buộc")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal BaoHiemBatBuoc
        {
            get
            {
                return _BaoHiemBatBuoc;
            }
            set
            {
                SetPropertyValue("BaoHiemBatBuoc", ref _BaoHiemBatBuoc, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập tính thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThuNhapTinhThue
        {
            get
            {
                return _ThuNhapTinhThue;
            }
            set
            {
                SetPropertyValue("ThuNhapTinhThue", ref _ThuNhapTinhThue, value);
            }
        }

        [ModelDefault("Caption", "Thuế TNCN đã khấu trừ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNDaKhauTru
        {
            get
            {
                return _ThueTNCNDaKhauTru;
            }
            set
            {
                SetPropertyValue("ThueTNCNDaKhauTru", ref _ThueTNCNDaKhauTru, value);
            }
        }

        [ModelDefault("Caption", "Tổng số thuế phải nộp")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoThuePhaiNop
        {
            get
            {
                return _SoThuePhaiNop;
            }
            set
            {
                SetPropertyValue("SoThuePhaiNop", ref _SoThuePhaiNop, value);
            }
        }

        [ModelDefault("Caption", "Tổng số thuế được miễn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoThueDuocMien
        {
            get
            {
                return _SoThueDuocMien;
            }
            set
            {
                SetPropertyValue("SoThueDuocMien", ref _SoThueDuocMien, value);
            }
        }

        [ModelDefault("Caption", "Số thuế nộp thừa")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoThueNopThua
        {
            get
            {
                return _SoThueNopThua;
            }
            set
            {
                SetPropertyValue("SoThueNopThua", ref _SoThueNopThua, value);
            }
        }

        [ModelDefault("Caption", "Số thuế khấu trừ thêm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoThueKhauTruThem
        {
            get
            {
                return _SoThueKhauTruThem;
            }
            set
            {
                SetPropertyValue("SoThueKhauTruThem", ref _SoThueKhauTruThem, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết 05A/BK-TNCN")]
        [Association("BangKeThueTNCNNhanVien-ListChiTietBangKeThueTNCNNhanVien")]
        public XPCollection<ChiTietBangKeThueTNCNNhanVien> ListChiTietBangKeThueTNCNNhanVien
        {
            get
            {
                return GetCollection<ChiTietBangKeThueTNCNNhanVien>("ListChiTietBangKeThueTNCNNhanVien");
            }
        }

        public BangKeThueTNCNNhanVien(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? or TinhTrang.TenTinhTrang not like ?", "%nghỉ việc%", "%nghỉ hưu%"));

            NVList.Criteria = go;
        }
    }

}
