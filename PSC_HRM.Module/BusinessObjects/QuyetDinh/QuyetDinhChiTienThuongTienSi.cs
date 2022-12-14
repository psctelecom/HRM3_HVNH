using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định chi tiền thưởng tiến sĩ")]
    public class QuyetDinhChiTienThuongTienSi : QuyetDinh
    {
        private QuyetDinhCongNhanDaoTao _QuyetDinhCongNhanDaoTao;
        //private string _LuuTru;
        private decimal _PhuCapTienSi;

        [ImmediatePostData]
        //[DataSourceProperty("QuyetDinhCongNhanDaoTaoList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Quyết định công nhận đào tạo")]
        public QuyetDinhCongNhanDaoTao QuyetDinhCongNhanDaoTao
        {
            get
            {
                return _QuyetDinhCongNhanDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhCongNhanDaoTao", ref _QuyetDinhCongNhanDaoTao, value);
            }
        }


        [ModelDefault("Caption", "Phụ cấp tiến sĩ")]
        [ModelDefault("EditMask", "n0")]
        [ModelDefault("DisplayFormat", "n0")]
        public decimal PhuCapTienSi
        {
            get
            {
                return _PhuCapTienSi;
            }
            set
            {
                SetPropertyValue("PhuCapTienSi", ref _PhuCapTienSi, value);
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhChiTienThuongTienSi-ListChiTietChiTienThuongTienSi")]
        public XPCollection<ChiTietChiTienThuongTienSi> ListChiTietChiTienThuongTienSi
        {
            get
            {
                return GetCollection<ChiTietChiTienThuongTienSi>("ListChiTietChiTienThuongTienSi");
            }
        }

        public QuyetDinhChiTienThuongTienSi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhChiTienThuongTienSi;
            //
            //UpdateQuyetDinhCongNhanDaoTaoList();
            //
            PhuCapTienSi = 50000000;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            //UpdateQuyetDinhCongNhanDaoTaoList();
        }
        [Browsable(false)]
        public XPCollection<QuyetDinhCongNhanDaoTao> QuyetDinhCongNhanDaoTaoList { get; set; }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietChiTienThuongTienSi item in ListChiTietChiTienThuongTienSi)
                    {
                        item.GiayToHoSo.QuyetDinh = this;
                        item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
                        item.GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                        item.GiayToHoSo.NgayLap = NgayQuyetDinh;
                        item.GiayToHoSo.LuuTru = GiayToHoSo.LuuTru;
                        item.GiayToHoSo.DuongDanFile = GiayToHoSo.DuongDanFile;
                    }
                }
            }
        }
        private void UpdateQuyetDinhCongNhanDaoTaoList()
        {
            if (QuyetDinhCongNhanDaoTaoList == null)
            {
                QuyetDinhCongNhanDaoTaoList = new XPCollection<QuyetDinhCongNhanDaoTao>(Session);
            }
            //CriteriaOperator filter = CriteriaOperator.Parse("TrinhDoChuyenMon.TenTrinhDoChuyenMon=? or TrinhDoChuyenMon.TenTrinhDoChuyenMon=?", "Tiến sỹ", "Tiến sĩ");
            GroupOperator go = new GroupOperator(GroupOperatorType.Or);
            go.Operands.Add(CriteriaOperator.Parse("QuyetDinhDaoTao.TrinhDoChuyenMon.TenTrinhDoChuyenMon=? ", "Tiến sỹ"));
            go.Operands.Add(CriteriaOperator.Parse("QuyetDinhDaoTao.TrinhDoChuyenMon.TenTrinhDoChuyenMon=? ", "Tiến sĩ"));
            //
            QuyetDinhCongNhanDaoTaoList.Criteria = go;

        }
    }

}
