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
    [ModelDefault("Caption", "Chi tiết Chi bổ sung lương phụ cấp thâm niên")]
    [Appearance("ChiBoSungPhuCapThamNien.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "BoSungLuongNhanVien is not null and BoSungLuongNhanVien.KyTinhLuong is not null and BoSungLuongNhanVien.KyTinhLuong.KhoaSo")]
    public class ChiBoSungPhuCapThamNien : ThuNhapBaseObject
    {
        private BoSungLuongNhanVien _BoSungLuongNhanVien;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DateTime _TuThang;
        private DateTime _DenThang;
        private DateTime _ThoiGianBatDauTinh;
        private string _ThangNamTu;
        private string _ThangNamDen;//Lưu ý: Cột này chính được tính đến tháng năm hiện tại

        public ChiBoSungPhuCapThamNien(Session session) : base(session) { }

        [Browsable(false)]
        [ModelDefault("Caption", "Chi bổ sung lương phụ cấp thâm niên")]
        [Association("BoSungLuongNhanVien-ListChiBoSungPhuCapThamNien")]
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
        [ModelDefault("Caption", "Thời gian bắt đầu tính")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime ThoiGianBatDauTinh
        {
            get
            {
                return _ThoiGianBatDauTinh;
            }
            set
            {
                SetPropertyValue("ThoiGianBatDauTinh", ref _ThoiGianBatDauTinh, value);
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

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết Chi bổ sung phụ cấp thâm niên")]
        [Association("ChiBoSungPhuCapThamNien-ListChiBoSungPhuCapThamNienChiTiet")]
        public XPCollection<ChiBoSungPhuCapThamNienChiTiet> ListChiBoSungPhuCapThamNienChiTiet
        {
            get
            {
                return GetCollection<ChiBoSungPhuCapThamNienChiTiet>("ListChiBoSungPhuCapThamNienChiTiet");
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
