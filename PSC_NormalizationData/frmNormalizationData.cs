using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Transactions;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using PSC_NormalizationData;
using System.Linq;
namespace PSC_NormalizationData
{

    public partial class frmNormalizationData : XtraForm
    {
        #region Field
        private SqlConnection _connection;
        private List<DanhMuc> _list1;
        private List<DanhMuc> _list2;
        #endregion

        #region Constructor
        public frmNormalizationData(String connectionString)
        {
            InitializeComponent();
            _connection = new SqlConnection(connectionString);
        }

        public frmNormalizationData(SqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
        }

        #endregion

        #region Method
        private void ChonDanhMucDung(DanhMuc obj)
        {
            txtRightData.Text = String.Format("Tên: {0}; Mã: {1}", obj.Name, obj.Code);
            txtRightData.Tag = obj;
            btnChoseRightData.Enabled = false;
            btnRemoveRightData.Enabled = true;
            _list1.Remove(obj);
            gridView1.RefreshData();
        }

        private void CauHinhLuoi(GridView gridView)
        {
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                GridColumn column = gridView.Columns[i];
                String fieldName = column.FieldName;
                if (fieldName == "Name" || fieldName == "Code")
                {
                    if (fieldName == "Name")
                    {
                        column.Caption = "Tên";
                    }
                    else if (fieldName == "Code")
                    {
                        column.Caption = "Mã";
                    }
                    column.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                    column.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                }
                else
                    column.Visible = false;
            }
        }

        private void LayDuLieuMoi()
        {
            using (DialogUtil.Wait(this))
            {
                DictionaryNormalizationData dic = cbTuDienDuLieu.GetSelectedDataRow() as DictionaryNormalizationData;
                //
                txtRightData.Text = "";
                txtRightData.Tag = null;
                //
                _list1 = new List<DanhMuc>();
                _list2 = new List<DanhMuc>();
                if (dic != null)
                {
                    dic.DanhMucList = DataHelper.GetDanhMucList(_connection, dic);
                    btnChoseRightData.Enabled = true;
                    btnRemoveRightData.Enabled = false;


                    foreach (var item in dic.DanhMucList)
                    {
                        _list1.Add(item);
                    }
                }
                gridView1.Columns.Clear();
                gridControl1.DataSource = _list1;
                gridView1.RefreshData();
                gridControl1.RefreshDataSource();
                CauHinhLuoi(gridView1);
                //

                gridView_WrongList.Columns.Clear();
                gridControl_WrongList.DataSource = _list2;
                gridView_WrongList.RefreshData();
                gridControl_WrongList.RefreshDataSource();
                CauHinhLuoi(gridView_WrongList);
            }
        }
        #endregion

        #region Event
        private void frmNormalizationData_Load(object sender, EventArgs e)
        {
            this.Shown += (object senderz, EventArgs ez) =>
            {
                using (DialogUtil.Wait(this))
                {
                    this.dictionaryNormalizationDataBindingSource.DataSource = DataHelper.GetDictionaries(_connection as SqlConnection);
                    GridUtil.SetSTTForGridView(new GridView[] { gridView1, gridView_WrongList }, 60);
                }
            };
        }
        private void btnChoseRightData_Click(object sender, EventArgs e)
        {
            DictionaryNormalizationData dic = cbTuDienDuLieu.GetSelectedDataRow() as DictionaryNormalizationData;
            if (dic != null)
                if (gridView1.SelectedRowsCount == 1)
                {
                    int[] rows = gridView1.GetSelectedRows();
                    DanhMuc danhMucDung = gridView1.GetRow(rows[0]) as DanhMuc;
                    if (danhMucDung != null)
                    {
                        ChonDanhMucDung(danhMucDung);

                    }
                }
                else
                    DialogUtil.ShowWarning("Bạn phải chọn một dòng để làm dữ liệu chuẩn hóa.");

        }
        private void btnRemoveRightData_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtRightData.Text) && txtRightData.Tag != null)
            {
                txtRightData.Text = string.Empty;

                _list1.Add(txtRightData.Tag as DanhMuc);
                txtRightData.Tag = null;

                gridView1.RefreshData();

                btnChoseRightData.Enabled = true;
                btnRemoveRightData.Enabled = false;
            }
            else
                DialogUtil.ShowWarning("Bạn phải chọn một dòng để làm dữ liệu chuẩn hóa.");
        }
        private void btnAddLessWrongData_Click(object sender, EventArgs e)
        {
            DictionaryNormalizationData dic = cbTuDienDuLieu.GetSelectedDataRow() as DictionaryNormalizationData;
            if (dic != null)
                using (DialogUtil.Wait(this))
                {

                    if (gridView1.SelectedRowsCount > 0)
                    {
                        int[] selectedRowIndexs = gridView1.GetSelectedRows();
                        List<DanhMuc> removeObjects = new List<DanhMuc>();
                        for (int i = 0; i < selectedRowIndexs.Length; i++)
                        {
                            DanhMuc danhMuc = gridView1.GetRow(selectedRowIndexs[i]) as DanhMuc;
                            //danhMuc.ForeignList = DataHelper.GetForeignList(_connection, dic, danhMuc);
                            if (danhMuc != null)
                            {
                                _list2.Add(danhMuc);
                                removeObjects.Add(danhMuc);
                            }
                        }
                        foreach (var item in removeObjects)
                        {
                            _list1.Remove(item);
                        }
                        gridView1.RefreshData();
                        gridView_WrongList.RefreshData();
                    }
                }
        }


        private void btnRemoveLessWrongData_Click(object sender, EventArgs e)
        {

            if (gridView_WrongList.SelectedRowsCount > 0)
            {
                int[] rows = gridView_WrongList.GetSelectedRows();
                List<DanhMuc> removeObjects = new List<DanhMuc>();
                for (int i = 0; i < rows.Length; i++)
                {
                    DanhMuc obj = gridView_WrongList.GetRow(rows[i]) as DanhMuc;
                    if (obj != null)
                    {
                        _list1.Add(obj);
                        removeObjects.Add(obj);

                    }
                }
                foreach (var item in removeObjects)
                {
                    _list2.Remove(item);
                }
                gridView1.RefreshData();
                gridView_WrongList.RefreshData();
            }
        }

        private void btnAddAllWrongData_Click(object sender, EventArgs e)
        {
            DictionaryNormalizationData dic = cbTuDienDuLieu.GetSelectedDataRow() as DictionaryNormalizationData;
            if (dic != null)
                using (DialogUtil.Wait(this))
                {
                    List<DanhMuc> removeObjects = new List<DanhMuc>();
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        DanhMuc danhMuc = gridView1.GetRow(i) as DanhMuc;
                        //danhMuc.ForeignList = DataHelper.GetForeignList(_connection, dic, danhMuc);
                        if (danhMuc != null)
                        {
                            _list2.Add(danhMuc);
                            removeObjects.Add(danhMuc);
                        }
                    }
                    foreach (var item in removeObjects)
                    {
                        _list1.Remove(item);
                    }
                    gridView1.RefreshData();
                    gridView_WrongList.RefreshData();
                }
        }

        private void btnRemoveAllWrongDataClick(object sender, EventArgs e)
        {
            List<DanhMuc> removeObjects = new List<DanhMuc>();
            for (int i = 0; i < gridView_WrongList.RowCount; i++)
            {
                DanhMuc obj = gridView_WrongList.GetRow(i) as DanhMuc;
                if (obj != null)
                {
                    _list1.Add(obj);
                    removeObjects.Add(obj);
                }
            }
            foreach (var item in removeObjects)
            {
                _list2.Remove(item);
            }
            gridView1.RefreshData();
            gridView_WrongList.RefreshData();
        }


        private void cbTuDienDuLieu_EditValueChanged(object sender, EventArgs e)
        {
            LayDuLieuMoi();
        }

        private void btnChapNhan_ChuanHoa_Click(object sender, EventArgs e)
        {
            DictionaryNormalizationData dic = cbTuDienDuLieu.GetSelectedDataRow() as DictionaryNormalizationData;
            if (dic == null)
            {
                DialogUtil.ShowWarning("Chưa chọn dữ liệu đúng");
            }
            else if (DialogUtil.ShowYesNo("Đồng ý chuẩn hóa?") == System.Windows.Forms.DialogResult.Yes)
            {

                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required,
                                        TimeSpan.FromSeconds(60 * 10)))
                {
                    try
                    {
                        DanhMuc danhMucDung = txtRightData.Tag as DanhMuc;

                        using (DialogUtil.Wait(this))
                        {

                            SqlConnection cnn = _connection;
                            foreach (DanhMuc danhMuc in _list2)
                            {
                                danhMuc.ForeignList = DataHelper.GetForeignList(_connection, dic, danhMuc);
                                foreach (var foreign in danhMuc.ForeignList)
                                {

                                    DataHelper.FixWrongItem(cnn, dic, danhMuc, foreign, danhMucDung.Id);
                                }
                                //xoa danh muc sai
                                DataHelper.DeleteWrongItem(cnn, danhMuc);
                            }

                            transaction.Complete();
                        }
                        DialogUtil.ShowInfo("Chuẩn hóa thành công");
                        LayDuLieuMoi();
                        {
                            danhMucDung = _list1.Find(x => x.Id.Equals(danhMucDung.Id));//(from o in _list1 where o.Id.Equals(danhMucDung.Id) select o).FirstOrDefault();
                            ChonDanhMucDung(danhMucDung);
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        DialogUtil.ShowError(ex.Message);
                    }

                }

            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }


        #endregion


    }


}