namespace PSC_HRM.Module.Win.Editors
{
    partial class frmChiTietCongThucTinhThuLao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChiTietCongThucTinhThuLao));
            this.trFields = new DevExpress.XtraTreeList.TreeList();
            this.colCaption = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colHienThi = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.trFields)).BeginInit();
            this.SuspendLayout();
            // 
            // trFields
            // 
            this.trFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trFields.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCaption,
            this.colHienThi});
            this.trFields.Location = new System.Drawing.Point(0, 0);
            this.trFields.Name = "trFields";
            this.trFields.OptionsBehavior.Editable = false;
            this.trFields.OptionsView.ShowIndicator = false;
            this.trFields.Size = new System.Drawing.Size(380, 296);
            this.trFields.TabIndex = 0;
            this.trFields.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler(this.trFields_BeforeExpand);
            this.trFields.DoubleClick += new System.EventHandler(this.trFields_DoubleClick);
            // 
            // colCaption
            // 
            this.colCaption.Caption = "Dữ liệu";
            this.colCaption.FieldName = "Caption";
            this.colCaption.Name = "colCaption";
            this.colCaption.OptionsColumn.AllowEdit = false;
            this.colCaption.Visible = true;
            this.colCaption.VisibleIndex = 0;
            this.colCaption.Width = 150;
            // 
            // colHienThi
            // 
            this.colHienThi.Caption = "Hiển thị";
            this.colHienThi.FieldName = "HienThi";
            this.colHienThi.Name = "colHienThi";
            this.colHienThi.OptionsColumn.AllowEdit = false;
            this.colHienThi.Visible = true;
            this.colHienThi.VisibleIndex = 1;
            this.colHienThi.Width = 70;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(212, 301);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(78, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Đồng ý";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(296, 301);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Không";
            // 
            // frmChiTietCongThucTinhThuLao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(380, 329);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.trFields);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChiTietCongThucTinhThuLao";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn dữ liệu";
            this.Load += new System.EventHandler(this.frmFormula_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trFields)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList trFields;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCaption;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colHienThi;
    }
}