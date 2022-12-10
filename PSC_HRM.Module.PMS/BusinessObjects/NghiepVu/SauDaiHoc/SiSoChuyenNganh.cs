using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.NghiepVu.SauDaiHoc
{
    [ModelDefault("Caption", "Sĩ số - chuyên ngành")]
    [DefaultProperty("TenNganh")]
    public class SiSoChuyenNganh : BaseObject
    {
        private ChuyenNganhDaoTao _ChuyenNganhDaoTao;
         private string _TenNganh;
         private string _Khoa;
         private string _TenGhep;
         private int _SoHocVien;

         [ModelDefault("Caption", "Chuyên ngành")]
        public ChuyenNganhDaoTao ChuyenNganhDaoTao
         {
             get { return _ChuyenNganhDaoTao; }
             set { SetPropertyValue("ChuyenNganhDaoTao", ref _ChuyenNganhDaoTao, value); }
         }
         [ModelDefault("Caption", "Tên ngành")]
         public string TenNganh
         {
             get { return _TenNganh; }
             set
             {
                 SetPropertyValue("TenNganh", ref _TenNganh, value);
             }
         }

         [ModelDefault("Caption", "Khóa")]
         public string Khoa
         {
             get { return _Khoa; }
             set
             {
                 SetPropertyValue("Khoa", ref _Khoa, value);
             }
         }

         [ModelDefault("Caption", "Tên ghép")]
         public string TenGhep
         {
             get
             {
                 if (TenNganh == string.Empty && SoHocVien == 0)
                     return "";
                 else
                 {
                     string TenNganhDT = "";
                     if (ChuyenNganhDaoTao != null)
                         TenNganhDT = ChuyenNganhDaoTao.TenChuyenNganh;
                     return String.Format("{0}{1}", TenNganhDT.Length > 16 ? TenNganhDT.Substring(0, 16) : TenNganhDT, Khoa);
                 }
             }
             //get { return _TenGhep; }
             //set
             //{
             //    SetPropertyValue("TenGhep", ref _TenGhep, value);
             //}
         }

         [ModelDefault("Caption", "Sĩ số học viên")]
         public int SoHocVien
         {
             get { return _SoHocVien; }
             set
             {
                 SetPropertyValue("SoHocVien", ref _SoHocVien, value);
             }
         }

         public SiSoChuyenNganh(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}