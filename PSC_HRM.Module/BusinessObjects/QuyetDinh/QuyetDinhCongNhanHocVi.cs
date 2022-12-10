using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định công nhận học vị")]
    //[Appearance("Hide_BUH", TargetItems = "", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]
    public class QuyetDinhCongNhanHocVi : QuyetDinh
    {
        private QuyetDinhDaoTao _QuyetDinhDaoTao;
        //private string _LuuTru;
        private bool _QuyetDinhMoi;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định đào tạo")]
        public QuyetDinhDaoTao QuyetDinhDaoTao
        {
            get
            {
                return _QuyetDinhDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhDaoTao", ref _QuyetDinhDaoTao, value);
                if (!IsLoading && value != null)
                {
                    ChiTietCongNhanHocVi chiTiet;
                    foreach (ChiTietDaoTao item in value.ListChiTietDaoTao)
                    {
                        chiTiet = Session.FindObject<ChiTietCongNhanHocVi>(CriteriaOperator.Parse("QuyetDinhCongNhanHocVi=? and ThongTinNhanVien=?", Oid, item.ThongTinNhanVien));
                        if (chiTiet == null)
                        {
                            chiTiet = new ChiTietCongNhanHocVi(Session);
                            chiTiet.QuyetDinhCongNhanHocVi = this;
                            chiTiet.BoPhan = item.BoPhan;
                            chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;

                            //Cập nhật văn bằng học vị mới
                            chiTiet.TrinhDoChuyenMonMoi = QuyetDinhDaoTao.TrinhDoChuyenMon;
                            chiTiet.ChuyenMonDaoTaoMoi = item.ChuyenMonDaoTao;
                            chiTiet.TruongDaoTaoMoi= QuyetDinhDaoTao.TruongDaoTao;
                            chiTiet.HinhThucDaoTaoMoi= QuyetDinhDaoTao.HinhThucDaoTao;
                            
                            //Cập nhật quyết định chuyển trường
                            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?",
                                        value, item.ThongTinNhanVien);
                            QuyetDinhChuyenTruongDaoTao qdChuyenTruong = Session.FindObject<QuyetDinhChuyenTruongDaoTao>(filter);
                            if (qdChuyenTruong != null)
                                chiTiet.TruongDaoTaoMoi = qdChuyenTruong.TruongDaoTaoMoi;

                            ListChiTietCongNhanHocVi.Add(chiTiet);
                        }
                    }
                }
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
        [Association("QuyetDinhCongNhanHocVi-ListChiTietCongNhanHocVi")]
        public XPCollection<ChiTietCongNhanHocVi> ListChiTietCongNhanHocVi
        {
            get
            {
                return GetCollection<ChiTietCongNhanHocVi>("ListChiTietCongNhanHocVi");
            }
        }

        public QuyetDinhCongNhanHocVi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NoiDung = "công nhận học vị";
            QuyetDinhMoi = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateGiayToList();
        }

        private void UpdateGiayToList()
        {
            if (ListChiTietCongNhanHocVi.Count == 1)
                GiayToList = ListChiTietCongNhanHocVi[0].ThongTinNhanVien.ListGiayToHoSo;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietCongNhanHocVi item in ListChiTietCongNhanHocVi)
                    {
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
                if (ListChiTietCongNhanHocVi.Count == 1)
                {
                    BoPhanText = ListChiTietCongNhanHocVi[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietCongNhanHocVi[0].ThongTinNhanVien.HoTen;
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
