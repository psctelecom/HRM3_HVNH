using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]    
    [ModelDefault("Caption", "Quyết định hủy đi nước ngoài")]
    public class QuyetDinhHuyDiNuocNgoai : QuyetDinhCaNhan
    {        
        private QuyetDinhDiNuocNgoai _QuyetDinhDiNuocNgoai;
        private TinhTrang _TinhTrangCu;
        private TinhTrang _TinhTrangMoi;
        private bool _QuyetDinhMoi;
        private string _LyDo;
              
        [ImmediatePostData]
        [DataSourceProperty("QuyetDinhList")]
        [ModelDefault("Caption", "Quyết định đi nước ngoài")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuyetDinhDiNuocNgoai QuyetDinhDiNuocNgoai
        {
            get
            {
                return _QuyetDinhDiNuocNgoai;
            }
            set
            {
                SetPropertyValue("QuyetDinhDiNuocNgoai", ref _QuyetDinhDiNuocNgoai, value);
                //if (!IsLoading && value != null)
                //    TuNgay = value.DenNgay;
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
        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrangMoi
        {
            get
            {
                return _TinhTrangMoi;
            }
            set
            {
                SetPropertyValue("TinhTrangMoi", ref _TinhTrangMoi, value);
            }
        }
        [Browsable(false)]
        public TinhTrang TinhTrangCu { get; set; }

        public QuyetDinhHuyDiNuocNgoai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhHuyDiNuocNgoai;
            
        }

        protected override void AfterNhanVienChanged()
        {
            QuyetDinhDiNuocNgoai = null;
            //cập nhật danh sách quyết định
            UpdateQuyetDinhList();

            //lấy quyết định đi nước ngoài mới nhất
            CriteriaOperator filter = CriteriaOperator.Parse("ListChiTietQuyetDinhDiNuocNgoai[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
            SortProperty sort = new SortProperty("NgayHieuLuc", DevExpress.Xpo.DB.SortingDirection.Descending);
            using (XPCollection<QuyetDinhDiNuocNgoai> qdList = new XPCollection<QuyetDinhDiNuocNgoai>(Session, filter, sort))
            {
                qdList.TopReturnedObjects = 1;
                if (qdList.Count == 1)
                    QuyetDinhDiNuocNgoai = qdList[0];
            }

            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định hủy đi nước ngoài"));
        }
        
        [Browsable(false)]
        public XPCollection<QuyetDinhBoiDuong> QuyetDinhList { get; set; }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhBoiDuong>(Session);
            QuyetDinhList.Criteria = CriteriaOperator.Parse("ListChiTietBoiDuong[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
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
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && ThongTinNhanVien != null)
            {
                TinhTrangCu = ThongTinNhanVien.TinhTrang;
                if(QuyetDinhMoi)
                {
                    ThongTinNhanVien.TinhTrang = TinhTrangMoi;
                }
                //luu tru giay to ho so can bo huong dan
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.SoGiayTo = SoQuyetDinh;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.TrichYeu = NoiDung;                            
            }
        }

        protected override void OnDeleting()
        {
            if (ThongTinNhanVien != null)
            {
                ThongTinNhanVien.TinhTrang = TinhTrangCu;
                //xoa giay to
                if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);                
            }
            base.OnDeleting();
        }
    }
}
