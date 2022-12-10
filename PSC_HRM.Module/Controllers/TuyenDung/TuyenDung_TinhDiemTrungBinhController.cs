using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Utils;
using PSC_HRM.Module.TuyenDung;
using DevExpress.XtraEditors;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class TuyenDung_TinhDiemTrungBinhController : ViewController
    {
        public TuyenDung_TinhDiemTrungBinhController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TuyenDung_TinhDiemTrungBinhController");
        }

        private void TuyenDung_TrungTuyenController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<VongTuyenDung>() &&
                HamDungChung.IsWriteGranted<ChiTietVongTuyenDung>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
            {
                VongTuyenDung vongTuyenDung = View.CurrentObject as VongTuyenDung;
                if (vongTuyenDung != null)
                {
                    IObjectSpace obs = View.ObjectSpace;
                    CriteriaOperator filter;
                    if (vongTuyenDung.BuocTuyenDung.CoToChucThiTuyen)
                    {
                        foreach (ChiTietVongTuyenDung ctItem in vongTuyenDung.ListChiTietVongTuyenDung)
                        {
                            filter = CriteriaOperator.Parse("DanhSachThi.BuocTuyenDung=? and UngVien=?",
                                vongTuyenDung.BuocTuyenDung.Oid, ctItem.UngVien.Oid);
                            using (XPCollection<ThiSinh> thiSinhList = new XPCollection<ThiSinh>(((XPObjectSpace)obs).Session, filter))
                            {
                                int diem = 0, heSo = 0;
                                bool dau = true, mienThi = true;

                                foreach (ThiSinh tsItem in thiSinhList)
                                {
                                    if (!tsItem.MienThi)
                                    {
                                        mienThi = false;
                                        diem += tsItem.DiemSo * tsItem.DanhSachThi.MonThi.HeSo;
                                        heSo += tsItem.DanhSachThi.MonThi.HeSo;

                                        if (tsItem.VangThi || tsItem.DiemSo < tsItem.DanhSachThi.MonThi.DiemSan)
                                            dau = false;
                                    }
                                }

                                if (mienThi)
                                    ctItem.DuocMien = true;
                                else
                                {
                                    ctItem.DuocChuyenQuaVongSau = dau;
                                    if (heSo != 0)
                                        ctItem.Diem = Math.Round((decimal)diem / heSo, 1, MidpointRounding.AwayFromZero);
                                    else
                                        XtraMessageBox.Show("Chưa nhập hệ số cho môn thi");
                                }
                            }
                        }
                        obs.CommitChanges();
                        HamDungChung.ShowSuccessMessage("Tính điểm trung bình thành công");
                    }
                    else
                        HamDungChung.ShowWarningMessage("Vòng tuyển dụng này không tổ chức thi tuyển.");
                }
            }
        }
    }
}
