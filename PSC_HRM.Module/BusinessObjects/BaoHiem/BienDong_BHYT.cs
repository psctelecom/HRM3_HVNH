using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_BienDong")]
    [ModelDefault("Caption", "Điều chỉnh BHYT")]
    [DefaultProperty("ThongTinNhanVien")]
    [Appearance("BienDong_BHYT", TargetItems = "SoTheBHYT;TuThang;DenThang;NoiDangKyKhamChuaBenh;QuyenLoiHuongBHYT", Enabled = false, Criteria = "PhanLoai=1")]
    public class BienDong_BHYT : BienDong
    {
        // Fields...
        private decimal _TNGD;
        private int _TNVK;
        private decimal _PCCV;
        private decimal _TienLuong;
        private decimal _PCK;
        private LoaiBienDongEnum _PhanLoai = LoaiBienDongEnum.BoSung;
        private DateTime _DenThang;
        private DateTime _TuThang;
        private QuyenLoiHuongBHYT _QuyenLoiHuongBHYT;
        private BenhVien _NoiDangKyKhamChuaBenh;
        private string _SoTheBHYT;

        [ModelDefault("Caption", "Tiền lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TienLuong
        {
            get
            {
                return _TienLuong;
            }
            set
            {
                SetPropertyValue("TienLuong", ref _TienLuong, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PCCV
        {
            get
            {
                return _PCCV;
            }
            set
            {
                SetPropertyValue("PCCV", ref _PCCV, value);
            }
        }

        [ModelDefault("Caption", "% Thâm niên vượt khung")]
        public int TNVK
        {
            get
            {
                return _TNVK;
            }
            set
            {
                SetPropertyValue("TNVK", ref _TNVK, value);
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% Thâm niên giảng dạy")]
        public decimal TNGD
        {
            get
            {
                return _TNGD;
            }
            set
            {
                SetPropertyValue("TNGD", ref _TNGD, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PCK
        {
            get
            {
                return _PCK;
            }
            set
            {
                SetPropertyValue("PCK", ref _PCK, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại")]
        public LoaiBienDongEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
                if (!IsLoading)
                {
                    LoaiBienDong = value == LoaiBienDongEnum.BoSung ? "Tăng BHYT" : "Giảm BHYT";
                }
            }
        }

        [ModelDefault("Caption", "Số thẻ BHYT")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string SoTheBHYT
        {
            get
            {
                return _SoTheBHYT;
            }
            set
            {
                SetPropertyValue("SoTheBHYT", ref _SoTheBHYT, value);
            }
        }

        [ModelDefault("Caption", "Từ tháng, năm")]
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

        [ModelDefault("Caption", "Đến tháng, năm")]
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

        [ModelDefault("Caption", "Nơi đăng ký khám chữa bệnh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BenhVien NoiDangKyKhamChuaBenh
        {
            get
            {
                return _NoiDangKyKhamChuaBenh;
            }
            set
            {
                SetPropertyValue("NoiDangKyKhamChuaBenh", ref _NoiDangKyKhamChuaBenh, value);
            }
        }

        [ModelDefault("Caption", "Quyền lợi hưởng BHYT")]
        public QuyenLoiHuongBHYT QuyenLoiHuongBHYT
        {
            get
            {
                return _QuyenLoiHuongBHYT;
            }
            set
            {
                SetPropertyValue("QuyenLoiHuongBHYT", ref _QuyenLoiHuongBHYT, value);
            }
        }

        public BienDong_BHYT(Session session) : base(session) { }

        protected override void AfterThongTinNhanVienChanged()
        {
            HoSoBaoHiem hoSo = Session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien));
            if (hoSo != null)
            {
                SoTheBHYT = hoSo.SoTheBHYT;
                TuThang = hoSo.TuNgay;
                DenThang = hoSo.DenNgay;
                NoiDangKyKhamChuaBenh = hoSo.NoiDangKyKhamChuaBenh;
                QuyenLoiHuongBHYT = hoSo.QuyenLoiHuongBHYT;
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted &&
                ThongTinNhanVien != null &&
                TuNgay != DateTime.MinValue)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", 
                    ThongTinNhanVien);
                HoSoBaoHiem hoSo = Session.FindObject<HoSoBaoHiem>(filter);
                if (hoSo != null)
                {
                    if (PhanLoai == LoaiBienDongEnum.BoSung)
                    {
                        hoSo.SoTheBHYT = SoTheBHYT;
                        hoSo.TuNgay = TuThang;
                        hoSo.DenNgay = DenThang;
                        hoSo.NoiDangKyKhamChuaBenh = NoiDangKyKhamChuaBenh;
                        hoSo.QuyenLoiHuongBHYT = QuyenLoiHuongBHYT;
                    }
                    else
                    {
                        hoSo.SoTheBHYT = "";
                        hoSo.TuNgay = DateTime.MinValue;
                        hoSo.DenNgay = DateTime.MinValue;
                        hoSo.NoiDangKyKhamChuaBenh = null;
                        hoSo.QuyenLoiHuongBHYT = null;
                    }

                    //tạo quá trình tham gia BHXH
                    filter = CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?", 
                        ThongTinNhanVien, TuNgay);
                    QuaTrinhThamGiaBHXH qtBHXH = Session.FindObject<QuaTrinhThamGiaBHXH>(filter);
                    if (qtBHXH == null)
                    {
                        qtBHXH = new QuaTrinhThamGiaBHXH(Session);
                        filter = CriteriaOperator.Parse("ThongTinNhanVien=?", 
                            ThongTinNhanVien);
                        qtBHXH.HoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                    }
                    qtBHXH.TuNam = TuNgay;
                    qtBHXH.DenNam = DenNgay;
                    qtBHXH.KhongThamGiaBHTN = hoSo.KhongThamGiaBHTN;
                    qtBHXH.KhongThamGiaBHYT = String.IsNullOrEmpty(hoSo.SoTheBHYT) ? true : false;
                }
            }
        }

        protected override void OnDeleting()
        {
            if (ThongTinNhanVien != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", 
                    ThongTinNhanVien);
                HoSoBaoHiem hoSo = Session.FindObject<HoSoBaoHiem>(filter);
                if (hoSo != null)
                {
                    if (PhanLoai == LoaiBienDongEnum.ThoaiTra)
                    {
                        hoSo.SoTheBHYT = SoTheBHYT;
                        hoSo.TuNgay = TuThang;
                        hoSo.DenNgay = DenThang;
                        hoSo.NoiDangKyKhamChuaBenh = NoiDangKyKhamChuaBenh;
                        hoSo.QuyenLoiHuongBHYT = QuyenLoiHuongBHYT;
                    }
                    else
                    {
                        hoSo.SoTheBHYT = "";
                        hoSo.TuNgay = DateTime.MinValue;
                        hoSo.DenNgay = DateTime.MinValue;
                        hoSo.NoiDangKyKhamChuaBenh = null;
                        hoSo.QuyenLoiHuongBHYT = null;
                    }
                }
                filter = CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?", 
                    ThongTinNhanVien, TuNgay);
                QuaTrinhThamGiaBHXH qtBHXH = Session.FindObject<QuaTrinhThamGiaBHXH>(filter);
                if (qtBHXH != null)
                {
                    Session.Delete(qtBHXH);
                    Session.Save(qtBHXH);
                }
            }
            base.OnDeleting();
        }
    }

}
