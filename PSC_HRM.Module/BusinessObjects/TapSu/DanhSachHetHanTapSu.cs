using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.TapSu
{
    [NonPersistent]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Danh sách hết hạn tập sự")]
    public class DanhSachHetHanTapSu : BaseObject
    {
        // Fields...
        private bool _ChonTatCa;
        private bool _QuyetDinhMoi;
        private DateTime _DenNgay;
        private DateTime _TuNgay;

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chọn tất cả")]
        public bool ChonTatCa
        {
            get
            {
                return _ChonTatCa;
            }
            set
            {
                SetPropertyValue("ChonTatCa", ref _ChonTatCa, value);
                if (!IsLoading)
                {
                    foreach (HetHanTapSu item in ListHetHanTapSu)
                        if (item.Chon != value)
                            item.Chon = value;
                }
            }
        }

        [ModelDefault("Caption", "Quyết định mới")]
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

        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<HetHanTapSu> ListHetHanTapSu { get; set; }

        public DanhSachHetHanTapSu(Session session)
            : base(session) 
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ListHetHanTapSu = new XPCollection<HetHanTapSu>(Session, false);
            DateTime current = HamDungChung.GetServerTime();
            TuNgay = current.SetTime(SetTimeEnum.StartMonth);
            DenNgay = current.SetTime(SetTimeEnum.EndMonth);

            //UTE không còn quyết định cũ
            if (TruongConfig.MaTruong == "UTE")
            {
                QuyetDinhMoi = true;
            }
        }

        public void XuLy()
        {
            if (TuNgay != DateTime.MinValue &&
                DenNgay != DateTime.MinValue &&
                TuNgay < DenNgay)
            {
                //quyet dinh tap su
                ListHetHanTapSu.Reload();
                CriteriaOperator filter = CriteriaOperator.Parse("DenNgay>=? and DenNgay<=?",
                    TuNgay.SetTime(SetTimeEnum.StartDay), DenNgay.SetTime(SetTimeEnum.EndDay));
                XPCollection<ChiTietQuyetDinhHuongDanTapSu> dsTapSu = new XPCollection<ChiTietQuyetDinhHuongDanTapSu>(Session, filter);

                HetHanTapSu hetHan;
                QuyetDinhTamHoanTapSu tamHoan;
                foreach (ChiTietQuyetDinhHuongDanTapSu item in dsTapSu)
                {
                    filter = CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=?",
                        item.Oid);
                    tamHoan = Session.FindObject<QuyetDinhTamHoanTapSu>(filter);
                    //chỉ lấy những cán bộ không bị tạm hoãn
                    if (tamHoan == null && item.CanBoHuongDan is ThongTinNhanVien)
                    {
                        hetHan = new HetHanTapSu(Session);
                        hetHan.QuyetDinhHuongDanTapSu = item.QuyetDinhHuongDanTapSu;
                        hetHan.ThongTinNhanVien = item.ThongTinNhanVien;
                        hetHan.BoPhan = item.BoPhan;
                        hetHan.CanBoHuongDan = (ThongTinNhanVien)item.CanBoHuongDan;
                        hetHan.BoPhanCanBoHuongDan = item.BoPhanCanBoHuongDan;
                        hetHan.NgayHetHanTapSu = item.DenNgay;
                        ChiTietQuyetDinhTuyenDung tuyenDung = Session.FindObject<ChiTietQuyetDinhTuyenDung>(CriteriaOperator.Parse("ThongTinNhanVien = ?", item.ThongTinNhanVien.Oid));
                        if (tuyenDung != null)
                        {
                            hetHan.NgachLuong = tuyenDung.NgachLuong;
                            hetHan.BacLuong = tuyenDung.BacLuong;
                            hetHan.HeSoLuong = tuyenDung.HeSoLuong;
                            hetHan.MocNangLuongCu = tuyenDung.NgayHuongLuong;
                            hetHan.MocNangLuongmoi = tuyenDung.NgayBoNhiemNgach;
                        }
                        ListHetHanTapSu.Add(hetHan);
                    }
                }


                //quyet dinh tam hoan tap su
                filter = CriteriaOperator.Parse("TapSuDenNgay>=? and TapSuDenNgay<=?",
                    TuNgay.SetTime(SetTimeEnum.StartDay), DenNgay.SetTime(SetTimeEnum.EndDay));
                XPCollection<QuyetDinhTamHoanTapSu> dsTamHoan = new XPCollection<QuyetDinhTamHoanTapSu>(Session, filter);

                ChiTietQuyetDinhHuongDanTapSu tapSu;
                foreach (QuyetDinhTamHoanTapSu item in dsTamHoan)
                {
                    tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?",
                            item.QuyetDinhHuongDanTapSu.Oid, item.ThongTinNhanVien.Oid));
                        //chỉ lấy những cán bộ bị tạm hoãn
                        if (tapSu != null && tapSu.CanBoHuongDan is ThongTinNhanVien)
                        {
                            hetHan = new HetHanTapSu(Session);
                            hetHan.QuyetDinhHuongDanTapSu = tapSu.QuyetDinhHuongDanTapSu;
                            hetHan.ThongTinNhanVien = item.ThongTinNhanVien;
                            hetHan.BoPhan = item.BoPhan;
                            hetHan.CanBoHuongDan = (ThongTinNhanVien)tapSu.CanBoHuongDan;
                            hetHan.BoPhanCanBoHuongDan = tapSu.BoPhanCanBoHuongDan;
                            hetHan.NgayHetHanTapSu = item.TapSuDenNgay;
                            ChiTietQuyetDinhTuyenDung tuyenDung = Session.FindObject<ChiTietQuyetDinhTuyenDung>(CriteriaOperator.Parse("ThongTinNhanVien = ?", item.ThongTinNhanVien.Oid));
                            if (tuyenDung != null)
                            {
                                hetHan.NgachLuong = tuyenDung.NgachLuong;
                                hetHan.BacLuong = tuyenDung.BacLuong;
                                hetHan.HeSoLuong = tuyenDung.HeSoLuong;
                                hetHan.MocNangLuongCu = tuyenDung.NgayHuongLuong;
                                hetHan.MocNangLuongmoi = tuyenDung.NgayBoNhiemNgach;
                            }
                            ListHetHanTapSu.Add(hetHan);
                        }
                }

            }
        }
    }

}
