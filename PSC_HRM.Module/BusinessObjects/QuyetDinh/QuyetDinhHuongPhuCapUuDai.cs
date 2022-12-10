using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định hưởng phụ cấp ưu đãi")]

    //[Appearance("Hide_BUH", TargetItems = "", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]
    //[Appearance("Hide_IUH", TargetItems = "ToTrinhDaoTao;ThoiGianDaoTao;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='IUH'")]
    //[Appearance("Hide_UTE", TargetItems = "ToTrinhDaoTao;ThoiGianDaoTao;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UTE'")]
    //[Appearance("Hide_LUH", TargetItems = "ToTrinhDaoTao;ThoiGianDaoTao;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'LUH'")]
    //[Appearance("Hide_DLU", TargetItems = "ToTrinhDaoTao;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'DLU'")]
    //[Appearance("Hide_HBU", TargetItems = "ToTrinhDaoTao;ThoiGianDaoTao;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HBU'")]

    //DLU
    public class QuyetDinhHuongPhuCapUuDai : QuyetDinh
    {
        //private string _LuuTru;
        private bool _QuyetDinhMoi;
        private int _Nam;

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
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

        [Aggregated]
        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhHuongPhuCapUuDai-ListChiTietHuongPhuCapUuDai")]
        public XPCollection<ChiTietHuongPhuCapUuDai> ListChiTietHuongPhuCapUuDai
        {
            get
            {
                return GetCollection<ChiTietHuongPhuCapUuDai>("ListChiTietHuongPhuCapUuDai");
            }
        }

        public QuyetDinhHuongPhuCapUuDai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NoiDung = "thực hiện chế độ phụ cấp ưu đãi Nhà giáo";//DLU
            QuyetDinhMoi = true;
            Nam = HamDungChung.GetServerTime().Year;
            //Lấy danh sách giảng viên hưởng phụ cấp ưu đãi
            if(TruongConfig.MaTruong.Equals("DLU"))
                UpdateListChiTietHuongPhuCapUuDai();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietHuongPhuCapUuDai item in ListChiTietHuongPhuCapUuDai)
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

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietHuongPhuCapUuDai.Count == 1)
                {
                    BoPhanText = ListChiTietHuongPhuCapUuDai[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietHuongPhuCapUuDai[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
        }

        private void UpdateListChiTietHuongPhuCapUuDai()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = CriteriaOperator.Parse("NhanVienThongTinLuong.PhuCapUuDai > 0 and NhanVienThongTinLuong.NgachLuong.TenNgachLuong like ?", "%Giảng viên%");
            if (NVList != null)
            {
                foreach (ThongTinNhanVien caNhan in NVList)
                {
                    if (!ExistsNhanVien(caNhan))
                    {
                        ChiTietHuongPhuCapUuDai chitiet = new ChiTietHuongPhuCapUuDai(Session);
                        chitiet.QuyetDinhHuongPhuCapUuDai = this;
                        chitiet.BoPhan = caNhan.BoPhan;
                        chitiet.ThongTinNhanVien = caNhan;
                        chitiet.PhuCapUuDai = caNhan.NhanVienThongTinLuong.PhuCapUuDai;
                        chitiet.PhuCapUuDaiMoi = caNhan.NhanVienThongTinLuong.PhuCapUuDai;
                        chitiet.GhiChu = "";
                    }
                }
            }
        }

        private bool ExistsNhanVien(ThongTinNhanVien nhanVien)
        {
            bool exists = (from d in ListChiTietHuongPhuCapUuDai
                           where d.ThongTinNhanVien == nhanVien
                           select true).FirstOrDefault();

            return exists;
        }
    }
}
