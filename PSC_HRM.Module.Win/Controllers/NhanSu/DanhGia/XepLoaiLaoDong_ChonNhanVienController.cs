using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.DanhGia;
using DevExpress.Xpo;

namespace PSC_HRM.Module.Win.Controllers.NhanSu
{
    public partial class XepLoaiLaoDong_ChonNhanVienController : ViewController
    {
        public XepLoaiLaoDong_ChonNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)obs).Session;

            using (frmChonCanBo chonCanBo = new frmChonCanBo(ses))
            {
                if (chonCanBo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    XepLoaiLaoDong xepLoai = View.CurrentObject as XepLoaiLaoDong;
                    if (xepLoai != null)
                    {
                        foreach (var item in chonCanBo.LayDanhSachNhanVien())
                        {
                            XepLoaiLan1 chitiet = new XepLoaiLan1(ses);
                            chitiet.ThongTinNhanVien = ses.GetObjectByKey<ThongTinNhanVien>(item.Oid);
                            chitiet.PhanMemDanhGia = XepLoaiDanhGiaEnum.LoaiA;
                            chitiet.TruongDonViDanhGia = XepLoaiDanhGiaEnum.LoaiA;
                            chitiet.CaNhanTuDanhGia = XepLoaiDanhGiaEnum.LoaiA;
                            //
                            xepLoai.ListXepLoaiLan1.Add(chitiet);
                        }
                    }
                    obs.CommitChanges();
                    obs.Refresh();
                }
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                e.ShowViewParameters.Context = TemplateContext.View;
            }
        }
    }
}