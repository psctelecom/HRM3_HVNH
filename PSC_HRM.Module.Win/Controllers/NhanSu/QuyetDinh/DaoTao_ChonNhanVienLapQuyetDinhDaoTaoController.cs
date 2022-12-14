using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.KhenThuong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Xpo;
using System.Windows.Forms;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers.NhanSu
{
    public partial class DaoTao_ChonNhanVienLapQuyetDinhDaoTaoController : ViewController
    {
        private QuyetDinhDaoTao qdDaoTao;
        private ChiTietDaoTao ctqdDaoTao;
        private TinhTrang tinhTrang;

        public DaoTao_ChonNhanVienLapQuyetDinhDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void DaoTao_ChonNhanVienLapQuyetDinhDaoTaoController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["Allow"] = HamDungChung.IsCreateGranted<QuyetDinhDaoTao>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            qdDaoTao = View.CurrentObject as QuyetDinhDaoTao;
            if (qdDaoTao != null)
            {
                using (frmChonCanBoLapQuyetDinhDaoTao chonCanBo = new frmChonCanBoLapQuyetDinhDaoTao(((XPObjectSpace)View.ObjectSpace).Session))
                {
                    if (chonCanBo.ShowDialog() == DialogResult.OK)
                    {
                        using (DialogUtil.AutoWait())
                        {
                            tinhTrang = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<TinhTrang>(chonCanBo.LayTinhTrang());
                            foreach (ThongTinNhanVien item in chonCanBo.LayDanhSachNhanVien())
                            {
                                if (!qdDaoTao.ExistsNhanVien(item))
                                {
                                    ctqdDaoTao = new ChiTietDaoTao(((XPObjectSpace)View.ObjectSpace).Session);
                                    ctqdDaoTao.QuyetDinhDaoTao = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<QuyetDinhDaoTao>(qdDaoTao.Oid);
                                    ctqdDaoTao.ThongTinNhanVien = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<ThongTinNhanVien>(item.Oid);
                                    ctqdDaoTao.BoPhan = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                    ctqdDaoTao.TinhTrang = tinhTrang;

                                    qdDaoTao.ListChiTietDaoTao.Add(ctqdDaoTao);
                                }
                            }
                            (View as DetailView).Refresh();
                        }
                    }
                }
            }
        }
    }
}
