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
using System.Collections.Generic;

namespace PSC_HRM.Module.NghiHuu
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách đến tuổi nghỉ hưu")]
    public class DanhSachDenTuoiNghiHuu : BaseObject
    {
        private DateTime _TinhDenNgay;

        [ModelDefault("Caption", "Tính đến ngày")]
        [ImmediatePostData]
        public DateTime TinhDenNgay
        {
            get
            {
                return _TinhDenNgay;
            }
            set
            {
                SetPropertyValue("TinhDenNgay", ref _TinhDenNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<DenTuoiNghiHuu> DenTuoiNghiHuuList { get; set; }

        public DanhSachDenTuoiNghiHuu(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DenTuoiNghiHuuList = new XPCollection<DenTuoiNghiHuu>(Session, false);
            TinhDenNgay = HamDungChung.GetServerTime();
        }

        public void LoadData()
        {
            if (TinhDenNgay != DateTime.MinValue)
            {
                GroupOperator mainGO = new GroupOperator(GroupOperatorType.And);
                mainGO.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang like ?", "%đang làm việc%"));
                mainGO.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(Session)));
                GroupOperator go = new GroupOperator(GroupOperatorType.Or);
                TuoiNghiHuu nam = Session.FindObject<TuoiNghiHuu>(CriteriaOperator.Parse("GioiTinh=?", GioiTinhEnum.Nam));
                TuoiNghiHuu nu = Session.FindObject<TuoiNghiHuu>(CriteriaOperator.Parse("GioiTinh=?", GioiTinhEnum.Nu));

                DateTime denNgay;

                if (nam != null)
                {
                    denNgay = TinhDenNgay.AddYears(-nam.Tuoi);

                    GroupOperator go1 = new GroupOperator(GroupOperatorType.And);
                    go1.Operands.Add(CriteriaOperator.Parse("GioiTinh=?", GioiTinhEnum.Nam));
                    if (TruongConfig.MaTruong.Equals("QNU"))
                        go1.Operands.Add(CriteriaOperator.Parse("(NgaySinh<=? and NgayNghiHuu is null) or (NgayNghiHuu<=?)",
                            HamDungChung.SetTime(denNgay.AddMonths(-1), 1),
                            HamDungChung.SetTime(TinhDenNgay.AddMonths(-1), 1)));
                    else
                        go1.Operands.Add(CriteriaOperator.Parse("(NgaySinh<=? and NgayNghiHuu is null) or (NgayNghiHuu<=?)",
                            HamDungChung.SetTime(denNgay, 1),
                            HamDungChung.SetTime(TinhDenNgay, 1)));
                    go.Operands.Add(go1);
                }
                if (nu != null)
                {
                    denNgay = TinhDenNgay.AddYears(-nu.Tuoi);

                    GroupOperator go1 = new GroupOperator(GroupOperatorType.And);
                    go1.Operands.Add(CriteriaOperator.Parse("GioiTinh=?", GioiTinhEnum.Nu));
                    if (TruongConfig.MaTruong.Equals("QNU"))
                        go1.Operands.Add(CriteriaOperator.Parse("(NgaySinh<=? and NgayNghiHuu is null) or (NgayNghiHuu<=?)",
                            HamDungChung.SetTime(denNgay.AddMonths(-1), 1),
                            HamDungChung.SetTime(TinhDenNgay.AddMonths(-1), 1)));
                    else
                        go1.Operands.Add(CriteriaOperator.Parse("(NgaySinh<=? and NgayNghiHuu is null) or (NgayNghiHuu<=?)",
                            HamDungChung.SetTime(denNgay, 1),
                            HamDungChung.SetTime(TinhDenNgay, 1)));
                    go.Operands.Add(go1);
                }

                mainGO.Operands.Add(go);



                DenTuoiNghiHuuList.Reload();
                XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(Session, mainGO);

                //nhung nguoi khong co gia han
                foreach (ThongTinNhanVien item in nvList)
                {
                    DenTuoiNghiHuuList.Add(new DenTuoiNghiHuu(Session) { ThongTinNhanVien = item });
                }
            }
        }
    }

}
