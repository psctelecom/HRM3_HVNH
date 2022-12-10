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
    public partial class XepLoaiLaoDong_DanhGiaLan2Controller : ViewController
    {
        public XepLoaiLaoDong_DanhGiaLan2Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("XepLoaiLaoDong_DanhGiaLan2Controller");
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            XepLoaiLaoDong xepLoai = View.CurrentObject as XepLoaiLaoDong;
            if (xepLoai != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                Session ses = ((XPObjectSpace)obs).Session;
                //
                foreach (var item in xepLoai.ListXepLoaiLan1)
                {
                    XepLoaiLan2 chitiet = new XepLoaiLan2(ses);
                    chitiet.ThongTinNhanVien = ses.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    chitiet.XepLoai = item.TruongDonViDanhGia;
                    //
                    xepLoai.ListXepLoaiLan2.Add(chitiet);
                }
                obs.CommitChanges();
                obs.Refresh();
            }
        }

        private void XepLoaiLaoDong_DanhGiaLan2Controller_Activated(object sender, EventArgs e)
        {
            simpleAction2.Active["TruyCap"] = HamDungChung.IsWriteGranted<XepLoaiLaoDong>() &&
                                              HamDungChung.IsWriteGranted<XepLoaiLan2>();
        }
    }
}
