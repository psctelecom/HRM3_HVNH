using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;

namespace PSC_HRM.Module.NghiHuu
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông tin chi tiết")]
    public class DenTuoiNghiHuu : BaseObject, ISupportController, IBoPhan
    {
        private bool _Chon;
        private string _SoHieuCongChuc;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private GioiTinhEnum _GioiTinh;
        private DateTime _NgaySinh;
        private DateTime _NgaySeNghiHuu;
        private bool _NghiHuuSom;
        private string _GhiChu;

        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Số hồ sơ")]
        public string SoHieuCongChuc
        {
            get
            {
                return _SoHieuCongChuc;
            }
            set
            {
                SetPropertyValue("SoHieuCongChuc", ref _SoHieuCongChuc, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [ImmediatePostData]
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
                    SoHieuCongChuc = value.SoHieuCongChuc;
                    BoPhan = value.BoPhan;
                    GioiTinh = value.GioiTinh;
                    NgaySinh = value.NgaySinh;

                    if (value.NgayNghiHuu == DateTime.MinValue)
                    {
                        TuoiNghiHuu tuoiNghiHuu = Session.FindObject<TuoiNghiHuu>(CriteriaOperator.Parse("GioiTinh=?", ThongTinNhanVien.GioiTinh));
                        if (TruongConfig.MaTruong.Equals("QNU"))
                        {
                            if (tuoiNghiHuu != null)                         
                                NgaySeNghiHuu = new DateTime(ThongTinNhanVien.NgaySinh.AddYears(tuoiNghiHuu.Tuoi).AddMonths(1).Year, ThongTinNhanVien.NgaySinh.AddMonths(1).Month, 1);
              
                            else if (value.GioiTinh == GioiTinhEnum.Nam)
                                NgaySeNghiHuu = new DateTime(ThongTinNhanVien.NgaySinh.AddYears(60).Year, ThongTinNhanVien.NgaySinh.AddMonths(1).Month, 1);
                            else
                                NgaySeNghiHuu = new DateTime(ThongTinNhanVien.NgaySinh.AddYears(55).Year, ThongTinNhanVien.NgaySinh.AddMonths(1).Month, 1);
                        }
                        else
                        {
                            if (tuoiNghiHuu != null)
                                NgaySeNghiHuu = ThongTinNhanVien.NgaySinh.AddYears(tuoiNghiHuu.Tuoi);
                            else if (value.GioiTinh == GioiTinhEnum.Nam)
                                NgaySeNghiHuu = value.NgaySinh.AddYears(60);
                            else
                                NgaySeNghiHuu = value.NgaySinh.AddYears(55);
                        }
                    }
                    else
                        NgaySeNghiHuu = value.NgayNghiHuu;
                }
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
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

        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return _GioiTinh;
            }
            set
            {
                SetPropertyValue("GioiTinh", ref _GioiTinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày sẽ nghỉ hưu")]
        public DateTime NgaySeNghiHuu
        {
            get
            {
                return _NgaySeNghiHuu;
            }
            set
            {
                SetPropertyValue("NgaySeNghiHuu", ref _NgaySeNghiHuu, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ hưu sớm")]
        [ModelDefault("AllowEdit", "False")]
        public bool NghiHuuSom
        {
            get
            {
                return _NghiHuuSom;
            }
            set
            {
                SetPropertyValue("NghiHuuSom", ref _NghiHuuSom, value);
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

        public DenTuoiNghiHuu(Session session)
            : base(session)
        { }

        
    }

}
