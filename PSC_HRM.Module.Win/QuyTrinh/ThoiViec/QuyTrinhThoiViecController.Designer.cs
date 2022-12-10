namespace PSC_HRM.Module.Win.QuyTrinh.ThoiViec
{
    partial class QuyTrinhThoiViecController
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
            this.btnQDThoiViec = new PSC_HRM.Module.Win.QuyTrinh.CustomButton();
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
            this.btnHelp.Location = new System.Drawing.Point(461, 5);
            this.btnHelp.Size = new System.Drawing.Size(26, 22);
            // 
            // txtNotification
            // 
            this.txtNotification.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtNotification.Properties.Appearance.Options.UseForeColor = true;
            this.txtNotification.Size = new System.Drawing.Size(359, 20);
            // 
            // mainPanel
            // 
            this.mainPanel.ContentImage = global::PSC_HRM.Module.Win.Properties.Resources.QuyTrinhNghiViec;
            this.mainPanel.Controls.Add(this.btnQDThoiViec);
            this.mainPanel.Size = new System.Drawing.Size(492, 178);
            this.mainPanel.Controls.SetChildIndex(this.btnQDThoiViec, 0);
            this.mainPanel.Controls.SetChildIndex(this.btnBatDau, 0);
            this.mainPanel.Controls.SetChildIndex(this.btnKetThuc, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.Location = new System.Drawing.Point(2, 199);
            this.panelControl1.Size = new System.Drawing.Size(492, 32);
            // 
            // groupControl1
            // 
            this.groupControl1.Size = new System.Drawing.Size(496, 233);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(432, 5);
            // 
            // btnKetThuc
            // 
            this.btnKetThuc.Location = new System.Drawing.Point(414, 71);
            // 
            // btnBatDau
            // 
            this.btnBatDau.Location = new System.Drawing.Point(6, 71);
            // 
            // btnQDThoiViec
            // 
            this.btnQDThoiViec.BackColor = System.Drawing.Color.White;
            this.btnQDThoiViec.Caption = "Lập QĐ thôi việc";
            this.btnQDThoiViec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQDThoiViec.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(173)))), ((int)(((byte)(136)))));
            this.btnQDThoiViec.HoverColor = System.Drawing.Color.YellowGreen;
            this.btnQDThoiViec.Location = new System.Drawing.Point(198, 59);
            this.btnQDThoiViec.Name = "btnQDThoiViec";
            this.btnQDThoiViec.NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnQDThoiViec.Radial = 0;
            this.btnQDThoiViec.Size = new System.Drawing.Size(85, 61);
            this.btnQDThoiViec.State = PSC_HRM.Module.Win.QuyTrinh.CustomButton.ButtonState.Disable;
            toolTipTitleItem1.Appearance.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_Contract_32x32;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_Contract_32x32;
            toolTipTitleItem1.Text = "Quyết định thôi việc";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Lập quyết định thôi việc";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.toolTipController1.SetSuperTip(this.btnQDThoiViec, superToolTip1);
            this.btnQDThoiViec.TabIndex = 2;
            this.btnQDThoiViec.Click += new System.EventHandler(this.btnQDThoiViec_Click);
            // 
            // QuyTrinhThoiViecController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "QuyTrinhThoiViecController";
            this.Size = new System.Drawing.Size(496, 233);
            this.Load += new System.EventHandler(this.QuyTrinhNghiHuuController_Load);
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

        private CustomButton btnQDThoiViec;


    }
}
