using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DanhMuc;


namespace PSC_HRM.Module.ChamCong
{
    [ImageName("BO_QuanLyChamCong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết bảng chấm công web")]
    [Appearance("CC_ChiTietChamCongNhanVien", TargetItems = "*", Enabled = false,
        Criteria = "CC_QuanLyChamCongNhanVien is not null and CC_QuanLyChamCongNhanVien.KyTinhLuong is not null and CC_QuanLyChamCongNhanVien.KyTinhLuong.KhoaSo")]

    public class CC_ChiTietChamCongNhanVien : TruongBaseObject
    {
         // Fields...
        private decimal _SoNgayCong;      
        private decimal _NghiCoPhep;
        private decimal _NghiRo;
        private decimal _NghiThaiSan;      
        private string _DanhGia;     
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private CC_QuanLyChamCongNhanVien _QuanLyChamCongNhanVien;
        private string _DienGiai;
        private string _BoPhanTheoBangCong;
     
        private decimal _NghiOm;
        private decimal _NghiDiHocKhongLuong;
        private decimal _NghiDiHocCoLuong;
        private bool _TrangThai;
        private bool _Khoa;
               
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý chấm công web")]
        [Association("CC_QuanLyChamCongNhanVien-ChiTietChamCongNhanVienList")]
        public CC_QuanLyChamCongNhanVien CC_QuanLyChamCongNhanVien
        {
            get
            {
                return _QuanLyChamCongNhanVien;
            }
            set
            {
                SetPropertyValue("CC_QuanLyChamCongNhanVien", ref _QuanLyChamCongNhanVien, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Số ngày công")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayCong
        {
            get
            {
                return _SoNgayCong;
            }
            set
            {
                SetPropertyValue("SoNgayCong", ref _SoNgayCong, value);
            }
        }       

        [ModelDefault("Caption", "Nghỉ có phép")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NghiCoPhep
        {
            get
            {
                return _NghiCoPhep;
            }
            set
            {
                SetPropertyValue("NghiCoPhep", ref _NghiCoPhep, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ trừ lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NghiRo
        {
            get
            {
                return _NghiRo;
            }
            set
            {
                SetPropertyValue("NghiRo", ref _NghiRo, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ thai sản")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NghiThaiSan
        {
            get
            {
                return _NghiThaiSan;
            }
            set
            {
                SetPropertyValue("NghiThaiSan", ref _NghiThaiSan, value);
            }
        }

        [ModelDefault("Caption", "Bộ phận theo bảng công")]
        public string BoPhanTheoBangCong
        {
            get
            {
                return _BoPhanTheoBangCong;
            }
            set
            {
                SetPropertyValue("BoPhanTheoBangCong", ref _BoPhanTheoBangCong, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ đi học không lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NghiDiHocKhongLuong
        {
            get
            {
                return _NghiDiHocKhongLuong;
            }
            set
            {
                SetPropertyValue("NghiDiHocKhongLuong", ref _NghiDiHocKhongLuong, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ đi học có lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NghiDiHocCoLuong
        {
            get
            {
                return _NghiDiHocCoLuong;
            }
            set
            {
                SetPropertyValue("NghiDiHocCoLuong", ref _NghiDiHocCoLuong, value);
            }
        }

        //BUH
        [ModelDefault("Caption", "Nghỉ ốm/ Vợ sinh")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NghiOm
        {
            get
            {
                return _NghiOm;
            }
            set
            {
                SetPropertyValue("NghiOm", ref _NghiOm, value);
            }
        }

        [ModelDefault("Caption", "Đánh giá")]
        public string DanhGia
        {
            get
            {
                return _DanhGia;
            }
            set
            {
                SetPropertyValue("DanhGia", ref _DanhGia, value);
            }
        }     

        [ModelDefault("Caption", "Trạng thái")]
        private bool TrangThai
        {
            get
            {
                return _TrangThai;
            }
            set
            {
                SetPropertyValue("TrangThai", ref _TrangThai, value);
            }
        }

        [ModelDefault("Caption", "Khóa")]
        private bool Khoa
        {
            get
            {
                return _Khoa;
            }
            set
            {
                SetPropertyValue("Khoa", ref _Khoa, value);
            }
        }

        [Size(8000)]
        [ModelDefault("Caption", "Diễn giải")]
        public string DienGiai
        {
            get
            {
                return _DienGiai;
            }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }  

        public CC_ChiTietChamCongNhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateNVList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateNVList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
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
