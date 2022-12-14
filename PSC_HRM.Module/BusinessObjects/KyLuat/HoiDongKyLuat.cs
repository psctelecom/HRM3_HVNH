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

namespace PSC_HRM.Module.KyLuat
{
    [ImageName("BO_HoiDongKhenThuong")]
    [ModelDefault("Caption", "Hội đồng kỷ luật")]
    public class HoiDongKyLuat : BaseObject
    {
        // Fields...
        private ChucDanhHoiDongKyLuat _ChucDanhHoiDongKyLuat;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuyetDinhThanhLapHoiDongKyLuat _QuyetDinhThanhLapHoiDongKyLuat;
        private ChucVu _ChucVu;
        private string _VaiTroDamNhiem;
        private GiayToHoSo _GiayToHoSo;
        
        [Browsable(false)]
        [Association("QuyetDinhThanhLapHoiDongKyLuat-ListHoiDongKyLuat")]
        public QuyetDinhThanhLapHoiDongKyLuat QuyetDinhThanhLapHoiDongKyLuat
        {
            get
            {
                return _QuyetDinhThanhLapHoiDongKyLuat;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapHoiDongKyLuat", ref _QuyetDinhThanhLapHoiDongKyLuat, value);
                //if (!IsLoading && value != null)
                //{
                //    GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                //    GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                //    GiayToHoSo.LuuTru = value.LuuTru;
                //    GiayToHoSo.TrichYeu = value.NoiDung;
                //}
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

        [ModelDefault("Caption", "Chức danh hội động kỷ luật")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucDanhHoiDongKyLuat ChucDanhHoiDongKyLuat
        {
            get
            {
                return _ChucDanhHoiDongKyLuat;
            }
            set
            {
                SetPropertyValue("ChucDanhHoiDongKyLuat", ref _ChucDanhHoiDongKyLuat, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Vai trò đảm nhiệm")]
        //[Appearance("VaiTroDamNhiemKL_UTE", TargetItems = "VaiTroDamNhiem", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'UTE'")]
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

        public HoiDongKyLuat(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNhanVienList();
            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định thành lập hội đồng kỷ luật"));
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
