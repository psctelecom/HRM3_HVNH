using System;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.BoNhiem;
using PSC_HRM.Module.DoanDang;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Collections.Generic;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định bổ nhiệm chức vụ đoàn")]
    public class QuyetDinhBoNhiemChucVuDoan : QuyetDinhCaNhan
    {
        private bool _HetHieuLuc;
        private DateTime _NgayPhatSinhBienDong;
        private ChucVuDoan _ChucVuDoanCu;
        private decimal _HSPCChucVuDoanCu;
        private ChucVuDoan _ChucVuDoan;
        private decimal _HSPCChucVuDoan;
        private bool _QuyetDinhMoi;
        private string _NhiemKy;
        private DateTime _NgayBoNhiemDoan;
        //
        private decimal _HSPCChucVuCu;
        private decimal _HSPCChucVuBaoLuuCu;
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
        //Lưu vết
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
        //Lưu vết
        [Browsable(false)]
        [ModelDefault("Caption", "HSPC chức vụ bảo lưu cũ")]
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
        //Lưu vết
        [Browsable(false)]
        [ModelDefault("Caption", "HSPC thâm niên")]
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
        //Lưu vết
        [Browsable(false)]
        [ModelDefault("Caption", "HSPC Ưu đãi")]
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
        [ModelDefault("Caption", "Chức vụ đoàn mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVuDoan ChucVuDoan
        {
            get
            {
                return _ChucVuDoan;
            }
            set
            {
                SetPropertyValue("ChucVuDoan", ref _ChucVuDoan, value);
                if(!IsLoading && value != null)
                {
                    HSPCChucVuDoan = value.HSPCChucVuDoan;
                }
                
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "HSPC chức vụ đoàn mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVuDoan
        {
            get
            {
                return _HSPCChucVuDoan;
            }
            set
            {
                SetPropertyValue("HSPCChucVuDoan", ref _HSPCChucVuDoan, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ đoàn cũ")]
        //[RuleRequiredField(DefaultContexts.Save)]
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
                {
                    HSPCChucVuDoanCu = value.HSPCChucVuDoan;
                }

            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "HSPC chức vụ đoàn cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
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

        [ModelDefault("Caption", "Nhiệm kỳ")]
        public string NhiemKy
        {
            get
            {
                return _NhiemKy;
            }
            set
            {
                SetPropertyValue("NhiemKy", ref _NhiemKy, value);
                
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


        [ModelDefault("Caption", "Ngày bổ nhiệm đoàn")]
        public DateTime NgayBoNhiemDoan
        {
            get
            {
                return _NgayBoNhiemDoan;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemDoan", ref _NgayBoNhiemDoan, value);
            }
        }
        public QuyetDinhBoNhiemChucVuDoan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhBoNhiemChucVuDoan;
        }
        protected override void AfterNhanVienChanged()
        {
            HSPCChucVuCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu;
            HSPCChucVuBaoLuuCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu;
            HSPCThamNien = ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien;
            HSPCUuDai = ThongTinNhanVien.NhanVienThongTinLuong.HSPCUuDai;
            //
            if (GiayToHoSo != null)
                GiayToHoSo.HoSo = ThongTinNhanVien;
        }

        protected override void QuyetDinhChanged()
        {
            if (NgayHieuLuc != DateTime.MinValue)
                NgayPhatSinhBienDong = NgayHieuLuc;
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
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                    ThongTinNhanVien.Oid);
                  DoanVien doanvien = Session.FindObject<DoanVien>(filter);

                if (QuyetDinhMoi && !HetHieuLuc && doanvien != null)
                {
                    if (HSPCChucVuDoan > ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu &&
                        HSPCChucVuDoan > ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu)
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = HSPCChucVuDoan;
                        //ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = 0;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu = 0;
                        doanvien.ChucVuDoan = ChucVuDoan;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChucVu = NgayHieuLuc;
                        

                        //cập nhật các hệ số phụ cấp
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien = Math.Round(((ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong
                                                                                + ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu
                                                                                + ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu
                                                                                + ThongTinNhanVien.NhanVienThongTinLuong.HSPCVuotKhung) * ThongTinNhanVien.NhanVienThongTinLuong.ThamNien) / 100, 4);

                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCUuDai = Math.Round((ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong
                                                                                + ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu
                                                                                + ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu
                                                                                + ThongTinNhanVien.NhanVienThongTinLuong.HSPCVuotKhung), 3);
                    }

                   

                    //Làm quyết định bổ nhiệm trước đó hết hiệu lực
                    filter = CriteriaOperator.Parse("ThongTinNhanVien=? And Oid!=?", ThongTinNhanVien, this.Oid);
                    SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                    XPCollection<QuyetDinhBoNhiemChucVuDoan> quyetDinhCuList = new XPCollection<QuyetDinhBoNhiemChucVuDoan>(Session, filter, sort);
                    quyetDinhCuList.TopReturnedObjects = 1;
                    if (quyetDinhCuList.Count == 1)
                        quyetDinhCuList[0].QuyetDinhMoi = false;
                   
                 }
              }
                //luu tru giay to ho so can bo huong dan
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.SoGiayTo = SoQuyetDinh;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.TrichYeu = NoiDung;
            }
        
        protected override void OnDeleting()
        {
            if (QuyetDinhMoi)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                DoanVien doanvien = Session.FindObject<DoanVien>(filter);
                using (XPCollection<QuyetDinhBoNhiemChucVuDoan> quyetdinh = new XPCollection<QuyetDinhBoNhiemChucVuDoan>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    if (quyetdinh.Count > 0)
                    {
                        if (quyetdinh[0] == This && QuyetDinhMoi)
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuDoan = HSPCChucVuDoanCu;
                            doanvien.ChucVuDoan = ChucVuDoanCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = HSPCChucVuCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu = HSPCChucVuBaoLuuCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien = HSPCThamNien;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCUuDai = HSPCUuDai;
                        }
                    }
                }

                //Làm quyết định bổ nhiệm trước đó có hiệu lực lại
                filter = CriteriaOperator.Parse("ThongTinNhanVien=? And Oid !=?",ThongTinNhanVien,this.Oid);
                sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                XPCollection<QuyetDinhBoNhiemChucVuDoan> quyetDinhCuList = new XPCollection<QuyetDinhBoNhiemChucVuDoan>(Session, filter, sort);
                quyetDinhCuList.TopReturnedObjects = 1;
                if (quyetDinhCuList.Count == 1)
                    quyetDinhCuList[0].QuyetDinhMoi = true;

               
            }
            //xoa giay to
            if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);

            base.OnDeleting();
        }

        
    }
}
