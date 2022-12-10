using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DoanDang;
using System.Collections.Generic;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn đơn vị")]
    public class ChiTietPhuCapLanhDao : BaseObject
    {
        // Fields...
        private decimal _HSPCKiemNhiem;
        private decimal _HSPCQuanLy;
        private ChucVuDoan _ChucVuDoan;
        private ChucVuDoanThe _ChucVuDoanThe;
        private ChucVuDang _ChucVuDang;
        private ChucVu _ChucVuKiemNhiem;
        private ChucVu _ChucVu;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
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
                    ThongTinNhanVien = null;
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    ChucVu = value.ChucVu;
                    if (ChucVu != null)
                        HSPCQuanLy = ChucVu.HSPCQuanLy;

                    ChucVuKiemNhiem = value.ChucVuKiemNhiem;
                    DangVien dangVien = Session.FindObject<DangVien>(CriteriaOperator.Parse("ThongTinNhanVien=?", value.Oid));
                    if (dangVien != null)
                        ChucVuDang = dangVien.ChucVuDang;
                    DoanVien doanVien = Session.FindObject<DoanVien>(CriteriaOperator.Parse("ThongTinNhanVien=?", value.Oid));
                    if (doanVien != null)
                        ChucVuDoan = doanVien.ChucVuDoan;
                    DoanThe doanThe = Session.FindObject<DoanThe>(CriteriaOperator.Parse("ThongTinNhanVien=?", value.Oid));
                    if (doanThe != null)
                        ChucVuDoanThe = doanThe.ChucVuDoanThe;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
        public ChucVu ChucVuKiemNhiem
        {
            get
            {
                return _ChucVuKiemNhiem;
            }
            set
            {
                SetPropertyValue("ChucVuKiemNhiem", ref _ChucVuKiemNhiem, value);
                if (!IsLoading && value != null)
                    XuLy();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ Đảng")]
        public ChucVuDang ChucVuDang
        {
            get
            {
                return _ChucVuDang;
            }
            set
            {
                SetPropertyValue("ChucVuDang", ref _ChucVuDang, value);
                if (!IsLoading && value != null)
                    XuLy();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ Công đoàn")]
        public ChucVuDoanThe ChucVuDoanThe
        {
            get
            {
                return _ChucVuDoanThe;
            }
            set
            {
                SetPropertyValue("ChucVuDoanThe", ref _ChucVuDoanThe, value);
                if (!IsLoading && value != null)
                    XuLy();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ Đoàn")]
        public ChucVuDoan ChucVuDoan
        {
            get
            {
                return _ChucVuDoan;
            }
            set
            {
                SetPropertyValue("ChucVuDoan", ref _ChucVuDoan, value);
                if (!IsLoading && value != null)
                    XuLy();
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC lãnh đạo")]
        public decimal HSPCQuanLy
        {
            get
            {
                return _HSPCQuanLy;
            }
            set
            {
                SetPropertyValue("HSPCQuanLy", ref _HSPCQuanLy, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC kiêm nhiệm")]
        public decimal HSPCKiemNhiem
        {
            get
            {
                return _HSPCKiemNhiem;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiem", ref _HSPCKiemNhiem, value);
            }
        }

        public ChiTietPhuCapLanhDao(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        private void XuLy()
        {
            List<decimal> hspcChucVu = new List<decimal>();

            if (ChucVuKiemNhiem != null)
                hspcChucVu.Add(ChucVuKiemNhiem.HSPCQuanLy);
            if (ChucVuDang != null)
                hspcChucVu.Add(ChucVuDang.HSPCChucVuDang);
            if (ChucVuDoanThe != null)
                hspcChucVu.Add(ChucVuDoanThe.HSPCChucVuDoanThe);
            if (ChucVuDoan != null)
                hspcChucVu.Add(ChucVuDoan.HSPCChucVuDoan);

            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinhBoNhiemKiemNhiem=? and HetHieuLuc=? and ThongTinNhanVien=?", "True", "False", ThongTinNhanVien.Oid);
            using (XPCollection<QuyetDinhBoNhiem> qdList = new XPCollection<QuyetDinhBoNhiem>(Session, filter))
            {
                if (qdList.Count > 0)
                {
                    foreach (QuyetDinhBoNhiem item in qdList)
                    {
                        if (ChucVuKiemNhiem != null
                            && ChucVuKiemNhiem.Oid == item.Oid)
                            continue;
                        hspcChucVu.Add(item.ChucVuMoi.HSPCQuanLy);
                    }
                }
            }

            foreach (decimal de in hspcChucVu)
            {
                HSPCKiemNhiem += de * 0.1m;
            }
        }
    }

}
