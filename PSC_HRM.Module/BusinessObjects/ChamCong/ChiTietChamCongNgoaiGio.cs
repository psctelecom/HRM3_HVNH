using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ChamCong
{
    [ImageName("BO_ChamCong")]
    [ModelDefault("Caption", "Chi tiết chấm công ngoài giờ")]
    [Appearance("ChiTietChamCongNgoaiGio", TargetItems = "*", Enabled = false, 
        Criteria = "BangChamCongNgoaiGio is not null and BangChamCongNgoaiGio.KyTinhLuong is not null and BangChamCongNgoaiGio.KyTinhLuong.KhoaSo")]
        
    public class ChiTietChamCongNgoaiGio : TruongBaseObject, IBoPhan
    {
        private decimal _SoCongNgoaiGio2Sau23Gio;
        private decimal _SoCongNgoaiGio1Sau23Gio;
        private decimal _SoCongNgoaiGioSau23Gio;
        private BangChamCongNgoaiGio _BangChamCongNgoaiGio;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private decimal _SoCongNgoaiGio;
        private decimal _SoCongNgoaiGio1;
        private decimal _SoCongNgoaiGio2;
        private decimal _SoNgayLamDem;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng chấm công ngoài giờ")]
        [Association("BangChamCongNgoaiGio-ListChiTietChamCongNgoaiGio")]
        public BangChamCongNgoaiGio BangChamCongNgoaiGio
        {
            get
            {
                return _BangChamCongNgoaiGio;
            }
            set
            {
                SetPropertyValue("BangChamCongNgoaiGio", ref _BangChamCongNgoaiGio, value);
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
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Số giờ ngày thường")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoCongNgoaiGio
        {
            get
            {
                return _SoCongNgoaiGio;
            }
            set
            {
                SetPropertyValue("SoCongNgoaiGio", ref _SoCongNgoaiGio, value);
            }
        }

        [ModelDefault("Caption", "Số giờ ngày thường sau 23h")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoCongNgoaiGioSau23Gio
        {
            get
            {
                return _SoCongNgoaiGioSau23Gio;
            }
            set
            {
                SetPropertyValue("SoCongNgoaiGioSau23Gio", ref _SoCongNgoaiGioSau23Gio, value);
            }
        }

        [ModelDefault("Caption", "Số giờ T7/CN")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoCongNgoaiGio1
        {
            get
            {
                return _SoCongNgoaiGio1;
            }
            set
            {
                SetPropertyValue("SoCongNgoaiGio1", ref _SoCongNgoaiGio1, value);
            }
        }

        [ModelDefault("Caption", "Số giờ T7/CN sau 23h")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoCongNgoaiGio1Sau23Gio
        {
            get
            {
                return _SoCongNgoaiGio1Sau23Gio;
            }
            set
            {
                SetPropertyValue("SoCongNgoaiGio1Sau23Gio", ref _SoCongNgoaiGio1Sau23Gio, value);
            }
        }

        [ModelDefault("Caption", "Số giờ ngày lễ")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoCongNgoaiGio2
        {
            get
            {
                return _SoCongNgoaiGio2;
            }
            set
            {
                SetPropertyValue("SoCongNgoaiGio2", ref _SoCongNgoaiGio2, value);
            }
        }

        [ModelDefault("Caption", "Số giờ ngày lễ sau 23h")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoCongNgoaiGio2Sau23Gio
        {
            get
            {
                return _SoCongNgoaiGio2Sau23Gio;
            }
            set
            {
                SetPropertyValue("SoCongNgoaiGio2Sau23Gio", ref _SoCongNgoaiGio2Sau23Gio, value);
            }
        }

        [ModelDefault("Caption", "Số ngày làm đêm")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoNgayLamDem
        {
            get
            {
                return _SoNgayLamDem;
            }
            set
            {
                SetPropertyValue("SoNgayLamDem", ref _SoNgayLamDem, value);
            }
        }

        public ChiTietChamCongNgoaiGio(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNhanVienList();
        }


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
