using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.ThuNhap.BoSungLuong
{
    [ImageName("BO_ChiTietLuong")]
    [ModelDefault("Caption", "Chi bổ sung phụ cấp ưu đãi")]
    [Appearance("ChiBoSungPhuCapUuDai.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "BoSungLuongNhanVien is not null and BoSungLuongNhanVien.KyTinhLuong is not null and BoSungLuongNhanVien.KyTinhLuong.KhoaSo")]
    public class ChiBoSungPhuCapUuDai : ThuNhapBaseObject
    {
        private BoSungLuongNhanVien _BoSungLuongNhanVien;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private decimal _HeSoLuongCu;
        private decimal _HeSoLuongMoi;
        private DateTime _TuThang;
        private DateTime _DenThang;
        private int _PhuCapUuDai;
        private decimal _ChenhLechLuong1Thang;
        private decimal _SoThangTruyLinh;
        private decimal _TongLuongChenhLech;
        private decimal _SoNgayNghi;
        private decimal _TienNgayNghi;
        private decimal _ThucNhan;
        private decimal _SoTienChiuThue;
        private string _GhiChu;

        public ChiBoSungPhuCapUuDai(Session session) : base(session) { }

        [Browsable(false)]
        [ModelDefault("Caption", "Chi bổ sung lương kỳ 1")]
        [Association("BoSungLuongNhanVien-ListChiBoSungPhuCapUuDai")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoSungLuongNhanVien BoSungLuongNhanVien
        {
            get
            {
                return _BoSungLuongNhanVien;
            }
            set
            {
                SetPropertyValue("BoSungLuongNhanVien", ref _BoSungLuongNhanVien, value);
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

        [ModelDefault("Caption", "Hệ số lương cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HeSoLuongCu
        {
            get
            {
                return _HeSoLuongCu;
            }
            set
            {
                SetPropertyValue("HeSoLuongCu", ref _HeSoLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Hệ số lương mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HeSoLuongMoi
        {
            get
            {
                return _HeSoLuongMoi;
            }
            set
            {
                SetPropertyValue("HeSoLuongMoi", ref _HeSoLuongMoi, value);
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

        [ModelDefault("Caption", "Phụ cấp ưu đãi")]
        public int PhuCapUuDai
        {
            get
            {
                return _PhuCapUuDai;
            }
            set
            {
                SetPropertyValue("PhuCapUuDai", ref _PhuCapUuDai, value);
            }
        }


        [ModelDefault("Caption", "Chênh lệch PCUD 1 tháng")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ChenhLechLuong1Thang
        {
            get
            {
                return _ChenhLechLuong1Thang;
            }
            set
            {
                SetPropertyValue("ChenhLechLuong1Thang", ref _ChenhLechLuong1Thang, value);
            }
        }

        [ModelDefault("Caption", "Số tháng truy lĩnh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoThangTruyLinh
        {
            get
            {
                return _SoThangTruyLinh;
            }
            set
            {
                SetPropertyValue("SoThangTruyLinh", ref _SoThangTruyLinh, value);
            }
        }

        [ModelDefault("Caption", "Tổng lương chênh lệch")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongLuongChenhLech
        {
            get
            {
                return _TongLuongChenhLech;
            }
            set
            {
                SetPropertyValue("TongLuongChenhLech", ref _TongLuongChenhLech, value);
            }
        }

        [ModelDefault("Caption", "Số ngày nghỉ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal SoNgayNghi
        {
            get
            {
                return _SoNgayNghi;
            }
            set
            {
                SetPropertyValue("SoNgayNghi", ref _SoNgayNghi, value);
            }
        }

        [ModelDefault("Caption", "Tiền ngày nghỉ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienNgayNghi
        {
            get
            {
                return _TienNgayNghi;
            }
            set
            {
                SetPropertyValue("TienNgayNghi", ref _TienNgayNghi, value);
            }
        }
       

        [ModelDefault("Caption", "Thực nhận")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThucNhan
        {
            get
            {
                return _ThucNhan;
            }
            set
            {
                SetPropertyValue("ThucNhan", ref _ThucNhan, value);
            }
        }

        [ModelDefault("Caption", "Số tiền chịu thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? and TinhTrang.TenTinhTrang not like ? and TinhTrang.TenTinhTrang not like ?", "%nghỉ việc%", "%nghỉ hưu%","%chuyển công tác%"));

            NVList.Criteria = go;
        }
    }

}
