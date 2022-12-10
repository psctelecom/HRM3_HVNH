namespace PSC_HRM.Module.Win.Forms
{
    partial class frmChonBoPhan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChonBoPhan));
            this.donViTreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miCheckSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miUncheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miUncheckSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.donViTreeList)).BeginInit();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // donViTreeList
            // 
            this.donViTreeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.donViTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.donViTreeList.ContextMenuStrip = this.menu;
            this.donViTreeList.Location = new System.Drawing.Point(0, 0);
            this.donViTreeList.Name = "donViTreeList";
            this.donViTreeList.OptionsBehavior.AllowIndeterminateCheckState = true;
            this.donViTreeList.OptionsBehavior.Editable = false;
            this.donViTreeList.OptionsBehavior.EnableFiltering = true;
            this.donViTreeList.OptionsSelection.MultiSelect = true;
            this.donViTreeList.OptionsSelection.UseIndicatorForSelection = true;
            this.donViTreeList.OptionsView.ShowCheckBoxes = true;
            this.donViTreeList.OptionsView.ShowIndicator = false;
            this.donViTreeList.Size = new System.Drawing.Size(283, 375);
            this.donViTreeList.TabIndex = 0;
            this.donViTreeList.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler(this.donViTreeList_BeforeExpand);
            this.donViTreeList.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.donViTreeList_BeforeCheckNode);
            this.donViTreeList.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.donViTreeList_AfterCheckNode);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Danh sách đơn vị";
            this.treeListColumn1.FieldName = "TenBoPhan";
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
            this.menu.Size = new System.Drawing.Size(276, 98);
            // 
            // miCheckAll
            // 
            this.miCheckAll.Image = global::PSC_HRM.Module.Win.Properties.Resources.symbol_add;
            this.miCheckAll.Name = "miCheckAll";
            this.miCheckAll.Size = new System.Drawing.Size(275, 22);
            this.miCheckAll.Text = "Chọn tất cả đơn vị";
            this.miCheckAll.Click += new System.EventHandler(this.miCheckAll_Click);
            // 
            // miCheckSelected
            // 
            this.miCheckSelected.Image = global::PSC_HRM.Module.Win.Properties.Resources.plus;
            this.miCheckSelected.Name = "miCheckSelected";
            this.miCheckSelected.Size = new System.Drawing.Size(275, 22);
            this.miCheckSelected.Text = "Chọn những đơn vị được đánh dấu";
            this.miCheckSelected.Click += new System.EventHandler(this.miCheckSelected_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(272, 6);
            // 
            // miUncheckAll
            // 
            this.miUncheckAll.Image = global::PSC_HRM.Module.Win.Properties.Resources.symbol_delete;
            this.miUncheckAll.Name = "miUncheckAll";
            this.miUncheckAll.Size = new System.Drawing.Size(275, 22);
            this.miUncheckAll.Text = "Bỏ chọn tất cả đơn vị";
            this.miUncheckAll.Click += new System.EventHandler(this.miUncheckAll_Click);
            // 
            // miUncheckSelected
            // 
            this.miUncheckSelected.Image = global::PSC_HRM.Module.Win.Properties.Resources.edit_remove;
            this.miUncheckSelected.Name = "miUncheckSelected";
            this.miUncheckSelected.Size = new System.Drawing.Size(275, 22);
            this.miUncheckSelected.Text = "Bỏ chọn những đơn vị được đánh dấu";
            this.miUncheckSelected.Click += new System.EventHandler(this.miUncheckSelected_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(202, 381);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Thoát";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton2.Location = new System.Drawing.Point(121, 381);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "Chọn";
            // 
            // frmChonBoPhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 412);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.donViTreeList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChonBoPhan";
            this.Text = "Danh sách đơn vị";
            this.Load += new System.EventHandler(this.frmBoPhan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.donViTreeList)).EndInit();
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList donViTreeList;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem miCheckAll;
        private System.Windows.Forms.ToolStripMenuItem miCheckSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miUncheckAll;
        private System.Windows.Forms.ToolStripMenuItem miUncheckSelected;
    }
}