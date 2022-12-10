using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.NangThamNien;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuaTrinh;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nâng phụ cấp thâm niên")]

    //[Appearance("Hide_BUH", TargetItems = "NgayPhatSinhBienDong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]

    public class QuyetDinhNangPhuCapThamNienNhaGiao : QuyetDinh
    {
        // Fields...
        private DeNghiNangPhuCapThamNien _DeNghiNangPhuCapThamNien;
        private DateTime _NgayPhatSinhBienDong;
        private bool _QuyetDinhMoi;
        private bool _Import;
        //private string _LuuTru;

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

        [ModelDefault("Caption", "Đề nghị nâng PC thâm niên")]
        public DeNghiNangPhuCapThamNien DeNghiNangPhuCapThamNien
        {
            get
            {
                return _DeNghiNangPhuCapThamNien;
            }
            set
            {
                SetPropertyValue("DeNghiNangPhuCapThamNien", ref _DeNghiNangPhuCapThamNien, value);
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

        [ModelDefault("Caption", "Import")]
        [NonPersistent]
        [Browsable(false)]
        public bool Imporrt
        {
            get
            {
                return _Import;
            }
            set
            {
                SetPropertyValue("Imporrt", ref _Import, value);
            }
        }

        //[ModelDefault("Caption", "Lưu trữ")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        //public string LuuTru
        //{
        //    get
        //    {
        //        return _LuuTru;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LuuTru", ref _LuuTru, value);
        //    }
        //}

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhNangPhuCapThamNienNhaGiao-ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao")]
        public XPCollection<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao> ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>("ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao");
            }
        }

        public QuyetDinhNangPhuCapThamNienNhaGiao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhNangPhuCapThamNienNhaGiao;
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //OnSaving();
        }
        public void CreateListChiTietQuyetDinhNangPhuCapThamNienNhaGiao(HoSo_NhanVienItem item)
        {
            ChiTietQuyetDinhNangPhuCapThamNienNhaGiao chiTiet = new ChiTietQuyetDinhNangPhuCapThamNienNhaGiao(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Add(chiTiet);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietQuyetDinhNangPhuCapThamNienNhaGiao item in ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao)
                    {
                        if(Imporrt)
                        {
                            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                    item.ThongTinNhanVien.Oid);
                            HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                            if (QuyetDinhMoi)
                            {
                                //phát sinh tăng mức đóng bảo hiểm
                                if (hoSoBaoHiem != null &&
                                    NgayPhatSinhBienDong != DateTime.MinValue)
                                    BienDongHelper.CreateBienDongThayDoiLuong(Session,
                                        this,
                                        item.BoPhan,
                                        item.ThongTinNhanVien,
                                        NgayPhatSinhBienDong,
                                        item.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong,
                                        item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu,
                                        item.ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung,
                                        item.ThamNienMoi,
                                        item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac,
                                        item.ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong
                                        );

                                //cập nhật thâm niên
                                if (item.NgayHuongThamNienMoi <= HamDungChung.GetServerTime())
                                {
                                    item.ThongTinNhanVien.NhanVienThongTinLuong.ThamNien = item.ThamNienMoi;
                                    item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongThamNien = item.NgayHuongThamNienMoi;
                                }
                            }


                            //update dien bien luong
                            filter = CriteriaOperator.Parse("ThongTinNhanVien=? and ThamNienCu=?",
                                item.ThongTinNhanVien.Oid, item.ThamNienCu);
                            ChiTietQuyetDinhNangPhuCapThamNienNhaGiao chiTiet = Session.FindObject<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>(filter);
                            if (chiTiet != null)
                            {
                                filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?",
                                    chiTiet.QuyetDinhNangPhuCapThamNienNhaGiao.Oid, item.ThongTinNhanVien.Oid);
                                DienBienLuong updateDienBienLuong = Session.FindObject<DienBienLuong>(filter);
                                if (updateDienBienLuong != null)
                                {
                                    updateDienBienLuong.DenNgay = item.NgayHuongThamNienMoi.AddDays(-1);
                                }
                            }

                            if (item.NgayHuongThamNienMoi != DateTime.MinValue)
                            {
                                //tạo mới diễn biến lương
                                QuaTrinhHelper.CreateDienBienLuong(Session, this, item.ThongTinNhanVien, item.NgayHuongThamNienMoi, item.Oid);

                                //Bảo hiểm xã hội
                                if (hoSoBaoHiem != null)
                                    QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(Session, this, hoSoBaoHiem, item.NgayHuongThamNienMoi);
                            }
                        }
                        if (item.GiayToHoSo != null)
                        {
                            item.GiayToHoSo.QuyetDinh = this;
                            item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
                            item.GiayToHoSo.TrichYeu = NoiDung;
                            item.GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                            item.GiayToHoSo.NgayLap = NgayQuyetDinh;
                            item.GiayToHoSo.LuuTru = GiayToHoSo.LuuTru;
                            item.GiayToHoSo.DuongDanFile = GiayToHoSo.DuongDanFile;
                        }
                    }
                }

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
        }
    }

}
