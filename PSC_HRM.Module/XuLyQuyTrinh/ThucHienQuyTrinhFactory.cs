using PSC_HRM.Module.XuLyQuyTrinh.BoiDuong;
using PSC_HRM.Module.XuLyQuyTrinh.BoNhiem;
using PSC_HRM.Module.XuLyQuyTrinh.ChuyenNgach;
using PSC_HRM.Module.XuLyQuyTrinh.XepLoaiLaoDong;
using PSC_HRM.Module.XuLyQuyTrinh.DaoTao;
using PSC_HRM.Module.XuLyQuyTrinh.DiNuocNgoai;
using PSC_HRM.Module.XuLyQuyTrinh.KhenThuong;
using PSC_HRM.Module.XuLyQuyTrinh.NangLuong;
using PSC_HRM.Module.XuLyQuyTrinh.NangNgach;
using PSC_HRM.Module.XuLyQuyTrinh.NangThamNien;
using PSC_HRM.Module.XuLyQuyTrinh.NghiHuu;
using PSC_HRM.Module.XuLyQuyTrinh.TapSu;
using PSC_HRM.Module.XuLyQuyTrinh.ThoiViec;
using PSC_HRM.Module.XuLyQuyTrinh.ThuViec;
using PSC_HRM.Module.XuLyQuyTrinh.TuyenDung;
using PSC_HRM.Module.XuLyQuyTrinh.XuLyViPham;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.XuLyQuyTrinh.NghiKhongHuongLuong;

namespace PSC_HRM.Module.XuLyQuyTrinh
{
    public class ThucHienQuyTrinhFactory
    {
        public static IThucHienQuyTrinh CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum type)
        {
            switch (type)
            {
                case ThucHienQuyTrinhTypeEnum.QuyTrinhDaoTao:
                    return new ThucHienQuyTrinhDaoTao();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhBoiDuong:
                    return new ThucHienQuyTrinhBoiDuong();
            	case ThucHienQuyTrinhTypeEnum.QuyTrinhNghiHuu:
                    return new ThucHienQuyTrinhNghiHuu();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhKhenThuong:
                    return new ThucHienQuyTrinhKhenThuong();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhDiNuocNgoai:
                    return new ThucHienQuyTrinhDiNuocNgoai();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhNangLuong:
                    return new ThucHienQuyTrinhNangLuong();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhNangThamNien:
                    return new ThucHienQuyTrinhNangThamNien();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhTapSu:
                    return new ThucHienQuyTrinhTapSu();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhThuViec:
                    return new ThucHienQuyTrinhThuViec();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhThoiViec:
                    return new ThucHienQuyTrinhThoiViec();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhTuyenDung:
                    return new ThucHienQuyTrinhTuyenDung();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhNangNgach:
                    return new ThucHienQuyTrinhNangNgach();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhChuyenNgach:
                    return new ThucHienQuyTrinhChuyenNgach();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhXepLoaiLaoDong:
                    return new ThucHienQuyTrinhXepLoaiLaoDong();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhBoNhiem:
                    return new ThucHienQuyTrinhBoNhiem();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhXuLyViPham:
                    return new ThucHienQuyTrinhXuLyViPham();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhNghiKhongHuongLuong:
                    return new ThucHienQuyTrinhNghiKhongHuongLuong();
                case ThucHienQuyTrinhTypeEnum.QuyTrinhChuyenCongTac:
                    return new ThucHienQuyTrinhChuyenCongTac();
                default:
                    return new ThucHienQuyTrinhNghiHuu();
            }
        }
    }
}
