namespace PSC_HRM.Module.Win.QuyTrinh.ChuyenNgach
{
    partial class QuyTrinhChuyenNgachController
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.btnQDChuyenNgachLuong = new PSC_HRM.Module.Win.QuyTrinh.CustomButton();
            this.btnLapDeNghiChuyenNgach = new PSC_HRM.Module.Win.QuyTrinh.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotification.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(308, 6);
            // 
            // txtNotification
            // 
            this.txtNotification.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtNotification.Properties.Appearance.Options.UseForeColor = true;
            this.txtNotification.Size = new System.Drawing.Size(208, 20);
            // 
            // mainPanel
            // 
            this.mainPanel.ContentImage = global::PSC_HRM.Module.Win.Properties.Resources.QuyTrinhChuyenNgach;
            this.mainPanel.Controls.Add(this.btnQDChuyenNgachLuong);
            this.mainPanel.Controls.Add(this.btnLapDeNghiChuyenNgach);
            this.mainPanel.Size = new System.Drawing.Size(338, 299);
            this.mainPanel.Controls.SetChildIndex(this.btnLapDeNghiChuyenNgach, 0);
            this.mainPanel.Controls.SetChildIndex(this.btnQDChuyenNgachLuong, 0);
            this.mainPanel.Controls.SetChildIndex(this.btnBatDau, 0);
            this.mainPanel.Controls.SetChildIndex(this.btnKetThuc, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.Location = new System.Drawing.Point(2, 320);
            this.panelControl1.Size = new System.Drawing.Size(338, 32);
            // 
            // groupControl1
            // 
            this.groupControl1.Size = new System.Drawing.Size(342, 354);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(281, 6);
            // 
            // btnKetThuc
            // 
            this.btnKetThuc.Location = new System.Drawing.Point(92, 236);
            // 
            // btnBatDau
            // 
            this.btnBatDau.Location = new System.Drawing.Point(8, 21);
            // 
            // btnQDChuyenNgachLuong
            // 
            this.btnQDChuyenNgachLuong.BackColor = System.Drawing.Color.White;
            this.btnQDChuyenNgachLuong.Caption = "Lập QĐ chuyển ngạch lương";
            this.btnQDChuyenNgachLuong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQDChuyenNgachLuong.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(173)))), ((int)(((byte)(136)))));
            this.btnQDChuyenNgachLuong.HoverColor = System.Drawing.Color.YellowGreen;
            this.btnQDChuyenNgachLuong.Location = new System.Drawing.Point(230, 225);
            this.btnQDChuyenNgachLuong.Name = "btnQDChuyenNgachLuong";
            this.btnQDChuyenNgachLuong.NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnQDChuyenNgachLuong.Radial = 0;
            this.btnQDChuyenNgachLuong.Size = new System.Drawing.Size(85, 61);
            this.btnQDChuyenNgachLuong.State = PSC_HRM.Module.Win.QuyTrinh.CustomButton.ButtonState.Disable;
            toolTipTitleItem1.Appearance.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_Contract_32x32;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_Contract_32x32;
            toolTipTitleItem1.Text = "Quyết định chuyển ngạch lương";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Lập quyết định chuyển ngạch lương";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.toolTipController1.SetSuperTip(this.btnQDChuyenNgachLuong, superToolTip1);
            this.btnQDChuyenNgachLuong.TabIndex = 22;
            this.btnQDChuyenNgachLuong.Click += new System.EventHandler(this.btnQDChuyenNgachLuong_Click);
            // 
            // btnLapDeNghiChuyenNgach
            // 
            this.btnLapDeNghiChuyenNgach.BackColor = System.Drawing.Color.White;
            this.btnLapDeNghiChuyenNgach.Caption = "Lập đề nghị chuyển ngạch";
            this.btnLapDeNghiChuyenNgach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLapDeNghiChuyenNgach.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(173)))), ((int)(((byte)(136)))));
            this.btnLapDeNghiChuyenNgach.HoverColor = System.Drawing.Color.YellowGreen;
            this.btnLapDeNghiChuyenNgach.Location = new System.Drawing.Point(122, 9);
            this.btnLapDeNghiChuyenNgach.Name = "btnLapDeNghiChuyenNgach";
            this.btnLapDeNghiChuyenNgach.NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnLapDeNghiChuyenNgach.Radial = 0;
            this.btnLapDeNghiChuyenNgach.Size = new System.Drawing.Size(85, 61);
            this.btnLapDeNghiChuyenNgach.State = PSC_HRM.Module.Win.QuyTrinh.CustomButton.ButtonState.Disable;
            toolTipTitleItem2.Appearance.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_QuyetDinh_32x32;
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            toolTipTitleItem2.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_QuyetDinh_32x32;
            toolTipTitleItem2.Text = "Lập đề nghị chuyển ngạch lương";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Lập đề nghị chuyển ngạch lương";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.toolTipController1.SetSuperTip(this.btnLapDeNghiChuyenNgach, superToolTip2);
            this.btnLapDeNghiChuyenNgach.TabIndex = 21;
            this.btnLapDeNghiChuyenNgach.Click += new System.EventHandler(this.btnLapDeNghiChuyenNgach_Click);
            // 
            // QuyTrinhChuyenNgachController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "QuyTrinhChuyenNgachController";
            this.Size = new System.Drawing.Size(342, 354);
            this.Load += new System.EventHandler(this.QuyTrinhChuyenNgachController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNotification.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomButton btnQDChuyenNgachLuong;
        private CustomButton btnLapDeNghiChuyenNgach;
    }
}
