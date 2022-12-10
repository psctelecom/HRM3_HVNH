using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.KhenThuong
{
    [ImageName("BO_HoiDongKhenThuong")]
    [ModelDefault("Caption", "Hội đồng khen thưởng")]
    public class HoiDongKhenThuong : BaseObject
    {
        // Fields...
        private ChucDanhHoiDongKhenThuong _ChucDanhHoiDongKhenThuong;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuyetDinhThanhLapHoiDongKhenThuong _QuyetDinhThanhLapHoiDongKhenThuong;
        private QuanLyKhenThuong _QuanLyKhenThuong;
        private ChucVu _ChucVu;
        private string _VaiTroDamNhiem;
        private GiayToHoSo _GiayToHoSo;

        [Browsable(false)]
        [Association("QuanLyKhenThuong-ListHoiDongKhenThuong")]
        public QuanLyKhenThuong QuanLyKhenThuong
        {
            get
            {
                return _QuanLyKhenThuong;
            }
            set
            {
                SetPropertyValue("QuanLyKhenThuong", ref _QuanLyKhenThuong, value);

            }
        }

        [Browsable(false)]
        [Association("QuyetDinhThanhLapHoiDongKhenThuong-ListHoiDongKhenThuong")]
        public QuyetDinhThanhLapHoiDongKhenThuong QuyetDinhThanhLapHoiDongKhenThuong
        {
            get
            {
                return _QuyetDinhThanhLapHoiDongKhenThuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapHoiDongKhenThuong", ref _QuyetDinhThanhLapHoiDongKhenThuong, value);
                if (!IsLoading && value != null)
                {
                    GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                    GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                    GiayToHoSo.LuuTru = value.GiayToHoSo.LuuTru;
                    GiayToHoSo.DuongDanFile = value.GiayToHoSo.DuongDanFile;
                    GiayToHoSo.TrichYeu = value.NoiDung;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    GiayToHoSo.HoSo = value;
                    ChucVu = value.ChucVu;
                }
            }
        }

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

        [ModelDefault("Caption", "Chức danh hội đồng khen thưởng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucDanhHoiDongKhenThuong ChucDanhHoiDongKhenThuong
        {
            get
            {
                return _ChucDanhHoiDongKhenThuong;
            }
            set
            {
                SetPropertyValue("ChucDanhHoiDongKhenThuong", ref _ChucDanhHoiDongKhenThuong, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Vai trò đảm nhiệm")]
        //[Appearance("VaiTroDamNhiemKT_UTE", TargetItems = "VaiTroDamNhiem", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'IUH'")]
        public string VaiTroDamNhiem
        {
            get
            {
                return _VaiTroDamNhiem;
            }
            set
            {
                SetPropertyValue("VaiTroDamNhiem", ref _VaiTroDamNhiem, value);
            }
        }

        [Aggregated]
        [Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
            }
        }

        public HoiDongKhenThuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateNhanVienList();
            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? and TinhTrang.TenTinhTrang not like ?", "%nghỉ việc%", "%nghỉ hưu%"));

            NVList.Criteria = go;
        }

        protected override void OnDeleting()
        {
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }
            base.OnDeleting();
        }
    }

}
