namespace PSC_HRM.Module.Win.QuyTrinh.BoiDuong
{
    partial class QuyTrinhBoiDuongController
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
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.btnQDBoiDuong = new PSC_HRM.Module.Win.QuyTrinh.CustomButton();
            this.btnDangKyBoiDuong = new PSC_HRM.Module.Win.QuyTrinh.CustomButton();
            this.btnLapDanhSachBoiDuong = new PSC_HRM.Module.Win.QuyTrinh.CustomButton();
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
            this.btnHelp.Location = new System.Drawing.Point(468, 5);
            // 
            // txtNotification
            // 
            this.txtNotification.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtNotification.Properties.Appearance.Options.UseForeColor = true;
            this.txtNotification.Size = new System.Drawing.Size(370, 20);
            // 
            // mainPanel
            // 
            this.mainPanel.ContentImage = global::PSC_HRM.Module.Win.Properties.Resources.QuyTrinhBoiDuong;
            this.mainPanel.Controls.Add(this.btnQDBoiDuong);
            this.mainPanel.Controls.Add(this.btnLapDanhSachBoiDuong);
            this.mainPanel.Controls.Add(this.btnDangKyBoiDuong);
            this.mainPanel.Size = new System.Drawing.Size(499, 263);
            this.mainPanel.Controls.SetChildIndex(this.btnDangKyBoiDuong, 0);
            this.mainPanel.Controls.SetChildIndex(this.btnLapDanhSachBoiDuong, 0);
            this.mainPanel.Controls.SetChildIndex(this.btnQDBoiDuong, 0);
            this.mainPanel.Controls.SetChildIndex(this.btnBatDau, 0);
            this.mainPanel.Controls.SetChildIndex(this.btnKetThuc, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.Location = new System.Drawing.Point(2, 284);
            this.panelControl1.Size = new System.Drawing.Size(499, 32);
            // 
            // groupControl1
            // 
            this.groupControl1.Size = new System.Drawing.Size(503, 318);
            this.groupControl1.Text = "Quy trình bồi dưỡng";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(440, 5);
            // 
            // btnKetThuc
            // 
            this.btnKetThuc.Location = new System.Drawing.Point(263, 216);
            // 
            // btnBatDau
            // 
            this.btnBatDau.Location = new System.Drawing.Point(8, 36);
            // 
            // btnQDBoiDuong
            // 
            this.btnQDBoiDuong.BackColor = System.Drawing.Color.White;
            this.btnQDBoiDuong.Caption = "Lập QĐ bồi dưỡng";
            this.btnQDBoiDuong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQDBoiDuong.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(173)))), ((int)(((byte)(136)))));
            this.btnQDBoiDuong.HoverColor = System.Drawing.Color.YellowGreen;
            this.btnQDBoiDuong.Location = new System.Drawing.Point(404, 132);
            this.btnQDBoiDuong.Name = "btnQDBoiDuong";
            this.btnQDBoiDuong.NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnQDBoiDuong.Radial = 0;
            this.btnQDBoiDuong.Size = new System.Drawing.Size(85, 61);
            this.btnQDBoiDuong.State = PSC_HRM.Module.Win.QuyTrinh.CustomButton.ButtonState.Disable;
            toolTipTitleItem1.Appearance.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_Contract_32x32;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_Contract_32x32;
            toolTipTitleItem1.Text = "Quyết định công nhận đào tạo";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Lập quyết định công nhận các cán bộ hoàn thành đào tạo";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.toolTipController1.SetSuperTip(this.btnQDBoiDuong, superToolTip1);
            this.btnQDBoiDuong.TabIndex = 8;
            this.btnQDBoiDuong.Click += new System.EventHandler(this.btnQDBoiDuong_Click);
            // 
            // btnDangKyBoiDuong
            // 
            this.btnDangKyBoiDuong.BackColor = System.Drawing.Color.White;
            this.btnDangKyBoiDuong.Caption = "Đăng ký bồi dưỡng";
            this.btnDangKyBoiDuong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangKyBoiDuong.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(173)))), ((int)(((byte)(136)))));
            this.btnDangKyBoiDuong.HoverColor = System.Drawing.Color.YellowGreen;
            this.btnDangKyBoiDuong.Location = new System.Drawing.Point(116, 24);
            this.btnDangKyBoiDuong.Name = "btnDangKyBoiDuong";
            this.btnDangKyBoiDuong.NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnDangKyBoiDuong.Radial = 0;
            this.btnDangKyBoiDuong.Size = new System.Drawing.Size(85, 61);
            this.btnDangKyBoiDuong.State = PSC_HRM.Module.Win.QuyTrinh.CustomButton.ButtonState.Disable;
            toolTipTitleItem3.Appearance.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_BangTheoDoiViPham_32x32;
            toolTipTitleItem3.Appearance.Options.UseImage = true;
            toolTipTitleItem3.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_BangTheoDoiViPham_32x32;
            toolTipTitleItem3.Text = "Đăng ký đào tạo";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Các phòng/khoa đăng ký kế hoạch đào tạo";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.toolTipController1.SetSuperTip(this.btnDangKyBoiDuong, superToolTip3);
            this.btnDangKyBoiDuong.TabIndex = 9;
            this.btnDangKyBoiDuong.Click += new System.EventHandler(this.btnDangKyBoiDuong_Click);
            // 
            // btnLapDanhSachBoiDuong
            // 
            this.btnLapDanhSachBoiDuong.BackColor = System.Drawing.Color.White;
            this.btnLapDanhSachBoiDuong.Caption = "Duyệt đăng ký bồi dưỡng";
            this.btnLapDanhSachBoiDuong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLapDanhSachBoiDuong.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(173)))), ((int)(((byte)(136)))));
            this.btnLapDanhSachBoiDuong.HoverColor = System.Drawing.Color.YellowGreen;
            this.btnLapDanhSachBoiDuong.Location = new System.Drawing.Point(404, 24);
            this.btnLapDanhSachBoiDuong.Name = "btnLapDanhSachBoiDuong";
            this.btnLapDanhSachBoiDuong.NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnLapDanhSachBoiDuong.Radial = 0;
            this.btnLapDanhSachBoiDuong.Size = new System.Drawing.Size(85, 61);
            this.btnLapDanhSachBoiDuong.State = PSC_HRM.Module.Win.QuyTrinh.CustomButton.ButtonState.Disable;
            toolTipTitleItem2.Appearance.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_BangTheoDoiViPham_32x32;
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            toolTipTitleItem2.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_BangTheoDoiViPham_32x32;
            toolTipTitleItem2.Text = "Duyệt đăng ký bồi dưỡng";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Duyệt đăng ký bồi dưỡng";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.toolTipController1.SetSuperTip(this.btnLapDanhSachBoiDuong, superToolTip2);
            this.btnLapDanhSachBoiDuong.TabIndex = 9;
            this.btnLapDanhSachBoiDuong.Click += new System.EventHandler(this.btnLapDanhSachBoiDuong_Click);
            // 
            // QuyTrinhBoiDuongController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "QuyTrinhBoiDuongController";
            this.Size = new System.Drawing.Size(503, 318);
            this.Load += new System.EventHandler(this.QuyTrinhBoiDuongController_Load);
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

        private CustomButton btnQDBoiDuong;
        private CustomButton btnLapDanhSachBoiDuong;
        private CustomButton btnDangKyBoiDuong;
    }
}
