using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DaoTao
{
    [DefaultClassOptions]
    [DefaultProperty("NamHoc")]
    [ImageName("BO_QuanLyDaoTao")]
    [ModelDefault("Caption", "Quản lý đào tạo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc")]
    public class QuanLyDaoTao : BaoMatBaseObject
    {
        // Fields...
        private NamHoc _NamHoc;

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Đăng ký đào tạo")]
        [Association("QuanLyDaoTao-ListDangKyDaoTao")]
        public XPCollection<DangKyDaoTao> ListDangKyDaoTao
        {
            get
            {
                return GetCollection<DangKyDaoTao>("ListDangKyDaoTao");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Duyệt đăng ký đào tạo")]
        [Association("QuanLyDaoTao-ListDuyetDangKyDaoTao")]
        public XPCollection<DuyetDangKyDaoTao> ListDuyetDangKyDaoTao
        {
            get
            {
                return GetCollection<DuyetDangKyDaoTao>("ListDuyetDangKyDaoTao");
            }
        }

        public QuanLyDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }

        /// <summary>
        /// Kiểm tra xem cán bộ đã tồn tại trong danh sách duyệt đào tạo chưa
        /// </summary>
        /// <param name="nhanVien">Cán bộ</param>
        /// <param name="trinhDo">Trình độ chuyên môn</param>
        /// <param name="nganh">Chuyên môn đào tạo</param>
        /// <param name="truong">Trường đào tạo</param>
        /// <returns></returns>
        public bool IsExist(TrinhDoChuyenMon trinhDo, ChuyenMonDaoTao nganh, TruongDaoTao truong)
        {
            foreach (DuyetDangKyDaoTao item in ListDuyetDangKyDaoTao)
            {
                if (item.TrinhDoChuyenMon.Oid == trinhDo.Oid &&
                    item.ChuyenMonDaoTao.Oid == nganh.Oid &&
                    item.TruongDaoTao.Oid == truong.Oid)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
