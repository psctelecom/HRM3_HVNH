using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.TuyenDung;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class HoSo_NgoaiNguChinhAction : ViewController
    {
        public HoSo_NgoaiNguChinhAction()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HoSo_NgoaiNguChinhAction");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            TrinhDoNgoaiNguKhac trinhDo = View.CurrentObject as TrinhDoNgoaiNguKhac;
            if (trinhDo != null)
            {
                if (trinhDo.HoSo is UngVien)
                {
                    UngVien ungVien = View.ObjectSpace.GetObjectByKey<UngVien>(trinhDo.HoSo.Oid);
                    if (ungVien != null)
                    {
                        ungVien.NgoaiNgu = trinhDo.NgoaiNgu;
                        ungVien.TrinhDoNgoaiNgu = trinhDo.TrinhDoNgoaiNgu;
                    }
                }
                else
                {
                    NhanVien nhanVien = View.ObjectSpace.GetObjectByKey<NhanVien>(trinhDo.HoSo.Oid);
                    if (nhanVien != null)
                    {
                        nhanVien.NhanVienTrinhDo.NgoaiNgu = trinhDo.NgoaiNgu;
                        nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu = trinhDo.TrinhDoNgoaiNgu;
                    }
                }

                //View.ObjectSpace.Refresh();
                View.Refresh();
            }
        }
    }
}
