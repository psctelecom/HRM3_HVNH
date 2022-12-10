using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nâng phụ cấp thâm niên hành chính")]
    public class QuyetDinhNangPhuCapThamNienHanhChinh : QuyetDinh
    {
        // Fields...
        private bool _QuyetDinhMoi;

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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhNangPhuCapThamNienHanhChinh-ListChiTietQuyetDinhNangPhuCapThamNienHanhChinh")]
        public XPCollection<ChiTietQuyetDinhNangPhuCapThamNienHanhChinh> ListChiTietQuyetDinhNangPhuCapThamNienHanhChinh
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhNangPhuCapThamNienHanhChinh>("ListChiTietQuyetDinhNangPhuCapThamNienHanhChinh");
            }
        }

        public QuyetDinhNangPhuCapThamNienHanhChinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (HamDungChung.CauHinhChung.CauHinhQuyetDinh != null)
            NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhNangPhuCapThamNienHanhChinh;
        }
        public void CreateListChiTietQuyetDinhNangPhuCapThamNienNhaGiao(HoSo_NhanVienItem item)
        {
            ChiTietQuyetDinhNangPhuCapThamNienHanhChinh chiTiet = new ChiTietQuyetDinhNangPhuCapThamNienHanhChinh(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietQuyetDinhNangPhuCapThamNienHanhChinh.Add(chiTiet);

        }
        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietQuyetDinhNangPhuCapThamNienHanhChinh.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhNangPhuCapThamNienHanhChinh[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhNangPhuCapThamNienHanhChinh[0].ThongTinNhanVien.HoTen;
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
