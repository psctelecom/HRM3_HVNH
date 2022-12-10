using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Base;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_BienDong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Điều chỉnh BHTN")]
    public class BienDong_BHTN : BienDong
    {
        // Fields...
        private LoaiBienDongEnum _PhanLoai = LoaiBienDongEnum.BoSung;
        private decimal _TNGD;
        private int _TNVK;
        private decimal _PCCV;
        private decimal _TienLuong;
        private decimal _PCK;

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

        [ModelDefault("Caption", "% Vượt khung")]
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
        [ModelDefault("Caption", "% Thâm niên")]
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
                    LoaiBienDong = value == LoaiBienDongEnum.BoSung ? "Tăng BHTN" : "Giảm BHTN";
                }
            }
        }

        public BienDong_BHTN(Session session) : base(session) { }

        protected override void AfterThongTinNhanVienChanged()
        {
            TienLuong = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
            PCCV = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu;
            TNGD = ThongTinNhanVien.NhanVienThongTinLuong.ThamNien;
            TNVK = ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung;
            PCK = ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac;
        }
        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //cập nhật hồ sơ bảo hiểm
                HoSoBaoHiem hoSo = Session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien));
                if (hoSo != null)
                {
                    if (PhanLoai == LoaiBienDongEnum.BoSung)
                        hoSo.KhongThamGiaBHTN = false;
                    else
                        hoSo.KhongThamGiaBHTN = true;

                    //tạo quá trình tham gia BHXH
                    QuaTrinhThamGiaBHXH qtBHXH = Session.FindObject<QuaTrinhThamGiaBHXH>(CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?", ThongTinNhanVien, TuNgay));
                    if (qtBHXH == null)
                    {
                        qtBHXH = new QuaTrinhThamGiaBHXH(Session);
                    }
                    qtBHXH.HoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien));
                    qtBHXH.TuNam = TuNgay;
                    qtBHXH.DenNam = DenNgay;
                    qtBHXH.KhongThamGiaBHTN = hoSo.KhongThamGiaBHTN;
                    qtBHXH.KhongThamGiaBHYT = String.IsNullOrEmpty(hoSo.SoTheBHYT) ? true : false;
                }
            }
        }

        protected override void OnDeleting()
        {
            HoSoBaoHiem hoSo = Session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien));
            if (hoSo != null)
            {
                if (PhanLoai == LoaiBienDongEnum.BoSung)
                    hoSo.KhongThamGiaBHTN = true;
                else
                    hoSo.KhongThamGiaBHTN = false;
            }
            QuaTrinhThamGiaBHXH qtBHXH = Session.FindObject<QuaTrinhThamGiaBHXH>(CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?", ThongTinNhanVien, TuNgay));
            if (qtBHXH != null)
            {
                Session.Delete(qtBHXH);
                Session.Save(qtBHXH);
            }

            base.OnDeleting();
        }
    }

}
