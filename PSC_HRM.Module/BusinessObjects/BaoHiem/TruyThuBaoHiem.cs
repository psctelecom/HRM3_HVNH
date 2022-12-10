using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_TruyThuBaoHiem")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết truy thu bảo hiểm")]
    public class TruyThuBaoHiem : BaseObject
    {
        // Fields...
        private int _SoThangNopCham;
        private decimal _LuongToiThieu;
        private decimal _LaiBHTN;
        private decimal _LaiBHYT;
        private decimal _LaiBHXH;
        private QuyetDinh.QuyetDinh _QuyetDinh;
        private QuanLyTruyThuBaoHiem _QuanLyTruyThuBaoHiem;
        private string _GhiChu;
        //private decimal _SoTienBHYT;
        //private decimal _SoTienBHTN;
        //private decimal _SoTienBHXH;
        private int _TongThoiGianChamDong;
        private decimal _PTBHTN;
        private decimal _PTBHYT;
        private decimal _PTBHXH;
        private int _SoThangTruyThu;
        private DateTime _DenThang;
        private DateTime _TuThang;
        private decimal _ChenhLechTienLuong;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;

        [Browsable(false)]
        [Association("QuanLyTruyThuBaoHiem-ListTruyThuBaoHiem")]
        public QuanLyTruyThuBaoHiem QuanLyTruyThuBaoHiem
        {
            get
            {
                return _QuanLyTruyThuBaoHiem;
            }
            set
            {
                SetPropertyValue("QuanLyTruyThuBaoHiem", ref _QuanLyTruyThuBaoHiem, value);
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
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
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
            }
        }

        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinh.QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Chênh lệch tiền lương")]
        [ModelDefault("EditMask", "N3")]
        [ModelDefault("DisplayFormat", "N3")]
        public decimal ChenhLechTienLuong
        {
            get
            {
                return _ChenhLechTienLuong;
            }
            set
            {
                SetPropertyValue("ChenhLechTienLuong", ref _ChenhLechTienLuong, value);
            }
        }

        [ModelDefault("Caption", "Từ tháng năm")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
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

        [ModelDefault("Caption", "Đến tháng năm")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
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

        [ModelDefault("Caption", "Số tháng truy thu")]
        public int SoThangTruyThu
        {
            get
            {
                return _SoThangTruyThu;
            }
            set
            {
                SetPropertyValue("SoThangTruyThu", ref _SoThangTruyThu, value);
            }
        }

        [ModelDefault("Caption", "Số tháng nộp chậm")]
        public int SoThangNopCham
        {
            get
            {
                return _SoThangNopCham;
            }
            set
            {
                SetPropertyValue("SoThangNopCham", ref _SoThangNopCham, value);
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

        [ModelDefault("Caption", "Tỷ lệ đóng BHXH")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal PTBHXH
        {
            get
            {
                return _PTBHXH;
            }
            set
            {
                SetPropertyValue("PTBHXH", ref _PTBHXH, value);
            }
        }

        [ModelDefault("Caption", "Tỷ lệ đóng BHYT")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal PTBHYT
        {
            get
            {
                return _PTBHYT;
            }
            set
            {
                SetPropertyValue("PTBHYT", ref _PTBHYT, value);
            }
        }

        [ModelDefault("Caption", "Tỷ lệ đóng BHTN")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal PTBHTN
        {
            get
            {
                return _PTBHTN;
            }
            set
            {
                SetPropertyValue("PTBHTN", ref _PTBHTN, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tổng thời gian chậm đóng")]
        public int TongThoiGianChamDong
        {
            get
            {
                return _TongThoiGianChamDong;
            }
            set
            {
                SetPropertyValue("TongThoiGianChamDong", ref _TongThoiGianChamDong, value);
                if (!IsLoading && value > 0)
                {
                    //TinhSoTienPhaiNop();
                    TinhLai();
                }
            }
        }

        //[ModelDefault("Caption", "Số tiền đóng BHXH")]
        //[ModelDefault("EditMask", "N0")]
        //[ModelDefault("DisplayFormat", "N0")]
        //public decimal SoTienBHXH
        //{
        //    get
        //    {
        //        return _SoTienBHXH;
        //    }
        //    set
        //    {
        //        SetPropertyValue("SoTienBHXH", ref _SoTienBHXH, value);
        //    }
        //}

        //[ModelDefault("Caption", "Số tiền đóng BHYT")]
        //[ModelDefault("EditMask", "N0")]
        //[ModelDefault("DisplayFormat", "N0")]
        //public decimal SoTienBHYT
        //{
        //    get
        //    {
        //        return _SoTienBHYT;
        //    }
        //    set
        //    {
        //        SetPropertyValue("SoTienBHYT", ref _SoTienBHYT, value);
        //    }
        //}

        //[ModelDefault("Caption", "Số tiền đóng BHTN")]
        //[ModelDefault("EditMask", "N0")]
        //[ModelDefault("DisplayFormat", "N0")]
        //public decimal SoTienBHTN
        //{
        //    get
        //    {
        //        return _SoTienBHTN;
        //    }
        //    set
        //    {
        //        SetPropertyValue("SoTienBHTN", ref _SoTienBHTN, value);
        //    }
        //}

        [ModelDefault("Caption", "Tiền lãi BHXH")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LaiBHXH
        {
            get
            {
                return _LaiBHXH;
            }
            set
            {
                SetPropertyValue("LaiBHXH", ref _LaiBHXH, value);
            }
        }

        [ModelDefault("Caption", "Tiền lãi BHYT")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LaiBHYT
        {
            get
            {
                return _LaiBHYT;
            }
            set
            {
                SetPropertyValue("LaiBHYT", ref _LaiBHYT, value);
            }
        }

        [ModelDefault("Caption", "Tiền lãi BHTN")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LaiBHTN
        {
            get
            {
                return _LaiBHTN;
            }
            set
            {
                SetPropertyValue("LaiBHTN", ref _LaiBHTN, value);
            }
        }

        [Persistent]
        [ModelDefault("Caption", "Tổng tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongTien
        {
            get
            {
                return LaiBHXH + LaiBHYT + LaiBHTN;
            }
        }

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

        public TruyThuBaoHiem(Session session) : base(session) { }

        protected override void OnDeleted()
        {
            BienDong_ThayDoiLuong luong = Session.FindObject<BienDong_ThayDoiLuong>(CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=? and DenNgay=?", ThongTinNhanVien, TuThang, DenThang));
            if (luong != null)
            {
                Session.Delete(luong);
                Session.Save(luong);
            }

            base.OnDeleted();
        }

        //public void TinhSoTienPhaiNop()
        //{
        //    SoTienBHXH = ChenhLechTienLuong * LuongToiThieu * PTBHXH;
        //    SoTienBHYT = ChenhLechTienLuong * LuongToiThieu * PTBHYT;
        //    SoTienBHTN = ChenhLechTienLuong * LuongToiThieu * PTBHTN;
        //}

        public void TinhLai()
        {
            const double bhxh = 0.01183d, bhyt = 0.0075d;

            LaiBHXH = ChenhLechTienLuong * LuongToiThieu * (PTBHXH / 100) * ((decimal)Math.Pow((1 + bhxh), SoThangNopCham) * (decimal)(Math.Pow((1 + bhxh), SoThangTruyThu) - 1) / (decimal)((1 + bhxh) - 1) - SoThangTruyThu);
            LaiBHYT = ChenhLechTienLuong * LuongToiThieu * (PTBHYT / 100) * ((decimal)Math.Pow((1 + bhyt), SoThangNopCham) * (decimal)(Math.Pow((1 + bhyt), SoThangTruyThu) - 1) / (decimal)((1 + bhyt) - 1) - SoThangTruyThu);
            LaiBHTN = ChenhLechTienLuong * LuongToiThieu * (PTBHTN / 100) * ((decimal)Math.Pow((1 + bhxh), SoThangNopCham) * (decimal)(Math.Pow((1 + bhxh), SoThangTruyThu) - 1) / (decimal)((1 + bhxh) - 1) - SoThangTruyThu);

        }
    }

}
