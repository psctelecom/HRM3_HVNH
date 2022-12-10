using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [DefaultProperty("NhanVien")]
    [ModelDefault("Caption", "Điều chỉnh 05A/BK-TNCN")]
    public class DieuChinhThueTNCNNhanVien : BaseObject
    {
        // Fields...
        private NhanVien _NhanVien;
        private BoPhan _BoPhan;
        private QuanLyDieuChinhThueTNCN _QuanLyDieuChinhThueTNCN;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý điều chỉnh thuế TNCN")]
        [Association("QuanLyDieuChinhThueTNCN-ListDieuChinhThueTNCNNhanVien")]
        public QuanLyDieuChinhThueTNCN QuanLyDieuChinhThueTNCN
        {
            get
            {
                return _QuanLyDieuChinhThueTNCN;
            }
            set
            {
                SetPropertyValue("QuanLyDieuChinhThueTNCN", ref _QuanLyDieuChinhThueTNCN, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách chi tiết điều chỉnh")]
        [Association("DieuChinhThueTNCNNhanVien-ListChiTietDieuChinhThueTNCNNhanVien")]
        public XPCollection<ChiTietDieuChinhThueTNCNNhanVien> ListChiTietDieuChinhThueTNCNNhanVien
        {
            get
            {
                return GetCollection<ChiTietDieuChinhThueTNCNNhanVien>("ListChiTietDieuChinhThueTNCNNhanVien");
            }
        }


        public DieuChinhThueTNCNNhanVien(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? or TinhTrang.TenTinhTrang not like ?", "%nghỉ việc%", "%nghỉ hưu%"));

            NVList.Criteria = go;
        }
    }

}
