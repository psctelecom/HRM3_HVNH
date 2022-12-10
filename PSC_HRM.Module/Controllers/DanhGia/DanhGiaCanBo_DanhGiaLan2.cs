using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhGia;
using DevExpress.Utils;
using PSC_HRM.Module.HoSo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class DanhGiaCanBo_DanhGiaLan2 : ViewController
    {
        public DanhGiaCanBo_DanhGiaLan2()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DanhGiaCanBo_DonViDanhGiaController");
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            DanhGiaCanBo danhGia = View.CurrentObject as DanhGiaCanBo;
            if (danhGia != null)
            {
                foreach (DanhGiaLan1 item in danhGia.DanhSachDanhGiaLan1)
                {
                    if (!IsExists(danhGia.DanhSachDanhGiaLan2, item.ThongTinNhanVien))
                    {
                        danhGia.DanhSachDanhGiaLan2.Add(new DanhGiaLan2(((XPObjectSpace)View.ObjectSpace).Session)
                        {
                            BoPhan = item.BoPhan,
                            ThongTinNhanVien = item.ThongTinNhanVien,
                            XepLoaiLaoDong = item.XepLoai
                        });
                    }
                }
            }
        }

        private bool IsExists(XPCollection<DanhGiaLan2> list, ThongTinNhanVien nv)
        {
            foreach (DanhGiaLan2 item in list)
            {
                if (item.ThongTinNhanVien.Oid == nv.Oid)
                    return true;
            }
            return false;
        }

        private void DanhGiaCanBo_DanhGiaLan2_Activated(object sender, EventArgs e)
        {
            simpleAction2.Active["TruyCap"] = true;
        }
    }
}
