﻿using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }
        [DisplayName("Giới tính nam")]
        public string GioiTinhNam { get; set; }
        [DisplayName("Giới tính nữ")]
        public string GioiTinhNu { get; set; }
        [DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Thâm niên cũ")]
        public string ThamNienCu { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng thâm niên cũ")]
        public string NgayHuongThamNienCu { get; set; }
        [System.ComponentModel.DisplayName("Thâm niên mới")]
        public string ThamNienMoi { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng thâm niên mới")]
        public string NgayHuongThamNienMoi { get; set; }
        [DisplayName("Mã ngạch lương")]
        public string MaNgachLuong { get; set; }
        [DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }

        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("Ngày Ký")]
        public string NgayKy { get; set; }
    }
}
