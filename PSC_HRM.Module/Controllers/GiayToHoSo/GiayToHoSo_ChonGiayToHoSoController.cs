using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.Controllers
{
    public partial class GiayToHoSo_ChonGiayToHoSoController : ViewController
    {
        private IObjectSpace obs;
        private GiayToHoSo_ChonGiayToHoSo giayToHoSo;
        private QuyetDinh.QuyetDinh quyetDinh;
        private QuyetDinhCaNhan quyetDinhCaNhan;
        private QuyetDinhNangLuong qdNangLuong;
        private QuyetDinhNangPhuCapThamNienNhaGiao qdNangThamNien;
        private QuyetDinhNangNgach qdNangNgach;
        private QuyetDinhBoNhiemNgach qdBoNhiemNgach;
        private QuyetDinhChuyenNgach qdChuyenNgach;
        private QuyetDinhDaoTao qdDaoTao;
        private QuyetDinhBoiDuong qdBoiDuong;
        private QuyetDinhTiepNhanDaoTao qdTiepNhanDaoTao;
        private QuyetDinhCongNhanDaoTao qdCongNhanDaoTao;
        private QuyetDinhCongNhanHocVi qdCongNhanHocVi;
        private QuyetDinhCongNhanHetHanTapSu qdCongNhanHetHanTapSu;
        private QuyetDinhHuongDanTapSu qdHuongDanTapSu;
        private QuyetDinhDiCongTac qdDiCongTac;
        private QuyetDinhDiNuocNgoai qdDiNuocNgoai;
        public GiayToHoSo_ChonGiayToHoSoController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("GiayToHoSo_ChonGiayToHoSoController");
        }

        private void DanhGia_ImportChamCongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = false;
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            quyetDinh = View.CurrentObject as QuyetDinh.QuyetDinh;           
            if (quyetDinh != null)
            {
                obs = Application.CreateObjectSpace();
                giayToHoSo = obs.CreateObject<GiayToHoSo_ChonGiayToHoSo>();
                quyetDinhCaNhan = obs.GetObjectByKey<QuyetDinhCaNhan>(quyetDinh.Oid);
                if (quyetDinhCaNhan != null)
                    giayToHoSo.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(quyetDinhCaNhan.ThongTinNhanVien.Oid);
                else
                {
                    if (quyetDinh is QuyetDinhNangLuong)
                    {
                        qdNangLuong = ((QuyetDinhNangLuong)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhNangPhuCapThamNienNhaGiao)
                    {
                        qdNangThamNien = ((QuyetDinhNangPhuCapThamNienNhaGiao)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhNangNgach)
                    {
                        qdNangNgach = ((QuyetDinhNangNgach)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhBoNhiemNgach)
                    {
                        qdBoNhiemNgach = ((QuyetDinhBoNhiemNgach)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhChuyenNgach)
                    {
                        qdChuyenNgach = ((QuyetDinhChuyenNgach)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhDaoTao)
                    {
                        qdDaoTao = ((QuyetDinhDaoTao)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhBoiDuong)
                    {
                        qdBoiDuong = ((QuyetDinhBoiDuong)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhTiepNhanDaoTao)
                    {
                        qdTiepNhanDaoTao = ((QuyetDinhTiepNhanDaoTao)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhCongNhanDaoTao)
                    {
                        qdCongNhanDaoTao = ((QuyetDinhCongNhanDaoTao)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhCongNhanHocVi)
                    {
                        qdCongNhanHocVi = ((QuyetDinhCongNhanHocVi)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhCongNhanHetHanTapSu)
                    {
                        qdCongNhanHetHanTapSu = ((QuyetDinhCongNhanHetHanTapSu)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhHuongDanTapSu)
                    {
                        qdHuongDanTapSu = ((QuyetDinhHuongDanTapSu)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhDiCongTac)
                    {
                        qdDiCongTac = ((QuyetDinhDiCongTac)quyetDinh);
                    }
                    else if (quyetDinh is QuyetDinhDiNuocNgoai)
                    {
                        qdDiNuocNgoai = ((QuyetDinhDiNuocNgoai)quyetDinh);
                    }                    
                }
                e.View = Application.CreateDetailView(obs, giayToHoSo);
            }            
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            quyetDinh.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);

            if (quyetDinhCaNhan == null)
            {
                if (qdNangLuong != null)
                {
                    foreach (ChiTietQuyetDinhNangLuong item in qdNangLuong.ListChiTietQuyetDinhNangLuong)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdNangThamNien != null)
                {
                    foreach (ChiTietQuyetDinhNangPhuCapThamNienNhaGiao item in qdNangThamNien.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdNangNgach != null)
                {
                    foreach (ChiTietQuyetDinhNangNgach item in qdNangNgach.ListChiTietQuyetDinhNangNgach)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdBoNhiemNgach != null)
                {
                    foreach (ChiTietQuyetDinhBoNhiemNgach item in qdBoNhiemNgach.ListChiTietQuyetDinhBoNhiemNgach)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdChuyenNgach != null)
                {
                    foreach (ChiTietQuyetDinhChuyenNgach item in qdChuyenNgach.ListChiTietQuyetDinhChuyenNgach)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdDaoTao != null)
                {
                    foreach (ChiTietDaoTao item in qdDaoTao.ListChiTietDaoTao)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdBoiDuong != null)
                {
                    foreach (ChiTietBoiDuong item in qdBoiDuong.ListChiTietBoiDuong)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdTiepNhanDaoTao != null)
                {
                    foreach (ChiTietTiepNhanDaoTao item in qdTiepNhanDaoTao.ListChiTietTiepNhanDaoTao)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdCongNhanDaoTao != null)
                {
                    foreach (ChiTietCongNhanDaoTao item in qdCongNhanDaoTao.ListChiTietCongNhanDaoTao)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdCongNhanHocVi != null)
                {
                    foreach (ChiTietCongNhanHocVi item in qdCongNhanHocVi.ListChiTietCongNhanHocVi)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdCongNhanHetHanTapSu != null)
                {
                    foreach (ChiTietCongNhanHetHanTapSu item in qdCongNhanHetHanTapSu.ListChiTietCongNhanHetHanTapSu)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdHuongDanTapSu != null)
                {
                    foreach (ChiTietQuyetDinhHuongDanTapSu item in qdHuongDanTapSu.ListChiTietQuyetDinhHuongDanTapSu)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdDiCongTac != null)
                {
                    foreach (ChiTietQuyetDinhDiCongTac item in qdDiCongTac.ListChiTietQuyetDinhDiCongTac)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
                else if (qdDiNuocNgoai != null)
                {
                    foreach (ChiTietQuyetDinhDiNuocNgoai item in qdDiNuocNgoai.ListChiTietQuyetDinhDiNuocNgoai)
                    {
                        item.GiayToHoSo = obs.GetObjectByKey<GiayToHoSo>(giayToHoSo.GiayToHoSo.Oid);
                    }
                }
            }
            //View.ObjectSpace.CommitChanges();
            //View.ObjectSpace.Refresh();
            View.Refresh();
        }
    }
}
