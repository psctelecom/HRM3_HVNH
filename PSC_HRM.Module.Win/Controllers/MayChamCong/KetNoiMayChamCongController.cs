using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BusinessObjects.MayChamCong;
using PSC_HRM.Module.Win.Controllers.Utilities;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class KetNoiMayChamCongController : ViewController
    {
        public ZkemClient objZkeeper;
        private bool isDeviceConnected = false;

        public bool IsDeviceConnected
        {
            get { return isDeviceConnected; }
            set
            {
                isDeviceConnected = value;
                if (isDeviceConnected)
                {
                    // ignored
                }
                else
                {
                    objZkeeper.Disconnect();
                }
            }
        }

        public KetNoiMayChamCongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void KetNoiMayChamCongController_Activated(object sender, EventArgs e)
        {
                btnKetNoiMCC.Active["TruyCap"] = (HamDungChung.IsWriteGranted<MayChamCong>());
        }

        private void btnKetNoiMCC_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = View.ObjectSpace;
            Session ses = ((XPObjectSpace)obs).Session;
            XPCollection<MayChamCong> dsMCC = new XPCollection<MayChamCong>(ses);

            objZkeeper = new ZkemClient(RaiseDeviceEvent);
            using (DialogUtil.AutoWait("Hệ thống đang kiểm tra"))
            {
                foreach (MayChamCong mcc in dsMCC)
                {
                    IsDeviceConnected = objZkeeper.Connect_Net(mcc.ipAddress.Trim(), Int16.Parse(mcc.portNumber.Trim()));
                    if (IsDeviceConnected)
                    {
                        mcc.TrangThai = "Thành công";
                    }
                    else
                    {
                        mcc.TrangThai = "Thất bại";
                    }
                }
            }
        }

        private void RaiseDeviceEvent(object sender, string actionType)
        {
            switch (actionType)
            {
                case UniversalStatic.acx_Disconnect:
                    {
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
