namespace PSC_NormalizationData
{
    partial class frmNormalizationData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNormalizationData));
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnChapNhan_ChuanHoa = new DevExpress.XtraEditors.SimpleButton();
            this.dictionaryNormalizationDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cbTuDienDuLieu = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl_WrongList = new DevExpress.XtraGrid.GridControl();
            this.gridView_WrongList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.txtRightData = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnRemoveAllWrongData = new DevExpress.XtraEditors.SimpleButton();
            this.btnChoseRightData = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddAllWrongData = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveLessWrongData = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveRightData = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddLessWrongData = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryNormalizationDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbTuDienDuLieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_WrongList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_WrongList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRightData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnThoat
            // 
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.Location = new System.Drawing.Point(875, 10);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(86, 23);
            this.btnThoat.TabIndex = 1;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnChapNhan_ChuanHoa
            // 
            this.btnChapNhan_ChuanHoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChapNhan_ChuanHoa.Location = new System.Drawing.Point(783, 10);
            this.btnChapNhan_ChuanHoa.Name = "btnChapNhan_ChuanHoa";
            this.btnChapNhan_ChuanHoa.Size = new System.Drawing.Size(86, 23);
            this.btnChapNhan_ChuanHoa.TabIndex = 0;
            this.btnChapNhan_ChuanHoa.Text = "Chuẩn hóa";
            this.btnChapNhan_ChuanHoa.Click += new System.EventHandler(this.btnChapNhan_ChuanHoa_Click);
            // 
            // dictionaryNormalizationDataBindingSource
            // 
            this.dictionaryNormalizationDataBindingSource.DataSource = typeof(PSC_NormalizationData.DictionaryNormalizationData);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cbTuDienDuLieu);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(973, 44);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "Từ điển dữ liệu";
            // 
            // cbTuDienDuLieu
            // 
            this.cbTuDienDuLieu.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbTuDienDuLieu.EditValue = "<<Chưa chọn>>";
            this.cbTuDienDuLieu.Location = new System.Drawing.Point(2, 21);
            this.cbTuDienDuLieu.Name = "cbTuDienDuLieu";
            this.cbTuDienDuLieu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTuDienDuLieu.Properties.DataSource = this.dictionaryNormalizationDataBindingSource;
            this.cbTuDienDuLieu.Properties.DisplayMember = "Name";
            this.cbTuDienDuLieu.Properties.NullText = "<<Chưa chọn>>";
            this.cbTuDienDuLieu.Properties.ValueMember = "Id";
            this.cbTuDienDuLieu.Properties.View = this.gridLookUpEdit1View;
            this.cbTuDienDuLieu.Size = new System.Drawing.Size(391, 20);
            this.cbTuDienDuLieu.TabIndex = 0;
            this.cbTuDienDuLieu.EditValueChanged += new System.EventHandler(this.cbTuDienDuLieu_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnThoat);
            this.panelControl1.Controls.Add(this.btnChapNhan_ChuanHoa);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 494);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(973, 45);
            this.panelControl1.TabIndex = 8;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 44);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl3);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(973, 450);
            this.splitContainerControl1.SplitterPosition = 395;
            this.splitContainerControl1.TabIndex = 9;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(395, 450);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Danh mục";
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 21);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(391, 427);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(119)))));
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridView1.Appearance.OddRow.Options.UseBackColor = true;
            this.gridView1.AppearancePrint.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(170)))));
            this.gridView1.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsDetail.ShowDetailTabs = false;
            this.gridView1.OptionsDetail.SmartDetailExpand = false;
            this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView1.OptionsPrint.ExpandAllGroups = false;
            this.gridView1.OptionsPrint.PrintGroupFooter = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.groupControl4);
            this.panelControl3.Controls.Add(this.groupControl3);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(52, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(521, 450);
            this.panelControl3.TabIndex = 1;
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.gridControl_WrongList);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl4.Location = new System.Drawing.Point(0, 44);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(521, 406);
            this.groupControl4.TabIndex = 1;
            this.groupControl4.Text = "Dữ liệu sai";
            // 
            // gridControl_WrongList
            // 
            this.gridControl_WrongList.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl_WrongList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_WrongList.Location = new System.Drawing.Point(2, 21);
            this.gridControl_WrongList.MainView = this.gridView_WrongList;
            this.gridControl_WrongList.Name = "gridControl_WrongList";
            this.gridControl_WrongList.Size = new System.Drawing.Size(517, 383);
            this.gridControl_WrongList.TabIndex = 0;
            this.gridControl_WrongList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_WrongList});
            // 
            // gridView_WrongList
            // 
            this.gridView_WrongList.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(119)))));
            this.gridView_WrongList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView_WrongList.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridView_WrongList.Appearance.OddRow.Options.UseBackColor = true;
            this.gridView_WrongList.AppearancePrint.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(170)))));
            this.gridView_WrongList.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.gridView_WrongList.GridControl = this.gridControl_WrongList;
            this.gridView_WrongList.Name = "gridView_WrongList";
            this.gridView_WrongList.OptionsBehavior.Editable = false;
            this.gridView_WrongList.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gridView_WrongList.OptionsCustomization.AllowGroup = false;
            this.gridView_WrongList.OptionsDetail.EnableMasterViewMode = false;
            this.gridView_WrongList.OptionsDetail.ShowDetailTabs = false;
            this.gridView_WrongList.OptionsDetail.SmartDetailExpand = false;
            this.gridView_WrongList.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView_WrongList.OptionsPrint.ExpandAllGroups = false;
            this.gridView_WrongList.OptionsPrint.PrintGroupFooter = false;
            this.gridView_WrongList.OptionsSelection.MultiSelect = true;
            this.gridView_WrongList.OptionsView.EnableAppearanceOddRow = true;
            this.gridView_WrongList.OptionsView.ShowAutoFilterRow = true;
            this.gridView_WrongList.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView_WrongList.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.txtRightData);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(521, 44);
            this.groupControl3.TabIndex = 0;
            this.groupControl3.Text = "Dữ liệu đúng";
            // 
            // txtRightData
            // 
            this.txtRightData.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtRightData.Location = new System.Drawing.Point(2, 21);
            this.txtRightData.Name = "txtRightData";
            this.txtRightData.Properties.ReadOnly = true;
            this.txtRightData.Size = new System.Drawing.Size(517, 20);
            this.txtRightData.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnRemoveAllWrongData);
            this.panelControl2.Controls.Add(this.btnChoseRightData);
            this.panelControl2.Controls.Add(this.btnAddAllWrongData);
            this.panelControl2.Controls.Add(this.btnRemoveLessWrongData);
            this.panelControl2.Controls.Add(this.btnRemoveRightData);
            this.panelControl2.Controls.Add(this.btnAddLessWrongData);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(52, 450);
            this.panelControl2.TabIndex = 0;
            // 
            // btnRemoveAllWrongData
            // 
            this.btnRemoveAllWrongData.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRemoveAllWrongData.Appearance.Options.UseFont = true;
            this.btnRemoveAllWrongData.Location = new System.Drawing.Point(4, 273);
            this.btnRemoveAllWrongData.Name = "btnRemoveAllWrongData";
            this.btnRemoveAllWrongData.Size = new System.Drawing.Size(44, 23);
            this.btnRemoveAllWrongData.TabIndex = 5;
            this.btnRemoveAllWrongData.Text = "|<<";
            this.btnRemoveAllWrongData.Click += new System.EventHandler(this.btnRemoveAllWrongDataClick);
            // 
            // btnChoseRightData
            // 
            this.btnChoseRightData.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnChoseRightData.Appearance.Options.UseFont = true;
            this.btnChoseRightData.Location = new System.Drawing.Point(27, 19);
            this.btnChoseRightData.Name = "btnChoseRightData";
            this.btnChoseRightData.Size = new System.Drawing.Size(22, 23);
            this.btnChoseRightData.TabIndex = 1;
            this.btnChoseRightData.Text = ">";
            this.btnChoseRightData.Click += new System.EventHandler(this.btnChoseRightData_Click);
            // 
            // btnAddAllWrongData
            // 
            this.btnAddAllWrongData.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddAllWrongData.Appearance.Options.UseFont = true;
            this.btnAddAllWrongData.Location = new System.Drawing.Point(4, 171);
            this.btnAddAllWrongData.Name = "btnAddAllWrongData";
            this.btnAddAllWrongData.Size = new System.Drawing.Size(44, 23);
            this.btnAddAllWrongData.TabIndex = 3;
            this.btnAddAllWrongData.Text = ">>|";
            this.btnAddAllWrongData.Click += new System.EventHandler(this.btnAddAllWrongData_Click);
            // 
            // btnRemoveLessWrongData
            // 
            this.btnRemoveLessWrongData.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRemoveLessWrongData.Appearance.Options.UseFont = true;
            this.btnRemoveLessWrongData.Location = new System.Drawing.Point(4, 244);
            this.btnRemoveLessWrongData.Name = "btnRemoveLessWrongData";
            this.btnRemoveLessWrongData.Size = new System.Drawing.Size(44, 23);
            this.btnRemoveLessWrongData.TabIndex = 4;
            this.btnRemoveLessWrongData.Text = "|<";
            this.btnRemoveLessWrongData.Click += new System.EventHandler(this.btnRemoveLessWrongData_Click);
            // 
            // btnRemoveRightData
            // 
            this.btnRemoveRightData.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRemoveRightData.Appearance.Options.UseFont = true;
            this.btnRemoveRightData.Location = new System.Drawing.Point(3, 19);
            this.btnRemoveRightData.Name = "btnRemoveRightData";
            this.btnRemoveRightData.Size = new System.Drawing.Size(22, 23);
            this.btnRemoveRightData.TabIndex = 0;
            this.btnRemoveRightData.Text = "<";
            this.btnRemoveRightData.Click += new System.EventHandler(this.btnRemoveRightData_Click);
            // 
            // btnAddLessWrongData
            // 
            this.btnAddLessWrongData.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddLessWrongData.Appearance.Options.UseFont = true;
            this.btnAddLessWrongData.Location = new System.Drawing.Point(4, 143);
            this.btnAddLessWrongData.Name = "btnAddLessWrongData";
            this.btnAddLessWrongData.Size = new System.Drawing.Size(44, 23);
            this.btnAddLessWrongData.TabIndex = 2;
            this.btnAddLessWrongData.Text = ">|";
            this.btnAddLessWrongData.Click += new System.EventHandler(this.btnAddLessWrongData_Click);
            // 
            // frmNormalizationData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 539);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNormalizationData";
            this.Text = "Chuẩn hóa dữ liệu";
            this.Load += new System.EventHandler(this.frmNormalizationData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryNormalizationDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbTuDienDuLieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_WrongList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_WrongList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRightData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnChapNhan_ChuanHoa;
        private System.Windows.Forms.BindingSource dictionaryNormalizationDataBindingSource;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GridLookUpEdit cbTuDienDuLieu;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraGrid.GridControl gridControl_WrongList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_WrongList;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.TextEdit txtRightData;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnRemoveAllWrongData;
        private DevExpress.XtraEditors.SimpleButton btnChoseRightData;
        private DevExpress.XtraEditors.SimpleButton btnAddAllWrongData;
        private DevExpress.XtraEditors.SimpleButton btnRemoveLessWrongData;
        private DevExpress.XtraEditors.SimpleButton btnRemoveRightData;
        private DevExpress.XtraEditors.SimpleButton btnAddLessWrongData;
    }
}