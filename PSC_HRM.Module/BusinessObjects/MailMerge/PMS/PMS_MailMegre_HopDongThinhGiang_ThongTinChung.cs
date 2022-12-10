using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class PMS_MailMegre_HopDongThinhGiang_ThongTinChung : IMailMergeBase
    {
        [System.ComponentModel.Browsable(false)]
        public string Oid { get; set; }
        public IList Master { get; set; }
        public IList Master1 { get; set; }
        public IList Detail { get; set; }
        public IList Detail1 { get; set; }

        [DisplayName("Mã nhân viên")]
        public string MaNhanVien { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Học hàm")]
        public string HocHam { get; set; }
        [DisplayName("Học vị")]
        public string HocVi { get; set; }
        [DisplayName("Loại GV")]
        public string LoaiGV { get; set; }
        [DisplayName("MSThuế GV")]
        public string MaSoThueGV { get; set; }
        [DisplayName("Số tài khoản GV")]
        public string SoTaiKhoan { get; set; }
        [DisplayName("Tên ngân hàng GV")]
        public string TenNganHang { get; set; }
        [DisplayName("Tên đơn vị")]
        public string TenBoPhan { get; set; }
        [DisplayName("Năm học")]
        public string TenNamHoc { get; set; }
        [DisplayName("Học kỳ")]
        public string TenHocKy { get; set; }
        [DisplayName("Điện thoại GV")]
        public string DienThoaiDiDong { get; set; }
        [DisplayName("Email GV")]
        public string Email { get; set; }
        [DisplayName("Tên đại diện 1")]
        public string TenDaiDien1 { get; set; }
        [DisplayName("Chức vụ đại diện 1")]
        public string NhanDaiDien1 { get; set; }
        [DisplayName("Tên đại diện 2")]
        public string TenDaiDien2 { get; set; }
        [DisplayName("Chức vụ đại diện 2")]
        public string NhanDaiDien2 { get; set; }
        [DisplayName("Địa chỉ trường")]
        public string FullDiaChi { get; set; }
        [DisplayName("MSThuế trường")]
        public string MaSoThue { get; set; }
        [DisplayName("Điện thoại trường")]
        public string DienThoai { get; set; }
        [DisplayName("Fax trường")]
        public string Fax { get; set; }
        [DisplayName("Nơi làm việc")]
        public string NoiLamViec { get; set; }
        [DisplayName("Chuyên ngành")]
        public string ChuyenNganh { get; set; }

        public PMS_MailMegre_HopDongThinhGiang_ThongTinChung()
        {
            Master = new List<PMS_MailMegre_HopDongThinhGiang_ThongTinMaster>();
            Detail = new List<PMS_MailMegre_HopDongThinhGiang_ThongTinDetail>();
        }
    }
}
