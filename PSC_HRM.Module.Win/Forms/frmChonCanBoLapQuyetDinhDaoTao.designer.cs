namespace PSC_HRM.Module.Win.Forms
{
    partial class frmChonCanBoLapQuyetDinhDaoTao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChonCanBoLapQuyetDinhDaoTao));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.donViTreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.nhanVienTreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miCheckSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miUncheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miUncheckSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.tinhTranglke = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.donViTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nhanVienTreeList)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tinhTranglke.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.donViTreeList);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.nhanVienTreeList);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(569, 408);
            this.splitContainerControl1.SplitterPosition = 255;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // donViTreeList
            // 
            this.donViTreeList.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(170)))));
            this.donViTreeList.Appearance.EvenRow.Options.UseBackColor = true;
            this.donViTreeList.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(170)))));
            this.donViTreeList.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.donViTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.donViTreeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.donViTreeList.Location = new System.Drawing.Point(0, 0);
            this.donViTreeList.Name = "donViTreeList";
            this.donViTreeList.OptionsBehavior.AllowIndeterminateCheckState = true;
            this.donViTreeList.OptionsBehavior.Editable = false;
            this.donViTreeList.OptionsBehavior.EnableFiltering = true;
            this.donViTreeList.OptionsView.ShowCheckBoxes = true;
            this.donViTreeList.OptionsView.ShowIndicator = false;
            this.donViTreeList.OptionsView.ShowSummaryFooter = true;
            this.donViTreeList.Size = new System.Drawing.Size(255, 408);
            this.donViTreeList.TabIndex = 0;
            this.donViTreeList.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.donViTreeList_BeforeCheckNode);
            this.donViTreeList.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.donViTreeList_AfterCheckNode);
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Danh sách đơn vị";
            this.treeListColumn2.FieldName = "TenBoPhan";
            this.treeListColumn2.MinWidth = 32;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // nhanVienTreeList
            // 
            this.nhanVienTreeList.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(170)))));
            this.nhanVienTreeList.Appearance.EvenRow.Options.UseBackColor = true;
            this.nhanVienTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.nhanVienTreeList.ContextMenuStrip = this.menu;
            this.nhanVienTreeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nhanVienTreeList.Location = new System.Drawing.Point(0, 0);
            this.nhanVienTreeList.Name = "nhanVienTreeList";
            this.nhanVienTreeList.OptionsBehavior.AllowIndeterminateCheckState = true;
            this.nhanVienTreeList.OptionsBehavior.Editable = false;
            this.nhanVienTreeList.OptionsBehavior.EnableFiltering = true;
            this.nhanVienTreeList.OptionsSelection.MultiSelect = true;
            this.nhanVienTreeList.OptionsSelection.UseIndicatorForSelection = true;
            this.nhanVienTreeList.OptionsView.ShowCheckBoxes = true;
            this.nhanVienTreeList.OptionsView.ShowSummaryFooter = true;
            this.nhanVienTreeList.Size = new System.Drawing.Size(309, 408);
            this.nhanVienTreeList.TabIndex = 0;
            this.nhanVienTreeList.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler(this.nhanVienTreeList_BeforeExpand);
            this.nhanVienTreeList.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.nhanVienTreeList_BeforeCheckNode);
            this.nhanVienTreeList.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.nhanVienTreeList_AfterCheckNode);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Danh sách cán bộ";
            this.treeListColumn1.FieldName = "HoTen";
            this.treeListColumn1.MinWidth = 32;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCheckAll,
            this.miCheckSelected,
            this.toolStripMenuItem1,
            this.miUncheckAll,
            this.miUncheckSelected});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(279, 98);
            // 
            // miCheckAll
            // 
            this.miCheckAll.Image = global::PSC_HRM.Module.Win.Properties.Resources.symbol_add;
            this.miCheckAll.Name = "miCheckAll";
            this.miCheckAll.Size = new System.Drawing.Size(278, 22);
            this.miCheckAll.Text = "Chọn tất cả cán bộ";
            this.miCheckAll.Click += new System.EventHandler(this.miCheckAll_Click);
            // 
            // miCheckSelected
            // 
            this.miCheckSelected.Image = global::PSC_HRM.Module.Win.Properties.Resources.plus;
            this.miCheckSelected.Name = "miCheckSelected";
            this.miCheckSelected.Size = new System.Drawing.Size(278, 22);
            this.miCheckSelected.Text = "Chọn những cán bộ được đánh dấu";
            this.miCheckSelected.Click += new System.EventHandler(this.miCheckSelected_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(275, 6);
            // 
            // miUncheckAll
            // 
            this.miUncheckAll.Image = global::PSC_HRM.Module.Win.Properties.Resources.symbol_delete;
            this.miUncheckAll.Name = "miUncheckAll";
            this.miUncheckAll.Size = new System.Drawing.Size(278, 22);
            this.miUncheckAll.Text = "Bỏ chọn tất cả cán bộ";
            this.miUncheckAll.Click += new System.EventHandler(this.miUncheckAll_Click);
            // 
            // miUncheckSelected
            // 
            this.miUncheckSelected.Image = global::PSC_HRM.Module.Win.Properties.Resources.edit_remove;
            this.miUncheckSelected.Name = "miUncheckSelected";
            this.miUncheckSelected.Size = new System.Drawing.Size(278, 22);
            this.miUncheckSelected.Text = "Bỏ chọn những cán bộ được đánh dấu";
            this.miUncheckSelected.Click += new System.EventHandler(this.miUncheckSelected_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.tinhTranglke);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btnAccept);
            this.panelControl1.Controls.Add(this.btnThoat);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 379);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(569, 29);
            this.panelControl1.TabIndex = 1;
            // 
            // tinhTranglke
            // 
            this.tinhTranglke.Location = new System.Drawing.Point(90, 4);
            this.tinhTranglke.Name = "tinhTranglke";
            this.tinhTranglke.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tinhTranglke.Properties.Appearance.Options.UseFont = true;
            this.tinhTranglke.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tinhTranglke.Properties.NullText = "Chọn tình trạng";
            this.tinhTranglke.Size = new System.Drawing.Size(310, 22);
            this.tinhTranglke.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(12, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 16);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Tình trạng";
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(406, 2);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Chọn";
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(487, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // frmChonCanBoLapQuyetDinhDaoTao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 408);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.splitContainerControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChonCanBoLapQuyetDinhDaoTao";
            this.Text = "Chọn cán bộ lập quyết định";
            this.Load += new System.EventHandler(this.frmChonCanBoLapQuyetDinhDaoTao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.donViTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nhanVienTreeList)).EndInit();
            this.menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tinhTranglke.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList donViTreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.TreeList nhanVienTreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem miCheckAll;
        private System.Windows.Forms.ToolStripMenuItem miCheckSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miUncheckAll;
        private System.Windows.Forms.ToolStripMenuItem miUncheckSelected;
        private DevExpress.XtraEditors.LookUpEdit tinhTranglke;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}