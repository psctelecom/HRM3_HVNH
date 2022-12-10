using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.ChamCong;

namespace PSC_HRM.Module.ChamCong
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách theo dõi ngày nghỉ")]
    public class DanhSachTheoDoiNgayNghi : BaseObject
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Từ ngày")]
        [ImmediatePostData]
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

        [ModelDefault("Caption", "Đến ngày")]
        [ImmediatePostData]
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
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<ChiTietDanhSachTheoDoiNgayNghi> DanhSachNhanVien { get; set; }

        public DanhSachTheoDoiNgayNghi(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DanhSachNhanVien = new XPCollection<ChiTietDanhSachTheoDoiNgayNghi>(Session, false);
            DateTime current = HamDungChung.GetServerTime();
            TuNgay = new DateTime(current.Year, current.Month, 1);
            DenNgay = TuNgay.AddMonths(1).AddDays(-1);
        }

        public void LoadData()
        {
            if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue &&
                TuNgay < DenNgay)
            {
                GroupOperator go = new GroupOperator() { OperatorType = GroupOperatorType.And };
                InOperator filter1 = new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(Session));
                CriteriaOperator filter2 = CriteriaOperator.Parse("ThongTinNhanVien.TinhTrang.KhongConCongTacTaiTruong = False");
                go.Operands.Add(filter1);
                go.Operands.Add(filter2);

                XPCollection<BangChamCongNgayNghi> nvList = new XPCollection<BangChamCongNgayNghi>(Session, go);

                DanhSachNhanVien.Reload();
                ChiTietDanhSachTheoDoiNgayNghi ngayNghi;
                foreach (BangChamCongNgayNghi item in nvList)
                {
                    XPCollection<CC_ChamCongNgayNghi> lstChamCong = new XPCollection<CC_ChamCongNgayNghi>(Session, CriteriaOperator.Parse("TuNgay <= ? and (DenNgay >=? or (DenNgay >=? and DenNgay <=?)) and BangChamCongNgayNghi = ?",
                    HamDungChung.SetTime(DenNgay, 1), HamDungChung.SetTime(DenNgay, 1), HamDungChung.SetTime(TuNgay, 0), HamDungChung.SetTime(DenNgay, 1), item.Oid));

                    foreach (CC_ChamCongNgayNghi itemChamCong in lstChamCong)
                    {
                        ngayNghi = new ChiTietDanhSachTheoDoiNgayNghi(Session);
                        ngayNghi.MaQuanLy = item.ThongTinNhanVien.MaQuanLy;
                        ngayNghi.BoPhan = item.BoPhan;
                        ngayNghi.ThongTinNhanVien = item.ThongTinNhanVien;
                        ngayNghi.HinhThucNghi = itemChamCong.IDHinhThucNghi;
                        ngayNghi.TuNgay = itemChamCong.TuNgay;
                        ngayNghi.DenNgay = itemChamCong.DenNgay;
                        ngayNghi.SoNgayNghi = itemChamCong.SoNgay;
                        ngayNghi.DienGiai = itemChamCong.DienGiai;
                        ngayNghi.TinhTrang = item.ThongTinNhanVien.TinhTrang;
                        DanhSachNhanVien.Add(ngayNghi);
                    }
                }
            }
        }
    }

}
