using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.ThuNhapTangThem
{
    [ModelDefault("Caption", "Chi tiết quyết toán thu nhập tăng thêm")]
    public class ChiTietQuyetToanThuNhapTangThem : ThuNhapBaseObject
    {
        // Fields...
        private decimal _TongThucNhan;
        private decimal _TongTamUng;
        private decimal _SoTienConLai;
        private decimal _SoTienChiuThue;
        private decimal _ThucNhanThang12;
        private decimal _ThucNhanThang11;
        private decimal _ThucNhanThang10;
        private decimal _ThucNhanThang9;
        private decimal _ThucNhanThang8;
        private decimal _ThucNhanThang7;
        private decimal _ThucNhanThang6;
        private decimal _ThucNhanThang5;
        private decimal _ThucNhanThang4;
        private decimal _ThucNhanThang3;
        private decimal _ThucNhanThang2;
        private decimal _ThucNhanThang1;
        private decimal _TamUngThang12;
        private decimal _TamUngThang11;
        private decimal _TamUngThang10;
        private decimal _TamUngThang9;
        private decimal _TamUngThang8;
        private decimal _TamUngThang7;
        private decimal _TamUngThang6;
        private decimal _TamUngThang5;
        private decimal _TamUngThang4;
        private decimal _TamUngThang3;
        private decimal _TamUngThang2;
        private decimal _TamUngThang1;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private BangQuyetToanThuNhapTangThem _BangQuyetToanThuNhapTangThem;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng quyết toán thu nhập tăng thêm")]
        [Association("BangQuyetToanThuNhapTangThem-ListChiTietQuyetToanThuNhapTangThem")]
        public BangQuyetToanThuNhapTangThem BangQuyetToanThuNhapTangThem
        {
            get
            {
                return _BangQuyetToanThuNhapTangThem;
            }
            set
            {
                SetPropertyValue("BangQuyetToanThuNhapTangThem", ref _BangQuyetToanThuNhapTangThem, value);
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
                if (!IsLoading && value != null)
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
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tạm ứng Tháng 1")]
        public decimal TamUngThang1
        {
            get
            {
                return _TamUngThang1;
            }
            set
            {
                SetPropertyValue("TamUngThang1", ref _TamUngThang1, value);
                if (!IsLoading)
                    TinhTongTamUng();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tạm ứng Tháng 2")]
        public decimal TamUngThang2
        {
            get
            {
                return _TamUngThang2;
            }
            set
            {
                SetPropertyValue("TamUngThang2", ref _TamUngThang2, value);
                if (!IsLoading)
                    TinhTongTamUng();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tạm ứng Tháng 3")]
        public decimal TamUngThang3
        {
            get
            {
                return _TamUngThang3;
            }
            set
            {
                SetPropertyValue("TamUngThang3", ref _TamUngThang3, value);
                if (!IsLoading)
                    TinhTongTamUng();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tạm ứng Tháng 4")]
        public decimal TamUngThang4
        {
            get
            {
                return _TamUngThang4;
            }
            set
            {
                SetPropertyValue("TamUngThang4", ref _TamUngThang4, value);
                if (!IsLoading)
                    TinhTongTamUng();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tạm ứng Tháng 5")]
        public decimal TamUngThang5
        {
            get
            {
                return _TamUngThang5;
            }
            set
            {
                SetPropertyValue("TamUngThang5", ref _TamUngThang5, value);
                if (!IsLoading)
                    TinhTongTamUng();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tạm ứng Tháng 6")]
        public decimal TamUngThang6
        {
            get
            {
                return _TamUngThang6;
            }
            set
            {
                SetPropertyValue("TamUngThang6", ref _TamUngThang6, value);
                if (!IsLoading)
                    TinhTongTamUng();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tạm ứng Tháng 7")]
        public decimal TamUngThang7
        {
            get
            {
                return _TamUngThang7;
            }
            set
            {
                SetPropertyValue("TamUngThang7", ref _TamUngThang7, value);
                if (!IsLoading)
                    TinhTongTamUng();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tạm ứng Tháng 8")]
        public decimal TamUngThang8
        {
            get
            {
                return _TamUngThang8;
            }
            set
            {
                SetPropertyValue("TamUngThang8", ref _TamUngThang8, value);
                if (!IsLoading)
                    TinhTongTamUng();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tạm ứng Tháng 9")]
        public decimal TamUngThang9
        {
            get
            {
                return _TamUngThang9;
            }
            set
            {
                SetPropertyValue("TamUngThang9", ref _TamUngThang9, value);
                if (!IsLoading)
                    TinhTongTamUng();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tạm ứng Tháng 10")]
        public decimal TamUngThang10
        {
            get
            {
                return _TamUngThang10;
            }
            set
            {
                SetPropertyValue("TamUngThang10", ref _TamUngThang10, value);
                if (!IsLoading)
                    TinhTongTamUng();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tạm ứng Tháng 11")]
        public decimal TamUngThang11
        {
            get
            {
                return _TamUngThang11;
            }
            set
            {
                SetPropertyValue("TamUngThang11", ref _TamUngThang11, value);
                if (!IsLoading)
                    TinhTongTamUng();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tạm ứng Tháng 12")]
        public decimal TamUngThang12
        {
            get
            {
                return _TamUngThang12;
            }
            set
            {
                SetPropertyValue("TamUngThang12", ref _TamUngThang12, value);
                if (!IsLoading)
                    TinhTongTamUng();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng tạm ứng")]
        public decimal TongTamUng
        {
            get
            {
                return _TongTamUng;
            }
            set
            {
                SetPropertyValue("TongTamUng", ref _TongTamUng, value);
                if (!IsLoading)
                    TinhChenhLech();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thực nhận Tháng 1")]
        public decimal ThucNhanThang1
        {
            get
            {
                return _ThucNhanThang1;
            }
            set
            {
                SetPropertyValue("ThucNhanThang1", ref _ThucNhanThang1, value);
                if (!IsLoading)
                    TinhTongThucNhan();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thực nhận Tháng 2")]
        public decimal ThucNhanThang2
        {
            get
            {
                return _ThucNhanThang2;
            }
            set
            {
                SetPropertyValue("ThucNhanThang2", ref _ThucNhanThang2, value);
                if (!IsLoading)
                    TinhTongThucNhan();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thực nhận Tháng 3")]
        public decimal ThucNhanThang3
        {
            get
            {
                return _ThucNhanThang3;
            }
            set
            {
                SetPropertyValue("ThucNhanThang3", ref _ThucNhanThang3, value);
                if (!IsLoading)
                    TinhTongThucNhan();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thực nhận Tháng 4")]
        public decimal ThucNhanThang4
        {
            get
            {
                return _ThucNhanThang4;
            }
            set
            {
                SetPropertyValue("ThucNhanThang4", ref _ThucNhanThang4, value);
                if (!IsLoading)
                    TinhTongThucNhan();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thực nhận Tháng 5")]
        public decimal ThucNhanThang5
        {
            get
            {
                return _ThucNhanThang5;
            }
            set
            {
                SetPropertyValue("ThucNhanThang5", ref _ThucNhanThang5, value);
                if (!IsLoading)
                    TinhTongThucNhan();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thực nhận Tháng 6")]
        public decimal ThucNhanThang6
        {
            get
            {
                return _ThucNhanThang6;
            }
            set
            {
                SetPropertyValue("ThucNhanThang6", ref _ThucNhanThang6, value);
                if (!IsLoading)
                    TinhTongThucNhan();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thực nhận Tháng 7")]
        public decimal ThucNhanThang7
        {
            get
            {
                return _ThucNhanThang7;
            }
            set
            {
                SetPropertyValue("ThucNhanThang7", ref _ThucNhanThang7, value);
                if (!IsLoading)
                    TinhTongThucNhan();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thực nhận Tháng 8")]
        public decimal ThucNhanThang8
        {
            get
            {
                return _ThucNhanThang8;
            }
            set
            {
                SetPropertyValue("ThucNhanThang8", ref _ThucNhanThang8, value);
                if (!IsLoading)
                    TinhTongThucNhan();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thực nhận Tháng 9")]
        public decimal ThucNhanThang9
        {
            get
            {
                return _ThucNhanThang9;
            }
            set
            {
                SetPropertyValue("ThucNhanThang9", ref _ThucNhanThang9, value);
                if (!IsLoading)
                    TinhTongThucNhan();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thực nhận Tháng 10")]
        public decimal ThucNhanThang10
        {
            get
            {
                return _ThucNhanThang10;
            }
            set
            {
                SetPropertyValue("ThucNhanThang10", ref _ThucNhanThang10, value);
                if (!IsLoading)
                    TinhTongThucNhan();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thực nhận Tháng 11")]
        public decimal ThucNhanThang11
        {
            get
            {
                return _ThucNhanThang11;
            }
            set
            {
                SetPropertyValue("ThucNhanThang11", ref _ThucNhanThang11, value);
                if (!IsLoading)
                    TinhTongThucNhan();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thực nhận Tháng 12")]
        public decimal ThucNhanThang12
        {
            get
            {
                return _ThucNhanThang12;
            }
            set
            {
                SetPropertyValue("ThucNhanThang12", ref _ThucNhanThang12, value);
                if (!IsLoading)
                    TinhTongThucNhan();
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng thực nhận")]
        public decimal TongThucNhan
        {
            get
            {
                return _TongThucNhan;
            }
            set
            {
                SetPropertyValue("TongThucNhan", ref _TongThucNhan, value);
                if (!IsLoading)
                    TinhChenhLech();
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Số tiền còn lại")]
        public decimal SoTienConLai
        {
            get
            {
                return _SoTienConLai;
            }
            set
            {
                SetPropertyValue("SoTienConLai", ref _SoTienConLai, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Số tiền chịu thuế")]
        public decimal SoTienChiuThue
        {
            get
            {
                return _SoTienChiuThue;
            }
            set
            {
                SetPropertyValue("SoTienChiuThue", ref _SoTienChiuThue, value);
            }
        }

        public ChiTietQuyetToanThuNhapTangThem(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<HoSo.ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? or TinhTrang.TenTinhTrang not like ?", "%nghỉ việc%", "%nghỉ hưu%"));

            NVList.Criteria = go;
        }

        private void TinhTongTamUng()
        {
            TongTamUng = TamUngThang1 + TamUngThang2 + TamUngThang3 + TamUngThang4 + TamUngThang5 + TamUngThang6 + TamUngThang7 + TamUngThang8 + TamUngThang9 + TamUngThang10 + TamUngThang11 + TamUngThang12;
        }

        private void TinhTongThucNhan()
        {
            TongThucNhan = ThucNhanThang1 + ThucNhanThang2 + ThucNhanThang3 + ThucNhanThang4 + ThucNhanThang5 + ThucNhanThang6 + ThucNhanThang7 + ThucNhanThang8 + ThucNhanThang9 + ThucNhanThang10 + ThucNhanThang11 + ThucNhanThang12;
        }

        private void TinhChenhLech()
        {
            SoTienConLai = TongThucNhan - TongTamUng;
            SoTienChiuThue = SoTienConLai;
        }
    }

}
