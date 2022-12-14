using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.GiayTo;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định cho phép nghỉ học tạm thời")]
    //DLU
    public class QuyetDinhChoPhepNghiHocTamThoi : QuyetDinhCaNhan
    {
        private DateTime _NghiTuNgay;
        private DateTime _NghiDenNgay;
        private DateTime _DaoTaoDenNgayCu;
        private DateTime _DaoTaoDenNgayMoi;

        private ChuyenMonDaoTao _ChuyenMonDaoTao;
        private TinhTrang _TinhTrangCu;
        private TinhTrang _TinhTrangMoi;
        
        private QuyetDinhDaoTao _QuyetDinhDaoTao;
        private bool _QuyetDinhMoi;
        private string _GhiChu;
        
        [ImmediatePostData]
        [DataSourceProperty("QuyetDinhList")]
        [ModelDefault("Caption", "Quyết định đào tạo")]
        //[RuleRequiredField(DefaultContexts.Save)]
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
                    ChiTietDaoTao chiTietDaoTao = Session.FindObject<ChiTietDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao.Oid = ? and ThongTinNhanVien.Oid = ?", value.Oid, ThongTinNhanVien.Oid));
                    if (chiTietDaoTao != null)
                    { ChuyenMonDaoTao = chiTietDaoTao.ChuyenMonDaoTao; }
                    DaoTaoDenNgayCu = value.DenNgay;
                    DaoTaoDenNgayMoi = DaoTaoDenNgayCu != DateTime.MinValue ? DaoTaoDenNgayCu.AddDays((NghiDenNgay - NghiTuNgay).TotalDays) : DateTime.MinValue; 
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nghỉ từ ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NghiTuNgay
        {
            get
            {
                return _NghiTuNgay;
            }
            set
            {
                SetPropertyValue("NghiTuNgay", ref _NghiTuNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    NghiDenNgay = value;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nghỉ đến ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NghiDenNgay
        {
            get
            {
                return _NghiDenNgay;
            }
            set
            {
                SetPropertyValue("NghiDenNgay", ref _NghiDenNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    DaoTaoDenNgayMoi = DaoTaoDenNgayCu != DateTime.MinValue ? DaoTaoDenNgayCu.AddDays((NghiDenNgay - NghiTuNgay).TotalDays) : DateTime.MinValue;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đào tạo đến ngày (cũ)")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime DaoTaoDenNgayCu
        {
            get
            {
                return _DaoTaoDenNgayCu;
            }
            set
            {
                SetPropertyValue("DaoTaoDenNgayCu", ref _DaoTaoDenNgayCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đào tạo đến ngày (mới)")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime DaoTaoDenNgayMoi
        {
            get
            {
                return _DaoTaoDenNgayMoi;
            }
            set
            {
                SetPropertyValue("DaoTaoDenNgayMoi", ref _DaoTaoDenNgayMoi, value);
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

        [Browsable(false)]
        public TinhTrang TinhTrangCu
        {
            get
            {
                return _TinhTrangCu;
            }
            set
            {
                SetPropertyValue("TinhTrangCu", ref _TinhTrangCu, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng mới")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "MaTruong != 'BUH'")]
        public ChuyenMonDaoTao ChuyenMonDaoTao
        {
            get
            {
                return _ChuyenMonDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenMonDaoTao", ref _ChuyenMonDaoTao, value);
            }
        }
        
        public QuyetDinhChoPhepNghiHocTamThoi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
            { NoiDung = "nghỉ học tạm thời"; }
            NghiTuNgay = HamDungChung.GetServerTime();
        }
        
        [Browsable(false)]
        public XPCollection<QuyetDinhDaoTao> QuyetDinhList { get; set; }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhDaoTao>(Session);
            QuyetDinhList.Criteria = CriteriaOperator.Parse("ListChiTietDaoTao[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
        }

        protected override void AfterNhanVienChanged()
        {
            QuyetDinhMoi = true;
            QuyetDinhDaoTao = null;
            //cập nhật danh sách quyết định
            UpdateQuyetDinhList();

            //lấy quyết định đào tạo mới nhất
            CriteriaOperator filter = CriteriaOperator.Parse("ListChiTietDaoTao[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
            SortProperty sort = new SortProperty("NgayHieuLuc", DevExpress.Xpo.DB.SortingDirection.Descending);
            using (XPCollection<QuyetDinhDaoTao> qdList = new XPCollection<QuyetDinhDaoTao>(Session, filter, sort))
            {
                qdList.TopReturnedObjects = 1;
                if (qdList.Count == 1)
                    QuyetDinhDaoTao = qdList[0];
            }
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

            if (!IsDeleted)
            {
                //luu tru giay to ho so 
                GiayToHoSo.QuyetDinh = this;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.SoGiayTo = SoQuyetDinh;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.TrichYeu = NoiDung;

                XuLy_Save_DLU();
            }
            
        }

        protected override void OnDeleting()
        {
            if (!IsSaving && ThongTinNhanVien != null)
            {
                //xoa giay to
                if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);

                XuLy_Delete_DLU();
            }
            base.OnDeleting();
        }

        public void XuLy_Save_DLU()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@NgayHienTai", HamDungChung.GetServerTime());
            DataProvider.ExecuteNonQuery("spd_Job_CapNhatTuNgay_QuyetDinhChoPhepNghiHocTamThoi", CommandType.StoredProcedure, parameter);

            SqlParameter[] parameter1 = new SqlParameter[1];
            parameter1[0] = new SqlParameter("@NgayHienTai", HamDungChung.GetServerTime());
            DataProvider.ExecuteNonQuery("spd_Job_CapNhatDenNgay_QuyetDinhChoPhepNghiHocTamThoi", CommandType.StoredProcedure, parameter1);
        }

        public void XuLy_Delete_DLU()
        {
            if (HamDungChung.GetServerTime() >= NghiTuNgay && HamDungChung.GetServerTime() <= NghiDenNgay)
            {
                ThongTinNhanVien.TinhTrang = TinhTrangCu;
            }
        }
    }

}
