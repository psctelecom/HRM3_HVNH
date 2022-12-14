using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Security;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class Luong_TinhLuongNhanVienController : ViewController
    {
        public Luong_TinhLuongNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void Luong_TinhLuongNhanVienController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangLuongNhanVien>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            BangLuongNhanVien bangLuong = View.CurrentObject as BangLuongNhanVien;
            if (bangLuong != null)
            {
                if (bangLuong.KyTinhLuong.KhoaSo)
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", bangLuong.KyTinhLuong.TenKy));
                else if (bangLuong.ChungTu != null)
                    DialogUtil.ShowWarning("Bảng lương đã được lập chứng từ chi tiền.");
                else if ((bangLuong.NgayLap < bangLuong.KyTinhLuong.TuNgay || bangLuong.NgayLap > bangLuong.KyTinhLuong.DenNgay) && TruongConfig.MaTruong != "HBU")
                {
                    DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                }
                else
                {
                    try
                    {
                        using (DialogUtil.AutoWait())
                        {
                            //Lấy công thức tính lương
                            XPCollection<CongThucTinhLuong> congThucTinhLuongList;
                            if (bangLuong.LoaiLuong == LoaiLuongEnum.LuongKy1)
                            {
                                congThucTinhLuongList = new XPCollection<CongThucTinhLuong>(((XPObjectSpace)View.ObjectSpace).Session, CriteriaOperator.Parse("LoaiLuong=?", 0));
                            }
                            else if (bangLuong.LoaiLuong == LoaiLuongEnum.LuongKy2)
                            {
                                congThucTinhLuongList = new XPCollection<CongThucTinhLuong>(((XPObjectSpace)View.ObjectSpace).Session, CriteriaOperator.Parse("LoaiLuong=?", 1));
                            }
                            else if (bangLuong.LoaiLuong == LoaiLuongEnum.LuongTienSi)
                            {
                                congThucTinhLuongList = new XPCollection<CongThucTinhLuong>(((XPObjectSpace)View.ObjectSpace).Session, CriteriaOperator.Parse("LoaiLuong=?", 2));
                            }
                            else 
                            {
                                congThucTinhLuongList = new XPCollection<CongThucTinhLuong>(((XPObjectSpace)View.ObjectSpace).Session, CriteriaOperator.Parse("LoaiLuong=?", 3));
                            }
                            //
                            SystemContainer.Resolver<ITaiChinh>("TinhLuongNhanVien").XuLy(View.ObjectSpace, bangLuong, congThucTinhLuongList);

                            //Refesh lại dữ liệu
                            View.ObjectSpace.ReloadObject(bangLuong);
                            (View as DetailView).Refresh();
                        }
                        //Thông báo kết quả
                        DialogUtil.ShowInfo("Tính lương thành công.");
                    }
                    catch (Exception ex)
                    {
                        //Thông báo lỗi
                        DialogUtil.ShowError("Tính lương không thành công." + ex.Message);
                    }
                }
            }
        }
    }
}
