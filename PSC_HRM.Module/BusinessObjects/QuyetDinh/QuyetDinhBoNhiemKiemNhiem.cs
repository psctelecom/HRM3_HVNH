using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.Xpo.DB;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BoNhiem;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định bổ nhiệm kiêm nhiệm")]
    public class QuyetDinhBoNhiemKiemNhiem : QuyetDinhCaNhan
    {
        private bool _HetHieuLuc;
        private BoPhan _BoPhanMoi;
        private decimal _NhiemKy;
        private decimal _HSPCKiemNhiemMoi;
        private ChucVu _ChucVuKiemNhiemMoi;
        private DateTime _NgayHetNhiemKy;
        private ChucVu _ChucVuKiemNhiemCu;
        private bool _QuyetDinhMoi;
        private decimal _HSPCKiemNhiemCu;
        //
        private decimal _HSPCKiemNhiemTrongTruongCu;
        private decimal _HSPCKiemNhiemTrongTruongMoi;
        private decimal _HSPCLanhDaoMoi;
        private decimal _HSPCLanhDaoCu;
        private decimal _HSPCChucVuBaoLuuCu;
        private decimal _HSPCChucVuCu;
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Kiêm nhiệm chức vụ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVu ChucVuKiemNhiemMoi
        {
            get
            {
                return _ChucVuKiemNhiemMoi;
            }
            set
            {
                SetPropertyValue("ChucVuKiemNhiemMoi", ref _ChucVuKiemNhiemMoi, value);
                if (!IsLoading && value != null)
                {
                    //if (ChucVuKiemNhiemCu == null ||
                    //    (ChucVuKiemNhiemCu != null &&
                    //    ChucVuKiemNhiemCu.HSPCChucVu < value.HSPCChucVu))
                    //    HSPCKiemNhiemMoi = value.HSPCChucVu * 0.1m;
                    //XuLy();

                    HSPCKiemNhiemMoi = value.HSPCChucVu;
                }
            }
        }

        [ModelDefault("Caption", "Tại Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhanMoi
        {
            get
            {
                return _BoPhanMoi;
            }
            set
            {
                SetPropertyValue("BoPhanMoi", ref _BoPhanMoi, value);
            }
        }

        [ModelDefault("Caption", "HSPC kiêm nhiệm")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCKiemNhiemMoi
        {
            get
            {
                return _HSPCKiemNhiemMoi;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiemMoi", ref _HSPCKiemNhiemMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "Số năm nhiệm kỳ")]
        public decimal NhiemKy
        {
            get
            {
                return _NhiemKy;
            }
            set
            {
                SetPropertyValue("NhiemKy", ref _NhiemKy, value);
                if (!IsLoading && NhiemKy > 0 && NgayHieuLuc != DateTime.MinValue)
                {
                    int soThang = (int)Math.Round(value * 12m, MidpointRounding.AwayFromZero);
                    NgayHetNhiemKy = NgayHieuLuc.AddMonths(soThang).AddDays(-1);
                }
            }
        }

        [ModelDefault("Caption", "Ngày hết nhiệm kỳ")]
        public DateTime NgayHetNhiemKy
        {
            get
            {
                return _NgayHetNhiemKy;
            }
            set
            {
                SetPropertyValue("NgayHetNhiemKy", ref _NgayHetNhiemKy, value);
            }
        }

        [ModelDefault("Caption", "Quyết định còn hiệu lực")]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Chức vụ kiêm nhiệm cũ")]
        public ChucVu ChucVuKiemNhiemCu
        {
            get
            {
                return _ChucVuKiemNhiemCu;
            }
            set
            {
                SetPropertyValue("ChucVuKiemNhiemCu", ref _ChucVuKiemNhiemCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "HSPC kiêm nhiệm cũ")]
        public decimal HSPCKiemNhiemCu
        {
            get
            {
                return _HSPCKiemNhiemCu;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiemCu", ref _HSPCKiemNhiemCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "HSPC chức vụ cũ")]
        public decimal HSPCChucVuCu
        {
            get
            {
                return _HSPCChucVuCu;
            }
            set
            {
                SetPropertyValue("HSPCChucVuCu", ref _HSPCChucVuCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "HSPCCV bảo lưu cũ")]
        public decimal HSPCChucVuBaoLuuCu
        {
            get
            {
                return _HSPCChucVuBaoLuuCu;
            }
            set
            {
                SetPropertyValue("HSPCChucVuBaoLuuCu", ref _HSPCChucVuBaoLuuCu, value);
            }
        }
        
        [ModelDefault("Caption", "HSPC kiêm nhiệm (trường) cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCKiemNhiemTrongTruongCu
        {
            get
            {
                return _HSPCKiemNhiemTrongTruongCu;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiemTrongTruongCu", ref _HSPCKiemNhiemTrongTruongCu, value);
            }
        }


        [ModelDefault("Caption", "HSPC kiêm nhiệm (trường) mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCKiemNhiemTrongTruongMoi
        {
            get
            {
                return _HSPCKiemNhiemTrongTruongMoi;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiemTrongTruongMoi", ref _HSPCKiemNhiemTrongTruongMoi, value);
            }
        }

        [ModelDefault("Caption", "HSPC lãnh đạo mới")]
        public decimal HSPCLanhDaoMoi
        {
            get
            {
                return _HSPCLanhDaoMoi;
            }
            set
            {
                SetPropertyValue("HSPCLanhDaoMoi", ref _HSPCLanhDaoMoi, value);
            }
        }

        [ModelDefault("Caption", "HSPC lãnh đạo cũ")]
        public decimal HSPCLanhDaoCu
        {
            get
            {
                return _HSPCLanhDaoCu;
            }
            set
            {
                SetPropertyValue("HSPCLanhDaoCu", ref _HSPCLanhDaoCu, value);
            }
        }

        [Browsable(false)]
        public bool HetHieuLuc
        {
            get
            {
                return _HetHieuLuc;

            }
            set
            {
                SetPropertyValue("HetHieuLuc", ref _HetHieuLuc, value);
            }
        }

        public QuyetDinhBoNhiemKiemNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhBoNhiemKiemNhiem;
        }

        protected override void AfterNhanVienChanged()
        {
            ChucVuKiemNhiemCu = ThongTinNhanVien.ChucVuKiemNhiem;
            HSPCKiemNhiemCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCKiemNhiem;
            HSPCChucVuCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu;
            HSPCChucVuBaoLuuCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu;
            //
            if (GiayToHoSo != null)
                GiayToHoSo.HoSo = ThongTinNhanVien;
        }

        protected override void OnLoaded()
        {
            base.OnLoading();

            if (GiayToHoSo == null)
            {
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
                if (GiayToList.Count > 0 && SoQuyetDinh != null)
                {
                    GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định", SoQuyetDinh);
                    if (GiayToList.Count > 0)
                        GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
                }
            }
            MaTruong = TruongConfig.MaTruong;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                if (QuyetDinhMoi
                    && !HetHieuLuc)
                {
                    //kiểm tra xem chức vụ kiệm nhiệm hiện tại
                    //nếu chưa có hoặc hspc chức vụ thì lấy chức vụ mới làm chức vụ kiêm nhiệm
                    if (ChucVuKiemNhiemCu == null ||
                        (ChucVuKiemNhiemCu != null &&
                         (ChucVuKiemNhiemCu.HSPCChucVu < ChucVuKiemNhiemMoi.HSPCChucVu ||
                         ChucVuKiemNhiemCu.HSPCQuanLy < ChucVuKiemNhiemMoi.HSPCQuanLy)))
                    {
                        ThongTinNhanVien.ChucVuKiemNhiem = ChucVuKiemNhiemMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCKiemNhiem = HSPCKiemNhiemMoi;
                        
                        if (HSPCKiemNhiemTrongTruongMoi > 0)
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCKiemNhiemTrongTruong = HSPCKiemNhiemTrongTruongMoi;
                        }
                    }

                    //Thêm chức vụ kiêm nhiệm vào thông tin nhân viên
                    //if (TruongConfig.MaTruong.Equals("HBU"))
                    //{
                        ChucVuKiemNhiem chucvukiemnhiem = Session.FindObject<ChucVuKiemNhiem>(CriteriaOperator.Parse("QuyetDinhBoNhiemKiemNhiem = ? and ThongTinNhanVien = ?", this.Oid, ThongTinNhanVien.Oid));
                        if(chucvukiemnhiem == null)
                        {
                            chucvukiemnhiem = new ChucVuKiemNhiem(Session);
                            chucvukiemnhiem.ThongTinNhanVien = ThongTinNhanVien;
                            chucvukiemnhiem.QuyetDinhBoNhiemKiemNhiem = this;
                        }
                        chucvukiemnhiem.NgayBoNhiem = NgayHieuLuc;
                        chucvukiemnhiem.BoPhan = BoPhanMoi;
                        chucvukiemnhiem.ChucVu = ChucVuKiemNhiemMoi;
                    //}
                    if (MaTruong.Equals("QNU"))
                    {
                        if (HSPCChucVuCu < HSPCKiemNhiemMoi && HSPCChucVuBaoLuuCu < HSPCKiemNhiemMoi)
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = HSPCKiemNhiemMoi;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu = 0;
                        }
                    }
                }

                //quá trình bổ nhiệm chức vụ
                QuaTrinhHelper.CreateQuaTrinhBoNhiem(Session, this, ChucVuKiemNhiemMoi, 0, DateTime.MinValue);

                //quá trình công tác
                //chỉ tạo quá trình công tác, khi nào miễn nhiệm thì điền thông tin đến năm vào
                QuaTrinhHelper.CreateQuaTrinhCongTac(Session, this, string.Format("Kiêm nhiệm chức vụ {0} {1}", ChucVuKiemNhiemMoi.TenChucVu, BoPhanMoi.TenBoPhan));

                //Cập nhật đến ngày của diễn biến lương
                if ((HSPCKiemNhiemMoi > 0 && ChucVuKiemNhiemCu != null))
                    QuaTrinhHelper.UpdateDienBienLuong(Session, this, ThongTinNhanVien, NgayHieuLuc);

                //Tạo mới diễn biến lương
                if (HSPCKiemNhiemMoi > 0)
                    QuaTrinhHelper.CreateDienBienLuong(Session, this, ThongTinNhanVien, NgayHieuLuc, null);

                //quan ly bo nhiem
                BoNhiemHelper.CreateBoNhiem(Session, this, ChucVuKiemNhiemMoi, true);

                if (MaTruong.Equals("NEU"))
                {
                    //luu tru giay to ho so can bo huong dan
                    GiayToHoSo.NgayLap = NgayQuyetDinh;
                    GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                    GiayToHoSo.SoGiayTo = SoQuyetDinh;
                    GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                    GiayToHoSo.TrichYeu = NoiDung;
                }

                //if (MaTruong.Equals("QNU"))
                //{
                //    if (HSPCChucVuCu < HSPCKiemNhiemMoi && HSPCChucVuBaoLuuCu < HSPCKiemNhiemMoi)
                //    {
                //        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = HSPCKiemNhiemMoi;
                //        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu = 0;
                //    }
                //}
            }
        }

        protected override void OnDeleting()
        {
            if (QuyetDinhMoi)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                using (XPCollection<QuyetDinhBoNhiemKiemNhiem> quyetdinh = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    if (quyetdinh.Count > 0)
                    {
                        if (quyetdinh[0] == This && QuyetDinhMoi)
                        {
                            ThongTinNhanVien.ChucVuKiemNhiem = ChucVuKiemNhiemCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCKiemNhiem = HSPCKiemNhiemCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCKiemNhiemTrongTruong = HSPCKiemNhiemTrongTruongCu;
                        }
                    }
                }
                if (MaTruong.Equals("QNU"))
                {
                    if (HSPCChucVuCu < HSPCKiemNhiemMoi && HSPCChucVuBaoLuuCu < HSPCKiemNhiemMoi)
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = HSPCChucVuCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu = HSPCChucVuBaoLuuCu;
                    }
                }
            }

            //xoa qua trinh bo nhiem
            QuaTrinhHelper.DeleteQuaTrinhNhanVien<QuaTrinhBoNhiem>(Session, this);

            //xoa qua trinh cong tac
            QuaTrinhHelper.DeleteQuaTrinhHoSo<QuaTrinhCongTac>(Session, ThongTinNhanVien, this);

            //xoa dien bien luong
            QuaTrinhHelper.DeleteQuaTrinhNhanVien<DienBienLuong>(Session, this);

            //xoa quan ly bo nhiem
            BoNhiemHelper.DeleteBoNhiem<ChiTietBoNhiem>(Session, this);

            //xóa chức vụ kiêm nhiệm
            BoNhiemHelper.DeleteChucVuKiemNhiem<ChucVuKiemNhiem>(Session, this);

            if (MaTruong.Equals("NEU"))
            {
                //xoa giay to
                if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);
            }

            //if (MaTruong.Equals("QNU"))
            //{
            //    if (HSPCChucVuCu < HSPCKiemNhiemMoi && HSPCChucVuBaoLuuCu < HSPCKiemNhiemMoi)
            //    {
            //        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = HSPCChucVuCu;
            //        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu = HSPCChucVuBaoLuuCu;
            //    }
            //}

            base.OnDeleting();
        }

        private void XuLy()
        {
            HSPCLanhDaoMoi = 0;
            HSPCKiemNhiemTrongTruongMoi = 0;

            decimal hspcldDangVien = 0;
            decimal hspcldChucVuKiemNhiem = 0;
            decimal hspcldChucVuChinh = 0;
            decimal hspcldChucVuDoanThe = 0;
            decimal hspcldChucVuDoanVien = 0;

            hspcldChucVuKiemNhiem = ChucVuKiemNhiemMoi.HSPCQuanLy;
            if (ThongTinNhanVien.ChucVu != null)
                hspcldChucVuChinh = ThongTinNhanVien.ChucVu.HSPCQuanLy;

            ///dua vo danh sach
            List<decimal> hspcChucVu = new List<decimal>();
            if (hspcldChucVuChinh > 0)
                hspcChucVu.Add(hspcldChucVuChinh);
            if (hspcldChucVuKiemNhiem > 0)
                hspcChucVu.Add(hspcldChucVuKiemNhiem);
            if (hspcldDangVien > 0)
                hspcChucVu.Add(hspcldDangVien);
            if (hspcldChucVuDoanVien > 0)
                hspcChucVu.Add(hspcldChucVuDoanVien);
            if (hspcldChucVuDoanThe > 0)
                hspcChucVu.Add(hspcldChucVuDoanThe);

            if (hspcChucVu.Count > 1)
            {
                int index = hspcChucVu.Count - 1;
                hspcChucVu.Sort((a, b) => decimal.Compare(a, b));
                
                HSPCKiemNhiemTrongTruongMoi = hspcChucVu[index - 1] * 0.1m;

            }

            CriteriaOperator filter = CriteriaOperator.Parse("Oid!=? and ThongTinNhanVien=? and !HetHieuLuc", Oid, ThongTinNhanVien.Oid);
            SortProperty sort = new SortProperty("ChucVuKiemNhiemMoi.HSPCQuanLy", SortingDirection.Descending);
            using (XPCollection<QuyetDinhBoNhiemKiemNhiem> qdList = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session, filter, sort))
            {
                int i = 0;
                foreach (QuyetDinhBoNhiemKiemNhiem item in qdList)
                {
                    if (i == 0)
                    {
                    //    if (item.ChucVuKiemNhiemMoi.HSPCQuanLy > ChucVuKiemNhiemMoi.HSPCQuanLy)
                    //        HSPCChucVu2Moi = item.ChucVuKiemNhiemMoi.HSPCQuanLy * HamDungChung.CauHinhChung.CauHinhHoSo.TyLeHeSoKiemNhiem1 / 100.0m;
                    //    else
                    //        HSPCChucVu2Moi = ChucVuKiemNhiemMoi.HSPCQuanLy * HamDungChung.CauHinhChung.CauHinhHoSo.TyLeHeSoKiemNhiem1 / 100.0m;
                    }
                    else if (i == 1)
                    {
                        //if (item.ChucVuKiemNhiemMoi.HSPCQuanLy > ChucVuKiemNhiemMoi.HSPCQuanLy)
                        //    HSPCChucVu3Moi = item.ChucVuKiemNhiemMoi.HSPCQuanLy * HamDungChung.CauHinhChung.CauHinhHoSo.TyLeHeSoKiemNhiem2 / 100.0m;
                        //else
                        //    HSPCChucVu3Moi = ChucVuKiemNhiemMoi.HSPCQuanLy * HamDungChung.CauHinhChung.CauHinhHoSo.TyLeHeSoKiemNhiem2 / 100.0m;
                        //break;
                    }
                    i++;
                }
            }
        }
    }
}
