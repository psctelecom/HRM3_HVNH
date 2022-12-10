using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DoanDang;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BoNhiem;
using System.Collections.Generic;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thôi chức vụ đoàn")]

    public class QuyetDinhThoiChucVuDoan : QuyetDinhCaNhan
    {
        private decimal _HSPCChucVuDoanCu;
        private decimal _HSPCChucVuMoi;
        private decimal _HSPCChucVu;
        private ChucVuDoan _ChucVuDoanMoi;
        private ChucVuDoan _ChucVuDoanCu;
        private DateTime _NgayPhatSinhBienDong;
        private QuyetDinhBoNhiemChucVuDoan _QuyetDinhBoNhiemChucVuDoan;
        private bool _QuyetDinhMoi;
        private DateTime _NgayThoiHuongHSPCChucVuDoan;
        private string _LyDo;
        private decimal _HSPCThamNien;
        private decimal _HSPCUuDai;

        [Browsable(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Ngày phát sinh biến động")]
        public DateTime NgayPhatSinhBienDong
        {
            get
            {
                return _NgayPhatSinhBienDong;
            }
            set
            {
                SetPropertyValue("NgayPhatSinhBienDong", ref _NgayPhatSinhBienDong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định bổ nhiệm chức vụ đoàn")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuyetDinhList")]
        public QuyetDinhBoNhiemChucVuDoan QuyetDinhBoNhiemChucVuDoan
        {
            get
            {
                return _QuyetDinhBoNhiemChucVuDoan;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiemChucVuDoan", ref _QuyetDinhBoNhiemChucVuDoan, value);
                if (!IsLoading && value != null)
                { 
                    ChucVuDoanCu = value.ChucVuDoan;
                    HSPCChucVuDoanCu = value.HSPCChucVuDoan;
                }
            }
        }

        [ModelDefault("Caption", "Ngày thôi hưởng HSPC chức vụ đoàn")]
        public DateTime NgayThoiHuongHSPCChucVuDoan
        {
            get
            {
                return _NgayThoiHuongHSPCChucVuDoan;
            }
            set
            {
                SetPropertyValue("NgayThoiHuongHSPCChucVuDoan", ref _NgayThoiHuongHSPCChucVuDoan, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    NgayPhatSinhBienDong = value;
                }
            }
        }


        [Browsable(false)]
        [ModelDefault("Caption", "Chức vụ đoàn mới")]
        public ChucVuDoan ChucVuDoanMoi
        {
            get
            {
                return _ChucVuDoanMoi;
            }
            set
            {
                SetPropertyValue("ChucVuDoanMoi", ref _ChucVuDoanMoi, value);
               // if (!IsLoading && value != null)
           

            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC chức vụ hiện tại")]
        public decimal HSPCChucVuMoi
        {
            get
            {
                return _HSPCChucVuMoi;
            }
            set
            {
                SetPropertyValue("HSPCChucVuMoi", ref _HSPCChucVuMoi, value);
            }
        }
        //lưu vết
        [Browsable(false)]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC chức vụ  ")]
        public decimal HSPCChucVu
        {
            get
            {
                return _HSPCChucVu;
            }
            set
            {
                SetPropertyValue("HSPCChucVu", ref _HSPCChucVu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("Caption", "HSPC thâm niên  ")]
        public decimal HSPCThamNien
        {
            get
            {
                return _HSPCThamNien;
            }
            set
            {
                SetPropertyValue("HSPCThamNien", ref _HSPCThamNien, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("EditMask", "N3")]
        [ModelDefault("DisplayFormat", "N3")]
        [ModelDefault("Caption", "HSPC ưu đãi  ")]
        public decimal HSPCUuDai
        {
            get
            {
                return _HSPCUuDai;
            }
            set
            {
                SetPropertyValue("HSPCUuDai", ref _HSPCUuDai, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ đoàn cũ")]
        public ChucVuDoan ChucVuDoanCu
        {
            get
            {
                return _ChucVuDoanCu;
            }
            set
            {
                SetPropertyValue("ChucVuDoanCu", ref _ChucVuDoanCu, value);
                if (!IsLoading && value != null)
                    //XuLyPhuCap();
                    HSPCChucVuDoanCu = value.HSPCChucVuDoan;

            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC chức vụ đoàn cũ")]
        public decimal HSPCChucVuDoanCu
        {
            get
            {
                return _HSPCChucVuDoanCu;
            }
            set
            {
                SetPropertyValue("HSPCChucVuDoanCu", ref _HSPCChucVuDoanCu, value);
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

        [ModelDefault("Caption", "Lý do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        public QuyetDinhThoiChucVuDoan(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThoiChucVuDoan;
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định thôi chức vụ Đoàn"));
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateQuyetDinhList();

            //if (GiayToHoSo == null)
            //{
            //    GiayToList = ThongTinNhanVien.ListGiayToHoSo;
            //    if (GiayToList.Count > 0 && SoQuyetDinh != null)
            //    {
            //        GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định", SoQuyetDinh);
            //        if (GiayToList.Count > 0)
            //            GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
            //    }
            //}
        }

        protected override void QuyetDinhChanged()
        {
            if (NgayHieuLuc != DateTime.MinValue)
                NgayPhatSinhBienDong = NgayHieuLuc;
        }

        protected override void AfterNhanVienChanged()
        {
            QuyetDinhBoNhiemChucVuDoan = null;
            UpdateQuyetDinhList();

            HSPCChucVu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu;
            HSPCThamNien = ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien;
            HSPCUuDai = ThongTinNhanVien.NhanVienThongTinLuong.HSPCUuDai;
        }

        [Browsable(false)]
        public XPCollection<QuyetDinhBoNhiemChucVuDoan> QuyetDinhList { get; set; }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhBoNhiemChucVuDoan>(Session);
            QuyetDinhList.Criteria = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien.Oid);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                QuyetDinhBoNhiemChucVuDoan.HetHieuLuc = true;

                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                DoanVien doanvien = Session.FindObject<DoanVien>(filter);
                if (QuyetDinhMoi)
                {
                    
                    doanvien.ChucVuDoan = ChucVuDoanMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = HSPCChucVuMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuDoan = 0;

                    //cập nhật các hệ số phụ cấp
                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien = Math.Round(((ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong
                                                                            + ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu
                                                                            + ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu
                                                                            + ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuDoan
                                                                            + ThongTinNhanVien.NhanVienThongTinLuong.HSPCVuotKhung) * ThongTinNhanVien.NhanVienThongTinLuong.ThamNien) / 100, 4);

                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCUuDai = Math.Round((ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong
                                                                            + ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu
                                                                            + ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu
                                                                            + ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuDoan
                                                                            + ThongTinNhanVien.NhanVienThongTinLuong.HSPCVuotKhung), 3);
                   
                }

                if (NgayHieuLuc != DateTime.MinValue)
                {
                    //cập nhật đến ngày của quá trình công tác
                    QuaTrinhHelper.UpdateQuaTrinhCongTac(Session, QuyetDinhBoNhiemChucVuDoan, NgayHieuLuc);

                    //Cập nhật đến ngày của quá trình bổ nhiệm
                    QuaTrinhHelper.UpdateQuaTrinhBoNhiem(Session, QuyetDinhBoNhiemChucVuDoan, NgayHieuLuc);

                    //Cập nhật đến ngày Diễn biến lương
                    QuaTrinhHelper.UpdateDienBienLuong(Session, this, ThongTinNhanVien, NgayHieuLuc);

                    //if (HSPCChucVuMoi > 0)
                    //{
                    //    //Tạo mới diễn biến lương
                    //    QuaTrinhHelper.CreateDienBienLuong(Session, this, ThongTinNhanVien, NgayHieuLuc,null);

                    //}
                }


                //luu tru giay to ho so can bo huong dan
                GiayToHoSo.NgayLap = NgayQuyetDinh;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.SoGiayTo = SoQuyetDinh;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.TrichYeu = NoiDung;
            }
        }

        protected override void OnDeleting()
        {
            if (QuyetDinhMoi)
            {
                QuyetDinhBoNhiemChucVuDoan.HetHieuLuc = false;
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                DoanVien doanvien = Session.FindObject<DoanVien>(filter);

                doanvien.ChucVuDoan = ChucVuDoanCu;
                ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuDoan = HSPCChucVuDoanCu;
                ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = HSPCChucVu;
                ThongTinNhanVien.NhanVienThongTinLuong.HSPCUuDai = HSPCUuDai;
                ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien = HSPCThamNien;
            }

            if (NgayPhatSinhBienDong != DateTime.MinValue)
            {
                //Xóa biến động giảm mức đóng
                BienDongHelper.DeleteBienDong<BienDong_ThayDoiLuong>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);

                //Xóa biến động thay đổi chức danh
                BienDongHelper.DeleteBienDong<BienDong_ThayDoiChucDanh>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);
            }

            //Xóa đến ngày trong quá trình bổ nhiệm
            QuaTrinhHelper.ResetQuaTrinhBoNhiem(Session, QuyetDinhBoNhiemChucVuDoan);

            //Xóa đến ngày diễn biến lương
            QuaTrinhHelper.ResetDienBienLuong(Session, ThongTinNhanVien);

            //Xóa đến ngày trong quá trình công tác
            QuaTrinhHelper.ResetQuaTrinhCongTac(Session, QuyetDinhBoNhiemChucVuDoan);

         
            //xoa giay to
            if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);

            base.OnDeleting();
        }

        //private void XuLyChucVu()
        //{
        //    int count = 0;
        //    //kiem nhiem
        //    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and !HetHieuLuc", ThongTinNhanVien);
        //    SortProperty sort = new SortProperty("ChucVuKiemNhiemMoi.HSPCChucVu", SortingDirection.Descending);
        //    using (XPCollection<QuyetDinhBoNhiemKiemNhiem> qdList = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session, filter, sort))
        //    {
        //        foreach (QuyetDinhBoNhiemKiemNhiem item in qdList)
        //        {
        //            //kiem tra xem quyet dinh bo nhiem kiem nhiem da duoc su dung de lam quyet dinh bo nhiem chua?
        //            if (item.ChucVuKiemNhiemMoi != QuyetDinhBoNhiem.ChucVuMoi
        //                && item.BoPhanMoi != QuyetDinhBoNhiem.BoPhan)
        //            {
        //                if (count == 0)
        //                {
        //                    ChucVuMoi = item.ChucVuKiemNhiemMoi;
        //                    HSPCChucVuMoi = item.ChucVuKiemNhiemMoi.HSPCChucVu;
        //                    HSPCChucVu1Moi = ChucVuMoi.HSPCQuanLy;
        //                }
        //                else
        //                {
        //                    ChucVuKiemNhiemMoi = item.ChucVuKiemNhiemMoi;
        //                    HSPCKiemNhiemMoi = item.ChucVuKiemNhiemMoi.HSPCChucVu * 0.1m;
        //                    break;
        //                }
        //                count++;
        //            }
        //        }
        //    }
        //}

        private void XuLyPhuCap()
        {
            //List<Guid> oid = new List<Guid>();
            //if (ChucVuMoi != null)
            //{
            //    oid.Add(ChucVuMoi.Oid);
            //}
            //if (ChucVuKiemNhiemMoi != null)
            //{
            //    oid.Add(ChucVuKiemNhiemMoi.Oid);
            //}

            //if (oid.Count == 2)
            //{
            //    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and !HetHieuLuc", ThongTinNhanVien.Oid);
            //    GroupOperator go = new GroupOperator(GroupOperatorType.And);
            //    go.Operands.Add(filter);
            //    go.Operands.Add(new InOperator("ChucVuKiemNhiemMoi.Oid", oid).Not());
            //    SortProperty sort = new SortProperty("ChucVuKiemNhiemMoi.HSPCQuanLy", SortingDirection.Descending);
            //    using (XPCollection<QuyetDinhBoNhiemKiemNhiem> qdList = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session, go, sort))
            //    {
            //        foreach (QuyetDinhBoNhiemKiemNhiem item in qdList)
            //        {
            //            HSPCChucVu3Moi = item.ChucVuKiemNhiemMoi.HSPCQuanLy * HamDungChung.CauHinhChung.CauHinhHoSo.TyLeHeSoKiemNhiem2 / 100m;
            //            break;
            //        }
            //    }
            //}
        }
    }
}
