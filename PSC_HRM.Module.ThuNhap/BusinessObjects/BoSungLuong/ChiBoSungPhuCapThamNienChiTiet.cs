using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.ThuNhap.BoSungLuong;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.ThuNhap.BoSungLuong
{
    [DefaultClassOptions]
    [ImageName("BO_ChiTietLuong")]
    [ModelDefault("Caption", "Chi tiết Chi bổ sung lương phụ cấp thâm niên")]
    public class ChiBoSungPhuCapThamNienChiTiet : BaseObject
    {
        private ChiBoSungPhuCapThamNien _ChiBoSungPhuCapThamNien;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DateTime _TuThang;
        private DateTime _DenThang;
        private string _ThangNamTu;
        private string _ThangNamDen;//Lưu ý: Cột này chính được tính đến tháng năm hiện tại
        private decimal _HeSoLuong;
        private decimal _HeSoChucVu;
        private int _TiLeThamNien;
        private decimal _LuongToiThieu;
        private decimal _Tien1Thang;
        private decimal _TongBaoHiem;
        private decimal _CacKhoanKhauTru;
        private decimal _TongBaoHiemCTy;
        private decimal _CacKhoanKhauTruCTy;
        private int _SoThangThucHienTrongNam;
        private decimal _ThucLanh;

        [Browsable(false)]
        [ModelDefault("Caption", "Chi tiết Chi bổ sung lương phụ cấp thâm niên")]
        [Association("ChiBoSungPhuCapThamNien-ListChiBoSungPhuCapThamNienChiTiet")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ChiBoSungPhuCapThamNien ChiBoSungPhuCapThamNien
        {
            get
            {
                return _ChiBoSungPhuCapThamNien;
            }
            set
            {
                SetPropertyValue("ChiBoSungPhuCapThamNien", ref _ChiBoSungPhuCapThamNien, value);
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
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                }
            }
        }
        [ModelDefault("Caption", "Từ tháng")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime TuThang
        {
            get
            {
                return _TuThang;
            }
            set
            {
                SetPropertyValue("TuThang", ref _TuThang, value);
            }
        }
        [ModelDefault("Caption", "Tháng năm từ")]
        public string ThangNamTu
        {
            get
            {
                return _ThangNamTu;
            }
            set
            {
                SetPropertyValue("ThangNamTu", ref _ThangNamTu, value);
            }
        }
        [ModelDefault("Caption", "Đến tháng")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime DenThang
        {
            get
            {
                return _DenThang;
            }
            set
            {
                SetPropertyValue("DenThang", ref _DenThang, value);
            }
        }
        [ModelDefault("Caption", "Tháng năm đến")]
        public string ThangNamDen
        {
            get
            {
                return _ThangNamDen;
            }
            set
            {
                SetPropertyValue("ThangNamDen", ref _ThangNamDen, value);
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }
        [ModelDefault("Caption", "Hệ số chức vụ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HeSoChucVu
        {
            get
            {
                return _HeSoChucVu;
            }
            set
            {
                SetPropertyValue("HeSoChucVu", ref _HeSoChucVu, value);
            }
        }
        [ModelDefault("Caption", "Tỉ lệ thâm niên")]
        public int TiLeThamNien
        {
            get
            {
                return _TiLeThamNien;
            }
            set
            {
                SetPropertyValue("TiLeThamNien", ref _TiLeThamNien, value);
            }
        }
        [ModelDefault("Caption", "Lương tối thiểu")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongToiThieu
        {
            get
            {
                return _LuongToiThieu;
            }
            set
            {
                SetPropertyValue("LuongToiThieu", ref _LuongToiThieu, value);
            }
        }
        [ModelDefault("Caption", "Tiền một tháng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal Tien1Thang
        {
            get
            {
                return _Tien1Thang;
            }
            set
            {
                SetPropertyValue("Tien1Thang", ref _Tien1Thang, value);
            }
        }
        [ModelDefault("Caption", "Tổng BH")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal TongBaoHiem
        {
            get
            {
                return _TongBaoHiem;
            }
            set
            {
                SetPropertyValue("TongBaoHiem", ref _TongBaoHiem, value);
            }
        }
        [ModelDefault("Caption", "Các khoản khấu trừ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal CacKhoanKhauTru
        {
            get
            {
                return _CacKhoanKhauTru;
            }
            set
            {
                SetPropertyValue("CacKhoanKhauTru", ref _CacKhoanKhauTru, value);
            }
        }
        [ModelDefault("Caption", "Tổng BH Cty")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal TongBaoHiemCTy
        {
            get
            {
                return _TongBaoHiemCTy;
            }
            set
            {
                SetPropertyValue("TongBaoHiemCTy", ref _TongBaoHiemCTy, value);
            }
        }
        [ModelDefault("Caption", "Các khoản khấu trừ CTy")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal CacKhoanKhauTruCTy
        {
            get
            {
                return _CacKhoanKhauTruCTy;
            }
            set
            {
                SetPropertyValue("CacKhoanKhauTruCTy", ref _CacKhoanKhauTruCTy, value);
            }
        }
        [ModelDefault("Caption", "Số tháng trong năm")]
        public int SoThangThucHienTrongNam
        {
            get
            {
                return _SoThangThucHienTrongNam;
            }
            set
            {
                SetPropertyValue("SoThangThucHienTrongNam", ref _SoThangThucHienTrongNam, value);
            }
        }
        [ModelDefault("Caption", "Thực lãnh")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThucLanh
        {
            get
            {
                return _ThucLanh;
            }
            set
            {
                SetPropertyValue("ThucLanh", ref _ThucLanh, value);
            }
        }
        public ChiBoSungPhuCapThamNienChiTiet(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? and TinhTrang.TenTinhTrang not like ? and TinhTrang.TenTinhTrang not like ?", "%nghỉ việc%", "%nghỉ hưu%", "%chuyển công tác%"));

            NVList.Criteria = go;
        }
    }
}
