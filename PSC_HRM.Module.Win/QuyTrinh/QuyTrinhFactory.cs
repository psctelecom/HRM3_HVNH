using DevExpress.ExpressApp;
using PSC_HRM.Module.Win.QuyTrinh.BoiDuong;
using PSC_HRM.Module.Win.QuyTrinh.BoNhiem;
using PSC_HRM.Module.Win.QuyTrinh.ChuyenNgach;
using PSC_HRM.Module.Win.QuyTrinh.XepLoaiLaoDong;
using PSC_HRM.Module.Win.QuyTrinh.DaoTao;
using PSC_HRM.Module.Win.QuyTrinh.DiNuocNgoai;
using PSC_HRM.Module.Win.QuyTrinh.KhenThuong;
using PSC_HRM.Module.Win.QuyTrinh.NangLuong;
using PSC_HRM.Module.Win.QuyTrinh.NangNgach;
using PSC_HRM.Module.Win.QuyTrinh.NangThamNien;
using PSC_HRM.Module.Win.QuyTrinh.NghiHuu;
using PSC_HRM.Module.Win.QuyTrinh.TapSu;
using PSC_HRM.Module.Win.QuyTrinh.ThoiViec;
using PSC_HRM.Module.Win.QuyTrinh.ThuViec;
using PSC_HRM.Module.Win.QuyTrinh.TuyenDung;
using PSC_HRM.Module.XuLyQuyTrinh;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.Win.QuyTrinh.NghiKhongHuongLuong;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    public class QuyTrinhFactory
    {
        private QuyTrinhFactory()
        { }

        public static ThucHienQuyTrinhTypeEnum Type { get; set; }

        public static QuyTrinhBaseController CreateControl(XafApplication app, IObjectSpace obs)
        {
            switch (Type)
            {
                case ThucHienQuyTrinhTypeEnum.QuyTrinhTapSu:
                    return new QuyTrinhTapSuController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhThuViec:
                    return new QuyTrinhThuViecController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhNghiHuu:
                    return new QuyTrinhNghiHuuController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhKhenThuong:
                    return new QuyTrinhKhenThuongController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhDiNuocNgoai:
                    return new QuyTrinhDiNuocNgoaiController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhThoiViec:
                    return new QuyTrinhThoiViecController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhDaoTao:
                    return new QuyTrinhDaoTaoController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhBoiDuong:
                    return new QuyTrinhBoiDuongController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhBoNhiem:
                    return new QuyTrinhBoNhiemController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhChuyenNgach:
                    return new QuyTrinhChuyenNgachController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhNangLuong:
                    return new QuyTrinhNangLuongController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhNangNgach:
                    return new QuyTrinhNangNgachController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhNangThamNien:
                    return new QuyTrinhNangThamNienController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhTuyenDung:
                    return new QuyTrinhTuyenDungController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhNghiKhongHuongLuong:
                    return new QuyTrinhNghiKhongHuongLuongController(app, obs);
                case ThucHienQuyTrinhTypeEnum.QuyTrinhChuyenCongTac:
                    return new QuyTrinhChuyenCongTacController(app, obs);
                default:
                    return new QuyTrinhBaseController(app, obs);
            }
        }
    }
}
