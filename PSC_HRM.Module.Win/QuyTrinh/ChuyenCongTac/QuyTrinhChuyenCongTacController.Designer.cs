namespace PSC_HRM.Module.Win.QuyTrinh
{
    partial class QuyTrinhChuyenCongTacController
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
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            this.btnGiayThoiTraLuong = new PSC_HRM.Module.Win.QuyTrinh.CustomButton();
            this.btnLapQDChuyenCongTac = new PSC_HRM.Module.Win.QuyTrinh.CustomButton();
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
            this.btnHelp.Location = new System.Drawing.Point(467, 6);
            // 
            // txtNotification
            // 
            this.txtNotification.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtNotification.Properties.Appearance.Options.UseForeColor = true;
            this.txtNotification.Size = new System.Drawing.Size(367, 20);
            // 
            // mainPanel
            // 
            this.mainPanel.ContentImage = global::PSC_HRM.Module.Win.Properties.Resources.QuyTrinhChuyenCongTac;
            this.mainPanel.Controls.Add(this.btnGiayThoiTraLuong);
            this.mainPanel.Controls.Add(this.btnLapQDChuyenCongTac);
            this.mainPanel.Size = new System.Drawing.Size(496, 181);
            this.mainPanel.Controls.SetChildIndex(this.btnLapQDChuyenCongTac, 0);
            this.mainPanel.Controls.SetChildIndex(this.btnGiayThoiTraLuong, 0);
            this.mainPanel.Controls.SetChildIndex(this.btnBatDau, 0);
            this.mainPanel.Controls.SetChildIndex(this.btnKetThuc, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.Location = new System.Drawing.Point(2, 202);
            this.panelControl1.Size = new System.Drawing.Size(496, 32);
            // 
            // groupControl1
            // 
            this.groupControl1.Size = new System.Drawing.Size(500, 236);
            this.groupControl1.Text = "Quy trình chuyển công tác";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(440, 6);
            // 
            // btnKetThuc
            // 
            this.btnKetThuc.Location = new System.Drawing.Point(415, 72);
            // 
            // btnBatDau
            // 
            this.btnBatDau.Location = new System.Drawing.Point(7, 72);
            // 
            // btnGiayThoiTraLuong
            // 
            this.btnGiayThoiTraLuong.BackColor = System.Drawing.Color.White;
            this.btnGiayThoiTraLuong.Caption = "In giấy thôi trả lương";
            this.btnGiayThoiTraLuong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGiayThoiTraLuong.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(173)))), ((int)(((byte)(136)))));
            this.btnGiayThoiTraLuong.HoverColor = System.Drawing.Color.YellowGreen;
            this.btnGiayThoiTraLuong.Location = new System.Drawing.Point(271, 60);
            this.btnGiayThoiTraLuong.Name = "btnGiayThoiTraLuong";
            this.btnGiayThoiTraLuong.NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnGiayThoiTraLuong.Radial = 0;
            this.btnGiayThoiTraLuong.Size = new System.Drawing.Size(85, 61);
            this.btnGiayThoiTraLuong.State = PSC_HRM.Module.Win.QuyTrinh.CustomButton.ButtonState.Disable;
            toolTipTitleItem4.Appearance.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_Contract_32x32;
            toolTipTitleItem4.Appearance.Options.UseImage = true;
            toolTipTitleItem4.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_Contract_32x32;
            toolTipTitleItem4.Text = "In giấy thôi trả lương";
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "In giấy thôi trả lương";
            superToolTip4.Items.Add(toolTipTitleItem4);
            superToolTip4.Items.Add(toolTipItem4);
            this.toolTipController1.SetSuperTip(this.btnGiayThoiTraLuong, superToolTip4);
            this.btnGiayThoiTraLuong.TabIndex = 22;
            this.btnGiayThoiTraLuong.Click += new System.EventHandler(this.btnGiayThoiTraLuong_Click);
            // 
            // btnLapQDChuyenCongTac
            // 
            this.btnLapQDChuyenCongTac.BackColor = System.Drawing.Color.White;
            this.btnLapQDChuyenCongTac.Caption = "Lập QĐ chuyển công tác";
            this.btnLapQDChuyenCongTac.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLapQDChuyenCongTac.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(173)))), ((int)(((byte)(136)))));
            this.btnLapQDChuyenCongTac.HoverColor = System.Drawing.Color.YellowGreen;
            this.btnLapQDChuyenCongTac.Location = new System.Drawing.Point(124, 60);
            this.btnLapQDChuyenCongTac.Name = "btnLapQDChuyenCongTac";
            this.btnLapQDChuyenCongTac.NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnLapQDChuyenCongTac.Radial = 0;
            this.btnLapQDChuyenCongTac.Size = new System.Drawing.Size(85, 61);
            this.btnLapQDChuyenCongTac.State = PSC_HRM.Module.Win.QuyTrinh.CustomButton.ButtonState.Disable;
            toolTipTitleItem3.Appearance.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_QuyetDinh_32x32;
            toolTipTitleItem3.Appearance.Options.UseImage = true;
            toolTipTitleItem3.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_QuyetDinh_32x32;
            toolTipTitleItem3.Text = "Lập đề nghị chuyển công tác";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Lập đề nghị chuyển công tác";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.toolTipController1.SetSuperTip(this.btnLapQDChuyenCongTac, superToolTip3);
            this.btnLapQDChuyenCongTac.TabIndex = 21;
            this.btnLapQDChuyenCongTac.Click += new System.EventHandler(this.btnLapQDChuyenCongTac_Click);
            // 
            // QuyTrinhChuyenCongTacController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "QuyTrinhChuyenCongTacController";
            this.Size = new System.Drawing.Size(500, 236);
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

        private CustomButton btnGiayThoiTraLuong;
        private CustomButton btnLapQDChuyenCongTac;
    }
}
