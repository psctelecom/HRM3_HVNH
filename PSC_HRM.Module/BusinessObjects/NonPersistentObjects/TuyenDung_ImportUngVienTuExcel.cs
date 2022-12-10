using System;

using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.TuyenDung;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Import ứng viên từ file excel")]
    public class TuyenDung_ImportUngVienTuExcel : BaseObject
    {
        // Fields...
        private ViTriTuyenDung _ViTriTuyenDung;

        [ModelDefault("Caption", "Vị trí tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ViTriTuyenDung ViTriTuyenDung
        {
            get
            {
                return _ViTriTuyenDung;
            }
            set
            {
                SetPropertyValue("ViTriTuyenDung", ref _ViTriTuyenDung, value);
            }
        }

        public TuyenDung_ImportUngVienTuExcel(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ViTriTuyenDung> ViTriTuyenDungList { get; set; }

        public void SetViTriTuyenDungList(QuanLyTuyenDung quanLyTuyenDung)
        {
            ViTriTuyenDungList = new XPCollection<ViTriTuyenDung>(Session,
                CriteriaOperator.Parse("QuanLyTuyenDung=?", quanLyTuyenDung.Oid));
        }

        public void XuLy(IObjectSpace obs)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.FileName = "";
                dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                    {
                        if (dt != null)
                        {
                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {

                                UngVien ungVien;
                                BoPhan boPhan;

                                foreach (DataRow item in dt.Rows)
                                {
                                    ungVien = uow.FindObject<UngVien>(CriteriaOperator.Parse("CMND_UngVien=?", item[6]));
                                    if (ungVien == null)
                                    {
                                        ungVien = new UngVien(uow);
                                        ungVien.QuanLyTuyenDung = uow.GetObjectByKey<QuanLyTuyenDung>(ViTriTuyenDung.QuanLyTuyenDung.Oid);

                                        //nhu cầu tuyển dụng
                                        boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan=?", item[1]));
                                        if (boPhan != null)
                                            ungVien.NhuCauTuyenDung = uow.FindObject<NhuCauTuyenDung>(CriteriaOperator.Parse("ViTriTuyenDung=? and BoPhan=?", ViTriTuyenDung.Oid, boPhan.Oid));
                                        
                                        //họ
                                        if (!item.IsNull(2) && item[2].ToString().Trim() != string.Empty)
                                            ungVien.Ho = HamDungChung.VietHoaChuDau(item[2].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

                                        //tên
                                        if (!item.IsNull(3) && item[3].ToString().Trim() != string.Empty)
                                            ungVien.Ten = HamDungChung.VietHoaChuDau(new string[] { item[3].ToString().Trim() });

                                        //ngày sinh
                                        int iTemp;
                                        if (!item.IsNull(4) && item[4].ToString().Trim() != string.Empty &&
                                            int.TryParse(item[4].ToString().Trim(), out iTemp))
                                            ungVien.NgaySinh = new DateTime(iTemp, 1, 1);

                                        //giới tính
                                        if (!item.IsNull(5) && item[5].ToString().Trim() != string.Empty &&
                                            item[5].ToString().Trim().ToLower() == "nam")
                                            ungVien.GioiTinh = GioiTinhEnum.Nam;
                                        else
                                            ungVien.GioiTinh = GioiTinhEnum.Nu;

                                        //CMND
                                        if (!item.IsNull(6) && item[6].ToString().Trim() != string.Empty)
                                            ungVien.CMND_UngVien = item[6].ToString().Trim();

                                        //điện thoại di động
                                        if (!item.IsNull(7) && item[7].ToString().Trim() != string.Empty)
                                            ungVien.DienThoaiDiDong = item[7].ToString().Trim();

                                        //địa chỉ thường trú
                                        DiaChi diaChi;
                                        if (!item.IsNull(8) && item[8].ToString().Trim() != string.Empty)
                                        {
                                            diaChi = new DiaChi(uow);
                                            diaChi.SoNha = item[8].ToString().Trim();
                                            ungVien.DiaChiThuongTru = diaChi;
                                        }

                                        //nơi ở hiện nay
                                        if (!item.IsNull(9) && item[9].ToString().Trim() != string.Empty)
                                        {
                                            diaChi = new DiaChi(uow);
                                            diaChi.SoNha = item[9].ToString().Trim();
                                            ungVien.NoiOHienNay = diaChi;
                                        }

                                        uow.CommitChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
