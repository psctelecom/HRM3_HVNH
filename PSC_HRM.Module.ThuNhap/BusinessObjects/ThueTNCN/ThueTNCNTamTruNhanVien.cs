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

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [ImageName("BO_HoaDon")]
    [DefaultProperty("NhanVien")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowEdit", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "Thuế TNCN tạm trừ cán bộ có HĐLĐ")]
    [Appearance("ThueTNCNTamTruNhanVien.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "BangThueTNCNTamTru is not null and ((BangThueTNCNTamTru.KyTinhLuong is not null and BangThueTNCNTamTru.KyTinhLuong.KhoaSo) or BangThueTNCNTamTru.ChungTu is not null)")]
    [RuleCombinationOfPropertiesIsUnique("ThueTNCNTamTruNhanVien.Unique", DefaultContexts.Save, "BangThueTNCNTamTru;NhanVien")]
    public class ThueTNCNTamTruNhanVien : BaseObject, IBoPhan
    {
        private decimal _ThueTNCNConThieu;
        private decimal _ThueTNCNNopThua;
        private decimal _ThuNhapChiuThue;
        private decimal _ThuNhapTrongThang;
        private decimal _TongThuNhap;
        private decimal _BHTNTrongThang;
        private decimal _BHYTTrongThang;
        private BangThueTNCNTamTru _BangThueTNCNTamTru;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private decimal _TongThuNhapChiuThue;
        private decimal _TongThueTNCNTamTru;
        private decimal _TongGiamTruGiaCanh;
        private decimal _TongTuThienNhanDao;
        private decimal _TongBHXH;
        private decimal _TongBHYT;
        private decimal _TongBHTN;
        private decimal _TongThuNhapTinhThue;
        private decimal _TongTNCTLamCanCuGiamTru;
        private decimal _ThuNhapChiuThueTrongThang;
        private decimal _GiamTruGiaCanhTrongThang;
        private decimal _TuThienNhanDaoTrongThang;
        private decimal _BHXHTrongThang;
        private decimal _ThuNhapTinhThueTrongThang;
        private decimal _ThueTNCNTamTruTrongThang;
        private decimal _ThuNhap;
        private decimal _ThueTNCNTamTru;
        private int _SoNguoiPhuThuoc;
        private int _SoThang = 1;

        [Browsable(false)]
        [Association("BangThueTNCNTamTru-DanhSachThueTNCNTamTru")]
        public BangThueTNCNTamTru BangThueTNCNTamTru
        {
            get
            {
                return _BangThueTNCNTamTru;
            }
            set
            {
                SetPropertyValue("BangThueTNCNTamTru", ref _BangThueTNCNTamTru, value);
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





        //********************************************************************
        //Tổng phát sinh từ đầu năm
        //********************************************************************
        //Tổng thu nhập từ đầu năm tới thời điểm hiện tại
        [ModelDefault("Caption", "Tổng thu nhập")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongThuNhap
        {
            get
            {
                return _TongThuNhap;
            }
            set
            {
                SetPropertyValue("TongThuNhap", ref _TongThuNhap, value);
            }
        }

        //Tổng thu nhập chịu thuế từ đầu năm tới thời điểm hiện tại
        [ModelDefault("Caption", "Tổng thu nhập chịu thuế")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        //tổng giảm trừ thu nhập chịu thuế từ đầu năm
        [ModelDefault("Caption", "Tổng giảm trừ gia cảnh")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongGiamTruGiaCanh
        {
            get
            {
                return _TongGiamTruGiaCanh;
            }
            set
            {
                SetPropertyValue("TongGiamTruGiaCanh", ref _TongGiamTruGiaCanh, value);
            }
        }

        [ModelDefault("Caption", "Tổng từ thiện nhân đạo")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongTuThienNhanDao
        {
            get
            {
                return _TongTuThienNhanDao;
            }
            set
            {
                SetPropertyValue("TongTuThienNhanDao", ref _TongTuThienNhanDao, value);
            }
        }

        [ModelDefault("Caption", "Tổng BHXH")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongBHXH
        {
            get
            {
                return _TongBHXH;
            }
            set
            {
                SetPropertyValue("TongBHXH", ref _TongBHXH, value);
            }
        }

        [ModelDefault("Caption", "Tổng BHYT")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongBHYT
        {
            get
            {
                return _TongBHYT;
            }
            set
            {
                SetPropertyValue("TongBHYT", ref _TongBHYT, value);
            }
        }

        [ModelDefault("Caption", "Tổng BHTN")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongBHTN
        {
            get
            {
                return _TongBHTN;
            }
            set
            {
                SetPropertyValue("TongBHTN", ref _TongBHTN, value);
            }
        }

        [ModelDefault("Caption", "Tổng thu nhập tính thuế")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongThuNhapTinhThue
        {
            get
            {
                return _TongThuNhapTinhThue;
            }
            set
            {
                SetPropertyValue("TongThuNhapTinhThue", ref _TongThuNhapTinhThue, value);
            }
        }

        //Tổng thuế TNCN đã tạm thu từ đầu năm tới giờ
        [ModelDefault("Caption", "Tổng thuế TNCN đã tạm thu")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongThueTNCNTamTru
        {
            get
            {
                return _TongThueTNCNTamTru;
            }
            set
            {
                SetPropertyValue("TongThueTNCNTamTru", ref _TongThueTNCNTamTru, value);
            }
        }

        //thu nhập từ tiền công tiền lương từ khu kinh tế
        //mình không sử dụng
        [ModelDefault("Caption", "Tổng TNCT làm căn cứ giảm trừ")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
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






        //********************************************************************
        //Phát sinh trong tháng
        //********************************************************************
        [ModelDefault("Caption", "Thu nhập trong tháng")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal ThuNhapTrongThang
        {
            get
            {
                return _ThuNhapTrongThang;
            }
            set
            {
                SetPropertyValue("ThuNhapTrongThang", ref _ThuNhapTrongThang, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập chịu thuế trong tháng")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal ThuNhapChiuThueTrongThang
        {
            get
            {
                return _ThuNhapChiuThueTrongThang;
            }
            set
            {
                SetPropertyValue("ThuNhapChiuThueTrongThang", ref _ThuNhapChiuThueTrongThang, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ gia cảnh trong tháng")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal GiamTruGiaCanhTrongThang
        {
            get
            {
                return _GiamTruGiaCanhTrongThang;
            }
            set
            {
                SetPropertyValue("GiamTruGiaCanhTrongThang", ref _GiamTruGiaCanhTrongThang, value);
            }
        }

        [ModelDefault("Caption", "Từ thiện nhân đạo trong tháng")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TuThienNhanDaoTrongThang
        {
            get
            {
                return _TuThienNhanDaoTrongThang;
            }
            set
            {
                SetPropertyValue("TuThienNhanDaoTrongThang", ref _TuThienNhanDaoTrongThang, value);
            }
        }

        [ModelDefault("Caption", "BHXH trong tháng")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal BHXHTrongThang
        {
            get
            {
                return _BHXHTrongThang;
            }
            set
            {
                SetPropertyValue("BHXHTrongThang", ref _BHXHTrongThang, value);
            }
        }

        [ModelDefault("Caption", "BHYT trong tháng")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal BHYTTrongThang
        {
            get
            {
                return _BHYTTrongThang;
            }
            set
            {
                SetPropertyValue("BHYTTrongThang", ref _BHYTTrongThang, value);
            }
        }

        [ModelDefault("Caption", "BHTN trong tháng")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal BHTNTrongThang
        {
            get
            {
                return _BHTNTrongThang;
            }
            set
            {
                SetPropertyValue("BHTNTrongThang", ref _BHTNTrongThang, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập tính thuế trong tháng")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal ThuNhapTinhThueTrongThang
        {
            get
            {
                return _ThuNhapTinhThueTrongThang;
            }
            set
            {
                SetPropertyValue("ThuNhapTinhThueTrongThang", ref _ThuNhapTinhThueTrongThang, value);
            }
        }

        [ModelDefault("Caption", "Thuế TNCN tạm trừ trong tháng")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal ThueTNCNTamTruTrongThang
        {
            get
            {
                return _ThueTNCNTamTruTrongThang;
            }
            set
            {
                SetPropertyValue("ThueTNCNTamTruTrongThang", ref _ThueTNCNTamTruTrongThang, value);
            }
        }







        //********************************************************************
        //Từng lần phát sinh trong tháng
        //********************************************************************
        [ModelDefault("Caption", "Thu nhập")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThuNhap
        {
            get
            {
                return _ThuNhap;
            }
            set
            {
                SetPropertyValue("ThuNhap", ref _ThuNhap, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập chịu thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThuNhapChiuThue
        {
            get
            {
                return _ThuNhapChiuThue;
            }
            set
            {
                SetPropertyValue("ThuNhapChiuThue", ref _ThuNhapChiuThue, value);
            }
        }

        //Thuế TNCN tạm thu
        [ModelDefault("Caption", "Thuế TNCN tạm trừ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal ThueTNCNTamTru
        {
            get
            {
                return _ThueTNCNTamTru;
            }
            set
            {
                SetPropertyValue("ThueTNCNTamTru", ref _ThueTNCNTamTru, value);
            }
        }

        [ModelDefault("Caption", "Số người phụ thuộc")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int SoNguoiPhuThuoc
        {
            get
            {
                return _SoNguoiPhuThuoc;
            }
            set
            {
                SetPropertyValue("SoNguoiPhuThuoc", ref _SoNguoiPhuThuoc, value);
            }
        }

        [ModelDefault("Caption", "Số tháng phát sinh thu nhập")]
        public int SoThang
        {
            get
            {
                return _SoThang;
            }
            set
            {
                SetPropertyValue("SoThang", ref _SoThang, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thuế TNCN nộp thừa")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal ThueTNCNNopThua
        {
            get
            {
                return _ThueTNCNNopThua;
            }
            set
            {
                SetPropertyValue("ThueTNCNNopThua", ref _ThueTNCNNopThua, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thuế TNCN còn thiếu")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal ThueTNCNConThieu
        {
            get
            {
                return _ThueTNCNConThieu;
            }
            set
            {
                SetPropertyValue("ThueTNCNConThieu", ref _ThueTNCNConThieu, value);
            }
        }

        public ThueTNCNTamTruNhanVien(Session session) : base(session) { }

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
